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
using System.Text;

using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;


namespace RmsPM.Web.Project
{
	/// <summary>
	/// CBSModify 的摘要说明。
	/// </summary>
	public partial class CBSModify : PageBase
	{


	
		protected void Page_Load(object sender, System.EventArgs e)
		{

			if (!IsPostBack)
			{
				string costCode = Request["CostCode"] + "";
				string act = Request["Action"] + "";
				ArrayList ar = null;
				if ( act != "AddChild" )
				{
					ar = user.GetCBSResourceRight(costCode);
					if ( !ar.Contains("040101"))
					{
						Response.Redirect( "../RejectAccess.aspx" );
						Response.End();
					}
				}

				IniPage();
				LoadData();

				if ( act != "AddChild" )
				{
					if ( ! ar.Contains("040102"))
					{
						this.btnAddChild.Visible = false;
						this.btnBatchModify.Visible = false;
						this.btnDelete.Visible = false;
						this.btnSave.Visible = false;
					}
				}

			}
		}


		private void IniPage()
		{
			string act = Request["Action"] + "";
			string projectCode = Request["ProjectCode"]+"";
			string costCode = Request["CostCode"] + "";
			this.ucInputSubject.ProjectCode = projectCode;
			this.txtSubjectSetCode.Value = BLL.ProjectRule.GetSubjectSetCodeByProject(projectCode);
            this.ucParent.ProjectCode = projectCode;

            switch (act)
			{
				case "AddChild":
//                    BLL.PageFacade.LoadCBSParentSelect(this.sltParent, costCode, projectCode, "");

                    this.ucParent.Value = costCode;

                    EntityData parentCBS = DAL.EntityDAO.CBSDAO.GetCBSByCode(costCode);
					if ( parentCBS.HasRecord())
					{
						this.txtSortID.Text = parentCBS.GetString("SortID");
//						this.labelParentCostName.Text = parentCBS.GetString("SortID")+" " + parentCBS.GetString("CostName") ;
						this.ucInputSubject.Value = parentCBS.GetString("SubjectCode");
					}
					parentCBS.Dispose();

//                    this.sltParent.Value = costCode;

					this.btnDelete.Visible = false;
					this.btnAddChild.Visible = false;
					this.btnBatchModify.Visible = false;
					break;
				case "Modify":
//                    BLL.PageFacade.LoadCBSParentSelect(this.sltParent, "", projectCode, costCode);
                    //					this.labelParentCostName.Text=BLL.CBSRule.GetParentCostName(costCode)  ;
					break;
			}

		}

		private void LoadData()
		{
			string projectCode = Request["ProjectCode"] + "";
			string act = Request["Action"] + "";;
			string costCode = Request["CostCode"] + "";;

			if ( act != "Modify" ) 
			{
				this.tableList.Visible = false;
				return;
			}

			try
			{
				EntityData entity=CBSDAO.GetCBSByCode(costCode);
				if (entity.HasRecord())
				{
					this.txtCostName.Text=entity.GetString("CostName");
					this.txtDescription.Text=entity.GetString("Description");
					this.txtCostAllocationDescription.Text=entity.GetString("CostAllocationDescription");
					this.txtSortID.Text = entity.GetString("SortID");
					string subjectCode = entity.GetString("SubjectCode");
					this.ucInputSubject.ProjectCode = projectCode;
					this.ucInputSubject.Value = subjectCode;

					this.ucBudgetType.Value = entity.GetString("BudgetType");

//                    this.sltParent.Value = entity.GetString("ParentCode");

                    this.ucParent.Value = entity.GetString("ParentCode");

                    int deep = entity.GetInt("Deep");
					string fullCode = entity.GetString("FullCode");

//					int isFixed = entity.GetInt("IsFixed");
//					if ( isFixed == 1)
//					{
//						this.btnBatchModify.Visible = false;
//						this.btnDelete.Visible = false;
//						this.btnSave.Visible = false;
//					}

					EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
					this.repeatList.DataSource = new DataView( cbs.CurrentTable, String.Format( "FullCode like '{0}%' and Deep={1} " , fullCode , (deep+1)  ), "SortID" ,DataViewRowState.CurrentRows );
					this.repeatList.DataBind();
					cbs.Dispose();

				}
				entity.Dispose();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "显示出错：" + ex.Message));
			}
		}

		/*
		//检查这个费用项是否已经做了预算， 如果做了预算，就不能删除 
		private bool CheckDelete ( string costCode )
		{
			string projectCode = Request["ProjectCode"] + "";
			bool isOK = false;
			
			try
			{
				V_BudgetCostStrategyBuilder sb = new V_BudgetCostStrategyBuilder();
				sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.ProjectCode,projectCode) );
				sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.CostCode,costCode) );

				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("V_BudgetCost",sql);
				qa.Dispose();

				if ( !entity.HasRecord())
					isOK = true;
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "删除出错：" + ex.Message));
			}
			return isOK;
		}
		*/

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
			string projectCode = Request["ProjectCode"] + "";
			string act = Request["Action"] + "";;
			string costCode = Request["CostCode"] + "";
			string subjectSetCode = this.txtSubjectSetCode.Value;

			if (this.txtCostName.Text.Trim().Length==0)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"请填写费用名称 ！"));
				return;
			}

			if (this.txtSortID.Text.Trim().Length==0)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"请填写费用项编号 ！"));
				return;
			}

            /*
            if (this.ucParent.Value.Trim().Length == 0)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "请填写上级费用项 ！"));
                return;
            }
            */

            if (act == "Modify")
            {
                if (this.ucParent.Value != "")
                {
                    if (costCode == this.ucParent.Value)
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "上级费用项不能是自己 ！"));
                        return;
                    }

                    if (BLL.CBSRule.IsChildCBS(this.ucParent.Value, costCode))
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "上级费用项不能是自己的子项 ！"));
                        return;
                    }
                }
            }


