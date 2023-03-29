namespace EndSickness.Application.Common.Interfaces.Services;

public interface IFileWrapper
{
    public void WriteAllBytes(string outputFile, byte[] content);
}
