//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分生成的。
// 此代码文件“App_Code\Migrated\oa\bill\Stub_enquipmentuse_ascx_cs.cs”已创建，其中包含一个抽象类 
//，该类在文件“oa\bill\enquipmentuse.ascx.cs”中用作类“Migrated_EnquipmentUse”的基类。
// 此项允许您的项目中的所有代码文件引用该基类。
// 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================




namespace RmsPM.Web.OA.Bill
 {

	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using RmsPM.Web.WorkFlowControl;

abstract public class EnquipmentUse :  RmsPM.Web.WorkFlowControl.WorkFlowControlClassBase
{
	abstract public void InitControl();
	abstract public EntityData SaveData(string unit,string apuser);


}



}
