using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RaycastController))]
public class PlayerObjectController : MonoBehaviour {

    InputController input;
    RaycastController raycaster;

    GroundMovement groundMovement = new GroundMovement();
    AirMovement airMovement = new AirMovement();
    BoostController boost = new BoostController();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
