# GEPARK
    • Aplicativo backend para gestionar parqueaderos
    • Se adjunta diagrama de clases con las reglas del negocio y diagrama entidad-relación ubicados dentro de la carpeta Modelado.
    • Se adjunta base de datos en SqlServer 2017. Está ubicada en la carpeta BaseDatos

Descripción técnica

    1. La solución está compuesta por 4 entidades de negocio:
    
    • Usuario: entidad que almacena el nombre y clave de los usuarios para que accedan al sistema.
    • Tarifa: representa las tarifas que se aplican al sistema de gestión de parqueadero.
    • Parking: entidad que almacena la información del proceso de solicitar un usuario acceso al parqueadero.
    • TicketPerdido: entidad que almacena la información de los ticket perdidos.
    
    2. Se utilizó para implementar la autenticación y autorización el estándar JWT.
    
    3. Para las búsquedas usando filtros y paginación se utilizó el protocolo ODATA.
    
    4. En la tabla Parking cuando se agrega un nuevo ticket se creó un trigger para generar el código de forma automática.
    
    5. Se creó el procedimiento almacenado CalcularValorPagar que permite realizar el cálculo del valor que debe pagar el conductor de acuerdo a la hora de ingreso.
    
    6.  Se creó el procedimiento almacenado ValidarEntradaParqueadero que valide antes de ingresar al parqueadero si todas las capacidades están ocupadas.
