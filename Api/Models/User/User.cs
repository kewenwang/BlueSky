using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.User
{
    public class User
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 新闻作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 发布新闻时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}