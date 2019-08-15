//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分生成的。
// 此代码文件“App_Code\Migrated\usercontrols\Stub_inputuser_ascx_cs.cs”已创建，其中包含一个抽象类 
//，该类在文件“usercontrols\inputuser.ascx.cs”中用作类“Migrated_InputUser”的基类。
// 此项允许您的项目中的所有代码文件引用该基类。
// 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
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
