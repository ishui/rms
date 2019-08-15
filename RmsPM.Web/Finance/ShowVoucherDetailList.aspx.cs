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
	/// ShowVoucherDetailList ��ժҪ˵����
	/// </summary>
	public partial class ShowVoucherDetailList : PageBase
	{
		protected AspWebControl.Calendar dtbCheckDate1;
		protected AspWebControl.Calendar dtbCheckDate0;
		protected System.Web.UI.HtmlControls.HtmlSelect sltAccountant;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected AspWebControl.Calendar dtbPayoutDate0;
		protected AspWebControl.Calendar dtbPayoutDate1;
		protected AspWebControl.Calendar dtbInputDate0;
		protected AspWebControl.Calendar dtbInputDate1;
		protected System.Web.UI.HtmlControls.HtmlTable divAdvSearch;
		protected RmsPM.Web.UserControls.InputUser ucInputPerson;
		protected RmsPM.Web.UserControls.InputUser ucCheckPerson;
		protected RmsPM.Web.UserControls.InputSystemGroup inputSystemGroup ;

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

        public string ParamCheckDateBegin
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["CheckDateBegin"]); }
        }

        public string ParamCheckDateEnd
        {
            get { return BLL.ConvertRule.ToString(Request.QueryString["CheckDateEnd"]); }
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

                //�������
                if ((this.ParamCheckDateBegin != "") || (this.ParamCheckDateEnd != ""))
                {
                    desc += Sep + "ƾ֤������ڣ�" + BLL.StringRule.GetDateRangeDesc(this.ParamCheckDateBegin, this.ParamCheckDateEnd);
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
				VoucherDetailStrategyBuilder sb = new VoucherDetailStrategyBuilder();
				sb.AddStrategy( new Strategy( VoucherDetailStrategyName.ProjectCode,txtProjectCode.Value));

                if (this.ParamSubjectCode != "")
                    sb.AddStrategy(new Strategy(VoucherDetailStrategyName.SubjectCodeIncludeAllChild, this.ParamSubjectCode));

                if (this.ParamContractCode != "")
					sb.AddStrategy( new Strategy( VoucherDetailStrategyName.ContractCode, this.ParamContractCode));

                if (this.ParamPaymentCode != "")
                    sb.AddStrategy(new Strategy(VoucherDetailStrategyName.PaymentCode, this.ParamPaymentCode));

                if (this.ParamCheckDateBegin != "" || this.ParamCheckDateEnd != "")
                {
                    ArrayList ar = new ArrayList();
                    ar.Add(this.ParamCheckDateBegin);
                    ar.Add(this.ParamCheckDateEnd);
                    sb.AddStrategy(new Strategy(VoucherDetailStrategyName.CheckDateRange, ar));
                }

                //����
                sb.AddStrategy(new Strategy(VoucherDetailStrategyName.Status, "1,2"));

                //���跽
                sb.AddStrategy(new Strategy(VoucherDetailStrategyName.OnlyDebit));

                //����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//ȱʡ����
					sb.AddOrder( "CheckDate" ,true);
					sb.AddOrder( "VoucherCode" ,true);
				}

				string sql = sb.BuildQueryViewString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "VoucherDetail",sql );
				qa.Dispose();

				string[] arrField = {"DebitMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				ViewState["SumMoney"] = arrSum[0].ToString("N");
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
			}
		}
	}
}
