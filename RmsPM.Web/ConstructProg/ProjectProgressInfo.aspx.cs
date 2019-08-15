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
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// ProjectProgressInfo ��ժҪ˵����
	/// </summary>
	public partial class ProjectProgressInfo : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadData();
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
			try 
			{
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtWBSCode.Value = Request.QueryString["WBSCode"];

				//��Ŀ�µĹ��������
				if (this.txtWBSCode.Value == "") 
				{
					this.txtWBSCode.Value = BLL.ConstructProgRule.GetRootTaskCode(this.txtProjectCode.Value);
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private string GetHref(string WBSCode, string TaskName, bool isCurr) 
		{
			try 
			{
				string s = "";

				if (isCurr) 
				{
					//��ǰ������ʱ��Ҳ��ʾ����
					s = string.Format("<a href=\"#\" onclick=\"GotoTask('{0}');\">{1}</a>", WBSCode, TaskName);
//					s = TaskName;
				}
				else 
				{
					s = string.Format("<a href=\"#\" onclick=\"GotoTask('{0}');\">{1}</a>", WBSCode, TaskName);
				}

				return s;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		private string GetNavigator(string WBSCode) 
		{
			try 
			{
				string Navigator = "";

				//����������ʾ��ǰλ��
				EntityData entity = DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
				if (entity.HasRecord()) 
				{
					string TaskName = entity.GetString("TaskName");
					string ParentCode = entity.GetString("ParentCode");
					Navigator = GetHref(WBSCode, TaskName, true);

					while (ParentCode != "") 
					{
						EntityData entity1 = DAL.EntityDAO.WBSDAO.GetTaskByCode(ParentCode);
						if (entity1.HasRecord()) 
						{
							string CurrWBSCode = entity1.GetString("WBSCode");
							ParentCode = entity1.GetString("ParentCode");
							TaskName = entity1.GetString("TaskName");

							Navigator = GetHref(CurrWBSCode, TaskName, false) + " -> " + Navigator;
						}
						else 
						{
							ParentCode = "";
						}
						entity1.Dispose();
					}
				}
				entity.Dispose();

				return Navigator;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		private void LoadData() 
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value.Trim();

				//this.lblNavigator.Text = GetNavigator(WBSCode);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			string FromUrl = this.txtFromUrl.Value.Trim();
			if (FromUrl != "") 
			{
				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}

	}
}
