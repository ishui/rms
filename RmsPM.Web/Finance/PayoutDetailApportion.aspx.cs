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
	/// 付款单分摊 的摘要说明。
	/// </summary>
	public partial class PayoutDetailApportion : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			this.txtAlloType.Value = "P";
			this.lblAllo.Text = "项目";

		}

		private void LoadData()
		{
			string payoutCodes = Request["PayoutCodes"]+"";
			string payoutItemCodes = Request["PayoutItemCodes"]+"";

			try
			{
				if ( payoutCodes=="" && payoutItemCodes != "")  //传入多个付款单明细
				{
					this.txtProjectCode.Value = "";
					decimal totalMoney = 0;

					this.tdPayout.InnerHtml = "付款款项";
					this.tdPayoutCodes.InnerHtml = "";

					foreach ( string payoutItemCode in payoutItemCodes.Split( new char[]{','} ))
					{
						EntityData payoutItem = DAL.EntityDAO.PaymentDAO.GetPayoutItemByCode(payoutItemCode);
						if ( payoutItem.HasRecord())
						{
							string payoutCode = payoutItem.GetString("PayoutCode");
							string payoutId = BLL.PaymentRule.GetPayoutID(payoutCode);
							string paymentItemCode = payoutItem.GetString("PaymentItemCode");
							string paymentItemName = "";
							EntityData paymentItem = DAL.EntityDAO.PaymentDAO.GetPaymentItemByCode(paymentItemCode);
							if ( paymentItem.HasRecord())
							{
								paymentItemName = paymentItem.GetString("Summary");
							}
							paymentItem.Dispose();

							if (this.tdPayoutCodes.InnerHtml != "")
								this.tdPayoutCodes.InnerHtml = this.tdPayoutCodes.InnerHtml + ",";
							this.tdPayoutCodes.InnerHtml = this.tdPayoutCodes.InnerHtml + payoutId + "->" + paymentItemName;

							totalMoney = totalMoney + payoutItem.GetDecimal("PayoutMoney");

							if (this.txtProjectCode.Value == "") 
							{
								EntityData payout = DAL.EntityDAO.PaymentDAO.GetPayoutByCode(payoutCode);
								this.txtProjectCode.Value = payout.GetString("ProjectCode");
								payout.Dispose();
							}
						}
						payoutItem.Dispose();
					}

					//总金额
					this.lblTotalMoney.Text = BLL.StringRule.BuildShowNumberString( totalMoney);
					this.txtTotalMoney.Value = totalMoney.ToString();

					//显示原来的分摊方式
					string AlloType = "";
					string AlloTypeName = "";
					bool IsManual = false;
					DataTable tb = BLL.PaymentRule.GetAllPayoutItemAlloTypeDetail(payoutItemCodes, ref AlloType, ref AlloTypeName, ref IsManual);

					this.txtAlloType.Value = AlloType;
					this.lblAllo.Text = AlloTypeName;
					this.chkManual.Checked = IsManual;

					if ( AlloType == "P" )
					{
						this.dgList.Visible = false;
					}
					else
					{
						this.dgList.Visible = true;
						BindDataGrid(tb);
					}
				}
				else if ( payoutCodes != "" )  //传入多个付款单
				{
					this.tdPayout.InnerHtml = "付款单";
					this.tdPayoutCodes.InnerHtml = "";

					decimal totalMoney = decimal.Zero;
					EntityData payout = null;
					foreach ( string payoutCode in payoutCodes.Split( new char[]{','} ))
					{
						payout = DAL.EntityDAO.PaymentDAO.GetPayoutByCode(payoutCode);
						if ( payout.HasRecord())
						{
							string payoutId = payout.GetString("PayoutID");
							totalMoney += payout.GetDecimal("Money");
							this.txtProjectCode.Value = payout.GetString("ProjectCode");

							if (this.tdPayoutCodes.InnerHtml != "")
								this.tdPayoutCodes.InnerHtml = this.tdPayoutCodes.InnerHtml + ",";
							this.tdPayoutCodes.InnerHtml = this.tdPayoutCodes.InnerHtml + payoutId;
						}
					}
					if ( payout != null )
						payout.Dispose();
					this.lblTotalMoney.Text = BLL.StringRule.BuildShowNumberString(totalMoney);
					this.txtTotalMoney.Value = totalMoney.ToString();
				}
				else
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"请选择要分摊的款项 ！"));
					Response.End();
				}
			}
			catch(Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));


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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// 显示分摊明细
		/// </summary>
		private void BindDataGrid(DataTable tb)
		{
			try
			{
				this.txtDetailCount.Value = tb.Rows.Count.ToString();

				string[] arrField = {"Money"};
				decimal[] arrValue = BLL.MathRule.SumColumn(tb, arrField);
				this.txtSumMoney.Value = BLL.MathRule.GetDecimalShowString(arrValue[0]);

				this.dgList.DataSource = new DataView(tb,"","BuildingName", DataViewRowState.CurrentRows )  ;
				this.dgList.DataBind();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"显示分摊明细失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示分摊明细失败：" + ex.Message));
			}
		}

		protected void btnSelectBuildingReturn_ServerClick(object sender, System.EventArgs e)
		{

			string alloType = this.txtAlloType.Value;
			string selectCodes = this.txtSelectCodes.Value;

			if ( alloType == "P"  )
				this.lblAllo.Text = "项目";
			else if ( alloType == "U" )
				this.lblAllo.Text = "单位工程";
			else if ( alloType == "B" )
				this.lblAllo.Text = "楼栋";

			if ( alloType == "P" )
			{
				this.dgList.Visible = false;
			}
			else
			{

				this.dgList.Visible = true;
				DataTable dt = new DataTable();
				dt.Columns.Add("BuildingCode",Type.GetType("System.String"));
				dt.Columns.Add("BuildingName",Type.GetType("System.String"));
				dt.Columns.Add("Money",Type.GetType("System.Decimal"));

				foreach ( string code in selectCodes.Split(new char[]{','}))
				{
					if ( code == "" )
						break;
					string buildingName="";
					if ( alloType == "B")
						buildingName = BLL.ProductRule.GetBuildingName(code);
					else if (alloType=="U")
						buildingName = BLL.ProductRule.GetPBSUnitName(code);

					DataRow newRow = dt.NewRow();
					newRow["BuildingCode"]=code;
					newRow["BuildingName"]=buildingName;
					newRow["Money"]=decimal.Zero;
					dt.Rows.Add(newRow);
				}

				BindDataGrid(dt);
				dt.Dispose();
			}

		}

		/// <summary>
		/// 检查总金额
		/// </summary>
		/// <returns></returns>
		private bool CheckTotalMoney()
		{
			decimal dOldTotalMoney = BLL.ConvertRule.ToDecimal(this.txtTotalMoney.Value);

			/*
			if ( dOldTotalMoney == 0  )
			{
				Response.Write( Rms.Web.JavaScript.Alert( true, "总金额为0，无法分摊，请检查 ！"));
				return false;
			}
			*/

			decimal dTotalMoney = decimal.Zero;

			foreach ( DataGridItem item in this.dgList.Items)
			{
				string sMoney = ((HtmlInputText)item.FindControl("txtMoney")).Value;
				string buildingName = ((HtmlInputHidden)item.FindControl("txtBuildingName")).Value;
				string buildingCode = ((HtmlInputHidden)item.FindControl("txtBuildingcode")).Value;

				if ((sMoney != "") && ( ! Rms.Check.StringCheck.IsNumber(sMoney)))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("楼栋“{0}”的金额格式不正确 ！", buildingName)));
					return false;
				}

				dTotalMoney += BLL.ConvertRule.ToDecimal(sMoney);
			}

			if ( ! BLL.MathRule.CheckDecimalEqual( dOldTotalMoney,dTotalMoney ) )
			{
				Response.Write( Rms.Web.JavaScript.Alert(true, string.Format("明细金额({0})和总金额({1})不平，请检查 ！", dTotalMoney, dOldTotalMoney)));
				return false;
			}

			return true;
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{

			try
			{
				//缺省是分摊到整个项目
				string alloType = this.txtAlloType.Value;
				if ( alloType == "" )
					alloType="P";
				string inputPayoutCodes = Request["PayoutCodes"]+"";
				string inputPayoutItemCodes = Request["PayoutItemCodes"]+"";

				bool isManual = ( this.chkManual.Checked );
				if ( alloType != "P" && isManual )
				{
					if ( !CheckTotalMoney())
						return;
				
				}

				decimal dOldTotalMoney = decimal.Parse(this.txtTotalMoney.Value);

				// 分摊付款单
				if( inputPayoutCodes !="" )
				{

					EntityData payout = null;
					if ( alloType == "P" )
					{
				
						foreach ( string payoutCode in inputPayoutCodes.Split( new char[]{','} ))
						{
							if ( payoutCode == "" )
								break;
							payout = DAL.EntityDAO.PaymentDAO.GetStandard_PayoutByCode(payoutCode);
							payout.CurrentRow["IsApportioned"]=1;

							foreach ( DataRow drPayoutItem in payout.Tables["PayoutItem"].Rows)
							{
								drPayoutItem["AlloType"]="P";
								drPayoutItem["IsManualAlloc"]=0;
							}
							payout.DeleteAllTableRow("PayoutItemBuilding");
							DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payout(payout);
						}

					}
					else
					{
						foreach ( string payoutCode in inputPayoutCodes.Split( new char[]{','} ))
						{
							if ( payoutCode=="" )
								break;
							payout = DAL.EntityDAO.PaymentDAO.GetStandard_PayoutByCode(payoutCode);
							payout.DeleteAllTableRow("PayoutItemBuilding");

							payout.SetCurrentTable("Payout");
							payout.CurrentRow["IsApportioned"]=1;

							foreach ( DataRow drPayoutItem in payout.Tables["PayoutItem"].Rows)
							{
								drPayoutItem["AlloType"]=alloType;
								if ( isManual )
									drPayoutItem["IsManualAlloc"]=1;
								else
									drPayoutItem["IsManualAlloc"]=0;
							}

							payout.SetCurrentTable("payoutItemBuilding");
							foreach ( DataGridItem item in this.dgList.Items)
							{
								string sMoney = ((HtmlInputText)item.FindControl("txtMoney")).Value;
								decimal dMoney = decimal.Zero;
								if ( isManual )
									dMoney=decimal.Parse(sMoney);
								string buildingName = ((HtmlInputHidden)item.FindControl("txtBuildingName")).Value;
								string buildingCode = ((HtmlInputHidden)item.FindControl("txtBuildingcode")).Value;

								foreach ( DataRow drPayoutItem in payout.Tables["PayoutItem"].Rows)
								{
									string payoutItemCode = BLL.ConvertRule.ToString(drPayoutItem["PayoutItemCode"]);
									decimal itemMoney = BLL.ConvertRule.ToDecimal(drPayoutItem["PayoutMoney"]);
									DataRow newRow = payout.GetNewRecord();
									newRow["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemBuildingCode");
									newRow["PayoutCode"]=payoutCode;
									newRow["PayoutItemCode"]=payoutItemCode;
									if ( alloType == "U" )
										newRow["PBSUnitCode"] = buildingCode;
									else
										newRow["BuildingCode"] = buildingCode;

									// 如果是手工分摊，计算分摊金额
									if ( isManual )
										newRow["ItemBuildingMoney"] =  itemMoney * dMoney / dOldTotalMoney ;

									payout.AddNewRecord(newRow);
								}
					
							}
							DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payout(payout);
						}
					}
					if ( payout != null)
						payout.Dispose();
				}
				else
				{
					// 分摊付款单明细
					string[] arr = inputPayoutItemCodes.Split(",".ToCharArray());
					foreach(string inputPayoutItemCode in arr) 
					{
						EntityData piTemp = DAL.EntityDAO.PaymentDAO.GetPayoutItemByCode(inputPayoutItemCode);
						string payoutCode = piTemp.GetString("payoutCode");
						piTemp.Dispose();

						EntityData payout = DAL.EntityDAO.PaymentDAO.GetStandard_PayoutByCode(payoutCode);
						payout.CurrentRow["IsApportioned"]=1;

						// 清除该款项的分摊
						foreach ( DataRow rrr in payout.Tables["PayoutItemBuilding"].Select( String.Format( " PayoutItemCode='{0}' " ,inputPayoutItemCode ) ))
							rrr.Delete();
				

						foreach ( DataRow drPayoutItem in payout.Tables["PayoutItem"].Select( String.Format( " PayoutItemCode='{0}' " ,inputPayoutItemCode ) ) )
						{
							drPayoutItem["AlloType"]=alloType;
							if ( isManual )
								drPayoutItem["IsManualAlloc"]=1;
							else
								drPayoutItem["IsManualAlloc"]=0;
						}

						payout.SetCurrentTable("payoutItemBuilding");
						foreach ( DataGridItem item in this.dgList.Items)
						{
							string sMoney = ((HtmlInputText)item.FindControl("txtMoney")).Value;
							decimal dMoney = decimal.Zero;
							if ( isManual )
								dMoney = decimal.Parse(sMoney);
							string buildingName = ((HtmlInputHidden)item.FindControl("txtBuildingName")).Value;
							string buildingCode = ((HtmlInputHidden)item.FindControl("txtBuildingcode")).Value;
					
							DataRow newRow = payout.GetNewRecord();
							newRow["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemBuildingCode");
							newRow["PayoutCode"]=payoutCode;
							newRow["PayoutItemCode"]=inputPayoutItemCode;
							if ( alloType == "U" )
								newRow["PBSUnitCode"] = buildingCode;
							else
								newRow["BuildingCode"] = buildingCode;

							// 如果是手工分摊，计算分摊金额
							if ( isManual )
								newRow["ItemBuildingMoney"] =  sMoney ;
							payout.AddNewRecord(newRow);
						}

						//					// 查看所有的款项是否都分摊过，都分摊过的整个付款单就是已经分摊了的。
						//					bool isAllApportion = true;
						//					foreach ( DataRow drPayoutItem in payout.Tables["PayoutItem"].Rows)
						//					{
						//						if( drPayoutItem.IsNull(""))
						//					}

						DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payout(payout);
						payout.Dispose();
					}
				}

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计金额
				((Label)e.Item.FindControl("lblSumMoney")).Text = this.txtSumMoney.Value;
			}
		}

	}
}
