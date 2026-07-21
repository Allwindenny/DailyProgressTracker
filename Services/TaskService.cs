using DailyProgressTracker.Data;
using DailyProgressTracker.Models;
using DailyProgressTracker.Interfaces;
using DailyProgressTracker.Repositories;

namespace DailyProgressTracker.Services;

public class TaskService
{

    private readonly IEmbeddingService embeddingService;
    private readonly EmbeddingRepository embeddingRepository;

    public TaskService()
{
    embeddingService = new EmbeddingService();
    embeddingRepository = new EmbeddingRepository();
}
    
    public void AddTask(string name, string? notes)
{
    using var db = new AppDbContext();

    TaskItem task = new TaskItem(name);
    task.LearningNotes = notes;

    // First save the task
    db.Tasks.Add(task);
    db.SaveChanges();

    // Now task.Id contains the real database ID
    if (!string.IsNullOrWhiteSpace(notes))
    {
        float[] embedding = embeddingService.GenerateEmbedding(notes);

        Console.WriteLine($"Embedding created with {embedding.Length} dimensions.");

        embeddingRepository.SaveEmbedding(task.Id, embedding);
    }
}

    public List<TaskItem> GetAllTasks()
    {
        using var db = new AppDbContext();

        return db.Tasks.ToList();
    }
    public TaskItem? GetTaskById(int id)
{
    using var db = new AppDbContext();

    return db.Tasks.Find(id);
}
    public bool DeleteTask(int id)
    {
        using var db = new AppDbContext();

        TaskItem? task = db.Tasks.Find(id);

        if (task == null)
        {
            return false;
        }

        db.Tasks.Remove(task);
        db.SaveChanges();

        return true;
    }

    public string MarkTaskCompleted(int id, string? notes)
{
    using var db = new AppDbContext();

    TaskItem? task = db.Tasks.Find(id);

    if (task == null)
    {
        return "Task not found.";
    }

    if (task.IsCompleted)
    {
        return "This task is already completed.";
    }

    if (string.IsNullOrWhiteSpace(task.LearningNotes))
    {
        if (string.IsNullOrWhiteSpace(notes))
        {
            return "Learning notes are required before completing a task.";
        }

        task.LearningNotes = notes;
    }

    task.MarkCompleted();

    db.SaveChanges();

    return "Task completed successfully!";
}
}