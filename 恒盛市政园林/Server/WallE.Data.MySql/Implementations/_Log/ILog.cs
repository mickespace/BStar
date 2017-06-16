// ***********************************************************************
// Assembly         : WallE.Core
// Author           : xxchen
// Created          : 11-26-2015
//
// Last Modified By : xxchen
// Last Modified On : 11-26-2015
// ***********************************************************************
// <copyright file="ILog.cs" company="深圳筑星科技有限公司">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace WallE.Data.MySql
{
    /// <summary>
    /// Interface ILog
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 是否启用Log
        /// </summary>
        /// <value><c>true</c> if this instance is enable; otherwise, <c>false</c>.</value>
        bool IsEnable { get; set; }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Errors the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        void Error(Exception e);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        void Error(string message, Exception e);

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        void Debug(string message, Exception e);

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(string message);
        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        void Warn(string message, Exception e);
    }
}