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
	/// PaymentList ��ժҪ˵����
	/// </summary>
	public partial class PaymentList : PageBase
	{

		protected System.Web.UI.HtmlControls.HtmlInputText txtVoucherID;
		protected System.Web.UI.HtmlControls.HtmlSelect sltAccountant;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSumPayoutMoney;
		protected System.Web.UI.HtmlControls.HtmlTable divAdvSearch1;
	
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

				this.inputSystemGroupPayment.ClassCode = "0601";

				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["Act"];
				this.txtStatus.Value = Request.QueryString["Status"];
                if (!string.IsNullOrEmpty(Request.QueryString["Pay_Load"]))
                {   //�������������
                    this.dtbPayDate1.Value = DateTime.Now.ToShortDateString();
                }

				ViewState["ImagePath"] = "../Images/";

				//Ȩ��
				this.btnAdd.Visible = base.user.HasRight("060102");
                this.btnAddCostBatch.Visible = base.user.HasRight("060111");
                this.btnPayout.Visible = base.user.HasRight("060202");

				switch (this.txtAct.Value) 
				{
					case "1"://Ӧ����
						this.spanTitle.InnerText = "Ӧ������";

						this.tdSearchStatus.Style["display"] = "none";

						this.chkStatus0.Checked = false;
						this.chkStatus1.Checked = true;
						this.chkStatus2.Checked = false;
                        this.chkStatus3.Checked = false;

						this.btnAdd.Style["display"] = "none";
                        this.btnAddCostBatch.Style["display"] = "none";
                        this.btnPayout.Style["display"] = "";

						this.dgList.Columns[0].Visible = true;

						break;

					default:
						//������
						this.chkStatus0.Checked = this.txtStatus.Value.IndexOf("0") >= 0;
						this.chkStatus1.Checked = this.txtStatus.Value.IndexOf("1") >= 0;
                        this.chkStatus2.Checked = this.txtStatus.Value.IndexOf("2") >= 0;
                        this.chkStatus3.Checked = this.txtStatus.Value.IndexOf("3") >= 0;

						break;
				}

//				string status = Request["Status"] + "" ;
//				if ( status.IndexOf("2") >= 0 )
//					this.chkStatus2.Checked = true;
//				if ( status.IndexOf("1") >= 0 )
//					this.chkStatus1.Checked = true;
//				if ( status.IndexOf("0")>= 0)
//					this.chkStatus0.Checked = true;
         
//				BLL.PageFacade.LoadUnitSelect( this.sltUnitCode,"");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
            txtPageSize.Value = Application["GridPageSize"].ToString();
		}

		private void LoadDataGrid()
		{
            dgList.PageSize = int.Parse(txtPageSize.Value);
            try
			{
				PaymentStrategyBuilder sb = new PaymentStrategyBuilder("V_Payment");

                if ( txtProjectCode.Value != "" )
                {
                    sb.AddStrategy(new Strategy(PaymentStrategyName.ProjectCode, txtProjectCode.Value));
                }

				string isContract = "";
				if ( this.chkIsContract.Checked && ! this.chkIsNotContract.Checked )
					isContract = "1";
				if ( !this.chkIsContract.Checked && this.chkIsNotContract.Checked )
					isContract = "0";

				if (isContract != "")
					sb.AddStrategy( new Strategy( PaymentStrategyName.IsContract,isContract));

				ArrayList arStatus = new ArrayList();
				if ( this.chkStatus0.Checked )
					arStatus.Add("0");
				if ( this.chkStatus1.Checked )
					arStatus.Add("1");
				if ( this.chkStatus2.Checked )
					arStatus.Add("2");
                if (this.chkStatus3.Checked)
                {
                    arStatus.Add("3");
                    switch (this.up_sPMName.ToUpper())
                    {
                        case "TANGCHENPM":
                            arStatus.Add("5"); //��������������еȴ�����Ҳ���������״̬��
                            arStatus.Add("6"); //��������������л��������Ҳ���������״̬��
                            break;
                        default:
                            break;

                    }
                }
				string status = BLL.ConvertRule.GetArrayLinkString(arStatus);
				if ( status != "" )
					sb.AddStrategy( new Strategy( PaymentStrategyName.Status, status ));

				if ( this.inputSystemGroupPayment.Value != "" )
				{
					ArrayList arGroup = new ArrayList();
					arGroup.Add(this.inputSystemGroupPayment.Value);
					arGroup.Add("0");
					sb.AddStrategy( new Strategy( PaymentStrategyName.GroupCodeEx,arGroup ));
				}

				if (this.txtAct.Value == "1")  //Ӧ����
				{
					sb.AddStrategy(new Strategy(PaymentStrategyName.NotPayout));
				}

                ////2007.2.8 ע�͵� ��ï�������򿪸߼���ѯҲ�ܹ���Ч
//				if (this.txtAdvSearch.Value != "none")
//				{
					//��Ӧ������
                    if ( this.txtSupplyName.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.SupplyName,this.txtSupplyName.Value ));
                    
                    //�ܿ�������
					if ( this.txtPayer.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.Payer,this.txtPayer.Value ));

                    //����
					if ( this.ucUnit.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.UnitCode,this.ucUnit.Value ));

                    //������
					if ( this.ucApplyPerson.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.ApplyPerson,this.ucApplyPerson.Value ));
					if ( this.ucCheckPerson.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.CheckPerson,this.ucCheckPerson.Value ));

                    //��������
					if ( this.dtbApplyDate0.Value != "" || this.dtbApplyDate1.Value != "" )
					{
						
                        ArrayList ar = new ArrayList();
						ar.Add(this.dtbApplyDate0.Value);
						ar.Add(this.dtbApplyDate1.Value);
						sb.AddStrategy( new Strategy( PaymentStrategyName.ApplyDate,ar ));
					}

                    ////������� �����
					if ( this.dtbCheckDate0.Value != "" || this.dtbCheckDate1.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtbCheckDate0.Value);
						ar.Add(this.dtbCheckDate1.Value);
						sb.AddStrategy( new Strategy( PaymentStrategyName.CheckDate,ar ));
					}

                    //����������
					if ( this.dtbPayDate0.Value != "" || this.dtbPayDate1.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtbPayDate0.Value);
						ar.Add(this.dtbPayDate1.Value);
						sb.AddStrategy( new Strategy( PaymentStrategyName.PayDate,ar ));
					}
                    //�ܿ���
                    if (this.txtTotalMoney0.Text != "" || this.txtTotalMoney1.Text != "")
                    {
                        ArrayList ar = new ArrayList();
                        ar.Add((this.txtTotalMoney0.Text == "") ? "" : this.txtTotalMoney0.ValueDecimal.ToString());
                        ar.Add((this.txtTotalMoney1.Text == "") ? "" : this.txtTotalMoney1.ValueDecimal.ToString());
                        sb.AddStrategy(new Strategy(PaymentStrategyName.Money, ar));
                    }

                    //�����
					if ( this.txtPaymentID.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.PaymentID,this.txtPaymentID.Value ));

                    //��ͬ���
					if ( this.txtContractID.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.ContractID,this.txtContractID.Value ));
                    //��ͬ����
					if ( this.txtContractName.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.PaymentNameEx,this.txtContractName.Value ));

                    //�ɱ��������
                    if (this.chkBatchPayment.Checked && !this.chkNotBatchPayment.Checked)
                        sb.AddStrategy(new Strategy(PaymentStrategyName.BatchPayment));

                    //�ǳɱ��������
                    if (!this.chkBatchPayment.Checked && this.chkNotBatchPayment.Checked)
                        sb.AddStrategy(new Strategy(PaymentStrategyName.NotBatchPayment));

                    if (this.txtPaymentTitle.Value != "")
                        sb.AddStrategy(new Strategy(PaymentStrategyName.PaymentTitle, this.txtPaymentTitle.Value));
