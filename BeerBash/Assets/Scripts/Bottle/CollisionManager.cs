using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionManager : MonoBehaviour {

    public GameObject GroundEffect;
    public bool Grounded { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGround(collision))
        {
            Grounded = true;

            if (collision.relativeVelocity.y > 10)
            {
                ImpactEffect(collision.contacts[0]);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (IsGround(collision))
        {
            Grounded = false;
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
