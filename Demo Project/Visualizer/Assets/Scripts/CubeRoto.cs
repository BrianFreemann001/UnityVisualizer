using UnityEngine;
using System.Collections;

public class CubeRoto : MonoBehaviour {

	public GameObject cubePivot;
	private float xrot = -1;
	private float yrot = -1;
	private float zrot = -1;
	public int count = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	//	gameObject.transform.RotateAround (cubePivot.transform.position, Vector3.up, 1.0f);
	//	cubePivot.transform.Rotate (0.0f, -1.0f, 0.0f);

		if (count >= 100) 
		{
			
			xrot = Random.Range (-1, 1);
			yrot = Random.Range (-1, 1);
			zrot = Random.Range (-1, 1);
			count = 0;
		}
		cubePivot.transform.Rotate(xrot, yrot, zrot);
		
		count++;
		//parent6.transform.rotation = Random.rotation;
		//cubePivot.transform.Rotate(-1.0f, -1.0f, -1.0f);
		//parent6.transform.RotateAround (Vector3.zero, Vector3.up, 90.0f * Time.deltaTime);
	}
}
