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