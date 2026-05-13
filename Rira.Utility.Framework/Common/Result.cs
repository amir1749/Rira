using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.Common
{
    public class Result
    {
        public bool Succeed { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }

        public static Result Okay(string message = "Operation is Successfull", int statusCode = 200) => new Result()
        {
            Succeed = true,
            Message = message,
            StatusCode = statusCode
        };

        public static Result Error(string message = "Operation failed", int statusCode = 500) => new Result()
        {
            Succeed = false,
            Message = message,
            StatusCode = statusCode
        };

        public static Result Exception(System.Exception ex, int statusCode = 500) => new Result()
        {
            Succeed = false,
            Message = ex.Message,
            StatusCode = statusCode
        };

        public static implicit operator bool(Result result) => result.Succeed;

        public override bool Equals(object obj) => obj is Result result && (this.Succeed == result.Succeed && this.StatusCode == result.StatusCode);

        public override int GetHashCode() => Convert.ToInt32(this.Succeed) + this.StatusCode;
    }
    public class Result<T> : Result
    {
        public T Data { get; set; }

        public static Result<T> Okay(T data, string message = "", int statusCode = 200)
        {
            Result<T> result = new Result<T>();
            result.Succeed = true;
            result.Message = message;
            result.Data = data;
            result.StatusCode = statusCode;
            return result;
        }

        public static Result<T> Error(string message = "", int statusCode = 500)
        {
            Result<T> result = new Result<T>();
            result.Succeed = false;
            result.Message = message;
            result.StatusCode = statusCode;
            return result;
        }

        public static Result<T> Exception(System.Exception ex, int statusCode = 500)
        {
            Result<T> result = new Result<T>();
            result.Succeed = false;
            result.Message = ex.Message;
            result.StatusCode = statusCode;
            return result;
        }

        public static implicit operator bool(Result<T> result) => result.Succeed;

        public override bool Equals(object obj)
        {
            if (!(obj is Result<T> result))
                return false;
            return this.Succeed && (object)this.Data != null ? this.Succeed == result.Succeed && this.StatusCode == result.StatusCode && this.Data.Equals((object)result.Data) : this.Succeed == result.Succeed && this.StatusCode == result.StatusCode;
        }

        public override int GetHashCode() => (object)this.Data == null ? Convert.ToInt32(this.Succeed) + this.StatusCode : Convert.ToInt32(this.Succeed) + this.StatusCode + this.Data.GetHashCode();
    }
}
