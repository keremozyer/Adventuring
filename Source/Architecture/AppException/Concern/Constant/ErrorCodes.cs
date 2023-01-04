namespace Adventuring.Architecture.AppException.Concern.Constant;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public enum ErrorCodes
{
    UnexpectedError = 1,
    NodeMustBeBalanced = 2,
    DataNotFound = 3,
    InvalidPassword = 4,
    ConfigurationValueCannotBeEmpty = 5,
    InvalidConfigurationValue = 6,
    UsernameAlreadyTaken = 7,
    RoleAlreadyExists = 8,
    RoleCannotBeGranted = 9,
    UnauthorizedOperation = 10,
    DefaultRoleCannotBeRemoved = 11,
    GameIsAlreadyOver = 12,
    FieldCannotBeEmpty = 13,
    AdventureAlreadyExists = 14
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member