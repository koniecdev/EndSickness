namespace EndSickness.Infrastructure.Services.FileStorage.DirectoryDecorator;

public class CustomDirectoryCreateService : ICustomDirectoryCreate
{
    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }
}
