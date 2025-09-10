using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NPMGUI.Core.Helpers;

public static class ProcessExecutor
{
    public static async Task<TaskStatus> RunAsync(string alias, string arguments, string workDir)
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

        using var process = new Process { StartInfo = startInfo };
        try
        {
            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new Exception($"{alias} failed: {error}");

            Console.WriteLine(output);
            return TaskStatus.RanToCompletion;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error running {alias}: {ex.Message}");
            return TaskStatus.Faulted;
        }
    }
}
