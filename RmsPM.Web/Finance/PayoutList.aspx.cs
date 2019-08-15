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
	/// PayoutList ��ժҪ˵����
	/// </summary>
	public partial class PayoutList : PageBase
	{

		protected System.Web.UI.HtmlControls.HtmlSelect sltAccountant;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;

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
				this.txtStatus.Value = Request.QueryString["Status"];

				this.inputSystemGroup.ClassCode="0602";

                this.ucInputSubjectStart.ProjectCode = this.txtProjectCode.Value;
                this.ucInputSubjectEnd.ProjectCode = this.txtProjectCode.Value;

				ViewState["ImagePath"] = "../Images/";

				//Ȩ��
				this.btnBuildVoucher.Visible = user.HasRight("060302");
				this.btnSelectVoucher.Visible = user.HasRight("060302");

				// �����޸Ĺ�Ӧ��
				this.btnBatchModify.Visible = user.HasRight("190122");

				this.chkStatus0.Checked = this.txtStatus.Value.IndexOf("0") >= 0;
				this.chkStatus1.Checked = this.txtStatus.Value.IndexOf("1") >= 0;

//				switch (this.txtAct.Value) 
//				{
//					case "1"://Ӧ��ƾ֤
//						this.spanTitle.InnerText = "Ӧ��ƾ֤";
//
//						this.tdSearchStatus.Style["display"] = "none";
//
//						this.chkStatus0.Checked = false;
//						this.chkStatus1.Checked = true;
//
//						this.dgList.Columns[0].Visible = true;
//
//						break;
//
//					default:
//						break;
//				}
         
//				BLL.PageFacade.LoadAllUserSelect(this.sltInputPerson,"");
//				BLL.PageFacade.LoadAllUserSelect(this.sltCheckPerson,"");
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
				PayoutStrategyBuilder sb = new PayoutStrategyBuilder("V_Payout");
				sb.AddStrategy( new Strategy( PayoutStrategyName.ProjectCode,txtProjectCode.Value));

				ArrayList arStatus = new ArrayList();
				if ( this.chkStatus0.Checked ) arStatus.Add("0");
				if ( this.chkStatus1.Checked ) arStatus.Add("1");
				string status = BLL.ConvertRule.GetArrayLinkString(arStatus);
				if ( status != "" )
					sb.AddStrategy( new Strategy( PayoutStrategyName.Status, status ));

				string isContract = "";
				if ( this.chkIsContract.Checked && ! this.chkIsNotContract.Checked )
					isContract = "1";
				if ( !this.chkIsContract.Checked && this.chkIsNotContract.Checked )
					isContract = "0";
				if (isContract != "")
					sb.AddStrategy( new Strategy( PayoutStrategyName.IsContract,isContract));

                ////2007.2.8 ע�͵� ��ï�������򿪸߼���ѯҲ�ܹ���Ч
//				if (this.txtAdvSearch.Value != "none") 
//				{
					if ( this.txtPayoutID.Value != "" )
						sb.AddStrategy( new Strategy( PayoutStrategyName.PayoutID,this.txtPayoutID.Value ));

					if ( this.txtPaymentID.Value != "" )
						sb.AddStrategy( new Strategy( PayoutStrategyName.PaymentID,this.txtPaymentID.Value ));

					if ( this.txtVoucherID.Value != "" )
						sb.AddStrategy( new Strategy( PayoutStrategyName.VoucherID,this.txtVoucherID.Value ));

					if ( this.txtContractID.Value != "" )
						sb.AddStrategy( new Strategy( PayoutStrategyName.ContractID,this.txtContractID.Value ));

					if ( this.txtContractName.Value != "" )
						sb.AddStrategy( new Strategy( PayoutStrategyName.ContractName,this.txtContractName.Value ));

					if ( this.txtSupplyName.Value != "" )
						sb.AddStrategy( new Strategy( PayoutStrategyName.SupplyName,this.txtSupplyName.Value ));

					if ( this.txtPayer.Value != "" )
						sb.AddStrategy( new Strategy( PayoutStrategyName.Payer,this.txtPayer.Value ));

					if ( this.ucInputPerson.Value != "" )
						sb.AddStrategy( new Strategy( PayoutStrategyName.InputPerson,this.ucInputPerson.Value ));
					if ( this.ucCheckPerson.Value != "" )
						sb.AddStrategy( new Strategy( PayoutStrategyName.CheckPerson,this.ucCheckPerson.Value ));

					if ( this.dtbInputDate0.Value != "" || this.dtbInputDate1.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtbInputDate0.Value);
						ar.Add(this.dtbInputDate1.Value);
						sb.AddStrategy( new Strategy( PayoutStrategyName.InputDateRange,ar ));
					}

					if ( this.dtbCheckDate0.Value != "" || this.dtbCheckDate1.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtbCheckDate0.Value);
						ar.Add(this.dtbCheckDate1.Value);
						sb.AddStrategy( new Strategy( PayoutStrategyName.CheckDateRange,ar ));
					}

					if ( this.dtbPayoutDate0.Value != "" || this.dtbPayoutDate1.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtbPayoutDate0.Value);
						ar.Add(this.dtbPayoutDate1.Value);
						sb.AddStrategy( new Strategy( PayoutStrategyName.PayoutDateRange,ar ));
					}

                    //�ɱ��������
                    if (this.chkBatchPayment.Checked && !this.chkNotBatchPayment.Checked)
                        sb.AddStrategy(new Strategy(PayoutStrategyName.BatchPayment));

                    //�ǳɱ��������
                    if (!this.chkBatchPayment.Checked && this.chkNotBatchPayment.Checked)
                        sb.AddStrategy(new Strategy(PayoutStrategyName.NotBatchPayment));
