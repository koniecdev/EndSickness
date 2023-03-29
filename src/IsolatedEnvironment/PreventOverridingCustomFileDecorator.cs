namespace IsolatedEnvironment;

internal class PreventOverridingCustomFileDecorator : CustomFileDecorator
{
    public PreventOverridingCustomFileDecorator(ICustomFile customFile) : base(customFile)
    {
    }

    public override void WriteAllBytes(string outputFile, byte[] content)
    {
        CustomFile.WriteAllBytes(RenameFile(outputFile), content);
    }

    private static string RenameFile(string outputFile)
    {
        string path = Path.GetDirectoryName(outputFile) ?? string.Empty;
        string fileName = Path.GetFileNameWithoutExtension(outputFile);
        string extension = Path.GetExtension(outputFile);
        string newFileName = $"{fileName}Copy{extension}";
        int counter = 1;

        while (File.Exists(Path.Combine(path, newFileName)))
        {
            newFileName = $"{fileName}Copy{counter}{extension}";
            counter++;
        }

        return Path.Combine(path, newFileName);
    }
}