namespace EndSickness.Application.Common.Interfaces;

public interface IResourceOwnershipService
{
    public void CheckOwnership(string ownerId);
}