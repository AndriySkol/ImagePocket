using MvcApplication4.Models;
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
        public HttpResponseMessage Post(BoxedString contact)
        {
            String a;
            
            var response = Request.CreateResponse<BoxedString>(System.Net.HttpStatusCode.Created, contact);
            // response.Headers.Add("Access-Control-Allow-Origin", "*");
            var base64Data = Regex.Match(contact.Text, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            byte[] bytes = Convert.FromBase64String(base64Data);

            Image image;
            using (ImagePocketEntities1 database = new ImagePocketEntities1())
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    var numb = database.ImagesPathes.Count() + 1;
                    image = Image.FromStream(ms);
                    var newpath = @"~/Photoes/newimage_" + Convert.ToString(numb) + ".png";
                    image.Save(string.Format(@"C:\Users\Андрій\Documents\Visual Studio 2013\Projects\MvcApplication4\MvcApplication4\Photoes\newimage_{0}.png", Convert.ToString(numb)));
                    image.GetThumbnailImage(640, 320, null, new IntPtr()).Save(String.Format(@"C:\Users\Андрій\Documents\Visual Studio 2013\Projects\MvcApplication4\MvcApplication4\Photoes\newimagePreview_{0}.png", Convert.ToString(numb)));
                    database.ImagesPathes.Add(new ImagesPathes { Path = newpath, ID = numb });
                    database.SaveChanges();
                }
            }


            return response;
        }
    }

}
