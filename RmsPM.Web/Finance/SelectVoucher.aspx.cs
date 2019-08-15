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
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// 
	/// </summary>
	public partial class SelectVoucher : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnOK;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadDataGrid();
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

		private void IniPage()
		{
			try 
			{
				this.txtAct.Value = Request["Act"];
				this.txtRefreshScript.Value = Request["RefreshScript"];
				this.txtProjectCode.Value = Request["ProjectCode"];

				BLL.PageFacade.LoadVoucherTypeSelect(this.sltSearchVoucherType, "");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ�б�
		/// </summary>
		private void LoadDataGrid() 
		{
			this.lblMessage.Text = "";

			try
			{
				string VoucherID = this.txtSearchVoucherID.Value.Trim();

				VoucherStrategyBuilder sb = new VoucherStrategyBuilder();
				sb.AddStrategy( new Strategy( VoucherStrategyName.ProjectCode,this.txtProjectCode.Value));

				if ( VoucherID.Length > 0 )
					sb.AddStrategy( new Strategy(DAL.QueryStrategy.VoucherStrategyName.VoucherID, VoucherID)  );

				if ( this.sltSearchVoucherType.Value != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.VoucherType,this.sltSearchVoucherType.Value ));

				sb.AddOrder( "VoucherID", true);
				string sql = sb.BuildQueryViewString();

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("Voucher",sql);

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();
				qa.Dispose();
			}
			catch ( Exception ex )
			{
				this.lblMessage.Text = "ѡ��ƾ֤��ѯ����";
				ApplicationLog.WriteLog(this.ToString(), ex, "ѡ��ƾ֤��ѯ����");
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

	}
}
