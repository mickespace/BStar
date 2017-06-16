// ***********************************************************************
// Assembly         : WallE.dll
// Author           : xx Chen
// Created          : 2015-08-31 13:32
// 
// Last Modified By : 郭华华
// Last Modified On : 2015-08-31 15:22
// ***********************************************************************
// <copyright file="NotifyObject.cs" company="深圳筑星科技有限公司">
//      Copyright (c) BStar All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace WallE.Data.MySql
{
    /// <summary>
    /// 具有属性变更通知的对象基类
    /// </summary>
    public class NotifyObject : INotifyPropertyChanged
    {
        protected bool Set<T>(string propertyName, ref T field, T newValue)
        {
            if (Equals(field, newValue))
                return false;
            VerifyPropertyName(propertyName);
            OnPropertyChanging(propertyName, field, newValue);
            field = newValue;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// 在属性值更改时发生。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            var e = new PropertyChangedEventArgs(propertyName);
            OnPropertyChanged(e);
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, e);
        }

        protected virtual void OnPropertyChanging(string propertyName, object oldValue, object newValue) { }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) { }

        #region Debugging
        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This
        /// method does not exist in a Release build.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        protected void VerifyPropertyName(string propertyName)
        {
            //Verify that the property name matches a real,  
            //public, instance property on this object.
            var pro = GetType().GetRuntimeProperty(propertyName);
            if (pro != null)
                return;
            var msg = "Invalid property name: " + propertyName;
            throw new Exception(msg);
        }
        #endregion // Debugging Aides
    }
}