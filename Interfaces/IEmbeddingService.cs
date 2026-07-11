namespace DailyProgressTracker.Interfaces;

public interface IEmbeddingService
{
    float[] GenerateEmbedding(string text);
}