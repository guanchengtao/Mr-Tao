 
using SDAU.GCT.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.IBLL
{
	
	 public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
        bool SetRole(int userId, List<int> roleInfoList);
    }
	
	 public partial interface IOrderInfoService:IBaseService<OrderInfo>
    {

    }
	
	 public partial interface IR_UserInfo_ActionInfoService:IBaseService<R_UserInfo_ActionInfo>
    {

    }
	
	 public partial interface IRoleInfoService:IBaseService<RoleInfo>
    {

    }
	
	 public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        IQueryable<UserInfo> LoadPageData(Model.Param.UserQueryParam userQueryParam);
        bool SetRole(int id, List<int> roleInfoList);
    }
	
	 public partial interface IUserInfoExtService:IBaseService<UserInfoExt>
    {

    }
}