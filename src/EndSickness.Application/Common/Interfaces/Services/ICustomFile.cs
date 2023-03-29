namespace EndSickness.Application.Common.Interfaces.Services;

public interface ICustomFile
{
   void WriteAllBytes(string outputFile, byte[] content);
}
