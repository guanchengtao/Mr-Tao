 
using SDAU.GCT.OA.Dal;
using SDAU.GCT.OA.IDal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.DALFactory
{
    public partial class StaticDalFactory
    {
        public static string assemblyname = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
	  
		  public static IActionInfoDal getActionInfoDal()
        {      
        return Assembly.Load(assemblyname).CreateInstance(assemblyname+".ActionInfoDal") as IActionInfoDal;
        }
	  
		  public static IOrderInfoDal getOrderInfoDal()
        {      
        return Assembly.Load(assemblyname).CreateInstance(assemblyname+".OrderInfoDal") as IOrderInfoDal;
        }
	  
		  public static IR_UserInfo_ActionInfoDal getR_UserInfo_ActionInfoDal()
        {      
        return Assembly.Load(assemblyname).CreateInstance(assemblyname+".R_UserInfo_ActionInfoDal") as IR_UserInfo_ActionInfoDal;
        }
	  
		  public static IRoleInfoDal getRoleInfoDal()
        {      
        return Assembly.Load(assemblyname).CreateInstance(assemblyname+".RoleInfoDal") as IRoleInfoDal;
        }
	  
		  public static IUserInfoDal getUserInfoDal()
        {      
        return Assembly.Load(assemblyname).CreateInstance(assemblyname+".UserInfoDal") as IUserInfoDal;
        }
	  
		  public static IUserInfoExtDal getUserInfoExtDal()
        {      
        return Assembly.Load(assemblyname).CreateInstance(assemblyname+".UserInfoExtDal") as IUserInfoExtDal;
        }
}
}