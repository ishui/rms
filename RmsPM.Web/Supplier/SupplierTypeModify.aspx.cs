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

using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;
using Rms.Web;


namespace RmsPM.Web.Supplier
{
	/// <summary>
	/// Supplier 的摘要说明。
	/// </summary>
	public partial class SupplierTypeModify : PageBase
	{
	
		private void Page_Load(object sender, System.EventArgs e)
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
			this.btnSave.ServerClick += new System.EventHandler(this.btnSave_ServerClick);
			this.btnDelete.ServerClick += new System.EventHandler(this.btnDelete_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void IniPage()
		{
			
		}
		private void LoadData()
		{
			string supplierTypeCode = Request["supplierTypeCode"] + "";
			string action = Request["Action"] + "";

			if ( action == "AddChild" )
			{
				this.tableList.Visible = false;
				this.btnAddChild.Visible= false;
				this.btnDelete.Visible = false;
				return;
			}

			try
			{
				EntityData entity=DAL.EntityDAO.ProjectDAO.GetSupplierTypeByCode(supplierTypeCode);
				if(entity.HasRecord())
				{
					this.txtSupplierTypeName.Text=entity.GetString("TypeName");
					this.txtDescription.Text=entity.GetString("Description");
				}
				entity.Dispose();
				EntityData allType = DAL.EntityDAO.ProjectDAO.GetAllSupplierType();
				this.repeatList.DataSource = new DataView( allType.CurrentTable, String.Format( "ParentCode='{0}'",supplierTypeCode ),"",DataViewRowState.CurrentRows );
				this.repeatList.DataBind();
				allType.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string inputSupplierTypeCode = Request["supplierTypeCode"] + "";
			string supplierTypeCode = "";
			string action = Request["Action"] + "";
			try
			{
				EntityData entity = null;
				DataRow dr = null;

				if ( action == "AddChild" )
				{
					string parentCode = inputSupplierTypeCode;
					string parentFullCode = "";
					int parentDeep = 0;
					string fullCode = "";
					if ( parentCode != "" )
					{

						EntityData pEntity = DAL.EntityDAO.ProjectDAO.GetSupplierTypeByCode( parentCode);
						parentFullCode = pEntity.GetString("FullCode");
						parentDeep = pEntity.GetInt("Deep");
						pEntity.Dispose();
					}
					entity = new EntityData("SupplierType");
					dr = entity.GetNewRecord();

					supplierTypeCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("supplierTypeCode");
					dr["supplierTypeCode"] = supplierTypeCode;
					if ( parentCode == "" )
						fullCode = supplierTypeCode;
					else
						fullCode = parentFullCode + "-" + supplierTypeCode;
					dr["parentCode"] = parentCode;
					dr["FullCode"] = fullCode;
					dr["Deep"] = parentDeep + 1;
				}
				else
				{
					supplierTypeCode = inputSupplierTypeCode;
					entity = DAL.EntityDAO.ProjectDAO.GetSupplierTypeByCode(supplierTypeCode);
					dr = entity.CurrentRow;
				}

				dr["TypeName"] = this.txtSupplierTypeName.Text;
				dr["Description"] = this.txtDescription.Text;

				if( action == "AddChild")
				{
					entity.AddNewRecord(dr);
					DAL.EntityDAO.ProjectDAO.InsertSupplierType(entity);
				}
				else
					DAL.EntityDAO.ProjectDAO.UpdateSupplierType(entity);

				entity.Dispose();
				CloseWindow();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		
		}

		private void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string inputSupplierTypeCode = Request["supplierTypeCode"] + "";
			string action = Request["Action"] + "";

			try
			{
				EntityData allType = DAL.EntityDAO.ProjectDAO.GetAllSupplierType();
				DeleteSupplierType(allType,inputSupplierTypeCode);
				DAL.EntityDAO.ProjectDAO.SubmitAllSupplierType(allType);
				allType.Dispose();
				CloseWindow();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void DeleteSupplierType ( EntityData allType, string supplierTypeCode)
		{
			try
			{
				foreach ( DataRow dr in allType.CurrentTable.Select( String.Format( "ParentCode='{0}'" ,supplierTypeCode)))
				{
					string tempCode = (string)dr["SupplierTypeCode"];
					DeleteSupplierType(allType,tempCode);
				}
				foreach ( DataRow dr in allType.CurrentTable.Select( String.Format( "SupplierTypeCode='{0}'" ,supplierTypeCode) ))
					dr.Delete();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void CloseWindow()
		{
			Response.Write(JavaScript.ScriptStart);
			Response.Write( " window.opener.location.reload(); " );
			Response.Write( " if ( window.opener.opener != null ) window.opener.opener.navigate(window.opener.opener.location);  "  );
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
		
		}
	}
}
