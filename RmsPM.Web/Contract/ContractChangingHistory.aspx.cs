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

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ContractChangingHistory ��ժҪ˵����
	/// </summary>
	public partial class ContractChangingHistory : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
			{
				LoadData();
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);

		}
		#endregion
		private void LoadData()
		{
			try
			{
				string ContractCode=Request.QueryString["ContractCode"]+"";
				string ContractLabel="";

				EntityData entity=DAL.EntityDAO.ContractDAO.GetContractByCode(ContractCode);
				if(entity.HasRecord())
				{
					ContractLabel=entity.GetString("ContractLabel");
				}
				
				RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();

				//��ʾ�ú�ͬ���еı����¼
				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.NotOriginalContract));
//				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.Status,"6"));

				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractLabel,ContractLabel));
				string sql = CSB.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData entityTemp = qa.FillEntityData("Contract",sql);
				qa.Dispose();

				this.dgList.DataSource = entityTemp.CurrentTable;
				this.dgList.DataBind();

				entity.Dispose();
				entityTemp.Dispose();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���غ�ͬ�б����");
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex=e.NewPageIndex;
			LoadData();
		}
	}
}
