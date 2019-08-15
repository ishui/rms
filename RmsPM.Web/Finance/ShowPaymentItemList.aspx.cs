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
	/// ShowPaymentItemList ��ժҪ˵����
	/// </summary>
	public partial class ShowPaymentItemList : PageBase
	{
		protected AspWebControl.Calendar dtbPayDate1;
		protected AspWebControl.Calendar dtbPayDate0;
		protected AspWebControl.Calendar dtbCheckDate1;
		protected AspWebControl.Calendar dtbCheckDate0;
		protected AspWebControl.Calendar dtbApplyDate1;
		protected AspWebControl.Calendar dtbApplyDate0;
		protected System.Web.UI.HtmlControls.HtmlInputText txtVoucherID;
		protected System.Web.UI.HtmlControls.HtmlSelect sltAccountant;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAdd;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnPayout;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSumPayoutMoney;
		protected System.Web.UI.HtmlControls.HtmlTable divAdvSearch1;
		protected RmsPM.Web.UserControls.InputUser ucApplyPerson;
		protected RmsPM.Web.UserControls.InputUser ucCheckPerson;
		protected RmsPM.Web.UserControls.InputUnit ucUnit;
		protected RmsPM.Web.UserControls.InputSystemGroup inputSystemGroupPayment;
	
		public string ParamCostBudgetSetCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["CostBudgetSetCode"]);}
		}

		public string ParamCostCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["CostCode"]);}
		}

        public string ParamSubjectCode
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["SubjectCode"]); }
        }

        public string ParamContractCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["ContractCode"]);}
		}

		public string ParamPaymentCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["PaymentCode"]);}
		}

		public string ParamIsContract
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["IsContract"]);}
		}

		public string ParamIsPayout
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["IsPayout"]);}
		}

		public string ParamPBSType
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["PBSType"]);}
		}

		public string ParamPBSCode
		{
			get {return BLL.ConvertRule.ToString(Request.QueryString["PBSCode"]);}
		}

        public string ParamPayDateBegin
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["PayDateBegin"]); }
        }

        public string ParamPayDateEnd
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["PayDateEnd"]); }
        }

        protected void Page_Load(object sender, System.EventArgs e)
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

				LoadParamDesc();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʼ����������
		/// </summary>
		private void LoadParamDesc()
		{
			try 
			{
				const string Sep = "<span style='width:20px'></span>";
				string desc = "";

				//Ԥ���
				if (this.ParamCostBudgetSetCode != "")
				{
					desc += Sep + "Ԥ���" + BLL.CostBudgetRule.GetCostBudgetSetName(this.ParamCostBudgetSetCode);
				}

				//������
				if (this.ParamCostCode != "")
				{
					desc += Sep + "�����" + BLL.CBSRule.GetCostName(this.ParamCostCode);
				}

                //��Ŀ
                if (this.ParamSubjectCode != "")
                {
                    string SubjectSetCode = BLL.ProjectRule.GetSubjectSetCodeByProject(this.txtProjectCode.Value);
                    desc += Sep + "��Ŀ��" + BLL.SubjectRule.GetSubjectName(this.ParamSubjectCode, SubjectSetCode);
                }

                //��ͬ
				if (this.ParamContractCode != "")
				{
					EntityData entity = DAL.EntityDAO.ContractDAO.GetContractByCode(ParamContractCode);
					if (entity.HasRecord()) 
					{
						desc += Sep + "��ͬ���ƣ�" + entity.GetString("ContractName");
					}
					entity.Dispose();
				}

				//�Ǻ�ͬ��
				if (this.ParamPaymentCode != "")
				{
					desc += Sep + "�Ǻ�ͬ����" + ParamPaymentCode;
				}

				//�Ǻ�ͬ
				if (this.ParamIsContract == "0")
				{
					desc += Sep + "�Ǻ�ͬ";
				}

				//�Ƿ��Ѹ�
				switch (this.ParamIsPayout)
				{
					case "0":
						desc += Sep + "δ��";
						break;

					case "1":
						desc += Sep + "�����Ѹ�";
						break;

					case "2":
						desc += Sep + "�Ѹ���";
						break;

					case "1,2":
						desc += Sep + "�Ѹ�";
						break;

					case "0,1":
						desc += Sep + "δ����";
						break;
				}

				//��λ����
				if ((this.ParamPBSType != "") || (this.ParamPBSCode != ""))
				{
					desc += Sep + "��λ���̣�" + BLL.CostBudgetRule.GetPBSName(ParamPBSType, ParamPBSCode);
				}

                //�������
                if ((this.ParamPayDateBegin != "") || (this.ParamPayDateEnd != ""))
                {
                    desc += Sep + "������ڣ�" + BLL.StringRule.GetDateRangeDesc(this.ParamPayDateBegin, this.ParamPayDateEnd);
                }

                this.lblParamDesc.Text = desc;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��������������" + ex.Message));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				PaymentItemStrategyBuilder sb = new PaymentItemStrategyBuilder();
				sb.AddStrategy( new Strategy( PaymentItemStrategyName.ProjectCode,txtProjectCode.Value));

				if (this.ParamCostBudgetSetCode != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.CostBudgetSetCode, this.ParamCostBudgetSetCode));

				if (this.ParamCostCode != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.CostCodeIncludeAllChild, this.ParamCostCode));

                if (this.ParamSubjectCode != "")
                    sb.AddStrategy(new Strategy(PaymentItemStrategyName.SubjectCodeIncludeAllChild, this.ParamSubjectCode));

                if (this.ParamContractCode != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.ContractCode, this.ParamContractCode));

				if (this.ParamPaymentCode != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.PaymentCode, this.ParamPaymentCode));

				if (this.ParamIsContract != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.IsContract, this.ParamIsContract));

				if (this.ParamIsPayout != "")
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.IsPayout, this.ParamIsPayout));

				if ((this.ParamPBSType != "") || (this.ParamPBSCode != ""))
				{
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.PBSType, this.ParamPBSType));
					sb.AddStrategy( new Strategy( PaymentItemStrategyName.PBSCode, this.ParamPBSCode));
				}

                if (this.ParamPayDateBegin != "" || this.ParamPayDateEnd != "")
                {
                    ArrayList ar = new ArrayList();
                    ar.Add(this.ParamPayDateBegin);
                    ar.Add(this.ParamPayDateEnd);
                    sb.AddStrategy(new Strategy(PaymentItemStrategyName.PayDate, ar));
                }
                
                //����
				sb.AddStrategy( new Strategy( PaymentItemStrategyName.Status, "1,2"));

				//����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//ȱʡ����
					sb.AddOrder( "CheckDate" ,true);
					sb.AddOrder( "PaymentCode" ,true);
				}

				string sql = sb.BuildQueryViewString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "PaymentItem",sql );
				qa.Dispose();

				//���Ѹ����
				if (!entity.CurrentTable.Columns.Contains("ItemPayoutMoney"))
				{
                    entity.CurrentTable.Columns.Add("ItemPayoutMoney", typeof(decimal));
					foreach(DataRow dr in entity.CurrentTable.Rows) 
					{
                        dr["ItemPayoutMoney"] = BLL.PaymentRule.GetPayoutMoneyByPaymentItem(dr["PaymentItemCode"]);
					}
				}

                string[] arrField = { "ItemMoney", "ItemPayoutMoney" };
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				ViewState["SumMoney"] = arrSum[0].ToString("N");
				ViewState["SumTotalPayoutMoney"] = arrSum[1].ToString("N");
				ViewState["SumTotalPayoutBalance"] = (arrSum[0] - arrSum[1]).ToString("N");
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
				((DataGrid)source).CurrentPageIndex = 0;
				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}	

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//��ʾ�ϼƽ��
				((Label)e.Item.FindControl("lblSumMoney")).Text = BLL.ConvertRule.ToString(ViewState["SumMoney"]);
				((Label)e.Item.FindControl("lblSumTotalPayoutMoney")).Text = BLL.ConvertRule.ToString(ViewState["SumTotalPayoutMoney"]);
				((Label)e.Item.FindControl("lblSumTotalPayoutBalance")).Text = BLL.ConvertRule.ToString(ViewState["SumTotalPayoutBalance"]);
			}
		}
	}
}
