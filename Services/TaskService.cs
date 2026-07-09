using DailyProgressTracker.Data;
using DailyProgressTracker.Models;

namespace DailyProgressTracker.Services;

public class TaskService
{
    public void AddTask(string name, string? notes)
    {
        using var db = new AppDbContext();

        TaskItem task = new TaskItem(name);

        task.LearningNotes = notes;

        db.Tasks.Add(task);

        db.SaveChanges();
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
}