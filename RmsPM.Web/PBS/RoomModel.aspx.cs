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
	/// RoomModel 的摘要说明。
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

				//权限
				this.btnModify.Visible = base.user.HasRight("010503");
				this.btnDelete.Visible = base.user.HasRight("010504");

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
				string code = this.txtModelCode.Value;
				if (code != "") 
				{
					EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByCode(code);
					if (entity.HasRecord())
					{
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.lblModelName.Text = entity.GetString("ModelName");
						this.lblBuildArea.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(entity.GetDecimal("BuildArea")), "平米");
						this.lblRoomArea.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(entity.GetDecimal("RoomArea")), "平米");
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
						Response.Write(Rms.Web.JavaScript.Alert(true, "户型不存在"));
						return;
					}
					entity.Dispose();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
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
				string code = this.txtModelCode.Value;
				BLL.ProductRule.DeleteModel(code);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 返回
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
