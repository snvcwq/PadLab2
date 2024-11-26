using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string batchFilePath = @"C:\Users\Asus\Desktop\PadLab2\rundotnet.bat";

        ExecuteBatchFile(batchFilePath);
    }

    static void ExecuteBatchFile(string path)
    {
        Process process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"/C {path}";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        process.WaitForExit();

        Console.WriteLine(process.StandardOutput.ReadToEnd());
    }
}
