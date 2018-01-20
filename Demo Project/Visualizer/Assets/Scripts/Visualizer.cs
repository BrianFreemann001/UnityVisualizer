using UnityEngine;
using System.IO;
using System.Collections;

public class Visualizer : MonoBehaviour {

	public GameObject prefab;
	public GameObject innerorb;
	public int numObjects;
	public int spectrumSamples = 1024;
	public float radius = 5f;
	public float cubeoffset = 5f;
	public float spectrumScale = 20f;
	public float cubespectrumScale = 20f;
	public GameObject[] circularcubies;
	public GameObject[] square0;
	public GameObject[] square1;
	public GameObject[] square2;
	public GameObject[] square3;
	public GameObject[] square4;
	public GameObject[] square5;
	public GameObject parent0;
	public GameObject parent1;
	public GameObject parent2;
	public GameObject parent3;
	public GameObject parent4;
	public GameObject parent5;
	public GameObject[] parent6;
	public FFTWindow fftWindow;
	public float testSprctrum;
	public float colorMultiplier;
	public float colorRatio;
	public float alphaRatio;
	public float cubieAlpha = 0.8f;
	public float cubeavgspect;
	public float cubeminscale;
	public float cubemaxscale;



	// Use this for initialization
	void Start () 
	{
	
		for (int i = 0; i < numObjects; i++) {

			float angle = i * Mathf.PI * 2 / numObjects;
			Vector3 pos = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius;
			Instantiate (prefab, pos, Quaternion.identity);
		}
//		Instantiate (parent0, new Vector3 (-cubeoffset, 0.0f, 0.0f), Quaternion.AngleAxis (90.0f, new Vector3 (0.0f, 0.0f, 1.0f)));
//		Instantiate (parent1, new Vector3 (0.0f, 0.0f, -cubeoffset), Quaternion.AngleAxis (90.0f, new Vector3 (1.0f, 0.0f, 0.0f)));
//		Instantiate (parent2, new Vector3 (cubeoffset, 0.0f, 0.0f), Quaternion.AngleAxis (90.0f, new Vector3 (0.0f, 0.0f, 1.0f)));
//		Instantiate (parent3, new Vector3 (0.0f, 0.0f, cubeoffset), Quaternion.AngleAxis (90.0f, new Vector3 (1.0f, 0.0f, 0.0f)));
//		Instantiate (parent4, new Vector3 (0.0f, cubeoffset, 0.0f), Quaternion.identity);
//		Instantiate (parent5, new Vector3 (0.0f, -cubeoffset, 0.0f), Quaternion.identity);

		//Instantiate (parent6);
		square0 = GameObject.FindGameObjectsWithTag ("RCubies");
		square1 = GameObject.FindGameObjectsWithTag ("BCubies");
		square2 = GameObject.FindGameObjectsWithTag ("OCubies");
		square3 = GameObject.FindGameObjectsWithTag ("GCubies");
		square4 = GameObject.FindGameObjectsWithTag ("YCubies");
		square5 = GameObject.FindGameObjectsWithTag ("WCubies");

	
			circularcubies = GameObject.FindGameObjectsWithTag ("CircularCubies");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		float[] spectrum = AudioListener.GetSpectrumData (spectrumSamples, 0, fftWindow);
		for (int i = 0; i < numObjects; i++) 
		{
			
			Vector3 prevScale = circularcubies [i].transform.localScale;
			prevScale.y = Mathf.Lerp (prevScale.y, spectrum [i] * spectrumScale, Time.deltaTime * spectrumScale);
			circularcubies [i].transform.localScale = prevScale;
			colorRatio = spectrum [i] * colorMultiplier;
			alphaRatio = colorRatio + 0.2f;
			if (colorRatio > 1) {
				colorRatio = 1.0f;
				alphaRatio = colorRatio;
			}
			Color rgb = new Color (colorRatio, 0.0f, 1.0f - colorRatio, alphaRatio);
			circularcubies [i].GetComponent<Renderer> ().material.color = rgb;
			circularcubies[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", rgb);

			if(i < 18)
			{
				Color currcol = new Color (square0[i].GetComponent<Renderer>().material.color.r, 
				                           square0[i].GetComponent<Renderer>().material.color.g,
				                           square0[i].GetComponent<Renderer>().material.color.b,
				                           cubieAlpha);
				
				square0[i].GetComponent<Renderer>().material.color = currcol;
				
				currcol = new Color (square1[i].GetComponent<Renderer>().material.color.r, 
				                     square1[i].GetComponent<Renderer>().material.color.g,
				                     square1[i].GetComponent<Renderer>().material.color.b,
				                     cubieAlpha);
				
				square1[i].GetComponent<Renderer>().material.color = currcol;
				
				currcol = new Color (square2[i].GetComponent<Renderer>().material.color.r, 
				                     square2[i].GetComponent<Renderer>().material.color.g,
				                     square2[i].GetComponent<Renderer>().material.color.b,
				                     cubieAlpha);
				
				square2[i].GetComponent<Renderer>().material.color = currcol;
				
				currcol = new Color (square3[i].GetComponent<Renderer>().material.color.r, 
				                     square3[i].GetComponent<Renderer>().material.color.g,
				                     square3[i].GetComponent<Renderer>().material.color.b,
				                     cubieAlpha);
				
				square3[i].GetComponent<Renderer>().material.color = currcol;
				
				currcol = new Color (square4[i].GetComponent<Renderer>().material.color.r, 
				                     square4[i].GetComponent<Renderer>().material.color.g,
				                     square4[i].GetComponent<Renderer>().material.color.b,
				                     cubieAlpha);
				
				square4[i].GetComponent<Renderer>().material.color = currcol;
				
				currcol = new Color (square5[i].GetComponent<Renderer>().material.color.r, 
				                     square5[i].GetComponent<Renderer>().material.color.g,
				                     square5[i].GetComponent<Renderer>().material.color.b,
				                     cubieAlpha);
				
				square5[i].GetComponent<Renderer>().material.color = currcol;
			}

			if (i == 0/*numObjects - 6*/) 
			{

				cubeavgspect = ((spectrum [i] + spectrum [i + 1] + spectrum [i + 2] + spectrum [i + 3] + spectrum [i + 4]) / 5.0f);

				Vector3 prevScale0 = parent6[0].transform.localScale;
				prevScale0.x = Mathf.Lerp (prevScale0.x, cubeavgspect * cubespectrumScale, Time.deltaTime * spectrumScale);
				prevScale0.y = Mathf.Lerp (prevScale0.y, cubeavgspect * cubespectrumScale, Time.deltaTime * spectrumScale);
				prevScale0.z = Mathf.Lerp (prevScale0.z, cubeavgspect * cubespectrumScale, Time.deltaTime * spectrumScale);

				if (prevScale0.x < cubeminscale) 
				{

					prevScale0 = new Vector3 (cubeminscale, cubeminscale, cubeminscale);
				}

				if (prevScale0.x > cubemaxscale && cubemaxscale > cubeminscale)
				{

					prevScale0 = new Vector3 (cubemaxscale, cubemaxscale, cubemaxscale);
				}


				parent6[0].transform.localScale = prevScale0;

				prevScale0 = parent6[1].transform.localScale;
				prevScale0.x = Mathf.Lerp (prevScale0.x, cubeavgspect * cubespectrumScale, Time.deltaTime * spectrumScale);
				prevScale0.y = Mathf.Lerp (prevScale0.y, cubeavgspect * cubespectrumScale, Time.deltaTime * spectrumScale);
				prevScale0.z = Mathf.Lerp (prevScale0.z, cubeavgspect * cubespectrumScale, Time.deltaTime * spectrumScale);

				if (prevScale0.x < cubeminscale) 
				{

					prevScale0 = new Vector3 (cubeminscale, cubeminscale, cubeminscale);
				}

				if (prevScale0.x > cubemaxscale && cubemaxscale > cubeminscale)
				{
					
					prevScale0 = new Vector3 (cubemaxscale, cubemaxscale, cubemaxscale);
				}

				parent6[1].transform.localScale = prevScale0;

			}
		}

//		for (int i = 0; i < 9; i++) 
//		{
//
//			Vector3 prevpos = square0 [i].transform.position;
//			prevpos.x = -Mathf.Lerp (prevpos.x, spectrum [i] * spectrumScale, Time.deltaTime * spectrumScale) - cubeoffset;
//			square0 [i].transform.position = prevpos;
//		}
	}
}














