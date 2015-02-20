using MvcApplication4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.IO;
using CDO;
using ADODB;
namespace MvcApplication4.Controllers
{
    public class HtmlController : ApiController
    {

        public HttpResponseMessage Post()
        {
            /*string filePath = @"c:/test/thelast2.mht",
                url2 = "http://stackoverflow.com/questions/10412691/file-api-not-working-with-blob-in-google-chrome-extension";
            bool result = false;
            CDO.Message msg = new CDO.Message();
            ADODB.Stream stm = null;
            try
            {
                msg.MimeFormatted = true;
                msg.CreateMHTMLBody(url2, CDO.CdoMHTMLFlags.cdoSuppressNone, "", "");
                stm = msg.GetStream();
                stm.SaveToFile(filePath, ADODB.SaveOptionsEnum.adSaveCreateOverWrite);
                msg = null;
                stm.Close();
                result = true;
            }
            catch
            { throw; }*/
            var x = Request.Content.ReadAsStringAsync();

            x.Wait();
            
            //var data = System.Text.Encoding.UTF8.GetString(x.Result);
            using (ImagePocketEntities1 database = new ImagePocketEntities1())
            {

                var numb = database.MhtmlPages.Count() + 1;
                var newpath = String.Format(@"~/Photoes/newpage_{0}.mht", Convert.ToString(numb));
                database.MhtmlPages.Add(new MhtmlPages { PagePath = newpath, ID = numb});
                database.SaveChanges();
                
                File.WriteAllText(String.Format(@"C:\Users\Андрій\Documents\Visual Studio 2013\Projects\MvcApplication4\MvcApplication4\Photoes\newpage_{0}.mht", Convert.ToString(numb)), x.Result);
                
            }





            var response = Request.CreateResponse<string>(System.Net.HttpStatusCode.Created, x.Result);
            return response;
            return null;
        }
    }
    
}
