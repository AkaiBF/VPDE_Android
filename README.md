# Tanks Android

Para desarrollar para Android usando Unity, lo primero que tenemos que tener en cuenta es el SDK de Android. Para ello, se debe añadir a la instalación de la versión de Unity
haciendo uso de Unity Hub.

Posteriormente, hemos de comprobar que la versión del SDK está en uso. Unity por defecto nos selecciona la SDK que considere más actualizada y se encarga de su descarga. Esto se hace desde el Project Settings. Una vez en el Project Settings, iremos a Player para asegurarnos de que el manifest se creará correctamente. Para ello, debemos ver si el nombre de paquete está bien definido (tampoco vale el valor por defecto).

Para hacer el juego jugable desde móvil, también hemos de añadir controles a un Canvas. En este caso he utilizado un joystick de la asset store para el movimiento y un botón para disparar. Además, como se requería el uso de vibración, se ha añadido una Handheld.Vibrate al método Fire.