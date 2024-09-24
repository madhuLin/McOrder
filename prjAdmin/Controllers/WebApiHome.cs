using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using prjAdmin.Models;

namespace prjAdmin
{
    public class WebApiController : ApiController
    {
        // GET api/<controller>
        dbOrderFoodEntities dbOrderFood = new dbOrderFoodEntities();
        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public int Delete(string id)
        {
            int num = 0;
            try
            {
                var employye = dbOrderFood.bEmployee.Where(m => m.bUserId == id).FirstOrDefault();
                dbOrderFood.bEmployee.Remove(employye);
                num = dbOrderFood.SaveChanges();
            }
            catch(Exception ex)
            {
            }

            return num;
        }



        private string basepath = "~/Img/product";

        //[HttpPost]
        //public String Upload(HttpPostedFileBase file)
        //{
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        //string extension = Path.GetExtension(file.FileName);
        //        //string extension = Path.GetFileName(file.FileName);
        //        //string fileName = $"{Guid.NewGuid()}{extension}";
        //        string fileName = Path.GetFileName(file.FileName);

        //        string savePath = Path.Combine(Server.MapPath(basepath), fileName);

        //        file.SaveAs(savePath);
        //        //ViewBag.Message = savePath;
        //        return savePath;
        //    }
        //    else
        //    {
        //        //ViewBag.Message = "No file uploaded";
        //        return "No file uploaded";
        //    }

        //}

        //public ActionResult Download(string name)
        //{
        //    try
        //    {
        //        var filePath = Path.Combine(Server.MapPath(basepath), name);

        //        if (System.IO.File.Exists(filePath))
        //        {
        //            return File(filePath, "application/octet-stream", name);
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return new HttpStatusCodeResult(500, e.Message);
        //    }
        //}



    }
}