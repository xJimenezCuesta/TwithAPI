# TwithAPI
Prueba técnica

-----------------------------

Ejecutar la aplicación

    - Abrir la solución PruebaTecnica.sln en Visual Studio y ejecutar manualmente.

    - Ejecutar en terminal mediante el comando "dotnet run --project PruebaTecnica"

----------------------------------


Decisiones técnicas

.Net ->

    Es un framework pensado para aplicaciones más gramdes, pero es con el que trabajo a diario y el que mejor representa mis habilidades.

Responses ->

    En la carpeta Responses están declaradas las respuestas de las 3 llamadas que se realizan a la api de Twitch. 
    

Models ->

    En la carpeta Models están definidos los objetos de devolvemos. 

Service ->

    Realiza las llamadas a la api de Twitch.
    

JSON ->

    Por simplicidad utilizamos JsonConvert para leer directamente las respuestas de las llamadas a Twitch.

snake_case vs PascalCase ->

    El estándar de nomenclatura de c# es PascalCase. No obstante, como la especificacíon del problema ya definía los nombres de las propiedades en snake_case, 
    hemos decicido seguirlo. Además, esto simplifica la conversión directa de JSON a nuestras clases modelo. 

Test ->

    Los test comprueban las respuestas 200, 400 y 402. Por como se crea el token de autenticación, no es posible mockear una respuesta 401.

----------------------------------

Puntos de mejora: 

    - Esta versión representa el la primera iteración del proyecto que cumple todas las especificaciones del problema. No es la versión final del programa. 

    - Se pide la autenticación al principio de cada llamada, el token no puede caducarse, pero se hacen 2 llamadas a la api de Twitch por cada petición. 

    - Las url, el ClientId y el ClientSecret están escritas directamente en código, por lo que no son configurables. 
    
    - Las lógicas de la llamada y la gestión de errores están juntas.



    
