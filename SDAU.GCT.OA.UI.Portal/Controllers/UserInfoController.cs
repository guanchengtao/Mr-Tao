using SDAU.GCT.OA.IBLL;
using SDAU.GCT.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDAU.GCT.OA.UI.Portal.Controllers
{
    public class UserInfoController :BaseController
    {
        // GET: UserInfo
        short delFlagNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllUserInfos()
        {
           
            int pageSize =Int32.Parse(Request["rows"] ?? "10");
            int pageIndex = Int32.Parse(Request["page"] ?? "1");
            int total = 0;
          
            string rchName = Request["rchName"];
            string rchRemark = Request["rchRemark"];
            var queryParam = new Model.Param.UserQueryParam
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                total = total,
                schName = rchName,
                schRemark = rchRemark
            };
            var pageData = UserInfoService.LoadPageData(queryParam);
          var temp=pageData.Select(u => new { u.Id, u.Uname, u.Pwd, u.Remark, u.ModfiedOn, u.Subtime, u.ShowName }).AsQueryable();

            //       .Select(u=>new {u.Id,u.Uname,u.Pwd,u.Remark,u.ModfiedOn,u.Subtime,u.ShowName });
            var data = new { queryParam.total, rows = temp.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);




        }
        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.Subtime = DateTime.Now;
            userInfo.ModfiedOn = DateTime.Now;
            userInfo.DelFlag = (short)Model.Enum.DelFlagEnum.Normal;
            UserInfoService.Add(userInfo);
            return Content("ok");

        }
        public ActionResult Delete(string ids)
        {    
            if(string .IsNullOrEmpty(ids))
            {
                return Content("请选择要删除的数据！！");
            }
            else
            {
                string[] deleteidlist = ids.Split(',');
                List<int> idlist = new List<int>();
                foreach (var id in deleteidlist)
                {
                    idlist.Add(Int32.Parse(id));
                }
                UserInfoService.DeleteListByLogicl(idlist);
                return Content("ok");
            }
        }
        public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault() as UserInfo;
            return View();
        }
        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfoService.Update(userInfo);
            return Content("ok");
        }

        public ActionResult setRole(int id)
        {
           UserInfo user= UserInfoService
                .GetEntities(u => u.Id == id).FirstOrDefault() as UserInfo;
            ViewBag.Uname = user.Uname;
            ViewBag.Id = user.Id;
            //获取所有的角色
            ViewBag.Allroles = RoleInfoService.GetEntities(u => u.DelFlag==delFlagNormal).ToList();
            //获取用户已经有的角色
            ViewBag.ExitsRoles = (from r in user.RoleInfo select r.Id).ToList();
            return View();
        }
        public ActionResult ProcessSetRole(int Uid)
        {
            //第一：当前用户id  --uid
            //第二：所有打上对勾的 角色。 ---> list
            List<int> setRoleIdList = new List<int>();
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    setRoleIdList.Add(roleId);
                }
            }

            UserInfoService.SetRole(Uid, setRoleIdList);
            return Content("ok");

        }



        public ActionResult setAction(int id)
        {
            ViewBag.User = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            //获取用户本身具备的权限
            var UserExistAction = R_UserInfo_ActionInfoService.GetEntities(r => r.UserInfoId == id && r.HasPermission == true && r.DelFlag == delFlagNormal).ToList();

            ViewBag.existAction = (from a in UserExistAction select a.Id).ToList();

            ViewData.Model = ActionInfoService.GetEntities(u => u.DelFlag == delFlagNormal).ToList();
            return View();
        }
        //设置用户特殊权限
        public ActionResult SetUserAction(int UId, int ActionId, int Value)
        {
            var ruseraction = R_UserInfo_ActionInfoService.GetEntities(u => u.ActionInfoId == ActionId && u.UserInfoId == UId).FirstOrDefault();

            if (ruseraction != null)
            {
                ruseraction.HasPermission = Value == 1 ? true : false;
                ruseraction.DelFlag = delFlagNormal;
                R_UserInfo_ActionInfoService.Update(ruseraction);
            }
            else
            {
                R_UserInfo_ActionInfo r_UserInfo_ActionInfo = new R_UserInfo_ActionInfo()
                {
                    ActionInfoId = ActionId,
                    UserInfoId = UId,
                    DelFlag = delFlagNormal,
                    HasPermission = Value == 1 ? true : false
                };
                R_UserInfo_ActionInfoService.Add(r_UserInfo_ActionInfo);
            }

            return Content("ok");
        }
        //删除特殊权限
        public ActionResult DeleteUserAction(int UId, int ActionId)
        {
            var ruseraction = R_UserInfo_ActionInfoService.GetEntities(u => u.ActionInfoId == ActionId && u.UserInfoId == UId).FirstOrDefault();

            if(ruseraction!=null)
            {
                //R_UserInfo_ActionInfoService.DeleteListByLogicl(new List<int> { ruseraction.Id });
                ruseraction.DelFlag = (short)Model.Enum.DelFlagEnum.Deleted;
                //一定要修改啊！！！保存修改！！光赋值有什么用啊！！？？
                R_UserInfo_ActionInfoService.Update(ruseraction);
            }

            return Content("ok");


        }
    }
}