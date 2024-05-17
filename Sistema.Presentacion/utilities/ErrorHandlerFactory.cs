using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Presentacion.utilities.impl
    {
   public class ErrorHandlerFactory
        {

        public static IErrorHandler GetErrorHandler(string errorType)
            {
            switch (errorType)
                {
                case "categorias":
                    return new CategoriaErrorHandler();
                // Agregar más casos según sea necesario
                case "articulos":
                    return new ProductoErrorHandler();
                // Agregar más casos según sea necesario
                default:
                    throw new ArgumentException($"Tipo de error no reconocido: {errorType}");
                }
            }


        }
    }



//El patrón Factory Method es un patrón de diseño creacional
//que se utiliza para crear objetos de una jerarquía de clases sin especificar explícitamente sus clases concretas.
//En lugar de llamar directamente al constructor de una clase concreta, el patrón Factory Method define un método (el "método de fábrica") en una clase base o en una interfaz para crear objetos.
//Luego, las subclases pueden implementar este método de fábrica para proporcionar instancias específicas de objetos según sea necesario.

//En resumen, el patrón Factory Method encapsula la lógica de creación de objetos en una clase separada (la "fábrica"),
//lo que promueve la abstracción, la flexibilidad y la modularidad en el diseño del software.
//Este patrón es útil cuando se necesita crear objetos de una jerarquía de clases,
//pero no se conoce la clase concreta en tiempo de compilación,
//o cuando se quiere delegar la creación de objetos a las subclases para permitir la variabilidad y la extensibilidad en la creación de objetos.


//Claro, en tu implementación estás aplicando el patrón Factory Method en la clase ErrorHandlerFactory. Aquí te muestro cómo se aplica el patrón Factory Method en tu código y cuál es la función de cada parte:

//Interfaz IErrorHandler:

//Función: Define un contrato para los manejadores de errores. Todas las implementaciones concretas de los manejadores de errores deben implementar esta interfaz, asegurando que cada manejador tenga un método ShowErrorMessage para mostrar mensajes de error.


//Implementación CategoriaErrorHandler:
//Función: Es una implementación concreta de IErrorHandler para manejar errores específicos relacionados con categorías. En este caso, muestra mensajes de error utilizando MessageBox cuando ocurren errores relacionados con categorías.


//Clase ErrorHandlerFactory:
//Función: Es la fábrica que proporciona una instancia adecuada de IErrorHandler según el tipo de error proporcionado. Utiliza un método estático GetErrorHandler que toma un parámetro errorType y devuelve una instancia de IErrorHandler correspondiente al tipo de error. Aquí es donde se aplica el patrón Factory Method:
//Factory Method: El método GetErrorHandler actúa como el Factory Method. Este método encapsula la lógica de creación de objetos IErrorHandler. Dependiendo del tipo de error proporcionado, este método devuelve una instancia adecuada de IErrorHandler (en este caso, CategoriaErrorHandler).


//En resumen, la clase ErrorHandlerFactory utiliza el patrón Factory Method para encapsular la creación de instancias de objetos IErrorHandler. Esto proporciona un punto centralizado para crear instancias de manejo de errores, lo que hace que el código sea más modular, flexible y fácil de mantener. Si necesitas agregar nuevos tipos de manejadores de errores en el futuro, simplemente puedes agregar nuevos casos al método GetErrorHandler, sin necesidad de cambiar el código que utiliza la fábrica.
