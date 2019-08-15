//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分修改的。
// 类名已更改，且类已修改为从文件“App_Code\Migrated\workflowcontrol\Stub_workflowcasestate_ascx_cs.cs”的抽象基类 
// 继承。
// 在运行时，此项允许您的 Web 应用程序中的其他类使用该抽象基类绑定和访问 
// 代码隐藏页。
// 关联的内容页“workflowcontrol\workflowcasestate.ascx”也已修改，以引用新的类名。
// 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
namespace RmsPM.Web.WorkFlowControl
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using RmsPM.BLL;
	using RmsPM.DAL.EntityDAO;
	using RmsPM.DAL.QueryStrategy;
	using RmsPM.Web;
	using Rms.ORMap;
	using Rms.WorkFlow;

	/// <summary>
	///		WorkFlowCaseState 的摘要说明。
	/// </summary>
    public partial class Migrated_WorkFlowCaseState : WorkFlowCaseState
	{
		private string _ActCode = null;
		private string _ApplicationCode = null;
		private string _FlowName = null;
		private string _UserCode = null;
        private bool _IsScoutPopedom = false;
        private WorkFlowToolbar _Toobar = null;
        private bool _IsUsePrint = false;

        override public WorkFlowToolbar Toobar
        {
            get { return _Toobar; }
            set { _Toobar = value; }
        }

        
        override public string CaseCode
        {
            get
            {
                if (this.ViewState["_CaseCode"] == null)
                    return "";
                return this.ViewState["_CaseCode"].ToString();
            }
            set
            {
                this.ViewState["_CaseCode"] = value;
            }
 
        }
		/// <summary>
		/// 流程监控标识
		/// </summary>
		private bool _Scout = false;
		/// <summary>
		/// 动作实例代码
		/// </summary>
		override public string ActCode
		{
			get
			{
				if ( _ActCode == null )
				{
					if(this.ViewState["_ActCode"] != null)
						return this.ViewState["_ActCode"].ToString();
					return "";
				}
				return _ActCode;
			}
			set
			{
				_ActCode = value;
				this.ViewState["_ActCode"] = value;
			}
		}
		/// <summary>
		/// 业务实例代码
		/// </summary>
//		public string ApplicationCode
//		{
		override public string ApplicationCode
		{
			get
			{
				if ( _ApplicationCode == null )
				{
					if(this.ViewState["_ApplicationCode"] != null)
						return this.ViewState["_ApplicationCode"].ToString();
					return "";
				}
				return _ApplicationCode;
			}
			set
			{
				_ApplicationCode = value;
				this.ViewState["_ApplicationCode"] = value;
			}
		}
		/// <summary>
		/// 工作流名称
		/// </summary>
//		public string FlowName
//		{
		override public string FlowName
		{
			get
			{
				if ( _FlowName == null )
				{
					if(this.ViewState["_FlowName"] != null)
						return this.ViewState["_FlowName"].ToString();
					return "";
				}
				return _FlowName;
			}
			set
			{
				_FlowName = value;
				this.ViewState["_FlowName"] = value;
			}
		}

		/// <summary>
		/// 工作流名称
		/// </summary>
		override public string UserCode
		{
			get
			{
				if ( _UserCode == null )
				{
					if(this.ViewState["_UserCode"] != null)
						return this.ViewState["_UserCode"].ToString();
					return "";
				}
				return _UserCode;
			}
			set
			{
				_UserCode = value;
				this.ViewState["_UserCode"] = value;
			}
		}

        /// <summary>
        /// 项目代码
        /// </summary>
        override public string ProjectCode
        {
            get
            {
                if (this.ViewState["_ProjectCode"] == null)
                    return "";
                return this.ViewState["_ProjectCode"].ToString();
            }
            set
            {
                this.ViewState["_ProjectCode"] = value;
            }
        }

		/// <summary>
		/// 流程监控标识
		/// </summary>
		override public bool Scout
		{
			get
			{
				if ( _Scout == false )
				{
					if(this.ViewState["_Scout"] != null)
						return (bool)this.ViewState["_Scout"];
				}
				return _Scout;
			}
			set
			{
				_Scout = value;
				this.ViewState["_Scout"] = value;
			}
		}

        /// <summary>
        /// 是否有流程监控权限
        /// </summary>
        override public bool IsScoutPopedom
        {
            get
            {
                if (_IsScoutPopedom == false)
                {
                    if (this.ViewState["_IsScoutPopedom"] != null)
                        return (bool)this.ViewState["_IsScoutPopedom"];
                }
                return _IsScoutPopedom;
            }
            set
            {
                _IsScoutPopedom = value;
                this.ViewState["_IsScoutPopedom"] = value;
            }
        }

        /// <summary>
        /// 是否屏蔽流程引擎
        /// </summary>
        override public bool IsUsePrint
        {
            get
            {
                if (_IsUsePrint == false)
                {
                    if (this.ViewState["_IsUsePrint"] != null)
                        return (bool)this.ViewState["_IsUsePrint"];
                }
                return _IsUsePrint;
            }
            set
            {
                _IsUsePrint = value;
                this.ViewState["_IsUsePrint"] = value;
            }
        }
    

		/// ****************************************************************************
		/// <summary>
		/// 更新数据重新显示
		/// </summary>
		/// ****************************************************************************
//		public void ControlDataBind()
		override public void ControlDataBind()
		{
			try
			{
                //clm修改，修改前语句为以下语句，修改日期：２００６０９２１
				//if(!(this.ActCode == "" && this.ApplicationCode == ""))
                if(this.CaseCode!="")
				{
					this.IniPage();
					this.BuildSqlString();
                    if (BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["FlowCaseState"]) == "1")
                    {
                        this.dgList.Visible = false;
                        this.DataGrid3.Visible = true;
                        this.DataGrid4.Visible = true;
                        this.Table2.Visible = true;
                        this.LoadData1();
                    }
                    else
                    {
                        this.dgList.Visible = true;
                        this.DataGrid3.Visible = false;
                        this.DataGrid4.Visible = false;
                        this.Table2.Visible = false;
                        this.LoadData();
                    }
				}
			}
			catch( Exception ex )
			{
                throw ex;
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		/// ****************************************************************************
		/// <summary>
		/// WorkFlowControlList 加载过程。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		/// ****************************************************************************
		/// <summary>
		/// 生成查看流程信息 sql 语句
		/// </summary>
		/// ****************************************************************************
		private void BuildSqlString()
		{
			//////根据动作代码查找流程代码//////
			string sql = "";
            if (CaseCode == "")
            {
                if (this.ActCode != "")
                {
                    DAL.QueryStrategy.WorkFlowActStrategyBuilder sb = new DAL.QueryStrategy.WorkFlowActStrategyBuilder();
                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.WorkFlowActStrategyName.ActCode, this.ActCode));


                    sql = sb.BuildMainQueryString();

                    QueryAgent QA = new QueryAgent();
                    EntityData entity = QA.FillEntityData("WorkFlowAct", sql);
                    this.ViewState["_CaseCode"] = entity.CurrentRow["CaseCode"].ToString();
                    QA.Dispose();
                    entity.Dispose();
                }
                /*else if (this.ApplicationCode != "")
                {
                    DAL.QueryStrategy.WorkFlowCaseStrategyBuilder sb = new DAL.QueryStrategy.WorkFlowCaseStrategyBuilder();
                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.WorkFlowCaseStrategyName.ApplicationCode, this.ApplicationCode));
                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.WorkFlowCaseStrategyName.ProcedureCode, BLL.WorkFlowRule.GetProcedureCodeByName(this.FlowName)));

                    sql = sb.BuildMainQueryString();

                    QueryAgent QA = new QueryAgent();
                    EntityData entity = QA.FillEntityData("WorkFlowCase", sql);
                    this.ViewState["_CaseCode"] = entity.CurrentRow["CaseCode"].ToString();
                    QA.Dispose();
                    entity.Dispose();
                }*/
            }

			WorkFlowActStrategyBuilder sb1 = new WorkFlowActStrategyBuilder();
			sb1.AddStrategy( new Strategy( WorkFlowActStrategyName.CaseCode,this.ViewState["_CaseCode"].ToString() ));
			sb1.AddOrder("FromDate",true);
			sql = sb1.BuildMainQueryString();
			this.ViewState.Add("SqlString",sql);
		}

		private void IniPage()
		{
			if ( Request["Debug"]+"" == "1" )
			{
				this.DataGrid1.Visible = true;
				this.DataGrid2.Visible = true;
			}
			else
			{
				this.DataGrid1.Visible = false;
				this.DataGrid2.Visible = false;
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// 执行 sql 语句将查询结果帮定显示。
		/// </summary>
		/// ****************************************************************************
		private void LoadData()
		{
			string sql = (string)this.ViewState["SqlString"];
			QueryAgent qa = new QueryAgent();
			EntityData entity = qa.FillEntityData( "WorkFlowAct",sql );
			qa.Dispose();
           

			DataColumn Opinion = new DataColumn();
			Opinion.ColumnName = "Opinion";
			Opinion.DefaultValue = "";
			Opinion.DataType = System.Type.GetType("System.String");
			entity.CurrentTable.Columns.Add(Opinion);

			WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
			System.Collections.IDictionaryEnumerator ie = workCase.GetOpinionEnumerator();
            string TaskCode = "";
            if (this.ActCode != "" && !this.Scout)
            {
                Act act = workCase.GetAct(this.ActCode);
                TaskCode = act.ToTaskCode;
            }
			while(ie.MoveNext())
			{
				Opinion Flowopinion = (Opinion)ie.Value;
				foreach(DataRow dr in entity.CurrentTable.Rows)
				{
                    if (dr["ActCode"].ToString() == Flowopinion.ApplicationCode)
                    {
                        if (dr["Status"].ToString() == "End")
                        {
                            Act tmpAct = workCase.GetAct(dr["ActCode"].ToString());


                            if (!(tmpAct.Copy == 1 && tmpAct.IsSleep == 0) || (tmpAct.ToTaskCode == TaskCode && tmpAct.FromUserCode == this.UserCode && tmpAct.Copy == 1) || tmpAct.ActUserCode == this.UserCode || this.IsScoutPopedom)
                            {
                                if (Flowopinion.OpinionText.Length > 0)
                                {
                                    ModuleState moduleState = this.Toobar.GetModuleState(WorkFlowRule.GetTaskOpinionTypeByActCode(dr["ActCode"].ToString()));
                                    if (moduleState != ModuleState.Sightless && moduleState != ModuleState.Other)
                                    {
                                        //dr["Opinion"] = "<a href=\"#\" OnClick=\"javascript:WorkFlowCaseStateOpenOpinionView('" + Flowopinion.OpinionCode + "');\">" + Flowopinion.OpinionText.Substring(0, 50) + "...</a>";
                                        dr["Opinion"] = Flowopinion.OpinionText.Replace("\n", "<br>") + "&nbsp;&nbsp;<a href=\"#\" OnClick=\"javascript:WorkFlowCaseStateOpenOpinionView('" + Flowopinion.OpinionCode + "');return false;\">[详细]";
                                        string tmpstr = DocumentRule.Instance().GetAttachListHtml("WorkFlowActOpinion", tmpAct.ActCode);
                                        if (tmpstr != "")
                                        {
                                            //dr["Opinion"] += " <img src=\"../Images/attach.gif\" style=\"border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none\" /></a><br>" + "附件：" + tmpstr;
                                            dr["Opinion"] += "</a><br>" + "附件：" + tmpstr;
                                        }
                                        else
                                        {
                                            dr["Opinion"] += "</a>";
                                        }
                                    }
                                }
                            }
                        }
                    }
				}
			}

			if(Request["Debug"]+"" == "1")
			{
				string procedureCode = entity.GetString("ProcedureCode");
				Procedure procedure = DefinitionManager.GetProcedureDifinition(procedureCode,true);

				/////////////////创建属性表///////////////////
				DataTable PropertyTable = BLL.WorkFlowRule.GetPropertyTable(workCase,procedure);

				this.DataGrid1.DataSource = entity;
				this.DataGrid1.DataBind();

				this.DataGrid2.DataSource = PropertyTable;
				this.DataGrid2.DataBind();
			}
            DataView dv;
            if (!((User)Session["user"]).HasOperationRight("090102"))
            {
                dv = new DataView(entity.CurrentTable, String.Format(" ActUserCode='{0}' or (FromUserCode='{0}' and Copy='1') or (Copy = '1' and IsSleep='1') or Copy='0'", UserCode), "", DataViewRowState.CurrentRows);
            }
            else 
            {
                dv = new DataView(entity.CurrentTable);
            }
            this.dgList.DataSource = dv;
            this.dgList.DataBind();

            if (dv.Count == 0)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
                if (this.ActCode != "" && !this.Scout)
                {
                    //Act act = workCase.GetAct(this.ActCode);
                    //string TaskCode = act.ToTaskCode;
                    for (int i = 0; i < dgList.Items.Count; i++)
                    {
                        //dgList.Items[i].Cells[10].Text.Trim() == "ActCode" && 
                        if (dgList.Items[i].Cells[8].Text.Trim() == TaskCode && dgList.Items[i].Cells[9].Text.Trim() == this.UserCode && dgList.Items[i].Cells[10].Text.Trim() == "1")
                        {
                            Act tmpact = workCase.GetAct(dgList.Items[i].Cells[7].Text.Trim());
                            ((CheckBox)dgList.Items[i].FindControl("chkopinionshow")).Checked = (tmpact.IsSleep == 1);
                            ((CheckBox)dgList.Items[i].FindControl("chkopinionshow")).Visible = true;
                        }
                        else
                        {
                            ((CheckBox)dgList.Items[i].FindControl("chkopinionshow")).Visible = false;
                            Act tmpact = workCase.GetAct(dgList.Items[i].Cells[7].Text.Trim());
                            if (tmpact.IsSleep == 1 && tmpact.Copy == 1)
                                dgList.Items[i].Cells[6].Text = "&nbsp;公开";
                            else if (tmpact.IsSleep == 0 && tmpact.Copy == 1)
                                dgList.Items[i].Cells[6].Text = "&nbsp;未公开";


                        }
                    }
                }
                if (this.Scout)
                {
                    for (int i = 0; i < dgList.Items.Count; i++)
                    {
                        ((CheckBox)dgList.Items[i].FindControl("chkopinionshow")).Visible = false;
                        Act tmpact = workCase.GetAct(dgList.Items[i].Cells[7].Text.Trim());
                        if (tmpact.IsSleep == 1 && tmpact.Copy == 1)
                            dgList.Items[i].Cells[6].Text = "&nbsp;公开";
                        else if (tmpact.IsSleep == 0 && tmpact.Copy == 1)
                            dgList.Items[i].Cells[6].Text = "&nbsp;未公开";
                    }

                }
            }
			entity.Dispose();
		}

        /// ****************************************************************************
        /// <summary>
        /// 执行 sql 语句将查询结果帮定显示。
        /// </summary>
        /// ****************************************************************************
        private void LoadData1()
        {
            string sql = (string)this.ViewState["SqlString"];
            QueryAgent qa = new QueryAgent();
            EntityData entity = qa.FillEntityData("WorkFlowAct", sql);
            qa.Dispose();

            DataColumn Opinion = new DataColumn();
            Opinion.ColumnName = "Opinion";
            Opinion.DefaultValue = "";
            Opinion.DataType = System.Type.GetType("System.String");
            //增加流程角色名
            DataColumn Opinion2 = new DataColumn();
            Opinion2.ColumnName = "RoleName";
            Opinion2.DefaultValue = "";
            Opinion2.DataType = System.Type.GetType("System.String");
            //结束
            DataColumn Opinion3 = new DataColumn();
            Opinion3.ColumnName = "IsEnd";
            Opinion3.DefaultValue = "1";
            Opinion3.DataType = System.Type.GetType("System.String");
          
            entity.CurrentTable.Columns.Add(Opinion);
            entity.CurrentTable.Columns.Add(Opinion2);
            entity.CurrentTable.Columns.Add(Opinion3);

            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
            System.Collections.IDictionaryEnumerator ie = workCase.GetOpinionEnumerator();
            string TaskCode = "";
            if (this.ActCode != "" && !this.Scout)
            {
                Act act = workCase.GetAct(this.ActCode);
                TaskCode = act.ToTaskCode;
            }
            while (ie.MoveNext())
            {
                Opinion Flowopinion = (Opinion)ie.Value;
                foreach (DataRow dr in entity.CurrentTable.Rows)
                {

                    //加载流程角色名
                    Procedure procedure = DefinitionManager.GetProcedureDifinition(dr["ProcedureCode"].ToString(), true);
                    Task task = procedure.GetTask(dr["ToTaskCode"].ToString());
                    //判断是否为结束
                    if (task.TaskType == 2)
                    {
                        dr["IsEnd"] = "0";
                    }

                    if (task != null)
                    {
                        if (dr["TaskActorID"].ToString() != "")
                        {
                            TaskActor taskActor = task.GetTaskActor(dr["TaskActorID"].ToString());
                            if (taskActor != null)
                            {
                                Role role = procedure.GetRole(taskActor.ActorCode);
                                if (role != null)
                                    dr["RoleName"] = role.RoleName;
                            }
                        }
                        else
                        {
                            Role role = procedure.GetRole(task.TaskRole);

                            if (role != null)
                            {
                                dr["RoleName"] = role.RoleName;
                            }
                        }
                    }

                    if (dr["ActCode"].ToString() == Flowopinion.ApplicationCode)
                    {
                        if (dr["Status"].ToString() == "End")
                        {
                            Act tmpAct = workCase.GetAct(dr["ActCode"].ToString());
                            if (!(tmpAct.Copy == 1 && tmpAct.IsSleep == 0) || (tmpAct.ToTaskCode == TaskCode && tmpAct.FromUserCode == this.UserCode && tmpAct.Copy == 1) || tmpAct.ActUserCode == this.UserCode || this.IsScoutPopedom)
                            {
                                if (Flowopinion.OpinionText.Length > 0)
                                {
                                     ModuleState moduleState = this.Toobar.GetModuleState(WorkFlowRule.GetTaskOpinionTypeByActCode(dr["ActCode"].ToString()));
                                     if (moduleState != ModuleState.Sightless && moduleState != ModuleState.Other)
                                     {
                                         //dr["Opinion"] = "<a href=\"#\" OnClick=\"javascript:WorkFlowCaseStateOpenOpinionView('" + Flowopinion.OpinionCode + "');\">" + Flowopinion.OpinionText.Substring(0, 50) + "...</a>";
                                         dr["Opinion"] = Flowopinion.OpinionText.Replace("\n", "<br>") + "&nbsp;&nbsp;";
                                         string tmpstr = DocumentRule.Instance().GetAttachListHtml("WorkFlowActOpinion", tmpAct.ActCode);
                                         if (tmpstr != "")
                                         {
                                             //dr["Opinion"] += " <img src=\"../Images/attach.gif\" style=\"border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none\" /></a><br>" + "附件：" + tmpstr;
                                             dr["Opinion"] += "<br>" + "附件：" + tmpstr;
                                         }
                                     }
                                }
                                /*if (Flowopinion.OpinionText.Length > 50)
                                {
                                    dr["Opinion"] = "<a href=\"#\" OnClick=\"javascript:WorkFlowCaseStateOpenOpinionView('" + Flowopinion.OpinionCode + "');\">" + Flowopinion.OpinionText.Substring(0, 50) + "...</a>";
                                }
                                else
                                {
                                    dr["Opinion"] = "<a href=\"#\" OnClick=\"javascript:WorkFlowCaseStateOpenOpinionView('" + Flowopinion.OpinionCode + "');\">" + Flowopinion.OpinionText + "</a>";
                                }*/
                            }
                        }
                    }
                }
            }

            if (Request["Debug"] + "" == "1")
            {
                string procedureCode = entity.GetString("ProcedureCode");
                Procedure procedure = DefinitionManager.GetProcedureDifinition(procedureCode, true);

                /////////////////创建属性表///////////////////
                DataTable PropertyTable = BLL.WorkFlowRule.GetPropertyTable(workCase, procedure);

                this.DataGrid1.DataSource = entity;
                this.DataGrid1.DataBind();

                this.DataGrid2.DataSource = PropertyTable;
                this.DataGrid2.DataBind();
            }

            foreach (DataRow dr in entity.CurrentTable.Select())
            {
                Act tmpAct = workCase.GetAct(dr["ActCode"].ToString());
                if (dr["IsEnd"].ToString() == "0")
                {
                    entity.CurrentTable.Rows.Remove(dr);
                }
            }
            DataView dv;
            if (!((User)Session["user"]).HasOperationRight("090102"))
            {
                dv = new DataView(entity.CurrentTable, String.Format(" ActUserCode='{0}' or (FromUserCode='{0}' and Copy='1') or (Copy = '1' and IsSleep='1') or Copy='0'", UserCode), "", DataViewRowState.CurrentRows);
            }
            else
            {
                dv = new DataView(entity.CurrentTable);
            }

          
            this.DataGrid3.DataSource = dv;
            this.DataGrid3.DataBind();
            this.DataGrid4.DataSource = dv;
            this.DataGrid4.DataBind();

            if (dv.Count == 0)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
                if (this.ActCode != "" && !this.Scout)
                {
                    //Act act = workCase.GetAct(this.ActCode);
                    //string TaskCode = act.ToTaskCode;
                    for (int i = 0; i < DataGrid3.Items.Count; i++)
                    {
                        //DataGrid3.Items[i].Cells[10].Text.Trim() == "ActCode" && 
                        if (DataGrid3.Items[i].Cells[11].Text.Trim() == TaskCode && DataGrid3.Items[i].Cells[12].Text.Trim() == this.UserCode && DataGrid3.Items[i].Cells[13].Text.Trim() == "1")
                        {
                            Act tmpact = workCase.GetAct(DataGrid3.Items[i].Cells[10].Text.Trim());
                            ((CheckBox)DataGrid3.Items[i].FindControl("chkopinionshow")).Checked = (tmpact.IsSleep == 1);
                            ((CheckBox)DataGrid3.Items[i].FindControl("chkopinionshow")).Visible = true;
                        }
                        else
                        {
                            ((CheckBox)DataGrid3.Items[i].FindControl("chkopinionshow")).Visible = false;
                            Act tmpact = workCase.GetAct(DataGrid3.Items[i].Cells[10].Text.Trim());
                            if (tmpact.IsSleep == 1 && tmpact.Copy == 1)
                                DataGrid3.Items[i].Cells[9].Text = "&nbsp;√";
                            else if (tmpact.IsSleep == 0 && tmpact.Copy == 1)
                                DataGrid3.Items[i].Cells[9].Text = "&nbsp;x";


                        }
                    }
                }
                if (this.Scout)
                {
                    for (int i = 0; i < DataGrid3.Items.Count; i++)
                    {
                        ((CheckBox)DataGrid3.Items[i].FindControl("chkopinionshow")).Visible = false;
                        Act tmpact = workCase.GetAct(DataGrid3.Items[i].Cells[10].Text.Trim());
                        if (tmpact.IsSleep == 1 && tmpact.Copy == 1)
                            DataGrid3.Items[i].Cells[9].Text = "&nbsp;√";
                        else if (tmpact.IsSleep == 0 && tmpact.Copy == 1)
                            DataGrid3.Items[i].Cells[9].Text = "&nbsp;x";
                    }
                }
            }
            entity.Dispose();
        }
		/// <summary>
		/// 提交数据
		/// </summary>
		override public void SubmitData()
		{
			if(this.ActCode != "" && !this.Scout)
			{
				WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
				Act act = workCase.GetAct(this.ActCode);
				string TaskCode = act.ToTaskCode;
                if (BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["FlowCaseState"]) == "1")
                {
                    for (int i = 0; i < DataGrid3.Items.Count; i++)
                    {
                        //DataGrid3.Items[i].Cells[10].Text.Trim() == "ActCode" && 
                        if (DataGrid3.Items[i].Cells[11].Text.Trim() == TaskCode && DataGrid3.Items[i].Cells[12].Text.Trim() == this.UserCode && DataGrid3.Items[i].Cells[13].Text.Trim() == "1")
                        {
                            Act currentAct = workCase.GetAct(DataGrid3.Items[i].Cells[10].Text.Trim());

                            if (((CheckBox)DataGrid3.Items[i].FindControl("chkopinionshow")).Checked)
                            {
                                currentAct.IsSleep = 1;
                            }
                            else
                            {
                                currentAct.IsSleep = 0;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dgList.Items.Count; i++)
                    {
                        //dgList.Items[i].Cells[10].Text.Trim() == "ActCode" && 
                        if (dgList.Items[i].Cells[8].Text.Trim() == TaskCode && dgList.Items[i].Cells[9].Text.Trim() == this.UserCode && dgList.Items[i].Cells[10].Text.Trim() == "1")
                        {
                            Act currentAct = workCase.GetAct(dgList.Items[i].Cells[7].Text.Trim());

                            if (((CheckBox)dgList.Items[i].FindControl("chkopinionshow")).Checked)
                            {
                                currentAct.IsSleep = 1;
                            }
                            else
                            {
                                currentAct.IsSleep = 0;
                            }
                        }
                    }
                }
				DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
				BLL.WorkFlowApplicationRule.SaveWorkCaseEx(ds,workCase.CaseCode);
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
