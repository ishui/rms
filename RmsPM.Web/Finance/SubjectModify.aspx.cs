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
	/// SubjectModify 的摘要说明。
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

				//新增时必须传入帐套代码
				if ((this.txtSubjectCode.Value == "") && (this.txtSubjectSetCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "无帐套代码，不能新增"));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
						Response.Write(Rms.Web.JavaScript.Alert(true, "科目不存在"));
					}
					entity.Dispose();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 删除科目
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			GoBack();
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

		/// 有效性检查
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
				Hint = "请输入科目代码";
				return false;
			}

			if (this.txtSubjectName.Value == "") 
			{
				Hint = "请输入科目名称";
				return false;
			}

//			if (SubjectRule.IsFitRule(subjectCode,ruleCode)==false)
//			{
//				Hint = string.Format("科目编号不符合规则“{0}”，请重新输入！", ruleCode);
//				return false;
//			}
				
			string parentSubjectCode = SubjectRule.GetParentSubjectCode(subjectCode,ruleCode);
			if (parentSubjectCode!="") 
			{
				EntityData entityParent = SubjectDAO.GetSubjectByCode(parentSubjectCode,subjectSetCode);

				if (!entityParent.HasRecord())
				{
					Hint = string.Format("该科目编号的上级科目“{0}”不存在，请重新输入！", parentSubjectCode);
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

					//修改主键时，先删除，再新增
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
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 返回
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
