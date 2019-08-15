//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\workflowcontrol\Stub_workflowopinion_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���workflowcontrol\workflowopinion.ascx.cs���������ࡰMigrated_WorkFlowOpinion���Ļ��ࡣ
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
	using Rms.ORMap;
	using RmsPM.Web.WorkFlowControl;

abstract public class WorkFlowOpinion :  RmsPM.Web.WorkFlowControl.WorkFlowControlClassBase
{
		abstract public string OpinionType
		{
		  get;
		  set;
		}
		abstract public string OpinionName
		{
		  get;
		  set;
		}
		abstract public bool IsTextBox
		{
		  get;
		  set;
		}
		abstract public string Value
		{
		  get;
		  set;
		}
		abstract public bool DisabledText
		{
		  get;
		  set;
		}
		abstract public ModuleState DISPLAY
		{
		  set;
		}
	abstract public void InitControl();
	abstract public void SubmitData();
	abstract public void DeleteData();


}



}
