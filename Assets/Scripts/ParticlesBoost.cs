using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlesBoost : MonoBehaviour
{
    public float BoostVel = 1;
    public bool PerformBoost;
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;

    private void LateUpdate()
    {
        if (!PerformBoost) return;
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            m_Particles[i].velocity *= BoostVel;
        }

        // Apply the particle changes to the particle system
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
    }
}
