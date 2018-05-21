using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionManager : MonoBehaviour {

    public GameObject GroundEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGround(collision))
        {
            if (collision.relativeVelocity.y > 10)
            {
                ImpactEffect(collision.contacts[0]);
            }
        }
    }

    bool IsGround(Collision collision)
    {
        return collision.transform.gameObject.layer == LayerMask.NameToLayer("Ground");
    }

    void ImpactEffect(ContactPoint contact)
    {
        Vector3 spawnPos = contact.point;
        spawnPos.y -= contact.separation;

        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);

        Instantiate(GroundEffect, spawnPos, rot);
    }
}
