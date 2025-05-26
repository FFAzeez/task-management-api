
namespace TeamTaskManagementAPI.Domain.BindingModels.Common
{
    public static class Constant
    {
        public const string Success = "Data successfully Fetched";
        public const string SuccessCreated = "Created successfully";
        public const string Error = "An unexpected error occurred";
        public const string SuccessUpdate = "Updated Successfully";
        public const string SuccessDelete = "Deleted Successfully";
        public const bool SuccessBool = true;
        public const bool FailedBool = false;
        public const string TaskNotFound = "Task Not Found";
        public const string UserNotFound = "User not found";
        public const string UserNotMember = "User is not a member of the team";
        public const string CannotDeleteTask = "Only admin can delete tasks.";
        public const string Admin = "Admin";
        public const string AlreadyMember = "User is already a member of the team";
        
    }
}
