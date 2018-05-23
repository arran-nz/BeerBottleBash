using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideExplode : MonoBehaviour {

    public GameObject Explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.sqrMagnitude > 1)
        {
            GetComponent<Collider>().isTrigger = true;

                       
            collision.collider.GetComponent<Rigidbody>().AddExplosionForce(70f, transform.position, 1f, 0.3f, ForceMode.Impulse);

            Explode();


        }
    }

    public void Explode()
    {
        ImpactEffect();
        Destroy(gameObject);
    }

    void ImpactEffect()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y += 1.4f;

        Quaternion rot = Quaternion.Euler(Vector3.up);

        Instantiate(Explosion, spawnPos, rot);
    }
}
