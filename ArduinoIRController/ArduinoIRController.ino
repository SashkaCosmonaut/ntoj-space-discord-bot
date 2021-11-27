const String FORWARD = "f";
const String BACK = "b";
const String LEFT = "l";
const String RIGHT = "r";
const String OK = "k";

String incomingString;  // Считываемся с компьютера строка
 
void setup() {
    Serial.begin(9600); // устанавливаем последовательное соединение
}

void loop() {
  if (Serial.available() == 0) {  // Если ничего не пришло, ждём
    return;
  }

  while(Serial.available() > 0) {
    incomingString = Serial.readString();
  }

  if (incomingString == FORWARD) {
    Serial.print(OK);
  }

  incomingString = "";
}