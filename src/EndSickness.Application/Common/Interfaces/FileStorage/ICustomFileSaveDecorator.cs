namespace EndSickness.Application.Common.Interfaces.FileStorage;

public interface ICustomFileSaveDecorator : ICustomFileSave
{
    public ICustomFileSave CustomFile { get; }
}