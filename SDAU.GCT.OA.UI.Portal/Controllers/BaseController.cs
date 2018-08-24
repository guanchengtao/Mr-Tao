using SDAU.GCT.OA.IBLL;
using SDAU.GCT.OA.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDAU.GCT.OA.UI.Portal.Controllers
{
    //抽象出一个基类，让所有的Controller继承此控制器
    public class BaseController:Controller
    {
        public bool IsCheck = true;
        public UserInfo LoginUserInfo { get; set; }
        //在当前控制器所有方法执行之前执行此代码
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //登录时不需要验证是否登录
            //#region 测试信息
            ////TODO:测试结束后删除
            //return; 
            //#endregion
            if (IsCheck)
            {
                //从mm缓存中读取数据
                if(Request.Cookies["loginuserId"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }

                string userGuid = Request.Cookies["loginuserId"].Value.ToString();
                UserInfo user = Common.Cache.CacheHelper.GetCache(userGuid) as UserInfo;
                //用户长时间不进行操作，超时了
                if (user==null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                LoginUserInfo = user;
                //设置滑动窗口机制,一旦登陆了，就给当前用户+20min
                Common.Cache.CacheHelper.SetCache(userGuid,user,DateTime.Now.AddMinutes(20));

                //给admin留后门,首页查询权限之后直接显示图标
                if (LoginUserInfo.Uname == "admin") return;
                else
                {
                    string url = Request.Url.AbsolutePath.ToLower();
                    string httpMethod = Request.HttpMethod.ToLower();

                    //通过一个容器创建对象
                    IApplicationContext ctx = ContextRegistry.GetContext();

                    IActionInfoService actionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
                    IR_UserInfo_ActionInfoService r_UserInfo_ActionInfoService = ctx.GetObject("R_UserInfo_ActionInfoService") as IR_UserInfo_ActionInfoService;

                    IUserInfoService userInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
                    var actionInfo=//拿到当前请求对应的权限
                        actionInfoService.GetEntities(u => u.Url.ToLower() == url && u.HttpMethod.ToLower() == httpMethod).FirstOrDefault();
                    if(actionInfo==null)
                    {
                        Response.Redirect("/Error.html");
                    }
                    
                    #region 第一条线
                    var action = r_UserInfo_ActionInfoService.GetEntities(u => u.UserInfoId == LoginUserInfo.Id);

                    var item = (from s in action
                                where s.ActionInfoId == actionInfo.Id
                                select s).FirstOrDefault();
                    if (item != null)
                    {
                        if (item.HasPermission == true)
                        {
                            return;
                        }
                        else
                        {
                            Response.Redirect("/Error.html");
                        }
                    }
                    #endregion

                    #region 第二条线
                    var userinfo = userInfoService.GetEntities(u => u.Id == LoginUserInfo.Id).FirstOrDefault();

                    //拿到所有角色
                    var roles = from r in userinfo.RoleInfo
                                select r;
                    //拿到所有角色对应的权限
                    var actions = from r in roles
                                  from a in r.ActionInfo
                                  select a;
                    //当前权限是否在角色对应的权限集合中
                    var temp = (from a in actions
                                where a.Id == actionInfo.Id
                                select a).Count();
                    if (temp <= 0)
                    {
                        Response.Redirect("/Error.html");
                    } 
                    #endregion
                }
            }
        }
    }
}