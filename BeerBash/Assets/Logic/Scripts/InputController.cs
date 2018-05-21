using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public Vector3 MovementDirection;
    public Vector3 LookDirection;
    public bool JumpPressed;
    public bool BoostPressed;
    public bool FlipPressed;
	
	// Update is called once per frame
	void Update () {
        MovementDirection.x = Input.GetAxis("L-Joy X");
        MovementDirection.z = Input.GetAxis("L-Joy Y");
        LookDirection.x = Input.GetAxis("R-Joy X");
        LookDirection.y = Input.GetAxis("R-Joy Y");

        BoostPressed = Input.GetAxis("BoostTrigger") != 0 || Input.GetButton("Boost");

        FlipPressed = Input.GetButton("Flip");

        JumpPressed = Input.GetButton("Jump");

        if(Input.GetButton("Restart"))
        {
            Application.LoadLevel(index: Application.loadedLevel);
        }
    }

}
