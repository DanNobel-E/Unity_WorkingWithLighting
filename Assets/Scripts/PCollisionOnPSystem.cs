using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCollisionOnPSystem : MonoBehaviour {
    public ParticleSystem PSystem;
    public List<ParticleCollisionEvent> collisionEvents;
    public bool detailedInfo;

    void Start () {
        PSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    //When OnParticleCollision is invoked from a script attached to a ParticleSystem,
    //  the GameObject parameter represents a GameObject with an attached Collider hitted by the ParticleSystem
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("I'm the Particle System. I just hitted " + other.name + " with my particles");
        if (!detailedInfo) return;

        int numCollisionEvents = PSystem.GetCollisionEvents(other, collisionEvents);
        Debug.Log("I'm the Particle System. numCollisionEvents: " + numCollisionEvents);
        
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
