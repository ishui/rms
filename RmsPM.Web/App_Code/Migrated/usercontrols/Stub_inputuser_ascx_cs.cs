//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\usercontrols\Stub_inputuser_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���usercontrols\inputuser.ascx.cs���������ࡰMigrated_InputUser���Ļ��ࡣ
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

abstract public class InputUser :  System.Web.UI.UserControl
{
		abstract public string ProjectCode
		{
		  get;
		  set;
		}
		abstract public string Value 
		{
		  get;
		  set;
		}
		abstract public string Text 
		{
		  get;
		}
		abstract public string Hint 
		{
		  get;
		}
		abstract public string ImagePath
		{
		  get;
		  set;
		}


}



}
