using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Swashbuckle.Swagger;
using WallE.Data.MySql;

namespace WallE.Platform.Support
{
    public class CachingSwaggerProvider : ISwaggerProvider
    {
        private static ConcurrentDictionary<string, SwaggerDocument> _cache =new ConcurrentDictionary<string, SwaggerDocument>();

        private readonly ISwaggerProvider _swaggerProvider;
        private static string _xmlFilePath;

        public CachingSwaggerProvider(ISwaggerProvider swaggerProvider,string xmlFilePath)
        {
            _swaggerProvider = swaggerProvider;
            _xmlFilePath = xmlFilePath;
        }

        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
            SwaggerDocument srcDoc = null;
            //只读取一次
            if (!_cache.TryGetValue(cacheKey, out srcDoc))
            {
                srcDoc = _swaggerProvider.GetSwagger(rootUrl, apiVersion);
                srcDoc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", GetControllerDesc() } };
                _cache.TryAdd(cacheKey, srcDoc);
            }
            return srcDoc;
        }

        /// <summary>
        /// 从API文档中读取控制器描述
        /// </summary>
        /// <returns>所有控制器描述</returns>
        public static ConcurrentDictionary<string, string> GetControllerDesc()
        {
            var controllerDescDict = new ConcurrentDictionary<string, string>();
            if (!File.Exists(_xmlFilePath))
            {
                LoggerHelper.Info("未找到Xml文件:" + _xmlFilePath);
                return controllerDescDict;
            }
            var xmldoc = new XmlDocument();
            xmldoc.Load(_xmlFilePath);
            var cCount = "Controller".Length;
            foreach (XmlNode node in xmldoc.SelectNodes("//member"))
            {
                var type = node.Attributes["name"].Value;
                if (type.StartsWith("T:"))
                {
                    //控制器
                    var arrPath = type.Split('.');
                    var length = arrPath.Length;
                    var controllerName = arrPath[length - 1];
                    //var version= arrPath[length - 2];
                    if (controllerName.EndsWith("Controller"))
                    {
                        //获取控制器注释
                        var summaryNode = node.SelectSingleNode("summary");
                        var ctrlName = controllerName.Remove(controllerName.Length - cCount, cCount);
                        //var key = $"{ctrlName} ({version})";
                        var key = ctrlName;
                        if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
                        {
                            controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                        }
                    }
                }
            }
            return controllerDescDict;
        }
    }
}
