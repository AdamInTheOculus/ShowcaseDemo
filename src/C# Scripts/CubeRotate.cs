using UnityEngine;
using System.Collections;

public class CubeRotate : MonoBehaviour {

    public float speed = 50.0f;
    private bool isRotating = false;
	
	// Update is called once per frame
	void Update () {

        // Cube will rotate on specified button: 1
        if (DataManager.irData == 93)
            isRotating = true;
        // Cube will stop rotating on specified button: 2
        if (DataManager.irData == 157)
            isRotating = false;

		// Update cubes rotation
        if (isRotating)
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
	}
}
