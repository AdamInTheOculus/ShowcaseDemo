/* Final project code for VR + DIY Controllers */
#include <IRremote.h>
#include <Wire.h>
#include "nunchuck_funcs.h"

#define NUM_OF_SIGNALS 21

boolean debug = 0;
byte nunchuck_package[4];

// Used to contain all unique commands
unsigned long validArray[NUM_OF_SIGNALS];
unsigned long emptyData = 0;

// Infrared variables
int IR = 6;
IRrecv irrecv(IR);
decode_results results;

void setup() {
  Serial.begin(19200);

  // Nunchuck initialized
  nunchuck_setpowerpins();
  nunchuck_init(); // Send init handshake
  Serial.println("WiiChuck initialized!");

  // IR initialized
  irrecv.enableIRIn();
  createList(); // Creates an array of potentially expected IR signals
  Serial.println("IR Receiver initialized!");
}

void loop() {

  /* ----------------------------------------- *
   * ----- Send Wii Nunchuck Remote Data ----- *
   * ----------------------------------------- */
  nunchuck_get_data(); // Read data every loop

  // Package all necessary data into array
  nunchuck_package[0] = nunchuck_joyx();
  nunchuck_package[1] = nunchuck_joyy();
  nunchuck_package[2] = nunchuck_zbutton();
  nunchuck_package[3] = nunchuck_cbutton();
  

  // Print data for debugging
  if(debug) print_nunchuck(nunchuck_package);
  
  // Send data via serial.write()
  send_package(nunchuck_package);

 /* ----------------------------------------- *
  * -----      Send IR Remote Data      ----- *
  * ----------------------------------------- */
  // Checks if any signals were sent
  // Sends signal as a hexadecimal value -- 4 bytes (unsigned long)
  handleIR();

  delay(50);
}

void print_nunchuck(byte package[]) {
  // Ensure package is not empty
  if(package != NULL) {
    Serial.print("JoyX: ");
    Serial.print(package[0]);

    Serial.print("\tJoyY: ");
    Serial.print(package[1]);

    Serial.print("\tZ-Button: ");
    Serial.print(package[2]);

    Serial.print("\tC-Button: ");
    Serial.print(package[3]);

    Serial.println();
  }
  else {
    Serial.println("Given package was NULL!");
  }
}

// Sends packaged data to Unity
void send_package(byte package[]) {
  Serial.write(package, 4); // 4 bytes in this package
}

// Check if valid IR signal and send to Unity
void handleIR() {
  if(irrecv.decode(&results)) {

    // If signal is valid, send to Unity
    if(searchList(results.value)) { 
      Serial.write(results.value); // Unsigned Long = 4 bytes
    }
    else {
      Serial.write(emptyData); // Always send data so arrangement isn't screwed up in Unity 
    }
    // Receive next value
    irrecv.resume(); 
  }
  else {
    Serial.write(emptyData); // Always send data so arrangement isn't screwed up in Unity 
  }
}

/* 
 *  Generates NUM_OF_SIGNALS elements; each element is hard-coded in.
 *  This method holds all unique IR values. Change to fit your remote.
 */
void createList() {
  for(int i=0; i<NUM_OF_SIGNALS; i++)
  {
    switch(i) 
    {
      // These values are unique to my own remote
      case 0: validArray[0] = 0xFFA25D;
      case 1: validArray[1] = 0xFF629D;
      case 2: validArray[2] = 0xFFE21D;
      case 3: validArray[3] = 0xFF22DD;
      case 4: validArray[4] = 0xFF02FD;
      case 5: validArray[5] = 0xFFC23D;
      case 6: validArray[6] = 0xFFE01F;
      case 7: validArray[7] = 0xFFA857;
      case 8: validArray[8] = 0xFF906F;
      case 9: validArray[9] = 0xFF6897;
      case 10: validArray[10] = 0xFF9867;
      case 11: validArray[11] = 0xFFB04F;
      case 12: validArray[12] = 0xFF30CF;
      case 13: validArray[13] = 0xFF18E7;
      case 14: validArray[14] = 0xFF7A85;
      case 15: validArray[15] = 0xFF10EF;
      case 16: validArray[16] = 0xFF38C7;
      case 17: validArray[17] = 0xFF5AA5;
      case 18: validArray[18] = 0xFF42BD;
      case 19: validArray[19] = 0xFF4AB5;
      case 20: validArray[20] = 0xFF52AD;
    }
  }
}

/* 
 *  Scan through list of numbers -- Complexity: O(n)
 *  If match, return true -- otherwise return false
 */

boolean searchList(unsigned long signalCheck) 
{
  for(int i=0; i<NUM_OF_SIGNALS; i++)
  {
    // Match found
    if(signalCheck == validArray[i])
      return true;
  }
  // No match found
  return false;
}

