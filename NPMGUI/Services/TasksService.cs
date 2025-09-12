using System;
using System.Collections.Generic;
using System.Diagnostics;
using NPMGUI.Interfaces;

namespace NPMGUI.Services;

public enum TaskType
{
    Install,
    Uninstall,
    ScriptExecution
}

public class Task
{
    public string id { get; private set; }
    public Process process { get; private set; }

    public Task(TaskType type, string unique_id, Process process)
    {
        this.id = $"{type.ToString()}-{unique_id}".ToLower();
        this.process = process;
    }
}

public class TasksService : ITasksService
{
    private List<Task> runningTasks = new();
    public event EventHandler<TasksChangedEventArgs>? TaskListChanged;
    
    public void RegisterProcess(Task task)
    {
        runningTasks.Add(task);
        OnProcessListChanged(task.process, true);
    }

    public void UnregisterProcess(Task task)
    {
        runningTasks.Remove(task);
        OnProcessListChanged(task.process, false);
    }

    public List<Process> GetRunningProcesses()
    {
        throw new NotImplementedException();
    }

    public List<Task> GetRunningTasks()
    {
        return runningTasks;
    }

    public Task? FindTaskByParams(TaskType type, string uniqueId)
    {
        return runningTasks.Find(t => t.id == $"{type.ToString()}-{uniqueId}".ToLower());
    }

    private void OnProcessListChanged(Process process, bool isAdded)
    {
        TaskListChanged?.Invoke(this, new TasksChangedEventArgs(process, isAdded));
    }
}

public class TasksChangedEventArgs
{
    public Process Process { get; }
    public bool IsAdded { get; }

    public TasksChangedEventArgs(Process process, bool isAdded)
    {
        Process = process;
        IsAdded = isAdded;
    }
}