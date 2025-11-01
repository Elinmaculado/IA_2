
# Descripción Breve de IA
Sigue al jugador a menos que se aleje mucho. Si se aleja de rango, espera un momento y regresa a su hogar.

# Procesamiento de IA (Que percibe y como actúa)
  La única percepción que tiene es la distancia entre la IA y el player. Para saber si se cumple la condición. También percibe el tiempo que espera después de que el jugador se aleja y percibe a donde tiene que ir una ves que el jugador se aleja.
# Diagrama Behavior Tree
<img src = "mermaid-diagram.jpg">

# Diagrama FSM

<img src = "Pasted%20image%2020251101092250.png">
## Dificultades o problemas de desarrollo
Para mi es más difícil visualizar el behavior tree que la state machine. Por lo que mentalmente me cuesta más la organización de un behavior tree.

## Pequeño analisis de en este caso que arquitectura funciona mejor y por que.
Creo que en este caso FSM funciona más para esta IA ya que es algo simple, y además son acciones bastante generales por lo que creo que es útil tener estas acciones modulares, ya que es muy probable que algún otro NPC vaya a quere hacer uso de las mismas acciones
