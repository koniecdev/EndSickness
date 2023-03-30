namespace EndSickness.Application.Common.Interfaces.FileStorage;

public interface ICustomDirectoryDecorator : ICustomDirectory
{
    public ICustomDirectory Directory { get; }
}