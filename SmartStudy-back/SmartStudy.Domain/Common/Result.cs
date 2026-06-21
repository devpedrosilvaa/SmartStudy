using SmartStudy.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public DomainError? Error { get; }

        protected Result(
            bool isSuccess,
            DomainError? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
            => new(true, null);

        public static Result Failure(
            DomainError error)
            => new(false, error);
    }
}
