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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// RoomModel ��ժҪ˵����
	/// </summary>
	public partial class RoomModel : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputText txtPBSTypeName;
		protected System.Web.UI.UserControl ucPBSTypeTree;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)	
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtModelCode.Value = Request.QueryString["ModelCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtAct.Value = Request.QueryString["Act"];

				if (this.txtAct.Value.ToLower() == "view") 
				{
					this.trTool.Style["display"] = "none";
					this.trBottom1.Style["display"] = "none";
					this.trBottom2.Style["display"] = "none";

					this.trClose.Style["display"] = "";
				}

				//Ȩ��
				this.btnModify.Visible = base.user.HasRight("010503");
				this.btnDelete.Visible = base.user.HasRight("010504");

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
				string code = this.txtModelCode.Value;
				if (code != "") 
				{
					EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByCode(code);
					if (entity.HasRecord())
					{
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.lblModelName.Text = entity.GetString("ModelName");
						this.lblBuildArea.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(entity.GetDecimal("BuildArea")), "ƽ��");
						this.lblRoomArea.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(entity.GetDecimal("RoomArea")), "ƽ��");
						this.lblStructure.Text = entity.GetString("Structure");
						this.lblRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");
						this.lblPBSTypeName.Text = BLL.PBSRule.GetPBSTypeFullName(entity.GetString("HouseType"));
						this.txtModelCode.Value = entity.GetString("ModelCode");

						string ImageCode = entity.GetString("ImageCode");
						if (ImageCode != "") 
						{
							this.imgMain.Src = "ShowPicture.aspx?FileID=" + ImageCode;
							this.imgMain.Style["display"] = "";
						}
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "���Ͳ�����"));
						return;
					}
					entity.Dispose();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
			}
		}

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
				string code = this.txtModelCode.Value;
				BLL.ProductRule.DeleteModel(code);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ɾ��ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			string FromUrl = this.txtFromUrl.Value.Trim();
			if (FromUrl != "") 
			{
				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
	}
}
