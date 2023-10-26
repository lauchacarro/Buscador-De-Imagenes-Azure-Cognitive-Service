namespace BuscadorImagenes.Api.Models
{
    public class Prompts
    {

        public const string MultidiomaPrompt = """
            Estoy desarrollando una aplicación de buscador de imágenes multidioma con Azure Computer Vision  y Azure Cognitive Search.

            La aplicación va a soportar los idiomas Español (es), Ingles (en) y Francés (fr).

            Tengo el siguiente JSON con valores que me devolvio Computer Vision de una imagen.
            El campo Palabras son las palabras que se encuentran en la imagen.
            Los campos Leyendas, Etiquetas y Objetos son generados por Computer Vision.

            {0}

            Para mi aplicación multidioma necesito que realices las siguientes tareas:

            1) Traduzcas los campos Palabras, Leyendas, Etiquetas y Objetos a español, ingles y francés.
            2) Genera un Nombre creativo para la imagen basándote en los campos anteriores.
            3) Traducir el Nombre a español, ingles y francés.
            4) Genera una Descripción amigable de 140 palabras de la imagen basándote en los campos anteriores.
            5) Agrega a la Descripción emojis 
            6) Traducir la Descripción a español, ingles y francés.
            7) Genera 20 Sinónimos, palabras o frases que no se estén utilizando actualmente, que faciliten la búsqueda de la imagen. 
            8) Traducir los Sinónimos a español, ingles y francés.
            9) Completa el siguiente JSON con los campos originales, con el nombre, descripción y sustantivos que generaste, y todas sus traducciones.

            {
              "nombre_es": "",
              "nombre_en": "",
              "nombre_fr": "",
              "descripcion_es": "",
              "descripcion_en": "",
              "descripcion_fr": "",
              "palabras_es": [],
              "palabras_en": [],
              "palabras_fr": [],
              "leyendas_es": [],
              "leyendas_en": [],
              "leyendas_fr": [],
              "etiquetas_es": [],
              "etiquetas_en": [],
              "etiquetas_fr": [],
              "objetos_es": [],
              "objetos_en": [],
              "objetos_fr": [],
              "sinonimos_es": [],
              "sinonimos_en": [],
              "sinonimos_fr": []
            }

            Respondeme solamente con el nuevo JSON y todos sus campos completos.
            """;

    }

}
