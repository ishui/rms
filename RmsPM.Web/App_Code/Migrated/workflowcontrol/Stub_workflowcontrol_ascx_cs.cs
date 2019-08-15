//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分生成的。
// 此代码文件“App_Code\Migrated\workflowcontrol\Stub_workflowcontrol_ascx_cs.cs”已创建，其中包含一个抽象类 
//，该类在文件“workflowcontrol\workflowcontrol.ascx.cs”中用作类“Migrated_WorkFlowControl”的基类。
// 此项允许您的项目中的所有代码文件引用该基类。
// 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================




namespace RmsPM.Web.WorkFlowControl
 {

	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Text;
	using System.Collections;
	using RmsPM.BLL;
	using RmsPM.DAL.EntityDAO;
	using RmsPM.DAL.QueryStrategy;
	using RmsPM.Web;
	using Rms.ORMap;
	using Rms.WorkFlow;

abstract public class WorkFlowControl :  System.Web.UI.UserControl
{
		abstract public User IUser
		{
		  get;
		}
		abstract public string ActCode
		{
		  get;
		  set;
		}
		abstract public string ApplicationCode
		{
		  get;
		  set;
		}
		abstract public string SelectRouterUrl
		{
		  get;
		  set;
		}
		abstract public Hashtable TaskActorHashtable
		{
		  get;
		  set;
		}
	abstract public void ControlDataBind();


}



}
