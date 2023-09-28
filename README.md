# TestIOL
Resultado del proyecto TEST para IOL

A continuación se describe brevemente la resolución para las dos necesidades de cambios indicados para el examen: el 1ero correspondiente al tema de poder agregar más figuras geométricas; el 2do correspondiente al manejo de los idiomas en los mensajes.

Figuras geométricas: Se resuelve que una de las soluciones es crear tantas clases como figuras nuevas se necesiten; de esta manera el cálculo del perímetro y área se pueda resolver de manera simple; ya que cada figura tiene un cálculo diferente a las figuras regulares (triangulo equilatero; cuadrado; circulo). Se agrego un nuevo caso de prueba para la nueva figura Trapecio.

Idioma: Se resuelve que una de las soluciones es crear un array, en donde cada columna representa el texto en un idioma para un mismo mensaje por fila. Si llegado el caso se requiere agregar más idiomas, solo requerira agregar N columnas que contendra la traducción requerida. En este caso se planteo una nueva clase para los mensajes en distintos idiomas, pero tranquilamente podría armarse un archivo json como repositorio de esta estructura.
