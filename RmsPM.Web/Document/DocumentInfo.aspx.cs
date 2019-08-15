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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Document
{
	/// <summary>
	/// DocumentInfo 的摘要说明。
	/// </summary>
	public partial class DocumentInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSave;
		protected System.Web.UI.WebControls.Label lblDocumentType;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)	
			{
				IniPage();
				LoadData();
			}

			this.myAttachMentList.AttachMentType = "DocumentAttach";
			this.myAttachMentList.MasterCode = this.txtDocumentCode.Value;
		}

		private void IniPage() 
		{
			try 
			{
//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtDocumentCode.Value = Request.QueryString["DocumentCode"];
				this.txtAct.Value = Request.QueryString["Action"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string code = this.txtDocumentCode.Value;

				if (code != "") 
				{
					//权限
					ArrayList ar = user.GetResourceRight(code,"Document");
					if ( ! ar.Contains("100101"))
					{
						Response.Redirect( "../RejectAccess.aspx?OperationCode=100101" );
						Response.End();
					}

					this.btnModify.Visible = ar.Contains("100103");
					this.btnDelete.Visible = ar.Contains("100104");
                    this.btnCheck.Visible = ar.Contains("100105");

					EntityData entity = RmsPM.DAL.EntityDAO.DocumentDAO.GetV_DocumentByCode (code);
					if (entity.HasRecord())
					{
                        this.lblTitle.Text = entity.GetString("Title");
                        this.lblDocumentID.Text = entity.GetString("DocumentID");
                        this.lblAuthor.Text = entity.GetString("Author");
                        this.lblGroupName.Text = BLL.SystemGroupRule.GetSystemGroupFullName(entity.GetString("GroupCode"));
                        //this.lblMainText.Text = entity.GetString("MainText").Replace("\n", "<br>");
                        this.divMainText.InnerHtml = HttpUtility.HtmlDecode(entity.GetString("HtmlMainText"));

                        this.txtStatus.Value = entity.GetInt("Status").ToString();
                        this.lblStatusName.Text = entity.GetString("StatusName");

                        this.lblCreatePersonName.Text = entity.GetString("CreatePersonName");
                        this.lblCreateDate.Text = entity.GetDateTimeOnlyDate("CreateDate");
                        this.lblModifyPersonName.Text = entity.GetString("ModifyPersonName");
                        this.lblModifyDate.Text = entity.GetDateTimeOnlyDate("ModifyDate");
                        this.lblCheckPersonName.Text = entity.GetString("CheckPersonName");
                        this.lblCheckDate.Text = entity.GetDateTimeOnlyDate("CheckDate");
                        this.KeeperLabel.Text = entity.GetString("Keeper");
                        this.FileKindLabel.Text = entity.GetString("FileKind");
                        this.FileDateLabel.Text = entity.GetDateTimeOnlyDate("FileDate");
                        this.SavePlaceLabel.Text = BLL.SystemRule.GetUnitFullName(entity.GetString("UnitCode")) + " " + entity.GetString("SavePlace");
                        this.SaveDateLabel.Text = entity.GetDateTimeOnlyDate("SaveDate");
                        this.RemarkLabel.Text = entity.GetString("Remark");
                        this.Counts.Text = entity.GetString("Counts");
//                        this.lblFixDocumentTypeName.Text = BLL.ConvertRule.Concat(entity.Tables["DocumentConfig"], "DocumentTypeName", ",", "fixed=1");

//						this.lblUnFixDocumentTypeName.Text = BLL.ConvertRule.Concat(entity.Tables["DocumentConfig"], "DocumentTypeName", ",", "fixed is null or fixed=0");
						//LoadAttachList(entity.Tables["Attachment"]);
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "文档不存在"));
						return;
					}
					entity.Dispose();

                    switch (this.txtStatus.Value)
                    {
                        case "1"://已审
                            this.btnModify.Style["display"] = "none";
                            this.btnDelete.Style["display"] = "none";
                            this.btnCheck.Style["display"] = "none";

                            this.btnModifyEx.Style["display"] = "";

                            break;

                        default://未审
                            break;
                    }
                }
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
			}
		}

//		/// <summary>
//		/// 显示附件列表
//		/// </summary>
//		/// <param name="tbAttach"></param>
//		private void LoadAttachList(DataTable tbAttach) 
//		{
//			try 
//			{
//				this.dgAttach.DataSource = tbAttach;
//				this.dgAttach.DataBind();
//			}
//			catch(Exception ex)
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"");
//				Response.Write(Rms.Web.JavaScript.Alert(true, "显示附件列表出错：" + ex.Message));
//			}
//		}

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

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string code = this.txtDocumentCode.Value;
				BLL.DocumentRule.Instance().DeleteDocument(code);

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.OpenerReload(false));
//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
	}
}
