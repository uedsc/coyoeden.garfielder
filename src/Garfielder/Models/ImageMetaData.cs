using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Garfielder.Models
{
    /// <summary>
    /// Image meta data
    /// </summary>
    public class ImageMetaData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<ImageThumbData> Thumbs { get; set; }

        public ImageMetaData()
        {
            Thumbs=new List<ImageThumbData>();
        }

        public ImageMetaData AddThumb(string flag,string src,int w,int h)
        {
            Thumbs.Add(new ImageThumbData(){Flag = flag,Src = src,Width = w,Height = h});
            return this;
        }

        public override string ToString()
        {
            return Json.Encode(this);
        }

        public string ToJson()
        {
            return this.ToString();
        }

        public static ImageMetaData EvalJson(string jsonData)
        {
            var obj = Json.Decode(jsonData, typeof (ImageMetaData));
            return (obj as ImageMetaData);
        }
    }
}