using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class UploadifyController : Controller
    {


        public ActionResult Uploadify()
        {
            return View();
        }


        #region 上传图片

        [HttpPost]
        public ActionResult Upload(int h = 300, int w = 300)
        {
            string filePath = "";
            foreach (string item in Request.Files)
            {
                HttpPostedFileBase postFile = Request.Files[item];
                if (postFile.ContentLength == 0)
                    continue;
                string newFilePath = Request.ApplicationPath + "UploadFile/image/" + string.Format("{0:yyyyMMdd}", DateTime.Today);
                if (!Directory.Exists(Server.MapPath(newFilePath)))     //存储路径，如果没有则创建路径
                {
                    Directory.CreateDirectory(Server.MapPath(newFilePath));
                }
                string file = newFilePath + "/" + string.Format("{0:hhmmss}", DateTime.Now) + "_" + System.Guid.NewGuid() + "." + postFile.FileName.Split('.').Last();
                filePath = file;
                Image imageFile = resizeImage(Image.FromStream(postFile.InputStream, true, true), new Size { Height = h, Width = w });
                imageFile.Save(Server.MapPath(file));
            }
            return Content(filePath);      //将文件路径Response
        }

        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
        #endregion

        #region 上传文件
        public ActionResult UpFile()
        {
            foreach (string item in Request.Files)
            {
                HttpPostedFileBase postFile = Request.Files[item];
                if (postFile.ContentLength > 0)
                {
                    var newFilePath = Request.ApplicationPath + "UploadFile/File/" + DateTime.Today.ToString("yyyyMMdd") + "/";

                    if (!Directory.Exists(Server.MapPath(newFilePath)))     //存储路径，如果没有则创建路径
                    {
                        Directory.CreateDirectory(Server.MapPath(newFilePath));
                    }

                    string fileUrl = Server.MapPath("~/")+newFilePath + Path.GetFileName(postFile.FileName);

                    postFile.SaveAs(fileUrl);
                }
            }
            return View();
        }
        #endregion

        #region 下载文件
        public void Download()
        {
            string fileName = "技术备注.txt";//客户端保存的文件名  
            string filePath = Server.MapPath("~/") + "UploadFile/File/20170314/技术备注.txt";

            //以字符流的形式下载文件  
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开  
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        #endregion
    }
}