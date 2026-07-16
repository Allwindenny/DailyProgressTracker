namespace DailyProgressTracker.Models;

public class EmbeddingRecord
{
    public int TaskId { get; set; }

    public float[] Vector { get; set; } = Array.Empty<float>();
}