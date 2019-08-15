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
	/// SystemGroupInfo ��ժҪ˵����
	/// </summary>
	public partial class SystemGroupInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputText txtGroupName;
		protected System.Web.UI.UserControl ucGroupTree;
	
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
				this.txtClassCode.Value = Request.QueryString["ClassCode"];
				this.txtAct.Value = Request.QueryString["Action"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

				//Ȩ��
				this.btnAddChild.Visible = user.HasRight("110302"); //����
				this.btnModify.Visible = user.HasRight("110303"); //�޸�
				this.btnDelete.Visible = user.HasRight("110304"); //ɾ��

				this.btnCopy.Visible = user.HasRight("110302");
				this.btnCut.Visible = user.HasRight("110302");
				this.btnPaste.Visible = user.HasRight("110302");

				this.btnAddAccess.Visible = user.HasRight("110305"); //��Ȩ
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
				string GroupCode = this.txtGroupCode.Value;
				string ClassCode = this.txtClassCode.Value;

				if (GroupCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ����������"));
					return;
				}

				if (ClassCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ����������"));
					return;
				}

				if (ClassCode == "0401") 
				{
					//������
					this.txtIsResource.Value = "1";
					this.trToolbar.Style["display"] = "none";

					EntityData entity = DAL.EntityDAO.CBSDAO.GetCBSByCode(GroupCode);
					if (entity.HasRecord())
					{
						this.lblGroupName.Text = entity.GetString("CostName") ;
						this.lblSortID.Text = entity.GetString("SortID");
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "���������"));
						return;
					}
					entity.Dispose();

					string ClassName = BLL.SystemRule.GetFunctionStructureName(this.txtClassCode.Value);
					this.lblParentName.Text = BLL.SystemGroupRule.GetSystemGroupFullNameEx("", ClassName);
				}
				else if (ClassCode == "1603") 
				{
					//��������
					this.trToolbar.Style["display"] = "none";

					EntityData entity = DAL.EntityDAO.OADAO.GetOAFileTypeByCode(GroupCode);
					if (entity.HasRecord())
					{
						this.lblGroupName.Text = entity.GetString("TypeName") ;
						//this.lblSortID.Text = entity.GetString("SortID");
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "�������������"));
						return;
					}
					entity.Dispose();

					string ClassName = BLL.SystemRule.GetFunctionStructureName(this.txtClassCode.Value);
					this.lblParentName.Text = BLL.SystemGroupRule.GetSystemGroupFullNameEx("", ClassName);
				}
				else 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetSystemGroupByCode(GroupCode);
					if (entity.HasRecord())
					{
						this.lblGroupName.Text = entity.GetString("GroupName");
                        this.lblGroupNameEn.Text = entity.GetString("GroupNameEn");
                        this.lblSortID.Text = entity.GetString("SortID");
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��𲻴���"));
						return;
					}
					entity.Dispose();

					string ClassName = BLL.SystemRule.GetFunctionStructureName(this.txtClassCode.Value);
					string ParentName = BLL.SystemGroupRule.GetSystemGroupFullName(this.txtParentCode.Value);
					this.lblParentName.Text = BLL.SystemGroupRule.GetSystemGroupFullNameEx(ParentName, ClassName);
				}

				LoadAccessRange();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ������" + ex.Message));
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
		/// ��ʾȨ���б�
		/// </summary>
		private void LoadAccessRange() 
		{
			try 
			{
				string GroupCode = this.txtGroupCode.Value;
				EntityData entity = null;

				//����Ȩ�޷�Χ-����
				if (this.txtIsResource.Value == "1") 
				{
					entity = DAL.EntityDAO.ResourceDAO.GetAccessRangeByResourceCode(GroupCode);
				}
				else 
				{
					entity = DAL.EntityDAO.ResourceDAO.GetAccessRangeByGroupCode(GroupCode);
				}
				DataTable tb = entity.CurrentTable;

				//��Ȩ�޷�Χ����
				DataTable tbRela = BLL.SystemGroupRule.GetSystemAccessDistinctRelation(tb);

				BLL.SystemGroupRule.AddSystemGroupAccessRelationName(tbRela);
				BLL.SystemGroupRule.AddSystemGroupAccessRelationImage(tbRela);

				//��Ȩ�޷�Χ���ܲ����б�
				BLL.SystemGroupRule.SetSystemAccessDistinctRelationOperationHtml(tbRela, tb);

				BindDataGrid(tbRela);

				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��λȨ�޳���" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
				DataView dv = new DataView(tb, "", "AccessRangeType, RelationName", DataViewRowState.CurrentRows);

				this.dgList.DataSource = dv;
				this.dgList.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��λȨ���б����" + ex.Message));
			}
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
				string code = this.txtGroupCode.Value;
				BLL.SystemGroupRule.DeleteSystemGroup(code);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ɾ��ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			RefreshAfterDelete();
		}

		private void RefreshAfterDelete() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			string ParentCode = this.txtParentCode.Value;
			string ItemType = "G";
			if (ParentCode == "") 
			{
				ParentCode = this.txtClassCode.Value;
				ItemType = "C";
			}

			Response.Write(string.Format("window.parent.frameMain.MyRefreshParent('{0}', '{1}');", ParentCode, ItemType));
//			Response.Write(string.Format("window.parent.frameMain.updateChildNodes('{0}', '{1}');", ParentCode, ItemType));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.parent.location = window.parent.location;");

//			Response.Write("window.location = '../Blank.aspx';");

//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ճ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPaste_ServerClick(object sender, System.EventArgs e)
		{
			string act = "";

			try 
			{
				if (this.txtIsCut.Value == "1") 
				{
					act = "move";
					BLL.SystemGroupRule.MoveSystemGroup(this.txtSrcGroupCode.Value, this.txtGroupCode.Value, this.txtClassCode.Value);
				}
				else
				{
					act = "insert";
					BLL.SystemGroupRule.CopySystemGroup(this.txtSrcGroupCode.Value, this.txtGroupCode.Value, this.txtClassCode.Value);
				}
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ճ��ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			string s = Rms.Web.JavaScript.ScriptStart
				+ string.Format("Refresh('{0}');", act)
				+ Rms.Web.JavaScript.ScriptEnd;
			this.RegisterStartupScript("start", s);
		}
	}
}
