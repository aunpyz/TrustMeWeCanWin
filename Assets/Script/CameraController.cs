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
        int randomNumber = Random.Range(0, 3);
        if (randomNumber == 0)
            theCameraShake.SetTrigger("CameraShake");
        else if (randomNumber == 1)
            theCameraShake.SetTrigger("CameraShake1");
        else if (randomNumber == 2)
            theCameraShake.SetTrigger("CameraShake2");
    }
}
