#include <Servo.h>

Servo myservo;

const int SERVO_EXTREMO_INICIAL = 172;    // un extremo del dial (RPM 0)
const int SERVO_EXTREMO_FINAL   = 2;      // otro extremo del dial (RPM 2400)

void setup() {
  Serial.begin(115200);
  myservo.attach(9);

  // Animacion inicial
  myservo.write(SERVO_EXTREMO_FINAL);
  delay(500);

  // animación hasta el extremo final
  for (int i = SERVO_EXTREMO_FINAL; i >= SERVO_EXTREMO_INICIAL; i--) {
    myservo.write(i);
    delay(15);
  }

  // queda en el extremo incial (aguja en 0 RPM)
  myservo.write(SERVO_EXTREMO_INICIAL);
}

void loop() {
  if (Serial.available()) {
    int angle = Serial.parseInt();

    if (angle > 0) {
      myservo.write(angle);
    }

    delay(20);
  }
}
