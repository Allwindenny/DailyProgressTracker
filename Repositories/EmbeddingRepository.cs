using System.Text.Json;
using DailyProgressTracker.Models;

namespace DailyProgressTracker.Repositories;

public class EmbeddingRepository
{
    private const string FilePath = "embeddings.json";

    // Save a new embedding to the JSON file
    public void SaveEmbedding(int taskId, float[] embedding)
    {
        // Load all existing embeddings
        List<EmbeddingRecord> embeddings = LoadEmbeddings();

        // Create a new embedding record
        EmbeddingRecord record = new EmbeddingRecord
        {
            TaskId = taskId,
            Vector = embedding
        };

        // Add the new embedding to the list
        embeddings.Add(record);

        // Convert the list into JSON
        string json = JsonSerializer.Serialize(
            embeddings,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        // Save the updated list back to the file
        File.WriteAllText(FilePath, json);

        Console.WriteLine($"Embedding saved to {FilePath}");
    }

    // Load all embeddings from the JSON file
    public List<EmbeddingRecord> LoadEmbeddings()
    {
        // If the file doesn't exist, return an empty list
        if (!File.Exists(FilePath))
        {
            return new List<EmbeddingRecord>();
        }

        // Read the JSON file
        string json = File.ReadAllText(FilePath);

        // Convert the JSON into a List<EmbeddingRecord>
        List<EmbeddingRecord>? embeddings =
            JsonSerializer.Deserialize<List<EmbeddingRecord>>(json);

        // Return the list (or an empty list if deserialization failed)
        return embeddings ?? new List<EmbeddingRecord>();
    }
}