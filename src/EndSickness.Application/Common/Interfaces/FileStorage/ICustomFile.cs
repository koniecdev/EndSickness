namespace EndSickness.Application.Common.Interfaces.FileStorage;

public interface ICustomFile
{
    void WriteAllBytes(string outputFile, byte[] content);
}
