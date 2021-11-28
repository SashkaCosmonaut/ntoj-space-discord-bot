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
#define P7          0x07
#define P8          0x15
#define P9          0x09
#define P0          0x19
#define P_STAR      0x16
#define P_HASH      0x0D
#define P_OK        0x1C
#define P_RIGHT     0x5A
#define P_LEFT      0x08
#define P_UP        0x18
#define P_DOWN      0x52

IRsend sender;        // Объект взаимодействия с ИК-диодом

char incomingChar;    // Считываемый с компьютера символ
int command;          // Расшифрованная команда для корабля

// По коду символа (полученного с компа через Serial) возвращает код кнопки на ИК-пульте. 0, если нет соответствия.
int decodeCommandChar(char commandChar)
{
  switch (commandChar) {
    case '0': return P0;
    case '1': return P1;
    case '2': return P2;
    case '3': return P3;
    case '4': return P4;
    case '5': return P5;
    case '6': return P6;
    case '7': return P7;
    case '8': return P8;
    case '9': return P9;
    case '*': return P_STAR;
    case '#': return P_HASH;
    case '.': return P_OK;
    case '>': return P_RIGHT;
    case '<': return P_LEFT;
    case '^': return P_UP;
    case 'v': return P_DOWN;
  }
  
  return  0;
}

void setup() {
  Serial.begin(9600); // Устанавливаем последовательное соединение

  IrSender.begin(IR_SEND_PIN, ENABLE_LED_FEEDBACK);  // Запускаем работу с ИК-диодом
}

void loop() {
  if (Serial.available() == 0) {  // Если ничего не пришло, ждём
    return;
  }

  while (Serial.available() > 0) { // Считываем всё, что приходит, оставляем последнее
    incomingChar = Serial.read();
  }

  command = decodeCommandChar(incomingChar);
  
  // В зависимости от константы посылаем сигнал кораблю, как бы пультом
  IrSender.sendNEC(ADDRESS, command, REPEATS);

  Serial.print(OK_STR);   // Отвечаем, что всё ОК

  incomingChar = ' ';
}
