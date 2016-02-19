using UnityEngine;
using System.Collections;

public class WallMoveAnimation : MonoBehaviour {

    void Update() {
		
		// When TV remote button 29 is pressed, move wall forwards (NO ANIMATION)
        if (DataManager.irData == 29)
            transform.position += 5.0f * Vector3.forward;
    }
}
