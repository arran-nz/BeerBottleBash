using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public InputController input;

    public float followMagnitude = 30f;
    public float rotationMagnitude = 10f;

    public Transform player = null;

    public Vector3 camOffset;
    public Vector3 rotOffset;

    private Quaternion lastQ;

    private Transform cam ;


    public void Start()
    {
        cam = transform;
    }


    public void FixedUpdate()
    {
        Vector3 newPos = transform.position;

        if(player.GetComponent<RaycastController>().Upright)
        {
            lastQ = Quaternion.LookRotation(player.forward, transform.up);
        }

        newPos = lastQ * camOffset + player.position;

        float dist = Vector3.Distance(newPos, transform.position);
        dist = Mathf.Max(1, dist);
        float t = (Time.fixedDeltaTime / dist) * followMagnitude;


        Vector3 lerpPos = Vector3.Lerp(transform.position, newPos, t);        
        cam.position =  lerpPos;




        Vector3 playerVel = player.gameObject.GetComponent<Rigidbody>().velocity;
        playerVel = Vector3.ClampMagnitude(playerVel, .2f);

        Quaternion rotationOffset = Quaternion.Euler(rotOffset);
        Vector3 relativePos = (player.position - transform.position) + playerVel;
        Quaternion lookDir = Quaternion.LookRotation(relativePos, Vector3.up);
        Quaternion finalRot = lookDir * rotationOffset;

        cam.rotation = Quaternion.Lerp(cam.rotation, finalRot, Time.fixedDeltaTime * rotationMagnitude);


    }


}


