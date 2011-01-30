using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garfielder.Models;

namespace Garfielder.ViewModels
{
    public class VMTopicFull:VMTopic
    {
        public List<VMXFileEdit> Files { get; set; }

        public List<Group> Groups { get; set; }
        public List<Tag> Tags { get; set; }

        public int GetPageNum(int itemsPerPage)
        {
            if (Files == null || Files.Count == 0||itemsPerPage<=0) return 0;

            var p = Files.Count/itemsPerPage + (Files.Count%itemsPerPage > 0 ? 1 : 0);
            return p;
        }

        public string UrlGoBack { get; set; }
    }
}