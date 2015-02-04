using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace MvcApplication4.Controllers
{
    public class Base64Text
    {
        public string text { get; set; }
    }
    public class ConnectController : ApiController
    {
        public string[] Get()
        {
            return new string[]
        {
             "Hello",
             "World"
        };
        }
        public HttpResponseMessage Post(Base64Text contact)
        {

            var response = Request.CreateResponse<Base64Text>(System.Net.HttpStatusCode.Created, contact);
            // response.Headers.Add("Access-Control-Allow-Origin", "*");
            var base64Data = Regex.Match(contact.text, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            byte[] bytes = Convert.FromBase64String(base64Data);

            Image image;
            using (ImagePocketEntities database = new ImagePocketEntities())
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    var numb = database.ImagesPathes.Count() + 1;
                    image = Image.FromStream(ms);
                    var newpath = "c:/test/new_" + Convert.ToString(numb) + ".png";
                    image.Save(newpath);
                    database.ImagesPathes.Add(new ImagesPathes { Path = newpath, ID = numb });
                    database.SaveChanges();
                }
            }


            return response;
        }
    }

}
