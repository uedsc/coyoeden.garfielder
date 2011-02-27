using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Helpers;
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
            var refid = req.Params["RefId"];
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
                        
                        vm.Id = Guid.NewGuid();
                        vm.CreatedAt = DateTime.Now;

                        path1=string.Format("{0}\\{1}",path1,fileName);
                        req.Files[0].SaveAs(path1);
                       
                        //图片缩略图处理);
                        if (IsImg(vm.Extension))
                        {
                            //大图800x600
                            var tempObj=ImageResizer.GetThumbnail(path1, GetImgName(path1, ImageFlags.L800X600), 800, 600, true);
                            vm.MetaData.Width = tempObj.RawSize.Width;
                            vm.MetaData.Height = tempObj.RawSize.Height;

                            //原图
                            vm.MetaData.AddThumb(ImageFlags.RAW,
                                string.Format("{0}{1}",
                                   Garfielder.Web.Utils.AbsoluteWebRoot, vm.Name),
                                   vm.MetaData.Width, vm.MetaData.Height);
                            
                            if(!tempObj.Error)
                            {
                                vm.MetaData.AddThumb(ImageFlags.L800X600,
                                                     string.Format("{0}{1}_800x600{2}",
                                                                   Garfielder.Web.Utils.AbsoluteWebRoot, vm.Name1,
                                                                   vm.Extension), tempObj.NewSize.Width, tempObj.NewSize.Height);
                            }
                            //中大图500x500
                            tempObj=ImageResizer.GetThumbnail(path1,GetImgName(path1,ImageFlags.L500X500),500,500,true);
                            if (!tempObj.Error)
                            {
                                vm.MetaData.AddThumb(ImageFlags.L500X500,
                                                     string.Format("{0}{1}_500x500{2}",
                                                                   Garfielder.Web.Utils.AbsoluteWebRoot, vm.Name1,
                                                                   vm.Extension), tempObj.NewSize.Width, tempObj.NewSize.Height);
                            }
                            //中
                            tempObj=ImageResizer.GetThumbnail(path1, GetImgName(path1,ImageFlags.M300X300), 300, 300, true);
                            if (!tempObj.Error)
                            {
                                vm.MetaData.AddThumb(ImageFlags.M300X300,
                                                     string.Format("{0}{1}_300x300{2}",
                                                                   Garfielder.Web.Utils.AbsoluteWebRoot, vm.Name1,
                                                                   vm.Extension), tempObj.NewSize.Width, tempObj.NewSize.Height);
                            }
                            //中小图
                            tempObj=ImageResizer.GetThumbnail(path1, GetImgName(path1,ImageFlags.M160X160), 160, 160, true);
                            if (!tempObj.Error)
                            {
                                vm.MetaData.AddThumb(ImageFlags.M160X160,
                                                     string.Format("{0}{1}_160x160{2}",
                                                                   Garfielder.Web.Utils.AbsoluteWebRoot, vm.Name1,
                                                                   vm.Extension), tempObj.NewSize.Width, tempObj.NewSize.Height);
                            }
                            tempObj=ImageResizer.GetThumbnail(path1, GetImgName(path1, ImageFlags.S160X100), 160, 100);
                            if (!tempObj.Error)
                            {
                                vm.MetaData.AddThumb(ImageFlags.S160X100,
                                                     string.Format("{0}{1}_160x100{2}",
                                                                   Garfielder.Web.Utils.AbsoluteWebRoot, vm.Name1,
                                                                   vm.Extension), tempObj.NewSize.Width, tempObj.NewSize.Height);
                            }
                            //小
                            tempObj=ImageResizer.GetThumbnail(path1, GetImgName(path1, ImageFlags.S64X64), 64, 64);
                            if (!tempObj.Error)
                            {
                                vm.MetaData.AddThumb(ImageFlags.S64X64,
                                                     string.Format("{0}{1}_64x64{2}",
                                                                   Garfielder.Web.Utils.AbsoluteWebRoot, vm.Name1,
                                                                   vm.Extension), tempObj.NewSize.Width, tempObj.NewSize.Height);
                            }
                           
                            
                        };//IsImg

                        dbm = new XFile();
                        dbm.Id = vm.Id;
                        dbm.Name = path0;
                        dbm.Url = path0;
                        dbm.Title = vm.Title;
                        dbm.Description = vm.Description;
                        dbm.Extension = vm.Extension;
                        dbm.CreatedAt=dbm.ModifiedAt = vm.CreatedAt;
                        dbm.Meta = vm.Meta;

                        dbm.UserID = Guid.Parse(uid);
                        db.CommandTimeout = 0;
                        db.AddToXFiles(dbm);
                        db.SaveChanges();
                        ClearCache();
                        //attach to topic
                        AttachToTopic(refid,db,dbm);
                    }
                    catch (Exception ex)
                    {
                        //TODO:log
                        vm.Error = true;
                        vm.Msg = ex.Message;
                    }//TRY

                }
                else { 
                    //FILE Exists
                    vm.Error = true;
                    vm.Msg = string.Format("File {0} exists!", path0);
                    //attach to topic
                    AttachToTopic(refid,db,dbm);
                };


                

            };//using
            return vm;

        }//SaveFile
        private static void AttachToTopic(string topicID,GarfielderEntities db,XFile file)
        {
            //refID proccess
            var objID = Guid.Parse(topicID);
            if(objID==Guid.Empty) return;
            var obj = file.Topics.SingleOrDefault(x => x.Id == objID);
            //exists
            if (obj != null) return;
            //attach
            file.Topics.Add(db.Topics.Single(x=>x.Id.Equals(objID)));
            db.SaveChanges();
        }
        /// <summary>
        /// attach a file to a topic
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="topicID"></param>
        /// <returns></returns>
        public static Msg AttachToTopic(string fileID,string topicID)
        {
            var msg = new Msg();
            try
            {
                var dbm = default(XFile);
                var id = Guid.Parse(fileID);
                using(var db=new GarfielderEntities())
                {
                    dbm = db.XFiles.SingleOrDefault(x => x.Id.Equals(id));
                    if(dbm==null)
                    {
                        msg.Error = true;
                        msg.Body = string.Format("File with ID {0} has been deleted!", fileID);
                    }else
                    {
                        AttachToTopic(topicID, db, dbm);
                    }
                }//using
            }
            catch (Exception ex)
            {

                msg.Error = true;
                msg.Body = ex.Message;
                //TODO:log
            }
            return msg;
        }
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
                    Title = x.Title,
                    CreatedAt = x.CreatedAt,
                    Extension = x.Extension,
                    Id = x.Id,
                    UserName = x.UserName,
                    Meta = x.Meta
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

		/// <summary>
		/// delete by specified id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Msg DeleteByID(params Guid[] id)
		{
			var r = new Msg();
			if (id == null || id.Length == 0)
			{
				r.Error = true;
				r.Body = "No selected items to be deleted!";
				return r;
			}
			using (var db = new GarfielderEntities())
			{
				try
				{
					var items = db.XFiles.Where(x => id.Contains(x.Id)).ToList();
					items.ForEach(obj =>
					{
						obj.Topics.Clear();
						db.XFiles.DeleteObject(obj);
					});
					db.SaveChanges();
					ClearCache();
				}
				catch (Exception ex)
				{
					r.Error = true;
					r.Body = ex.Message;
				}
			}//using
			return r;
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