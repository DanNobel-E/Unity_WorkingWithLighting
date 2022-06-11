using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTriggers : MonoBehaviour
{
    public float velMul;
    public bool ModifyEnter, ModifyExit, ModifyOutside;

    public void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        // particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> outside = new List<ParticleSystem.Particle>();

        // get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        int numOutSide = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);

        // iterate
        if (ModifyEnter)
        {
            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = new Color32(255, 0, 0, 255);
                p.velocity = p.velocity * velMul;
                enter[i] = p;
            }
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        }
        if (ModifyExit)
        {
            for (int i = 0; i < numExit; i++)
            {
                ParticleSystem.Particle p = exit[i];
                p.startColor = new Color32(0, 255, 0, 255);
                p.velocity = p.velocity / velMul;
                exit[i] = p;
            }
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        }
        if (ModifyOutside)
        {
            for (int i = 0; i < numOutSide; i++)
            {
                ParticleSystem.Particle p = outside[i];
                p.startColor = new Color32(0, 255, 255, 255);
                outside[i] = p;
            }
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);
        }
    }
}
