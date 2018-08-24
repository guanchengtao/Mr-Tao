using SDAU.GCT.OA.IBLL;
using SDAU.GCT.OA.Model;
using SDAU.GCT.OA.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDAU.GCT.OA.UI.Portal.Controllers
{     //意味着登录的时候不需要参与校验
    [LoginCheckFilterAttr(IsCheck =false)]
    public class UserLoginController :BaseController
    {
        public UserLoginController()
        {
            this.IsCheck = false;
        }
        // GET: UserLogin
        public IUserInfoService UserInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 测试列表添加代码段
        public ActionResult Index2()
        {
            ViewData.Model = UserInfoService.GetEntities(u => true);
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                UserInfoService.Add(userInfo);
            }
            return RedirectToAction("Index2");
        } 
        #endregion
        public ActionResult ShowVcode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string Codestr = validateCode.CreateValidateCode(4);
            Session["VCode"] = Codestr;
            byte[] img = validateCode.CreateValidateGraphic(Codestr);
            return File(img, @"image/jpeg");
        }
        public ActionResult LoginProcess()
        {
            #region 验证码
            string strcode = Request["vCode"];
            string sessioncode = Session["Vcode"].ToString();
            if (string.IsNullOrEmpty(sessioncode) || strcode != sessioncode)
            {
                return Content("验证码错误");
            }
            #endregion

            string name = Request["LoginCode"].ToString();
            string pwd = Request["LoginPwd"].ToString();
            short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
           var userInfo =
                UserInfoService.GetEntities(u => u.Uname == name && u.Pwd == pwd&&u.DelFlag==delNormal)
                .FirstOrDefault();
            if(userInfo==null)
            {
                return Content("登陆失败！！！");
            }
            else
            {
                //改用mm+cookie
                string loginuserId = Guid.NewGuid().ToString();
                //把用户数据写入memCache
                Common.Cache.CacheHelper.AddCache(loginuserId, userInfo, DateTime.Now.AddMinutes(20));

               // int count = (int)Common.Cache.CacheHelper.GetCache(loginuserId);
                
                //往客户端写入cookie
                Response.Cookies["loginuserId"].Value = loginuserId;
                return Content("ok");
            }
        }
    }
}