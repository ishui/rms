//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\oa\bill\Stub_enquipmentuse_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���oa\bill\enquipmentuse.ascx.cs���������ࡰMigrated_EnquipmentUse���Ļ��ࡣ
// ��������������Ŀ�е����д����ļ����øû��ࡣ
// �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
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
