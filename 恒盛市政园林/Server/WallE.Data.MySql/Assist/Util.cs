// ***********************************************************************
// Assembly         : WallE.Assist
// Author           : xxchen
// Created          : 11-26-2015
//
// Last Modified By : xxchen
// Last Modified On : 11-26-2015
// ***********************************************************************
// <copyright file="Util.cs" company="深圳筑星科技有限公司">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace WallE.Data.MySql
{
    public static class Util
    {
        /// <summary>
		/// 根据输入的str获得其转化为Bytes后的长度
		/// 可用于获得界面上显示的中文的实际长度
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int GetBytesLength(string str)
        {
            return Encoding.GetEncoding("UTF-8").GetBytes(str).Length;
        }

        /// <summary>
        /// 返回带有省略号的字符串
        /// </summary>
        public static string GetEllipsisString(string str, int strLength)
        {
            if (String.IsNullOrEmpty(str) || str.Length <= strLength)
                return str;
            return str.Substring(0, strLength) + "...";
        }

        /// <summary>
        /// 应用程序全路径
        /// </summary>
        /// <value>The application file path.</value>
        public static string AppFilePath
        {
            get { return Assembly.GetEntryAssembly().Location; }
        }

        public static object ChangeType(object value, Type newType)
        {
            if (value == null || value is DBNull)
                return null;
            if (value.GetType() == newType)
                return value;
            try
            {
                if (newType.IsEnum)
                    return Enum.ToObject(newType, value);
                if (newType.IsValueType)
                    return newType.Name == "Nullable`1" ? value : Convert.ChangeType(value, newType);
                return value;
            }
            catch (Exception e)
            {
                Debug.WriteLine("类型转换失败:" + e.Message);
                return value;
            }
        }

        #region 字符串的加密与解密
        private static readonly byte[] EncryptKey = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// MD5 加密字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后字符串</returns>
        public static string Md5Encrypt(string source)
        {
            // 创建MD5类的默认实例：MD5CryptoServiceProvider
            var md5 = MD5.Create();
            var bs = Encoding.UTF8.GetBytes(source);
            var hs = md5.ComputeHash(bs);
            var sb = new StringBuilder();
            foreach (var b in hs)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        /// <summary>
        /// MD5 16位加密
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.String.</returns>
        public static string Md5Encrypt16(string source)
        {
            var md5 = MD5.Create();
            var bs = Encoding.UTF8.GetBytes(source);
            var hs = md5.ComputeHash(bs);
            var sb = new StringBuilder();
            for (var i = 4; i < 12; i++)
                sb.Append(hs[i].ToString("x2"));
            return sb.ToString();
        }

        /// <summary>
        /// MD5盐值加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="salt">盐值</param>
        /// <returns>加密后字符串</returns>
        public static string Md5Encrypt(string source, object salt)
        {
            return salt == null ? source : Md5Encrypt(source + "{" + salt + "}");
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string DesEncrypt(string encryptString, string encryptKey = null)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptString))
                    return encryptString;
                return Convert.ToBase64String(DesEncrypt(Encoding.UTF8.GetBytes(encryptString), encryptKey));
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DesDecrypt(string decryptString, string decryptKey = null)
        {
            try
            {
                if (string.IsNullOrEmpty(decryptString))
                    return decryptString;
                return Encoding.UTF8.GetString(DesDecrypt(Convert.FromBase64String(decryptString), decryptKey));
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptBytes">The encrypt bytes.</param>
        /// <param name="encryptKey">The encrypt key.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] DesEncrypt(byte[] encryptBytes, string encryptKey = null)
        {
            try
            {
                if (encryptBytes == null || encryptBytes.Length == 0)
                    return null;
                var rgbKey = string.IsNullOrEmpty(encryptKey)
                    ? EncryptKey
                    : Encoding.UTF8.GetBytes(encryptKey.Substring(0, Math.Min(encryptKey.Length, 8)).PadRight(8, '*'));
                var rgbIv = EncryptKey;
                var dCsp = new DESCryptoServiceProvider();
                using (var mStream = new MemoryStream())
                {
                    using (var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIv),
                            CryptoStreamMode.Write))
                    {
                        cStream.Write(encryptBytes, 0, encryptBytes.Length);
                        cStream.FlushFinalBlock();
                        return mStream.ToArray();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptBytes">The decrypt bytes.</param>
        /// <param name="decryptKey">The decrypt key.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] DesDecrypt(byte[] decryptBytes, string decryptKey = null)
        {
            try
            {
                if (decryptBytes == null || decryptBytes.Length == 0)
                    return null;
                var rgbKey = string.IsNullOrEmpty(decryptKey)
                    ? EncryptKey
                    : Encoding.UTF8.GetBytes(decryptKey.Substring(0, Math.Min(decryptKey.Length, 8)).PadRight(8, '*'));
                var rgbIv = EncryptKey;
                var dcsp = new DESCryptoServiceProvider();
                using (var mStream = new MemoryStream())
                {
                    using (var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv),
                            CryptoStreamMode.Write))
                    {
                        cStream.Write(decryptBytes, 0, decryptBytes.Length);
                        cStream.FlushFinalBlock();
                        return mStream.ToArray();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        public static string GetPinYing(string str)
        {
            return PinYinConverter.Get(str);
        }

        public static string GetPinYingFirst(string str)
        {
            return PinYinConverter.GetFirst(str);
        }

        /// <summary>
        /// 将int类型转换为color类型
        /// </summary>
        /// <param name="rgba">The rgba.</param>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="a">a.</param>
        public static void ToColor(int rgba, out byte r, out byte g, out byte b, out byte a)
        {
            r = (byte)(rgba >> 24);
            g = (byte)((rgba >> 16) & 255);
            b = (byte)((rgba >> 8) & 255);
            a = (byte)(rgba & 255);
        }

        /// <summary>
        /// 该方法是否是主程序调用的
        /// </summary>
        /// <returns><c>true</c> if [is main program invoked]; otherwise, <c>false</c>.</returns>
        internal static bool IsMainProgramInvoked()
        {
            var st = new StackTrace();
            foreach (var frame in st.GetFrames())
            {
                var loc = frame.GetMethod().Module.Assembly.Location;
                if (loc.EndsWith(".exe"))
                    return true;
            }
            return false;
        }
    }
}