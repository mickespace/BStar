using System;
using System.Drawing;
using System.IO;
using WallE.Data.MySql;

namespace WallE.Platform.Support
{
    /// <summary>
    /// 实际文件管理器
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        public static void SaveFile(string filePath, Stream fileStream)
        {
            var fileContent = StreamToBytes(fileStream);
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fs.Seek(0, SeekOrigin.Begin); //设置文件的写入位置
                fs.Write(fileContent, 0, fileContent.Length);
            }
        }

        /// <summary>
        /// Streams to bytes.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>System.Byte[].</returns>
        private static byte[] StreamToBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// 获取缩略图
        /// </summary>
        public static bool GenerateThumbnail(string srcPath, string thumbnailPath)
        {
            try
            {
                var standardLength = 300;
                var image = Image.FromFile(srcPath);
                var srcWidth = image.Width;
                var srcHeight = image.Height;
                var tempHeight = standardLength;
                if (srcWidth >= srcHeight)
                {
                    var scale = (double)srcHeight / srcWidth;
                    tempHeight = (int)(scale * standardLength);
                }
                var tempY = (standardLength - tempHeight) / 2;
                var tempWidth = standardLength;
                if (srcHeight > srcWidth)
                {
                    var scale = (double)srcWidth / srcHeight;
                    tempWidth = (int)(scale * standardLength);
                }
                var tempX = (standardLength - tempWidth) / 2;
                using (var bmp = new Bitmap(standardLength, standardLength))
                {
                    var gr = Graphics.FromImage(bmp);
                    gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    var rectDestination = new Rectangle(tempX, tempY, tempWidth, tempHeight);
                    gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                    bmp.Save(thumbnailPath);
                    bmp.Dispose();
                    image.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("生成缩略图失败", ex);
                return false;
            }
        }
    }
}