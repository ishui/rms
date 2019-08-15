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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSModify ��ժҪ˵����
	/// </summary>
	public partial class PBSModify : PageBase
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
				this.txtPBSTypeCode.Value = Request.QueryString["PBSTypeCode"];
				this.txtParentCode.Value = Request.QueryString["ParentCode"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["Action"];

				//����ʱ���봫����Ŀ����
				if ((this.txtPBSTypeCode.Value == "") && (this.txtProjectCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "����Ŀ���룬��������"));
					return;
				}

				if (this.txtPBSTypeCode.Value == "") 
				{
					//����ʱ����Ʒ���ֻ�ܵ�����
					if (this.txtParentCode.Value != "") 
					{
						EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSTypeByCode(this.txtParentCode.Value);
						if (entity.HasRecord()) 
						{
							if (entity.GetInt("deep") > 1) 
							{
								Response.Write(Rms.Web.JavaScript.ScriptStart);
								Response.Write(Rms.Web.JavaScript.Alert(false, "��Ʒ���ֻ�ܵ����������ܼ�������"));
								Response.Write(Rms.Web.JavaScript.WinClose(false));
								Response.Write(Rms.Web.JavaScript.ScriptEnd);
							}
						}
						else 
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "��Ʒ��ϲ�����"));
							return;
						}
						entity.Dispose();
					}
				}

//				if (this.txtPBSTypeCode.Value == "") 
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
				string code = this.txtPBSTypeCode.Value;
				if (code != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.PBSDAO.GetPBSTypeByCode(code);
					if (entity.HasRecord())
					{
						this.txtPBSTypeName.Value = entity.GetString("PBSTypeName");
						this.txtDescription.Value = entity.GetString("Description");
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��Ʒ��ϲ�����"));
					}
					entity.Dispose();
				}

				this.lblParentName.Text = BLL.PBSRule.GetPBSTypeName(this.txtParentCode.Value);
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
				int sortID=0;

				if (parentCode.Length>0)
				{
				
					EntityData entityParent=PBSDAO.GetPBSTypeByCode(parentCode);		
					if (entityParent.HasRecord())
					{
						deep=entityParent.GetInt("Deep");
						fullID=entityParent.GetString("fullID");
					}
					entityParent.Dispose();

				}
				

				EntityData entity=RmsPM.DAL.EntityDAO.PBSDAO.GetPBSTypeByCode(code);
				DataRow dr=null;
				bool IsNew=false;

				if (entity.HasRecord())
				{
					IsNew=false;
					dr=entity.CurrentRow;

				}
				else
				{
					dr=entity.GetNewRecord();
					IsNew=true;
					curCode=SystemManageDAO.GetNewSysCode("PBSTypeCode");
					sortID=PBSDAO.GetPBSTypeMaxSortID(this.txtProjectCode.Value,parentCode);
					dr["PBSTypeCode"]=curCode;
					dr["Deep"]=deep+1;
					if (fullID.Length>0)
					{
						dr["FullID"]=fullID+"-"+curCode;
					}
					else
					{
						dr["FullID"]=curCode;
					}
					dr["ParentCode"]=parentCode;
					dr["ProjectCode"]=this.txtProjectCode.Value;
					dr["SortID"]=sortID+1;
				}
				
				
				dr["PBSTypeName"] = this.txtPBSTypeName.Value;
				dr["Description"] = this.txtDescription.Value;
				
				if ( IsNew )
				{
					entity.AddNewRecord(dr);
					PBSDAO.InsertPBSType(entity);
					
				}
				else
				{	
					PBSDAO.UpdatePBSType(entity);
					
				}

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

			if (this.txtPBSTypeName.Value.Trim() == "") 
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
				string code = this.txtPBSTypeCode.Value;
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
				string code = this.txtPBSTypeCode.Value;
				BLL.PBSRule.DeletePBSType(code);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ɾ��ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

			GoBack();
		}

	}
}
