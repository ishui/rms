//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\usercontrols\Stub_ucduty_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���usercontrols\ucduty.ascx.cs���������ࡰMigrated_UCDuty���Ļ��ࡣ
// ��������������Ŀ�е����д����ļ����øû��ࡣ
// �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================




namespace RmsPM.Web.UserControls
 {

	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using RmsPM.DAL.EntityDAO;

abstract public class UCDuty :  System.Web.UI.UserControl
{
		abstract public string CtrlPath
		{
		  set;
		}
		abstract public string Value
		{
		  get;
		  set;
		}


}



}
