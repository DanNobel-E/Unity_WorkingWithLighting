using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSCustomData : MonoBehaviour {
    ParticleSystem ps;
    public Gradient newGradientCol;

    void Start () {
        ps = GetComponent<ParticleSystem>();
        ParticleSystem.CustomDataModule customData = ps.customData;
        customData.enabled = true;

        /*If you want to specify a Gradient by yourself
        Gradient newGradientCol = new Gradient();
        newGradientCol.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) },
                                                        new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        */
        customData.SetMode(ParticleSystemCustomData.Custom2, UnityEngine.ParticleSystemCustomDataMode.Color);
        customData.SetColor(ParticleSystemCustomData.Custom2, newGradientCol);
    }

    void LateUpdate()
    {
        //Get all the alive particles
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.main.maxParticles];
        int aliveParticles = ps.GetParticles(particles);

        //The order of custom per-particle data returned by GetCustomParticleData() is the same of GetParticles()
        /*  This is useful, because you know that if CustomData is a curve from 0 (at particle initial lifetime) to 10 (at particle end lifetime),
         *  if p[i] is a particle at normalized lifetime 0.5 (half its lifetime), it will get a CustomData value of 5
         */
        List<Vector4> customData1 = new List<Vector4>();
        ps.GetCustomParticleData(customData1, ParticleSystemCustomData.Custom1);
        List<Vector4> customData2 = new List<Vector4>();
        ps.GetCustomParticleData(customData2, ParticleSystemCustomData.Custom2);

        for (int i = 0; i < aliveParticles; i++)
        {
            ParticleSystem.Particle p = particles[i];

            p.rotation3D = new Vector3(customData1[i].x, customData1[i].y, customData1[i].z);
            p.startColor = new Color(customData2[i].x, customData2[i].y, customData2[i].z, customData2[i].w);
            particles[i] = p;
        }

        ps.SetParticles(particles, aliveParticles);
    }
}
