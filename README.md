# 安装脚本控制台应用程序

这是一个使用 C# 编写的控制台应用程序，用于自动化安装多个软件和执行一些操作，例如 osu! 游戏相关操作。此应用程序将批处理文件转换为 .NET 控制台应用程序。

## 目录

- [前提条件](#前提条件)
- [项目结构](#项目结构)
- [安装](#安装)
- [运行](#运行)
- [功能说明](#功能说明)
- [错误处理](#错误处理)

## 前提条件

在开始之前，请确保您的系统上已经安装了以下软件：

- [.NET SDK](https://dotnet.microsoft.com/download)

## 项目结构

项目目录结构如下：

ConsoleApp1
├── ConsoleApp1.sln
└── ConsoleApp1
├── ConsoleApp1.csproj
├── Program.cs


## 安装

1. 克隆或下载该仓库到本地。

    ```sh
    git clone <仓库地址>
    cd ConsoleApp1
    ```

2. 确保所有需要安装的可执行文件都已放置在项目目录中，如上面的项目结构所示。

3. 打开终端或命令行，导航到项目目录并运行以下命令以还原 .NET 项目依赖项：

    ```sh
    dotnet restore
    ```

## 运行

1. 在终端或命令行中，导航到项目目录并运行以下命令以构建项目：

    ```sh
    dotnet build
    ```

2. 构建成功后，运行以下命令以启动应用程序：

    ```sh
    dotnet run
    ```

    这将启动控制台应用程序并开始安装各个软件。

## 功能说明

该控制台应用程序执行以下操作：

1. 安装 .NET 运行时
2. 安装 PotPlayer
3. 启动 v2rayN
4. 安装 .NET SDK
5. 安装 Discord
6. 安装 Python
7. 安装 Chrome
8. 安装其他软件
9. 安装 IDM
10. 安装 VS Code 并设置为中文，添加到 PATH 和右键菜单
11. 安装 TeamSpeak
12. 应用 TeamSpeak 中文翻译
13. osu! 相关操作：
    - 检测 USB 驱动器并搜索 osu 文件夹
    - 安装 osu! 并处理 osu 文件
14. 安装 GRLPackage

## 错误处理

- 对于每个安装操作，程序会检查相应的文件是否存在。如果文件不存在，将输出错误信息并跳过该操作。
- 在访问受保护路径时，程序会捕获 `UnauthorizedAccessException` 并输出相应的错误信息，以避免程序崩溃。

如果您在运行过程中遇到任何问题，请检查错误信息并确保所有文件都放置在正确的位置。

## 许可证
MIT 许可证 (MIT)

版权所有 (c) 2024 [SeaStar]

特此免费授予任何获得本软件及相关文档文件（“软件”）副本的人无限制地处理本软件的权利，包括但不限于使用、复制、修改、合并、出版、发布、再许可和/或出售本软件的副本，并允许本软件的供应者在满足以下条件的情况下也这样做：

上述版权声明和本许可声明应包含在本软件的所有副本或主要部分中。

本软件按“原样”提供，不提供任何明示或暗示的担保，包括但不限于适销性、特定用途适用性和不侵犯版权的担保。在任何情况下，作者或版权持有人均不对因本软件或本软件的使用或其他处理而产生的任何索赔、损害或其他责任承担任何责任，无论是在合同诉讼、侵权诉讼或其他诉讼中。
