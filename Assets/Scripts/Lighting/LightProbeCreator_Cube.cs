using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightProbeGroup))]
[ExecuteInEditMode]
public class LightProbeCreator_Cube : MonoBehaviour
{
    public Vector3 ProbsNumber;
    public Vector3 ProbsSize;
    public bool UseMesh;
    public float ProbsDistance;
    [Range(1f,100f)]
    public int VertexStep=1;
    LightProbeGroup lightProbeGroup;
    List<Vector3> probePositions= new List<Vector3>();
    List<Vector3> normals = new List<Vector3>();
    Vector3 Offsets;

    // Start is called before the first frame update
    void Start()
    {
        lightProbeGroup = GetComponent<LightProbeGroup>();
    }

    private void OnValidate()
    {
        probePositions.Clear();
        if (UseMesh)
        {
            Mesh m = GetComponent<MeshFilter>().sharedMesh;
            for (int i = 0; i < m.vertices.Length; i += VertexStep)
            {
                Vector3 pos = m.vertices[i];
                Vector3 normal = m.normals[i];
                if (transform.position.y+pos.y <= 0)
                {
                    if (normal.y < 0)
                        continue;
                    else
                        pos = new Vector3(pos.x, pos.y + 0.25f, pos.z);
                }
                    
                Vector3 probePos = pos + (normal * ProbsDistance);
                probePositions.Add(probePos);

            }


        }
        else
        {




            Offsets = new Vector3(ProbsSize.x / ProbsNumber.x, ProbsSize.y / ProbsNumber.y, ProbsSize.z / ProbsNumber.z);
            float sizeX = ProbsSize.x * 0.5f;
            float sizeY = ProbsSize.y * 0.5f;
            float sizeZ = ProbsSize.z * 0.5f;


            for (float x = -sizeX + 0.75f; x < sizeX + 0.75f; x += Offsets.x)
            {

                for (float y = 0.5f; y < sizeY * 2 + 0.5f; y += Offsets.y)
                {
                    for (float z = -sizeZ + 0.75f; z < sizeZ + 0.75f; z += Offsets.z)
                    {
                        Vector3 pos = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z);
                        probePositions.Add(pos);
                    }
                }
            }


        }
            if (lightProbeGroup != null)
                lightProbeGroup.probePositions = probePositions.ToArray();
    }
}
