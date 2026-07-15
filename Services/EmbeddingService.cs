using System.Net.Http;
using System.Text;
using System.Text.Json;
using DailyProgressTracker.Interfaces;

namespace DailyProgressTracker.Services;

public class EmbeddingService : IEmbeddingService
{
     private readonly HttpClient _httpClient;
    public EmbeddingService()
    {
        _httpClient = new HttpClient();
    }
   public float[] GenerateEmbedding(string text)
{
    var request = new
    {
        model = "nomic-embed-text",
        prompt = text
    };

    string json = JsonSerializer.Serialize(request);

    var content = new StringContent(
        json,
        Encoding.UTF8,
        "application/json");

    HttpResponseMessage response =
        _httpClient.PostAsync(
            "http://localhost:11434/api/embeddings",
            content).Result;

    string responseJson =
        response.Content.ReadAsStringAsync().Result;

    using JsonDocument document =
        JsonDocument.Parse(responseJson);

    JsonElement embedding =
        document.RootElement.GetProperty("embedding");

    List<float> vector = new();

    foreach (JsonElement value in embedding.EnumerateArray())
    {
        vector.Add(value.GetSingle());
    }

    return vector.ToArray();
}
}