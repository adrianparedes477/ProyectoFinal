# ProyectoFinal Softtek C#
El proyecto está desarrollado con Net Core 6
​
## **Especificación de la Arquitectura**
​
### **Capa API**
Será el punto de entrada a la API. 
*Controlladores: los controladores deberíamos definir la menor cantidad de lógica posible y utilizarlos como un pasamanos con la capa de servicios.
*	Helper: Definiremos lógica que pueda ser de utilidad para todo el proyecto. Por ejemplo el mapper* o envio de correos.
​*	Negocio: Se definirán las interfases y la logica de negocio que utilizaremos.
*	Mapper: En esta carpeta irán las clases de mapeo para vincular entidad-dto o dto-entidad

### **Capa Core**
Es donde definiremos el DbContext y crearemos los seeds correspondientes parapopular nuestras entidades.
*	Entidades: se definiran las entidades q se crearan en la base de datos
*	Especificaciones: se definirán los clases para la paginacion.
*	Models: se definirán los modelos que necesitemos para el desarrollo. Dentro de esta carpeta encontramos DTO, para definirlos ahí dentro.

​
### **Capa Infraestructura**
En este nivel de la arquitectura definiremos todas las entidades de la base de datos.
*Repositories:En esta capa definiremos las clases correspondientes para realizar el repositorio genérico y la unidad de trabajo
*	Data:se definiran mediante sub carpetas la configuracion de cada entidad, los repositorios que se utilizaran para los metodos genericos y no genericos y los seed para la carga a la base de datos.
​*	Helper: Definiremos lógica que pueda ser de utilidad para todo el proyecto. Por ejemplo, algún método para encriptar/desencriptar contraseñas
*	Migration: en esta carpeta iran todos los movientos que se vallan haciendo en la base de datos actualizacion de entidad etc.
*	Respose:Definiremos los codigos de estados y las devoluciones de msj para un mejor entendimiento del cliente .


​
## **Especificación de GIT**
​
* Se deberán crear las ramas a partir de DEV. La nomenclatura para el nombre de las ramas será la sigueinte: Feature/Us-xx (donde xx corresponde con el número de historia)
* El título del pull request debe contener el título de la historia tomada.
* Los commits deben llevar descripciones.
* El pull request solo debe contener cambios relacionados con la historia tomada.
* Se deben agregar capturas de pantalla como evidencia en la descripción de los pool request.
