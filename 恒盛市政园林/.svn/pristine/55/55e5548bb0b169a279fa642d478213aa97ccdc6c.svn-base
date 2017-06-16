using System;

namespace WallE.Data.MySql
{
    /// <summary>
    /// Class LoggerHelper.
    /// </summary>
    public static class LoggerHelper
    {
        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Info(string message)
        {
            if (M.LogManager != null)
                M.LogManager.Info(message);
        }

        /// <summary>
        /// Errors the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        public static void Error(Exception e)
        {
            if (M.LogManager != null)
                M.LogManager.Error(e);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public static void Error(string message, Exception e)
        {
            if (M.LogManager != null)
                M.LogManager.Error(message, e);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Debug(string message)
        {
            if (M.LogManager != null)
                M.LogManager.Debug(message);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public static void Debug(string message, Exception e)
        {
            if (M.LogManager != null)
                M.LogManager.Debug(message, e);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Warn(string message)
        {
            if (M.LogManager != null)
                M.LogManager.Warn(message);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public static void Warn(string message, Exception e)
        {
            if (M.LogManager != null)
                M.LogManager.Warn(message, e);
        }
    }
}
