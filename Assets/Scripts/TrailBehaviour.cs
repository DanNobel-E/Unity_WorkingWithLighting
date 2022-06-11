using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBehaviour : MonoBehaviour
{
    public Transform Root;
    public ParticleSystem PS;
    Transform bike;
    TrailRenderer tr;
    int prevSegments;
    BoxCollider currBox;
    GameObject currObj;
    void Start()
    {
        bike = transform.parent;
        tr = GetComponent<TrailRenderer>();
        prevSegments = 0;
        

    }
    private void OnEnable()
    {
        EventManager.OnDie.AddListener(OnDie);
    }

    private void OnDisable()
    {
        EventManager.OnDie.RemoveListener(OnDie);

    }

    private void OnDie(int index)
    {
        if (index == (int)bike.GetComponent<BikeMovement>().Player)
        {
            ParticleSystem.EmissionModule e = PS.emission;
            e.enabled = false;
            PS.TriggerSubEmitter(0);
        }
     }

    void Update()
    {


        if (tr.positionCount != 0)
        {




            int segments = tr.positionCount;
            if (segments > prevSegments)
            {
                

                prevSegments = segments;
                currObj = new GameObject();
                currObj.transform.SetParent(Root);
                currBox = currObj.AddComponent<BoxCollider>();
                Rigidbody rb = currObj.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                Vector3 pos;

                if (segments > 1)
                    pos = (tr.GetPosition(segments - 1)+tr.GetPosition(segments-2))*0.5f;
                else
                    pos = Root.position;

                currObj.transform.position = pos;
                currBox.size = new Vector3(0, transform.localScale.y * 0.5f, 0);

            }
            else
            {
                Vector3 pos;

                if (segments > 1)
                    pos = (tr.GetPosition(segments - 1) + tr.GetPosition(segments - 2)) * 0.5f;
                else
                    pos = Root.position;

                currObj.transform.position = pos;
                Vector3 dir = bike.position - currObj.transform.position;
                float dist = dir.magnitude*0.75f;
                Vector3 size = new Vector3(0, transform.localScale.y * 0.5f, dist);
                currBox.size = size;

                Quaternion rot;
                int index = currObj.transform.GetSiblingIndex() - 1;
                if (index >= 0)
                {
                   Quaternion rot1= Root.GetChild(index).rotation;
                   Quaternion rot2= Quaternion.LookRotation(dir.normalized, Vector3.up);
                    rot = Quaternion.Slerp(rot1,rot2,0.5f);
                }
                else
                {

                    rot = Quaternion.LookRotation(dir.normalized, Vector3.up);
                }

                currObj.transform.rotation = rot;


            }

        }

    }
}
