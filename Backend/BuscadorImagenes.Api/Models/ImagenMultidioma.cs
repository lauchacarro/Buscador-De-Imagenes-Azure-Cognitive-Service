using System.Text.Json.Serialization;

namespace BuscadorImagenes.Api.Models
{
    public class ImagenMultidioma
    {
        [JsonPropertyName("id")]

        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("nombre_es")]

        public string NombreEs { get; set; } = string.Empty;
        [JsonPropertyName("nombre_en")]

        public string NombreEn { get; set; } = string.Empty;
        [JsonPropertyName("nombre_fr")]
        public string NombreFr { get; set; } = string.Empty;


        [JsonPropertyName("descripcion_es")]
        public string DescripcionEs { get; set; } = string.Empty;

        [JsonPropertyName("descripcion_en")]

        public string DescripcionEn { get; set; } = string.Empty;
        [JsonPropertyName("descripcion_fr")]

        public string DescripcionFr { get; set; } = string.Empty;

        [JsonPropertyName("palabras_es")]
        public List<string> PalabrasEs { get; set; } = new();

        [JsonPropertyName("palabras_en")]
        public List<string> PalabrasEn { get; set; } = new();

        [JsonPropertyName("palabras_fr")]
        public List<string> PalabrasFr { get; set; } = new();

        [JsonPropertyName("leyendas_es")]
        public List<string> LeyendasEs { get; set; } = new();

        [JsonPropertyName("leyendas_en")]
        public List<string> LeyendasEn { get; set; } = new();

        [JsonPropertyName("leyendas_fr")]
        public List<string> LeyendasFr { get; set; } = new();


        [JsonPropertyName("etiquetas_es")]
        public List<string> EtiquetasEs { get; set; } = new();

        [JsonPropertyName("etiquetas_en")]
        public List<string> EtiquetasEn { get; set; } = new();

        [JsonPropertyName("etiquetas_fr")]
        public List<string> EtiquetasFr { get; set; } = new();



        [JsonPropertyName("objetos_es")]
        public List<string> ObjetosEs { get; set; } = new();

        [JsonPropertyName("objetos_en")]
        public List<string> ObjetosEn { get; set; } = new();

        [JsonPropertyName("objetos_fr")]
        public List<string> ObjetosFr { get; set; } = new();


        [JsonPropertyName("sinonimos_es")]
        public List<string> SinonimosEs { get; set; } = new();

        [JsonPropertyName("sinonimos_en")]
        public List<string> SinonimosEn { get; set; } = new();

        [JsonPropertyName("sinonimos_fr")]
        public List<string> SinonimosFr { get; set; } = new();


        [JsonPropertyName("url")]

        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("fecha_carga")]
        public DateTimeOffset FechaCarga { get; set; } = DateTimeOffset.Now;
    }

}