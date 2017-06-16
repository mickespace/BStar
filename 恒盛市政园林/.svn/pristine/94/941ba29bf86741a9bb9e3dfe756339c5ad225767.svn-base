// ***********************************************************************
// Assembly         : WallE.dll
// Author           : xx Chen
// Created          : 2015-06-18 14:08
// 
// Last Modified By : 郭华华
// Last Modified On : 2015-08-31 15:22
// ***********************************************************************
// <copyright file="Result.cs" company="深圳筑星科技有限公司">
//      Copyright (c) BStar All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace WallE.Data.MySql
{
    /// <summary>
    /// 操作结果类
    /// </summary>
    public sealed class Result
    {
        #region 静态实例
        /// <summary>
        /// The _ok
        /// </summary>
        private static Result _ok;

        /// <summary>
        /// The _nochange
        /// </summary>
        private static Result _nochange;

        /// <summary>
        /// Gets the ok.
        /// </summary>
        /// <value>The ok.</value>
        public static Result Ok
        {
            get { return _ok ?? (_ok = new Result()); }
        }

        /// <summary>
        /// Gets the nochange.
        /// </summary>
        /// <value>The nochange.</value>
        public static Result Nochange
        {
            get { return _nochange ?? (_nochange = new Result(ResultType.NoChanged)); }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="error">The error.</param>
        /// <param name="data">The data.</param>
        public Result(ResultType resultType, Exception error, object data)
        {
            ResultType = resultType;
            Error = error;
            Data = data;
            Message = data == null ? resultType.ToString() : data.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public Result(object result)
            : this(ResultType.Ok, null, result)
        {
            Message = "Ok";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public Result(Exception error)
            : this(ResultType.Error, error, null)
        {
            if (error != null)
                Message = error.Message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        public Result(ResultType resultType)
            : this(resultType, null, null)
        {
            Message = resultType.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        public Result()
            : this(ResultType.Ok, null, null)
        {
            Message = "Ok";
        }

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>The type of the result.</value>
        public ResultType ResultType { get; private set; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>The error.</value>
        public Exception Error { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; private set; }
    }

    /// <summary>
    /// Enum ResultType
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// The ok
        /// </summary>
        Ok,

        /// <summary>
        /// The error
        /// </summary>
        Error,

        /// <summary>
        /// The no changed
        /// </summary>
        NoChanged,
    }
}