using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class KS_CameraFollowMouse : MonoBehaviour
{
    [Header("Ce script sert a faire bouger un objet. \nÉtant basé sur le pointeur de la souris"),
        SerializeField]
    private bool _onlyTranslateObject = true;
    [Foldout("Réferences"), SerializeField, Tooltip("Le temps avant de rafraichir la position de l'objet\nsur la pointeur")]
    private int _translateObjectRefresh = 1;

    [Foldout("Réferences"), SerializeField, Tooltip("L'objet qui va être attiré sur la pointeur")] 
    private Transform target;

    [Foldout("Réferences"), SerializeField, Tooltip("Adoucir le mouvement de la caméra, 0 pour un mouvement directe et brute, > 1 pour \n un mouvement lent et doux")]
    private float smoothLerp = 3;

    [Foldout("Réferences"), SerializeField]
    private CinemachineCameraOffset cameraOffset;

    WaitForSeconds TranlateObjectWaitTime => new((int)_translateObjectRefresh);


    private void Start()
    {
        if (_onlyTranslateObject)
        {
            StartCoroutine(nameof(OnlyTranslateObject));
            cameraOffset = GetComponent<CinemachineCameraOffset>();
        }
    }

    private IEnumerator OnlyTranslateObject()
    {
        do
        {
            cameraOffset.m_Offset.y = Mathf.Lerp(cameraOffset.m_Offset.y, Input.mousePosition.normalized.y, smoothLerp);


            yield return TranlateObjectWaitTime;
        }
        while (true);
    }
}