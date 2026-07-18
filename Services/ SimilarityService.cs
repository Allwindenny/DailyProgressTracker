using DailyProgressTracker.Interfaces;

namespace DailyProgressTracker.Services;

public class SimilarityService : ISimilarityService
{
    public double CosineSimilarity(float[] vector1, float[] vector2)
    {
      double dotProduct = 0;

     for (int i = 0; i < vector1.Length; i++)
     {
       dotProduct += vector1[i] * vector2[i];
       }

     double magnitude1 = 0;

     for (int i = 0; i < vector1.Length; i++)
     {
       magnitude1 += vector1[i] * vector1[i];
       }

     magnitude1 = Math.Sqrt(magnitude1);

     return dotProduct;
    }
}