//                }

				//Ȩ��
				ArrayList arA = new ArrayList();
				arA.Add("060201");
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.PayoutStrategyName.AccessRange,arA));

				//����
				if (  this.inputSystemGroup.Value != "" )
				{
					ArrayList arS = new ArrayList();
					arS.Add( this.inputSystemGroup.Value );
					arS.Add("0");
					sb.AddStrategy( new Strategy( DAL.QueryStrategy.PayoutStrategyName.GroupCodeEx,arS));
				}

                //������Ŀ
                if (this.ucInputSubjectStart.Value != "")
                { 
                    sb.AddStrategy( new Strategy( DAL.QueryStrategy.PayoutStrategyName.SubjectCodeStart,this.ucInputSubjectStart.Value));
                }
                if (this.ucInputSubjectEnd.Value != "")
                {
                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.PayoutStrategyName.SubjectCodeEnd, this.ucInputSubjectEnd.Value));
                }

                if (this.TxtGreateCash.Value.Trim() != "")
                {
                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.PayoutStrategyName.GreatRootCash, this.TxtGreateCash.Value.Trim()));
                }
                if (this.TxtSmallCash.Value.Trim() != "")
                {
                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.PayoutStrategyName.SmallRootCash,this.TxtSmallCash.Value.Trim()));
                }

				//����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//ȱʡ����
					sb.AddOrder( "PayoutDate" ,false);
					sb.AddOrder( "PayoutCode" ,false);
				}

				string sql = sb.BuildMainQueryString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}



                QueryAgent qa = new QueryAgent();
                EntityData entity = qa.FillEntityData("V_Payout", sql);
                qa.Dispose();

                //EntityData entity = new EntityData("Standard_Payout");
                //StandardEntityDAO dao = new StandardEntityDAO("Standard_Payout");

                //dao.FillEntity(, "@PayoutCode", code, entity, "Payout");
                //dao.FillEntity(SqlManager.GetSqlStruct("PayoutItem", "SelectByPayoutCode").GetSqlStringWithOrder(), "@PayoutCode", code, entity, "PayoutItem");
                //dao.FillEntity(SqlManager.GetSqlStruct("PayoutItemBuilding", "SelectByPayoutCode").GetSqlStringWithOrder(), "@PayoutCode", code, entity, "PayoutItemBuilding");


				string[] arrField = {"Money"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.txtSumMoney.Value = arrSum[0].ToString("N");
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

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
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
	}
}
