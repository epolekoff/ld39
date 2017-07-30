using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCone : MonoBehaviour {

    public ParticleSystem Particles;
    public GameObject Hitbox;

    private const int ParticleEmissionAmount = 50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Shoot some particles.
    /// </summary>
    public void EmitParticles()
    {
        Particles.Emit(ParticleEmissionAmount);
    }
}
