using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCollisionOnGObj : MonoBehaviour {
    public ParticleSystem PSystem;
    public List<ParticleCollisionEvent> collisionEvents;
    public bool detailedInfo;

    void Start () {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    //When OnParticleCollision is invoked from a script attached to a GameObject with a Collider,
    //  the GameObject parameter represents the ParticleSystem.
    void OnParticleCollision(GameObject other)
    {
        PSystem = other.GetComponent<ParticleSystem>();
        Debug.Log("I'm the GameObject. I've been hitted from " + other.name + " PSystem");
        if (!detailedInfo) return;

        int numCollisionEvents = PSystem.GetCollisionEvents(gameObject, collisionEvents);
        Debug.Log("I'm the GameObject. numCollisionEvents: " + numCollisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            float velMagnitudeNormalized = collisionEvents[i].velocity.magnitude / PSystem.main.startSpeed.constantMax;
            //Retrive position, normal and velocity of the particle impact on the gameObject
            Debug.DrawRay(collisionEvents[i].intersection,
                collisionEvents[i].normal * velMagnitudeNormalized * 4,
                Color.Lerp(Color.green, Color.red, velMagnitudeNormalized), 1);
        }
    }
}
