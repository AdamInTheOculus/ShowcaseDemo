using UnityEngine;
using System.Collections;

public class RandomColour : MonoBehaviour {

    Color[] colorArray;

	// Use this for initialization
	void Start () {
        colorArray = new Color[5];
        colorArray[0] = Color.red;
        colorArray[1] = Color.blue;
        colorArray[2] = Color.green;
        colorArray[3] = Color.magenta;
        colorArray[4] = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
	    // Change to random color if IR button: 4 is pressed
        if (DataManager.irData == 221) {
            GetComponent<Light>().color = colorArray[Random.Range(0, 4)];
        }
	}
}
