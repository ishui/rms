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
using System.IO;

using Rms.Interface.UFSoft;
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// VoucherInput 的摘要说明。
	/// </summary>
	public partial class VoucherInput : PageBase//System.Web.UI.Page
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( ! this.IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			this.inputSystemGroupPayout.ClassCode="0602";
			this.inputSystemGroupPayment.ClassCode="0601";
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

		}
		#endregion

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string projectCode=Request["ProjectCode"] + "" ; 
				if (this.txtFile.PostedFile.FileName == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "请选择文件！"));
					return;
				}

				if ( this.txtFile.PostedFile.ContentLength == 0 )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "该文件是空文件！"));
					return;
				}

				if ( this.inputSystemGroupPayout.Value == "" )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "请选择付款类型！"));
					return;
				}
				
				if ( this.inputSystemGroupPayment.Value == "" )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "请选择请款类型！"));
					return;
				}

				try
				{
					EntityData CBS = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);

					//检查费用项是否已建立
					if (!CBS.HasRecord())
						throw new Exception("未建立费用项，不能导入凭证");

					/*----------------------------记录要导入的凭证号（可能有多个）--------------------------*/
					DataTable tbV = new DataTable();
					tbV.Columns.Add("VoucherCode");
					tbV.Columns.Add("VoucherID");

					ArrayList arr = new ArrayList();

					StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

					// 第一行跳过
					m_sr.ReadLine();
					string strTemp;
					while ( (strTemp = m_sr.ReadLine()) != null  )
					{
						arr.Add(strTemp);

						//记录凭证号
						string [] sFields = strTemp.Split(new char[]{','});
						for ( int i=0;i<sFields.Length ; i++)
						{
							sFields[i] = sFields[i].Replace( "\"","" );
						}

						string VoucherID = "B" + sFields[2];
						if (tbV.Select("VoucherID='" + VoucherID + "'").Length == 0) 
						{
							string VoucherCode = "";

							//取系统的凭证号
							QueryAgent qa = new Rms.ORMap.QueryAgent();
							try 
							{
								VoucherCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select VoucherCode from Voucher where VoucherID = '{0}' and ProjectCode='{1}'", VoucherID, projectCode)));
							}
							finally
							{
								qa.Dispose();
							}
						
							DataRow drNew = tbV.NewRow();
							drNew["VoucherCode"] = VoucherCode;
							drNew["VoucherID"] = VoucherID;
							tbV.Rows.Add(drNew);
						}
					}
					m_sr.Close();

					//删除老的凭证、请款、付款
					foreach(DataRow drV in tbV.Rows) 
					{
						//事务处理
						using(StandardEntityDAO dao = new StandardEntityDAO("Voucher"))
						{
							dao.BeginTrans();
							try
							{
								string VoucherCode = drV["VoucherCode"].ToString();
								string VoucherID = drV["VoucherID"].ToString();

								if (VoucherCode != "")
								{
									//删除凭证
									dao.EntityName = "Standard_Voucher";
									EntityData entityV = DAL.EntityDAO.PaymentDAO.GetStandard_VoucherByCode(VoucherCode);
									if (entityV.HasRecord())
									{
										dao.DeleteAllRow(entityV);
										dao.DeleteEntity(entityV);
									}
									entityV.Dispose();
								}

								//取系统的请款单号
								string PaymentCode = "";
								QueryAgent qa = new Rms.ORMap.QueryAgent();
								try 
								{
									PaymentCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select PaymentCode from Payment where PaymentID = '{0}' and ProjectCode='{1}'", VoucherID, projectCode)));
								}
								finally
								{
									qa.Dispose();
								}

								//删除请款
								if (PaymentCode != "")
								{
									dao.EntityName = "Standard_Payment";
									EntityData entityP = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(PaymentCode);
									if (entityP.HasRecord())
									{
										dao.DeleteAllRow(entityP);
										dao.DeleteEntity(entityP);
									}
									entityP.Dispose();
								}

								//取系统的付款单号
								string PayoutCode = "";
								qa = new Rms.ORMap.QueryAgent();
								try 
								{
									PayoutCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select PayoutCode from Payout where PayoutID = '{0}' and ProjectCode='{1}'", VoucherID, projectCode)));
								}
								finally
								{
									qa.Dispose();
								}

								//删除付款
								if (PayoutCode != "")
								{
									dao.EntityName = "Standard_Payout";
									EntityData entityP = DAL.EntityDAO.PaymentDAO.GetStandard_PayoutByCode(PayoutCode);
									if (entityP.HasRecord())
									{
										dao.DeleteAllRow(entityP);
										dao.DeleteEntity(entityP);
									}
									entityP.Dispose();
								}

								dao.CommitTrans();
							}
							catch(Exception ex)
							{
								try 
								{
									//RollBackTrans会报错：此 SqlTransaction 已完成；它再也无法使用
									dao.RollBackTrans();
								}
								catch 
								{
								}

								throw ex;
							}
						}
					}

					EntityData voucher = new EntityData("Standard_Voucher");
					EntityData payout = new EntityData("Standard_Payout");
					EntityData payment = new EntityData("Standard_Payment");

					voucher.Tables["Voucher"].Columns.Add("SupplierCode");
					voucher.Tables["Voucher"].Columns.Add("SupplierName");

					long LastVoucherCode = BLL.ConvertRule.ToLong(BLL.PaymentRule.GetNextVoucherCode()) - 1;

					foreach(string val in arr)
					{

						string [] sFields = val.Split(new char[]{','});
						for ( int i=0;i<sFields.Length ; i++)
						{
							sFields[i] = sFields[i].Replace( "\"","" );
						}
						string std=sFields[0];
						string type = sFields[1];

						if ((type != "2") && (type != "4") && (type != "5"))
						{
							throw new Exception("只能导入付出凭证");
						}

//						if ( type.Length == 1 )
//							type = "0" + type;

						string voucherID = "B" + sFields[2];

						// 凭证表
						string voucherCode = "";
						DataRow drV = null ;
						voucher.SetCurrentTable("Voucher");
						DataRow[] drVs = voucher.CurrentTable.Select( String.Format("VoucherID='{0}'",voucherID) );
						if ( drVs.Length>0 )
						{
							drV= drVs[0];
							voucherCode=(string)drV["VoucherCode"];
						}
						else
						{
							drV = voucher.GetNewRecord();
							LastVoucherCode++;
							voucherCode = LastVoucherCode.ToString();
							drV["VoucherCode"] = voucherCode;

							drV["ProjectCode"]=projectCode;
							drV["VoucherID"]=voucherID;
							drV["VoucherType"]=type;
							drV["MakeDate"]=std;
							drV["Status"]=2;
							drV["Accountant"]=user.UserCode;
							drV["AccountDate"]=std;
							drV["IsExported"] = 1;
							drV["ExportDate"] = DateTime.Now;
							drV["CheckPerson"]=user.UserCode;
							drV["CheckDate"]=std;

							voucher.AddNewRecord(drV);
						}

						//用友的供应商代码
						string UFSupplierCode = sFields[18];
						string SupplierCode = "";
						if (UFSupplierCode != "")
						{
							//按用友代码取系统代码
							QueryAgent qa = new Rms.ORMap.QueryAgent();
							try 
							{
								SupplierCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select SupplierCode from Supplier where U8Code = '{0}'", UFSupplierCode)));
							}
							finally
							{
								qa.Dispose();
							}
						}
						drV["SupplierCode"] = SupplierCode;

						//供应商名称
						string supplierName = "";
						if ( sFields.Length > 66 )
							supplierName = sFields[65];
						drV["SupplierName"]=supplierName;

						//凭证分录  begin-----------------------------------------------------
						voucher.SetCurrentTable("VoucherDetail");
						DataRow drVD = voucher.GetNewRecord();
						drVD["VoucherDetailCode"]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("VoucherDetailCode");
						drVD["VoucherCode"]=voucherCode;
						drVD["Summary"]=sFields[4];

						drVD["SubjectCode"]=sFields[5];
						if ( Rms.Check.StringCheck.IsNumber(sFields[6]))
							drVD["DebitMoney"]=sFields[6];
						else
							drVD["DebitMoney"]=decimal.Zero;

						if ( Rms.Check.StringCheck.IsNumber(sFields[7]))
							drVD["CrebitMoney"]=sFields[7];
						else
							drVD["CrebitMoney"]=decimal.Zero;

						drVD["BillNo"] = sFields[13];
						drVD["ProjectCode"]=projectCode;
						drVD["RelaType"]="付款";

						//用友的项目代码，即系统的项目代码
						drVD["UFProjectCode"] = sFields[20];

						//供应商代码
						drVD["CustCode"] = SupplierCode;

						voucher.AddNewRecord(drVD);

						//凭证分录  end-----------------------------------------------------
					}
					m_sr.Close();

					// 补付款和请款
					voucher.SetCurrentTable("Voucher");
					foreach ( DataRow drV in voucher.Tables["Voucher"].Rows)
					{
						string voucherCode = (string)drV["VoucherCode"];
						string voucherID = (string)drV["VoucherID"];

						// 付款
						payout.SetCurrentTable("Payout");
						DataRow drP  = payout.GetNewRecord();
						string payoutCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");

						drP["PayoutCode"]=payoutCode;
						drP["ProjectCode"]=projectCode;
						drP["PayoutDate"]=drV["MakeDate"];

						//现金的科目以“101”开头
						if ( voucher.Tables["VoucherDetail"].Select( String.Format( " VoucherCode='{0}' and SubjectCode like '101' " , voucherCode )).Length>0)  
							drP["PaymentType"]="现金";
						else
							drP["PaymentType"]="支票";

						drP["Payer"]="补单";
						drP["SupplyCode"] = drV["SupplierCode"];
						drP["SupplyName"] = drV["SupplierName"];

						drP["PayoutID"] = voucherID;
						DataRow[] drsCre = voucher.Tables["VoucherDetail"].Select(String.Format( " VoucherCode='{0}' and CrebitMoney>0 " , voucherCode ));
						decimal payoutMoney = BLL.MathRule.SumColumn( drsCre,"CrebitMoney" );
						drP["Money"]=payoutMoney;

						// 凭证的总金额项
						drV["TotalMoney"]=payoutMoney;

						if( drsCre.Length>0)
							drP["SubjectCode"]=drsCre[0]["SubjectCode"];

						drP["Status"]=1;
						drP["CheckPerson"]=user.UserCode;
						drP["CheckDate"]=drV["MakeDate"];
						drP["GroupCode"]=this.inputSystemGroupPayout.Value;
						drP["IsApportioned"]=0;
						payout.AddNewRecord(drP);

						// 请款
						payment.SetCurrentTable("Payment");
						DataRow drPM = payment.GetNewRecord();
						string paymentCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentCode");
						drPM["PaymentCode"]=paymentCode;
						drPM["ProjectCode"]=projectCode;
						drPM["PaymentID"] = voucherID;
						drPM["VoucherID"]=voucherID;

						drPM["ApplyPerson"]=user.UserCode;
						drPM["ApplyDate"]=drV["MakeDate"];
						drPM["Payer"]="补单";
						drPM["PayDate"]=drV["MakeDate"];
						drPM["Money"]=payoutMoney;

						drPM["CheckPerson"]=user.UserCode;
						drPM["CheckDate"]=drV["MakeDate"];
						drPM["IsContract"]=0;
						drPM["Status"]=2;
						drPM["IsApportion"]=0;
						drPM["SupplyCode"]=drV["SupplierCode"];
						drPM["SupplyName"]=drV["SupplierName"];
						drPM["GroupCode"]=this.inputSystemGroupPayment.Value;
						payment.AddNewRecord(drPM);



						// 付款请款分项
						payout.SetCurrentTable("PayoutItem");
						payment.SetCurrentTable("PaymentItem");
						foreach ( DataRow drVD in voucher.Tables["VoucherDetail"].Select(String.Format( " VoucherCode='{0}' and DebitMoney <> 0 " , voucherCode )))
						{
							string subjectCode = BLL.ConvertRule.ToString(drVD["SubjectCode"]);
							
							DataRow drPMItem = payment.GetNewRecord();
							string paymentItemCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentItemCode");
							drPMItem["PaymentItemCode"]=paymentItemCode;
							drPMItem["PaymentCode"]=paymentCode;
							drPMItem["ItemMoney"]=drVD["DebitMoney"];
							drPMItem["CostCode"]=GetCostCode(subjectCode,CBS);
							drPMItem["Summary"]=drVD["Summary"];
							drPMItem["AlloType"]="P";
							payment.AddNewRecord(drPMItem);

							DataRow drPD = payout.GetNewRecord();
							drPD["PayoutItemCode"]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemCode");
							drPD["PayoutCode"]=payoutCode;
							drPD["PayoutMoney"]=drVD["DebitMoney"];
							drPD["SubjectCode"]=subjectCode;
							drPD["Remark"]=drVD["Summary"];
							drPD["AlloType"]="P";
							drPD["IsManualAlloc"]=0;
							drPD["PaymentItemCode"]=paymentItemCode;
							payout.AddNewRecord(drPD);

							// 凭证的相关编码
							drVD["RelaCode"]=payoutCode;
						}
					}

					DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payout(payout);
					DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Voucher(voucher);
					DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payment(payment);

					voucher.Dispose();
					payout.Dispose();
					payment.Dispose();
					CBS.Dispose();


					Response.Write(JavaScript.ScriptStart);
					Response.Write(JavaScript.Alert(false,"导入成功 ！"));
					Response.Write(JavaScript.OpenerReload(false));
					Response.Write("window.close();");
					Response.Write(JavaScript.ScriptEnd);

				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog (this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "导入失败：" + ex.Message));
			}
		}

		private string GetCostCode ( string subjectCode,EntityData CBS )
		{
			string costCode ="";
			DataRow[] drs = CBS.CurrentTable.Select( String.Format( " SubjectCode='{0}' ",subjectCode ) );
			if ( drs.Length>0)
				costCode= BLL.ConvertRule.ToString( drs[0]["CostCode"] );
			return costCode;
		}

	}
}
