using EndSickness.Application.Common.Interfaces.FileStorage;

namespace EndSickness.Infrastructure.Services.FileStorage.FileDecorator;

public class CustomFileService : ICustomFile
{
    public void WriteAllBytes(string outputFile, byte[] content)
    {
        File.WriteAllBytes(outputFile, content);
    }
}
