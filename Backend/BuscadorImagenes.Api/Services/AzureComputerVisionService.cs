namespace BuscadorImagenes.Api.Services
{
    using Azure;
    using Azure.AI.Vision.Common;
    using Azure.AI.Vision.ImageAnalysis;

    using BuscadorImagenes.Api.Models;

    using System;
    using System.Threading.Tasks;


    public class AzureComputerVisionService
    {
        private readonly VisionServiceOptions _visionServiceOptions;

        public AzureComputerVisionService()
        {

            _visionServiceOptions = CreateVisionServiceOptions();
        }


        private static VisionServiceOptions CreateVisionServiceOptions()
        {
            string endpoint = "https://buscador-imagenes-test-vision.cognitiveservices.azure.com/";
            string apikey = null;
            VisionServiceOptions visionServiceOptions = new VisionServiceOptions(endpoint, new AzureKeyCredential(apikey));
            return visionServiceOptions;
        }


        public async Task<Imagen> AnalyzeImageAsync(Uri file)
        {
            var analysisOptions = new ImageAnalysisOptions()
            {
                Features =

                    ImageAnalysisFeature.DenseCaptions
                    | ImageAnalysisFeature.Objects
                    | ImageAnalysisFeature.Text
                    | ImageAnalysisFeature.Tags
            };


            using var imageSource = VisionSource.FromUrl(file);

            using var analyzer = new ImageAnalyzer(_visionServiceOptions, imageSource, analysisOptions);

            var result = await analyzer.AnalyzeAsync();


            var imagen = new Imagen();

            imagen.Etiquetas = result.Tags?.Select(tag => tag.Name).ToList() ?? new List<string>();
            imagen.Objetos = result.Objects?.Select(obj => obj.Name).ToList() ?? new List<string>();
            imagen.Leyendas = result.DenseCaptions?.Select(cap => cap.Content).ToList() ?? new List<string>();
            imagen.Palabras = result.Text?.Lines?.Select(txt => txt.Content).ToList() ?? new List<string>();


            return imagen;
        }
    }

}
