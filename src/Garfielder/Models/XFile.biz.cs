using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;

using Garfielder.ViewModels;
using Garfielder.Core.Infrastructure;

namespace Garfielder.Models
{
    /// <summary>
    /// biz logic for XFile 
    /// </summary>
    public partial class XFile
    {
        public static string[] IMG_EXTS = new string[] { ".jpg", ".gif", ".png", ".jpeg", ".bmp", ".ico" };
        const string FILE_STR = "Assets/Upload/{0}/{1}/{2}";

        #region private area
        private static readonly string _uploadsFolder = HostingEnvironment.MapPath("~/Assets/Upload/");
        /// <summary>
        /// get full file name
        /// </summary>
        /// <param name="uname">user name</param>
        /// <param name="fileName">file name</param>
        /// <returns></returns>
        private static string getFileName(string uname,string fileName) {
            return string.Format(FILE_STR, uname, DateTime.Now.ToString("yyyyMMdd"),fileName);
        }
        #endregion

        /// <summary>
        /// Is img
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static bool IsImg(string ext) {
            return ext.InArray(IMG_EXTS);
        }
        /// <summary>
        /// get image name of specified size
        /// </summary>
        /// <param name="imgName"></param>
        /// <param name="sizeFlag"></param>
        /// <returns></returns>
        public static string GetImgName(string imgName,string sizeFlag) {
            sizeFlag = string.IsNullOrWhiteSpace(sizeFlag) ? "" : string.Format("_{0}",sizeFlag);
            var ext = imgName.Substring(imgName.LastIndexOf("."));
            var name = imgName.Substring(0, imgName.Length - ext.Length);
            return string.Format("{0}{1}{2}",name,sizeFlag,ext);
        }
        /// <summary>
        /// Save current file uploaded
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static VMXFileEdit SaveFile(HttpRequestBase req)
        {
            var vm = new VMXFileEdit();
            vm.NoFlash = true;

            if (req.Files.Count == 0 || req.Files[0].FileName == "" || req.Files[0].ContentLength==0)
            {
                vm.Error = true;
                vm.Msg = "No file selected!Please select a file to upload!";
                return vm;
            };

            var uid = req.Params["UserID"];
            var uname = req.Params["UserName"];
            var fileName=req.Files[0].FileName.CHSToPinyin("-").ToLower();
            var path0 = getFileName(uname, fileName);//虚拟路径
            var path1 = Path.Combine(_uploadsFolder, uname, DateTime.Now.ToString("yyyyMMdd"));//物理路径
            //db op
            using (var db = new GarfielderEntities())
            {
                var dbm = db.XFiles.SingleOrDefault(x => x.Name ==path0);
                if (dbm == null)
                {
                    //save to disk
                    vm.Name = path0;
                    
                    if (!Directory.Exists(path1))
                    {
                        Directory.CreateDirectory(path1);
                    }

                    try
                    {
                        vm.Title = req.Files[0].FileName;
                        vm.Description = vm.Title;
                        vm.Extension = vm.Name.Substring(vm.Name.LastIndexOf("."));
                        vm.Name1 = path0.Substring(0, path0.Length - vm.Extension.Length);
                        vm.Id = Guid.NewGuid();
                        vm.CreatedAt = DateTime.Now;

                        path1=string.Format("{0}\\{1}",path1,fileName);
                        req.Files[0].SaveAs(path1);

                        //图片缩略图处理
                        if (IsImg(vm.Extension)) { 
                            //大图800x600
                            ImageResizer.GetThumbnail(path1, GetImgName(path1, "800x600"), 800, 600, true);
                            //中大图500x500
                            ImageResizer.GetThumbnail(path1,GetImgName(path1,"500x500"),500,500,true);
                            //中
                            ImageResizer.GetThumbnail(path1, GetImgName(path1, "300x300"), 300, 300, true);
                            //中小图
                            ImageResizer.GetThumbnail(path1, GetImgName(path1, "160x160"), 160, 160, true);
                            ImageResizer.GetThumbnail(path1, GetImgName(path1, "160x100"), 160, 100);
                            //小
                            ImageResizer.GetThumbnail(path1, GetImgName(path1, "64x64"), 64, 64);
                        };

                        dbm = new XFile();
                        dbm.Id = vm.Id;
                        dbm.Name = path0;
                        dbm.Title = vm.Title;
                        dbm.Description = vm.Description;
                        dbm.Extension = vm.Extension;
                        dbm.CreatedAt = vm.CreatedAt;

                        dbm.UserID = Guid.Parse(uid);
                        db.CommandTimeout = 0;
                        db.AddToXFiles(dbm);
                        db.SaveChanges();
                        ClearCache();
                    }
                    catch (Exception ex)
                    {
                        vm.Error = true;
                        vm.Msg = ex.Message;
                    }//TRY

                }
                else { 
                    //FILE Exists
                    vm.Error = true;
                    vm.Msg = string.Format("File {0} exists!", path0);
                };

            };//using
            return vm;

        }//SaveFile
        /// <summary>
        /// list all items
        /// </summary>
        /// <returns></returns>
        public static List<XFile> ListAll()
        {
            using (var db = new GarfielderEntities())
            {
                if (_Items == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Items == null)
                        {
                            _Items = db.XFiles.ToList();
                            _Items.ForEach(x=>x.UserName=x.User.Name);
                        }
                    }
                }

                return _Items;
            }//using

        }
        /// <summary>
        /// list all data
        /// </summary>
        /// <returns></returns>
        public static List<VMXFileEdit> ListAllData()
        {
            var items = ListAll().OrderByDescending(x => x.CreatedAt).ToList();
            var r = new List<VMXFileEdit>();
            items.ForEach(x => r.Add(
                new VMXFileEdit
                {
                    Name = x.Name,
                    Name1 = x.Name.Substring(0, x.Name.Length - x.Extension.Length),
                    Title = x.Title,
                    CreatedAt = x.CreatedAt,
                    Extension = x.Extension,
                    Id = x.Id,
                    UserName = x.UserName
                }));
            return r;

        }
        /// <summary>
        /// clear cache
        /// </summary>
        public static void ClearCache()
        {
            _Items = null;
        }

        #region private properties
        private static object _SyncRoot = new object();
        private static List<XFile> _Items;
        #endregion

        #region extra public properties

        public string UserName { get; set; }
        #endregion
    }
}