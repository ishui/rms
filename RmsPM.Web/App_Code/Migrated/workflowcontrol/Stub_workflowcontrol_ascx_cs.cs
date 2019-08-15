//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\workflowcontrol\Stub_workflowcontrol_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���workflowcontrol\workflowcontrol.ascx.cs���������ࡰMigrated_WorkFlowControl���Ļ��ࡣ
// ��������������Ŀ�е����д����ļ����øû��ࡣ
// �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
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
