using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web.Configuration;
namespace NetFramework
{
    public class Util_WEB
    {
        public static string CurrentUrl = "";
        public static string OpenUrl(string TargetURL, string RefURL, string Body, string Method, System.Net.CookieCollection BrowCookie, bool AllowRedirect = true, string ContentType = "application/json;charset=UTF-8")
        {
            return OpenUrl(TargetURL, RefURL, Body, Method, BrowCookie, Encoding.ASCII, AllowRedirect, ContentType);
        }
        public static string OpenUrl(string TargetURL, string RefURL, string Body, string Method, System.Net.CookieCollection BrowCookie, Encoding ContactEncoding, bool AllowRedirect = true, string ContentType = "application/json;charset=UTF-8")
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 5000;
            System.Net.ServicePointManager.SetTcpKeepAlive(true, 3000, 3000);
            //HttpWebRequest LoginPage = null;
            //    GetHttpWebResponseNoRedirect(TargetURL,"","",out LoginPage);

            WebRequest LoginPage = HttpWebRequest.Create(TargetURL);
            ((HttpWebRequest)LoginPage).AllowAutoRedirect = AllowRedirect;
            ((HttpWebRequest)LoginPage).KeepAlive = true;
            ((HttpWebRequest)LoginPage).Timeout = 60000;
            LoginPage.Method = Method;
            switch (Method)
            {
                case "GET":
                    ((HttpWebRequest)LoginPage).Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-powerpoint, application/msword, application/vnd.ms-excel,application/json, text/plain, */*";
                    ((HttpWebRequest)LoginPage).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36 OPR/52.0.2871.40";
                    LoginPage.Headers.Add("Accept-Encoding", "gzip, deflate");
                    ((HttpWebRequest)LoginPage).CookieContainer = new CookieContainer();
                    ((HttpWebRequest)LoginPage).CookieContainer.Add(BrowCookie);
                    ((HttpWebRequest)LoginPage).ServicePoint.Expect100Continue = false;
                    ((HttpWebRequest)LoginPage).Connection = "KeepAlive";
                    ((HttpWebRequest)LoginPage).Referer = RefURL;
                    break;
                case "POST":
                    ((HttpWebRequest)LoginPage).Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-powerpoint, application/msword, application/vnd.ms-excel,application/json, text/plain, */*";
                    ((HttpWebRequest)LoginPage).Referer = RefURL;
                    ((HttpWebRequest)LoginPage).UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
                    LoginPage.Headers.Add("Accept-Encoding", "gzip, deflate");
                    ((HttpWebRequest)LoginPage).CookieContainer = new CookieContainer();
                    ((HttpWebRequest)LoginPage).CookieContainer.Add(BrowCookie);
                    ((HttpWebRequest)LoginPage).ContentType = ContentType;
                    ((HttpWebRequest)LoginPage).ServicePoint.Expect100Continue = false;
                    ((HttpWebRequest)LoginPage).Connection = "KeepAlive";
                    LoginPage.Headers.Add("Origin", ((HttpWebRequest)LoginPage).Referer.Substring(0, ((HttpWebRequest)LoginPage).Referer.Length - 1));
                    if (Body != "")
                    {
                        Stream bodys = LoginPage.GetRequestStream();

                        byte[] text = ContactEncoding.GetBytes(Body);

                        bodys.Write(text, 0, text.Length);

                        bodys.Flush();
                        bodys.Close();
                    }
                    break;
                default:
                    break;
            }
            LoginPage.Timeout = 60 * 1000;
            System.GC.Collect();
            HttpWebResponse LoginPage_Return = null;
            try
            {
                CurrentUrl = "正在下载" + TargetURL;
                LoginPage_Return = (HttpWebResponse)LoginPage.GetResponse();

                CurrentUrl = "已下载" + TargetURL;

                if (((HttpWebResponse)LoginPage_Return).Headers["Set-Cookie"] != null)
                {
                    string Start = LoginPage.RequestUri.Host.Substring(0, LoginPage.RequestUri.Host.LastIndexOf("."));
                    string Host = LoginPage.RequestUri.Host.Substring(LoginPage.RequestUri.Host.LastIndexOf("."));

                    Host = Start.Substring(Start.LastIndexOf(".")) + Host;
                    AddCookieWithCookieHead(BrowCookie, ((HttpWebResponse)LoginPage_Return).Headers["Set-Cookie"], Host);
                }

            }
            catch (Exception AnyError)
            {

                throw AnyError;
            }

