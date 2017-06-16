// ***********************************************************************
// Assembly         : WallE.Core
// Author           : xxchen
// Created          : 11-26-2015
//
// Last Modified By : xxchen
// Last Modified On : 11-26-2015
// ***********************************************************************
// <copyright file="ILogManager.cs" company="深圳筑星科技有限公司">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace WallE.Data.MySql
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public interface ILogManager : ILog
    {
        /// <summary>
        /// 添加一个日志记录器
        /// </summary>
        /// <param name="log">The log.</param>
        void AppendLog(ILog log);

        /// <summary>
        /// 移除一个日志记录器
        /// </summary>
        /// <param name="log">The log.</param>
        void RemoveLog(ILog log);

        /// <summary>
        /// 获取所有的日志信息
        /// </summary>
        /// <returns>IEnumerable&lt;LogItem&gt;.</returns>
        IEnumerable<LogItem> GetLogItems();
    }
}
