using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player { P1,P2}
public class BikeMovement : MonoBehaviour
{
    public Player Player;
    public KeyCode SwitchCamera;
    public Camera Cam;
    CamBehaviour[] behaviours;
    public string HAxis = "Horizontal";
    public string VAxis = "Vertical";

    public float SpeedT = 1;
    public float SpeedR = 1;

    private void OnEnable()
    {
        EventManager.OnDie.AddListener(OnDie);

        behaviours = Cam.GetComponents<CamBehaviour>();

    }

    private void OnDie(int index)
    {
        if (index == (int)Player)
        {
            SpeedT = 0;
            SpeedR = 0;
        }
    }

    private void OnDisable()
    {
        EventManager.OnDie.RemoveListener(OnDie);

    }

    void Update()
    {
       float yRotVal= Input.GetAxis(HAxis)*SpeedR*Time.deltaTime;
       float xVal = Input.GetAxis(VAxis) * SpeedT * Time.deltaTime;
        Quaternion rotYaw = Quaternion.Euler(0, yRotVal, 0);
       
        if (xVal>=0)
        transform.position += transform.forward*xVal;

        transform.rotation *= rotYaw;

        if (Input.GetKeyDown(SwitchCamera))
        {
            for (int i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].enabled = !behaviours[i].enabled;
            }
        }

    }


}
