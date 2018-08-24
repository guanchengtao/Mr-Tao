using SDAU.GCT.OA.IBLL;
using SDAU.GCT.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDAU.GCT.OA.UI.Portal.Controllers
{
    public class RoleInfoController :BaseController
    {
        // GET: RoleInfo
        short DelFlagNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IRoleInfoService RoleInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllRoleInfos()
        {
            int pageSize = Int32.Parse(Request["rows"] ?? "10");
            int pageIndex = Int32.Parse(Request["page"] ?? "1");
            int total = 0;
            var temp = RoleInfoService.GetTInfoPage(pageSize, pageIndex, out total,
                                              u => u.DelFlag == DelFlagNormal, u => u.Id, true);
            var tempData =
                temp.Select(
                    a =>
                    new
                    {
                        a.Id,
                        a.RoleName,
                        a.Remark,
                        a.Subtime,
                        a.ModfiedOn,
                        a.DelFlag,
                    });

            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = DelFlagNormal;
            roleInfo.ModfiedOn = DateTime.Now;
            roleInfo.Subtime = DateTime.Now;
            RoleInfoService.Add(roleInfo);
            return Content("ok");
        }

    }
}