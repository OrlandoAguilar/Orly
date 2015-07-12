using UnityEngine;
using System.Collections;

public class Girator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		HingeJoint2D comp=GetComponent<HingeJoint2D>();
		comp.connectedAnchor=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
