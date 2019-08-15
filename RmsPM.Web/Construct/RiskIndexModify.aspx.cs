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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// RiskIndexModify ��ժҪ˵����
	/// </summary>
	public partial class RiskIndexModify : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtIndexCode.Value = Request.QueryString["IndexCode"];
				this.txtAct.Value = Request.QueryString["Action"];

				if (this.txtIndexCode.Value == "") 
				{
					this.btnDelete.Visible = false;
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string code = this.txtIndexCode.Value;
				if (code != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.ConstructDAO.GetRiskIndexByCode(code);
					if (entity.HasRecord())
					{
						this.txtIndexName.Value = entity.GetString("IndexName");
						this.txtIndexLevel.Value = entity.GetIntString("IndexLevel");
						this.chkIsDefault.Checked = (entity.GetInt("IsDefault") == 1);
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "����ָ��������"));
					}
					entity.Dispose();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
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

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="code"></param>
		/// <param name="parentCode"></param>
		private void SavaData()
		{
			try
			{				
				string IndexCode = this.txtIndexCode.Value;

				EntityData entity = DAL.EntityDAO.ConstructDAO.GetRiskIndexByCode(IndexCode);
				DataRow dr;
				bool isNew = !entity.HasRecord();


				if (isNew)
				{
					dr = entity.GetNewRecord();

					IndexCode = SystemManageDAO.GetNewSysCode("RiskIndexCode");
					this.txtIndexCode.Value = IndexCode;
					dr["IndexCode"] = IndexCode;
					entity.CurrentTable.Rows.Add(dr);
				}
				else
				{
					dr=entity.CurrentRow;
				}
				
				dr["IndexName"] = this.txtIndexName.Value;
				dr["IndexLevel"] = BLL.ConvertRule.ToInt(this.txtIndexLevel.Value);

				if (this.chkIsDefault.Checked) 
				{
					dr["IsDefault"] = 1;
				}
				else 
				{
					dr["IsDefault"] = 0;
				}
				
				DAL.EntityDAO.ConstructDAO.SubmitAllRiskIndex(entity);
				entity.Dispose();

				//����������¼��Ĭ��ֵ
				if (this.chkIsDefault.Checked) 
				{
					RiskIndexStrategyBuilder sb = new RiskIndexStrategyBuilder();

					sb.AddStrategy( new Strategy( RiskIndexStrategyName.IsDefault,"1"));
					sb.AddStrategy( new Strategy( RiskIndexStrategyName.ExceptIndexCode,IndexCode));

					string sql = sb.BuildMainQueryString();

					QueryAgent qa = new QueryAgent();
					entity = qa.FillEntityData( "RiskIndex",sql );
					qa.Dispose();

					if (entity.HasRecord()) 
					{
						foreach(DataRow drTemp in entity.CurrentTable.Rows) 
						{
							drTemp["IsDefault"] = 0;
						}

						DAL.EntityDAO.ConstructDAO.SubmitAllRiskIndex(entity);
					}

				}
					
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtIndexName.Value.Trim() == "") 
			{
				Hint = "���������ָ��";
				return false;
			}

			if (this.txtIndexLevel.Value.Trim() == "") 
			{
				Hint = "��������ռ���";
				return false;
			}

			if ( txtIndexLevel.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(txtIndexLevel.Value))
				{
					Hint = "���ռ������������ �� ";
					return false;
				}
			}

			return true;
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				SavaData();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.ConstructRule.DeleteRiskIndex(this.txtIndexCode.Value);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ɾ��ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

	}
}
