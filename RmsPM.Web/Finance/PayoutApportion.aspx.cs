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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PayoutApportion ��ժҪ˵����
	/// </summary>
	public partial class PayoutApportion : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();

				BuildSqlString();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.ViewState.Add("SortColumn","PayoutDate");
				this.ViewState.Add("SortASC","False");
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

				string sql = (string)this.ViewState["SqlString"];

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_Payout",sql );
				qa.Dispose();

				string[] arrField = {"Money"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.txtSumMoney.Value = arrSum[0].ToString("N");
				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();

				this.gpControl.RowsCount = entity.CurrentTable.Rows.Count.ToString();
				entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

        private void BuildSqlString()
        {
            string projectCode = Request["ProjectCode"] + "";

            PayoutStrategyBuilder sb = new PayoutStrategyBuilder("V_Payout");
            sb.AddStrategy(new Strategy(PayoutStrategyName.ProjectCode, projectCode));
            ArrayList arStatus = new ArrayList();
            if ((Request["IsApportioned"] + "") == "1")
                arStatus.Add("1");
            if ((Request["NotIsApportioned"] + "") == "1")
                arStatus.Add("0");
            string apportionStatus = BLL.ConvertRule.GetArrayLinkString(arStatus);
            if (apportionStatus != "")
                sb.AddStrategy(new Strategy(PayoutStrategyName.IsApportioned, apportionStatus));

            //�ض����Ѿ���˹���
            sb.AddStrategy(new Strategy(PayoutStrategyName.Status, "1"));


            //				if ( this.txtPayoutID.Value != "" )
            //					sb.AddStrategy( new Strategy( PayoutStrategyName.PayoutID,this.txtPayoutID.Value ));
            //
            //				if ( this.txtPaymentID.Value != "" )
            //					sb.AddStrategy( new Strategy( PayoutStrategyName.PaymentID,this.txtPaymentID.Value ));
            //
            //				if ( this.txtVoucherID.Value != "" )
            //					sb.AddStrategy( new Strategy( PayoutStrategyName.VoucherID,this.txtVoucherID.Value ));

            string contractID = Request["ContractID"] + "";
            if (contractID != "")
                sb.AddStrategy(new Strategy(PayoutStrategyName.ContractID, contractID));

            string contractName = Request["ContractName"] + "";
            if (contractName != "")
                sb.AddStrategy(new Strategy(PayoutStrategyName.ContractName, contractName));

            string supplierName = Request["SupplierName"] + "";
            if (supplierName != "")
                sb.AddStrategy(new Strategy(PayoutStrategyName.SupplyName, supplierName));

            string payer = Request["Payer"] + "";
            if (payer != "")
                sb.AddStrategy(new Strategy(PayoutStrategyName.Payer, payer));


            //Ȩ����ȥ��
            //			ArrayList arA = new ArrayList();
            //			arA.Add("060201");
            //			arA.Add(user.UserCode);
            //			arA.Add(user.BuildStationCodes());
            //			sb.AddStrategy( new Strategy( DAL.QueryStrategy.PayoutStrategyName.AccessRange,arA));

            //����
            string payoutTypeCode = Request["payoutTypeCode"] + "";
            if (payoutTypeCode != "")
            {
                ArrayList arS = new ArrayList();
                arS.Add(payoutTypeCode);
                arS.Add("0");
                sb.AddStrategy(new Strategy(DAL.QueryStrategy.PayoutStrategyName.GroupCodeEx, arS));
            }

            // �ڷ�̯��ʽ�в���
            string alloType = Request["AlloType"] + "";
            string buildingCode = Request["BuildingCode"] + "";
            string buildingName = Request["BuildingName"] + "";
            string isInType = Request["IsInType"] + "";

            if (isInType != "" && alloType != "")
            {
                ArrayList arAllo = new ArrayList();
                arAllo.Add(alloType);
                arAllo.Add(buildingCode);
                sb.AddStrategy(new Strategy(DAL.QueryStrategy.PayoutStrategyName.AlloType, arAllo));
            }

            string sortColumn = (string)this.ViewState["SortColumn"];
            string sortASC = (string)this.ViewState["SortASC"];
            bool isAsc = (sortASC == "True");
            sb.AddOrder(sortColumn, isAsc);

            string sql = sb.BuildMainQueryString();

            //			Response.Write(sql);

            this.ViewState.Add("SqlString", sql);
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
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

//		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
//		{
//			this.dgList.CurrentPageIndex = e.NewPageIndex;
//			LoadDataGrid();
//		}

		private void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.gpControl.CurrentPageIndex=1;
			LoadDataGrid();
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//��ʾ�ϼƽ��
				((Label)e.Item.FindControl("lblSumMoney")).Text = this.txtSumMoney.Value;
			}
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			string oldSortColumn = (string) this.ViewState["SortColumn"];
			string oldSortASC = (string) this.ViewState["SortASC"];
			if ( e.SortExpression == oldSortColumn )
			{
				if ( oldSortASC == "True" )
					this.ViewState.Add( "SortASC","False" );
				else
					this.ViewState.Add( "SortASC","True" );
			}
			this.ViewState.Add("SortColumn",e.SortExpression);

			BuildSqlString();
			LoadDataGrid();
		}

		protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

//		private void btnApportion_ServerClick(object sender, System.EventArgs e)
//		{
//			string payoutCodes = "";
//			foreach( DataGridItem item in this.dgList.Items)
//			{
//				(HtmlInputCheckBox)item.FindControl("")
//			}
//		}
	}
}
