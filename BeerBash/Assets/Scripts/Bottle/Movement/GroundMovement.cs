using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour {

    public float MaxGroundVelocity = 20f;
    public float MaxAirVelocity = 45f;

    public float GroundAcceleration = 60f;
    public float JumpHeight = 5f;

    public float TurningSpeed = 200f;



    Rigidbody rb;
    InputController input;
    RaycastController raycaster;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputController>();
        raycaster = GetComponent<RaycastController>();
	}

    Vector3 AngledVectorFromSurfaceNormal(Vector3 surfaceNormal, Vector3 vector)
    {
        Quaternion difference = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
        return difference * vector;
    }

    Vector3 CalculateInputForce(Vector3 uprightDirection)
    {
        // Move relevative to the foward direction of the rigidbody
        Vector3 moveDir = rb.rotation * input.MovementDirection;

        // Adjust movement vector for angled surfaces
        Vector3 adjustedMovement
            = AngledVectorFromSurfaceNormal(uprightDirection, moveDir);

        // Multiply by force
        Vector3 force = adjustedMovement * GroundAcceleration ;

        Debug.DrawRay(rb.position, adjustedMovement, Color.red);

        return force;
    }

    void KeepUpright(Vector3 uprightDirection)
    {
        Debug.DrawRay(rb.position, uprightDirection, Color.green);

        Vector3 removeVertical = new Vector3(transform.forward.x, 0, transform.forward.z);


        float lookAmount = input.LookDirection.x * Time.fixedDeltaTime * TurningSpeed;
        Quaternion lookDirection = Quaternion.Euler(0, lookAmount, 0);

        Vector3 desiredForward = lookDirection * removeVertical;

        Debug.DrawRay(rb.position, desiredForward);

        Vector3 adjustedDirection = AngledVectorFromSurfaceNormal(uprightDirection, desiredForward);

        Quaternion finalRot = Quaternion.LookRotation(adjustedDirection, uprightDirection);


        rb.rotation = finalRot;

    }

    Vector3 CalculateGroundFriction()
    {
        Vector3 inverseVelocity = rb.velocity * -1;
        Vector3 friction = inverseVelocity;
        return friction;
    }

    void Jump(Vector3 direction)
    {
        Vector3 jumpForce = direction * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
        rb.AddForce(jumpForce, ForceMode.VelocityChange);
    }

	void FixedUpdate ()
    {
        if (raycaster.Touching)
        {
            CalculateForces();
        }
	}

    void CalculateForces()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxGroundVelocity);

        rb.maxAngularVelocity = 2f;
        rb.angularVelocity = Vector3.Slerp(rb.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 6f);

        SurfaceInformatiom surfaceInfo = raycaster.GetSurfaceInfo();
        Vector3 appliedForce = Vector3.zero;


        if (raycaster.Upright)
        {
            //rb.freezeRotation = true;

            appliedForce += CalculateInputForce(surfaceInfo.Normal);
            KeepUpright(surfaceInfo.Normal);
        }
        else
        {
            //rb.freezeRotation = false;
        }

        appliedForce += CalculateGroundFriction();

        rb.AddForce(appliedForce, ForceMode.Acceleration);

        if (input.JumpPressed)
        {
            Jump(Vector3.up);
        }
    }
}
