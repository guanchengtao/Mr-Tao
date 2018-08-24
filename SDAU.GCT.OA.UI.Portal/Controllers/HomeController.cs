using SDAU.GCT.OA.IBLL;
using SDAU.GCT.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDAU.GCT.OA.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {
        public IUserInfoService UserInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        short delFlagNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public ActionResult Index()
        {
            ViewBag.data = LoadUserMenu();

            int uid = LoginUserInfo.Id;
            var user = UserInfoService.GetEntities(u => u.Id == uid).FirstOrDefault();
            ViewBag.LoginUserName = user.Uname;
            //this.UserInfo.DelFlag;
            return View();
        }
        //根据用户权限加载菜单
        public List<ActionInfo> LoadUserMenu()
        {
            int uid = LoginUserInfo.Id;
            var user = UserInfoService.GetEntities(u => u.Id == uid).FirstOrDefault();

            var allRole = user.RoleInfo;
            //用户所有的角色对应的权限
            var allRoleActionIds = (from r in allRole
                                    from a in r.ActionInfo
                                    select a.Id).ToList();
            //拿到用户本身就不具备的权限
            var alldeleteActionIds = (from r in user.R_UserInfo_ActionInfo
                                      where r.HasPermission == false&&r.DelFlag==delFlagNormal
                                      select r.ActionInfoId).ToList();
            //做一个差集：角色权限-特殊拒绝权限
            //var allactionIds = allRoleActionIds.Where(u=>!alldeleteActionIds.Contains(u));
            var allactionIds = (from r in allRoleActionIds
                                where !alldeleteActionIds.Contains(r)
                                select r).ToList();


            //拿到用户本身就具有的权限
            var allUserActionIds = (from r in user.R_UserInfo_ActionInfo
                                    where r.HasPermission == true &&r.DelFlag== delFlagNormal
                                    select r.ActionInfoId).ToList();
            //把当前用户所有权限取出
            allactionIds.AddRange(allUserActionIds.AsEnumerable());
            allactionIds = allactionIds.Distinct().ToList();//去重操作
            //拿到菜单权限
            var actionList = ActionInfoService.GetEntities(u => allactionIds.Contains(u.Id) && u.IsMenu == true && u.DelFlag == (short)Model.Enum.DelFlagEnum.Normal).ToList();


            return actionList;
            //var data = from a in actionList
            //           select new { icon = a.MenuIcon, title = a.ActionName, url = a.Url };
            //return Json(data, JsonRequestBehavior.AllowGet);
            
        }
    }
}