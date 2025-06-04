using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

public class GeminiAIService
{
    private static readonly string apiKey = "AIzaSyCGvoJwycHRHoNgN-bHzHD8DQJhePCvIGo";  
    private static readonly string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + apiKey;

    public static async Task<string> GeneraPianoAlimentareAsync(string prompt, HttpClient client)
    {
        try
        {
            client.Timeout = TimeSpan.FromSeconds(300);

            var body = new
            {
                contents = new[] {
                    new {
                        parts = new[] {
                            new { text = prompt }
                        }
                    }
                }
            };

            //serializza il corpo della richiesta in JSON
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //imposta l'intestazione di autorizzazione
            var response = await client.PostAsync(apiUrl, content);

            //controllo errore
            if (!response.IsSuccessStatusCode)
            {
                //deserializza la ripsosta
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Errore API: {response.StatusCode} - {responseContent}");
                return $"Errore: {response.StatusCode} - {responseContent}";
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            dynamic parsed = JsonConvert.DeserializeObject(responseJson);

           //se la risposta contiene i candidati, restituisci il primo
            if (parsed?.candidates != null && parsed.candidates.Count > 0)
            {
                return parsed.candidates[0].content.parts[0].text;
            }
            else
            {
                //controllo per risposta malformata
                Console.WriteLine("Risposta malformata dall'API: " + responseJson);
                return "Errore: Risposta malformata dall'API";
            }
        }
        catch (Exception ex)
        {
            //errore in caso della chiamata API
            Console.WriteLine($"Errore durante la chiamata API: {ex.Message}");
            return $"Errore: {ex.Message}";
        }
    }
}
