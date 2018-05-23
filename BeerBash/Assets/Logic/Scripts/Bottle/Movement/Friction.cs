using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bottle.PhysicalProperties
{
    public class Friction
    {
        public void ApplyFriction(float amount, Rigidbody rb)
        {
            Vector3 friction = CalculateGroundFriction(rb.velocity, amount);
            rb.AddForce(friction, ForceMode.Force);
        }

        Vector3 CalculateGroundFriction(Vector3 velocity, float amount)
        {
            Vector3 inverseVelocity = velocity * -1;
            return inverseVelocity * amount;
        }
    }
}
