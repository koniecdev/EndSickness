using EndSickness.Domain.Entities;
using MediatR;

namespace EndSickness.Application.Common.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException() : base($"Resource do not exists")
    {

    }
    public ResourceNotFoundException(string resourceName, int resoruceId) : base($"Resource {resourceName} with id of: {resoruceId} could not be found")
    {

    }
}