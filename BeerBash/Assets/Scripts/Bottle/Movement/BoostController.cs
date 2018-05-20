using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : MonoBehaviour {

    public float BoostAccerleration = 50f;
    public EmissionController BoostEffect;

    Rigidbody rb;
    InputController input;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputController>();
	}
	
	// Update is called once per frame
	void Update () {

        bool boostPressed = input.BoostPressed;

        BoostEffect.ToggleEmitter(boostPressed);
		if(boostPressed)
        {
            Boost();
        }
	}

    void Boost()
    {
        Vector3 boostDirection = transform.up * -1;
        Vector3 force = boostDirection * BoostAccerleration;
        rb.AddForce(force, ForceMode.Acceleration);
    }
}
