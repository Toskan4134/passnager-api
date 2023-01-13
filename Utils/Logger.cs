using passnager_api;
using Microsoft.EntityFrameworkCore;
public class Logger
{

    private static string _path;
    private static string _folder;
    private static string _filePath;
    public Logger(string path)
    {
        _path = path;
        CheckAndCreateFolder();
        CreateFile();
        Console.WriteLine($"Path: {_path}, Folder: {_folder}, File: {_filePath}");
    }

    private void CheckAndCreateFolder()
    {
        DateTime date = DateTime.Now;
        _folder = date.ToString("dd-MM-yyyy");
        string folderDir = Path.Combine(_path, _folder);
        if (Directory.Exists(folderDir)) return;
        Directory.CreateDirectory(folderDir);
    }
    private static void CreateFile()
    {
        DateTime date = DateTime.Now;
        _filePath = Path.Combine(_path, _folder, $"log_{((DateTimeOffset)date).ToUnixTimeSeconds()}.txt");
        File.Create(_filePath).Close();
        File.AppendAllText(_filePath, "Log Iniciado\n\n");
    }


    public static void addLog(string text, string type = "info")
    {
        DateTime date = DateTime.Now;
        if (!File.Exists(_filePath)) CreateFile();
        string header = "";
        switch (type)
        {
            case "info":
                header = "[INFO]";
                break;
            case "warn":
                header = "[WARN]";
                break;
            case "error":
                header = "[ERROR]";
                break;
        }
        File.AppendAllText(_filePath, date.ToLongTimeString() + " " + header + " " + text + "\n\n");
    }
    // public async Task<List<ProfileEntity>> GetAll()
    // {
    // }

    // public async Task<ProfileEntity> GetProfileById(int id)
    // {
    // }

    // public async Task<Boolean> CheckLogin(ProfileEntity profile)
    // {
    // }

    // public async Task<ProfileEntity> Create(ProfileEntity profile)
    // {
    // }

    // public async Task<ProfileEntity> Update(ProfileEntity profile)
    // {
    // }


    // public async Task<ProfileEntity> DeleteById(int id)
    // {
    // }
}

