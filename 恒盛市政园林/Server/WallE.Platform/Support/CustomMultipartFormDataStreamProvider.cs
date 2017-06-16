using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WallE.Platform.Support
{
    public class CustomMultipartFormDataStreamProvider : MultipartStreamProvider
    {
        private List<string> _formData=new List<string>();
        private readonly Collection<bool> _isFormData = new Collection<bool>();
        private readonly Collection<MyMultipartFileData> _fileData = new Collection<MyMultipartFileData>();

        public List<string> FormData
        {
            get { return _formData; }
        }

        public Collection<MyMultipartFileData> FileData
        {
            get { return _fileData; }
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            var contentDisposition = headers.ContentDisposition;
            if (contentDisposition == null)
                throw new InvalidOperationException("Can not find header field..");
            if (!String.IsNullOrEmpty(contentDisposition.FileName))
            {
                _isFormData.Add(false);
                return new MemoryStream();
            }
            _isFormData.Add(true);
            return new MemoryStream();
        }

        /// <summary>
        /// 读取所有信息
        /// </summary>
        /// <returns></returns>
        public override async Task ExecutePostProcessingAsync()
        {
            for (var index = 0; index < Contents.Count; index++)
            {
                var content = Contents[index];
                if (_isFormData[index])
                {
                    var data = await content.ReadAsStringAsync();
                    _formData.Add(data);
                }else
                {
                    var byteData = await content.ReadAsByteArrayAsync();
                    var fileData = new MyMultipartFileData(content.Headers, byteData);
                    FileData.Add(fileData);
                }
            }
        }
    }

    public class MyMultipartFileData
    {
        public MyMultipartFileData(HttpContentHeaders headers, byte[] fileContent)
        {
            Headers = headers;
            FileContent = fileContent;
        }

        public HttpContentHeaders Headers { get; private set; }
        public byte[] FileContent { get; private set; }
    }
}