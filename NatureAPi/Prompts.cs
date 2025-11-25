namespace NatureAPi;

public static class Prompts
{
    public static string GenerateTrailsPrompt(string jsonData)
    {
        return $@"
        Eres un analista de datos experto en excursiones a lugares naturales de mexico.
        Analiza los sigueinte datos de trails (en JSON) {jsonData}

        Debes responder exlusivamnte en formato JSON de la sigueinte manera:
        {{
            ""totalTrails"": int,
            ""avgEstimatedTime"": string,
            ""MostLongerTrail"": string,
            ""avgEstimatedDistance"": string
            ""patterns"": [string],
        }}
        En el apartado de patterns agrega apartados como: 
        Cual es el trail mas turistico.
        Cual es el trail mas cerca de ciudad de mexico.
        Cual es el mas recomendado por los turistas extranjeros.
        Si por alguna razonn no puede generar esta respuesta vlaida, por ejeplo: te hacen falta datos o tienes algun error en el formato responde SOLO con el texto: error.
        No me saludes, no me des explicaciones, no me des comentarios y no incluyas texto adicional.
";
        
        
    }
    
   
}