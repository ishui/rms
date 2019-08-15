namespace RmsPM.Web.WorkFlowControl
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Text;
	using System.Collections;

	using RmsPM.BLL;
	using RmsPM.DAL.EntityDAO;
	using RmsPM.DAL.QueryStrategy;
	using RmsPM.Web;
	using Rms.ORMap;
	using Rms.WorkFlow;

	/// *******************************************************************************************
	/// <summary>
	///	WorkFLowOpinionControl 工作流流程独立组件。
	/// Programmer:CLM; Email:nmgclm626@hotmail.com
	/// </summary>
	/// <remarks>
	/// 工作流流程独立组件主要为复杂流程设计。每个组件完成独立操作，一般情况下需结合流程工具栏使用。
	/// 如有问题或发现 bug 请与 Programmer 联系！
	/// </remarks>
	/// <example>
	/// 以下示例过程说明如何用 WorkFlowOpinionControl 进行流程工作。
	/// <p>1.将组件托放至页面</p>
	/// <p>2.增加组件的定义过程。如下：</p>
	/// <code><![CDATA[
	///     protected RmsPM.Web.WorkFlowControl.WorkFlowOpinionControl WorkFlowOpinionControl1;
	/// ]]>
	/// </code>
	/// <p>3.完成属性付值。（需要时可以为公用属性付匹配值）如下：</p>
	/// <code>
	/// <![CDATA[
	///     string actCode = "";
	///     if(Request["ActCode"] != null)
	///         actCode = Request["ActCode"].ToString();
	///     this.WorkFlowOpinionControl1.ActCode = actCode;
	///     this.WorkFlowOpinionControl1.Title = " 会签审核";
	///     this.WorkFlowOpinionControl1.SelectRouterUrl = "../WorkFlowContral/";
	///     this.WorkFlowOpinionControl1.TaskCode = "100098";
	///     this.WorkFlowOpinionControl1.TaskActorID = "1";
	/// ]]>
	/// </code>
	/// </example>
	/// *******************************************************************************************
	public partial class WorkFlowOpinionControl : System.Web.UI.UserControl
	{
		#region ********** 页面元素 **********
		// *****************************************************************************

		/// <summary>
		/// 标题显示 span 标签
		/// </summary>
		/// <summary>
		/// 内容显示 span 标签
		/// </summary>

		// *****************************************************************************
		#endregion

		#region  ********** 私有对象 **********
		// *****************************************************************************

		/// <summary>
		/// 当前登录用户
		/// </summary>
		private User _User = null;
		/// <summary>
		/// 动作代码
		/// </summary>
		private string _ActCode = null;
		/// <summary>
		/// 选择路由路径
		/// </summary>
		private string _SelectRouterUrl = null;
		/// <summary>
		/// 任务代码
		/// </summary>
		private string _TaskCode = null;
		/// <summary>
		/// 会签角色ID
		/// </summary>
		private string _TaskActorID = null;
		/// <summary>
		/// 会签人员列表
		/// </summary>
		private Hashtable _TaskActorHashtable = new Hashtable();



		// *****************************************************************************
		#endregion

		#region ********** 属性 **********
		// *****************************************************************************

		/// <summary>
		/// 显示标题（始终显示）
		/// </summary>
		public string Title
		{
			get
			{
				return this.titlespan.InnerHtml;
			}
			set
			{
				this.titlespan.InnerHtml = value;
			}
		}
		/// <summary>
		/// 当前系统用户。只提供 get 方法。
		/// </summary>
		public User IUser
		{
			get
			{
				if ( Session["User"] != null )
				{
					_User = (User)Session["User"];
				}
				return _User;
			}
		}
		/// <summary>
		/// 动作代码
		/// </summary>
		public string ActCode
		{
			get
			{
				if ( _ActCode == null )
				{
					if(this.ViewState["_ActCode"] != null)
					{
						return this.ViewState["_ActCode"].ToString();
					}
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
		/// 选择路由路径。默认为 "../../WorkFlowContral/" 。
		/// </summary>
		public string SelectRouterUrl
		{
			get
			{
				if ( _SelectRouterUrl == null )
				{
					if(this.ViewState["_SelectRouterUrl"] != null)
					{
						return this.ViewState["_SelectRouterUrl"].ToString();
					}
					return "../../WorkFlowContral/";
				}
				return _SelectRouterUrl;
			}
			set
			{
				_SelectRouterUrl = value;
				this.ViewState["_SelectRouterUrl"] = value;
			}
		}

		/// <summary>
		/// 任务代码
		/// </summary>
		public string TaskCode
		{
			get
			{
				if ( _TaskCode == null )
				{
					if(this.ViewState["_TaskCode"] != null)
					{
						return this.ViewState["_TaskCode"].ToString();
					}
					return "";
				}
				return _TaskCode;
			}
			set
			{
				_TaskCode = value;
				this.ViewState["_TaskCode"] = value;
			}
		}

		/// <summary>
		/// 会签角色ID。会签时使用，用来区分会签角色
		/// </summary>
		public string TaskActorID
		{
			get
			{
				if ( _TaskActorID == null )
				{
					if(this.ViewState["_TaskActorID"] != null)
					{
						return this.ViewState["_TaskActorID"].ToString();
					}
					return "";
				}
				return _TaskActorID;
			}
			set
			{
				_TaskActorID = value;
				this.ViewState["_TaskActorID"] = value;
			}
		}

		/// <summary>
		/// 会签人员列表。需要选择会签人员时使用。提供格式为哈希表。value 值为用户代码。
		/// </summary>
		public Hashtable TaskActorHashtable
		{
			get
			{
				return _TaskActorHashtable;
			}
			set
			{
				_TaskActorHashtable = value;
			}
		}

		// *****************************************************************************
		#endregion

		/// ****************************************************************************
		/// <summary>
		/// 页面加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( this.ActCode != "" )
			{
				LoadData();
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// 进行初始化显示过程。完成组件的状态和数据的刷新。
		/// </summary>
		/// ****************************************************************************
		public void ControlDataBind()
		{
			LoadData();
		}
		/// ****************************************************************************
		/// <summary>
		/// 数据加载
		/// </summary>
		/// ****************************************************************************
		private void LoadData()
		{
			try
			{
				WorkFlowActStrategyBuilder sb = new WorkFlowActStrategyBuilder();
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.WorkFlowActStrategyName.ActCode,this.ActCode));
	
				string sql = sb.BuildMainQueryString();
				
				QueryAgent QA = new QueryAgent();
				EntityData entity = QA.FillEntityData("WorkFlowAct",sql);
				this.ViewState["_CaseCode"] = entity.CurrentRow["CaseCode"].ToString();
				QA.Dispose();
				entity.Dispose();
				
				WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase( this.ViewState["_CaseCode"].ToString() );

				this.ViewState["_ApplicationCode"] = workCase.ApplicationCode;

				Act currentAct = workCase.GetAct( this.ActCode );

				Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode,true);

				this.ViewState["_ProcedureCode"] = procedure.ProcedureCode;

				Task currentTask = procedure.GetTask( currentAct.ToTaskCode );

				this.contentspan.InnerHtml = WriteOpinion( workCase, currentTask, currentAct );
			}
			catch (Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#region ********** 显示格式及数据的组织 ********** 
		// *****************************************************************************

		/// ****************************************************************************
		/// <summary>
		/// 生成显示脚本
		/// </summary>
		/// <param name="workCase">流程实体</param>
		/// <param name="currentTask">任务</param>
		/// <param name="currentAct">流程行动</param>
		/// <returns></returns>
		/// ****************************************************************************
		private string WriteOpinion (   WorkCase workCase, Task currentTask , Act currentAct  ) 
		{
			StringBuilder sb = new StringBuilder();

			EntityData pE = DAL.EntityDAO.WorkFlowDAO.GetStandard_WorkFlowProcedureByCode( workCase.ProcedureCode );
			EntityData wE = DAL.EntityDAO.WorkFlowDAO.GetStandard_WorkFlowCaseByCode( workCase.CaseCode );

			sb.Append( @"<table cellSpacing=0 cellPadding=0 width=100% border=0 class=list >" );

			DataRow[] dr = pE.Tables["WorkFlowTask"].Select( " TaskCode='"+this.TaskCode+"'","SortID",DataViewRowState.CurrentRows  );

			int taskType = (int)dr[0]["TaskType"];
			int HasOpinion = (int)dr[0]["HasOpinion"];

			sb.Append( @"<tr>" );
			sb.Append( @"<td>" );
			sb.Append( @"<br>" );

			// 如果是会签节点 
			if ( taskType == 5 )
			{
				/*******************  判断是否为会签最后一个参与者  **********************/
				bool SendFlag = false;
				if(StatusEndCount(this.ViewState["_CaseCode"].ToString(),this.TaskCode) == 1)
					SendFlag = true;

				/*******************  获取当前会签数据  **********************/
				DataRow[] drTaskActor = pE.Tables["WorkFlowTaskActor"].Select( String.Format( "TaskCode='{0}' and TaskActorID='{1}'",this.TaskCode,this.TaskActorID ),"IOrder",DataViewRowState.CurrentRows  );
					
				string taskActorID = BLL.ConvertRule.ToString(drTaskActor[0]["TaskActorID"]);
				string taskActorName = BLL.ConvertRule.ToString(drTaskActor[0]["taskActorName"]);

				bool isEdit = ( currentAct.ActUserCode == IUser.UserCode && this.TaskCode == currentTask.TaskCode && this.TaskActorID == currentAct.TaskActorID);
				DataRow[] drsOpinion = wE.Tables["WorkFlowOpinion"].Select( String.Format( "TaskCode='{0}' and TaskActorID='{1}'  "  ,this.TaskCode,this.TaskActorID ),"OpinionDate desc"  );
				if ( drsOpinion.Length > 0 )
				{
					if(currentAct.Status == ActStatus.DealWith && isActorUser() && isEdit)
					{
						BuildOpinionCanEdit ( sb , drsOpinion[0],true ,SendFlag,HasOpinion);
					}
					else
					{
						BuildOpinionCanNotEdit ( sb , drsOpinion[0] );
					}
				}
				else
				{
					if ( isEdit && isActorUser())
					{
						BuildOpinionCanEdit ( sb , null,true ,SendFlag,HasOpinion);
					}
				}

			}
			else // 非会签节点
			{
				bool isEdit = (currentAct.ActUserCode == IUser.UserCode && currentTask.TaskCode == this.TaskCode);
				DataRow[] drsOpinion = wE.Tables["WorkFlowOpinion"].Select( String.Format( "TaskCode='{0}'  "  ,this.TaskCode ) ,"OpinionDate desc" );
				if ( drsOpinion.Length > 0 )
				{
					if ( isEdit )
						BuildOpinionCanEdit ( sb , drsOpinion[0] ,false,true,HasOpinion);
					else
						BuildOpinionCanNotEdit ( sb , drsOpinion[0] );
				}
				else
				{
					if ( isEdit )
						BuildOpinionCanEdit ( sb , null,false,true,HasOpinion );
				}
			}

			sb.Append( @"<br>" );
			sb.Append( @"</td>" );
			sb.Append( @"</tr>" );
			
			sb.Append( @"</table>" );

			sb.Append("<script>");
			sb.Append("function returnSelectRouterControl( routerCode , routerName  , userCodes , userNames ,copyUsers)");
			sb.Append("{");
			sb.Append("    document.all(\"divRouterName\").value = routerName;");
			sb.Append("    document.all(\"divUserNames\").value = userNames;");
			sb.Append("    document.all(\""+this.ID+"_txtSelectRouterCode\").value = routerCode;");
			sb.Append("    document.all(\""+this.ID+"_txtSelectUserCodes\").value = userCodes;");
			sb.Append("    document.all(\""+this.ID+"_HiddenCopyUsers\").value = copyUsers;");
			sb.Append("    sendOpinionControl();");
			sb.Append("} ");
			sb.Append("	function sendOpinionControl()");
			sb.Append("{");
			sb.Append("    if ( (Form1."+this.ID+"_txtSelectRouterCode.value == '' )  || ( Form1."+this.ID+"_txtSelectUserCodes.value == '' ))");
			sb.Append("    {");
			sb.Append("        alert( '请选择流向 ！' );");
			sb.Append("        return;");
			sb.Append("    }");
			sb.Append("    if ( document.all(\"txtOpinion\") != null )");
			sb.Append("        Form1."+this.ID+"_HiddenOption.value = document.all(\"txtOpinion\").value;");
			sb.Append("    Form1."+this.ID+"_btnSendOpinion.onclick();");
			sb.Append("	}");

			sb.Append("	function endOpinionControl()");
			sb.Append("{");
			sb.Append("    if ( document.all(\"txtOpinion\") != null )");
			sb.Append("        Form1."+this.ID+"_HiddenOption.value = document.all(\"txtOpinion\").value;");
			sb.Append("    Form1."+this.ID+"_btnEndOpinion.onclick();");
			sb.Append("	}");
	
			sb.Append(" function saveOpinion()");
			sb.Append("{");
			sb.Append("    if ( document.all(\"txtOpinion\") != null )");
			sb.Append("        Form1."+this.ID+"_HiddenOption.value = document.all(\"txtOpinion\").value;");
			sb.Append("    Form1."+this.ID+"_btnSubmitOpinion.onclick();");
			sb.Append("	}");

			sb.Append(" function selectRouterControl(ProcedureCode,UserCode) ");
			sb.Append("{");
			sb.Append("    OpenMiddleWindow('"+this.SelectRouterUrl+"WorkFlowSelectRouter.aspx?ProcedureCode='+ProcedureCode+'&CaseCode="+this.ViewState["_CaseCode"].ToString()+"&ApplicationCode="+workCase.ApplicationCode+"&UserCode=' + UserCode + '&ActCode="+this.ActCode+"&Control=1'   ,'选择发送对象');");
			sb.Append("}");
			sb.Append("</script>");

			pE.Dispose();
			wE.Dispose();
			return sb.ToString();;
		}

		/// ****************************************************************************
		/// <summary>
		/// 可编辑的流程信息
		/// </summary>
		/// <param name="sb">显示字符串脚本</param>
		/// <param name="drOpinion">流程意见行对象</param>
		/// <param name="isSign">参数标识</param>
		/// ****************************************************************************
		private void BuildOpinionCanEdit ( StringBuilder sb , DataRow drOpinion , bool isSign ,bool SendFlag ,int HasOpinion )
		{
			string opinionText = "";
			if ( drOpinion != null )
			{
				opinionText = BLL.ConvertRule.ToString( drOpinion["OpinionText"]);
			}
			if ( HasOpinion == 1 )
			{
				sb.Append("<font color=\"#660066\">意&nbsp;&nbsp;&nbsp;见&nbsp;：&nbsp;</font><br>");
				sb.Append( @"<textarea id=txtOpinion rows=4 style='width:80%;' >" + opinionText  + @"</textarea>"  );
				sb.Append("<br>");
			}
			sb.Append( @"<input id=""divRouterName"" type=""hidden"" name=""divRouterName"" runat=""server"">" );
			sb.Append( @"<input id=""divUserNames"" type=""hidden"" name=""divUserNames"" runat=""server"">" );
			sb.Append("<font color=\"#660066\">用&nbsp;&nbsp;&nbsp;户&nbsp;：&nbsp;</font>");
			sb.Append( IUser.UserName ) ;
			sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
			sb.Append("<font color=\"#660066\">日&nbsp;&nbsp;&nbsp;期&nbsp;：&nbsp;</font>");
			sb.Append( DateTime.Now.ToString("yyyy-MM-dd")  );
			sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
			if ( isSign && HasOpinion == 1)
			{
				sb.Append( @"&nbsp;&nbsp;" );
				sb.Append( @"<input type=button class=button-small id=btnSaveOpinion onclick='saveOpinion();' value='会签意见' >"  );
			}
			sb.Append( @"&nbsp;&nbsp;" );

			if(SendFlag)
				sb.Append( @"<input type=button class=button-small id=btnSelectRouter onclick='selectRouterControl("+this.ViewState["_ProcedureCode"].ToString()+","+this.IUser.UserCode+");' value=' 完 成 ' >"  );
			else
				sb.Append( @"<input type=button class=button-small id=btnSelectRouter onclick='endOpinionControl();' value=' 完 成 ' >"  );

			sb.Append("<br>");
		}

		/// ****************************************************************************
		/// <summary>
		/// 不可编辑的流程信息
		/// </summary>
		/// <param name="sb">显示字符串脚本</param>
		/// <param name="drOpinion">流程意见行对象</param>
		/// ****************************************************************************
		private void BuildOpinionCanNotEdit ( StringBuilder sb , DataRow drOpinion )
		{
			string opinionText = "";
			string opinionDate = "";
			string opinionUserCode = "";
			if ( drOpinion != null )
			{
				opinionText = BLL.ConvertRule.ToString( drOpinion["OpinionText"]);
				opinionDate = BLL.ConvertRule.ToString( drOpinion["OpinionDate"]);
				opinionUserCode = BLL.ConvertRule.ToString( drOpinion["UserCode"]);
			}
			sb.Append("<font color=\"#660066\">意&nbsp;&nbsp;&nbsp;见&nbsp;：&nbsp;</font>");
			sb.Append( BLL.StringRule.FormartInput( opinionText ) ) ;
			sb.Append("<br>");
			sb.Append("<font color=\"#660066\">用&nbsp;&nbsp;&nbsp;户&nbsp;：&nbsp;</font>");
			sb.Append( BLL.SystemRule.GetUserName ( opinionUserCode ) );
			sb.Append("<br>");
			sb.Append("<font color=\"#660066\">日&nbsp;&nbsp;&nbsp;期&nbsp;：&nbsp;</font>");
			sb.Append( opinionDate );
			sb.Append("<br>");
		}
		/// ****************************************************************************
		/// <summary>
		/// 获取会签中还未完成的数量
		/// </summary>
		/// <param name="CaseCode">流程实体代码</param>
		/// <param name="ToTaskCode">目标流程任务代码</param>
		/// <returns>会签中还未完成的数量</returns>
		/// ****************************************************************************
		private int StatusEndCount(string CaseCode,string ToTaskCode)
		{
			TaskStatusStrategyBuilder sbq = new TaskStatusStrategyBuilder();

			sbq.AddStrategy( new Strategy( TaskActorStrategyName.CaseCode,CaseCode ));
			sbq.AddStrategy( new Strategy( TaskActorStrategyName.ToTaskCode,ToTaskCode ));
			sbq.AddStrategy( new Strategy( TaskActorStrategyName.Status,"End" ));

			string sql = sbq.BuildMainQueryString();

			QueryAgent qa = new QueryAgent();
			DataSet ds = qa.ExecSqlForDataSet(sql);

			int count = qa.FillEntityData("WorkFlowAct",sql).Tables[0].Rows.Count;

			qa.Dispose();

			if(this._TaskActorHashtable.Count != 0)
				count = count - _TaskActorHashtable.Count;

			return count;

		}

		/// ****************************************************************************
		/// <summary>
		/// 判断是否为会签人员
		/// </summary>
		/// <returns>是否为会签人员（true 为是，false 为不是）</returns>
		/// ****************************************************************************
		private bool isActorUser()
		{
			bool _isActorUser = false;

			if(_TaskActorHashtable.Count == 0)
			{
				_isActorUser = true;
			}
			else
			{
				System.Collections.IDictionaryEnumerator ActorIE = _TaskActorHashtable.GetEnumerator();

				while ( ActorIE.MoveNext())
				{
					if(ActorIE.Value.ToString() == IUser.UserCode.ToString())
						_isActorUser = true;
				}
			}
			return _isActorUser;
		}
		// *****************************************************************************
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region ********** 按钮事件集合 ********** 
		// *****************************************************************************

		/// ****************************************************************************
		/// <summary>
		/// 提交意见按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void btnSubmitOpinion_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase( this.ViewState["_CaseCode"].ToString() );

				Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode,true);

				Act currentAct = workCase.GetAct( this.ActCode );
				Task currentTask = procedure.GetTask(currentAct.ToTaskCode);
				Rms.WorkFlow.WorkCaseManager.SaveSignOpinionText(workCase,currentAct,currentTask,this.IUser.UserCode,this.HiddenOption.Value);
				BLL.WorkFlowRule.SaveWorkFlowCase(workCase);

				//WorkFlowSaveClick(this,EventArgs.Empty);

				Response.Write( Rms.Web.JavaScript.ScriptStart);
				Response.Write( Rms.Web.JavaScript.OpenerReload(false));
				Response.Write( Rms.Web.JavaScript.WinClose(false));
				Response.Write( Rms.Web.JavaScript.ScriptEnd);

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		
		}

		/// ****************************************************************************
		/// <summary>
		/// 发送按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void btnSendOpinion_ServerClick(object sender, System.EventArgs e)
		{
			string routerCode = this.txtSelectRouterCode.Value;
			string userCodes = this.txtSelectUserCodes.Value;
			string opinionText = this.HiddenOption.Value;
			string copyUsers = this.HiddenCopyUsers.Value;
			
			try
			{
				WorkCase workCase =  Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
				
				Rms.WorkFlow.WorkCaseManager.ForwardWorkCase(workCase,workCase.ApplicationCode,this.ActCode,routerCode,userCodes,this.IUser.UserCode,opinionText,"");
				if(copyUsers.Length>0)
					Rms.WorkFlow.WorkCaseManager.ForwardCopyWorkCase(workCase,workCase.ApplicationCode,this.ActCode,routerCode,copyUsers,this.IUser.UserCode,"","","");
				DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
				BLL.WorkFlowRule.SaveWorkFlowCase(ds,this.ViewState["_CaseCode"].ToString());
				ds.Dispose();
				//WorkFlowSendClick(this,EventArgs.Empty);
				Response.Write( Rms.Web.JavaScript.ScriptStart);
				Response.Write( Rms.Web.JavaScript.OpenerReload(false));
				Response.Write( Rms.Web.JavaScript.WinClose(false));
				Response.Write( Rms.Web.JavaScript.ScriptEnd);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// 完成按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void btnEndOpinion_ServerClick(object sender, System.EventArgs e)
		{
			string routerCode = this.txtSelectRouterCode.Value;
			string userCodes = this.txtSelectUserCodes.Value;
			string opinionText = this.HiddenOption.Value;
			
			try
			{
				WorkCase workCase =  Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
				
				Rms.WorkFlow.WorkCaseManager.EndWorkCase(workCase,workCase.ApplicationCode,this.ActCode,routerCode,userCodes,this.IUser.UserCode,opinionText);
				DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
				BLL.WorkFlowRule.SaveWorkFlowCase(ds,this.ViewState["_CaseCode"].ToString());
				ds.Dispose();
				//WorkFlowSendClick(this,EventArgs.Empty);
				Response.Write( Rms.Web.JavaScript.ScriptStart);
				Response.Write( Rms.Web.JavaScript.OpenerReload(false));
				Response.Write( Rms.Web.JavaScript.WinClose(false));
				Response.Write( Rms.Web.JavaScript.ScriptEnd);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		// *****************************************************************************
		#endregion 
	}
}
