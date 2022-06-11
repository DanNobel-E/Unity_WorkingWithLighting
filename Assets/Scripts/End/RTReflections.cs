using UnityEngine;
using System.Collections;

public class RTReflections : MonoBehaviour
{

    ReflectionProbe probe;
    Vector3 basePos;
    Vector3 offsetVector;

    public Camera Cam;
    public Transform reflectiveObj;
    public bool flipX;
    public bool flipY;
    public bool flipZ;

    void Awake()
    {
        probe = GetComponent<ReflectionProbe>();
        basePos = reflectiveObj.transform.position;
    }

    void Update()
    {
        Vector3 camPos = Cam.transform.position;
        float dx = camPos.x - basePos.x;
        float dy = camPos.y - basePos.y;
        float dz = camPos.z - basePos.z;
        float offsetVectorX = flipX ? (dx > 0 ? basePos.x + dx : basePos.x - dx) : camPos.x;
        float offsetVectorY = flipY ? (dy > 0 ? basePos.y - dy : basePos.y + dy) : camPos.y;
        float offsetVectorZ = flipZ ? (dz > 0 ? basePos.z + dz : basePos.z - dz) : camPos.z;
        offsetVector = new Vector3(offsetVectorX, offsetVectorY, offsetVectorZ);
        probe.transform.position = offsetVector;

        probe.RenderProbe();
    }
}