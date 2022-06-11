using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LightProbeGroup))]
[ExecuteInEditMode]
public class LPAdder : MonoBehaviour
{
    public Transform NewLPPos;
    public bool AddLP;
    LightProbeGroup lpg;

    void Start()
    {
        lpg = GetComponent<LightProbeGroup>();
    }

    void Update()
    {
        if (AddLP)
        {
            AddLP = false;
            List<Vector3> lprobes = new List<Vector3>(lpg.probePositions);
            //We need to provide relative position
            lprobes.Add(NewLPPos.position - lpg.transform.position);
            lpg.probePositions = lprobes.ToArray();
        }
    }
}
