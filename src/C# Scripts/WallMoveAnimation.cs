using UnityEngine;
using System.Collections;

public class WallMoveAnimation : MonoBehaviour {

    // Solution was found from: https://www.youtube.com/watch?v=U1UkKw12pQo
    // By MollyPlaysGames -- SOLUTION IS NOT USED

    void Update() {
        if (DataManager.irData == 29)
            transform.position += 5.0f * Vector3.forward;
    }
}
