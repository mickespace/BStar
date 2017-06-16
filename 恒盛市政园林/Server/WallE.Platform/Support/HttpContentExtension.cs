using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WallE.Platform.Support
{
    public static class HttpContentExtension
    {
        public static async Task<List<StreamInfo>> GetFileStreamAsync(this HttpContent content)
        {
            if (!content.IsMimeMultipartContent())
                return null;
            var memoryStreamProvide = await content.ReadAsMultipartAsync();
            var fileInfoList = new List<StreamInfo>();
            foreach (var item in memoryStreamProvide.Contents)
            {
                var stream = await item.ReadAsStreamAsync();
                var headers = item.Headers;
                var fileInfo = new StreamInfo
                {
                    Name = headers.ContentDisposition.Name.Trim('"'),
                    FileName = headers.ContentDisposition.FileName.Trim('"'),
                    Stream = stream
                };
                fileInfoList.Add(fileInfo);
            }
            return fileInfoList;
        }
    }
}