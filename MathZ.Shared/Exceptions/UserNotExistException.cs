namespace MathZ.Shared.Exceptions
{
    using System;

    public class UserNotExistException : Exception
    {
        public UserNotExistException()
            : base("Provided user does not exist.")
        {
        }
    }
}