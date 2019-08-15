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
	/// DocumentTypeModify 的摘要说明。
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
						this.tdTitle.InnerText = "文档类型新增";
						break;

					case "insert":
						this.tdTitle.InnerText = "文档类型新增";
						break;

					case "modify":
						this.tdTitle.InnerText = "文档类型修改";
						break;
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面错误"));
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
						Response.Write(Rms.Web.JavaScript.Alert(true, "文档类型不存在"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
					}
					entity.Dispose();
				}

				this.lblParentName.Text = BLL.DocumentRule.Instance().GetDocumentTypeName(this.txtParentCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"显示文档类型错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示文档类型错误"));
			}
		}

		/// <summary>
		/// 保存
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
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtTypeName.Value.Trim() == "") 
			{
				Hint = "请输入文档类型名称";
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
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