//                }

				//Ȩ��
				ArrayList arA = new ArrayList();
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.PaymentStrategyName.AccessRange,arA));

				//����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//ȱʡ����
					sb.AddOrder( "ApplyDate" ,false);
					sb.AddOrder( "PaymentCode" ,false);
				}

				string sql = sb.BuildMainQueryString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Payment",sql );
				qa.Dispose();

				string[] arrField = {"Money", "TotalPayout"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.txtSumMoney.Value = arrSum[0].ToString("N");
				this.txtSumTotalPayoutMoney.Value = arrSum[1].ToString("N");
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
			this.dgList.ItemDataBound+=new DataGridItemEventHandler(dgList_ItemDataBound);


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
//                check();

				this.LoadDataGrid();
//                show();
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
				((Label)e.Item.FindControl("lblSumTotalPayoutMoney")).Text = this.txtSumTotalPayoutMoney.Value;
			}
		}

        /* ��ҳ��������Ĵ���ʽӰ�쵽��ѡ��㡰����Ĺ��ܣ���ע�͵�
         * 
        //checkbox��ҳ��������
        private void check()
        {
            Hashtable ht = new Hashtable();
            if (ViewState["userlistPaymentCheckBox"] != null)
            {
                ht = (Hashtable)ViewState["userlistPaymentCheckBox"];
                if (ht != null)
                {
                    for (int i = 0; i < dgList.Items.Count; i++)
                    {
                        if ((dgList.Items[i].Cells[0].FindControl("chkSelect") as System.Web.UI.HtmlControls.HtmlInputCheckBox).Checked)
                        {
                            if (!ht.ContainsKey(((Label)dgList.Items[i].Cells[1].FindControl("lblPaymentID")).Text.ToString().Trim()))
                            {
                                ht.Add(((Label)dgList.Items[i].Cells[1].FindControl("lblPaymentID")).Text.ToString().Trim(), dgList.Items[i].Cells[3].Text.ToString().Trim());
                            }
                        }
                        else
                        {
                            if (ht.ContainsKey(((Label)dgList.Items[i].Cells[1].FindControl("lblPaymentID")).Text.ToString().Trim()))
                            {
                                ht.Remove(((Label)dgList.Items[i].Cells[1].FindControl("lblPaymentID")).Text.ToString().Trim());
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dgList.Items.Count; i++)
                {
                    if (((System.Web.UI.HtmlControls.HtmlInputCheckBox)dgList.Items[i].Cells[0].FindControl("chkSelect")).Checked)
                    {
                        ht.Add(((Label)dgList.Items[i].Cells[1].FindControl("lblPaymentID")).Text.ToString().Trim(), dgList.Items[i].Cells[3].Text.ToString().Trim());
                    }
                }
            }

            ViewState["userlistPaymentCheckBox"] = ht;
        }

        private void show()
        {
            if (ViewState["userlistPaymentCheckBox"] != null)
            {
                Hashtable ht = (Hashtable)ViewState["userlistPaymentCheckBox"];
                if (ht != null)
                {
                    for (int i = 0; i < dgList.Items.Count; i++)
                    {
                        if (ht.ContainsKey(((Label)dgList.Items[i].Cells[1].FindControl("lblPaymentID")).Text.ToString().Trim()))
                            (dgList.Items[i].Cells[0].FindControl("chkSelect") as System.Web.UI.HtmlControls.HtmlInputCheckBox).Checked = true;

                    }
                }
            }

        }
        */
 
	}
}
