namespace SimpleTaskManager.Models
{
    class TaskItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "Completed" : "Pending";
            string due = DueDate.HasValue ? DueDate.Value.ToString("dd-MM-yyyy") : "No due date";
            return $"{Id}. [{status}] {Title} (Due Date: {due}) Deskripsi: {Description}";
        }
    }
}