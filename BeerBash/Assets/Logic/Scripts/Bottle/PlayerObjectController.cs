using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bottle.MovementTypes;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RaycastController))]
public class PlayerObjectController : MonoBehaviour {

    InputController input;
    RaycastController raycaster;
    Rigidbody rb;

    UprightMovement uprightMovement;
    AirMovement airMovement;
    Jump jump;
    Boost boost;

    [SerializeField]
    EmissionController boostParticle;

    public bool Grounded { get; private set; }
    public bool Upright { get; private set; }


    [SerializeField]
    float maxUprightPitch = 45f;

    [SerializeField]
    BottleMovementConfiguration currentMovementConfig;



    // Use this for initialization
    void Start () {

        boost = new Boost(currentMovementConfig);
        jump = new Jump(currentMovementConfig);
        uprightMovement = new UprightMovement(currentMovementConfig);
        airMovement = new AirMovement(currentMovementConfig);

        raycaster = GetComponent<RaycastController>();
        rb = GetComponent<Rigidbody>();

        input = FindObjectOfType<InputController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        SurfaceInfo surfaceInfo = raycaster.GetSurfaceInfo(GetComponent<CapsuleCollider>());

        Upright = surfaceInfo.Pitch <= maxUprightPitch && surfaceInfo.RaycastOriginResult;
        Grounded = surfaceInfo.Pitch <= maxUprightPitch && !surfaceInfo.RaycastOriginResult;

        Movement(Upright, surfaceInfo);

        InputJump(Grounded || Upright);

        InputBoost();


    }

    void Movement(bool Upright, SurfaceInfo surfaceInfo)
    {
        if (Upright)
        {
            rb.freezeRotation = true;
            uprightMovement.ApplyMovementForces(rb, input, surfaceInfo);
        }
        else
        {
            rb.freezeRotation = false;
            airMovement.ApplyMovementForces(rb, input);
        }
    }

    void InputJump(bool canJump)
    {
        if (canJump && input.JumpPressed)
        {

            jump.ApplyJump(rb, Vector3.up);
        }
    }

    void InputBoost()
    {
        if (input.BoostPressed)
        {
            boost.ApplyBoost(rb);
            boostParticle.ToggleEmitter(true);
        }
        else
        {
            boostParticle.ToggleEmitter(false);
        }
    }
}
