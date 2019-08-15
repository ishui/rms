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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.Web;

namespace RmsPM.Web.Document
{
	/// <summary>
	/// DocumentTypeModify ��ժҪ˵����
	/// </summary>
	public partial class DocumentTypeModify : PageBase
	{
//		private bool iIsFolder;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
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
				this.txtDocumentTypeCode.Value = Request.QueryString["DocumentTypeCode"];
				this.txtParentCode.Value = Request.QueryString["ParentCode"];
				this.txtAct.Value = Request.QueryString["Action"];
			
				switch ( this.txtAct.Value.ToLower())
				{
					case "addchild":
						this.tdTitle.InnerText = "�ĵ���������";
						break;

					case "insert":
						this.tdTitle.InnerText = "�ĵ���������";
						break;

					case "modify":
						this.tdTitle.InnerText = "�ĵ������޸�";
						break;
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����"));
			}
		}

		private void LoadData()
		{
			try 
			{
				if ( this.txtDocumentTypeCode.Value != "" ) 
				{
					EntityData entity = DocumentDAO.GetDocumentTypeByCode(this.txtDocumentTypeCode.Value);

					if(entity.HasRecord())
					{
						this.txtTypeName.Value = entity.GetString("TypeName");
						this.txtDescription.Value = entity.GetString("Description");
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "�ĵ����Ͳ�����"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
					}
					entity.Dispose();
				}

				this.lblParentName.Text = BLL.DocumentRule.Instance().GetDocumentTypeName(this.txtParentCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾ�ĵ����ʹ���");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�ĵ����ʹ���"));
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		private void SaveData()
		{
			try 
			{
				string DocumentTypeCode = this.txtDocumentTypeCode.Value;
				string ParentCode = this.txtParentCode.Value;

				EntityData entity = DocumentDAO.GetDocumentTypeByCode(DocumentTypeCode);
				bool isNew = !entity.HasRecord();
				DataRow dr;

				if (isNew) 
				{
					dr = entity.CurrentTable.NewRow();
					DocumentTypeCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("DocumentType");
					dr["DocumentTypeCode"] = DocumentTypeCode;
					entity.CurrentTable.Rows.Add(dr);

					int deep = 1;
					string FullCode = DocumentTypeCode;
					int SortID = 1;

					if (ParentCode != "") 
					{
						EntityData entityP = DAL.EntityDAO.DocumentDAO.GetDocumentTypeByCode(ParentCode);
						if (entityP.HasRecord()) 
						{
							deep = entityP.GetInt("deep") + 1;
							FullCode = entityP.GetString("FullCode") + "-" + DocumentTypeCode;
						}
						entityP.Dispose();
					}

					EntityData entityC = DAL.EntityDAO.DocumentDAO.GetDocumentTypeChildByParentCode(ParentCode);
					if (entityC.HasRecord()) 
					{
						DataView dv = new DataView(entityC.CurrentTable, "", "SortID desc", DataViewRowState.CurrentRows);
						if (dv.Count > 0) 
						{
							SortID = BLL.ConvertRule.ToInt(dv[0]["SortID"]) + 1;
						}
					}
					entityC.Dispose();

					dr["ParentCode"] = ParentCode;
					dr["deep"] = deep;
					dr["FullCode"] = FullCode;
					dr["SortID"] = SortID;
				}
				else 
				{
					dr = entity.CurrentRow;
				}
				entity.Dispose();

				dr["TypeName"] = this.txtTypeName.Value;
				dr["Description"] = this.txtDescription.Value;

				if (isNew) 
				{
					DocumentDAO.InsertDocumentType(entity);
				}
				else 
				{
					DocumentDAO.UpdateDocumentType(entity);
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

			if (this.txtTypeName.Value.Trim() == "") 
			{
				Hint = "�������ĵ���������";
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

				SaveData();

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
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
