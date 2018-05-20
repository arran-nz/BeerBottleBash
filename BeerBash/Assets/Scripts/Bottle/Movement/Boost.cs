using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bottle.MovementTypes
{
    public class Boost
    {
        const float BoostAccerleration = 50f;

        // Update is called once per frame
        public void ApplyBoost(Rigidbody rb)
        {
            Vector3 boostDirection = rb.transform.up * -1;
            Vector3 force = boostDirection * BoostAccerleration;

            rb.AddForce(force, ForceMode.Acceleration);
        }

    }
}
