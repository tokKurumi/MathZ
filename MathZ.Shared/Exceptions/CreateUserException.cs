﻿namespace MathZ.Shared.Exceptions
{
    using Microsoft.AspNetCore.Identity;

    public class CreateUserException : Exception
    {
        public IEnumerable<IdentityError> Problems { get; set; } = Enumerable.Empty<IdentityError>();
    }
}