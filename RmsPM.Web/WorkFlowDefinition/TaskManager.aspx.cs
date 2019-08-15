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
using Rms.WorkFlow;
using Infragistics.WebUI.UltraWebGrid;

namespace RmsPM.Web.WorkFlowDefinition
{
	/// *******************************************************************************************
	/// <summary>
	/// TaskManager 的摘要说明，任务管理
	/// </summary>
	/// *******************************************************************************************
	public partial class TaskManager : PageBase
	{
	
		/// ****************************************************************************
		/// <summary>
		/// 页面加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				PageInit();
				LoadData();
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// 加载数据
		/// </summary>
		/// ****************************************************************************
		private void LoadData()
		{

			Procedure procedure = (Procedure)Session["Procedure"];
			string taskCode = Request["TaskCode"]+"";
			if ( taskCode == "" )
			{
				this.btnDelete.Visible = false;
				Task task = Rms.WorkFlow.DefinitionManager.NewTask();
				task.ProcedureCode = procedure.ProcedureCode;
				task.IsNew = true;
				Session["Task"] = task;
				return;
			}


			DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);

			try
			{
				
				Task task = procedure.GetTask(taskCode);
				Session["Task"]= task;

				this.txtTaskName.Value = task.TaskName;
				this.txtTaskID.Value = task.TaskID;
				this.txtDescription.Value = task.Description;
				this.txtTaskTitle.Value = task.TaskTitle;
				this.sltTaskRole.Value = task.TaskRole;
				this.sltTaskProperty.Value = task.TaskProperty;
				this.sltTaskType.Value = task.TaskType.ToString();
				this.chkTaskCopy.Checked = (task.Copy == 1);
				this.txtModuleState.Value = task.ModuleState;
				this.sltWayOfSelectPerson.Value = task.WayOfSelectPerson;
                this.ChkCanManual.Checked = (task.CanManual == 1);
                this.txtOpinionType.Value = task.OpinionType;
				((HtmlSelect)this.UltraWebTab1.Tabs.GetTab(0).FindControl("sltMeetType")).Value = task.TaskMeetType;
				((HtmlInputCheckBox)this.UltraWebTab1.Tabs.GetTab(0).FindControl("chkMeetOrder")).Checked = (task.IsOrderly ==1);
				((HtmlInputControl)this.UltraWebTab1.Tabs.GetTab(1).FindControl("txtCopyTitle")).Value = task.TaskActorType;
                //是否等待初始化
                ((HtmlInputCheckBox)this.UltraWebTab1.Tabs.GetTab(1).FindControl("chkWaitForCopy")).Checked = (task.CanEdit == 1);

				UltraWebGrid UWGridCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(0).FindControl("UltraWebGrid3"));
				UltraWebGrid UWGridCopyCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(1).FindControl("UltraWebGrid3"));

				UWGridCase.DataSource = new DataView( ds.Tables["WorkFlowTaskActor"],String.Format( "TaskCode='{0}' and TaskActorID='0'" ,task.TaskCode),"IOrder",DataViewRowState.CurrentRows );
				UWGridCase.DataBind();
				for(int i=0;i<UWGridCase.Rows.Count;i++)
				{
					if((string)UWGridCase.Rows[i].Cells[6].Value == "1")
					{
						UWGridCase.Rows[i].Cells[4].Value = true;
					}
					else
					{
						UWGridCase.Rows[i].Cells[4].Value = false;
					}
					if((string)UWGridCase.Rows[i].Cells[11].Value == "1")
					{
						UWGridCase.Rows[i].Cells[7].Value = true;
					}
					else
					{
						UWGridCase.Rows[i].Cells[7].Value = false;
					}
					
					UWGridCase.Rows[i].Cells[10].Value = "<a href='javascript:deleteActor(\""+(string)UWGridCase.Rows[i].Cells[0].Value+"\",\"0\")'>删除</a>";
				}

