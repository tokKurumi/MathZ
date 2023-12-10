namespace MathZ.Services.UserAPI.Exceptions
{
    public class InvalidPatchDocumentException : Exception
    {
        public InvalidPatchDocumentException()
            : base("Invalid patch document body.")
        {
        }
    }
}