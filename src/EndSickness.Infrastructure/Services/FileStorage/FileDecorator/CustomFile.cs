namespace EndSickness.Infrastructure.Services.FileStorage.FileDecorator;

public abstract class CustomFile : ICustomFile
{
    public abstract void WriteAllBytes(string outputFile, byte[] content);
}
