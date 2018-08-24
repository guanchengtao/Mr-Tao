 
using SDAU.GCT.OA.Dal;
using SDAU.GCT.OA.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.DALFactory
{
   public partial class DbSession:IDbSession
    { 
	  
	public IActionInfoDal ActionInfoDal { get
            {
                return StaticDalFactory.getActionInfoDal();
            }
        }
	  
	public IOrderInfoDal OrderInfoDal { get
            {
                return StaticDalFactory.getOrderInfoDal();
            }
        }
	  
	public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal { get
            {
                return StaticDalFactory.getR_UserInfo_ActionInfoDal();
            }
        }
	  
	public IRoleInfoDal RoleInfoDal { get
            {
                return StaticDalFactory.getRoleInfoDal();
            }
        }
	  
	public IUserInfoDal UserInfoDal { get
            {
                return StaticDalFactory.getUserInfoDal();
            }
        }
	  
	public IUserInfoExtDal UserInfoExtDal { get
            {
                return StaticDalFactory.getUserInfoExtDal();
            }
        }
}
}