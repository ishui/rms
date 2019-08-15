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
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SubjectModify ��ժҪ˵����
	/// </summary>
	public partial class SubjectModify : PageBase
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
				this.txtSubjectCode.Value = Request.QueryString["SubjectCode"];
				this.txtOldSubjectCode.Value = this.txtSubjectCode.Value;
				this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];
				this.txtAct.Value = Request.QueryString["Action"];

				//����ʱ���봫�����״���
				if ((this.txtSubjectCode.Value == "") && (this.txtSubjectSetCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "�����״��룬��������"));
					return;
				}

				if (this.txtSubjectCode.Value == "") 
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
				string subjectCode = this.txtSubjectCode.Value;
				string subjectSetCode = this.txtSubjectSetCode.Value;

				if (subjectCode != "") 
				{
					EntityData entity = SubjectDAO.GetSubjectByCode(subjectCode,subjectSetCode);
					if (entity.HasRecord())
					{
						this.txtSubjectName.Value = entity.GetString("SubjectName");

						if (entity.GetInt("IsDebit")==1)
						{
							this.CheckBoxIsDebit.Checked=true;
						}
						else
						{
							this.CheckBoxIsDebit.Checked=false;
						}

						if (entity.GetInt("IsCrebit")==1)
						{
							this.CheckBoxIsCrebit.Checked=true;
						}
						else
						{
							this.CheckBoxIsCrebit.Checked=false;
						}
					
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��Ŀ������"));
					}
					entity.Dispose();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ɾ����Ŀ
		/// </summary>
		/// <param name="subjectCode"></param>
		/// <param name="subjectSetCode"></param>
		private void DeleteSubject()
		{
			try
			{
				string subjectCode = this.txtSubjectCode.Value;
				string subjectSetCode = this.txtSubjectSetCode.Value;

				BLL.SubjectRule.DeleteSubject(subjectCode, subjectSetCode);

//				Response.Write(JavaScript.ScriptStart);
//				Response.Write("window.close();"+"\n");
//				string m_strParentCode="";
//				if(subjectCode.Length > 3)
//				{
//					m_strParentCode=(""+Request["SubjectCode"]).Substring(0,(""+Request["SubjectCode"]).Length-2);
//				}
//				Response.Write("window.opener.updateChildNodes('"+m_strParentCode+"','"+((""+Request["SubjectCode"]).Length-1)/2+"');");
//				Response.Write(JavaScript.ScriptEnd);

				
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
				return;
			}

			GoBack();
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

		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint, ref int parentLayer) 
		{
			Hint = "";
			parentLayer = 0;

			string subjectCode = this.txtSubjectCode.Value;
			string subjectSetCode = this.txtSubjectSetCode.Value;
			string ruleCode = SubjectRule.GetSubjectSetRuleCode(subjectSetCode);

			if (subjectCode == "") 
			{
				Hint = "�������Ŀ����";
				return false;
			}

			if (this.txtSubjectName.Value == "") 
			{
				Hint = "�������Ŀ����";
				return false;
			}

//			if (SubjectRule.IsFitRule(subjectCode,ruleCode)==false)
//			{
//				Hint = string.Format("��Ŀ��Ų����Ϲ���{0}�������������룡", ruleCode);
//				return false;
//			}
				
			string parentSubjectCode = SubjectRule.GetParentSubjectCode(subjectCode,ruleCode);
			if (parentSubjectCode!="") 
			{
				EntityData entityParent = SubjectDAO.GetSubjectByCode(parentSubjectCode,subjectSetCode);

				if (!entityParent.HasRecord())
				{
					Hint = string.Format("�ÿ�Ŀ��ŵ��ϼ���Ŀ��{0}�������ڣ����������룡", parentSubjectCode);
					return false;
				}
				else 
				{
					parentLayer = entityParent.GetInt("Layer");
				}
				
				entityParent.Dispose();
			}

			return true;
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string Hint = "";
				int parentLayer = 0;

				if (!CheckValid(ref Hint, ref parentLayer)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				string subjectCode = this.txtSubjectCode.Value;
				string subjectSetCode = this.txtSubjectSetCode.Value;

				EntityData entity = SubjectDAO.GetSubjectByCode(this.txtOldSubjectCode.Value, subjectSetCode);
				DataRow dr = null;
				bool IsNew = false;

				if (entity.HasRecord())
				{
					IsNew = false;
					dr = entity.CurrentRow;

					//�޸�����ʱ����ɾ����������
					if (this.txtSubjectCode.Value != this.txtOldSubjectCode.Value)
					{
						dr.Delete();

						IsNew = true;
						dr = entity.GetNewRecord();
						dr["SubjectSetCode"]=subjectSetCode;
					}
				}
				else
				{
					IsNew = true;
					dr = entity.GetNewRecord();
					dr["SubjectSetCode"]=subjectSetCode;
				}

				dr["SubjectName"] = this.txtSubjectName.Value;
				dr["SubjectCode"] = this.txtSubjectCode.Value;
				dr["Layer"] = parentLayer + 1;
				
				if (this.CheckBoxIsDebit.Checked==true)
				{
					dr["IsDebit"]=1;
				}
				else
				{
					dr["IsDebit"]=0;
				}

				if (this.CheckBoxIsCrebit.Checked==true)
				{
					dr["IsCrebit"]=1;
				}
				else
				{
					dr["IsCrebit"]=0;
				}

				if (IsNew)
				{
					entity.AddNewRecord(dr);
				}

				SubjectDAO.SubmitAllSubject(entity);
				this.txtOldSubjectCode.Value = this.txtSubjectCode.Value;

				entity.Dispose();
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
			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
//			if(act!="Modify")
//			{
//				string m_strParentCode="";
//				if(this.TextBoxSubjectCode.Text.Length>3)
//				{
//					m_strParentCode=this.TextBoxSubjectCode.Text.Substring(0,this.TextBoxSubjectCode.Text.Length-2);
//				}
//				Response.Write("window.opener.updateChildNodes('"+m_strParentCode+"','"+(this.TextBoxSubjectCode.Text.Length-1)/2+"');");
//			}
//			else
//			{
//				Response.Write("window.opener.updateNode('"+this.TextBoxSubjectCode.Text+"');");
//			}
			Response.Write(JavaScript.WinClose(false));
			Response.Write(JavaScript.ScriptEnd);
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			DeleteSubject();
		}
	}
}
