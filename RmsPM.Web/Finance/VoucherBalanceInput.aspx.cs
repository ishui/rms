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
	/// VoucherBalanceInput ��ժҪ˵����
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
				string subjectSetCode = BLL.ProjectRule.GetSubjectSetCodeByProject(projectCode);

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

				//ȡ�ļ���
				string filename = this.txtFile.PostedFile.FileName;
				filename = filename.Substring(filename.LastIndexOf("\\")+1);
				int ipos = filename.LastIndexOf(".");
				if (ipos >= 0)
				{
					filename = filename.Substring(0, ipos);
				}

				//��������
				int CVoucherType = 4;

				try
				{
					EntityData CBS = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);

					//���������Ƿ��ѽ���
					if (!CBS.HasRecord())
						throw new Exception("δ������������ܵ���ƾ֤");

					//���ʳɱ�ƾ֤IDΪ��C����ͷ����������ļ���
					string VoucherID = "C" + filename;
					string VoucherCode = "";

					DeleteSingle(projectCode, VoucherID);

					EntityData voucher = new EntityData("Standard_Voucher");
					EntityData payout = new EntityData("Standard_Payout");
					EntityData payment = new EntityData("Standard_Payment");

					voucher.Tables["Voucher"].Columns.Add("SupplierCode");
					voucher.Tables["Voucher"].Columns.Add("SupplierName");

					//ƾ֤������һ��  begin----------------------------------------------
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

					//��Ӧ������
					drV["SupplierName"] = "�����ɱ�";

					//ƾ֤������һ��  end----------------------------------------------

					StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

					// ��һ������
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
							//ƾ֤��¼  begin-----------------------------------------------------
							voucher.SetCurrentTable("VoucherDetail");

							//����
							string dir = sFields[8];

							//��Ŀ
							string SubjectCode = sFields[3];

							//���
							string Balance = sFields[9];

							if (Balance.StartsWith("��"))
							{
								Balance = Balance.Substring(1, Balance.Length - 1);
							}

							//ֻ��������Ϊ���衱����Ŀ��Ϊ�ա���Ϊ��
							if ((dir == "��") && (SubjectCode != "") && (Balance != ""))
							{
								decimal dBalance = BLL.ConvertRule.ToDecimal(Balance);
								if (dBalance != 0) 
								{
									DataRow drVD;

									//�� begin----------------------------------------------
									//һ����Ŀһ������
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
										drVD["Summary"] = "�����ɱ�";

										drVD["CrebitMoney"] = decimal.Zero;

										drVD["SubjectCode"] = SubjectCode;

										drVD["ProjectCode"] = projectCode;
										drVD["RelaType"] = "����";

										voucher.AddNewRecord(drVD);
									}

									drVD["DebitMoney"] = dBalance;

									//�� end----------------------------------------------

									//�� begin--------------------------------------------
									//һ����Ŀһ������
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
										drVD["Summary"] = "�����ɱ�";

										drVD["DebitMoney"] = decimal.Zero;

										drVD["SubjectCode"] = SubjectCode;

										drVD["ProjectCode"] = projectCode;
										drVD["RelaType"] = "����";

										voucher.AddNewRecord(drVD);
									}

									drVD["CrebitMoney"] = dBalance;

									//�� end--------------------------------------------
								}
							}
						}

						//ƾ֤��¼  end-----------------------------------------------------
					}
					m_sr.Close();

					// ����������
					if (drV != null)
					{
						// ����
						payout.SetCurrentTable("Payout");
						DataRow drP  = payout.GetNewRecord();
						string payoutCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");

						drP["PayoutCode"]=payoutCode;
						drP["ProjectCode"]=projectCode;
						drP["PayoutDate"]=drV["MakeDate"];

						//�ֽ�Ŀ�Ŀ�ԡ�101����ͷ
						if ( voucher.Tables["VoucherDetail"].Select( String.Format( " VoucherCode='{0}' and SubjectCode like '101' " , VoucherCode )).Length>0)  
							drP["PaymentType"]="�ֽ�";
						else
							drP["PaymentType"]="֧Ʊ";

						drP["Payer"] = "����";
						drP["SupplyCode"] = drV["SupplierCode"];
						drP["SupplyName"] = drV["SupplierName"];

						drP["PayoutID"] = VoucherID;
						DataRow[] drsCre = voucher.Tables["VoucherDetail"].Select(String.Format( " VoucherCode='{0}' and CrebitMoney>0 " , VoucherCode ));
						decimal payoutMoney = BLL.MathRule.SumColumn( drsCre,"CrebitMoney" );
						drP["Money"] = payoutMoney;

						// ƾ֤���ܽ����
						drV["TotalMoney"] = payoutMoney;

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
						drPM["PaymentID"] = VoucherID;
						drPM["VoucherID"]=VoucherID;

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
						foreach ( DataRow drVD in voucher.Tables["VoucherDetail"].Select(String.Format( " VoucherCode='{0}' and DebitMoney <> 0 " , VoucherCode )))
						{
							string subjectCode = BLL.ConvertRule.ToString(drVD["SubjectCode"]);
							
							DataRow drPMItem = payment.GetNewRecord();
							string paymentItemCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentItemCode");
							drPMItem["PaymentItemCode"]=paymentItemCode;
							drPMItem["PaymentCode"]=paymentCode;
							drPMItem["ItemMoney"]=drVD["DebitMoney"];
							drPMItem["CostCode"]=GetCostCode(subjectCode,CBS);

							//���ȡ��Ŀ����
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

		/// <summary>
		/// ɾ������ϵͳ���������ƾ֤
		/// </summary>
		/// <param name="ProjectCode"></param>
		private static void DeleteAll(string ProjectCode)
		{
			try 
			{
				QueryAgent qa = new QueryAgent();
				try 
				{
					//ȡ����ϵͳ���������ƾ֤(IDΪ��C����ͷ)
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
		/// ���
		/// </summary>
		private static void DeleteSingle(string ProjectCode, string VoucherID)
		{
			try 
			{
				string VoucherCode = "";

				//ȡϵͳ��ƾ֤��
				QueryAgent qa = new QueryAgent();
				try 
				{
					VoucherCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select VoucherCode from Voucher where VoucherID = '{0}' and ProjectCode='{1}'", VoucherID, ProjectCode)));
				}
				finally
				{
					qa.Dispose();
				}

				//ɾ���ϵ�ƾ֤��������
				//������
				using(StandardEntityDAO dao = new StandardEntityDAO("Voucher"))
				{
					dao.BeginTrans();
					try
					{
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
						qa = new Rms.ORMap.QueryAgent();
						try 
						{
							PaymentCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select PaymentCode from Payment where PaymentID = '{0}' and ProjectCode='{1}'", VoucherID, ProjectCode)));
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
							PayoutCode = BLL.ConvertRule.ToString(qa.ExecuteScalar(String.Format("select PayoutCode from Payout where PayoutID = '{0}' and ProjectCode='{1}'", VoucherID, ProjectCode)));
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
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// ���
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
				Response.Write(JavaScript.Alert(true, "���ʧ�ܣ�" + ex.Message));
			}
		}

	}
}
