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

    UprightMovement uprightMovement = new UprightMovement();
    AirMovement airMovement = new AirMovement();
    Boost boost = new Boost();
    Jump jump = new Jump();

    [SerializeField]
    EmissionController boostParticle;

    public bool Grounded => GetComponent<CollisionManager>().Grounded;
    public bool Upright { get; private set; }


    [SerializeField]
    private float MaxUprightPitch = 45f;

    
	// Use this for initialization
	void Start () {
        raycaster = GetComponent<RaycastController>();
        rb = GetComponent<Rigidbody>();

        input = FindObjectOfType<InputController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        SurfaceInfo surfaceInfo = raycaster.GetSurfaceInfo();
        //Grounded = surfaceInfo.Solid;
        Upright = surfaceInfo.Pitch <= MaxUprightPitch;

        Movement(Upright, surfaceInfo);
        InputJump(Grounded);
        InputBoost();

	}

    void Movement(bool Upright, SurfaceInfo surfaceInfo)
    {
        if (Upright)
        {
            uprightMovement.ApplyMovementForces(rb, input, surfaceInfo);
        }
        else
        {
            airMovement.ApplyMovementForces(rb, input);
        }
    }

    void InputJump(bool grounded)
    {
        if (grounded && input.JumpPressed)
        {
            jump.ApplyJump(rb);
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
