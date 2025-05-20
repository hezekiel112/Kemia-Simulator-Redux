// CHANGE LOG
// 
// CHANGES || version VERSION
//
// "Enable/Disable Headbob, Changed look rotations - should result in reduced camera jitters" || version 1.0.1

using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Services.Lobbies;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : NetworkBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Vector3[] spawnPositions;

    public CharacterController characterController;

    [SerializeField] private Animator animator;

    [Space]
    [SerializeField] private TextMeshProUGUI username;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!IsOwner)
        {
            playerCamera.enabled = false;
            return;
        }
    }

    public bool IsMoving() { return characterController.velocity != Vector3.zero; }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        SpawnPlayerServerRpc(new(19, 4, 0));
        meshRenderer.material = GameManager.Instance.GetRandomMaterial(0);

        KS_NetworkHUDManager.Instance.UpdateUsername(username);
    }


    void Update()
    {
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnPlayerServerRpc(new(17, 4, 0));
            return;
        }

        animator.SetFloat("Horizontal", characterController.velocity.x);
        animator.SetFloat("Vertical", characterController.velocity.z);
        #endregion
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(Vector3 v3)
    {
        transform.position = v3;
    }
}