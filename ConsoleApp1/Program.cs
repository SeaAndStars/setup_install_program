using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        InstallNetRuntime(baseDirectory);
        InstallPotPlayer(baseDirectory);
        StartV2rayN(baseDirectory);
        InstallNetSdk(baseDirectory);
        InstallDiscord(baseDirectory);
        InstallPython(baseDirectory);
        InstallChrome(baseDirectory);
        InstallOtherSoftware(baseDirectory);
        InstallIDM(baseDirectory);
        InstallVsCode(baseDirectory);
        InstallTeamSpeak(baseDirectory);
        ApplyTeamSpeakTranslation(baseDirectory);
        HandleOsuOperations(baseDirectory);
        InstallGrlPackage(baseDirectory);

        Console.WriteLine("安装过程已完成。");
        Console.ReadLine(); // Pause the console to see the output
    }

    static void RunCommand(string filePath, string arguments, bool waitForExit = true)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        Process process = new Process();
        process.StartInfo.FileName = filePath;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WorkingDirectory = Path.GetDirectoryName(filePath);
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;
        process.Start();
        if (waitForExit)
        {
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            if (!string.IsNullOrEmpty(output))
                Console.WriteLine(output);
            if (!string.IsNullOrEmpty(error))
                Console.WriteLine($"Error: {error}");
        }
    }

    static void InstallNetRuntime(string baseDirectory)
    {
        Console.WriteLine("安装 .NET 运行时");
        RunCommand(Path.Combine(baseDirectory, "windowsdesktop-runtime-8.0.4-win-x64.exe"), "/S");
    }

    static void InstallPotPlayer(string baseDirectory)
    {
        Console.WriteLine("安装 PotPlayer");
        RunCommand(Path.Combine(baseDirectory, "potplayer.exe"), "/S");
    }

    static void StartV2rayN(string baseDirectory)
    {
        Console.WriteLine("启动 v2rayN");
        RunCommand(Path.Combine(baseDirectory, "v2rayN-Core\\v2rayN.exe"), "", false);
    }

    static void InstallNetSdk(string baseDirectory)
    {
        Console.WriteLine("安装 .NET SDK");
        RunCommand(Path.Combine(baseDirectory, "dotnet_5.0.7_64bit_Setup.exe"), "/S");
    }

    static void InstallDiscord(string baseDirectory)
    {
        Console.WriteLine("安装 Discord");
        RunCommand(Path.Combine(baseDirectory, "DiscordSetup.exe"), "/S /D=%LOCALAPPDATA%\\Discord");
    }

    static void InstallPython(string baseDirectory)
    {
        Console.WriteLine("安装 Python");
        RunCommand(Path.Combine(baseDirectory, "python-3.12.3-amd64.exe"), "/S PrependPath=1 InstallAllUsers=1");
    }

    static void InstallChrome(string baseDirectory)
    {
        Console.WriteLine("安装 Chrome");
        RunCommand(Path.Combine(baseDirectory, "ChromeStandaloneSetup64.exe"), "/S /D=%LOCALAPPDATA%\\Chrome");
    }

    static void InstallOtherSoftware(string baseDirectory)
    {
        Console.WriteLine("安装其他软件");
        RunCommand(Path.Combine(baseDirectory, "install.exe"), "/S");
    }

    static void InstallIDM(string baseDirectory)
    {
        Console.WriteLine("安装 IDM");
        RunCommand(Path.Combine(baseDirectory, "IDM\\IDMan.exe"), "/S", false);
    }

    static void InstallVsCode(string baseDirectory)
    {
        Console.WriteLine("安装 VS Code 并设置为中文，并添加到PATH和右键菜单");
        RunCommand(Path.Combine(baseDirectory, "VSCodeUserSetup-x64-1.89.0.exe"), "/S /ALLUSERS /LANG=zh-hans /VERYSILENT /MERGETASKS=\"runcode,addcontextmenufiles,addcontextmenufolders,addtopath\"");
    }

    static void InstallTeamSpeak(string baseDirectory)
    {
        Console.WriteLine("安装 TeamSpeak");
        RunCommand(Path.Combine(baseDirectory, "TeamSpeak3-Client-win64-3.6.2.exe"), "/S");
    }

    static void ApplyTeamSpeakTranslation(string baseDirectory)
    {
        Console.WriteLine("应用 TeamSpeak 中文翻译");
        RunCommand(Path.Combine(baseDirectory, "Chinese_Translation_zh-CN.ts3_translation"), "");
    }

    static void HandleOsuOperations(string baseDirectory)
    {
        Console.WriteLine("osu! 相关操作");
        string osuFolder = FindOsuFolderOnUsb();
        if (osuFolder == null)
        {
            Console.WriteLine("未在任何驱动器上找到 osu 文件夹。退出。");
            return;
        }

        Console.WriteLine($"在 {osuFolder} 找到 osu 文件夹");

        string osuPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop\\programs\\osu!install.exe");
        string osuAppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "osu!");
        string programsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop\\programs");

        if (Directory.Exists(osuAppDataPath))
        {
            Console.WriteLine("osu! 已安装。跳过安装。");
        }
        else
        {
            Console.WriteLine("osu! 未安装。继续安装。");
            if (!Directory.Exists(programsDir))
            {
                Console.WriteLine("创建程序目录...");
                Directory.CreateDirectory(programsDir);
            }

            if (!File.Exists(osuPath))
            {
                Console.WriteLine("下载 osu! 安装程序...");
                RunCommand("powershell", $"-Command \"Invoke-WebRequest -Uri 'https://m1.ppy.sh/r/osu!install.exe' -OutFile '{osuPath}'\"");
            }

            Console.WriteLine("运行 osu! 安装程序...");
            RunCommand(osuPath, "", true);
        }

        Console.WriteLine("将 osu 文件夹复制到程序目录...");
        RunCommand("xcopy", $"\"{osuFolder}\" \"{Path.Combine(programsDir, "osu")}\" /E /H /C /I");

        Console.WriteLine("将 .osz 文件移动到 osu 的 Songs 目录...");
        string osuSongsDir = Path.Combine(osuAppDataPath, "Songs");
        if (!Directory.Exists(osuSongsDir))
        {
            Directory.CreateDirectory(osuSongsDir);
        }

        RunCommand("move", $"\"{Path.Combine(programsDir, "osu\\*.osz")}\" \"{osuSongsDir}\"");
    }

    static string FindOsuFolderOnUsb()
    {
        for (char drive = 'D'; drive <= 'Z'; drive++)
        {
            string drivePath = $"{drive}:\\";
            if (Directory.Exists(drivePath))
            {
                string osuFolder = FindOsuFolder(drivePath);
                if (osuFolder != null)
                {
                    return osuFolder;
                }
            }
        }
        return null;
    }

    static string FindOsuFolder(string drive)
    {
        try
        {
            foreach (string dir in Directory.GetDirectories(drive, "osu", SearchOption.AllDirectories))
            {
                if (Directory.GetFiles(dir, "*.osz").Length > 0)
                {
                    return dir;
                }
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"访问被拒绝: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"错误: {ex.Message}");
        }
        return null;
    }

    static void InstallGrlPackage(string baseDirectory)
    {
        Console.WriteLine("安装 GRLPackage");
        RunCommand(Path.Combine(baseDirectory, "GRLPackage_1.0.24.0426_52pj.exe"), "/S");
    }
}
