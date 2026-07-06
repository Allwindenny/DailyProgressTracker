/*public class TaskItem
{   
    public int Id { get; set; }
    public string Name { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    new code with  changed database design(adding the notes)

   */// adding the new feature notes(which can be null on createion)
  public class TaskItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? LearningNotes { get; set; }// added the new feature

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public TaskItem(string name)
    {
        Name = name;
        IsCompleted = false;
        CreatedDate = DateTime.Now;
    }

    public void MarkCompleted()
    {
        IsCompleted = true;
        CompletedDate = DateTime.Now;
    }
}