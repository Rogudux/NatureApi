namespace NatureAPi;

public static class Prompts
{
    public static string GenerateTrailsPrompt(string jsonData)
    {
        return $@"
        Eres un analista de datos experto en excursiones y atracciones turisticas de lugares naturales en mexico.
        Analiza los sigueinte datos de trails (en JSON) {jsonData}

        Debes responder exclusivamente en formato JSON de la siguiente manera:
        {{
            ""totalTrails"": int,
            ""avgEstimatedTime"": string,
            ""MostLongerTrail"": string,
            ""avgEstimatedDistance"": string
            ""patterns"": [string],
        }}
        En el apartado de patterns agrega apartados como: 
        Cual es el trail mas turistico de los disponibles?
        Cual es el trail mas cercano de ciudad de mexico?
        Cual es el mas recomendado por los turistas extranjeros?
        Si por alguna razon no puede generar esta respuesta valida, por ejeplo: te hacen falta datos o tienes algun error en el formato responde cual fue el error, se claro con cual fue el error.
        No me saludes, no me des explicaciones, no me des comentarios y no incluyas texto adicional.
";
        
        
    }
    
   
}