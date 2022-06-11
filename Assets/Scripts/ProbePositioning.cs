using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbePositioning : MonoBehaviour
{
    
    public Transform Player, Probe, Mirror;
    public LayerMask LayerMask;
    public bool Reverse;
    RaycastHit hitInfo;
    Vector3 currDist;
  
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Mirror.position-Player.position;
        //Vector3 dir = new Vector3(d.x,Player.position.y,d.z);

        Ray ray = new Ray(Player.position, dir.normalized);
        Debug.DrawRay(ray.origin,ray.direction.normalized*20);
        if (Physics.Raycast(ray, out hitInfo, 200, LayerMask))
        {

            currDist = Vector3.Project(dir, hitInfo.normal);
            Probe.position = Player.position + (2 * currDist);



        }


    }
}
