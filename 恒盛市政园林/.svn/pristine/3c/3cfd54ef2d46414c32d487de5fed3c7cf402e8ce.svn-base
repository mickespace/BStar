using System;

namespace WallE.Data.MySql
{
    /// <summary>
    /// 结果基类
    /// </summary>
    public class ApiResult
    {
        public static ApiResult Ok { get; } = new ApiResult();

        public ApiResult()
        {
            IsSuccess = true;
        }

        public ApiResult(Result result)
        {
            if (result.ResultType != ResultType.Ok)
            {
                IsSuccess = false;
                ErrorMessage = result.Message;
            }
            else
                IsSuccess = true;
        }

        public ApiResult(Exception exception)
        {
            IsSuccess = false;
            ErrorMessage = exception.Message;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 分页时使用的总条目
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// 结果的泛型基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResult
    {
        public ApiResult(T data)
        {
            Data = data;
        }

        public ApiResult(Exception e) : base(e) { }

        public T Data { get; set; }
    }
}