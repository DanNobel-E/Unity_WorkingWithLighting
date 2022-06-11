using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveTRStrafe : MonoBehaviour
{
    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    void Update()
    {
        float strafe = Input.GetAxis("Strafe") * speed;
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        strafe *= Time.deltaTime;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(strafe, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}
