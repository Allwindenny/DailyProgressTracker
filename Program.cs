using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();

    static void Main(string[] args)
    {
      /*  using (var db = new AppDbContext())
    {
        db.Database.EnsureCreated();
    }*/// removing the  for the EF core migration for new database
        while (true)
        {
            Console.WriteLine("\n==== Daily Progress Tracker ====");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Delete");
            Console.WriteLine("5. Exit");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();

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
                    return;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    /*static void AddTask()
    {
        Console.Write("Enter task name: ");
        string name = Console.ReadLine();

        tasks.Add(new TaskItem(name));

        Console.WriteLine("Task added successfully!");
    }
*/
    static void AddTask()
{
    Console.Write("Enter task name: ");
    string? name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Task name cannot be empty.");
        return;
    }

    Console.Write("Enter learning notes (optional, press Enter to skip): ");//adding the notes which can be empty
    string? notes = Console.ReadLine();

    using (var db = new AppDbContext())
    {
        TaskItem task = new TaskItem(name);

        task.LearningNotes = notes;//implementing the note feature

        db.Tasks.Add(task);
        db.SaveChanges();
    }

    Console.WriteLine("Task added successfully!");
}
    
  /*  static void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        Console.WriteLine("\nYour Tasks:");

        for (int i = 0; i < tasks.Count; i++)
       {
         string status = tasks[i].IsCompleted
          ? " Completed"
          : " Pending";

         Console.WriteLine(
          $"{i + 1}. {tasks[i].Name}"
          );

          Console.WriteLine(
          $"   Status: {status}"
            );

         Console.WriteLine(
         $"   Created: {tasks[i].CreatedDate}"
           );

         Console.WriteLine(
          $"   Completed: {(tasks[i].CompletedDate?.ToString() ?? "Not Completed")}"
          );

         Console.WriteLine();
         }
    }*/
    static void ViewTasks()
{
    using (var db = new AppDbContext())
    {
        var tasks = db.Tasks.ToList();

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
}

  /*  static void MarkTask()
    {
        ViewTasks();

        Console.Write("Enter task number to mark completed: ");
        int index = Convert.ToInt32(Console.ReadLine()) - 1;

        if (index >= 0 && index < tasks.Count)
        {
            tasks[index].MarkCompleted();
            Console.WriteLine("Task marked as completed!");
        }
        else
        {
            Console.WriteLine("Invalid task number!");
        }
    }*/
     static void MarkTask()
    {
     ViewTasks();

     Console.Write("Enter Task ID to mark as completed: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid input.");
        return;
    }

    using (var db = new AppDbContext())
    {
        TaskItem? task = db.Tasks.Find(id);

        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }
        if (task.IsCompleted)
       {
           Console.WriteLine("This task is already completed.");
           return;
        }
        if (string.IsNullOrWhiteSpace(task.LearningNotes))
       {
           Console.WriteLine("\nLearning notes are required before completing a task.");

           Console.Write("Enter your learning notes: ");
            string? notes = Console.ReadLine();

          if (string.IsNullOrWhiteSpace(notes))
         {
           Console.WriteLine("Task cannot be completed without learning notes.");
            return;
          }

         task.LearningNotes = notes;
        }

        task.MarkCompleted();

        db.SaveChanges();
    }

    Console.WriteLine("Task marked as completed!");
    
    
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

    using (var db = new AppDbContext())
    {
        TaskItem? task = db.Tasks.Find(id);

        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        db.Tasks.Remove(task);

        db.SaveChanges();
    }

    Console.WriteLine("Task deleted successfully!");
}

}
