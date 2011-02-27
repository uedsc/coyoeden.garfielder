using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.ViewModels;
using Garfielder.Core.Infrastructure;

namespace Garfielder.Models
{
    public partial class Topic
    {
        /// <summary>
        /// topic icon
        /// </summary>
        public VMXFileEdit Icon { get; set; }
 
        /// <summary>
        /// validate a slug
        /// </summary>
        /// <param name="slug">slug</param>
        /// <param name="db">GarfielderEntities instance</param>
        /// <returns></returns>
        public static Msg ValidateSlug(string slug,GarfielderEntities db=null)
        {
            var r = new Msg();
            if (string.IsNullOrWhiteSpace(slug))
            {
                r.Error = true;
                r.Body = "Slug can't be empty!";
                r.Context["Slug"] = Utils.RandomStr(8);
                return r;
            }
            var isNewConnection = db == null;
            db = db ?? new GarfielderEntities();
            r.Context["Slug"] = slug;
            try
            {
                var topic = db.Topics.SingleOrDefault(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
                if (topic == null) return r;
                r.Error = true;
                r.Body = string.Format("Slug [{0}] has been used by another topic!", slug);
                r.Context["Slug"] = string.Format("{0}{1}", slug, Utils.RandomStr(4));
            }
            catch (Exception ex)
            {

                r.Error = true;
                r.Body = string.Format("Error:{0}", ex.Message);
            }finally
            {
                if (isNewConnection)
                    db.Dispose();
            }
            return r;

        }
        
        public static VMCampTopicShow  GetTopic(Guid id) {
            using (var db = new GarfielderEntities())
            {
                
                var dbm= db.Topics.Single(x => x.Id == id);
                if (dbm == null) return null;
                var vm = new VMCampTopicShow();
                vm.Id = dbm.Id;
                vm.Title = dbm.Title;
                vm.Slug = dbm.Slug;
                vm.Url = dbm.Url;
                vm.Grade = dbm.Grade;
                vm.Description = dbm.Description;
                vm.ContentX = dbm.ContentX;
            	vm.Logo = dbm.Logo;

                vm.Groups = dbm.Groups.ToList();

                vm.Tags = dbm.Tags.ToList();

            	//vm.Attachments = dbm.XFiles.ToList();

                return vm; 

            };
        }
		/// <summary>
		/// get attachment ids
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static List<Guid> GetAttachments(Guid id)
		{
			var retVal = new List<Guid>();
			try
			{
				using (var db=new GarfielderEntities())
				{
					var obj = db.Topics.Single(x => x.Id.Equals(id));
					obj.XFiles.ToList().ForEach(y=>retVal.Add(y.Id));
				}
			}
			catch (Exception ex)
			{
				
				//do nothing			
			
			}
			return retVal;
		}
		/// <summary>
		/// delete by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public static Msg DeleteByID(params Guid[] id)
        {
            var r = new Msg();
            if(id==null||id.Length==0)
            {
                r.Error = true;
                r.Body = "No selected topics to be deleted!";
                return r;
            }
            using (var db=new GarfielderEntities())
            {
                try
                {
                    var items = db.Topics.Where(x => id.Contains(x.Id)).ToList();
                    items.ForEach(obj=> {
                        obj.Tags.Clear();
                        obj.Groups.Clear();
                        db.Topics.DeleteObject(obj);
                    });
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    r.Error = true;
                    r.Body = ex.Message;
                }
            }//using
            return r;
        }
        /// <summary>
        /// topic statistics
        /// </summary>
        /// <returns></returns>
        public static dynamic TopicStat()
        {
            using(var db=new GarfielderEntities())
            {
                var sdate = DateTime.Today;
                var edate = sdate.AddDays(1);
                return new{
                    CntTopic = db.Topics.Count(),
                    CntTopicToday = db.Topics.Count(x => x.CreatedAt>=sdate||x.CreatedAt<edate),
                    CntGroup = Group.ListAll().Count,
                    CntComment = db.TopicComments.Count(),
                    CntTag = Tag.ListAll().Count
                };


            }
            
        }//TopicStat
        /// <summary>
        /// check the specified slug whether exists.if it exists we provide a new one 
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static string AutoSlug(string slug,GarfielderEntities db=null)
        {
            if (string.IsNullOrWhiteSpace(slug)) return Utils.RandomStr(8);
            slug = slug.Trim().CHSToPinyin("-").ToLower().RemoveWhitespace();
            //slug length limit
            slug = slug.Length > 45 ? slug.Substring(0, 45) : slug;
            var needDispose = false;
            try
            {
                if (db == null)
                {
                    db = new GarfielderEntities();
                    needDispose = true;
                }
                    

                var obj = db.Topics.SingleOrDefault(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
                if (null != obj)
                {
                    slug = string.Format("{0}{1}", slug, Utils.RandomStr(5));
                }
            }
            catch (Exception ex)
            {
                //TODO:log the exception
                slug=string.Format("{0}{1}", slug, Utils.RandomStr(5));
            }finally
            {
                if(needDispose)
                    db.Dispose();
            }//try

            return slug;
        }//autoslug
        /// <summary>
        /// list topic files for specified topic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static VMXFileList ListFileData(Guid id)
        {
            var r = new VMXFileList();
            r.FileList=new List<VMXFileEdit>();
            if (id == Guid.Empty) return r;
            using(var db=new GarfielderEntities())
            {
                var obj = db.Topics.SingleOrDefault(x => x.Id.Equals(id));
                if (obj == null) return r;
                obj.XFiles.ToList().ForEach(x => r.FileList.Add(new VMXFileEdit
                                                           {
                                                               Id = x.Id,
                                                               Title = x.Title,
                                                               Extension = x.Extension,
                                                               Description = x.Description,
                                                               UserName = x.User.Name,
                                                               Name = x.Name,
                                                               CreatedAt = x.CreatedAt
                                                           }));
                r.RefTopic = obj;
            }//using
            return r;
        }//ListFileData
        /// <summary>
        /// Detach files
        /// </summary>
        /// <param name="topicID"></param>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public static Msg DetachFiles(Guid topicID,params Guid[] fileID)
        {
            var msg = new Msg();
            try
            {
                using (var db = new GarfielderEntities())
                {
                    var obj = db.Topics.SingleOrDefault(x => x.Id.Equals(topicID));
                    if(obj==null||obj.XFiles.Count==0)
                    {
                        msg.Error = true;
                        msg.Body = string.Format("Topic with id [{0}] not exists or it has no attachments!", topicID);
                        return msg;
                    }//if

                    //delete all attached files
                    if(fileID==null||fileID.Length==0)
                    {
                        obj.XFiles.Clear();
                        db.SaveChanges();
                        return msg;
                    }//if
                    
                    //delete specified files
                    var items = obj.XFiles.Where(x => !fileID.Contains(x.Id));
                    obj.XFiles.Clear();
                    items.ToList().ForEach(x => obj.XFiles.Add(x));
                    db.SaveChanges();
                } //using
            }catch(Exception ex)
            {
                //TODO:log
                msg.Error = true;
                msg.Body = ex.Message;
            }//try
            return msg;
        }

        /// <summary>
        /// star a topic
        /// </summary>
        /// <param name="id"></param>
        /// <param name="who"></param>
        /// <param name="noStar"></param>
        /// <returns></returns>
        public static Msg Star(Guid id,string who,bool noStar=false)
        {
            var msg = new Msg();
            try
            {
                if(TopicElected.Exists(id))
                {
                    return msg;
                }

                msg=TopicElected.Create(id, DateTime.Now, DateTime.Now.AddMonths(1), who);
            }
            catch (Exception ex)
            {

                msg.Error = true;
                msg.Body = ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// list all items by specified conditions
        /// </summary>
        /// <param name="published"></param>
        /// <param name="term"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static List<Topic> ListAll(bool published=false,string term="",Action<List<Topic>> callback=null)
        {
            var retVal = new List<Topic>();

            //get topic data
            using (var db = new GarfielderEntities())
            {
                //TODO:searching optimize
                var q = default(IQueryable<Topic>);
                //filter-whether is published
                q = published ? db.Topics.Where(x => x.Published) : db.Topics;
                //filter-searching term
                if (!string.IsNullOrWhiteSpace(term))
                {
                    q = from obj in q
                        where obj.Title.ToLower().Contains(term)
                        select obj;
                }
                //sort
                retVal = q.OrderByDescending(x => x.CreatedAt).ToList();
                if(null!=callback)
                {
                    callback(retVal);
                }
                
            }//using

            return retVal;
        }
        /// <summary>
        /// list all starred topics
        /// </summary>
        /// <returns></returns>
        public static List<Topic> ListAllStarred(Action<List<Topic>> callback=null)
        {
            var retVal = new List<Topic>();
            try
            {
                //get topic data
                using (var db = new GarfielderEntities())
                {
                    //TODO:searching optimize
                    var q = default(IQueryable<Topic>);
                    //filter-starred
                    q = from obj in db.Topics
                        where obj.Published&&obj.TopicElected!=null
                        select obj;
                    //sort
                    retVal = q.OrderByDescending(x => x.CreatedAt).ToList();
                    //parse icon
                    retVal.ForEach(x =>
                                       {
                                           var tempFile = db.XFiles.SingleOrDefault(y => y.Meta.IndexOf(x.Logo)>=0) ??
                                                          x.XFiles.OrderByDescending(y => y.Title).FirstOrDefault();

											if(null!=tempFile)
											{
												x.Icon = new VMXFileEdit
															{
																Id = tempFile.Id,
																Title = tempFile.Title,
																Extension = tempFile.Extension,
																Description = tempFile.Description,
																UserName = tempFile.User.Name,
																Name = tempFile.Name,
																CreatedAt = tempFile.CreatedAt
															};
											}
                                           
                                       });
                    //callback);
                    if (null != callback)
                    {
                        callback(retVal);
                    }

                }//using
            }
            catch (Exception ex)
            {
                //TODO:log
      
            }

            return retVal;
        }
		/// <summary>
		/// update logo field
		/// </summary>
		/// <param name="img"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Msg UpdateLogo(string img,string id)
		{
			var msg = new Msg();
			if(string.IsNullOrWhiteSpace(img))
			{
				msg.Error = true;
				msg.Body = "No Logo specified!";
				return msg;
			}
			try
			{
				var gid = Guid.Parse(id);
				var topic = default(Topic);
				using(var db=new GarfielderEntities())
				{
					topic = db.Topics.SingleOrDefault(x => x.Id.Equals(gid));
					if(topic!=null)
					{
						topic.Logo = img;
						db.SaveChanges();
					}
				}
			}catch(Exception ex)
			{
				msg.Error = true;
				msg.Body = ex.Message;
			}
			return msg;
		}


        #region frontend methods
        /// <summary>
        /// get full topic data by slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public static VMTopicFull GetTopic(string slug)
        {
            slug = slug.Trim().ToLower();
            var obj = new VMTopicFull();
            obj.Files=new List<VMXFileEdit>();
            obj.Groups=new List<Group>();
            obj.Tags=new List<Tag>();
            try
            {
                using (var db = new GarfielderEntities())
                {

                    var dbm = db.Topics.Single(x => x.Slug == slug);
                    if (dbm == null) return obj;


                    obj.Id = dbm.Id;
                    obj.Title = dbm.Title;
                    obj.Slug = dbm.Slug;
                    obj.Desc = dbm.Description;
                    obj.XContent = dbm.ContentX;
                    obj.DateCreated = dbm.CreatedAt;
                	obj.Logo = dbm.Logo;

                    obj.Groups = dbm.Groups.ToList();

                    obj.Tags = dbm.Tags.ToList();

                    dbm.XFiles.ToList().ForEach(x => obj.Files.Add(new VMXFileEdit
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Extension = x.Extension,
                        Description = x.Description,
                        UserName = x.User.Name,
                        Name = x.Name,
                        CreatedAt = x.CreatedAt
                    }));

                };//using
            }
            catch (Exception ex)
            {
                
                //TODO:log
            }//try
            return obj;

        }
        #endregion
    }
}