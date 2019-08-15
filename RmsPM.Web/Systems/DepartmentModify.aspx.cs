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
using System.IO;

using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.BLL;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// DepartmentModify 的摘要说明。
	/// </summary>
	public partial class DepartmentModify : PageBase
	{
	

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			string action = Request.QueryString["Action"] + "";
			string unitCode = Request["UnitCode"] + "";
			this.txtInputUnitCode.Value = unitCode;
			this.txtAction.Value = action;
			this.txtRefreshScript.Value = Request["RefreshScript"] + "";

			RmsPM.BLL.PageFacade.LoadAllUserSelect(this.sltPrincipal,"");
			//RmsPM.BLL.PageFacade.LoadSubjectSetSelect(this.sltSubjectSet,"");
			switch( action )
			{
				case "Insert":
					this.LabelTitle.Text=((Request.QueryString["UnitCode"]!=null&&Request.QueryString["UnitCode"]!="")?"新增子部门":"新增部门");

					try
					{
						EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitByCode(unitCode);
						if ( entity.HasRecord())
						{
							this.lblParentUnitName.Text = entity.GetString("UnitName");
                            //2006-12-22 日添加 
                            parentUnit.Value = unitCode;
                            parentUnitName.Value = entity.GetString("UnitName");
                            ///////////////
						}
					}
					catch( Exception ex )
					{
						ApplicationLog.WriteLog(this.ToString(),ex,"读取部门节点错误");
						Response.Write(Rms.Web.JavaScript.Alert(true, "读取部门节点错误"));
					}


					break;
				case "Modify":
					this.LabelTitle.Text="修改部门信息";
					break;
			}

		}

		private void LoadData()
		{
			string action = this.txtAction.Value;
			string unitCode = this.txtInputUnitCode.Value;

			if ( action != "Modify")
				return;

			if ( unitCode == "" )
				return;

			try
			{
				EntityData entity = DAL.EntityDAO.OBSDAO.GetStandard_UnitByCode(unitCode);
				if ( entity.HasRecord())
				{
					this.txtSortID.Text = entity.GetString("SortID");
					//this.sltSubjectSet.Value  = entity.GetString("SubjectSetCode");
					this.sltUnitType.Value = entity.GetString("UnitType");
                    //if ( entity.GetInt("SelfAccount")==1)
                    //{
                    //    this.chkSelfAccount.Checked = true;
                    //}
                    
					this.TextBoxName.Text = entity.GetString("UnitName");
                    
                    this.parentUnit.Value = entity.GetString("ParentUnitCode");
                    EntityData entityParent = DAL.EntityDAO.OBSDAO.GetStandard_UnitByCode(this.parentUnit.Value);
                    if (entityParent.HasRecord())
                    {
                        this.parentUnitName.Value = entityParent.GetString("UnitName");
                    }
                    entityParent.Dispose();

                    this.TextBoxRemark.Text = entity.GetString("Remark");
					this.sltPrincipal.Value = entity.GetString("Principal");
				}

				//显示财务编码
				this.ucInputSubjectSet.LoadData(entity.Tables["UnitSubjectSet"]); 

				entity.Dispose();
                
				if (unitCode == "00000") 
				{
					this.sltUnitType.Disabled = false;
					//this.sltSubjectSet.Disabled = false;
					//this.chkSelfAccount.Disabled = false;
				}
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"读取部门节点错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "读取部门节点错误"));
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



		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string action = this.txtAction.Value;
			string unitCode = this.txtInputUnitCode.Value;
			string parentSubjectSetCode = "";

			string unitName = this.TextBoxName.Text.Trim();
			if ( unitName == "" )
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"请填写部门名称 ！"));
				return;
			}

            string parentUnitName = this.parentUnitName.Value.Trim();
            if (parentUnitName == "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "请填写上级部门名称 ！"));
                return;
            }

			string principal = this.sltPrincipal.Value;
//			if ( principal == "" )
//			{
//				Response.Write(Rms.Web.JavaScript.Alert(true,"请指定部门负责人 ！"));
//				return;
//			}
            StreamWriter sw=null;
			try
			{

				EntityData entity = null;

				DataRow dr = null;
				if ( action == "Modify")
				{
                    
					entity = DAL.EntityDAO.OBSDAO.GetStandard_UnitByCode(unitCode);
					dr = entity.CurrentRow;
                   
                   if (this.parentUnit.Value == dr["UnitCode"].ToString())
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "不能将部门设置为自身的子部门 ！"));
                        entity.Dispose();
                        return;
                    }

                    EntityData parent = DAL.EntityDAO.OBSDAO.GetUnitByCode(this.parentUnit.Value);
                    int parentDeep = parent.GetInt("Deep");
                    dr["ParentUnitCode"] = this.parentUnit.Value;
                    dr["Deep"] = parentDeep + 1;
                    dr["FullCode"] = parent.GetString("FullCode") +"-"+ dr["UnitCode"].ToString();
				}
				else
				{
					entity = new EntityData("Standard_Unit");
					string tempCode =DAL.EntityDAO.SystemManageDAO.GetNewSysCode("UnitCode");

					int parentDeep = 0 ;
					string parentFullCode = "";

					//取父节点资料
					if ( unitCode != "" )
					{

						EntityData parent = DAL.EntityDAO.OBSDAO.GetUnitByCode(unitCode);
						parentDeep = parent.GetInt("Deep") ;
						parentFullCode = parent.GetString("FullCode");
						parentSubjectSetCode = parent.GetString("SubjectSetCode");
						parent.Dispose();
					}
					dr = entity.GetNewRecord();

					dr["UnitCode"] =  tempCode ;
					//					dr["ProjectCode"] = "P1010";

					dr["ParentUnitCode"] = unitCode;
					dr["Deep"] = parentDeep + 1;
					if ( parentFullCode != "" )
						dr["FullCode"] = parentFullCode + "-" + tempCode;
					else
						dr["FullCode"] = tempCode;

					entity.AddNewRecord(dr);

				}

				dr["UnitName"] = unitName;
				dr["Principal"] = principal;
				dr["Remark"] = this.TextBoxRemark.Text;
				dr["UnitType"] = this.sltUnitType.Value;
				dr["SortID"] = this.txtSortID.Text;

                
                //if ( this.sltUnitType.Value == "公司"  )
                //{
                //    //if ( this.chkSelfAccount.Checked )
                //    //{
                //    //    dr["SelfAccount"] = 1;
                //    //    dr["SubjectSetCode"] = this.sltSubjectSet.Value;
                //    //}
                //    //else
                //    //{
                //    //    dr["SelfAccount"] = 0;
                //    //    dr["SubjectSetCode"] = parentSubjectSetCode;
                //    //}
                //}
                //else
                //{
                //    dr["SelfAccount"] = 0;
                //    dr["SubjectSetCode"] = "";
                //}

				//保存财务编码
				this.ucInputSubjectSet.SaveData(entity.Tables["UnitSubjectSet"], dr["UnitCode"].ToString());

				DAL.EntityDAO.OBSDAO.SubmitAllStandard_Unit(entity);
				entity.Dispose();

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				if (this.txtRefreshScript.Value.Trim() != "") 
				{
					Response.Write( "window.opener." + this.txtRefreshScript.Value.Trim());
				}
				else 
				{
                    Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                    Response.Write("window.opener.parent.frames['Left'].location.reload();");

				}

              	Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错"));
			}
		}
	}
}
