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
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// TaskBudgetModify ��ժҪ˵����
	/// </summary>
	public partial class TaskBudgetModify : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			string wbsCode = Request["WBSCode"]+"";
			this.lblTaskName.Text = BLL.WBSRule.GetWBSName(wbsCode);
		}


		private void LoadData()
		{
			string wbsCode = Request["WBSCode"]+"";
			try
			{
				EntityData entity=DAL.EntityDAO.WBSDAO.GetStandard_WBSByCode( wbsCode);
				entity.Tables["TaskBudget"].Columns.Add("PayConditionHtml");
				entity.Tables["TaskBudget"].Columns.Add("CostName");
				entity.SetCurrentTable("TaskBudget");
				int iCount = entity.Tables["TaskBudget"].Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					string taskBudgetCode = entity.GetString("TaskBudgetCode");
					string costCode = entity.GetString("CostCode");
					entity.CurrentRow["CostName"]=BLL.CBSRule.GetCostName(costCode);

					//��������
					entity.CurrentRow["PayConditionHtml"] = BLL.WBSRule.GetTaskBudgetPayConditionHtml(taskBudgetCode, entity.Tables["TaskBudgetCondition"], true);
				}
				AddNewEmptyRow(entity,wbsCode,"TaskBudget","TaskBudgetCode",5);
				this.dgCostList.DataSource = entity.Tables["TaskBudget"];
				this.dgCostList.DataBind();
				Session["WBSEntity"]=entity;
				entity.Dispose();
			}
			catch ( Exception ex )
			{ApplicationLog.WriteLog(this.ToString(),ex,"");}

		}

		private void AddNewEmptyRow( EntityData entity , string wbsCode, string tableName, string keyColumnName, int rows  )
		{
			for ( int i=0;i<rows;i++)
			{
				DataRow dr = entity.GetNewRecord(tableName);
				dr[keyColumnName]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyColumnName);
				dr["WBSCode"]=wbsCode;

				if (entity.Tables[tableName].Columns.Contains("PayConditionHtml")) 
				{
					//��������
					dr["PayConditionHtml"] = BLL.WBSRule.GetTaskBudgetPayConditionHtml(dr[keyColumnName].ToString(), entity.Tables["TaskBudgetCondition"], true);
				}

				entity.AddNewRecord(dr,tableName);
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
			this.dgCostList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCostList_DeleteCommand);

		}
		#endregion

		protected void btnNewItem_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string wbsCode = Request["WBSCode"] + "" ;
				string msg = SaveToSession();
				EntityData entity = (EntityData)Session["WBSEntity"];
				AddNewEmptyRow(entity,wbsCode,"TaskBudget","TaskBudgetCode",5);

				this.dgCostList.DataSource=new DataView( entity.Tables["TaskBudget"] ,"" ,"",DataViewRowState.CurrentRows) ;
				this.dgCostList.DataBind();
				Session["WBSEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������ϸ����" + ex.Message));
			}
		}


		private string  SaveToSession()
		{

			string alertMsg = "";
			try
			{
				string wbsCode = Request["WBSCode"] + "" ;

				EntityData entity = (EntityData)Session["WBSEntity"];

				foreach ( DataGridItem li in this.dgCostList.Items)
				{
					string itemName = ((HtmlInputText)li.FindControl("txtItemName")).Value.Trim();
					string planningPayDate = ((AspWebControl.Calendar)li.FindControl("dtPlanningPayDate")).Value;
					string costCode = ((HtmlInputHidden)li.FindControl("txtcostCode")).Value.Trim();
					string costName = BLL.CBSRule.GetCostName(costCode);
					string money =((HtmlInputText)li.FindControl("txtMoney")).Value.Trim();

					if ( costCode != "" )
					{
						if ( ! CBSRule.CheckCBSLeafNode(costCode))
						{
							costCode="";
							alertMsg += itemName + "�����������ָ������ϸ������ ��" + "\n";
						}
					}

					if ( money != "" )
					{
						if ( ! Rms.Check.StringCheck.IsNumber(money))
						{
							money="";
							alertMsg += itemName + "������ʽ����ȷ ��" + "\n";
						}
					}

					string taskBudgetCode = li.Cells[0].Text;
					foreach ( DataRow dr in entity.Tables["TaskBudget"].Select(String.Format( "TaskBudgetCode='{0}'"  ,taskBudgetCode )))
					{
						dr["CostCode"]=costCode;
						dr["CostName"]=costName;
						dr["ItemName"]=itemName;
						if ( money !="" )
							dr["Money"]=decimal.Parse(money);
						else
							dr["Money"]=decimal.Zero;

						if ( planningPayDate != "" )
							dr["PlanningPayDate"] = planningPayDate;
						else
							dr["PlanningPayDate"] = System.DBNull.Value;
					}
				}

				Session["WBSEntity"]=entity;
				entity.Dispose();
				
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
			return alertMsg;

		}

		
		private void ClearData()
		{
			try
			{
				string wbsCode = Request["WBSCode"]+"";
				EntityData entity = (EntityData)Session["WBSEntity"];

				// �������
				foreach ( DataRow dr in entity.Tables["TaskBudget"].Select("","",DataViewRowState.CurrentRows  ))
				{
					string taskBudgetCode = (string)Session["TaskBudgetCode"];
					bool isDelete = false;
					if ( dr.IsNull("ItemName"))
					{
						isDelete=true;
					}
					else if ( (string)dr["ItemName"] == "" )
					{
						isDelete=true;
					}

					if ( dr.IsNull("Money"))
					{
						isDelete=true;
						continue;
					}
					else if ( (decimal)dr["Money"] == decimal.Zero )
					{
						isDelete=true;
					}

					if ( dr.IsNull("PlanningPayDate"))
					{
						isDelete=true;
					}

					if ( isDelete )
					{
						foreach ( DataRow drCondition in entity.Tables["TaskBudgetCondition"].Select(String.Format( "TaskBudgetCode='{0}'",taskBudgetCode ),"",DataViewRowState.CurrentRows))
							drCondition.Delete();
						dr.Delete();
					}
				}

				Session["WBSEntity"]=entity;
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}

		private void dgCostList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string msg = SaveToSession();
				string wbsCode = Request["WBSCode"] +"" ;

				string codeTemp = e.Item.Cells[0].Text;

				EntityData entity = (EntityData)Session["WBSEntity"];
				foreach ( DataRow dr in entity.Tables["TaskBudgetCondition"].Select(String.Format( "TaskBudgetCode='{0}'" ,codeTemp)))
					dr.Delete();

				//ɾ����ظ�������
				foreach ( DataRow dr in entity.Tables["TaskBudget"].Select( String.Format("TaskBudgetCode='{0}'" ,codeTemp ) ))
					dr.Delete();

				this.dgCostList.DataSource=new DataView( entity.Tables["TaskBudget"] ,"" , "",DataViewRowState.CurrentRows) ;
				this.dgCostList.DataBind();

				Session["WBSEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
			}
		}

		protected void btnPayConditionReturn_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string msg = SaveToSession();
				EntityData entity = (EntityData)Session["WBSEntity"];

				//���¸���ʱ��
				string taskBudgetCode = this.txtTaskBudgetCode.Value;
				string sPayDate = this.txtConditionPayDate.Value;
				if (sPayDate != "") 
				{
					DataRow[] drs = entity.Tables["TaskBudget"].Select("TaskBudgetCode='" + taskBudgetCode + "'");
					if (drs.Length > 0) 
					{
						drs[0]["PlanningPayDate"] = sPayDate;
					}
				}

				this.dgCostList.DataSource=new DataView( entity.Tables["TaskBudget"] ,"" ,"",DataViewRowState.CurrentRows) ;
				this.dgCostList.DataBind();

				Session["WBSEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ������ϸ����" + ex.Message));
			}
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string msg = SaveToSession();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
					return;
				}
				ClearData();

				EntityData entity = (EntityData)Session["WBSEntity"];
				DAL.EntityDAO.WBSDAO.SubmitAllStandard_WBS(entity);
				entity.Dispose();

				CloseWindow();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ������ϸ����" + ex.Message));
			}
		}


		private void CloseWindow()
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
