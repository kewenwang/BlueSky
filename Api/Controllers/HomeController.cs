using Api.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.App_Common;

namespace Api.Controllers
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// GET获取全部新闻
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAllNews()
        {
            var news = new NewsRepository().GetAllNews();
            return ApiHelper.CreateBackMessage(news);
        }

        /// <summary>
        /// GET获取指定ID新闻
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewsByID(int id,int a,int b)
        {
            var news = new NewsRepository().GetAllNews().Where((p) => p.Id == id);
            return ApiHelper.CreateBackMessage(news);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostNews(User model)
        {   
            return ApiHelper.CreateBackMessage();
        }
    }


    public class NewsRepository
    {
        public IEnumerable<User> GetAllNews()
        {
            User[] news = new User[]
            {
                new User { Id = 1, Title="新闻标题1", Content="新闻内容1", Author="xishuai", CreateTime=DateTime.Now },
                new User { Id = 2, Title="新闻标题2", Content="新闻内容2", Author="xishuai", CreateTime=DateTime.Now },
                new User { Id = 3, Title="新闻标题2", Content="新闻内容3", Author="xishuai", CreateTime=DateTime.Now }
            };
            return news;
        }
    }
}
