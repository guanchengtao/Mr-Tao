
using SDAU.GCT.OA.Dal;
using SDAU.GCT.OA.DALFactory;
using SDAU.GCT.OA.IBLL;
using SDAU.GCT.OA.IDal;
using SDAU.GCT.OA.Model;
using SDAU.GCT.OA.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace SDAU.GCT.OA.BLL
{
	
	  public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
	  public override void GetCurrentDal()
        {
            CurrentDal =DbSession.ActionInfoDal;
        }
        public bool SetRole(int userId, List<int> roleInfoList)
        {
            var action = DbSession.ActionInfoDal.GetEntities(u => u.Id == userId).FirstOrDefault();
            action.RoleInfo.Clear();//全剁掉。

            var allRoles = DbSession.RoleInfoDal.GetEntities(r => roleInfoList.Contains(r.Id));
            foreach (var roleInfo in allRoles)
            {
                action.RoleInfo.Add(roleInfo);//加最新的角色。
            }
            DbSession.Savechanges();
            return true;


        }
    }
	
	  public partial class OrderInfoService : BaseService<OrderInfo>, IOrderInfoService
    {
	  public override void GetCurrentDal()
        {
            CurrentDal =DbSession.OrderInfoDal;
        }
		}
	
	  public partial class R_UserInfo_ActionInfoService : BaseService<R_UserInfo_ActionInfo>, IR_UserInfo_ActionInfoService
    {
	  public override void GetCurrentDal()
        {
            CurrentDal =DbSession.R_UserInfo_ActionInfoDal;
        }
		}
	
	  public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
	  public override void GetCurrentDal()
        {
            CurrentDal =DbSession.RoleInfoDal;
        }
		}
	
	  public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
	  public override void GetCurrentDal()
        {
            CurrentDal =DbSession.UserInfoDal;
        }

        public IQueryable<UserInfo> LoadPageData(UserQueryParam userQueryParam)
        {
            short DelFlagNormal =(short) Model.Enum.DelFlagEnum.Normal;
            var temp = DbSession.UserInfoDal.GetEntities(u => u.DelFlag == DelFlagNormal);
            if(!string.IsNullOrEmpty(userQueryParam.schName))
            {
                temp = temp.Where(u => u.Uname.Contains(userQueryParam.schName));
            }
            if (!string.IsNullOrEmpty(userQueryParam.schRemark))
            {
                temp = temp.Where(u => u.Remark.Contains(userQueryParam.schRemark));
            }
         userQueryParam.total = temp.Count();
            return temp.OrderBy(u => u.Id)
                 .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                 .Take(userQueryParam.PageSize).AsQueryable();
        }

        public bool SetRole(int userId, List<int> roleInfoList)
        {
            var user = DbSession.UserInfoDal.GetEntities(u => u.Id == userId).FirstOrDefault();
            user.RoleInfo.Clear();//全剁掉。

            var allRoles = DbSession.RoleInfoDal.GetEntities(r => roleInfoList.Contains(r.Id));
            foreach (var roleInfo in allRoles)
            {
                user.RoleInfo.Add(roleInfo);//加最新的角色。
            }
            DbSession.Savechanges();
            return true;


        }
    }
	
	  public partial class UserInfoExtService : BaseService<UserInfoExt>, IUserInfoExtService
    {
	  public override void GetCurrentDal()
        {
            CurrentDal =DbSession.UserInfoExtDal;
        }
		}
}