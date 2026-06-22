using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Domain.Common.Errors
{
    public static class UserError
    {
        public static DomainError UserEmailAlreadyExists =>
            new DomainError(
                    "USER_EMAIL_ALREADY_EXISTS",
                    "User with this email already exists."
                );

        public static DomainError UserEmailNotFound =>
            new DomainError(
                    "USER_EMAIL_NOT_FOUND",
                    "User with email not found."
                );
        public static DomainError UserPasswordInvalid =>
            new DomainError(
                    "USER_PASSWORD_INVALID",
                    "User password invalid or incorrect."
                );
    }
}
