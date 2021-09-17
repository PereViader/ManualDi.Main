using System;

namespace ManualDi.Main
{
    public struct Result<T>
    {
        public bool IsError => Exception != null;
        public Exception Exception { get; }
        public T Value { get; }

        public Result(T value)
        {
            Exception = default;
            this.Value = value;
        }

        public Result(Exception exception)
        {
            Exception = exception;
            Value = default;
        }

        public T GetValueOrThrowIfError()
        {
            if (IsError)
            {
                throw Exception;
            }
            return Value;
        }

        public static implicit operator T(Result<T> result)
        {
            return result.GetValueOrThrowIfError();
        }
    }
}
