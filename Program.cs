using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;// for reading the text for using a class builder called string
using DailyProgressTracker.Data;
using DailyProgressTracker.Models;
using DailyProgressTracker.Services;


class Program
{
    static TaskService taskService = new TaskService();
    
    static SearchService searchService = new SearchService();
    static List<TaskItem> tasks = new List<TaskItem>();

    static void Main(string[] args)
    {

      EmbeddingService embeddingService = new EmbeddingService();
      float[] embedding = embeddingService.GenerateEmbedding("Today I learned about Entity Framework Core.");
      Console.WriteLine($"Embedding size: {embedding.Length}");
      
        while (true)
        {
            Console.WriteLine("\n==== Daily Progress Tracker ====");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Delete");
            Console.WriteLine("5. Learning Assistant");
            Console.WriteLine("6. Exit");
            Console.Write("Choose option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;

                case "2":
                    ViewTasks();
                    break;

                case "3":
                    MarkTask();
                    break;

                case "4":
                    DeleteTask();
                    break;

                case "5":
                  AskLearningAssistant();
                  break;

                case "6":
                 return;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
   static void AddTask()
{
    Console.Write("Enter task name: ");
    string? name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Task name cannot be empty.");
        return;
    }

    string notes = ReadLearningNotes();

    taskService.AddTask(name, notes);

    Console.WriteLine("Task added successfully!");
}
 
    static void ViewTasks()
{
    
   
        List<TaskItem> tasks = taskService.GetAllTasks();

        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        Console.WriteLine("\nYour Tasks:");

        foreach (var task in tasks)
        {
            Console.WriteLine($"{task.Id}. {task.Name}");
            Console.WriteLine($"   Status: {(task.IsCompleted ? "Completed" : "Pending")}");
            Console.WriteLine($"   Created: {task.CreatedDate}");

            if (task.CompletedDate != null)
                Console.WriteLine($"   Completed: {task.CompletedDate}");
            else
                Console.WriteLine("   Completed: Not Completed");
            if (string.IsNullOrWhiteSpace(task.LearningNotes))//implementing the note feature
                Console.WriteLine("   Learning Notes: No notes added");
            else
                Console.WriteLine($"   Learning Notes: {task.LearningNotes}");

            Console.WriteLine();
        }
    
}

  
   static void MarkTask()
{
    ViewTasks();

    Console.Write("Enter Task ID to mark as completed: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid input.");
        return;
    }

    Console.WriteLine("\nEnter learning notes if required.");
    Console.WriteLine("Type END on a new line when finished.\n");

    string notes = ReadLearningNotes();

    string result = taskService.MarkTaskCompleted(id, notes);

    Console.WriteLine(result);
}
   static void DeleteTask()
{
    ViewTasks();

    Console.Write("Enter Task ID to delete: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid input.");
        return;
    }

    if (taskService.DeleteTask(id))
    {
        Console.WriteLine("Task deleted successfully!");
    }
    else
    {
        Console.WriteLine("Task not found.");
    }
}
//The new helper methode for the string building
static string ReadLearningNotes()
{
    Console.WriteLine("\nEnter your learning notes.");
    Console.WriteLine("Type END on a new line when finished.\n");

    StringBuilder builder = new StringBuilder();

    while (true)
    {
        string? line = Console.ReadLine();

        if (line == null)
            continue;

        if (line.Equals("END", StringComparison.OrdinalIgnoreCase))
            break;

        builder.AppendLine(line);
    }

    return builder.ToString().Trim();


} 
static void AskLearningAssistant()
{
    Console.WriteLine();
    Console.Write("Ask a question about your learning: ");

    string? question = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(question))
    {
        Console.WriteLine("Please enter a valid question.");
        return;
    }

    TaskItem? task = searchService.FindMostRelevantTask(question);

    if (task == null)
    {
        Console.WriteLine("\nNo relevant learning notes found.");
        return;
    }

    Console.WriteLine("\n===== Learning Assistant =====");
    Console.WriteLine($"Task: {task.Name}");
    Console.WriteLine();
    Console.WriteLine("Learning Notes:");
    Console.WriteLine(task.LearningNotes);
    Console.WriteLine();
    Console.WriteLine($"Status: {(task.IsCompleted ? "Completed" : "Pending")}");
    Console.WriteLine($"Created: {task.CreatedDate}");
}





}
