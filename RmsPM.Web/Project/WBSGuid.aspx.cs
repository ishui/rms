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
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSGuid ��ժҪ˵����
	/// ָʾ��ϸ�б�������ʾ
	/// <author>unm</author>
	/// <date>2004/11/10</date>
	/// <version>1.0</version>
	/// </summary>
	public partial class WBSGuid : System.Web.UI.Page
	{
		private string strTaskGuidCode = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// �ڴ˴������û������Գ�ʼ��ҳ��
				InitPage();
				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"����ָʾ�б�ʧ��");
			}
		}

		/// <summary>
		/// ҳ���ʼ��
		/// </summary>
		private void InitPage()
		{
			this.strTaskGuidCode = Request["TaskGuidCode"]+"";
			ViewState["WBSCode"] = Request["WBSCode"]+"";
			this.lblTitle.Text = "������ʾ";

			CheckRole();
		}

		/// <summary>
		/// ��������
		/// </summary>
		private void LoadData()
		{
			EntityData entity = WBSDAO.GetTaskGuidByCode(strTaskGuidCode);
			if(entity.HasRecord())
			{
				this.tdGuidMan.InnerText =RmsPM.BLL.SystemRule.GetUserName(entity.GetString("TaskGuidPerson")); 
				this.tdDate.InnerText = entity.GetDateTimeOnlyDate("CreateDate");
				this.tdContent.InnerText = entity.GetString("TaskGuidContent");
			}
		
//			string strUsers = "";
//			string strUserNames = "";
//			string strStations = "";
//			string strStationNames = "";
//			BLL.ResourceRule.GetAccessRange(strTaskGuidCode,"0701","070109",ref strUsers,ref strUserNames,ref strStations,ref strStationNames);
			this.lblUser.Text = this.GetGuidUser((string)ViewState["WBSCode"]);//strUserNames + "&nbsp;&nbsp;"+strStationNames;
		}

		private void CheckRole()
		{
			// ���Ȩ��
			User user = (User)Session["User"];
			if(!user.HasResourceRight(this.strTaskGuidCode,"070402"))
			{
				Response.Redirect( "../RejectAccess.aspx" );
				Response.End();
			}
		}

		private string GetGuidUser(string txtWBSCode)
		{			
			string SelectName = "";
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(txtWBSCode);
			if (entityUser.HasRecord())
			{
				DataTable dtUserNew = entityUser.CurrentTable.Copy();					
				
				for (int i = 0; i < dtUserNew.Rows.Count; i++)
				{
					if (dtUserNew.Rows[i]["Type"].ToString() == "5"&&dtUserNew.Rows[i]["ExecuteCode"].ToString()==this.strTaskGuidCode) // �ַ�����ָʾ��Ա
					{
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
						{
							SelectName += (SelectName.Length>0)?",":"";
							SelectName += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
						}
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
						{
							SelectName += (SelectName.Length>0)?",":"";
							SelectName += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
						}
					}
				}
			}
			entityUser.Dispose();
			return SelectName;
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
	}
}
