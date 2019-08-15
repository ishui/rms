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
	/// VoucherInput ��ժҪ˵����
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

		}
		#endregion

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string projectCode=Request["ProjectCode"] + "" ; 
				if (this.txtFile.PostedFile.FileName == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ���ļ���"));
					return;
				}

				if ( this.txtFile.PostedFile.ContentLength == 0 )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "���ļ��ǿ��ļ���"));
					return;
				}

				if ( this.inputSystemGroupPayout.Value == "" )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ�񸶿����ͣ�"));
					return;
				}
				
				if ( this.inputSystemGroupPayment.Value == "" )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ��������ͣ�"));
					return;
				}

				try
				{
					EntityData CBS = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);

					//���������Ƿ��ѽ���
					if (!CBS.HasRecord())
						throw new Exception("δ������������ܵ���ƾ֤");

					/*----------------------------��¼Ҫ�����ƾ֤�ţ������ж����--------------------------*/
					DataTable tbV = new DataTable();
					tbV.Columns.Add("VoucherCode");
					tbV.Columns.Add("VoucherID");

					ArrayList arr = new ArrayList();

					StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

					// ��һ������
					m_sr.ReadLine();
					string strTemp;
					while ( (strTemp = m_sr.ReadLine()) != null  )
					{
						arr.Add(strTemp);

						//��¼ƾ֤��
						string [] sFields = strTemp.Split(new char[]{','});
						for ( int i=0;i<sFields.Length ; i++)
						{
							sFields[i] = sFields[i].Replace( "\"","" );
						}

						string VoucherID = "B" + sFields[2];
						if (tbV.Select("VoucherID='" + VoucherID + "'").Length == 0) 
						{
							string VoucherCode = "";

							//ȡϵͳ��ƾ֤��
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

					//ɾ���ϵ�ƾ֤��������
					foreach(DataRow drV in tbV.Rows) 
					{
						//������
						using(StandardEntityDAO dao = new StandardEntityDAO("Voucher"))
						{
							dao.BeginTrans();
							try
							{
								string VoucherCode = drV["VoucherCode"].ToString();
								string VoucherID = drV["VoucherID"].ToString();

								if (VoucherCode != "")
								{
									//ɾ��ƾ֤
									dao.EntityName = "Standard_Voucher";
									EntityData entityV = DAL.EntityDAO.PaymentDAO.GetStandard_VoucherByCode(VoucherCode);
									if (entityV.HasRecord())
									{
										dao.DeleteAllRow(entityV);
										dao.DeleteEntity(entityV);
									}
									entityV.Dispose();
								}

								//ȡϵͳ������
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

								//ɾ�����
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

								//ȡϵͳ�ĸ����
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

								//ɾ������
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
									//RollBackTrans�ᱨ���� SqlTransaction ����ɣ�����Ҳ�޷�ʹ��
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
							throw new Exception("ֻ�ܵ��븶��ƾ֤");
						}

//						if ( type.Length == 1 )
//							type = "0" + type;

						string voucherID = "B" + sFields[2];

						// ƾ֤��
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

						//���ѵĹ�Ӧ�̴���
						string UFSupplierCode = sFields[18];
						string SupplierCode = "";
						if (UFSupplierCode != "")
						{
							//�����Ѵ���ȡϵͳ����
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

						//��Ӧ������
						string supplierName = "";
						if ( sFields.Length > 66 )
							supplierName = sFields[65];
						drV["SupplierName"]=supplierName;

						//ƾ֤��¼  begin-----------------------------------------------------
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
						drVD["RelaType"]="����";

						//���ѵ���Ŀ���룬��ϵͳ����Ŀ����
						drVD["UFProjectCode"] = sFields[20];

						//��Ӧ�̴���
						drVD["CustCode"] = SupplierCode;

						voucher.AddNewRecord(drVD);

						//ƾ֤��¼  end-----------------------------------------------------
					}
					m_sr.Close();

					// ����������
					voucher.SetCurrentTable("Voucher");
					foreach ( DataRow drV in voucher.Tables["Voucher"].Rows)
					{
						string voucherCode = (string)drV["VoucherCode"];
						string voucherID = (string)drV["VoucherID"];

						// ����
						payout.SetCurrentTable("Payout");
						DataRow drP  = payout.GetNewRecord();
						string payoutCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");

						drP["PayoutCode"]=payoutCode;
						drP["ProjectCode"]=projectCode;
						drP["PayoutDate"]=drV["MakeDate"];

						//�ֽ�Ŀ�Ŀ�ԡ�101����ͷ
						if ( voucher.Tables["VoucherDetail"].Select( String.Format( " VoucherCode='{0}' and SubjectCode like '101' " , voucherCode )).Length>0)  
							drP["PaymentType"]="�ֽ�";
						else
							drP["PaymentType"]="֧Ʊ";

						drP["Payer"]="����";
						drP["SupplyCode"] = drV["SupplierCode"];
						drP["SupplyName"] = drV["SupplierName"];

						drP["PayoutID"] = voucherID;
						DataRow[] drsCre = voucher.Tables["VoucherDetail"].Select(String.Format( " VoucherCode='{0}' and CrebitMoney>0 " , voucherCode ));
						decimal payoutMoney = BLL.MathRule.SumColumn( drsCre,"CrebitMoney" );
						drP["Money"]=payoutMoney;

						// ƾ֤���ܽ����
						drV["TotalMoney"]=payoutMoney;

						if( drsCre.Length>0)
							drP["SubjectCode"]=drsCre[0]["SubjectCode"];

						drP["Status"]=1;
						drP["CheckPerson"]=user.UserCode;
						drP["CheckDate"]=drV["MakeDate"];
						drP["GroupCode"]=this.inputSystemGroupPayout.Value;
						drP["IsApportioned"]=0;
						payout.AddNewRecord(drP);

						// ���
						payment.SetCurrentTable("Payment");
						DataRow drPM = payment.GetNewRecord();
						string paymentCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentCode");
						drPM["PaymentCode"]=paymentCode;
						drPM["ProjectCode"]=projectCode;
						drPM["PaymentID"] = voucherID;
						drPM["VoucherID"]=voucherID;

						drPM["ApplyPerson"]=user.UserCode;
						drPM["ApplyDate"]=drV["MakeDate"];
						drPM["Payer"]="����";
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



						// ����������
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

							// ƾ֤����ر���
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
					Response.Write(JavaScript.Alert(false,"����ɹ� ��"));
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
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
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
