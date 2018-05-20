using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bottle.MovementTypes
{
    public class Jump
    {
        const float JumpHeight = 5f;

        public void ApplyJump(Rigidbody rb)
        {
            Vector3 force = GetJump(Vector3.up);
            rb.AddForce(force, ForceMode.VelocityChange);
        }

        Vector3 GetJump(Vector3 direction)
        {
            Vector3 jumpForce = direction * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
            return jumpForce;
        }
    }
}
