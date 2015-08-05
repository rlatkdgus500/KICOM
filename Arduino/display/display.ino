//Compatible with the Arduino IDE 1.0
//Library version:1.1
#include <Wire.h> 
#include <LiquidCrystal_I2C.h>

LiquidCrystal_I2C lcd(0x27,16,2);  // set the LCD address to 0x20 for a 16 chars and 2 line display

String inputString = "";
boolean stringComplete = false;
void setup()
{
  Serial.begin(9600);
  
  lcd.init();                      // initialize the lcd 
 
  // Print a message to the LCD.
  lcd.backlight();
  lcd.print("Hello, World");
}

void loop()
{
  if(stringComplete){
    int len = inputString.length();
    
    Serial.println(inputString);
    if(len <= 16){
      lcd.clear();
      lcd.setCursor(0,0);
      lcd.print(inputString);
    }else if( len > 16 && len<= 32){
      lcd.clear();
      lcd.setCursor(0,0);
      lcd.print(inputString);
      inputString.remove(0,16);
      lcd.setCursor(0,1);
      lcd.print(inputString);
    }
    
    inputString = "";
    stringComplete = false;
  }
}

void serialEvent()
{
  while(Serial.available()){
    char inChar = (char) Serial.read();
    if(inChar == '\n'){
      stringComplete = true;
      break;
    }
    inputString += inChar;
  }
}
