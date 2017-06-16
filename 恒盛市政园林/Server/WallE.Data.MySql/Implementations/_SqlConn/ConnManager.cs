using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace WallE.Data.MySql
{
    /// <summary>
    /// 数据库连接字符串管理器
    /// </summary>
    [Export(typeof(IConnManager))]
    internal class ConnManager:IConnManager
    {
        private readonly Object _thisLock = new object();
        private Dictionary<string, string> connStrDic = new Dictionary<string, string>();

        public ConnManager() { }

        /// <summary>
        /// 表示当前项目的文件夹路径
        /// </summary>
        private string GetConnectionXml()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConnectionCenter.xml");
            return filePath;
        }

        /// <summary>
        /// 根据Key获取连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConnString(string key)
        {
            if (connStrDic.ContainsKey(key))
                return connStrDic[key];
            lock (_thisLock)
            {
                var filePath = GetConnectionXml();
                if (!File.Exists(filePath))
                    CreateConnectionXml(filePath);
                var doc = XDocument.Load(filePath);
                if (doc.Root != null)
                {
                    var ele = doc.Root.Elements("DbConnectionInfo").FirstOrDefault(
                        t => t.Attribute("Key").Value == key);
                    var value = ele == null ? null : ele.Attribute("ConnectionString").Value;
                    if (!string.IsNullOrEmpty(value))
                        connStrDic[key] = value;
                    return value;
                }
                return null;
            }
        }

        public void AddConnString(string key, string connectionString)
        {
            lock (_thisLock)
            {
                var filePath = GetConnectionXml();
                if (!File.Exists(filePath))
                    CreateConnectionXml(filePath);
                var doc = XDocument.Load(filePath);
                var element = new XElement("DbConnectionInfo",
                    new XAttribute("Key", key),
                    new XAttribute("ConnectionString", connectionString)
                );
                if (doc.Root != null) doc.Root.Add(element);
                doc.Save(filePath);
            }
        }

        public void RemoveConnString(string key)
        {
            connStrDic.Remove(key);
            lock (_thisLock)
            {
                var filePath = GetConnectionXml();
                if (!File.Exists(filePath))
                    CreateConnectionXml(filePath);
                var doc = XDocument.Load(filePath);
                if (doc.Root != null)
                {
                    var ele = doc.Root.Elements("DbConnectionInfo").FirstOrDefault(
                        t => t.Attribute("Key").Value == key);
                    if (ele == null)
                        return;
                    ele.Remove();
                }
                doc.Save(filePath);
            }
        }

        public string GetMasterConnString()
        {
            var connStr = GetConnString(Guid.Empty + "");
            var connStrBuilder = new MySqlConnectionStringBuilder(connStr) { Database = "information_schema" ,PersistSecurityInfo = true};
            return connStrBuilder.ToString();
        }

        public string GetManagementDbName()
        {
            var connStr = GetConnString(Guid.Empty + "");
            var connStrBuilder = new MySqlConnectionStringBuilder(connStr);
            return connStrBuilder.Database;
        }

        public string GetDbName(string key)
        {
            var connStr = GetConnString(key);
            if (string.IsNullOrEmpty(connStr))
                return null;
            var connStrBuilder = new MySqlConnectionStringBuilder(connStr);
            return connStrBuilder.Database;
        }

        /// <summary>
        /// 获取所有连接字符串
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetConnectionInfo()
        {
            lock (_thisLock)
            {
                var connectionInfoDic = new Dictionary<string, string>();
                var filePath = GetConnectionXml();
                if (!File.Exists(filePath))
                    CreateConnectionXml(filePath);
                var doc = XDocument.Load(filePath);
                if (doc.Root != null)
                {
                    var eles = doc.Root.Elements("DbConnectionInfo");
                    foreach (var xElement in eles)
                    {
                        if (xElement.Attribute("ConnectionString") == null)
                            continue;
                        string projectItemKey;
                        if (xElement.Attribute("Key") == null)
                            projectItemKey = Guid.Empty + "";
                        else
                            projectItemKey = xElement.Attribute("Key").Value;
                        var connStr = xElement.Attribute("ConnectionString").Value;
                        connectionInfoDic[projectItemKey] = connStr;
                    }
                }
                return connectionInfoDic;
            }
        }

        private void CreateConnectionXml(string filePath)
        {
            var newDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Root"));
            //添加默认连接字符串
            const string connectionString = "Data Source=hdm140015221.my3w.com;Database=hdm140015221_db;User ID=hdm140015221;Password=E403admin";
            var element = new XElement("DbConnectionInfo",
                    new XAttribute("Key", Guid.Empty),
                    new XAttribute("ConnectionString", connectionString)
                );
            if (newDoc.Root != null) newDoc.Root.Add(element);
            newDoc.Save(filePath);
        }
    }
}
