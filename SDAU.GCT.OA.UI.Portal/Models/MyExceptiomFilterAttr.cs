using SDAU.GCT.OA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDAU.GCT.OA.UI.Portal.Models
{
    public class MyExceptiomFilterAttr: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);


            ////直接把错误信息写到日志里面去
            LogHelper.WriteLog(filterContext.Exception.ToString());

        }
    }
}