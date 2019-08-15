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
	/// PaymentList 的摘要说明。
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
                {   //付款到期提醒条件
                    this.dtbPayDate1.Value = DateTime.Now.ToShortDateString();
                }

				ViewState["ImagePath"] = "../Images/";

				//权限
				this.btnAdd.Visible = base.user.HasRight("060102");
                this.btnAddCostBatch.Visible = base.user.HasRight("060111");
                this.btnPayout.Visible = base.user.HasRight("060202");

				switch (this.txtAct.Value) 
				{
					case "1"://应付款
						this.spanTitle.InnerText = "应付款项";

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
						//请款管理
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
                            arStatus.Add("5"); //汤臣的情况，还有等待汇总也算是审核中状态。
                            arStatus.Add("6"); //汤臣的情况，还有汇总审核中也算是审核中状态。
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

				if (this.txtAct.Value == "1")  //应付款
				{
					sb.AddStrategy(new Strategy(PaymentStrategyName.NotPayout));
				}

                ////2007.2.8 注释掉 世茂——不打开高级查询也能够生效
//				if (this.txtAdvSearch.Value != "none")
//				{
					//供应商条件
                    if ( this.txtSupplyName.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.SupplyName,this.txtSupplyName.Value ));
                    
                    //受款人条件
					if ( this.txtPayer.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.Payer,this.txtPayer.Value ));

                    //请款部门
					if ( this.ucUnit.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.UnitCode,this.ucUnit.Value ));

                    //申请人
					if ( this.ucApplyPerson.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.ApplyPerson,this.ucApplyPerson.Value ));
					if ( this.ucCheckPerson.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.CheckPerson,this.ucCheckPerson.Value ));

                    //申请日期
					if ( this.dtbApplyDate0.Value != "" || this.dtbApplyDate1.Value != "" )
					{
						
                        ArrayList ar = new ArrayList();
						ar.Add(this.dtbApplyDate0.Value);
						ar.Add(this.dtbApplyDate1.Value);
						sb.AddStrategy( new Strategy( PaymentStrategyName.ApplyDate,ar ));
					}

                    ////审核日期 审核人
					if ( this.dtbCheckDate0.Value != "" || this.dtbCheckDate1.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtbCheckDate0.Value);
						ar.Add(this.dtbCheckDate1.Value);
						sb.AddStrategy( new Strategy( PaymentStrategyName.CheckDate,ar ));
					}

                    //最晚付款日期
					if ( this.dtbPayDate0.Value != "" || this.dtbPayDate1.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtbPayDate0.Value);
						ar.Add(this.dtbPayDate1.Value);
						sb.AddStrategy( new Strategy( PaymentStrategyName.PayDate,ar ));
					}
                    //受款金额
                    if (this.txtTotalMoney0.Text != "" || this.txtTotalMoney1.Text != "")
                    {
                        ArrayList ar = new ArrayList();
                        ar.Add((this.txtTotalMoney0.Text == "") ? "" : this.txtTotalMoney0.ValueDecimal.ToString());
                        ar.Add((this.txtTotalMoney1.Text == "") ? "" : this.txtTotalMoney1.ValueDecimal.ToString());
                        sb.AddStrategy(new Strategy(PaymentStrategyName.Money, ar));
                    }

                    //请款单编号
					if ( this.txtPaymentID.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.PaymentID,this.txtPaymentID.Value ));

                    //合同编号
					if ( this.txtContractID.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.ContractID,this.txtContractID.Value ));
                    //合同名称
					if ( this.txtContractName.Value != "" )
						sb.AddStrategy( new Strategy( PaymentStrategyName.PaymentNameEx,this.txtContractName.Value ));

                    //成本批量请款
                    if (this.chkBatchPayment.Checked && !this.chkNotBatchPayment.Checked)
                        sb.AddStrategy(new Strategy(PaymentStrategyName.BatchPayment));

                    //非成本批量请款
                    if (!this.chkBatchPayment.Checked && this.chkNotBatchPayment.Checked)
                        sb.AddStrategy(new Strategy(PaymentStrategyName.NotBatchPayment));

                    if (this.txtPaymentTitle.Value != "")
                        sb.AddStrategy(new Strategy(PaymentStrategyName.PaymentTitle, this.txtPaymentTitle.Value));
//                }

				//权限
				ArrayList arA = new ArrayList();
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.PaymentStrategyName.AccessRange,arA));

				//排序
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//缺省排序
					sb.AddOrder( "ApplyDate" ,false);
					sb.AddOrder( "PaymentCode" ,false);
				}

				string sql = sb.BuildMainQueryString();

				if (sortsql != "")
				{
					//点列标题排序
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}


		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
				//显示合计金额
				((Label)e.Item.FindControl("lblSumMoney")).Text = this.txtSumMoney.Value;
				((Label)e.Item.FindControl("lblSumTotalPayoutMoney")).Text = this.txtSumTotalPayoutMoney.Value;
			}
		}

        /* 分页保留问题的处理方式影响到多选后点“付款”的功能，先注释掉
         * 
        //checkbox分页保留问题
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