            string responseBody = string.Empty;
            if (LoginPage_Return.ContentEncoding.ToLower().Contains("gzip"))
            {
                using (GZipStream stream = new GZipStream(LoginPage_Return.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            else if (LoginPage_Return.ContentEncoding.ToLower().Contains("deflate"))
            {
                using (DeflateStream stream = new DeflateStream(LoginPage_Return.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                using (Stream stream = LoginPage_Return.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            LoginPage.Abort();

            return responseBody;
        }


        public static string UploadWXImage(string ImgFilePath, string MyUserID, string TOUserID, string JavaTimeSpan, CookieCollection tmpcookie, Newtonsoft.Json.Linq.JObject RequestBase)
        {
            #region 上传文件
            FileInfo fi = new FileInfo(ImgFilePath);
            //POST /cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json HTTP/1.1
            //Content-Type: multipart/form-data; boundary=----WebKitFormBoundarywWMqeF7OGA3s1GXQ

            //Host: file.wx2.qq.com





            string UploadUrl = "https://file.wx2.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json";
            System.Net.ServicePointManager.DefaultConnectionLimit = 5000;
            System.Net.ServicePointManager.SetTcpKeepAlive(true, 3000, 3000);

            string optionurl = "https://file.wx2.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json";
            WebRequest OptionPage = HttpWebRequest.Create(optionurl);
            ((HttpWebRequest)OptionPage).Method = "OPTIONS";
            ((HttpWebRequest)OptionPage).Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-powerpoint, application/msword, application/vnd.ms-excel,application/json, text/plain, */*";
            ((HttpWebRequest)OptionPage).Referer = "https://wx2.qq.com/";
            ((HttpWebRequest)OptionPage).UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
            OptionPage.Headers.Add("Accept-Encoding", "gzip, deflate");
            ((HttpWebRequest)OptionPage).CookieContainer = new CookieContainer();
            ((HttpWebRequest)OptionPage).CookieContainer.Add(tmpcookie);
            ((HttpWebRequest)OptionPage).ServicePoint.Expect100Continue = false;
            ((HttpWebRequest)OptionPage).Connection = "KeepAlive";
            OptionPage.Headers.Add("Origin", ((HttpWebRequest)OptionPage).Referer.Substring(0, ((HttpWebRequest)OptionPage).Referer.Length - 1));

            StreamReader OptionReader = new StreamReader(OptionPage.GetResponse().GetResponseStream());
            string OptionResult = OptionReader.ReadToEnd();


            WebRequest LoginPage = HttpWebRequest.Create(UploadUrl);
            ((HttpWebRequest)LoginPage).AllowAutoRedirect = false;
            ((HttpWebRequest)LoginPage).KeepAlive = true;
            ((HttpWebRequest)LoginPage).Timeout = 60000;
            ((HttpWebRequest)LoginPage).Method = "POST";
            ((HttpWebRequest)LoginPage).Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-powerpoint, application/msword, application/vnd.ms-excel,application/json, text/plain, */*";
            ((HttpWebRequest)LoginPage).Referer = "https://wx2.qq.com/";
            ((HttpWebRequest)LoginPage).UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
            LoginPage.Headers.Add("Accept-Encoding", "gzip, deflate");
            ((HttpWebRequest)LoginPage).CookieContainer = new CookieContainer();
            ((HttpWebRequest)LoginPage).CookieContainer.Add(tmpcookie);
            ((HttpWebRequest)LoginPage).ContentType = "multipart/form-data; boundary=----WebKitFormBoundarywWMqeF7OGA3s1GXQ";
            ((HttpWebRequest)LoginPage).ServicePoint.Expect100Continue = false;
            ((HttpWebRequest)LoginPage).Connection = "KeepAlive";
            LoginPage.Headers.Add("Origin", ((HttpWebRequest)LoginPage).Referer.Substring(0, ((HttpWebRequest)LoginPage).Referer.Length - 1));
            Stream Strem_ToPost = LoginPage.GetRequestStream();

            //数据
            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="id"
            //WU_FILE_2
            byte[] buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"id\"" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("WU_FILE_0" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);


            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="name"
            //Data.jpg
            buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"name\"" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes(fi.Name + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="type"
            //image/jpeg
            buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"type\"" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("image/jpeg" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="lastModifiedDate"
            //Mon Apr 09 2018 17:40:22 GMT+0800 (中国标准时间)
            buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"lastModifiedDate\"" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);

            buf = Encoding.UTF8.GetBytes(fi.LastWriteTime.ToString("r") + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="size"
            //79253
            buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"size\"" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes(fi.Length.ToString() + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);


            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="mediatype"
            //pic
            buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"mediatype\"" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("pic" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="uploadmediarequest"
            //{"UploadType":2,"BaseRequest":{"Uin":2402981522,"Sid":"W8Ia83fMnlcuKK0U","Skey":"@crypt_bbd454c7_9465a672aa848c64c765ea727877bdd1","DeviceID":"e718028710913369"},"ClientMediaId":1523267109886,"TotalLen":79253,"StartPos":0,"DataLen":79253,"MediaType":4,"FromUserName":"@ac0308d92ae0d88beb8d90feee45a86c02f36bd5f3560398b544abeac4e70a14","ToUserName":"@@f2a3e52ae022d3303864e9dfcda631635a429b2c28c4d93388ec25429280df00","FileMd5":"a5a03dda3342443cfd15c2ccc8f970e5"}
            buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"uploadmediarequest\"" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            Newtonsoft.Json.Linq.JObject J_ToPost = new Newtonsoft.Json.Linq.JObject();
            J_ToPost.Add("UploadType", 2);
            J_ToPost.Add("BaseRequest", RequestBase["BaseRequest"]);

            J_ToPost.Add("ClientMediaId", JavaTimeSpan);
            J_ToPost.Add("TotalLen", fi.Length);
            J_ToPost.Add("StartPos", 0);
            J_ToPost.Add("DataLen", fi.Length);
            J_ToPost.Add("MediaType", 4);
            J_ToPost.Add("FromUserName", MyUserID);
            J_ToPost.Add("ToUserName", TOUserID);
            J_ToPost.Add("FileMd5", Util_MD5.GetMD5HashFromFile(ImgFilePath));



            buf = Encoding.UTF8.GetBytes(J_ToPost.ToString().Replace(Environment.NewLine, "") + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="webwx_data_ticket"
            //gScs3xfj201uhj/fk3wxSMQA
            buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"webwx_data_ticket\"" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes(tmpcookie["webwx_data_ticket"].Value + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);

            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="pass_ticket"
            //undefined
            buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"pass_ticket\"" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes(
                (tmpcookie["pass_ticket"] == null ? "undefined" : tmpcookie["pass_ticket"].Value) + Environment.NewLine
                );
            Strem_ToPost.Write(buf, 0, buf.Length);
            //------WebKitFormBoundarywWMqeF7OGA3s1GXQ
            //Content-Disposition: form-data; name="filename"; filename="Data.jpg"
            //Content-Type: image/jpeg
            buf = Encoding.UTF8.GetBytes("------WebKitFormBoundarywWMqeF7OGA3s1GXQ" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"filename\" filename=\"" + fi.Name + "\"" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);
            buf = Encoding.UTF8.GetBytes("Content-Type: image/jpeg" + Environment.NewLine + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);

            FileStream fs = fi.OpenRead();
            int Read = fs.ReadByte();
            while (Read != -1)
            {
                Strem_ToPost.WriteByte((byte)Read);
                Read = fs.ReadByte();
            }
            buf = Encoding.UTF8.GetBytes(Environment.NewLine + "------WebKitFormBoundarywWMqeF7OGA3s1GXQ");
            Strem_ToPost.Write(buf, 0, buf.Length);

            buf = Encoding.UTF8.GetBytes("--" + Environment.NewLine);
            Strem_ToPost.Write(buf, 0, buf.Length);


            Strem_ToPost.Flush();
            Strem_ToPost.Close();

            LoginPage.Timeout = 60 * 1000;
            System.GC.Collect();
            HttpWebResponse LoginPage_Return = null;
            try
            {
                NetFramework.Util_WEB.CurrentUrl = "正在下载" + UploadUrl;
                LoginPage_Return = (HttpWebResponse)LoginPage.GetResponse();
                NetFramework.Util_WEB.CurrentUrl = "已下载" + UploadUrl;

                if (((HttpWebResponse)LoginPage_Return).Headers["Set-Cookie"] != null)
                {
                    string Start = LoginPage.RequestUri.Host.Substring(0, LoginPage.RequestUri.Host.LastIndexOf("."));
                    string Host = LoginPage.RequestUri.Host.Substring(LoginPage.RequestUri.Host.LastIndexOf("."));

                    Host = Start.Substring(Start.LastIndexOf(".")) + Host;
                    AddCookieWithCookieHead(tmpcookie, ((HttpWebResponse)LoginPage_Return).Headers["Set-Cookie"], Host);
                }

            }
            catch (Exception AnyError)
            {

                throw AnyError;
            }

            string responseBody = string.Empty;
            if (LoginPage_Return.ContentEncoding.ToLower().Contains("gzip"))
            {
                using (System.IO.Compression.GZipStream stream = new System.IO.Compression.GZipStream(LoginPage_Return.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            else if (LoginPage_Return.ContentEncoding.ToLower().Contains("deflate"))
            {
                using (System.IO.Compression.DeflateStream stream = new System.IO.Compression.DeflateStream(LoginPage_Return.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                using (Stream stream = LoginPage_Return.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            LoginPage.Abort();
            return responseBody;
            //返回：
            //            {
            //"BaseResponse": {
            //"Ret": 0,
            //"ErrMsg": ""
            //}
            //,
            //"MediaId": "@crypt_d4a4de27_1bf919affd8946a58cfbaad9eeea3cc0eb86c6890d6d66227dff9d3ace4cb1b4ec8db12709140e6c0bfdfc6b92c27e3c4426225ddc43c9241aacaf18dc8a5f92bc13106caccea22ba76b324c9a796ef1377e83a585b8ceab687df00db68f39fee1d7531b1594737c44e379a4b4d539c466d377a749f21ae1dd3917dcaca2c5ba223d3034eb193a4258dca898ad4aa9d5b1e356eb5e7879bea7b9a0897f1f7f96a6acdb2ea255f9a8873cacc1c3fa827ca5a7c9182e149ca80c5ff2d2a0048fdb8a1c0e61b6a3cc3eb5902f7a4f9b524983eefc37bc84e69f5374898f3312d615022d188fd04b91b0e6be51118a3e7df645512d6c5419e80f32584a3bbec8692179478ea4ee6c4a85c99ec92d8d1ba965a1be94aaf3fb5f5de702bf519aacc073242189f72616b7590fe94986b43a395b63a8d889b6e82375d472cc57df1c0422",
            //"StartPos": 79253,
            //"CDNThumbImgHeight": 100,
            //"CDNThumbImgWidth": 74,
            //"EncryFileName": "Data%2Ejpg"
            //}


            #endregion





            //POST /cgi-bin/mmwebwx-bin/webwxsendmsgimg?fun=async&f=json HTTP/1.1
            //Host: wx2.qq.com
            //        {
            //    "BaseRequest": {
            //        "Uin": 2402981522,
            //        "Sid": "W8Ia83fMnlcuKK0U",
            //        "Skey": "@crypt_bbd454c7_9465a672aa848c64c765ea727877bdd1",
            //        "DeviceID": "e871841233370548"
            //    },
            //    "Msg": {
            //        "Type": 3,
            //        "MediaId": "@crypt_d4a4de27_1bf919affd8946a58cfbaad9eeea3cc0eb86c6890d6d66227dff9d3ace4cb1b4ec8db12709140e6c0bfdfc6b92c27e3c4426225ddc43c9241aacaf18dc8a5f92bc13106caccea22ba76b324c9a796ef1377e83a585b8ceab687df00db68f39fee1d7531b1594737c44e379a4b4d539c466d377a749f21ae1dd3917dcaca2c5ba223d3034eb193a4258dca898ad4aa9d5b1e356eb5e7879bea7b9a0897f1f7f96a6acdb2ea255f9a8873cacc1c3fa827ca5a7c9182e149ca80c5ff2d2a0048fdb8a1c0e61b6a3cc3eb5902f7a4f9b524983eefc37bc84e69f5374898f3312d615022d188fd04b91b0e6be51118a3e7df645512d6c5419e80f32584a3bbec8692179478ea4ee6c4a85c99ec92d8d1ba965a1be94aaf3fb5f5de702bf519aacc073242189f72616b7590fe94986b43a395b63a8d889b6e82375d472cc57df1c0422",
            //        "Content": "",
            //        "FromUserName": "@ac0308d92ae0d88beb8d90feee45a86c02f36bd5f3560398b544abeac4e70a14",
            //        "ToUserName": "@@f2a3e52ae022d3303864e9dfcda631635a429b2c28c4d93388ec25429280df00",
            //        "LocalID": "15232671098860552",
            //        "ClientMsgId": "15232671098860552"
            //    },
            //    "Scene": 0
            //}


        }



        #region 从包含多个 Cookie 的字符串读取到 CookieCollection 集合中
        public static void AddCookieWithCookieHead(CookieCollection cookieCol, string cookieHead, string defaultDomain)
        {
            if (cookieHead == null) return;

            string[] ary = cookieHead.Split(new string[] { "GMT" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ary.Length; i++)
            {
                string CookieStr = ary[i].Trim() + "GMT";
                if (CookieStr.StartsWith(","))
                {
                    CookieStr = CookieStr.Substring(1);
                }
                Cookie ck = GetCookieFromString(CookieStr, defaultDomain);
                if (ck != null)
                {
                    cookieCol.Add(ck);
                }
            }


        }
        #endregion
        #region 读取某一个 Cookie 字符串到 Cookie 变量中
        private static Cookie GetCookieFromString(string cookieString, string defaultDomain)
        {
            string[] ary = cookieString.Split(';');
            System.Collections.Hashtable hs = new System.Collections.Hashtable();
            for (int i = 0; i < ary.Length; i++)
            {
                string s = ary[i].Trim();
                int index = s.IndexOf("=");
                if (index > 0)
                {
                    hs.Add(s.Substring(0, index), s.Substring(index + 1));
                }
            }
            Cookie ck = new Cookie();
            foreach (object Key in hs.Keys)
            {
                if (Key.ToString().ToLower() == "path") ck.Path = hs[Key].ToString();

                else if (Key.ToString().ToLower() == "expires")
                {
                    //ck.Expires = DateTime.Parse(hs[Key].ToString());
                }
                else if (Key.ToString().ToLower() == "domain") ck.Domain = defaultDomain;//hs[Key].ToString();
                else
                {
                    ck.Name = Key.ToString();
                    ck.Value = hs[Key].ToString();
                }
            }
            if (ck.Name == "") return null;
            if (ck.Domain == "") ck.Domain = defaultDomain;
            return ck;
        }
        #endregion


         public static string CleanHtml(string strHtml)
    {
      if (string.IsNullOrEmpty(strHtml)) return strHtml;
      //删除脚本
      //Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase)
      strHtml = Regex.Replace(strHtml, "(<script(.+?)</script>)|(<style(.+?)</style>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
      //删除标签
      var r = new Regex(@"</?[^>]*>", RegexOptions.IgnoreCase);
      Match m;
      for (m = r.Match(strHtml); m.Success; m = m.NextMatch())
      {
        strHtml = strHtml.Replace(m.Groups[0].ToString(), "");
      }
      return strHtml.Trim();
    }

    }
    public class Util_MD5
    {
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
        public static string GetStrMd5X2(string ConvertString)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2;
        }
        public static bool MD5Success(string Total, out DateTime? Value, Guid MyGuid)
        {
            byte[] Totalb = null;
            try
            {

                Totalb = Convert.FromBase64String(Total);



                string Text = Encoding.UTF8.GetString(Totalb);
                string Time = Text.Substring(Text.Length - 59);
                //yyyy-MM-dd HH:mm：ss FFFF
                //bf697c61-e1ef-4848-9f03-558ab55686e9 36位
                string MD5 = Text.Substring(0, Text.Length - 59);
                string CheckMD5 = GetStrMd5X2(Time);

                Guid Passid = new Guid(Time.Substring(0, 36));
                string OutTime = Time.Substring(36);
                if (CheckMD5 == MD5 && Passid == MyGuid)
                {
                    Value = DateTime.Parse(OutTime);
                    return true;
                }
                else
                {
                    Value = null;
                    return false;
                }
            }
            catch (Exception)
            {
                Value = null;
                return false;
            }
        }

        public static string BuidMD5ActiveCode(DateTime EndDate, Guid MyGuid)
        {
            string ToConvert = MyGuid.ToString() + EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ToConvert = GetStrMd5X2(ToConvert) + ToConvert;
            byte[] bs = Encoding.UTF8.GetBytes(ToConvert);
            return Convert.ToBase64String(bs);
        }
    }

    public class Util_Math
    {
        public static bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
                !objTwoDotPattern.IsMatch(strNumber) &&
                !objTwoMinusPattern.IsMatch(strNumber) &&
                objNumberPattern.IsMatch(strNumber);
        }

        public static decimal NullToZero(decimal? dbvalue,Int32 KeepCount=0)
        {
            return dbvalue.HasValue ? Math.Round( dbvalue.Value,KeepCount) : 0;
        }
    }

    public class Util_Cofig
    {



        //加密web.Config中的指定节
        public static void ProtectSection(string sectionName,string Path)
        {
            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(Path);
            System.Configuration.ConfigurationSection section = config.GetSection(sectionName);
            if (section != null && !section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
            }
        }

        //解密web.Config中的指定节
         public static void UnProtectSection(string sectionName,string Path)
        {
            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(Path);
            System.Configuration.ConfigurationSection section = config.GetSection(sectionName);
            if (section != null && section.SectionInformation.IsProtected)
            {
                section.SectionInformation.UnprotectSection();
                config.Save();
            }
        }
    }

}
