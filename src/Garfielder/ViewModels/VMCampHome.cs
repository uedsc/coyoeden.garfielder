using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garfielder.ViewModels
{
    public class VMCampHome:VMBase
    {
        #region .ctor
        public VMCampHome()
        {
            StatData=new Dictionary<string, int>();
        }
        #endregion
        /// <summary>
        /// today's topic count 
        /// </summary>
        public int CntTopicToday { get; set; }
        /// <summary>
        /// total topic number
        /// </summary>
        public int CntTopic { get; set; }
        /// <summary>
        /// total comment number
        /// </summary>
        public int CntComment { get; set; }

        public int CntGroup { get; set; }

        public int CntTag { get; set; }

        public int Total
        {
            get { return (CntComment + CntGroup + CntTag + CntTopic); }
        }

        public Dictionary<string, int> StatData { get; set; }

        #region UI Helper methods
        /// <summary>
        /// Get the Element width (in percentage) by its value
        /// </summary>
        /// <param name="val"></param>
        /// <param name="valIfZero"></param>
        /// <returns></returns>
        public int Percent(int val,int valIfZero=3)
        {
            if (Total == 0) return valIfZero;
            var r = Convert.ToInt32(Math.Floor((double)val*100 / Total));
            if (r == 0) return valIfZero;
            return r;
        }
        /// <summary>
        /// Sort the stat data
        /// </summary>
        public void Sort()
        {
            
            StatData["CntTopic"]=CntTopic;
            StatData["CntGroup"]=CntGroup;
            StatData["CntTag"]=CntTag;
            StatData["CntComment"]=CntComment;
            StatData["CntTopicToday"]=CntTopicToday;

            StatData=StatData.OrderBy(x => x.Value).ToDictionary<KeyValuePair<string,int>,string,int>(x=>x.Key,y=>y.Value);
        }
        /// <summary>
        /// Get the element's weight by its key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Weight(string key)
        {
            return Array.IndexOf(StatData.Keys.ToArray(), key);
        }
        #endregion 

    }
}