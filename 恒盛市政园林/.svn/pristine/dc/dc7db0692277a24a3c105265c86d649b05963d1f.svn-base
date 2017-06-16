using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace WallE.Data.MySql
{
    public static class CommonExtension
    {
        /// <summary>
        /// 将object类型中的属性转换为字典类型
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>IDictionary&lt;System.String, System.Object&gt;.</returns>
        public static IDictionary<string, object> AsDictionary(this object obj)
        {
            if (obj == null)
                return null;
            if (obj is IDictionary<string, object>)
                return obj as IDictionary<string, object>;

            var dic = new Dictionary<string, object>();
            foreach (var proInfo in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty))
            {
                dic[proInfo.Name] = proInfo.GetValue(obj);
            }
            return dic;
        }

        /// <summary>
        /// 获取指定名称的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>T.</returns>
        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue = default(T))
        {
            T result;
            return TryGetPropertyValue(obj, propertyName, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 尝试获取指定名称的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool TryGetPropertyValue<T>(this object obj, string propertyName, out T value)
        {
            value = default(T);
            var proInfo = obj?.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            if (proInfo == null)
                return false;
            var val = proInfo.GetValue(obj);
            if (val is string && typeof(T).IsEnum)
            {
                try
                {
                    value = (T)Enum.Parse(typeof(T), val as string, true);
                    return true;
                }
                catch (Exception)
                {
                    Debug.WriteLine($"无法将字符串{val}转换为{typeof(T)}类型！");
                    return false;
                }
            }
            if (!(val is T))
                return false;
            value = (T)val;
            return true;
        }
    }
}
