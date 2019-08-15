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
using Rms.Web;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetFrame 的摘要说明。
	/// </summary>
	public partial class CostBudgetFrame : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUpdate;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtCostBudgetBackupCode.Value = Request.QueryString["CostBudgetBackupCode"];
				this.txtOnline.Value = Request.QueryString["Online"];
				this.txtAct.Value = Request.QueryString["Act"];

                string FullCost = "" + Request.QueryString["FullCost"];
                string HideBudget = "" + Request.QueryString["HideBudget"];

                if (HideBudget == "1")
                {
                    this.lblTitle.Text = "成本控制";
                }
                else
                {
                    if (FullCost == "1")
                    {
                        this.lblTitle.Text = "项目成本";
                    }
                }

				this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);

				//取非即时设置
				decimal OfflineValidHours = BLL.CostBudgetRule.GetOfflineValidHours(this.txtProjectCode.Value);

				if (this.txtCostBudgetBackupCode.Value == "") //非存档
				{
					if ((this.txtOnline.Value != "1") && (OfflineValidHours > 0)) //使用非即时
					{
						//检查当前时间是否已过期
						EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetBackupByCode(BLL.CostBudgetRule.GetOfflineBackupCode(txtProjectCode.Value));
						if (entity.HasRecord()
							&& (!BLL.CostBudgetRule.IsOfflineExpire(OfflineValidHours, (DateTime)entity.CurrentRow["BackupDate"]))) 
						{
							//未过期时，使用非即时版本
							this.txtCostBudgetBackupCode.Value = entity.GetString("CostBudgetBackupCode");
						}
						else 
						{
							//已过期时，更新非即时版本
							this.txtCostBudgetBackupCode.Value = BLL.CostBudgetRule.OnlineUpdate(this.txtProjectCode.Value, base.user.UserCode);
						}
						entity.Dispose();
					}
				}

				EntityData entityBackup = null;

				if (this.txtCostBudgetBackupCode.Value != "")
				{
					//取备份表
					entityBackup = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetBackupByCode(this.txtCostBudgetBackupCode.Value);
					if (!entityBackup.HasRecord()) 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "项目费用备份表不存在"));
						return;
					}
				}

                this.lblDynamicDateDesc.InnerText = BLL.CostBudgetRule.GetNoTimeDateDesc(this.txtCostBudgetBackupCode.Value, entityBackup);

				if (entityBackup != null) entityBackup.Dispose();

				BLL.PageFacade.LoadCostBudgetBackupSelect(this.sltBackup, this.txtCostBudgetBackupCode.Value, this.txtProjectCode.Value);

				//按钮是否显示
				if (this.txtCostBudgetBackupCode.Value == BLL.CostBudgetRule.GetOfflineBackupCode(txtProjectCode.Value)) //非即时
				{
				}
				else 
				{
					if (this.txtCostBudgetBackupCode.Value != "") //存档
					{
						this.btnBackup.Visible = false;
					}

					this.btnOnlineUpdate.Visible = false;
				}

				//权限
				if (!base.user.HasRight("041305"))
					this.btnBackup.Visible = false;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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

	}
}
