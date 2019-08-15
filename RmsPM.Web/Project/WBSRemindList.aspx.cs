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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;


namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSRemindList 的摘要说明。
	/// </summary>
	public partial class WBSRemindList : System.Web.UI.Page
	{

	
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
				ApplicationLog.WriteLog(this.ToString(),ex,"获取提醒列表失败");
			}
		}

		private void InitPage()
		{
			this.lblTitle.Text = "提醒列表";
			string strType = Request["Type"] + "";
			if(!this.IsPostBack)
			{
				this.dtbStartDate.Value = "";
				this.dtbEndDate.Value = "";

				// 载入提醒定义
				int i=0;			
				foreach(string[] arType in ComSource.arRemind)
				{
					this.ddlRemindType.Items.Add(new ListItem(arType[1],arType[0]));
					if(!this.IsPostBack&&strType==arType[0]) this.ddlRemindType.SelectedIndex = i+1; 
					i++;
				}
			}			
		}

		private void LoadData()
		{
			RemindObjectStrategyBuilder rsb = new RemindObjectStrategyBuilder();
			rsb.AddStrategy( new Strategy( DAL.QueryStrategy.RemindObjectName.User,((User)Session["User"]).UserCode));		
			if(this.txtRemindMessage.Value.Trim().Length>0)
				rsb.AddStrategy( new Strategy( DAL.QueryStrategy.RemindObjectName.Message,this.txtRemindMessage.Value));
			if(this.ddlRemindType.SelectedIndex>0)
				rsb.AddStrategy( new Strategy( DAL.QueryStrategy.RemindObjectName.Type,this.ddlRemindType.SelectedValue));
			if(this.dtbStartDate.Value!="")
				rsb.AddStrategy( new Strategy( DAL.QueryStrategy.RemindObjectName.CreateDate,this.dtbStartDate.Value));
			if(this.dtbEndDate.Value!="")
				rsb.AddStrategy( new Strategy( DAL.QueryStrategy.RemindObjectName.EndDate,this.dtbEndDate.Value));
			rsb.AddOrder("CreateDate",false);
			string sql = rsb.BuildMainQueryString();
			QueryAgent qa = new QueryAgent();
			EntityData entityRemind = qa.FillEntityData("RemindObject",sql);
			qa.Dispose();
			this.dgRemindList.DataSource = DisposeRemind(entityRemind.CurrentTable);
			this.dgRemindList.DataBind();
			this.trNoRemind.Visible = (this.dgRemindList.Items.Count>0)?false:true;
		}

		private DataTable DisposeRemind(DataTable dt)
		{
			DataTable dtNew = dt.Copy();			
			dtNew.Columns.Add("RemindType");
			dtNew.Columns.Add("Url");

			for(int i=0;i<dtNew.Rows.Count;i++)
			{
				string Img = "";
				if(dtNew.Rows[i]["IsDesk"].ToString()=="1")
					Img = "<img src=../images/ToolsHistory.gif>";
				// 提醒类别
				if(dtNew.Rows[i]["Type"].ToString().Substring(0,1)=="0") 
				{					
					dtNew.Rows[i]["RemindType"]="工作提醒"+Img;
					dtNew.Rows[i]["Url"] = "WBSInfo.aspx?WBSCode="+dtNew.Rows[i]["MasterCode"].ToString()+"&Type=0";
				}
				if(dtNew.Rows[i]["Type"].ToString().Substring(0,1)=="1") 
				{
					dtNew.Rows[i]["RemindType"]="应付未付提醒"+Img;
					dtNew.Rows[i]["Url"] = "../Finance/PaymentList.aspx?Status=0&ProjectCode="+Session["ProjectCode"].ToString();
				}
				if(dtNew.Rows[i]["Type"].ToString().Substring(0,1)=="2") 
				{
					dtNew.Rows[i]["RemindType"]="工程计划提醒"+Img;
					dtNew.Rows[i]["Url"] = "../Construct/ConstructPlanInfo.aspx?PBSUnitCode="+dtNew.Rows[i]["MasterCode"].ToString();
				}
				if(dtNew.Rows[i]["Type"].ToString().Substring(0,1)=="3") 
				{
					dtNew.Rows[i]["RemindType"]="工作变更提醒"+Img;					
					dtNew.Rows[i]["Url"] = "WBSInfo.aspx?WBSCode="+dtNew.Rows[i]["MasterCode"].ToString()+"&Type=3";
				}
				if(dtNew.Rows[i]["Type"].ToString()=="41") 
				{
					dtNew.Rows[i]["RemindType"]="待办事项提醒"+Img;					
					dtNew.Rows[i]["Url"] = "../oa/PendingTask/View.aspx?OAWaitTaskCode="+dtNew.Rows[i]["MasterCode"].ToString()+"&Type=41";
				}
			}
			return dtNew;
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
			this.dgRemindList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgRemindList_DeleteCommand);

		}
		#endregion

		private void dgRemindList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string strKey = this.dgRemindList.DataKeys[(int)e.Item.ItemIndex].ToString();
				EntityData entity = RemindDAO.GetRemindObjectByCode(strKey);
				RemindDAO.DeleteRemindObject(entity);
				entity.Dispose();
				this.LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"获取提醒列表失败");
			}
		}

		
	}
}
