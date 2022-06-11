using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfFacingCamera : MonoBehaviour
{
    public GameObject GObjToHide;
    public Camera Cam;

    void Update()
    {
        GObjToHide.SetActive(Vector3.Dot(Cam.transform.position - transform.position, transform.forward) < 0);
    }
}
