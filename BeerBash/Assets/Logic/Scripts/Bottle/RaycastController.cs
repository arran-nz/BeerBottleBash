using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceInfo
{
    public bool RaycastOriginResult;
    public float Friction;
    public Vector3 Normal;
    public float Pitch;
}

public class RaycastController : MonoBehaviour {

    public Transform RaycastOrigin;
    public float RayDistance = 0.25f;
    public LayerMask GroundLayer;

    public SurfaceInfo GetSurfaceInfo(CapsuleCollider capsule)
    {
        RaycastHit hit;
        bool rayResult = Physics.Raycast(RaycastOrigin.position, capsule.transform.up * -1, out hit, RayDistance, GroundLayer);

        // if the first ray fails to return a result, check the overlapping capsule
        if(!rayResult)
        {
            Vector3 p1 = capsule.transform.position + capsule.center + Vector3.up * -capsule.height * 0.5F;
            Vector3 p2 = p1 + Vector3.up * capsule.height;
            //Physics.CapsuleCast(p1, p2, capsule.radius * 1.05f,new Vector3(0,1,0), out hit, 10f, GroundLayer);
            Collider[] colliders = Physics.OverlapCapsule(p1, p2, capsule.radius * 1.05f, GroundLayer);

            if(colliders.Length != 0)
            {
                RaycastHit capsuleHit;
                Physics.Raycast(capsule.transform.position, Vector3.down, out capsuleHit, capsule.radius * 1.05f, GroundLayer);
                hit = capsuleHit;
            }

        }


        float friction = 0;
        if(hit.collider)
        {
            friction = hit.collider.material.dynamicFriction;
        }

        SurfaceInfo hitInfo =
            new SurfaceInfo
            {
                RaycastOriginResult = rayResult,
                Friction = friction,
                Normal = hit.normal,
                Pitch = GetSurfacePitch(hit.normal)
            };

        return hitInfo;
    }

    private float GetSurfacePitch(Vector3 normal)
    {
        return Vector3.Angle(normal, Vector3.up);
    }
   
}
