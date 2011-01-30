using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.Models
{
    public partial class TopicElected
    {
        #region private properties
        private static object _SyncRoot = new object();
        private static List<TopicElected> _Items;

        #endregion

        public static List<TopicElected> ListAll()
        {
            using (var db = new GarfielderEntities())
            {
                if (_Items == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Items == null)
                        {
                            _Items = db.TopicElecteds.ToList();
                        }
                    }
                }

                return _Items;
            }//using

        }

        public static bool Exists(Guid id)
        {
            var items = ListAll();
            return items.Exists(x => x.Id.Equals(id));
        }

        public static void ClearCache()
        {
            _Items = null;
        }
        /// <summary>
        /// delete a record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Msg Delete(Guid id)
        {
            var msg = new Msg();
            try
            {
                var goOn = TopicElected.Exists(id);
                if(!goOn)
                {
                    msg.Error = true;
                    msg.Body = string.Format("Topic {0} doesn't exist!", id);
                }else
                {
                    var dbm = ListAll().Single(x => x.Id.Equals(id));
                    using (var db=new GarfielderEntities())
                    {
                        db.Attach(dbm);
                        db.DeleteObject(dbm);
                        db.SaveChanges();
                        ClearCache();
                    }//using
                }
            }
            catch (Exception ex)
            {
                msg.Error = true;
                msg.Body = ex.Message;
                
            }
            return msg;
        }
        /// <summary>
        /// create a record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="who"></param>
        /// <returns></returns>
        public static Msg Create(Guid id,DateTime begin,DateTime end,string who)
        {
            var msg = new Msg();
            try
            {
                using( var db=new GarfielderEntities())
                {
                    var dbm = new TopicElected()
                                  {
                                      CreatedAt = DateTime.Now,
                                      CreatedBy = who,
                                      DateBegin = begin,
                                      DateEnd = end,
                                      Id = id
                                  };
                    db.AddToTopicElecteds(dbm);
                    db.SaveChanges();
                    ClearCache();
                }
            }
            catch (Exception ex)
            {

                msg.Error = true;
                msg.Body = ex.Message;
            }
            return msg;
        }
    }
}