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

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// SystemGroupModify ��ժҪ˵����
	/// </summary>
	public partial class SystemGroupModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnDelete;
	
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
				this.txtGroupCode.Value = Request.QueryString["GroupCode"];
				this.txtParentCode.Value = Request.QueryString["ParentCode"];
				this.txtClassCode.Value = Request.QueryString["ClassCode"];
				this.txtAct.Value = Request.QueryString["Action"];

				//����ʱ���봫��������
				if ((this.txtGroupCode.Value == "") && (this.txtClassCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "�޴�����룬��������"));
					return;
				}

				if (this.txtGroupCode.Value == "") 
				{
					if (this.txtParentCode.Value != "") 
					{
						EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByCode(this.txtParentCode.Value);
						if (!entity.HasRecord()) 
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "��𲻴���"));
							return;
						}
						entity.Dispose();
					}
				}

//				if (this.txtGroupCode.Value == "") 
//				{
//					this.btnDelete.Visible = false;
//				}
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
				string code = this.txtGroupCode.Value;
				if (code != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetSystemGroupByCode(code);
					if (entity.HasRecord())
					{
						this.txtGroupName.Value = entity.GetString("GroupName");
                        this.txtGroupNameEn.Value = entity.GetString("GroupNameEn");
                        this.txtSortID.Value = entity.GetString("SortID");
						this.txtParentCode.Value = entity.GetString("ParentCode");
						this.txtClassCode.Value = entity.GetString("ClassCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��𲻴���"));
						return;
					}
					entity.Dispose();
				}

				string ClassName = BLL.SystemRule.GetFunctionStructureName(this.txtClassCode.Value);
				string ParentName = BLL.SystemGroupRule.GetSystemGroupFullName(this.txtParentCode.Value);
				this.lblParentName.Text = BLL.SystemGroupRule.GetSystemGroupFullNameEx(ParentName, ClassName);
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
		private void SavaData(string code,string parentCode)
		{
			try
			{				
				string curCode="";

				int deep=0;
				string fullID="";
//				int sortID=0;

				if (parentCode.Length>0)
				{
				
					EntityData entityParent = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByCode(parentCode);		
					if (entityParent.HasRecord())
					{
						deep = entityParent.GetInt("Deep");
						fullID = entityParent.GetString("fullID");
					}
					entityParent.Dispose();
				}
				

				EntityData entity=RmsPM.DAL.EntityDAO.SystemManageDAO.GetSystemGroupByCode(code);
				DataRow dr=null;

				if (entity.HasRecord())
				{
					dr=entity.CurrentRow;
				}
				else
				{
					dr=entity.GetNewRecord();
					curCode = SystemManageDAO.GetNewSysCode("SystemGroupCode");
//					sortID = PBSDAO.GetGroupMaxSortID(this.txtClassCode.Value,parentCode);
					dr["GroupCode"]=curCode;
					dr["Deep"]=deep+1;
					if (fullID.Length>0)
					{
						dr["FullID"]=fullID+"-"+curCode;
					}
					else
					{
						dr["FullID"]=curCode;
					}
					dr["ParentCode"] = parentCode;
					dr["ClassCode"] = this.txtClassCode.Value;
//					dr["SortID"]=sortID+1;

					entity.AddNewRecord(dr);
				}
				
				dr["GroupName"] = this.txtGroupName.Value;
                dr["GroupNameEn"] = this.txtGroupNameEn.Value;
                dr["SortID"] = this.txtSortID.Value;
				
				DAL.EntityDAO.SystemManageDAO.SubmitAllSystemGroup(entity);

				entity.Dispose();
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

			if (this.txtGroupName.Value.Trim() == "") 
			{
				Hint = "����������";
				return false;
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

				string parentCode = this.txtParentCode.Value;
				string code = this.txtGroupCode.Value;
				SavaData(code,parentCode);
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
			Response.Write(string.Format("window.opener.Refresh('{0}');", this.txtAct.Value));
			Response.Write(Rms.Web.JavaScript.WinClose(false));

//			Response.Write("window.opener.location = window.opener.location;");
//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
