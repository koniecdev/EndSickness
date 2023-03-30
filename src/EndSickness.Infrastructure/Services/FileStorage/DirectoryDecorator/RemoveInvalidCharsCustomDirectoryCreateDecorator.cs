using System.Text.RegularExpressions;

namespace EndSickness.Infrastructure.Services.FileStorage.DirectoryDecorator;

public class RemoveInvalidCharsCustomDirectoryCreateDecorator : ICustomDirectoryCreateDecorator
{
    public ICustomDirectoryCreate Directory { get; private set; }
    public RemoveInvalidCharsCustomDirectoryCreateDecorator(ICustomDirectoryCreate directory)
    {
        Directory = directory;
    }
    public void CreateDirectory(string path)
    {
        string output = FormatPath(path);
        Directory.CreateDirectory(output);
    }
    private static string FormatPath(string path)
    {
        RemoveInvalidPathChars(ref path);
        RemoveWhitespaces(ref path);
        return path;
    }
    private static string RemoveInvalidPathChars(ref string path)
    {
        string formattedPath = string.Join("_", path.Split(Path.GetInvalidPathChars())).Trim();
        return formattedPath;
    }
    private static string RemoveWhitespaces(ref string path)
    {
        string pattern = @"\s";
        string replacement = "__";
        string output = Regex.Replace(path, pattern, replacement);
        return output;
    }
}
