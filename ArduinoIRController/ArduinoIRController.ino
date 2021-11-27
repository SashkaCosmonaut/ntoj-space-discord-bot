#include <IRremote.h>

#define IR_SEND_PIN 13      // Константа из библиотеки задания пина для ИК-диода
#define ADDRESS     0x0102  // Взято из примера, хардкод
#define REPEATS     0       // Кол-во повторений миганий ИК-диодом

// Символьные константы, ожидаемые от программы
#define FORWARD_STR 'f'
#define BACK_STR    'b'
#define LEFT_STR    'l'
#define RIGHT_STR   'r'
#define STOP_STR    's'
#define OK_STR      'k'

// Сигналы пульта, посылаемые кораблю
#define P1          0x45
#define P2          0x46
#define P3          0x47
#define P4          0x44
#define P5          0x40
#define P6          0x43
#define P8          0x15

IRsend sender;        // Объект взаимодействия с ИК-диодом

char incomingString;  // Считываемый с компьютера символ
 
void setup() {
  Serial.begin(9600); // Устанавливаем последовательное соединение
    
  IrSender.begin(IR_SEND_PIN, true);  // Запускаем работу с ИК-диодом
}

void loop() {
  if (Serial.available() == 0) {  // Если ничего не пришло, ждём
    return;
  }

  while(Serial.available() > 0) { // Считываем всё, что приходит
    incomingString = Serial.read();
  }

  // В зависимости от константы посылаем сигнал кораблю, как бы пультом
  switch (incomingString) {
    case FORWARD_STR:
      IrSender.sendNEC(ADDRESS, P2, REPEATS);
      break;
        
    case BACK_STR:
      IrSender.sendNEC(ADDRESS, P8, REPEATS);
      break;

    case LEFT_STR:
      IrSender.sendNEC(ADDRESS, P4, REPEATS);
      break;

    case RIGHT_STR:
      IrSender.sendNEC(ADDRESS, P6, REPEATS);
      break;

    case STOP_STR:
      IrSender.sendNEC(ADDRESS, P5, REPEATS);
      break;
  }

  Serial.print(OK_STR);   // Отвечаем, что всё ОК

  incomingString = ' ';
}
