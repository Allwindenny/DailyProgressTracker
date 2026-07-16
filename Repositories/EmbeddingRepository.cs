using System.Text.Json;
using DailyProgressTracker.Models;

namespace DailyProgressTracker.Repositories;

public class EmbeddingRepository
{ 
    private const string FilePath = "embeddings.json";
    public void SaveEmbedding(int taskId, float[] embedding)
    {
        EmbeddingRecord record = new EmbeddingRecord
        {
         TaskId = taskId,
          Vector = embedding
        };
        string json = JsonSerializer.Serialize(record,
        new JsonSerializerOptions
        {
          WriteIndented = true
         });
        File.WriteAllText(FilePath, json);
        Console.WriteLine($"Embedding saved to {FilePath}");
    }
}