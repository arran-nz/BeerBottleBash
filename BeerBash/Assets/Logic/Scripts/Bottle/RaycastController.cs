using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceInfo
{
    public Vector3 Normal;
    public float Pitch;
}

public class RaycastController : MonoBehaviour {

    public Transform RaycastOrigin;
    public float RayDistance = 0.25f;
    public LayerMask GroundLayer;

    public SurfaceInfo GetSurfaceInfo()
    {
        RaycastHit hit;
        Physics.Raycast(RaycastOrigin.position, transform.up * -1, out hit, RayDistance, GroundLayer);

        SurfaceInfo info =
            new SurfaceInfo
            {
                Normal = hit.normal,
                Pitch = GetSurfacePitch(hit.normal)
            };

        return info;
    }

    private float GetSurfacePitch(Vector3 normal)
    {
        return Vector3.Angle(normal, Vector3.up);
    }
   
}
