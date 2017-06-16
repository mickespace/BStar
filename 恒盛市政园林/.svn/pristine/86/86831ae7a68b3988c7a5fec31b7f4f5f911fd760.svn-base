// ***********************************************************************
// Assembly         : WallE.Core
// Author           : xxchen
// Created          : 12-03-2015
//
// Last Modified By : xxchen
// Last Modified On : 12-03-2015
// ***********************************************************************
// <copyright file="LogItem.cs" company="深圳筑星科技有限公司">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace WallE.Data.MySql
{
    /// <summary>
    /// Class LogItem.
    /// </summary>
    public class LogItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogItem" /> class.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public LogItem(LogType logType, string message, Exception e)
        {
            DateTime = DateTime.Now;
            LogType = logType;
            Message = message;
            Exception = e;
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>The date time.</value>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Gets the type of the log.
        /// </summary>
        /// <value>The type of the log.</value>
        public LogType LogType { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; private set; }
    }
}
