using System.Text.Json.Serialization;

namespace BuscadorImagenes.Api.Models
{
    public class ImagenMultidioma
    {
        [JsonPropertyName("id")]

        public Guid Id { get; set; }

        [JsonPropertyName("nombre_es")]

        public string NombreEs { get; set; }
        [JsonPropertyName("nombre_en")]

        public string NombreEn { get; set; }
        [JsonPropertyName("nombre_fr")]
        public string NombreFr { get; set; }


        [JsonPropertyName("descripcion_es")]
        public string DescripcionEs { get; set; }

        [JsonPropertyName("descripcion_en")]

        public string DescripcionEn { get; set; }
        [JsonPropertyName("descripcion_fr")]

        public string DescripcionFr { get; set; }

        [JsonPropertyName("palabras_es")]
        public List<string> PalabrasEs { get; set; }

        [JsonPropertyName("palabras_en")]
        public List<string> PalabrasEn { get; set; }

        [JsonPropertyName("palabras_fr")]
        public List<string> PalabrasFr { get; set; }

        [JsonPropertyName("leyendas_es")]
        public List<string> LeyendasEs { get; set; }

        [JsonPropertyName("leyendas_en")]
        public List<string> LeyendasEn { get; set; }

        [JsonPropertyName("leyendas_fr")]
        public List<string> LeyendasFr { get; set; }


        [JsonPropertyName("etiquetas_es")]
        public List<string> EtiquetasEs { get; set; }

        [JsonPropertyName("etiquetas_en")]
        public List<string> EtiquetasEn { get; set; }

        [JsonPropertyName("etiquetas_fr")]
        public List<string> EtiquetasFr { get; set; }



        [JsonPropertyName("objetos_es")]
        public List<string> ObjetosEs { get; set; }

        [JsonPropertyName("objetos_en")]
        public List<string> ObjetosEn { get; set; }

        [JsonPropertyName("objetos_fr")]
        public List<string> ObjetosFr { get; set; }


        [JsonPropertyName("sinonimos_es")]
        public List<string> SinonimosEs { get; set; }

        [JsonPropertyName("sinonimos_en")]
        public List<string> SinonimosEn { get; set; }

        [JsonPropertyName("sinonimos_fr")]
        public List<string> SinonimosFr { get; set; }


        [JsonPropertyName("url")]

        public string Url { get; set; }

        [JsonPropertyName("fecha_carga")]
        public DateTimeOffset FechaCarga { get; set; }
    }

}