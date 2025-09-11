using System;
using System.Collections.Generic;
using System.Diagnostics;
using NPMGUI.Interfaces;

namespace NPMGUI.Services;

public class ProcessService : IProcessService
{
    private List<Process> runningProcesses = new();
    public event EventHandler<ProcessChangedEventArgs>? ProcessListChanged;
    
    public void RegisterProcess(Process process)
    {
        runningProcesses.Add(process);
        OnProcessListChanged(process, true);
    }

    public void UnregisterProcess(Process process)
    {
        runningProcesses.Remove(process);
        OnProcessListChanged(process, false);
    }

    public List<Process> GetRunningProcesses()
    {
        return runningProcesses;
    }
    
    protected virtual void OnProcessListChanged(Process process, bool isAdded)
    {
        ProcessListChanged?.Invoke(this, new ProcessChangedEventArgs(process, isAdded));
    }
}

public class ProcessChangedEventArgs
{
    public Process Process { get; }
    public bool IsAdded { get; }

    public ProcessChangedEventArgs(Process process, bool isAdded)
    {
        Process = process;
        IsAdded = isAdded;
    }
}