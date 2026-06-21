using SmartStudy.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Domain.Common
{
    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(
            T value)
            : base(true, null)
        {
            Value = value;
        }

        private Result(
            DomainError error)
            : base(false, error)
        {
        }

        public static Result<T> Success(
            T value)
            => new(value);

        public static new Result<T> Failure(
            DomainError error)
            => new(error);
    }
}
