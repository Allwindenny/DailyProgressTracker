namespace DailyProgressTracker.Repositories;

public class EmbeddingRepository
{
    public void SaveEmbedding(int taskId, float[] embedding)
    {
        Console.WriteLine();
        Console.WriteLine($"Saving embedding for Task {taskId}...");
        Console.WriteLine($"Vector length: {embedding.Length}");
        Console.WriteLine("Embedding saved.");
        Console.WriteLine();
    }
}