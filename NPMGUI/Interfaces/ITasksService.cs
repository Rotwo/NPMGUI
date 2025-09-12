using System;
using System.Collections.Generic;
using System.Diagnostics;
using NPMGUI.Services;

namespace NPMGUI.Interfaces;

public interface ITasksService
{
    event EventHandler<TasksChangedEventArgs>? TaskListChanged;
    
    void RegisterProcess(Task task);
    void UnregisterProcess(Task task);
    List<Task> GetRunningTasks();
    Task? FindTaskByParams(TaskType type, string uniqueId);
}