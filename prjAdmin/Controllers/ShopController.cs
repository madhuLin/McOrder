using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace prjAdmin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        //建立可存取dbShoppingCar.mdf 資料庫的dbShoppingCarEntities 類別物件db
        dbOrderFoodEntities db = new dbOrderFoodEntities();
        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowDish(String name = "")
        {
            List<Dish> dish;
            var categories = db.Category.OrderByDescending(m => m.Sort).ToArray();
            var tmp = new String[categories.Length];
            for (int i = 0; i < categories.Length; i++)
            {
                tmp[i] = categories[i].Name;
            }

            ViewBag.category = tmp;
            if (name == "")
            {
                //取得所有產品放入products
                dish = db.Dish
                    .OrderByDescending(m => m.UpdateTime).ToList();
                ViewBag.Name = "全部";
            }
            else
            {
                int categoryId = db.Category.Where(m => m.Name == name).FirstOrDefault().Id;
                //取得所有產品放入products
                dish = db.Dish.Where(m => m.CategoryId == categoryId)
                    .OrderByDescending(m => m.UpdateTime).ToList();
                ViewBag.Name = name;
            }

            return View(dish);
        }
        //Get: Home/Login
        public ActionResult Login()
        {
            return View();
        }
        //Post: Home/Login
        [HttpPost]
        public ActionResult Login(string fUserId, string fPwd)
        {
            // 依帳密取得會員並指定給member
            var member = db.tMember
                .Where(m => m.tUserId == fUserId && m.tPwd == fPwd)
                .FirstOrDefault();
            //若member為null，表示會員未註冊
            if (member == null)
            {
                ViewBag.Message = "帳密錯誤，登入失敗";
                return View();
            }
            //使用Session變數記錄歡迎詞
            Session["WelCome"] = member.tName + "歡迎光臨";
            FormsAuthentication.RedirectFromLoginPage(fUserId, true);
            return RedirectToAction("Index", "Member");
        }

        //Get:Home/Register
        public ActionResult Register()
        {
            return View();
        }
        //Post:Home/Register
        [HttpPost]
        public ActionResult Register(tMember pMember)
        {
            //若模型沒有通過驗證則顯示目前的View
            if (ModelState.IsValid == false)
            {
                return View();
            }
            // 依帳號取得會員並指定給member
            var member = db.tMember
                .Where(m => m.tUserId == pMember.tUserId)
                .FirstOrDefault();
            //若member為null，表示會員未註冊
            if (member == null)
            {
                //將會員記錄新增到tMember資料表
                db.tMember.Add(pMember);
                db.SaveChanges();
                //執行Home控制器的Login動作方法
                return RedirectToAction("Login");
            }
            ViewBag.Message = "此帳號己有人使用，註冊失敗";
            return View();
        }

        public ActionResult Dishdetails(int Id)
        {
            return View(db.Dish.Find(Id));
        }
    }
}