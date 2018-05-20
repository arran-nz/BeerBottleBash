using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EmissionController : MonoBehaviour {

    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        em = ps.emission;
	}
	
	public void ToggleEmitter(bool decise)
    {
        em.enabled = decise;
    }

    public void BurstEmitter()
    {
        if (!ps.main.loop)
        {
            ps.Play();
        }
        else
        {
            Debug.LogError("Should be using ToggleEmitter() for this emmitter");
        }
    }
}
