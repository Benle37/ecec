using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using do_an_ao_hoa.Models;
using MongoDB.Driver;

namespace do_an_ao_hoa.Controllers
{
    public class StudentController : Controller
    {
        public StudentController()
        {
            MongoHelper.ConnectToMongoService();
            MongoHelper.students_collection = MongoHelper.database.GetCollection<Student>("students");
        }
        // GET: Student
        public ActionResult Index()
        {

            var filter = Builders<Student>.Filter.Ne("_id", "");
            var result = MongoHelper.students_collection.Find(filter).ToList();
            return View(result);
        }

        // GET: Student/Details/5
        public ActionResult Details(string id)
        {
            var filter = Builders<Student>.Filter.Eq("_id", id);
            var result = MongoHelper.students_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        public static Random r = new Random();
        private object random(int v)
        {
            string code = "abcefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(code, v).Select(s => s[r.Next(s.Length)]).ToArray());
        }

        // POST: Student/Create
        [HttpPost]
        public  ActionResult Create(FormCollection collection)
        {
            Object _id = random(24);
            var file = Request.Files["file"];
            string image = string.Empty;
            if (file != null)
            {
                try
                {
                    string path = Server.MapPath("~/Assets/img/" + file.FileName);
                    file.SaveAs(path);
                    image = file.FileName;
                }
                catch (Exception)
                {
                }
            }
            MongoHelper.students_collection.InsertOneAsync(new Student()
            {
                _id = _id,
                code = collection["code"],
                name = collection["name"],
                email = collection["email"],
                phone = collection["phone"],
                address = collection["address"],
                image = image
            });
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        public ActionResult Edit(string id)
        {
            var filter = Builders<Student>.Filter.Eq("_id", id);
            var result = MongoHelper.students_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                string image = collection["image"];

                var file = Request.Files["file"];
                if (file != null)
                {
                    try
                    {
                        string path = Server.MapPath("~/Assets/img/" + file.FileName);
                        file.SaveAs(path);
                        image = file.FileName;
                    }
                    catch (Exception)
                    {
                    }
                }
                var filter = Builders<Student>.Filter.Eq("_id", id);
                var update = Builders<Student>.Update
                    .Set("code", collection["code"])
                    .Set("name", collection["name"])
                    .Set("email", collection["email"])
                    .Set("phone", collection["phone"])
                    .Set("address", collection["address"])
                    .Set("image", image);
                MongoHelper.students_collection.UpdateOneAsync(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq("_id", id);
                var result = MongoHelper.students_collection.Find(filter).FirstOrDefault();
                MongoHelper.students_collection.DeleteOneAsync(filter);
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = 0
                });

            }
           
            return Json(new
            {
                status = 1
            });
        }
    }
}
