using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeCollisions : MonoBehaviour
{
    public string HAxis = "Horizontal";
    public float SpeedR = 1;
    Transform bike;
    RaycastHit hitInfo;
    bool canCollide = false;
    float stopSign;
    bool stop;
    Animator anim;
    private void Start()
    {
        bike = transform.parent;
        anim = bike.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.OnDie.AddListener(OnDie);
    }

    private void OnDisable()
    {
        EventManager.OnDie.RemoveListener(OnDie);

    }


    private void Update()
    {
        Ray ray = new Ray(bike.position, bike.forward);

        if (Physics.Raycast(ray, out hitInfo, 3))
            canCollide = true;

        float rVal = -Input.GetAxis(HAxis);

        float rotX = rVal * 45 * SpeedR * Time.deltaTime;

        float sign = Mathf.Sign(rotX);

        if (sign == 0)
            sign = stopSign;

        if (transform.localEulerAngles.x + Mathf.Abs(rotX) > 315)
        {
            if (!stop)
            {
                stop = true;
                stopSign = sign;
            }
        }


        if (stop)
        {

            if (sign != stopSign)
            {
                transform.Rotate(rotX, 0, 0);
                stop = false;

            }

        }
        else
            transform.Rotate(rotX, 0, 0);





    }


    public void OnDie(int index)
    {
        if (index == (int)bike.GetComponent<BikeMovement>().Player)
        {
            anim.SetTrigger("Death");
            SpeedR = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canCollide)
            EventManager.OnDie.Invoke((int)bike.GetComponent<BikeMovement>().Player);

    }
}

