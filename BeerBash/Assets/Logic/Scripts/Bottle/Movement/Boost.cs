using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bottle.MovementTypes
{
    public class Boost
    {
        readonly BottleMovementConfiguration settings;

        float BoostAcceleration => settings.BoostAcceleration;

        public Boost(BottleMovementConfiguration settings)
        {
            this.settings = settings;
        }

        // Update is called once per frame
        public void ApplyBoost(Rigidbody rb)
        {
            Vector3 boostDirection = rb.transform.up * -1;
            Vector3 force = boostDirection * BoostAcceleration;

            rb.AddForce(force, ForceMode.Acceleration);
        }

    }
}
