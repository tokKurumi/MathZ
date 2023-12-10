namespace MathZ.Shared.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException()
            : base("Given password does not match account password.")
        {
        }
    }
}