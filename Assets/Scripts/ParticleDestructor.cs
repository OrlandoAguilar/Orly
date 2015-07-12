using UnityEngine;
using System.Collections;

public class ParticleDestructor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	//	Destroy(gameObject, particleSystem.duration);
	}
	
	// Update is called once per frame
	void Update () {
		if (!particleSystem.IsAlive())
			Destroy(gameObject);
	}
}
