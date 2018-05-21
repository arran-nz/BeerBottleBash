using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bottle.MovementTypes
{
    public class Jump
    {
        readonly BottleMovementConfiguration settings;

        float JumpHeight => settings.JumpHeight;

        public Jump(BottleMovementConfiguration settings)
        {
            this.settings = settings;
        }

        public void ApplyJump(Rigidbody rb, Vector3 jumpDir)
        {
            Vector3 force = GetJump(jumpDir);
            rb.AddForce(force, ForceMode.VelocityChange);
        }

        Vector3 GetJump(Vector3 direction)
        {
            Vector3 jumpForce = direction * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
            return jumpForce;
        }
    }
}
