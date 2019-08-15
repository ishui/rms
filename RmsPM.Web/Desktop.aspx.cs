using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using RmsPM.Web.Components;
using Rms.Web;
using RmsOA.BFL;
using System.Collections.Generic;

namespace RmsPM.Web
{
	/// <summary>
	/// Desktop ��ժҪ˵����
	/// 1,���������б�:unm,2004/10/21,version 1.1
	/// 2,�ҵĹ�ע�б�:unm,2004/10/21,version 1.0
	/// 3,�����еĹ���,���ڵĹ�����unm��2004/10/31,version 1.0
	/// 4,��ͬ�������õ����:unm , 2004/11/2,version 1.0
	/// 
	/// 2004/11/17 unm �޸������ѵĲ�ѯ����˵���ʾ��ʽ
	/// 200412/3 unm ���Ͷ�̬������ʱȡ��Ȩ��
	/// 
	/// 2005.4.4 unm ����������ʹ��;���������
	/// </summary>
	public partial class Desktop :  PageBase
	{ 

		/// <summary>
		/// ���Ĭ��ȡ�ü�¼��
		/// </summary>
		//private int intListAuditNum = 80;
// �ж������̾ͳ��������� 

		/// <summary>
		/// ����Ĭ��ȡ�ü�¼��
		/// </summary>
//		private int intListRemindNum = 4;
		

		/// <summary>
		/// ��Ŀ���
		/// </summary>
		//protected string projectCode = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			DefaultSet();
		}
		/// <summary>
		/// ��ʼ��ҳ���������
		/// </summary>
		private void InitPage()
		{
//			try
//			{
//				projectCode = Session["ProjectCode"].ToString();
//			}
//			catch(Exception ex)
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"�û�û��Ĭ����Ŀ");
//			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		private void DefaultSet()
		{
			GetDesktopMessage();
			IsAllowManage();
            if (this.up_sPMName.ToUpper() == "TIANYANGOA")
            {
                this.Control_rpnotice1.Visible = true;
            }
			this.InitPage();			
		}
		#region ��̬��ʾ����
		private void GetDesktopMessage()
		{	
			string station =BLL.SystemRule.GetStationListByUserCode(this.user.UserCode);			
			GetDesktopMessage(station,user.UserCode);				
		}
		private void GetDesktopMessage(string staticonID,string userid)
		{
			try
			{
				
				DataTable dt = StyleOperation.GetStationConfig(staticonID,userid);
				ShowDeskTop(dt);
			}
			catch(Exception ex)
			{
				Response.Write(JavaScript.Alert(true,ex.Message));			
			}
		}
		private void ShowDeskTop(DataTable dt)
		{
            List<string> list = new List<string>();
			foreach(DataRow dr in dt.Rows)
			{
                
				Control parent = Page.FindControl(dr["TableID"].ToString());
                if (ControlInDesktop(dr["ControlID"].ToString()))
                {
                    ShowControl newControl = new ShowControl(dr["ControlSrc"], dr["CacheTime"]);//(dr["ControlSrc"],dr["CacheTime"]);							
                    parent.Controls.Add(newControl);
                    parent.Controls.Add(new LiteralControl("<" + "br" + ">"));
                    parent.Visible = true;
                }
			}
		}
        public bool ControlInDesktop(string controlID)
        {
            string strDeskType = Request.QueryString["DesktopType"];
            DesktopType dkTT = DesktopType.OA;
            deskLabel.Text = "�칫��������";
            if (strDeskType.Equals("PM"))
            {
                dkTT = DesktopType.PM;
                deskLabel.Text = "��Ŀ��������";
            }
            List<string> listValue = DeskTopTypeBFL.GetIDCollectionByDesktopType(dkTT);
            foreach (string s in listValue)
            {
                if (s.Equals(controlID))
                {
                    return true;
                }
            }
            return false;
        }
	#endregion ��̬��ʾ����
		#region �Ƿ�����Ȩ�޸�������
		private void IsAllowManage()
		{
			string station =BLL.SystemRule.GetStationByUserCode(this.user.UserCode);
			if(station=="0")
			{
				IsAllow.Visible=true;
				IsAllow.Attributes.Add("onclick","OpenDesktopSet()");
				IsAllow.Attributes.Add("style","CURSOR: hand");	
				Lb_DesktopM.Attributes.Add("onclick","OpenDesktopManage()");
				Lb_DesktopM.Attributes.Add("style","CURSOR: hand");
			}
			else if(station=="")
			{
				Lb_DesktopM.Visible=false;
				IsAllow.Visible=false;				
			}
			else
			{
				Lb_DesktopM.Attributes.Add("onclick","OpenDesktopManage()");
				Lb_DesktopM.Attributes.Add("style","CURSOR: hand");
			}

            lblChangePWD.Attributes.Add("onclick", "doChangePWD()");
            lblChangePWD.Attributes.Add("style", "CURSOR: hand");
		}		
		#endregion

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		#region ���ú���
		/// <summary>
		/// ȡ��ָ���û����û���λ
		/// </summary>
		/// <param name="strUser"></param>
		/// <returns></returns>
		public string GetUserStation(string strUser)
		{
			string strRole = "";
			try
			{
				EntityData entityRole = DAL.EntityDAO.OBSDAO.GetStationByUserCode(strUser);
				for(int i=0;i<entityRole.CurrentTable.Rows.Count;i++)
				{
					if(strRole.Length>1)  strRole+=",";
					strRole+=entityRole.CurrentTable.Rows[i]["StationCode"].ToString();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ȡ���û���ɫʧ��");
			}
			return strRole;
		}

		/// <summary>
		/// �жϵ�ǰ�û��Ƿ���Ȩ��
		/// </summary>
		/// <returns></returns>
		public bool IsInRole()
		{
			User myUser = new User(user.UserCode);
			if(myUser.HasOperationRight("080101"))// 080101Ϊ֪ͨ����Ȩ��
				return true;
			else
				return false;
		}
		#endregion
	}
}
