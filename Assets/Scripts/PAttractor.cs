using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttractor : MonoBehaviour
{
    public Transform Attractor;
    public float Strength;
    ParticleSystem ps;
    ParticleSystem.Particle[] particles;
    int pAlive;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    // Update is called once per frame
    void Update()
    {
        pAlive = ps.GetParticles(particles);

        for (int i = 0; i < pAlive; i++)
        {
            Vector3 dir = (Attractor.position - particles[i].position).normalized;

            particles[i].velocity += dir * Strength;
        }

        ps.SetParticles(particles);
    }
}
