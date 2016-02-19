using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    // Player movement attributes
    public float moveSpeed = 10.0f;

    // Player flashlight + Player camera
    GameObject flashlight;
    GameObject camera;

	void Start () {

        // Retrieve relative game objects
        camera = this.transform.GetChild(0).gameObject;
        flashlight = camera.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        /* ---------------------
         * ----- DEBUGGING -----
         * --------------------- */
        //DataManager.printWiiData();
        //DataManager.printIRData();

        /* -----------------------
         * -- ORIENTATION RESET --
         * ----------------------- */
        if (DataManager.cBut == 1)
            UnityEngine.VR.InputTracking.Recenter();

        /* --------------------
         * FLASH-LIGHT handling 
         * -------------------- */
        // Flashlight ON
        if (DataManager.zBut == 1 && flashlight != null)
            flashlight.SetActive(true);
        // Flashlight OFF
        else
            flashlight.SetActive(false);

        /* ---------------------
         * -- PLAYER movement --
         * --------------------- */

        /* 
         * Player movement is NOT smooth at the moment. The player moves in the direction the camera is facing.
         * In other words, the player moves in the direction the user is facing. 
         * If the user is facing upwards, moving forwards will cause the player to repeatedly "jump" in the air.
         * If the user is facing downwards, moving backwards will also cause the player to jump in the air.
         * Moving side-to-side causes slight skips. 
         * The overall movement is unpleasant. 
         * 
         *    Y
         *    | \           Y - Direction Player is looking at (a,b,c)
         *    |  \          X - Same direction player is looking at, as well as same X & Y values (d,b,c)
         *    |   \         Player - Player's head is tracked constantly so we get some data to play with (e,f,g)
         *    |    \
         *    |     \
         *    X______\ Player
         *    
         *    Currently, the player's forward vector is always Y. This causes the player to jump while moving forward
         *    and looking up at the same time.
         *    
         *    I need forward vector to be X, which will have the same (x,z) values as Player 
         */

        Vector3 forwardDirection = Vector3.Scale(camera.transform.forward, Vector3.forward + Vector3.right);

        // JoyX in neutral; JoyY pushed forward
        // FORWARD
        if (DataManager.joyX >= 90 && DataManager.joyX <= 200 && DataManager.joyY >= 200)
            transform.position += forwardDirection.normalized * moveSpeed * Time.deltaTime;

        // JoyX in neutral; JoyY pulled back
        // BACKWARDS
        else if (DataManager.joyX >= 70 && DataManager.joyX <= 190 && DataManager.joyY <= 50)
            transform.position += -forwardDirection.normalized * moveSpeed * Time.deltaTime;

        // JoyX pushed to the right; JoyY neutral
        // STRAFE RIGHT
        else if (DataManager.joyX >= 200 && DataManager.joyY >= 120 && DataManager.joyY <= 150)
            transform.position += camera.transform.right * moveSpeed * Time.deltaTime;

        // JoyX pushed to the left; JoyY neutral
        // STRAFE LEFT
        else if (DataManager.joyX <= 50 && DataManager.joyY >= 120 && DataManager.joyY <= 150)
            transform.position += -camera.transform.right * moveSpeed * Time.deltaTime;

        // JoyX in neutral; JoyY in neutral
        // Do not move player
        if (DataManager.joyX >= 120 && DataManager.joyX <= 150 && DataManager.joyY >= 120 && DataManager.joyY <= 150) {
            // Do nothing
        }
    }
}
