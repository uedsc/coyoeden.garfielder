using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Garfielder.Core.Infrastructure
{
    public class ImageResizer
    {

        /// <summary>
        /// 制作小正方形,如果原图为长方形，则不足处以透明背景色填充
        /// </summary>
        /// <param name="fileName">原图的文件路径</param>
        /// <param name="newFileName">新地址</param>
        /// <param name="newSize">长度或宽度</param>
        public static void GetSquareImage(string fileName, string newFileName, int newSize)
        {
            Image image = Image.FromFile(fileName);

            int width = image.Width;
            int height = image.Height;
            Bitmap b = new Bitmap(newSize, newSize);

            try
            {
                using (var g = Graphics.FromImage(b))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.High;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    //清除整个绘图面并以透明背景色填充
                    g.Clear(Color.Transparent);
                    if (width < height)
                        g.DrawImage(image, new Rectangle(0, 0, newSize, newSize), new Rectangle(0, (height - width) / 2, width, width), GraphicsUnit.Pixel);
                    else
                        g.DrawImage(image, new Rectangle(0, 0, newSize, newSize), new Rectangle((width - height) / 2, 0, height, height), GraphicsUnit.Pixel);

                    saveImage(b, newFileName, GetCodecInfo("image/" + GetFormat(fileName).ToString().ToLower()));
                }
            }
            finally
            {
                image.Dispose();
                b.Dispose();
            }
        }

        /// <summary>
        /// 制作缩略图
        /// </summary>
        /// <param name="fileName">原图路径</param>
        /// <param name="newFileName">新图路径</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public static void GetThumbnail(string fileName, string newFileName, int maxWidth, int maxHeight)
        {
            try
            {
                using(var original = Image.FromFile(fileName)){
                    var _newSize = GetImgSize(original.Width, original.Height, maxWidth, maxHeight);
            
                    var thumbnailBitmap = new Bitmap(_newSize.Width, _newSize.Height);
                    var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                    thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    var imageRectangle = new Rectangle(0, 0, _newSize.Width,_newSize.Height);
                    thumbnailGraph.DrawImage(original, imageRectangle);

                    thumbnailBitmap.Save(newFileName, GetFormat(fileName));

                    thumbnailGraph.Dispose();
                    thumbnailBitmap.Dispose();
                }
            }catch{}
        }
        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="scaleFactor">缩放比例</param>
        /// <param name="fromStream"></param>
        /// <param name="toStream"></param>
        public void ResizeImage(double scaleFactor, Stream fromStream, Stream toStream)
        {
            using (var image = Image.FromStream(fromStream))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);

                ResizeImage(newWidth, newHeight, fromStream, toStream);
            }

        }
        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="fromStream"></param>
        /// <param name="toStream"></param>
        public void ResizeImage(int newWidth,int newHeight, Stream fromStream, Stream toStream)
        {

            var image = Image.FromStream(fromStream);

            var thumbnailBitmap = new Bitmap(newWidth, newHeight);

            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;


            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(image, imageRectangle);

            thumbnailBitmap.Save(toStream, image.RawFormat);

            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();

        }
        /// <summary>
        /// 得到图片格式
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        public static ImageFormat GetFormat(string name)
        {
            string ext = name.Substring(name.LastIndexOf(".") + 1);
            switch (ext.ToLower())
            {
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }
        /// <summary>
        /// 计算新尺寸
        /// </summary>
        /// <param name="width">原始宽度</param>
        /// <param name="height">原始高度</param>
        /// <param name="maxWidth">最大新宽度</param>
        /// <param name="maxHeight">最大新高度</param>
        /// <returns></returns>
        public static Size GetImgSize(int width, int height, int maxWidth, int maxHeight)
        {
            decimal MAX_WIDTH = (decimal)maxWidth;
            decimal MAX_HEIGHT = (decimal)maxHeight;
            decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;
            decimal originalWidth = (decimal)width;
            decimal originalHeight = (decimal)height;

            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                // determine the largest factor 
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
                else
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }
            return new Size(newWidth, newHeight);
        }
        /// <summary>
        /// 获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        public static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType)
                    return ici;
            }
            return null;
        }



        #region helper methods
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image">Image 对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">指定格式的编解码参数</param>
        private static void saveImage(Image image, string savePath, ImageCodecInfo ici)
        {
            //设置 原图片 对象的 EncoderParameters 对象
            using (var parameters = new EncoderParameters(1))
            {
                parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)100));
                image.Save(savePath, ici, parameters);
            }
        }
        #endregion

    }
}
