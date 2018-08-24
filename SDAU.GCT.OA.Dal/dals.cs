 
using SDAU.GCT.OA.IDal;
using SDAU.GCT.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SDAU.GCT.OA.Dal
{
	
	 public partial class ActionInfoDal : BaseDal<ActionInfo>, IActionInfoDal
	 {
	 }
	
	 public partial class OrderInfoDal : BaseDal<OrderInfo>, IOrderInfoDal
	 {
	 }
	
	 public partial class R_UserInfo_ActionInfoDal : BaseDal<R_UserInfo_ActionInfo>, IR_UserInfo_ActionInfoDal
	 {
	 }
	
	 public partial class RoleInfoDal : BaseDal<RoleInfo>, IRoleInfoDal
	 {
	 }
	
	 public partial class UserInfoDal : BaseDal<UserInfo>, IUserInfoDal
	 {
	 }
	
	 public partial class UserInfoExtDal : BaseDal<UserInfoExt>, IUserInfoExtDal
	 {
	 }
}