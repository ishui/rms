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
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.Sal
{
	/// <summary>
	/// ImportSalOldDlg ��ժҪ˵����
	/// </summary>
	public partial class ImportSalOldDlg : PageBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtRefreshFunc;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.divProjectName.InnerText = this.txtProjectName.Value;

			if (!Page.IsPostBack) 
			{
				IniPage();
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

		}
		#endregion

		private void IniPage() 
		{
			this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			this.txtRefreshScript.Value = Request.QueryString["RefreshScript"];
			this.txtProjectName.Value = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
			this.divProjectName.InnerText = this.txtProjectName.Value;

//			BLL.PageFacade.LoadProjectSelect(this.sltProject, this.txtProjectCode.Value);

			//			if (this.txtProjectCode.Value == "") 
			//			{
			//				Response.Write(Rms.Web.JavaScript.Alert(true, "����Ŀ���룬����ѡ����Ŀ"));
			//				Response.End();
			//			}
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (this.txtFile.PostedFile.FileName == "") 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ���ļ�"));
				return;
			}

			string ProjectCode = this.txtProjectCode.Value.Trim();
			if (ProjectCode == "") 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ���Ӧ��Ŀ"));
				return;
			}

			try
			{
				string jd = this.txtJd.Value.Trim();

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

				//��1���Ǳ���
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
				}

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();

					ImportSalSingle(s, ProjectCode, jd);

				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				throw ex;
			}

			Response.Write(JavaScript.ScriptStart);
			Response.Write(JavaScript.Alert(false,"�������"));
			if (this.txtRefreshScript.Value.Trim() != "")
			{
				Response.Write(string.Format("window.opener.{0}", this.txtRefreshScript.Value));
			}
			else 
			{
				Response.Write("window.opener.location = window.opener.location;");
			}
			Response.Write(JavaScript.WinClose(false));
			Response.Write(JavaScript.ScriptEnd);
			Response.End();
		}

		private void ImportSalSingle(string s, string ProjectCode, string jd) 
		{
			string[] m_strSub = s.Replace("\"","").Split(',');

			if (m_strSub.Length < 9)// && (m_strSub[0] != "��Ӧ�̴���"))
				return;

			//С�ơ��ϼƣ����ͻ�����Ϊ�գ�����
			if (m_strSub[1].Trim() == "")
				return;

			//���˻���
			if (jd != "") 
			{
				if (m_strSub[4] != jd) 
					return;
			}

			string ContractCode = m_strSub[2];

			using(StandardEntityDAO dao=new StandardEntityDAO("SalContract"))
			{
				dao.BeginTrans();
				try
				{
					ImportSalContract(m_strSub, ContractCode, ProjectCode, dao);
					ImportSalPay(m_strSub, ContractCode, ProjectCode, dao);
//					ImportSalPayPlan(m_strSub, ContractCode, ProjectCode, dao);
//					ImportSalPayRela(m_strSub, ContractCode, ProjectCode, dao);

//					EntityData entitySalContract = ImportSalContract(ContractCode, ProjectCode, dsSrc, dao);
//					EntityData entitySalPay = ImportSalPay(ContractCode, ProjectCode, dsSrc, dao);

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

		private void ImportSalContract(string[] m_strSub, string ContractCode, string ProjectCode, StandardEntityDAO dao) 
		{
			try 
			{
				dao.EntityName = "SalContract";
				EntityData entity = new EntityData("SalContract");
				entity = dao.SelectbyPrimaryKey(ContractCode);
				DataRow dr;

				if (entity.HasRecord()) 
				{
					dr = entity.CurrentTable.Rows[0];
				}
				else 
				{
					dr = entity.CurrentTable.NewRow();
				}

				dr["ProjectCode"] = ProjectCode;
				dr["ContractCode"] = ContractCode;
				dr["ContractID"] = m_strSub[2];
				dr["ClientName"] = m_strSub[1];
				dr["Room"] = m_strSub[6];

				try 
				{
					dr["UnitPrice"] = decimal.Parse(m_strSub[8]);
				}
				catch 
				{
					dr["UnitPrice"] = 0;
				}

				dr["BofangCode"] = m_strSub[3];
				dr["BuildingName"] = m_strSub[4];
				dr["ChamberName"] = m_strSub[5];

				try 
				{
					dr["BuildDim"] = decimal.Parse(m_strSub[7]);
				}
				catch 
				{
					dr["BuildDim"] = 0;
				}

				//����ͬ��ȡ��Ӧ�̴���
				//					string SuplName = GetSuplNameByContract(dr["ContractID"].ToString());
				//					string SuplCode = GetSuplCodeByName(SuplName, ProjectCode);
				//					dr["SuplCode"] = SuplCode;

				if (!entity.HasRecord()) 
				{
					entity.AddNewRecord(dr);
					dao.InsertEntity(entity);
				}
				else 
				{
					dao.UpdateEntity(entity);
				}

				entity.Dispose();
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		private void ImportSalPay(string[] m_strSub, string ContractCode, string ProjectCode, StandardEntityDAO dao) 
		{
			try 
			{
				dao.EntityName = "SalPay";
				EntityData entity = new EntityData("SalPay");

				string[] os = {"@ContractCode"};
				object[] ob = {ContractCode};
				dao.FillEntity(SqlManager.GetSqlStruct("SalPay","SelectByContract").SqlString,os,ob,entity,"SalPay");

				if (entity.HasRecord()) 
				{
					dao.DeleteAllRow(entity);
					dao.DeleteEntity(entity);
				}

				DataRow dr;

				dr = entity.CurrentTable.NewRow();

				dr["ProjectCode"] = ProjectCode;
				dr["ContractCode"] = ContractCode;
				dr["ContractID"] = m_strSub[2];
				dr["PayCode"] = m_strSub[2];
				dr["AccountCode"] = m_strSub[2];

				try 
				{
					dr["PayMoney"] = m_strSub[9];
				}
				catch 
				{
					dr["PayMoney"] = 0;
				}

				entity.AddNewRecord(dr);
				dao.InsertEntity(entity);

				entity.Dispose();
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		/*
		private void ImportSalPayPlan(string[] m_strSub, string ContractCode, string ProjectCode, StandardEntityDAO dao) 
		{
			try 
			{
				dao.EntityName = "SalPayPlan";
				EntityData entity = new EntityData("SalPayPlan");

				string[] os = {"@ContractCode"};
				object[] ob = {ContractCode};
				dao.FillEntity(SqlManager.GetSqlStruct("SalPayPlan","SelectByContract").SqlString,os,ob,entity,"SalPayPlan");

				if (entity.HasRecord()) 
				{
					dao.DeleteAllRow(entity);
					dao.DeleteEntity(entity);
				}

				DataRow dr;

				dr = entity.CurrentTable.NewRow();

				dr["ProjectCode"] = ProjectCode;
				dr["ContractCode"] = ContractCode;
				dr["PayPlanCode"] = m_strSub[2];

				try 
				{
					dr["PlanMoney"] = m_strSub[9];
				}
				catch 
				{
					dr["PlanMoney"] = 0;
				}

				entity.AddNewRecord(dr);
				dao.InsertEntity(entity);

				entity.Dispose();
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		private void ImportSalPayRela(string[] m_strSub, string ContractCode, string ProjectCode, StandardEntityDAO dao) 
		{
			try 
			{
				dao.EntityName = "SalPayRela";
				EntityData entity = new EntityData("SalPayRela");

				string[] os = {"@ContractCode"};
				object[] ob = {ContractCode};
				dao.FillEntity(SqlManager.GetSqlStruct("SalPayRela","SelectByContract").SqlString,os,ob,entity,"SalPayRela");

				if (entity.HasRecord()) 
				{
					dao.DeleteAllRow(entity);
					dao.DeleteEntity(entity);
				}

				DataRow dr;

				dr = entity.CurrentTable.NewRow();

				dr["ProjectCode"] = ProjectCode;
				dr["ContractCode"] = ContractCode;
				dr["SystemID"] = m_strSub[2];
				dr["PayPlanCode"] = m_strSub[2];
				dr["PayCode"] = m_strSub[2];

				try 
				{
					dr["PayMoney"] = m_strSub[9];
				}
				catch 
				{
					dr["PayMoney"] = 0;
				}

				entity.AddNewRecord(dr);
				dao.InsertEntity(entity);

				entity.Dispose();
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}
*/

		protected void btnClear_ServerClick(object sender, System.EventArgs e)
		{
			string ProjectCode = this.txtProjectCode.Value.Trim();
			if (ProjectCode == "") 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ���Ӧ��Ŀ"));
				return;
			}

			//ɾ������Ŀ����������
			BLL.DtsPayRule.ClearDtsPay(ProjectCode);

			Response.Write(Rms.Web.JavaScript.Alert(true, "�����"));
		}

	}

}
