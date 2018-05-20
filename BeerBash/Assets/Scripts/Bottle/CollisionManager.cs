using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionManager : MonoBehaviour {

    public LayerMask Ground;
    public GameObject GroundEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Ground")
            && collision.relativeVelocity.y > 10)
        {
            if (GroundEffect.GetComponent<ParticleSystem>())
            {
                ContactPoint contact = collision.contacts[0];

                Vector3 spawnPos = contact.point;
                spawnPos.y -= contact.separation;

                Quaternion rot =  Quaternion.FromToRotation(Vector3.up, contact.normal);

                Instantiate(GroundEffect, spawnPos, rot);
            }
        }
    }
}
