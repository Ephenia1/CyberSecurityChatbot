using System.Collections.Generic;

namespace WpfApp2
{
    public class TaskManager
    {
        public List<string> Tasks { get; set; }

        public TaskManager()
        {
            Tasks = new List<string>();
        }

        public void AddTask(string task)
        {
            Tasks.Add(task);
        }

        public string ViewTasks()
        {
            if (Tasks.Count == 0)
                return "No tasks available.";

            return string.Join("\n", Tasks);
        }
    }
}