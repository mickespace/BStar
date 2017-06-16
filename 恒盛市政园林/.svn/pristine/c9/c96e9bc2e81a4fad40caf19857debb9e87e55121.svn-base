// ***********************************************************************
// Assembly         : WallE.Core.dll
// Author           : xxchen
// Created          : 11-26-2015
//
// Last Modified By : xxchen
// Last Modified On : 11-26-2015
// ***********************************************************************
// <copyright file="CoreUtil.cs" company="深圳筑星科技有限公司">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WallE.Data.MySql
{
    /// <summary>
    /// Class CoreUtil.
    /// </summary>
    public static class CoreUtil
    {
        /// <summary>
        /// 应用程序所在的文件夹
        /// </summary>
        public static string AppDirectory
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"bin"); }
        }

        private static string _appDocDirectory;

        /// <summary>
        /// 应用程序的Document文件夹
        /// </summary>
        public static string AppDocDirectory
        {
            get
            {
                if (!string.IsNullOrEmpty(_appDocDirectory))
                    return _appDocDirectory;
                var dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var assembly = Assembly.GetEntryAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                var productNAme = string.IsNullOrEmpty(fvi.ProductName) ? "BIM-STAR" : fvi.ProductName;
                _appDocDirectory = Path.Combine(dir, productNAme);
                if (!Directory.Exists(_appDocDirectory))
                    Directory.CreateDirectory(_appDocDirectory);
                return _appDocDirectory;
            }
        }

        /// <summary>
        /// 应用程序全局设置文件夹
        /// </summary>
        public static string AppSettingDirectory
        {
            get
            {
                var dir = Path.Combine(AppDocDirectory, "Settings");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }

        /// <summary>
        /// 获取应用程序子文档文件夹下的子文件夹
        /// </summary>
        /// <param name="subDirName">Name of the sub dir.</param>
        /// <returns>System.String.</returns>
        public static string GetAppDocDirectory(string subDirName)
        {
            var dir = Path.Combine(AppDocDirectory, subDirName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="items">The items.</param>
        public static void AddRange<T>(this ICollection<T> col, IEnumerable<T> items)
        {
            items.ForEachEx(col.Add);
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEachEx<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (action == null || items == null)
                return;
            foreach (var item in items)
                action(item);
        }

        /// <summary>
        /// 将IDictionary转换成指定类型的实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns>T.</returns>
        public static T As<T>(this IDictionary<string, object> dictionary)
            where T : class, new()
        {
            var type = typeof(T);
            var ins = new T();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var mapper = ins as IPropertyMapper;
            foreach (var property in properties)
            {
                var propertyName = mapper == null ? property.Name : mapper.MapPropertyName(property.Name);
                if (string.IsNullOrEmpty(propertyName))
                    continue;
                if (dictionary.ContainsKey(propertyName))
                    property.SetValue(ins, Util.ChangeType(dictionary[propertyName], property.PropertyType));
            }
            return ins;
        }

        /// <summary>
        /// 将对象属性转换为字典
        /// </summary>
        /// <param name="proMapper">The object.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public static Dictionary<string, object> ToDictionary(this IPropertyMapper proMapper)
        {
            if (proMapper == null)
                return null;
            var dic = new Dictionary<string, object>();
            var properties = proMapper.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                var propertyName = proMapper.MapPropertyName(property.Name);
                if (string.IsNullOrEmpty(propertyName))
                    continue;
                dic[propertyName] = property.GetValue(proMapper);
            }
            return dic;
        }

        /// <summary>
        /// 转换成大写字母，同时用连接符连接每个单词
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="connectStr"></param>
        /// <returns>System.String.</returns>
        public static string ToUpperWord(this string s, string connectStr = "_")
        {
            var regex = new Regex("[A-Z]+[a-z0-9]*");
            var matches = regex.Matches(s);
            var strBuilder = new StringBuilder();
            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                strBuilder.Append(match.Value.ToUpper());
                if (i != matches.Count - 1 && !string.IsNullOrEmpty(connectStr))
                    strBuilder.Append(connectStr);
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// 是否包含指定的字符串
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="word">The word.</param>
        /// <param name="compareOptions">The compare options.</param>
        /// <returns><c>true</c> if [contains] [the specified word]; otherwise, <c>false</c>.</returns>
        public static bool Contains(this string str, string word, CompareOptions compareOptions)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(str, word, compareOptions) >= 0;
        }

        /// <summary>
        /// 安全执行指定的函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The function.</param>
        /// <returns>Task&lt;ApiResult&lt;T&gt;&gt;.</returns>
        public static async Task<ApiResult<T>> SafeDoAsync<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func();
                return new ApiResult<T>(result);
            }
            catch (Exception e)
            {
                return new ApiResult<T>(e);
            }
        }

        /// <summary>
        /// 安全执行指定的函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The function.</param>
        /// <returns>ApiResult&lt;T&gt;.</returns>
        public static ApiResult<T> SafeDo<T>(Func<T> func)
        {
            try
            {
                var result = func();
                return new ApiResult<T>(result);
            }
            catch (Exception e)
            {
                return new ApiResult<T>(e);
            }
        }

        /// <summary>
        /// 安全执行指定的函数
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Task&lt;ApiResult&gt;.</returns>
        public static async Task<ApiResult> SafeDoAsync(Func<Task> action)
        {
            try
            {
                await action();
                return ApiResult.Ok;
            }
            catch (Exception e)
            {
                return new ApiResult(e);
            }
        }

        /// <summary>
        /// 安全执行指定的函数
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>ApiResult.</returns>
        public static ApiResult SafeDo(Action action)
        {
            try
            {
                action();
                return ApiResult.Ok;
            }
            catch (Exception e)
            {
                return new ApiResult(e);
                throw;
            }
        }
    }
}
