namespace IsolatedEnvironment;

internal class CustomFileService : CustomFile
{
    public override void WriteAllBytes(string outputFile, byte[] content)
    {
        File.WriteAllBytes(outputFile, content);
    }
}
