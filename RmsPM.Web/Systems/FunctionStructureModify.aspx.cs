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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;


namespace RmsPM.Web.Systems
{
	/// <summary>
	/// FunctionStructureModify 的摘要说明。
	/// </summary>
	public partial class FunctionStructureModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableRow trSQLScript;



		protected RmsPM.Web.UserControls.InputSubject ucInputSubject;

	
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
			string act = Request["Action"] + "";
			string functionStructureCode = Request["FunctionStructureCode"] + "";
			this.txtInputCode.Value = functionStructureCode;
			switch ( act  )
			{
				case "AddChild":
					ShowParentInfo(functionStructureCode);
					this.btnDelete.Visible = false;
					this.btnAddChild.Visible = false;
					break;
				case "Modify":
					string parentCode = functionStructureCode.Substring(0,(functionStructureCode.Length-2));
					this.txtFunctionStructureCode.Attributes.Add("readonly","true");
					ShowParentInfo(parentCode);
					break;
			}

		}


		private void ShowParentInfo(string parentCode)
		{
			if ( parentCode == "")
				return;
			EntityData parent = DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByCode(parentCode);
			if ( parent.HasRecord())
			{
				this.labelParent.Text = parentCode +" " + parent.GetString("FunctionStructureName") ;
			}
			parent.Dispose();
		}


		private void LoadData()
		{
			string act = Request["Action"] + "";;
			string functionStructureCode = Request["FunctionStructureCode"] + "";;

			if ( act != "Modify" ) 
			{
				this.tableList.Visible = false;
				this.TableSQLScript.Visible = false;	//SQL Script
				return;
			}

			try
			{
				EntityData entity= DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByCode(functionStructureCode);
				if (entity.HasRecord())
				{
					this.txtFunctionStructureCode.Value = functionStructureCode;
					this.txtFunctionStructureName.Value = entity.GetString("FunctionStructureName");
					this.txtDescription.Value=entity.GetString("Description");
					this.txtProjectSpecialDescription.Value=entity.GetString("ProjectSpecialDescription");
					this.txtOtherSpecialDescription.Value = entity.GetString("OtherSpecialDescription");
					this.chkRight.Checked = ( !( entity.GetInt("IsRightControlPoint").ToString() == "1") ) ;
					this.chkRole.Checked = ( ! ( entity.GetInt("IsRoleControlPoint").ToString() == "1" )) ;
					this.chkIsAvailable.Checked = ( ! ( entity.GetInt("IsAvailable").ToString() == "1" )) ;
					this.chkSystemClass.Checked = (entity.GetInt("IsSystemClass").ToString() == "1") ;

					#region 显示功能点 SQL Script
					string strSQLScript="";
					string strColumnName="";
					string strColumnValue="";

					// ******************************************************************************
					//排除字段，因为 enttiy 中有字段并不存在实际 DataTable 中
					ArrayList arrDebarColumn=new ArrayList();
					arrDebarColumn.Add("ChildCount");
					// ******************************************************************************

					int ic=entity.CurrentTable.Columns.Count;
					for(int i=0;i<ic;i++)
					{
						string tempName=entity.CurrentTable.Columns[i].ColumnName;
						object tempValue=entity.CurrentRow[tempName];

						if ( arrDebarColumn.Contains(tempName) )
						{
							continue;
						}

						if ( 0!=i )
						{
							strColumnName+=",";
							strColumnValue+=",";
						}
						strColumnName+="[" + tempName + "]";
						if ( DBNull.Value==tempValue )
						{
							strColumnValue+="Null";
						}
						else
						{
							strColumnValue+="'"+ tempValue.ToString() +"'";
						}
						
					}
					strSQLScript+="\r\n\r\n";
					strSQLScript+="-- Start 新增 当前功能点";
					strSQLScript+="\r\n";
					strSQLScript+="Insert Into [FunctionStructure]";
					strSQLScript+=" (";
					strSQLScript+=strColumnName;
					strSQLScript+=") Values (";
					strSQLScript+=strColumnValue;
					strSQLScript+=")";
					strSQLScript+="\r\n";
					strSQLScript+="-- End 新增 当前功能点";
					strSQLScript+="\r\n\r\n";

					this.txtSQLScript.Value=strSQLScript;
					#endregion

					EntityData child = DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByParentCode(functionStructureCode);
					this.repeatList.DataSource = child.CurrentTable;
					this.repeatList.DataBind();
					child.Dispose();
				}
				entity.Dispose();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "显示出错：" + ex.Message));
			}
		}

		private void DeleteFunctionStructure( string functionStructureCode)
		{
			
			if (  functionStructureCode == "" )
				return;

			try
			{

				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetAllFunctionStructure();
				foreach( DataRow dr in entity.CurrentTable.Select ( String.Format( "FunctionStructureCode like '{0}%' ",functionStructureCode) ))
				{
					dr.Delete();
				}

				DAL.EntityDAO.SystemManageDAO.SubmitAllFunctionStructure(entity);
				entity.Dispose();
				CloseWindow();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "删除出错：" + ex.Message));
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

		private void SavaData()
		{
			string act = Request["Action"] + "";;
			string functionStructureCode = Request["FunctionStructureCode"] + "";

			if (this.txtFunctionStructureName.Value.Trim().Length==0)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"请填写名称 ！"));
				return;
			}

			if (this.txtFunctionStructureCode.Value.Trim().Length==0)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"请填写编号 ！"));
				return;
			}

			try
			{
				string currentCode = "";
				int deep =0 ;

				string parentCode = "";
				int parentDeep = 0;

				EntityData entity = null;
				DataRow dr = null;

				if ( act == "AddChild" )
				{
					parentCode = functionStructureCode;
					if ( parentCode != "" )
					{
						EntityData parent = DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByCode(parentCode);
						parentDeep = parent.GetInt("Deep");
						parent.Dispose();
					}
					
					currentCode = this.txtFunctionStructureCode.Value;

					entity = new EntityData("FunctionStructure");
					dr = entity.GetNewRecord();
					dr["FunctionStructureCode"] = currentCode;
					dr["ParentCode"] = parentCode;

					deep = parentDeep + 1;
					dr["Deep"] = deep ;

					entity.AddNewRecord(dr);

				}
				else if ( act == "Modify" )
				{
					entity = DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByCode(functionStructureCode);
					dr = entity.CurrentRow;
				}
				
				dr["FunctionStructureName"]=this.txtFunctionStructureName.Value;
				dr["Description"]=this.txtDescription.Value;
				dr["ProjectSpecialDescription"] = this.txtProjectSpecialDescription.Value;
				dr["OtherSpecialDescription"]=this.txtOtherSpecialDescription.Value;

				if ( this.chkRight.Checked )
					dr["IsRightControlPoint"]=0;
				else
					dr["IsRightControlPoint"]=1;

				if ( this.chkRole.Checked )
					dr["IsRoleControlPoint"]=0;
				else
					dr["IsRoleControlPoint"]=1;

				if ( this.chkIsAvailable.Checked )
					dr["IsAvailable"]=0;
				else
					dr["IsAvailable"]=1;

				if ( this.chkSystemClass.Checked)
					dr["IsSystemClass"] = 1;
				else
					dr["IsSystemClass"] = 0;

				DAL.EntityDAO.SystemManageDAO.SubmitAllFunctionStructure(entity);
				entity.Dispose();

				// 对该节点以下的子节点做处理
				if ( (act == "Modify") && ( ! this.chkIsAvailable.Checked ) )
				{
					EntityData eee = DAL.EntityDAO.SystemManageDAO.GetAllFunctionStructure();
					foreach( DataRow dr1 in eee.CurrentTable.Select ( String.Format( "FunctionStructureCode like '{0}%' ",functionStructureCode) ))
					{
						dr1["IsAvailable"]=1;
					}
					DAL.EntityDAO.SystemManageDAO.SubmitAllFunctionStructure(eee);
					eee.Dispose();
				}

				CloseWindow();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string functionStructureCode = Request["FunctionStructureCode"] + "";
			DeleteFunctionStructure(functionStructureCode);
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			SavaData();
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
