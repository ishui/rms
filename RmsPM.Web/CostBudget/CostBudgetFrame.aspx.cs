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
	/// CostBudgetFrame ��ժҪ˵����
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
                    this.lblTitle.Text = "�ɱ�����";
                }
                else
                {
                    if (FullCost == "1")
                    {
                        this.lblTitle.Text = "��Ŀ�ɱ�";
                    }
                }

				this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);

				//ȡ�Ǽ�ʱ����
				decimal OfflineValidHours = BLL.CostBudgetRule.GetOfflineValidHours(this.txtProjectCode.Value);

				if (this.txtCostBudgetBackupCode.Value == "") //�Ǵ浵
				{
					if ((this.txtOnline.Value != "1") && (OfflineValidHours > 0)) //ʹ�÷Ǽ�ʱ
					{
						//��鵱ǰʱ���Ƿ��ѹ���
						EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetBackupByCode(BLL.CostBudgetRule.GetOfflineBackupCode(txtProjectCode.Value));
						if (entity.HasRecord()
							&& (!BLL.CostBudgetRule.IsOfflineExpire(OfflineValidHours, (DateTime)entity.CurrentRow["BackupDate"]))) 
						{
							//δ����ʱ��ʹ�÷Ǽ�ʱ�汾
							this.txtCostBudgetBackupCode.Value = entity.GetString("CostBudgetBackupCode");
						}
						else 
						{
							//�ѹ���ʱ�����·Ǽ�ʱ�汾
							this.txtCostBudgetBackupCode.Value = BLL.CostBudgetRule.OnlineUpdate(this.txtProjectCode.Value, base.user.UserCode);
						}
						entity.Dispose();
					}
				}

				EntityData entityBackup = null;

				if (this.txtCostBudgetBackupCode.Value != "")
				{
					//ȡ���ݱ�
					entityBackup = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetBackupByCode(this.txtCostBudgetBackupCode.Value);
					if (!entityBackup.HasRecord()) 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��Ŀ���ñ��ݱ�����"));
						return;
					}
				}

                this.lblDynamicDateDesc.InnerText = BLL.CostBudgetRule.GetNoTimeDateDesc(this.txtCostBudgetBackupCode.Value, entityBackup);

				if (entityBackup != null) entityBackup.Dispose();

				BLL.PageFacade.LoadCostBudgetBackupSelect(this.sltBackup, this.txtCostBudgetBackupCode.Value, this.txtProjectCode.Value);

				//��ť�Ƿ���ʾ
				if (this.txtCostBudgetBackupCode.Value == BLL.CostBudgetRule.GetOfflineBackupCode(txtProjectCode.Value)) //�Ǽ�ʱ
				{
				}
				else 
				{
					if (this.txtCostBudgetBackupCode.Value != "") //�浵
					{
						this.btnBackup.Visible = false;
					}

					this.btnOnlineUpdate.Visible = false;
				}

				//Ȩ��
				if (!base.user.HasRight("041305"))
					this.btnBackup.Visible = false;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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

	}
}
