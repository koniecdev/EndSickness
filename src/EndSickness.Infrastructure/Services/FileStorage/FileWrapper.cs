namespace EndSickness.Infrastructure.Services.FileStorage;
public class FileWrapper : IFileWrapper
{
    public void WriteAllBytes(string outputFile, byte[] content)
    {
        File.WriteAllBytes(outputFile, content);
    }
}
