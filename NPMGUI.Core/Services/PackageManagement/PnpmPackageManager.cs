using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NPMGUI.Core.Services.PackageManagement;

public class PnpmPackageManager : PackageManager
{
    public PnpmPackageManager()
    {
        Alias = "pnpm";
        LookupFiles = ["pnpm-lock.yaml"];
    }

    public override async Task<TaskStatus> InstallPackage(string package, string workDir, bool isDevDependency = false)
    {
        string shell;
        string command;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            shell = "cmd.exe";
            command = $"/c {Alias} add {(isDevDependency ? "-D" : "")} {package}";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            shell = "/bin/bash";
            command = $"-c \"{Alias} add {(isDevDependency ? "-D" : "")} {package}\"";
        }
        else
        {
            throw new PlatformNotSupportedException("Unsupported OS platform");
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = shell,
            Arguments = command,
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

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new Exception($"npm install failed: {error}");
            }

            Console.WriteLine(output);
            return TaskStatus.RanToCompletion;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error installing package: {ex.Message}");
            return TaskStatus.Faulted;
        }
    }
}