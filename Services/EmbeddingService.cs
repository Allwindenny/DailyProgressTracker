using DailyProgressTracker.Interfaces;

namespace DailyProgressTracker.Services;

public class EmbeddingService : IEmbeddingService
{
    public float[] GenerateEmbedding(string text)
    {
        Console.WriteLine();
        Console.WriteLine("Generating embedding...");
        Console.WriteLine($"Learning note length: {text.Length} characters");
        Console.WriteLine("Embedding generated successfully.");
        Console.WriteLine();

        return Array.Empty<float>();
    }
}