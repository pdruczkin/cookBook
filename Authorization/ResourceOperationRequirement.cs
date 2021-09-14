using Microsoft.AspNetCore.Authorization;

namespace cookBook.Authorization
{
    public enum ResourceOperation
    {
        Create,
        AddToExisted,
        Read,
        Update,
        Delete
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get;}

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
    }
}