using UnityEngine;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(Animator))]
public class KS_PlayerMovement : MonoBehaviour
{
    public float WalkSpeed = 5f, RunIncrementer = 1.5f;

    private CharacterController m_controller;
    private Animator m_animator;

    [SerializeField] private Vector3 Velocity
    {
        get
        {
            return moveInputs;
        }
        set
        {
            value.x = moveInputs.x;
            value.z = moveInputs.z;
        }
    }

    [Header("Move Inputs :")]
    [SerializeField] private Vector3 moveInputs;

    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
    }

    public float GetSpeed() { return WalkSpeed; }
    
    public float GetRunSpeed() { return WalkSpeed * RunIncrementer; }

    public bool IsRunning() { return Input.GetKeyDown(KeyCode.LeftShift) 
            ||  Input.GetKeyDown(KeyCode.RightShift);}


    private void Update()
    {
        // TODO : Sys pour courir

        moveInputs.x = Input.GetAxis("Horizontal");
        moveInputs.z = Input.GetAxis("Vertical");

        m_controller.Move(Velocity * GetSpeed() * Time.deltaTime);

        HandlePlayerAnimations();
    }

    private void HandlePlayerAnimations()
    {
        m_animator.SetBool("Idle", m_controller.velocity.magnitude <= 0);

        if (Velocity != null)
        {
            m_animator.SetBool("Forward", m_controller.velocity.x > .5f || m_controller.velocity.z > .5f);
            m_animator.SetBool("Forward", moveInputs.x >= 1 || moveInputs.z >= 1);
            m_animator.SetBool("Backward", moveInputs.x < 1);
        }
    }
}