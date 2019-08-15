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
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ChangeInfo ��ժҪ˵����
	/// </summary>
	public partial class ChangeInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable tableButton;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViewCheckOpinion;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
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
		private void LoadData()
		{
			try
			{
				string contractCode=Request["ContractCode"] + "" ;

				EntityData entity=RmsPM.DAL.EntityDAO.ContractDAO.GetContractByCode(contractCode); 
				if(entity.HasRecord())
				{

					this.lblContractID.Text=entity.GetString("ContractID");
					this.lblContractName.Text=entity.GetString("ContractName");
					this.lblChangePersonName.Text=BLL.SystemRule.GetUserName(entity.GetString("ChangePerson"));
					this.lblChangeOpinionDate.Text=entity.GetDateTimeOnlyDate("ChangeOpinionDate");
					this.lblChangeReason.Text = BLL.StringRule.FormartInput(entity.GetString("ChangeReason"));
					this.lblChangeRemark.Text = BLL.StringRule.FormartInput(entity.GetString("ChangeRemark"));
				}
				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"�������ݳ���");
			}
		}




	}
}
