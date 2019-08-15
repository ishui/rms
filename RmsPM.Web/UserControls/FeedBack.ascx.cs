namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.DAL.EntityDAO;
	using Rms.ORMap;
	using RmsPM.BLL ;

	/// <summary>
	///		FeedBack 的摘要说明。
	/// </summary>
	public partial class FeedBack : System.Web.UI.UserControl
	{

		//private string strType = "";
		protected System.Web.UI.WebControls.TextBox txtCode;
	
		public string FeedBackType 
		{
			set
			{
				//this.strType = value;
				this.ViewState["FeedBackType"] = value;
			}
			get
			{
				//return this.strType;
				if ( null!=this.ViewState["FeedBackType"] )
				{
					return this.ViewState["FeedBackType"].ToString();
				}
				return "";
			}
		}

		//private string strMasterCode = "";

		public string MasterCode
		{
			set
			{
				//this.strMasterCode = value;
				this.ViewState["MasterCode"] = value;
			}
			get
			{
				//return this.strMasterCode;
				if ( null!=this.ViewState["MasterCode"] )
				{
					return this.ViewState["MasterCode"].ToString();
				}
				return "";
			}
		}

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
				ApplicationLog.WriteLog(this.ToString(),ex,"反馈载入失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "反馈载入失败：" + ex.Message));
			}

		}

		private void InitPage()
		{}

		private void LoadData()
		{
			//EntityData	entity = WBSDAO.GetFeedBackByMasterCode(this.strMasterCode);
			EntityData	entity = WBSDAO.GetFeedBackByMasterCode(this.ViewState["MasterCode"].ToString());
			if(entity.HasRecord())
			{
				DataView dv = new DataView(entity.CurrentTable,"","CreateDate desc",System.Data.DataViewRowState.CurrentRows);				
				foreach(DataRowView drv in dv)
				{
					drv["Content"] = drv["Content"].ToString().Replace("\n","<br>");
				}
				this.rpFeedBack.DataSource = dv;
				this.rpFeedBack.DataBind();
			}
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.btSave.Click +=new EventHandler(btSave_Click);
			this.rpFeedBack.ItemCommand +=new RepeaterCommandEventHandler(rpFeedBack_ItemCommand);
		}
		//this.btSave.Click +=new EventHandler(btSave_Click);
		//this.rpFeedBack.ItemCommand +=new RepeaterCommandEventHandler(rpFeedBack_ItemCommand);
		#endregion

		private void btSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(this.txtFeedBack.Text.Trim().Length <= 0)
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "请输入反馈内容"));
					return;
				}

				//EntityData	entity = WBSDAO.GetFeedBackByMasterCode(this.strMasterCode);
				EntityData	entity = WBSDAO.GetFeedBackByMasterCode(this.ViewState["MasterCode"].ToString());
				DataRow dr = entity.GetNewRecord();
				dr["FeedBackCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("FeedBack");		
				//dr["FeedBackType"] = this.strType;
				dr["FeedBackType"] = this.ViewState["FeedBackType"];
				//dr["MasterCode"] = this.strMasterCode;
				dr["MasterCode"] = this.ViewState["MasterCode"];
				dr["Content"] = (this.txtFeedBack.Text.Length>4000)?this.txtFeedBack.Text.Substring(0,4000):this.txtFeedBack.Text;
				dr["Author"] = ((User)Session["User"]).UserCode;
				dr["CreateDate"] = DateTime.Now;
				entity.AddNewRecord(dr);
				WBSDAO.InsertFeedBack(entity);
				entity.Dispose();
				// 载入新数据
				this.LoadData();
				this.txtFeedBack.Text = "";
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"反馈保存失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "反馈保存失败：" + ex.Message));
			}
		}

		private void rpFeedBack_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			try
			{
				if(e.CommandName=="Delete")
				{
					LinkButton lbt = (LinkButton)e.Item.FindControl("lbtDelFeedBack");
					HtmlTableCell cell = (HtmlTableCell)e.Item.FindControl("DelCode");
					string strCode = cell.InnerText;
					EntityData entity = WBSDAO.GetFeedBackByCode(strCode);
					WBSDAO.DeleteFeedBack(entity);	
			
					this.LoadData();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"反馈删除失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "反馈删除失败：" + ex.Message));
			}
		}
	}
}
