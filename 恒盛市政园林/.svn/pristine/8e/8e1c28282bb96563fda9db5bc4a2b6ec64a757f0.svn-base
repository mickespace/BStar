// ***********************************************************************
// Assembly         : WallE.Framework
// Author           : xxchen
// Created          : 11-26-2015
//
// Last Modified By : xxchen
// Last Modified On : 11-26-2015
// ***********************************************************************
// <copyright file="Log4Net.cs" company="深圳筑星科技有限公司">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using log4net.Config;

namespace WallE.Data.MySql
{
    /// <summary>
    /// Class Log4Net.
    /// </summary>
    internal class Log4Net : ILog
    {
        /// <summary>
        /// The _log
        /// </summary>
        private readonly log4net.ILog _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4Net"/> class.
        /// </summary>
        /// <param name="configPath">The configuration path.</param>
        public Log4Net(string configPath = null)
        {
            if (string.IsNullOrEmpty(configPath))
                configPath = Path.Combine(CoreUtil.AppDirectory, "log.config");
            _log = log4net.LogManager.GetLogger("WallE.Framework.Log4Net");
            if (File.Exists(configPath))
                XmlConfigurator.Configure(new FileInfo(configPath));
        }

        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            if (IsEnable)
                _log.Info(message);
        }

        /// <summary>
        /// Errors the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        public void Error(Exception e)
        {
            if (IsEnable)
                _log.Error(e);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Error(string message, Exception e)
        {
            if (IsEnable)
                _log.Error(message, e);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            if (IsEnable)
                _log.Debug(message);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Debug(string message, Exception e)
        {
            if (IsEnable)
                _log.Debug(message, e);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            if (IsEnable)
                _log.Warn(message);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Warn(string message, Exception e)
        {
            if (IsEnable)
                _log.Warn(message, e);
        }
    }
}