//			子项费用项对应到科目
//			string subjectCode = this.txtSubjectCode.Text.Trim();
//			if (this.txtSubjectCode.Text.Trim().Length==0)
//			{
//				Response.Write(Rms.Web.JavaScript.Alert(true,"请填写科目编号 ！"));
//				return;
//			}

			string subjectCode = "";
			if ( this.ucInputSubject.Value != null )
				subjectCode = this.ucInputSubject.Value;

			if (subjectCode != "" )
			{
				string subjectName = BLL.SubjectRule.GetSubjectName(subjectCode,this.txtSubjectSetCode.Value);
				if ( subjectName == "" )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"不存在这个科目编号 ！"));
					return;
				}
			}


			try
			{
				bool isNew = false;
				string currentCode = "";
				string parentCode = "";
				int parentDeep = 0;
				string parentFullCode = "";
				int deep =0 ;

				EntityData entity = null;
				DataRow dr = null;

				if ( act == "AddChild" )
				{
					isNew = true;
					parentCode = this.ucParent.Value;
					if ( parentCode != "" )
					{
						EntityData parent = DAL.EntityDAO.CBSDAO.GetCBSByCode(costCode);
						parentDeep = parent.GetInt("Deep");
						parentFullCode = parent.GetString("FullCode");
						parent.Dispose();
					}
					
					currentCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostCode");
                    this.txtTempCostCode.Value = currentCode;
					entity = new EntityData("CBS");
					dr = entity.GetNewRecord();
					dr["CostCode"] = currentCode;
					dr["ParentCode"] = parentCode;

					if ( parentFullCode == "" )
						dr["FullCode"] = currentCode;
					else
						dr["FullCode"] = parentFullCode + "-" + currentCode;

					deep = parentDeep + 1;
					dr["Deep"] = deep ;
					dr["ProjectCode"] = projectCode;
					dr["SubjectSetCode"] = subjectSetCode;
				}
				else if ( act == "Modify" )
				{
					isNew = false;
					entity = DAL.EntityDAO.CBSDAO.GetCBSByCode(costCode );
					dr = entity.CurrentRow;
				}
				
				dr["CostName"]=this.txtCostName.Text;
				dr["Description"]=this.txtDescription.Text;
				dr["SortID"] = this.txtSortID.Text.Trim();
				dr["CostAllocationDescription"]=txtCostAllocationDescription.Text;

				dr["SubjectCode"] = subjectCode;
				dr["BudgetType"] = this.ucBudgetType.Value;

				if ( isNew )
				{
					entity.AddNewRecord(dr);
					DAL.EntityDAO.CBSDAO.InsertCBS(entity);
				}
				else
				{
					DAL.EntityDAO.CBSDAO.UpdateCBS(entity);
				}

                //修改上级费用项
                if (entity.GetString("ParentCode") != this.ucParent.Value)
                {
                    BLL.CBSRule.UpdateCBSParent(costCode, this.ucParent.Value);
                }
                
                entity.Dispose();

				CloseWindow();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 删除费用项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string CostCode = Request["CostCode"] + "";
			if (  CostCode == "" ) return;

			try
			{
				BLL.CBSRule.DeleteCBS(CostCode);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			CloseWindow();
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			SavaData();
            if(!string.IsNullOrEmpty(this.txtTempCostCode.Value)&&user.HasRight("110301"))
            {
                string Url = "";

                Url = string.Format("'../Systems/SystemGroupInfo.aspx?GroupCode={0}&ClassCode=0401'", this.txtTempCostCode.Value);
                string k = "window.open(" + Url + ",'设置权限');";
                Response.Write(JavaScript.ScriptStart);
                Response.Write(k);
                Response.Write(JavaScript.ScriptEnd);

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
