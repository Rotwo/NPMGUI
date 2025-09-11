using System;
using System.Collections.Generic;
using System.Diagnostics;
using NPMGUI.Services;

namespace NPMGUI.Interfaces;

public interface IProcessService
{
    event EventHandler<ProcessChangedEventArgs>? ProcessListChanged;
    
    void RegisterProcess(Process process);
    void UnregisterProcess(Process process);
    List<Process> GetRunningProcesses();
}