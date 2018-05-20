using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SurfaceInformatiom
{
    public bool Solid;
    public Vector3 Normal;
    public float Pitch;
}

public class RaycastController : MonoBehaviour {

    public Transform RaycastOrigin;
    public float RayDistance = 0.25f;
    public float UprightMaxAngle = 45f;
    public LayerMask GroundLayer;

    public bool Touching;
    public bool Upright;

    public SurfaceInformatiom GetSurfaceInfo()
    {
        RaycastHit hit;
        bool surfacePresent = Physics.Raycast(RaycastOrigin.position, transform.up * -1, out hit, RayDistance, GroundLayer);

        SurfaceInformatiom info =
            new SurfaceInformatiom
            {
                Solid = surfacePresent,
                Normal = hit.normal,
                Pitch = GetSurfacePitch(hit.normal)
            };

        return info;
    }

    private float GetSurfacePitch(Vector3 normal)
    {
        return Vector3.Angle(normal, Vector3.up);
    }

    private bool CheckIfTouching()
    {
        return Physics.CheckSphere(RaycastOrigin.position, RayDistance, GroundLayer);
    }
    

	void FixedUpdate () {

        if (RaycastOrigin != null)
        {
            Touching = CheckIfTouching();

            SurfaceInformatiom current = GetSurfaceInfo();
           // Touching = current.Solid;

            if(current.Pitch <= UprightMaxAngle)
            {
                Upright = true;
            }
            else
            {
                Upright = false;
            }
        }
	}
}
