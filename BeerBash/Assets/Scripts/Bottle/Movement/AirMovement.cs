using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovement : MonoBehaviour {

    public float MaxAirVelocity = 30f;
    public float AirAcceleration = 10f;

    Rigidbody rb;
    InputController input;
    RaycastController raycaster;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputController>();
        raycaster = GetComponent<RaycastController>();
    }
	
    /// <summary>
    /// Unused ATM
    /// </summary>
    void Movement()
    {
        Vector3 appliedForce = Vector3.zero;

        Vector3 moveDir = input.MovementDirection;

        Vector3 force = moveDir * AirAcceleration;
        appliedForce += force;

        rb.AddForce(appliedForce, ForceMode.Acceleration);
    }

    void Yaw()
    {
        // TODO: have option to always face forward towards sky

        float yaw = input.MovementDirection.x * Time.fixedDeltaTime * 300f;

        Vector3 y_angle = Vector3.up;
        Quaternion y_rot = Quaternion.AngleAxis(yaw, y_angle);

        rb.AddRelativeTorque(y_angle * yaw);
    }

    void PitchRoll()
    {
        

        float pitch = input.MovementDirection.z * Time.fixedDeltaTime * 350f;
        float roll = input.MovementDirection.x * Time.fixedDeltaTime * 350f;

        Vector3 p_angle = Vector3.right;
        Quaternion p_rot = Quaternion.AngleAxis(pitch, p_angle);

        Vector3 r_angle = Vector3.back;
        Quaternion r_rot = Quaternion.AngleAxis(roll, r_angle);

        rb.AddRelativeTorque(p_angle * pitch);
        rb.AddRelativeTorque(r_angle * roll);
    }

    private void Flipping()
    {


        if (input.FlipPressed)
        {
            Yaw();
        }
        else
        {
            PitchRoll();
        }

        rb.maxAngularVelocity = 5.5f;
        rb.angularVelocity = Vector3.Slerp(rb.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 4f);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
		if(!raycaster.Touching)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxAirVelocity);
            //Movement();
            //FlipRotate();
            Flipping();
        }
	}
}
