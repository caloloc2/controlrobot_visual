# controlrobot_visual
Control de movimientos de un robot carro con Visual Basic.
Controla los movimientos adelante, izquierda, derecha y detener.
Control manual o automatico y un led para control de un sensor de evasion.

Desde el software se envia la siguiente cadena
--- 0*0*0*0*0*0*0*0  ----
que corresponde a
--- adelante*derecha*izquierda*detener*accion1*accion2*automatico*manual ---
respectivamente

Desde el arduino se recibe un 1 o 0 para el sensor de evasion.
