using DailyProgressTracker.Interfaces;
using DailyProgressTracker.Repositories;
using DailyProgressTracker.Models;
namespace DailyProgressTracker.Services;

public class SearchService : ISearchService
{
    private readonly IEmbeddingService embeddingService;
    private readonly EmbeddingRepository embeddingRepository;
    private readonly ISimilarityService similarityService;
    private readonly TaskService taskService;

    public SearchService()
    {
        embeddingService = new EmbeddingService();
        embeddingRepository = new EmbeddingRepository();
        similarityService = new SimilarityService();
        taskService = new TaskService();
    }

    public TaskItem? FindMostRelevantTask(string question)
{
    float[] questionEmbedding =
        embeddingService.GenerateEmbedding(question);

    List<EmbeddingRecord> embeddings =
        embeddingRepository.LoadEmbeddings();

    double highestSimilarity = -1;
    int bestTaskId = -1;

    foreach (EmbeddingRecord embedding in embeddings)
 {
    double similarity =
        similarityService.CosineSimilarity(
            questionEmbedding,
            embedding.Vector);

    if (similarity > highestSimilarity)
    {
        highestSimilarity = similarity;
        bestTaskId = embedding.TaskId;
    }
 }
    return taskService.GetTaskById(bestTaskId);
}
}