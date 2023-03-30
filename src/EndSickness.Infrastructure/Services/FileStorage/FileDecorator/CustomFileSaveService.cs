namespace EndSickness.Infrastructure.Services.FileStorage.FileDecorator;

public class CustomFileSaveService : ICustomFileSave
{
    public void WriteAllBytes(string outputFile, byte[] content)
    {
        File.WriteAllBytes(outputFile, content);
    }
}
