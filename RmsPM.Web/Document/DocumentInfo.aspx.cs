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
	/// DocumentInfo ��ժҪ˵����
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string code = this.txtDocumentCode.Value;

				if (code != "") 
				{
					//Ȩ��
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
						Response.Write(Rms.Web.JavaScript.Alert(true, "�ĵ�������"));
						return;
					}
					entity.Dispose();

                    switch (this.txtStatus.Value)
                    {
                        case "1"://����
                            this.btnModify.Style["display"] = "none";
                            this.btnDelete.Style["display"] = "none";
                            this.btnCheck.Style["display"] = "none";

                            this.btnModifyEx.Style["display"] = "";

                            break;

                        default://δ��
                            break;
                    }
                }
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
			}
		}

//		/// <summary>
//		/// ��ʾ�����б�
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
//				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�����б����" + ex.Message));
//			}
//		}

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

		/// <summary>
		/// ɾ��
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
				Response.Write(JavaScript.Alert(true, "ɾ��ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// ����
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