				UWGridCopyCase.DataSource = new DataView( ds.Tables["WorkFlowTaskActor"],String.Format( "TaskCode='{0}' and TaskActorID='1'" ,task.TaskCode),"IOrder",DataViewRowState.CurrentRows );
				UWGridCopyCase.DataBind();
				for(int i=0;i<UWGridCopyCase.Rows.Count;i++)
				{
					if((string)UWGridCopyCase.Rows[i].Cells[6].Value == "1")
					{
						UWGridCopyCase.Rows[i].Cells[4].Value = true;
					}
					else
					{
						UWGridCopyCase.Rows[i].Cells[4].Value = false;
					}
                    if ((string)UWGridCopyCase.Rows[i].Cells[9].Value == "1")
                    {
                        UWGridCopyCase.Rows[i].Cells[8].Value = true;
                    }
                    else
                    {
                        UWGridCopyCase.Rows[i].Cells[8].Value = false;
                    }
                    if ((int)UWGridCopyCase.Rows[i].Cells[11].Value == 1)
                    {
                        UWGridCopyCase.Rows[i].Cells[10].Value = true;
                    }
                    else
                    {
                        UWGridCopyCase.Rows[i].Cells[10].Value = false;
                    }
					UWGridCopyCase.Rows[i].Cells[7].Value = "<a href='javascript:deleteActor(\""+(string)UWGridCopyCase.Rows[i].Cells[0].Value+"\",\"1\")'>删除</a>";
				}
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
			ds.Dispose();

		}
		/// ****************************************************************************
		/// <summary>
		/// 初始化
		/// </summary>
		/// ****************************************************************************
		private void PageInit()
		{
			// 流程实体
			Procedure procedure = (Procedure)Session["Procedure"];
			UltraWebGrid UWGridCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(0).FindControl("UltraWebGrid3"));
			UltraWebGrid UWGridCopyCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(1).FindControl("UltraWebGrid3"));

			// 流程角色帮定
			RoleBound(this.sltTaskRole,procedure);
			RoleBound(UWGridCase.Columns[2].ValueList,procedure);
			RoleBound(UWGridCopyCase.Columns[2].ValueList,procedure);

			// 流程属性帮定
			PropertyBound(this.sltTaskProperty,procedure);
			PropertyBound(UWGridCase.Columns[3].ValueList,procedure);
			PropertyBound(UWGridCopyCase.Columns[3].ValueList,procedure);

			// 类型帮定
			RoleTypeBound(((HtmlSelect)this.UltraWebTab1.Tabs.GetTab(0).FindControl("sltMeetType")));
			RoleTypeBound(UWGridCase.Columns[1].ValueList);
			RoleTypeBound(UWGridCopyCase.Columns[1].ValueList);

		}

		/// ****************************************************************************
		/// <summary>
		/// 确定按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{

				string taskCode = Request["TaskCode"]+"";
				

				Procedure procedure = (Procedure)Session["Procedure"];
				Task task = (Task)Session["Task"];
				bool isNew = (taskCode=="");
				if ( isNew )
					procedure.AddNewTask(task);

				task.TaskName = this.txtTaskName.Value;
				task.TaskID = this.txtTaskID.Value;
				task.Description = this.txtDescription.Value;
				task.TaskTitle = this.txtTaskTitle.Value;
				task.TaskRole = this.sltTaskRole.Value;
				task.TaskProperty = this.sltTaskProperty.Value;
				task.TaskType = int.Parse(this.sltTaskType.Value.ToString());
				task.Copy = (this.chkTaskCopy.Checked) ? 1:0;
                task.CanManual = (this.ChkCanManual.Checked) ? 1 : 0;
				task.ModuleState = this.txtModuleState.Value;
				task.WayOfSelectPerson = this.sltWayOfSelectPerson.Value;
				task.TaskMeetType = ((HtmlSelect)this.UltraWebTab1.Tabs.GetTab(0).FindControl("sltMeetType")).Value;
				task.IsOrderly = (((HtmlInputCheckBox)this.UltraWebTab1.Tabs.GetTab(0).FindControl("chkMeetOrder")).Checked) ? 1:0;
                //是否等待
                task.CanEdit = (((HtmlInputCheckBox)this.UltraWebTab1.Tabs.GetTab(1).FindControl("chkWaitForCopy")).Checked) ? 1 : 0;
				task.TaskActorType = ((HtmlInputControl)this.UltraWebTab1.Tabs.GetTab(1).FindControl("txtCopyTitle")).Value;
                task.OpinionType = this.txtOpinionType.Value;

				UltraWebGrid UWGridCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(0).FindControl("UltraWebGrid3"));
				for (int i=0;i<UWGridCase.Rows.Count;i++)
				{
					bool isNewTaskActor = (UWGridCase.Rows[i].Cells[0].Text == null);
					TaskActor TaskActorCase = null;
					if ( isNewTaskActor )
					{
						TaskActorCase = Rms.WorkFlow.DefinitionManager.NewTaskActor();
						TaskActorCase.TaskCode = task.TaskCode;
						TaskActorCase.ProcedureCode = procedure.ProcedureCode;
						task.AddNewTaskActor(TaskActorCase);
					}
					else
					{
						TaskActorCase = task.GetTaskActor(UWGridCase.Rows[i].Cells[0].Text);
					}

                    TaskActorCase.OpinionType = (string)UWGridCase.Rows[i].Cells[9].Value;
					TaskActorCase.TaskActorType = (string)UWGridCase.Rows[i].Cells[1].Value;
					TaskActorCase.ActorCode = (string)UWGridCase.Rows[i].Cells[2].Value;
					if((string)UWGridCase.Rows[i].Cells[3].Value == "--选择--" || (string)UWGridCase.Rows[i].Cells[3].Value == null)
					{
						TaskActorCase.ActorProperty = "";
					}
					else
					{
						TaskActorCase.ActorProperty = (string)UWGridCase.Rows[i].Cells[3].Value;
					}
					TaskActorCase.ActorModuleState = (string)UWGridCase.Rows[i].Cells[5].Value;
                    if (UWGridCase.Rows[i].Cells[8].Value == null)
                    {
                        TaskActorCase.IOrder = 1;
                    }
                    else
                    {
                        TaskActorCase.IOrder = (int)UWGridCase.Rows[i].Cells[8].Value;
                    }
					TaskActorCase.TaskActorID = "0";
					if((bool)UWGridCase.Rows[i].Cells[4].Value)
					{
						TaskActorCase.ActorNeed = "1";
					}
					else
					{
						TaskActorCase.ActorNeed = "0";
					}
					if((bool)UWGridCase.Rows[i].Cells[7].Value)
					{
						TaskActorCase.TaskActorName = "1";
					}
					else
					{
						TaskActorCase.TaskActorName = "0";
					}
				}


				UltraWebGrid UWGridCopyCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(1).FindControl("UltraWebGrid3"));
				for (int i=0;i<UWGridCopyCase.Rows.Count;i++)
				{
					bool isNewTaskActor = (UWGridCopyCase.Rows[i].Cells[0].Text == null);
					TaskActor TaskActorCase = null;
					if ( isNewTaskActor )
					{
						TaskActorCase = Rms.WorkFlow.DefinitionManager.NewTaskActor();
						TaskActorCase.TaskCode = task.TaskCode;
						TaskActorCase.ProcedureCode = procedure.ProcedureCode;
						task.AddNewTaskActor(TaskActorCase);
					}
					else
					{
						TaskActorCase = task.GetTaskActor(UWGridCopyCase.Rows[i].Cells[0].Text);
					}


                    TaskActorCase.OpinionType = (string)UWGridCopyCase.Rows[i].Cells[12].Value;
					TaskActorCase.TaskActorType = (string)UWGridCopyCase.Rows[i].Cells[1].Value;
					TaskActorCase.ActorCode = (string)UWGridCopyCase.Rows[i].Cells[2].Value;
					if((string)UWGridCopyCase.Rows[i].Cells[3].Value == "--选择--" || (string)UWGridCopyCase.Rows[i].Cells[3].Value == null)
					{
						TaskActorCase.ActorProperty = "";
					}
					else
					{
						TaskActorCase.ActorProperty = (string)UWGridCopyCase.Rows[i].Cells[3].Value;
					}
					TaskActorCase.ActorModuleState = (string)UWGridCopyCase.Rows[i].Cells[5].Value;
					TaskActorCase.IOrder = i;
					TaskActorCase.TaskActorID = "1";
					if((bool)UWGridCopyCase.Rows[i].Cells[4].Value)
					{
						TaskActorCase.ActorNeed = "1";
					}
					else
					{
						TaskActorCase.ActorNeed = "0";
					}
                    if ((bool)UWGridCopyCase.Rows[i].Cells[8].Value)
                    {
                        TaskActorCase.TaskActorName = "1";
                    }
                    else
                    {
                        TaskActorCase.TaskActorName = "0";
                    }
                    if ((bool)UWGridCopyCase.Rows[i].Cells[10].Value)
                    {
                        TaskActorCase.ActorType = 1;
                    }
                    else
                    {
                        TaskActorCase.ActorType = 0;
                    }
				}

				Session["Procedure"] = procedure;
				CloseWindow();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		
		}

		#region *** 帮定流程角色数据 ***
		/// ****************************************************************************
		/// <summary>
		/// 流程角色帮定
		/// </summary>
		/// <param name="ValueListCase">帮定对象</param>
		/// <param name="ProcedureCase">流程实体</param>
		/// ****************************************************************************
		public static void RoleBound(ValueList ValueListCase,Procedure ProcedureCase)
		{
			RoleBoundProcess(ValueListCase,"ValueList",ProcedureCase);
		}
		/// ****************************************************************************
		/// <summary>
		/// 流程角色帮定
		/// </summary>
		/// <param name="HtmlSelectCase">帮定对象</param>
		/// <param name="ProcedureCase">流程实体</param>
		/// ****************************************************************************
		public static void RoleBound(HtmlSelect HtmlSelectCase,Procedure ProcedureCase)
		{
			RoleBoundProcess(HtmlSelectCase,"HtmlSelect",ProcedureCase);
		}
		/// ****************************************************************************
		/// <summary>
		/// 流程角色帮定逻辑实现
		/// </summary>
		/// <param name="ObjectCase">帮定对象</param>
		/// <param name="ObjectType">帮定类型</param>
		/// <param name="ProcedureCase">流程对象</param>
		/// ****************************************************************************
		private static void RoleBoundProcess(object ObjectCase,string ObjectType,Procedure ProcedureCase)
		{
			System.Collections.IDictionaryEnumerator ie = ProcedureCase.GetRoleEnumerator();

			DataTable dt = new DataTable();

			dt.Columns.Add("ItemText");
			dt.Columns.Add("ItemValue");
			dt.Columns.Add("Remak");


			while(ie.MoveNext())
			{
				DataRow drNew = dt.NewRow();

				Role RoleCase =(Role)ie.Value;
				drNew["ItemText"] = RoleCase.RoleName;
				drNew["ItemValue"] = RoleCase.WorkFlowRoleCode;
				drNew["Remak"] = RoleCase.Remak;

				dt.Rows.Add(drNew);
			}

			foreach ( DataRow dr in dt.Select("","Remak",System.Data.DataViewRowState.CurrentRows) )
			{
				if(ObjectType == "HtmlSelect")
				{
					ListItem ListItemCase = new ListItem();
					ListItemCase.Text = dr["ItemText"].ToString();
					ListItemCase.Value = dr["ItemValue"].ToString();
					((HtmlSelect)ObjectCase).Items.Add(ListItemCase);
				}
				if(ObjectType == "ValueList")
				{
					((ValueList)ObjectCase).ValueListItems.Add(dr["ItemValue"].ToString(),dr["ItemText"].ToString());
				}
			}
		}
		#endregion

		#region *** 帮定流程属性数据 ***
		/// ****************************************************************************
		/// <summary>
		/// 流程属性帮定
		/// </summary>
		/// <param name="ValueListCase">帮定对象</param>
		/// <param name="ProcedureCase">流程实体</param>
		/// ****************************************************************************
		public static void PropertyBound(ValueList ValueListCase,Procedure ProcedureCase)
		{
			PropertyBoundProcess(ValueListCase,"ValueList",ProcedureCase);
		}
		/// ****************************************************************************
		/// <summary>
		/// 流程属性帮定
		/// </summary>
		/// <param name="HtmlSelectCase">帮定对象</param>
		/// <param name="ProcedureCase">流程实体</param>
		/// ****************************************************************************
		public static void PropertyBound(HtmlSelect HtmlSelectCase,Procedure ProcedureCase)
		{
			PropertyBoundProcess(HtmlSelectCase,"HtmlSelect",ProcedureCase);
		}
		/// ****************************************************************************
		/// <summary>
		/// 流程属性帮定逻辑实现
		/// </summary>
		/// <param name="ObjectCase">帮定对象</param>
		/// <param name="ObjectType">帮定类型</param>
		/// <param name="ProcedureCase">流程对象</param>
		/// ****************************************************************************
		private static void PropertyBoundProcess(object ObjectCase,string ObjectType,Procedure ProcedureCase)
		{
			if(ObjectType == "HtmlSelect")
			{
				ListItem ListItemCase = new ListItem();
				ListItemCase.Text = "--选择--";
				ListItemCase.Value = "";
				((HtmlSelect)ObjectCase).Items.Add(ListItemCase);
			}
			if(ObjectType == "ValueList")
			{
				((ValueList)ObjectCase).ValueListItems.Add("","--选择--");
			}

			System.Collections.IDictionaryEnumerator ie = ProcedureCase.GetPropertyEnumerator();
			while(ie.MoveNext())
			{
				Property PropertyCase =(Property)ie.Value;
				string ItemText = PropertyCase.ProcedurePropertyName;
				string ItemValue = PropertyCase.WorkFlowProcedurePropertyCode;;

				if(ObjectType == "HtmlSelect")
				{
					ListItem ListItemCase = new ListItem();
					ListItemCase.Text = ItemText;
					ListItemCase.Value = ItemValue;
					((HtmlSelect)ObjectCase).Items.Add(ListItemCase);
				}
				if(ObjectType == "ValueList")
				{
					((ValueList)ObjectCase).ValueListItems.Add(ItemValue,ItemText);
				}
			}
		}
		#endregion

		#region *** 帮定类型数据 ***
		/// ****************************************************************************
		/// <summary>
		/// 帮定类型数据
		/// </summary>
		/// <param name="ValueListCase">帮定对象</param>
		/// ****************************************************************************
		private void RoleTypeBound(ValueList ValueListCase)
		{
			RoleTypeBoundProcess(ValueListCase,"ValueList");
		}
		/// ****************************************************************************
		/// <summary>
		/// 帮定类型数据
		/// </summary>
		/// <param name="HtmlSelectCase">帮定对象</param>
		/// ****************************************************************************
		private void RoleTypeBound(HtmlSelect HtmlSelectCase)
		{
			RoleTypeBoundProcess(HtmlSelectCase,"HtmlSelect");
		}
		/// ****************************************************************************
		/// <summary>
		/// 帮定类型数据逻辑实现
		/// </summary>
		/// <param name="ObjectCase">帮定对象</param>
		/// <param name="ObjectType">帮定类型</param>
		/// ****************************************************************************
		private void RoleTypeBoundProcess(object ObjectCase,string ObjectType)
		{
			Type rtype = typeof(RoleType);
			foreach(string s in Enum.GetNames(rtype))
			{
				string ItemText = "未知";
				string ItemValue = s;
				switch (s)
				{
					case "Unit":
						ItemText = "部门";
						break;
					case "Porson":
						ItemText = "人";
						break;
					case "Station":
						ItemText = "岗位";
						break;
					case "All":
						ItemText = "所有";
						break;
					case "Other":
						ItemText = "其他";
						break;
					default:
						ItemText = "未知";
						break;
				}
				if(ObjectType == "HtmlSelect")
				{
					ListItem ListItemCase = new ListItem();
					ListItemCase.Text = ItemText;
					ListItemCase.Value = s;
					((HtmlSelect)ObjectCase).Items.Add(ListItemCase);
				}
				if(ObjectType == "ValueList")
				{
					((ValueList)ObjectCase).ValueListItems.Add(ItemValue,ItemText);
				}
			}
		}
		#endregion

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

		/// ****************************************************************************
		/// <summary>
		/// 任务删除按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string taskCode = Request["TaskCode"]+"";
			if ( taskCode == "" )
				return;

			Procedure procedure = (Procedure)Session["Procedure"];
			procedure.RemoveTask(taskCode,false);
			Session["Task"] = null;
			Session["Procedure"] = procedure;
			CloseWindow();
		}

		/// ****************************************************************************
		/// <summary>
		/// 关闭当前窗口并更新父页面数据
		/// </summary>
		/// ****************************************************************************
		private void CloseWindow()
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			//Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			Response.Write("window.opener.LoadTask();");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 删除会签成员
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelActor_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				
				Procedure procedure = (Procedure)Session["Procedure"];
				Task task = (Task)Session["Task"];
				task.RemoveTaskActor(this.DelObjectCode.Value,false);

				DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
				UltraWebGrid UWGridCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(0).FindControl("UltraWebGrid3"));

				UWGridCase.DataSource = new DataView( ds.Tables["WorkFlowTaskActor"],String.Format( "TaskCode='{0}' and TaskActorID='0'" ,task.TaskCode),"IOrder",DataViewRowState.CurrentRows );
				UWGridCase.DataBind();
				for(int i=0;i<UWGridCase.Rows.Count;i++)
				{
					if((string)UWGridCase.Rows[i].Cells[6].Value == "1")
					{
						UWGridCase.Rows[i].Cells[4].Value = true;
					}
					else
					{
						UWGridCase.Rows[i].Cells[4].Value = false;
					}
					UWGridCase.Rows[i].Cells[10].Value = "<a href='javascript:deleteActor(\""+(string)UWGridCase.Rows[i].Cells[0].Value+"\",\"0\")'>删除</a>";
				}

				Session["Procedure"] = procedure;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 删除抄送人员
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelActorCopy_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				
				Procedure procedure = (Procedure)Session["Procedure"];
				Task task = (Task)Session["Task"];
				task.RemoveTaskActor(this.DelObjectCode.Value,false);

				DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
				UltraWebGrid UWGridCopyCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(1).FindControl("UltraWebGrid3"));

				UWGridCopyCase.DataSource = new DataView( ds.Tables["WorkFlowTaskActor"],String.Format( "TaskCode='{0}' and TaskActorID='1'" ,task.TaskCode),"IOrder",DataViewRowState.CurrentRows );
				UWGridCopyCase.DataBind();
				for(int i=0;i<UWGridCopyCase.Rows.Count;i++)
				{
					if((string)UWGridCopyCase.Rows[i].Cells[6].Value == "1")
					{
						UWGridCopyCase.Rows[i].Cells[4].Value = true;
					}
					else
					{
						UWGridCopyCase.Rows[i].Cells[4].Value = false;
					}
					UWGridCopyCase.Rows[i].Cells[7].Value = "<a href='javascript:deleteActor(\""+(string)UWGridCopyCase.Rows[i].Cells[0].Value+"\",\"1\")'>删除</a>";
				}

				Session["Procedure"] = procedure;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		
		}
	}
}
