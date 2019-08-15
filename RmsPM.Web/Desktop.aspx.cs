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
	/// Desktop 的摘要说明。
	/// 1,工作报告列表:unm,2004/10/21,version 1.1
	/// 2,我的关注列表:unm,2004/10/21,version 1.0
	/// 3,进行中的工作,延期的工作：unm，2004/10/31,version 1.0
	/// 4,合同，请款，费用的审核:unm , 2004/11/2,version 1.0
	/// 
	/// 2004/11/17 unm 修改了提醒的查询，审核的显示方式
	/// 200412/3 unm 请款和动态费用暂时取消权限
	/// 
	/// 2005.4.4 unm 添加其他类型待和决单据链接
	/// </summary>
	public partial class Desktop :  PageBase
	{ 

		/// <summary>
		/// 审核默认取得记录数
		/// </summary>
		//private int intListAuditNum = 80;
// 有多少流程就出多少流程 

		/// <summary>
		/// 提醒默认取得记录数
		/// </summary>
//		private int intListRemindNum = 4;
		

		/// <summary>
		/// 项目编号
		/// </summary>
		//protected string projectCode = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			DefaultSet();
		}
		/// <summary>
		/// 初始化页面基本数据
		/// </summary>
		private void InitPage()
		{
//			try
//			{
//				projectCode = Session["ProjectCode"].ToString();
//			}
//			catch(Exception ex)
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"用户没有默认项目");
//			}
		}

		/// <summary>
		/// 载入数据
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
		#region 动态显示桌面
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
            deskLabel.Text = "办公管理桌面";
            if (strDeskType.Equals("PM"))
            {
                dkTT = DesktopType.PM;
                deskLabel.Text = "项目管理桌面";
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
	#endregion 动态显示桌面
		#region 是否显有权限更改桌面
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

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		#region 公用函数
		/// <summary>
		/// 取得指定用户的用户岗位
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
				ApplicationLog.WriteLog(this.ToString(),ex,"取得用户角色失败");
			}
			return strRole;
		}

		/// <summary>
		/// 判断当前用户是否有权限
		/// </summary>
		/// <returns></returns>
		public bool IsInRole()
		{
			User myUser = new User(user.UserCode);
			if(myUser.HasOperationRight("080101"))// 080101为通知新增权限
				return true;
			else
				return false;
		}
		#endregion
	}
}
