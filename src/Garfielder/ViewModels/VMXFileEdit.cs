using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Garfielder.Models;
using Garfielder.Web;

namespace Garfielder.ViewModels
{
    public class VMXFileEdit:VMBase
    {

        #region .ctor
        public VMXFileEdit()
        {
            MetaData=new ImageMetaData();//TODO FileMetaData
        }
        #endregion

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }

        /// <summary>
        /// No need flash upload control on the page
        /// </summary>
        public bool NoFlash { get; set; }
        /// <summary>
        /// Name without extension.Readonly
        /// </summary>
        public string Name1
        {
            get
            {
            	return string.IsNullOrWhiteSpace(Name) ? "" : Name.Substring(0, Name.Length - Extension.Length);
            }
        }

		/// <summary>
		/// topic id
		/// </summary>
		public Guid RefId { get; set; }

        #region meta data

        public ImageMetaData MetaData { get; set; }
        /// <summary>
        /// meta json string.
        /// </summary>
        [ScaffoldColumn(false)]
        [ScriptIgnore]
        public string Meta
        {
            get
            {
                if (MetaData == null) return null;
                return MetaData.ToString();
            }
            set
            {
                if(!string.IsNullOrWhiteSpace(value))
                {
                    MetaData = ImageMetaData.EvalJson(value);
                }
            }
        }
        #endregion
        /// <summary>
        /// Get full url of current file.
        /// </summary>
        /// <param name="flag">flag</param>
        /// <returns></returns>
        public string Url(string flag=null)
        {
            //Url.Home()+Model.Name1+"_160x100"+Model.Extension 
            if (string.IsNullOrWhiteSpace(flag))
                return string.Format("{0}{1}", Utils.AbsoluteWebRoot, Name);

            return string.Format("{0}{1}_{2}{3}", Utils.AbsoluteWebRoot, Name1, flag, Extension);
        }

    }
}