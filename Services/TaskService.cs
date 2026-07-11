using DailyProgressTracker.Data;
using DailyProgressTracker.Models;
using DailyProgressTracker.Interfaces;

namespace DailyProgressTracker.Services;

public class TaskService
{

    private readonly IEmbeddingService embeddingService;

    public TaskService()
{
    embeddingService = new EmbeddingService();
}
    public void AddTask(string name, string? notes)
    {
        using var db = new AppDbContext();

        TaskItem task = new TaskItem(name);
        task.LearningNotes = notes;

        db.Tasks.Add(task);
        db.SaveChanges();

        if (!string.IsNullOrWhiteSpace(notes))
     {
        embeddingService.GenerateEmbedding(notes);
      }
    }

    public List<TaskItem> GetAllTasks()
    {
        using var db = new AppDbContext();

        return db.Tasks.ToList();
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