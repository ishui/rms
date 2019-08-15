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
using RmsPM.BLL;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// VoucherList ��ժҪ˵����
	/// </summary>
	public partial class VoucherList : PageBase
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//Ȩ��
				this.btnAdd.Visible = base.user.HasRight("060302");
				this.btnCheck.Visible = base.user.HasRight("060305");
				this.btnDownload.Visible = base.user.HasRight("060306");
                this.btnBalanceInput.Visible = base.user.HasOperationRight("060308");  //���ʵ���

				BLL.PageFacade.LoadVoucherTypeSelect(this.sltVoucherType, "");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				VoucherStrategyBuilder sb = new VoucherStrategyBuilder();
				sb.AddStrategy( new Strategy( VoucherStrategyName.ProjectCode,txtProjectCode.Value));

				if ( this.ucAccountant.Value != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.Accountant,this.ucAccountant.Value ));

				if ( this.dtbMakeDate0.Value != "" || this.dtbMakeDate1.Value != "" )
				{
					ArrayList ar = new ArrayList();
					ar.Add(this.dtbMakeDate0.Value);
					ar.Add(this.dtbMakeDate1.Value);
					sb.AddStrategy( new Strategy( VoucherStrategyName.MakeDate,ar ));
				}

				if ( this.sltVoucherType.Value != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.VoucherType,this.sltVoucherType.Value ));

				if ( this.txtVoucherID.Value != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.VoucherID,this.txtVoucherID.Value ));

				ArrayList arStatus = new ArrayList();
				if ( this.chkStatus0.Checked )
					arStatus.Add("0");
				if ( this.chkStatus1.Checked )
					arStatus.Add("1");
				if ( this.chkStatus2.Checked )
					arStatus.Add("2");
				string status = BLL.ConvertRule.GetArrayLinkString(arStatus);
				if ( status != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.Status, status ));

//				if ( this.sltIsExported.Value != "" )
//					sb.AddStrategy( new Strategy( VoucherStrategyName.IsExported,this.sltIsExported.Value ));

				sb.AddOrder( "MakeDate" ,false);
				sb.AddOrder( "VoucherCode" ,false);
				string sql = sb.BuildQueryViewString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Voucher",sql );
				qa.Dispose();

				string[] arrField = {"TotalMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.dgList.Columns[3].FooterText = arrSum[0].ToString("N");

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();

                this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();
                
                entity.Dispose();
				

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
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
			this.btnAllowPaging.ServerClick += new System.EventHandler(this.btnAllowPaging_ServerClick);
			this.btnSearch.ServerClick += new System.EventHandler(this.btnSearch_ServerClick);
		}
		#endregion

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			int index = e.NewPageIndex;
			this.dgList.CurrentPageIndex = index;
			LoadDataGrid();
		}

		private void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
            this.dgList.CurrentPageIndex = 0;
            LoadDataGrid();
		}

		/*
		/// <summary>
		/// ����һ�������ƾ֤
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDownloadHidden_ServerClick(object sender, System.EventArgs e)
		{
			string codes = this.txtSelect.Value;
			string SaveFileNameHttp = BLL.VoucherRule.MakeVoucherFile(codes, Server);
			Response.Write(Rms.Web.JavaScript.WinOpen(true, SaveFileNameHttp,"","","","","",true,true,false,true,true,true,false,false));

			//ˢ�µ�����־
			LoadDataGrid();
		}
*/

		private void btnAllowPaging_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.AllowPaging = !(this.dgList.AllowPaging);

			if (this.dgList.AllowPaging) 
			{
				this.btnAllowPaging.Value = "ȡ����ҳ";
			}
			else 
			{
				this.btnAllowPaging.Value = "��ҳ��ʾ";
			}

			this.LoadDataGrid();
		}

        protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
        {
            try
            {
                this.LoadDataGrid();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
            }
        }
    }
}
