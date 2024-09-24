using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjAdmin.Models;
using PagedList;
using System.IO;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace prjAdmin.Controllers
{
    public class HomeController : Controller
    {

        dbOrderFoodEntities dbOrderFood = new dbOrderFoodEntities();



        // GET: Home
        public ActionResult Index()
        {
            var data = dbOrderFood.tOrderDetail.OrderBy(m => m.fId);
            int currentMonth = DateTime.Now.Month;

            var Orders = dbOrderFood.tOrder.OrderBy(m => m.fId).ToArray();
            for (int i = 0; i < Orders.Length; i++)
            {
                String guid = Orders[i].fOrderGuid;
                var ordersDetails = dbOrderFood.tOrderDetail.Where(m => m.fOrderGuid == guid);
                int price = 0;
                foreach (var item in ordersDetails)
                {
                    price += item.fPrice.Value;
                }
                Orders[i].Price = price;
            }
            int sum = 0, curr = 0;
            foreach (var item in Orders)
            {
                DateTime dateTime = (DateTime)item.fDate;
                if (currentMonth == dateTime.Month) curr += item.Price;
                sum += item.Price;   
            }
            ViewBag.sum = sum;
            ViewBag.curr = curr;
            return View();
        }



        public ActionResult ShowEmployee()
        {
            var employee = dbOrderFood.bEmployee.OrderByDescending(m => m.bUpdateTime).ToList();
            return View(employee);
        }

        public ActionResult Delete(string bUserId)
        {
            var employee = dbOrderFood.bEmployee.Where(m => m.bUserId == bUserId).FirstOrDefault();
            dbOrderFood.bEmployee.Remove(employee);
            dbOrderFood.SaveChanges();
            return RedirectToAction("ShowEmployee");
        }

        public ActionResult Detailsemplooyee(string bUserId = "user01")
        {
            bEmployee employee = dbOrderFood.bEmployee.Where(m => m.bUserId == bUserId).FirstOrDefault();

            return View(employee);
        }

        public ActionResult Edit(string bUserId)
        {
            bEmployee employee = dbOrderFood.bEmployee.Where(m => m.bUserId == bUserId).FirstOrDefault();
            ViewBag.Id = employee.bId;
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(bEmployee employee, string bUserId)
        {
            if(ModelState.IsValid)
            {
                var dbEmployee = dbOrderFood.bEmployee.Find(employee.bId);
                dbEmployee.bUserId = employee.bUserId;
                dbEmployee.bName = employee.bName;
                dbEmployee.bSex = employee.bSex;
                dbEmployee.bPwd = employee.bPwd;
                dbEmployee.bEmail = employee.bEmail;
                dbEmployee.bUpdateTime = DateTime.Now;

                dbOrderFood.SaveChanges();
                return RedirectToAction("ShowEmployee");
            }
            
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(bEmployee employee)
        {
            //if(employee.bSex == "男")
            try
            {
                if(ModelState.IsValid)
                {
                    employee.bUpdateTime = DateTime.Now;
                    dbOrderFood.bEmployee.Add(employee);
                    dbOrderFood.SaveChanges();
                    return RedirectToAction("ShowEmployee");
                }
            }
            catch(Exception ex) {
                ViewBag.Error = true;
            }
            return View(employee);
        }





        public ActionResult ShowMember()
        {
            var Member = dbOrderFood.tMember.OrderByDescending(m => m.bUpdateTime).ToList();
            return View(Member);
        }

        public ActionResult DeleteMember(string bUserId)
        {
            var member = dbOrderFood.tMember.Where(m => m.tUserId == bUserId).FirstOrDefault();
            dbOrderFood.tMember.Remove(member);
            dbOrderFood.SaveChanges();
            return RedirectToAction("ShowMember");
        }

        public ActionResult DetailsMember(string bUserId = "user01")
        {
            bEmployee employee = dbOrderFood.bEmployee.Where(m => m.bUserId == bUserId).FirstOrDefault();

            return View(employee);
        }

        public ActionResult EditMember(string bUserId)
        {
            tMember member = dbOrderFood.tMember.Where(m => m.tUserId == bUserId).FirstOrDefault();
            //ViewBag.Id = member.tId;
            return View(member);
        }

        [HttpPost]
        public ActionResult EditMember(tMember member)
        {
            if (ModelState.IsValid)
            {
                var dbmember = dbOrderFood.tMember.Find(member.tId);
                dbmember.tUserId = member.tUserId;
                dbmember.tName = member.tName;
                dbmember.tPwd = member.tPwd;
                dbmember.tEmail = member.tEmail;
                dbmember.bUpdateTime = DateTime.Now;

                dbOrderFood.SaveChanges();
                return RedirectToAction("ShowMember");
            }

            return View(member);
        }

        public ActionResult CreateMember()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMember(tMember member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    member.bUpdateTime = DateTime.Now;
                    dbOrderFood.tMember.Add(member);
                    dbOrderFood.SaveChanges();
                    return RedirectToAction("ShowMember");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
            }
            return View(member);
        }






        public ActionResult ShowCategory()
        {
            var category = dbOrderFood.Category.OrderBy(m => m.Id);
            return View(category);
        }


        public ActionResult EditCategory(int Id=1)
        {
            Category category= dbOrderFood.Category.Where(m => m.Id == Id).FirstOrDefault();
            ViewBag.Id = category.Id;
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                var data = dbOrderFood.Category.Find(category.Id);
                data.Name = category.Name;
                data.Type = category.Type;
                data.Sort = category.Sort;
                data.UpdateTime = DateTime.Now;
                dbOrderFood.SaveChanges();
                return RedirectToAction("ShowCategory");
            }   
            return View(category);
        }

        public ActionResult DeleteCategory(int Id)
        {
            var category= dbOrderFood.Category.Where(m => m.Id == Id).FirstOrDefault();
            dbOrderFood.Category.Remove(category);
            dbOrderFood.SaveChanges();
            return RedirectToAction("ShowCategory");
        }


        public ActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category category)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    category.UpdateTime = DateTime.Now;
                    dbOrderFood.Category.Add(category);
                    dbOrderFood.SaveChanges();
                    return RedirectToAction("ShowCategory");
                }
            }
            catch(Exception ex) {
                ViewBag.Error = true;
            }
            return View(category);
        }

        public ActionResult ShowDish()
        {
            var dishs = dbOrderFood.Dish.OrderByDescending(m => m.UpdateTime);
            Dictionary<int, String> dic = new Dictionary<int, String>();
            var options = dbOrderFood.Category.OrderBy(m => m.Id);
            foreach (var option in options) dic.Add(option.Id, option.Name);
            //dic.Add(0, "未知");
            ViewBag.dic = dic;
            return View(dishs);
        }


        public ActionResult EditDish(int Id)
        {
            Dish dish = dbOrderFood.Dish.Find(Id);
            dish.Id = dish.Id;
            dish.Img = dish.Img;
            ViewBag.select = dish.CategoryId.ToString();
            var options = dbOrderFood.Category.OrderBy(m => m.Id);
            dish.selectLists = new SelectList(options, "Id", "Name");
            return View(dish);
        }

        [HttpPost]
        public ActionResult EditDish(Dish dish)
        {
            try
            {
                // 執行驗證操作的程式碼
                if (ModelState.IsValid)
                {
                    var data = dbOrderFood.Dish.Find(dish.Id);
                    data.Name = dish.Name;
                    data.Img = dish.Img;
                    data.Sort = dish.Sort;
                    data.Code = dish.Code;
                    data.CategoryId = dish.CategoryId;
                    data.UpdateTime = DateTime.Now;
                    dbOrderFood.SaveChanges();
                    return RedirectToAction("ShowDish");
                }
            }
            catch (DbEntityValidationException ex)
            {
                String str = "";
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("屬性名稱: {0}, 驗證錯誤: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        str += validationError.ErrorMessage;
                    }
                }
                ViewBag.Error = str;
            }
            var options = dbOrderFood.Category.OrderBy(m => m.Id);
            dish.selectLists = new SelectList(options, "Id", "Name");
            ViewBag.select = dish.CategoryId.ToString();
            return View(dish);
        }

        public ActionResult DeleteDish(int Id)
        {
            var dish = dbOrderFood.Dish.Find(Id);
            dbOrderFood.Dish.Remove(dish);
            dbOrderFood.SaveChanges();
            return RedirectToAction("ShowDish");
        }


        public ActionResult CreateDish()
        {
            //var options = dbOrderFood.Category.Select(m => new SelectListItem 
            //{ Value = m.Id.ToString() , Text = m.Name });
            var options = dbOrderFood.Category.OrderBy(m => m.Id);
            Dish dish = new Dish();
            dish.selectLists = new SelectList(options, "Id", "Name");
            //ViewBag.selectList = new SelectList(options, "Id", "Name");

            return View(dish);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDish(Dish dish)
        {
            try
            {
                //IEnumerable<SelectListItem> selectedItems = dish.CategoryList;
                //string selectedValue = "";

                //// 遍历选中的选项
                //foreach (var item in selectedItems)
                //{
                //    if (item.Selected)
                //    {
                //        selectedValue = item.Value;
                //        // 执行其他操作
                //    }
                //}
                if (ModelState.IsValid)
                {
                    dish.UpdateTime = DateTime.Now;
                    dbOrderFood.Dish.Add(dish);
                    dbOrderFood.SaveChanges();
                    return RedirectToAction("ShowDish");
                }
            }
            catch (DbEntityValidationException ex)
            {
                String str = "";
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        str += validationError.ErrorMessage;
                    }
                }
                ViewBag.Error = str;
            }
            var options = dbOrderFood.Category.OrderBy(m => m.Id);
            dish.selectLists = new SelectList(options, "Id", "Name");
            return View(dish);
        }

        //private string basepath = "Img/product";
        private string fileName = "";

        [HttpPost]
        public String Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                //string extension = Path.GetExtension(file.FileName);
                //string extension = Path.GetFileName(file.FileName);
                //string fileName = $"{Guid.NewGuid()}{extension}";
                fileName = Path.GetFileName(file.FileName);

                string savePath = Path.Combine(Server.MapPath("~/Img/product"), fileName);

                file.SaveAs(savePath);
                ViewBag.Message = fileName;
                return fileName;
            }
            else
            {
                return "No file uploaded";
            }
        }

        public ActionResult ShowOrder()
        {
            var Orders = dbOrderFood.tOrder.OrderBy(m => m.fId).ToArray();
            for(int i = 0; i < Orders.Length; i++ )
            {
                String guid = Orders[i].fOrderGuid;
                var ordersDetails = dbOrderFood.tOrderDetail.Where(m => m.fOrderGuid == guid);
                int price = 0;
                foreach(var item in ordersDetails)
                {
                    price += item.fPrice.Value;
                }
                Orders[i].Price = price;
            }
            return View(Orders);
        }


        [HttpGet]
        public ActionResult getData()
        {
            int[] months = new int [12];

            var Orders = dbOrderFood.tOrder.OrderBy(m => m.fId).ToArray();
            for (int i = 0; i < Orders.Length; i++)
            {
                String guid = Orders[i].fOrderGuid;
                var ordersDetails = dbOrderFood.tOrderDetail.Where(m => m.fOrderGuid == guid);
                int price = 0;
                foreach (var item in ordersDetails)
                {
                    price += item.fPrice.Value;
                }
                Orders[i].Price = price;
            }

            foreach (var item in Orders)
            {
                DateTime dateTime = (DateTime)item.fDate;
                int currentMonth = dateTime.Month;
                months[currentMonth-1] += item.Price;
            }
            return Json(months, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult getData2()
        {
            var orderDetails = dbOrderFood.tOrderDetail.OrderBy(m => m.fId).ToArray();

            Dictionary<int, String> dic = new Dictionary<int, String>();
            var options = dbOrderFood.Category.OrderBy(m => m.Id);
            foreach (var option in options) dic.Add(option.Id, option.Name);

            Dictionary<String, int> res = new Dictionary<String, int>();


            foreach (var item in orderDetails)
            {
                var dish = dbOrderFood.Dish.Find(int.Parse(item.fPId));
                if (!res.ContainsKey(dic[dish.CategoryId])) res.Add(dic[dish.CategoryId], 0);
                res[dic[dish.CategoryId]] += dish.Sort.Value;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}