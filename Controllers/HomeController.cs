using HtmlAgilityPack;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;



namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        public static bool isValidURL(string url) 
        {
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public ActionResult Test() 
        {
            TestViewModel model = new TestViewModel();
            string SourceURL_1 = "https://movies.yahoo.com.tw/movie_intheaters.html";
            //string SourceURL_1 = "http://gnn.gamer.com.tw/4/133124.html";
            //string SourceURL_1 = "http://www.coolpc.com.tw/evaluate.phpsdfsdfanlksjfiojsidhfsdfsdfsdfsdf";
            string SourceURL_2 = "http://www.coolpc.com.tw/evaluate.php";
            
            string Endpoint = "";
            if (isValidURL(SourceURL_1))
            {
                model.dataSource = "目前使用第一個資料來源";
                Endpoint = SourceURL_1;
            }
            else if (isValidURL(SourceURL_2))
            {
                model.dataSource = "目前使用第二個資料來源";
                Endpoint = SourceURL_2;
            }
            else 
            {
                model.dataSource = "兩個資料來源都掛惹";
            }

            WebClient url = new WebClient();
            MemoryStream ms = new MemoryStream(url.DownloadData(Endpoint));
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);
            
            var list_type = new List<string>();
            //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//select[@name='n4']//optgroup");
            //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@id=\"content_l\"]//div[2]/ul[2]//li");//*[@id="#content_l"]/div[2]/ul[2]/li[1]/div[2]
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul[@class=\"release_list\"]//li");
            foreach (HtmlNode node in nodes) 
            {
                //var type = node.Attributes["href"].Value;
                var type = node.InnerHtml;
                list_type.Add(type);
            }
            List<string> list_name = new List<string>();
            for (int i = 0; i < list_type.Count; i++) 
            {
                var name = doc.DocumentNode.SelectSingleNode("//div[@class=\"release_movie_name\"]").SelectNodes("div[@class='en']");//InnerText.Split('\n').ToList();
                foreach (HtmlNode j in name)
                {
                    var str = j.InnerText.Split('\n');
                    string str1 = str[2].Trim();
                    list_name.Add(str1);
                }
            }
            //*[@id="content_l"]/div[2]/ul[2]/li[1]/div[2]
            //List<string> list_name = doc.DocumentNode.SelectSingleNode("//select[@name='n4']").InnerText.Split('\n').ToList();

            //list_name = list_name.Where(x => x != "").ToList();
            //list_name.RemoveRange(0, 1);

            var models = new List<MovieInfo>();
            int number = 0;
            /*for (int i = 0; i < list_name.Count; i++) 
            {
                string type = list_type[number];
                string name = list_name[i];
                if (name == "")
                {
                    number++;
                }
                else 
                {
                    models.Add(new MovieInfo()
                    {
                        movie_name_ch = type,
                        movie_name_en = name
                    }) ;

                }
            }*/
            List<string> temp = models.Select(x => x.movie_name_ch + " " + x.movie_name_en).ToList();
            model.data1 = string.Join("<br />", temp);
            return View(model);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}