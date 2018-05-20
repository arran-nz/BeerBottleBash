using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bottle.MovementTypes
{

    public class AirMovement
    {

        const float MaxAirVelocity = 30f;
        const float AirAcceleration = 10f;

        const float YawSpeed = 300f;
        const float RollSpeed = 350f;
        const float PitchSpeed = 350f;

        const float MaxAngularVelocity = 5.5f;
        const float AngularDampening = 4f;

        public void ApplyMovementForces(Rigidbody rb, InputController input)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxAirVelocity);
            Flipping(rb, input);
        }

        #region Private Methods

        void Yaw(Rigidbody rb, InputController input)
        {
            // TODO: have option to always face forward towards sky

            float yaw = input.MovementDirection.x * Time.fixedDeltaTime * YawSpeed;

            Vector3 y_angle = Vector3.up;

            rb.AddRelativeTorque(y_angle * yaw);
        }

        void PitchRoll(Rigidbody rb, InputController input)
        {


            float pitch = input.MovementDirection.z * Time.fixedDeltaTime * PitchSpeed;
            float roll = input.MovementDirection.x * Time.fixedDeltaTime * RollSpeed;

            Vector3 p_angle = Vector3.right;
            Vector3 r_angle = Vector3.back;

            rb.AddRelativeTorque(p_angle * pitch);
            rb.AddRelativeTorque(r_angle * roll);
        }

        private void Flipping(Rigidbody rb, InputController input)
        {
            if (input.FlipPressed)
            {
                Yaw(rb, input);
            }
            else
            {
                PitchRoll(rb, input);
            }

            rb.maxAngularVelocity = MaxAngularVelocity;

            rb.angularVelocity = Vector3.Slerp(
                rb.angularVelocity,
                Vector3.zero,
                Time.fixedDeltaTime * AngularDampening);
        }

        #endregion


    }
}
