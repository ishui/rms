//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\workflowoperation\Stub_checkcontrol_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���workflowoperation\checkcontrol.ascx.cs���������ࡰMigrated_CheckControl���Ļ��ࡣ
// ��������������Ŀ�е����д����ļ����øû��ࡣ
// �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================




namespace RmsPM.Web.WorkFlowOperation
 {

	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.Web.WorkFlowControl;

abstract public class CheckControl :  System.Web.UI.UserControl
{
		abstract public string Result
		{
		  get;
		  set;
		}
		abstract public ModuleState State
		{
		  get;
		  set;
		}
	abstract public void InitControl();
	abstract public void LoadData();


}



}
