using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject potionPrefab;
	public int numPotions = 3;

	float[] xpos = { -22f, -22f, -22f, -5f, 4.7f, 4f };
	float[] ypos = {-11.8f, -6.25f, 9f, 2.5f, 6.2f, -1f };

	int arrayIndex;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < numPotions; i++) {

			do {
			 arrayIndex = Random.Range (0, xpos.Length);
			} while ( xpos[arrayIndex] == 0 );

			Instantiate (potionPrefab, new Vector3 (xpos [arrayIndex], ypos [arrayIndex], 0), Quaternion.identity);
			xpos[arrayIndex] = 0;
			ypos[arrayIndex] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
