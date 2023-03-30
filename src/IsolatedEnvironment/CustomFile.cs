namespace IsolatedEnvironment;

internal abstract class CustomFile : ICustomFile
{
    public abstract void WriteAllBytes(string outputFile, byte[] content);
}
