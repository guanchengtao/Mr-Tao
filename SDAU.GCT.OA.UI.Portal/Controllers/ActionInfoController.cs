
using SDAU.GCT.OA.IBLL;
using SDAU.GCT.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDAU.GCT.OA.UI.Portal.Controllers
{
    public class ActionInfoController : BaseController
    {
        // GET: ActionInfo
        short DelFlagNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IActionInfoService ActionInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public ActionResult Index()
        {        
            return View();
        }
        public ActionResult GetAllActionInfos()
        {
            int pageSize = Int32.Parse(Request["rows"] ?? "10");
            int pageIndex = Int32.Parse(Request["page"] ?? "1");
            int total = 0;
            var temp = ActionInfoService.GetTInfoPage(pageSize, pageIndex, out total,
                                              u => u.DelFlag == DelFlagNormal, u => u.Id, true);
            var tempData =
                temp.Select(
                    a =>
                    new
                    {
                        a.Id,
                        a.IsMenu,
                        a.Url,
                        a.Remark,
                        a.Sort,
                        a.Subtime,
                        a.ModfiedOn,
                        a.HttpMethod,
                        a.MenuIcon,
                        a.ActionName
                    });

            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Add(ActionInfo actionInfo)
        {
            actionInfo.Subtime = DateTime.Now;
            actionInfo.ModfiedOn = DateTime.Now;
            actionInfo.DelFlag = DelFlagNormal;
            actionInfo.IsMenu = Request["IsMenu"] == "true" ? true : false;
            ActionInfoService.Add(actionInfo);
            return Content("ok");
            
        }
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
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
                ActionInfoService.DeleteListByLogicl(idlist);
                return Content("ok");
            }
        }
        public ActionResult Edit(int id)
        {
            ViewData.Model = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault() as ActionInfo;
            return View();
        }
        [HttpPost]
        public ActionResult Edit(ActionInfo actionInfo)
        {
            ActionInfoService.Update(actionInfo);
            return Content("ok");
        }
        #region 上传图片处理
        public ActionResult UploadImage()
        {
            string path = "ok";
            HttpPostedFileBase file = Request.Files["fileMenuIcon"];
            if ( file!=null)
            {
                 path = "/UploadFiles/UploadImage/" + Guid.NewGuid().ToString() + "-" + file.FileName;
                file.SaveAs(Request.MapPath(path));
            }      
           return Content(path);
        }
        #endregion
        public ActionResult setRole(int id)
        {
            ActionInfo user = ActionInfoService
                 .GetEntities(u => u.Id == id).FirstOrDefault() as ActionInfo;
            ViewBag.Uname = user.ActionName;
            ViewBag.Id = user.Id;
            //获取所有的角色
            ViewBag.Allroles = RoleInfoService.GetEntities(u => u.DelFlag == DelFlagNormal).ToList();
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

            ActionInfoService.SetRole(Uid, setRoleIdList);
            return Content("ok");

        }

    }
}