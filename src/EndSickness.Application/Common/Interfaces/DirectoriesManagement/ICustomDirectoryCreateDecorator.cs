namespace EndSickness.Application.Common.Interfaces.FileStorage;

public interface ICustomDirectoryCreateDecorator : ICustomDirectoryCreate
{
    public ICustomDirectoryCreate Directory { get; }
}