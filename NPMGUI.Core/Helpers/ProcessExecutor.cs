using System.Diagnostics;
using System.Runtime.InteropServices;
using NPMGUI.Core.DTOs;

namespace NPMGUI.Core.Helpers;

public static class ProcessExecutor
{
    private static List<Process> _processList = [];
    
    public static ProcessExecution RunAsync(
        string alias,
        string arguments,
        string workDir,
        Action<string>? onOutput = null,
        Action<string>? onError = null)
    {
        string shell, cmd;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            shell = "cmd.exe";
            cmd = $"/c {alias} {arguments}";
        }
        else
        {
            shell = "/bin/bash";
            cmd = $"-c \"{alias} {arguments}\"";
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = shell,
            Arguments = cmd,
            WorkingDirectory = workDir,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        
        var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true };
        
        process.OutputDataReceived += (s, e) =>
        {
            if (e.Data != null)
                onOutput?.Invoke(e.Data);
        };

        process.ErrorDataReceived += (s, e) =>
        {
            if (e.Data != null)
                onError?.Invoke(e.Data);
        };

        var tcs = new TaskCompletionSource<bool>();

        process.Exited += (s, e) =>
        {
            tcs.TrySetResult(true);
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        _processList.Append(process);

        return new ProcessExecution
        {
            Process = process,
            Task = tcs.Task,
        };
    }

    public static void Stop(Process process)
    {
        if (process != null && !process.HasExited)
        {
            process.Kill(true);
            process.Dispose();
        }
    }

    public static List<Process> GetActiveProcesses()
    {
        return _processList;
    }
}
