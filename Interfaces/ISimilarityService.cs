namespace DailyProgressTracker.Interfaces;

public interface ISimilarityService
{
    double CosineSimilarity(float[] vector1, float[] vector2);
}