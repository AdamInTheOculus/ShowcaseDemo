using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class DataManager : MonoBehaviour {

    // Variables to handle all Wii data
    public static int joyX, joyY, zBut, cBut;
    public static int irData;
    SerialPort arduinoPort = new SerialPort("COM5", 19200);

    Thread serialThread;

	// Use this for initialization
	void Start () 
    {
        try
        {
            arduinoPort.Open();
        }
        catch(System.IO.IOException e)
        {
            // Do something. Debug log for now
            Debug.LogError(e);
        }
        
        serialThread = new Thread(new ThreadStart(updateWiiData));
        serialThread.Start();
	}

    void updateWiiData()
    {
        while(true) // Not sure how I feel about this piece of code, but it works.
        {
            // Read in all serial data
            joyX = arduinoPort.ReadByte();
            joyY = arduinoPort.ReadByte();
            zBut = arduinoPort.ReadByte();
            cBut = arduinoPort.ReadByte();

            // Read in potential IR value
            irData = arduinoPort.ReadByte();
            Thread.Sleep(10);
        }
    }

    public static void printWiiData()
    {
        Debug.Log("JoyX: " + joyX + "\tJoyY: " + joyY + "\tZ-But: " + zBut + "\tC-But: " + cBut);
    }

    public static void printIRData()
    {
        Debug.Log("IR Value: " + irData);
    }
}
