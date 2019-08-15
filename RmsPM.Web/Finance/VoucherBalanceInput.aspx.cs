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
	/// VoucherBalanceInput 的摘要说明。
	/// </summary>
	public partial class VoucherBalanceInput : PageBase//System.Web.UI.Page
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
				string subjectSetCode = BLL.ProjectRule.GetSubjectSetCodeByProject(projectCode);

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

				//取文件名
				string filename = this.txtFile.PostedFile.FileName;
				filename = filename.Substring(filename.LastIndexOf("\\")+1);
				int ipos = filename.LastIndexOf(".");
				if (ipos >= 0)
				{
					filename = filename.Substring(0, ipos);
				}

				//常量定义
				int CVoucherType = 4;

				try
				{
					EntityData CBS = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);

					//检查费用项是否已建立
					if (!CBS.HasRecord())
						throw new Exception("未建立费用项，不能导入凭证");

					//总帐成本凭证ID为“C”开头，后跟导入文件名
					string VoucherID = "C" + filename;
					string VoucherCode = "";

					DeleteSingle(projectCode, VoucherID);

					EntityData voucher = new EntityData("Standard_Voucher");
					EntityData payout = new EntityData("Standard_Payout");
					EntityData payment = new EntityData("Standard_Payment");

					voucher.Tables["Voucher"].Columns.Add("SupplierCode");
					voucher.Tables["Voucher"].Columns.Add("SupplierName");

					//凭证表新增一条  begin----------------------------------------------
					DataRow drV = null ;
					voucher.SetCurrentTable("Voucher");
					drV = voucher.GetNewRecord();
					VoucherCode = BLL.PaymentRule.GetNextVoucherCode();
					drV["VoucherCode"] = VoucherCode;
					drV["ProjectCode"] = projectCode;
                    drV["SubjectSetCode"] = subjectSetCode;
					drV["VoucherID"] = VoucherID;
					drV["VoucherType"] = CVoucherType;
					drV["MakeDate"] = DateTime.Today;
					drV["Status"]=2;
					drV["Accountant"] = user.UserCode;
					drV["AccountDate"] = DateTime.Now;
					drV["IsExported"] = 1;
					drV["ExportDate"] = DateTime.Now;
					drV["CheckPerson"] = user.UserCode;
					drV["CheckDate"] = DateTime.Now;

					voucher.AddNewRecord(drV);

					//供应商名称
					drV["SupplierName"] = "开发成本";

					//凭证表新增一条  end----------------------------------------------

					StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

					// 第一行跳过
					m_sr.ReadLine();
					string strTemp;
					while ( (strTemp = m_sr.ReadLine()) != null  )
					{

						string [] sFields = strTemp.Split(new char[]{','});
						for ( int i=0;i<sFields.Length ; i++)
						{
							sFields[i] = sFields[i].Replace( "\"","" );
						}

						if (sFields.Length >= 10) 
						{
							//凭证分录  begin-----------------------------------------------------
							voucher.SetCurrentTable("VoucherDetail");

							//方向
							string dir = sFields[8];

							//科目
							string SubjectCode = sFields[3];

							//余额
							string Balance = sFields[9];

							if (Balance.StartsWith("￥"))
							{
								Balance = Balance.Substring(1, Balance.Length - 1);
							}

							//只导：方向为“借”、科目不为空、余额不为空
							if ((dir == "借") && (SubjectCode != "") && (Balance != ""))
							{
								decimal dBalance = BLL.ConvertRule.ToDecimal(Balance);
								if (dBalance != 0) 
								{
									DataRow drVD;

									//借 begin----------------------------------------------
									//一个科目一条借金额
									DataRow[] drsVD = voucher.CurrentTable.Select("SubjectCode='" + SubjectCode + "' and DebitMoney <> 0");
									if (drsVD.Length > 0)
									{
										drVD = drsVD[0];
									}
									else
									{
										drVD = voucher.GetNewRecord();

										drVD["VoucherDetailCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("VoucherDetailCode");
										drVD["VoucherCode"] = VoucherCode;
										drVD["Summary"] = "开发成本";

										drVD["CrebitMoney"] = decimal.Zero;

										drVD["SubjectCode"] = SubjectCode;

										drVD["ProjectCode"] = projectCode;
										drVD["RelaType"] = "付款";

										voucher.AddNewRecord(drVD);
									}

									drVD["DebitMoney"] = dBalance;

									//借 end----------------------------------------------

									//贷 begin--------------------------------------------
									//一个科目一条借金额
									drsVD = voucher.CurrentTable.Select("SubjectCode='" + SubjectCode + "' and CrebitMoney <> 0");
									if (drsVD.Length > 0)
									{
										drVD = drsVD[0];
									}
									else
									{
										drVD = voucher.GetNewRecord();

										drVD["VoucherDetailCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("VoucherDetailCode");
										drVD["VoucherCode"] = VoucherCode;
										drVD["Summary"] = "开发成本";

										drVD["DebitMoney"] = decimal.Zero;

										drVD["SubjectCode"] = SubjectCode;

										drVD["ProjectCode"] = projectCode;
										drVD["RelaType"] = "付款";

										voucher.AddNewRecord(drVD);
									}

									drVD["CrebitMoney"] = dBalance;

									//贷 end--------------------------------------------
								}
							}
						}

						//凭证分录  end-----------------------------------------------------
					}
					m_sr.Close();

					// 补付款和请款
					if (drV != null)
					{
						// 付款
						payout.SetCurrentTable("Payout");
						DataRow drP  = payout.GetNewRecord();
						string payoutCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");

						drP["PayoutCode"]=payoutCode;
						drP["ProjectCode"]=projectCode;
						drP["PayoutDate"]=drV["MakeDate"];

						//现金的科目以“101”开头
						if ( voucher.Tables["VoucherDetail"].Select( String.Format( " VoucherCode='{0}' and SubjectCode like '101' " , VoucherCode )).Length>0)  
							drP["PaymentType"]="现金";
						else
							drP["PaymentType"]="支票";

						drP["Payer"] = "补单";
						drP["SupplyCode"] = drV["SupplierCode"];
						drP["SupplyName"] = drV["SupplierName"];

						drP["PayoutID"] = VoucherID;
						DataRow[] drsCre = voucher.Tables["VoucherDetail"].Select(String.Format( " VoucherCode='{0}' and CrebitMoney>0 " , VoucherCode ));
						decimal payoutMoney = BLL.MathRule.SumColumn( drsCre,"CrebitMoney" );
						drP["Money"] = payoutMoney;

						// 凭证的总金额项
						drV["TotalMoney"] = payoutMoney;

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
						drPM["PaymentID"] = VoucherID;
						drPM["VoucherID"]=VoucherID;

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
						foreach ( DataRow drVD in voucher.Tables["VoucherDetail"].Select(String.Format( " VoucherCode='{0}' and DebitMoney <> 0 " , VoucherCode )))
						{
							string subjectCode = BLL.ConvertRule.ToString(drVD["SubjectCode"]);
							
							DataRow drPMItem = payment.GetNewRecord();
							string paymentItemCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentItemCode");
							drPMItem["PaymentItemCode"]=paymentItemCode;
							drPMItem["PaymentCode"]=paymentCode;
							drPMItem["ItemMoney"]=drVD["DebitMoney"];
							drPMItem["CostCode"]=GetCostCode(subjectCode,CBS);

							//款项：取科目名称
							string summary = "";
							summary = BLL.SubjectRule.GetSubjectName(subjectCode, subjectSetCode);
							if (summary == "")
							{
								summary = BLL.ConvertRule.ToString(drVD["Summary"]);
							}
							drPMItem["Summary"] = summary;

							drPMItem["AlloType"]="P";
							payment.AddNewRecord(drPMItem);

							DataRow drPD = payout.GetNewRecord();
							drPD["PayoutItemCode"]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemCode");
							drPD["PayoutCode"]=payoutCode;
							drPD["PayoutMoney"]=drVD["DebitMoney"];
							drPD["SubjectCode"]=subjectCode;
							drPD["Remark"]=drPMItem["Summary"];
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

		/// <summary>
		/// 删除所有系统导入的总帐凭证
		/// </summary>
		/// <param name="ProjectCode"></param>
		private static void DeleteAll(string ProjectCode)
		{
			try 
			{
				QueryAgent qa = new QueryAgent();
				try 
				{
					//取所有系统导入的总帐凭证(ID为“C”开头)
					string sql = string.Format("select VoucherID from voucher where ProjectCode = '{0}' and VoucherID like 'C%'", ProjectCode);
					DataSet ds = qa.ExecSqlForDataSet(sql);

					foreach(DataRow dr in ds.Tables[0].Rows)
					{
						string VoucherID = BLL.ConvertRule.ToString(dr["VoucherID"]);
						DeleteSingle(ProjectCode, VoucherID);
					}
				}
				finally 
				{
					qa.Dispose();
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		private static void DeleteSingle(string ProjectCode, string VoucherID)
		{
			try 
			{
				string VoucherCode = "";

				//取系统的凭证号
				QueryAgent qa = new QueryAgent();
				try 
				{
					VoucherCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select VoucherCode from Voucher where VoucherID = '{0}' and ProjectCode='{1}'", VoucherID, ProjectCode)));
				}
				finally
				{
					qa.Dispose();
				}

				//删除老的凭证、请款、付款
				//事务处理
				using(StandardEntityDAO dao = new StandardEntityDAO("Voucher"))
				{
					dao.BeginTrans();
					try
					{
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
						qa = new Rms.ORMap.QueryAgent();
						try 
						{
							PaymentCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select PaymentCode from Payment where PaymentID = '{0}' and ProjectCode='{1}'", VoucherID, ProjectCode)));
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
							PayoutCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select PayoutCode from Payout where PayoutID = '{0}' and ProjectCode='{1}'", VoucherID, ProjectCode)));
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
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string ProjectCode = Request["ProjectCode"] + "" ; 
				DeleteAll(ProjectCode);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog (this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "清空失败：" + ex.Message));
			}
		}

	}
}
