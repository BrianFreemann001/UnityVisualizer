using UnityEngine;
using System.Collections;

public class CameraRoto : MonoBehaviour {

	public GameObject camPivot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		camPivot.transform.Rotate (0.1f, 0.1f, 0.0f);
	}
}
