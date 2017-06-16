// ***********************************************************************
// Assembly         : WallE.Core
// Author           : xxchen
// Created          : 12-03-2015
//
// Last Modified By : xxchen
// Last Modified On : 12-03-2015
// ***********************************************************************
// <copyright file="LogType.cs" company="深圳筑星科技有限公司">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WallE.Data.MySql
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// The information
        /// </summary>
        Info,
        /// <summary>
        /// The debug
        /// </summary>
        Debug,
        /// <summary>
        /// The warn
        /// </summary>
        Warn,
        /// <summary>
        /// The error
        /// </summary>
        Error,
    }
}
