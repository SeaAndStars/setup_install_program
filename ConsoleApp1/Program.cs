using static Install;
using System;

class Program
{
    static void Main(string[] args)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        Init.Initialize(baseDirectory);
        Install.ExecuteInstallations(baseDirectory);

        Console.WriteLine("安装过程已完成。");
        Console.ReadLine(); // Pause the console to see the output
    }
}
