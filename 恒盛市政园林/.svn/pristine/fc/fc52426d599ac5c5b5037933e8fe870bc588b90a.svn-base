// ***********************************************************************
// Assembly         : WallE.Framework
// Author           : xxchen
// Created          : 11-26-2015
//
// Last Modified By : xxchen
// Last Modified On : 11-26-2015
// ***********************************************************************
// <copyright file="LogManager.cs" company="深圳筑星科技有限公司">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace WallE.Data.MySql
{
    /// <summary>
    /// Class LogManager.
    /// </summary>
    [Export(typeof(ILogManager))]
    internal class LogManager : ILogManager
    {
        private const int Capacity = 200;

        public LogManager()
        {
            _logs.Add(new Log4Net());
        }

        #region 默认对象
        /// <summary>
        /// The _logs
        /// </summary>
        private readonly List<ILog> _logs = new List<ILog>();

        private readonly Queue<LogItem> _logItems = new Queue<LogItem>();

        /// <summary>
        /// 添加一个日志记录器
        /// </summary>
        /// <param name="log">The log.</param>
        public void AppendLog(ILog log)
        {
            _logs.Add(log);
        }

        /// <summary>
        /// 移除一个日志记录器
        /// </summary>
        /// <param name="log">The log.</param>
        public void RemoveLog(ILog log)
        {
            _logs.Remove(log);
        }

        /// <summary>
        /// 获取所有的日志信息
        /// </summary>
        /// <returns>IEnumerable&lt;LogItem&gt;.</returns>
        public IEnumerable<LogItem> GetLogItems()
        {
            return _logItems;
        }

        private void AppendLogItem(LogItem logItem)
        {
            var flag = false;
            while (_logItems.Count > Capacity - 1)
            {
                _logItems.Dequeue();
                flag = true;
            }
            _logItems.Enqueue(logItem);
            if (flag)
                _logItems.TrimExcess();
        }
        #endregion

        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            if (!IsEnable)
                return;
            foreach (var log in _logs)
                log.Info(message);
            AppendLogItem(new LogItem(LogType.Info, message, null));
        }

        /// <summary>
        /// Errors the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        public void Error(Exception e)
        {
            if (!IsEnable)
                return;
            foreach (var log in _logs)
                log.Error(e);
            AppendLogItem(new LogItem(LogType.Error, "", e));
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Error(string message, Exception e)
        {
            if (!IsEnable)
                return;
            foreach (var log in _logs)
                log.Error(message, e);
            AppendLogItem(new LogItem(LogType.Error, message, e));
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            if (!IsEnable)
                return;
            System.Diagnostics.Debug.WriteLine(message);
            foreach (var log in _logs)
                log.Debug(message);
            AppendLogItem(new LogItem(LogType.Debug, message, null));
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Debug(string message, Exception e)
        {
            if (!IsEnable)
                return;
            System.Diagnostics.Debug.WriteLine(message);
            if (e != null)
                System.Diagnostics.Debug.WriteLine(e);
            foreach (var log in _logs)
                log.Debug(message, e);
            AppendLogItem(new LogItem(LogType.Debug, message, e));
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            if (!IsEnable)
                return;
            foreach (var log in _logs)
                log.Warn(message);
            AppendLogItem(new LogItem(LogType.Warn, message, null));
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Warn(string message, Exception e)
        {
            if (!IsEnable)
                return;
            foreach (var log in _logs)
                log.Warn(message, e);
            AppendLogItem(new LogItem(LogType.Warn, message, e));
        }
    }
}
