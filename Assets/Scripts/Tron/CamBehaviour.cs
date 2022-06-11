using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehaviour : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public Vector3 RotOffset;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = Target.position + Offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //transform.position = Target.position + (transform.forward*Offset.magnitude);
        Quaternion rot = Target.rotation;
        transform.position = rot*(Offset+RotOffset);
        transform.position = Target.position + transform.position;

        Vector3 dir = ((Target.position+RotOffset)- transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        
    }
}
