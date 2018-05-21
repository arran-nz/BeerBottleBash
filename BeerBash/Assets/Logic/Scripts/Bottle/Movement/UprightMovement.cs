using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bottle.MovementTypes
{
    public class UprightMovement
    {
        readonly BottleMovementConfiguration settings;

        float MaxUprightVelocity => settings.MaxUprightVelocity;
        float UprightAcceleration => settings.UprightAcceleration;

        float TurningSpeed => settings.TurningSpeed;

        float MaxAngularVelocity => settings.UprightMaxAngularVelocity;
        float AngularDampening => settings.UprightAngularDampening;

        public UprightMovement(BottleMovementConfiguration settings)
        {
            this.settings = settings;
        }

        public void ApplyMovementForces(Rigidbody rb, InputController input, SurfaceInfo surfaceInfo)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxUprightVelocity);

            rb.maxAngularVelocity = MaxAngularVelocity;
            rb.angularVelocity = Vector3.Slerp(rb.angularVelocity, Vector3.zero, Time.fixedDeltaTime * AngularDampening);

            Vector3 appliedForce = Vector3.zero;

            appliedForce += CalculateInputForce(rb, input, surfaceInfo.Normal);
            KeepUpright(rb, input, surfaceInfo.Normal);

            appliedForce += CalculateGroundFriction(rb.velocity);

            rb.AddForce(appliedForce, ForceMode.Acceleration);
        }

        #region Private Methods

        Vector3 AngledVectorFromSurfaceNormal(Vector3 surfaceNormal, Vector3 vector)
        {
            Quaternion difference = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
            return difference * vector;
        }

        Vector3 CalculateInputForce(Rigidbody rb, InputController input, Vector3 uprightDirection)
        {
            // Move relevative to the foward direction of the rigidbody
            Vector3 moveDir = rb.rotation * input.MovementDirection;

            // Adjust movement vector for angled surfaces
            Vector3 adjustedMovement
                = AngledVectorFromSurfaceNormal(uprightDirection, moveDir);

            // Multiply by force
            Vector3 force = adjustedMovement * UprightAcceleration;

            Debug.DrawRay(rb.position, adjustedMovement, Color.red);

            return force;
        }

        void KeepUpright(Rigidbody rb, InputController input, Vector3 uprightDirection)
        {
            Vector3 removeVerticalFromForward = new Vector3(rb.transform.forward.x, 0, rb.transform.forward.z);

            float lookAmount = input.LookDirection.x * Time.fixedDeltaTime * TurningSpeed;
            Quaternion lookDirection = Quaternion.Euler(0, lookAmount, 0);

            Vector3 desiredForward = lookDirection * removeVerticalFromForward;
            Vector3 adjustedDirection = AngledVectorFromSurfaceNormal(uprightDirection, desiredForward);

            Quaternion finalRot = Quaternion.LookRotation(adjustedDirection, uprightDirection);

            rb.rotation = finalRot;

            Debug.DrawRay(rb.position, desiredForward);
            Debug.DrawRay(rb.position, uprightDirection, Color.green);

        }

        Vector3 CalculateGroundFriction(Vector3 velocity)
        {
            Vector3 inverseVelocity = velocity * -1;
            Vector3 friction = inverseVelocity * 2f;
            return friction;
        }

        #endregion
    }
}
