namespace IsolatedEnvironment;

internal interface ICustomFile
{
   void WriteAllBytes(string outputFile, byte[] content);
}
