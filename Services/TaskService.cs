using System;
using System.Collections.Generic;
using SimpleTaskManager.Models;

namespace SimpleTaskManager.Services
{
    class TaskService
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private int nextId = 1;

        public List<TaskItem> GetAll()
        {
            return tasks;
        }

        public void AddTask(string title, string description, DateTime? dueDate)
        {
            tasks.Add(new TaskItem
            {
                Id = nextId++,
                Title = title,
                Description = description,
                DueDate = dueDate,
                IsCompleted = false
            });
        }

        public bool MarkDone(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task == null) return false;
            
            task.IsCompleted = true;
            return true;
        }

        public bool DeleteTask(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task == null) return false;

            tasks.Remove(task);
            return true;
        }
    }
}