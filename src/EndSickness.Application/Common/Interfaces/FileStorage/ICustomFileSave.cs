namespace EndSickness.Application.Common.Interfaces.FileStorage;

public interface ICustomFileSave
{
    void WriteAllBytes(string outputFile, byte[] content);
}
