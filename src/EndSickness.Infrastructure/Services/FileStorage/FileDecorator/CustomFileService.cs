namespace EndSickness.Infrastructure.Services.FileStorage.FileDecorator;

public class CustomFileService : CustomFile
{
    public override void WriteAllBytes(string outputFile, byte[] content)
    {
        File.WriteAllBytes(outputFile, content);
    }
}
