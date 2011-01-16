using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Garfielder.Core.Infrastructure
{
    /// <summary>
    /// Image meta data for ImageResizer
    /// </summary>
    public struct ImgMetaData
    {
        public Size RawSize { get; set; }
        public Size NewSize { get; set; }
        public bool Error { get; set; }
        public string Msg { get; set; }
    }
}
