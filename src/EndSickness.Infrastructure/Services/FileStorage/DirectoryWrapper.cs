namespace EndSickness.Infrastructure.Services.FileStorage;
public class DirectoryWrapper : IDirectoryWrapper
{
    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }
}
