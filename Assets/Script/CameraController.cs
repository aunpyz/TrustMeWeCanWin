using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Animator theCameraShake;
    void Start()
    {

    }

    void Update()
    {

    }


    public void CameraShake()
    {
        theCameraShake.SetTrigger("CameraShake");
    }
}
