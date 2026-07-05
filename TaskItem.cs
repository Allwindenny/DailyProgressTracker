public class TaskItem
{   
    public int Id { get; set; }
    public string Name { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public TaskItem(string name)
    {
        Name = name;
        IsCompleted = false;

        CreatedDate = DateTime.Now;

        CompletedDate = null;
    }

    public void MarkCompleted()
    {
        IsCompleted = true;

        CompletedDate = DateTime.Now;
    }
}