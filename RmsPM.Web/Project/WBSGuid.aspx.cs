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
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSGuid 的摘要说明。
	/// 指示明细列表，包括批示
	/// <author>unm</author>
	/// <date>2004/11/10</date>
	/// <version>1.0</version>
	/// </summary>
	public partial class WBSGuid : System.Web.UI.Page
	{
		private string strTaskGuidCode = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// 在此处放置用户代码以初始化页面
				InitPage();
				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"载入指示列表失败");
			}
		}

		/// <summary>
		/// 页面初始化
		/// </summary>
		private void InitPage()
		{
			this.strTaskGuidCode = Request["TaskGuidCode"]+"";
			ViewState["WBSCode"] = Request["WBSCode"]+"";
			this.lblTitle.Text = "工作批示";

			CheckRole();
		}

		/// <summary>
		/// 载入数据
		/// </summary>
		private void LoadData()
		{
			EntityData entity = WBSDAO.GetTaskGuidByCode(strTaskGuidCode);
			if(entity.HasRecord())
			{
				this.tdGuidMan.InnerText =RmsPM.BLL.SystemRule.GetUserName(entity.GetString("TaskGuidPerson")); 
				this.tdDate.InnerText = entity.GetDateTimeOnlyDate("CreateDate");
				this.tdContent.InnerText = entity.GetString("TaskGuidContent");
			}
		
//			string strUsers = "";
//			string strUserNames = "";
//			string strStations = "";
//			string strStationNames = "";
//			BLL.ResourceRule.GetAccessRange(strTaskGuidCode,"0701","070109",ref strUsers,ref strUserNames,ref strStations,ref strStationNames);
			this.lblUser.Text = this.GetGuidUser((string)ViewState["WBSCode"]);//strUserNames + "&nbsp;&nbsp;"+strStationNames;
		}

		private void CheckRole()
		{
			// 检查权限
			User user = (User)Session["User"];
			if(!user.HasResourceRight(this.strTaskGuidCode,"070402"))
			{
				Response.Redirect( "../RejectAccess.aspx" );
				Response.End();
			}
		}

		private string GetGuidUser(string txtWBSCode)
		{			
			string SelectName = "";
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(txtWBSCode);
			if (entityUser.HasRecord())
			{
				DataTable dtUserNew = entityUser.CurrentTable.Copy();					
				
				for (int i = 0; i < dtUserNew.Rows.Count; i++)
				{
					if (dtUserNew.Rows[i]["Type"].ToString() == "5"&&dtUserNew.Rows[i]["ExecuteCode"].ToString()==this.strTaskGuidCode) // 分发对象，指示人员
					{
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 类型为人
						{
							SelectName += (SelectName.Length>0)?",":"";
							SelectName += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
						}
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // 类型为岗位
						{
							SelectName += (SelectName.Length>0)?",":"";
							SelectName += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
						}
					}
				}
			}
			entityUser.Dispose();
			return SelectName;
		}


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
	}
}
