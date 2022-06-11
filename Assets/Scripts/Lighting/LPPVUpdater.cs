using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightProbeProxyVolume))]
public class LPPVUpdater : MonoBehaviour
{
    public bool UpdateLPPV;
    LightProbeProxyVolume lppv;
    void Start()
    {
        lppv = GetComponent<LightProbeProxyVolume>();
    }

    void Update()
    {
        if (UpdateLPPV)
        {
            UpdateLPPV = false;
            lppv.Update();
        }
    }
}
