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
	///	WorkFLowOpinionControl ���������̶��������
	/// Programmer:CLM; Email:nmgclm626@hotmail.com
	/// </summary>
	/// <remarks>
	/// ���������̶��������ҪΪ����������ơ�ÿ�������ɶ���������һ��������������̹�����ʹ�á�
	/// ����������� bug ���� Programmer ��ϵ��
	/// </remarks>
	/// <example>
	/// ����ʾ������˵������� WorkFlowOpinionControl �������̹�����
	/// <p>1.������з���ҳ��</p>
	/// <p>2.��������Ķ�����̡����£�</p>
	/// <code><![CDATA[
	///     protected RmsPM.Web.WorkFlowControl.WorkFlowOpinionControl WorkFlowOpinionControl1;
	/// ]]>
	/// </code>
	/// <p>3.������Ը�ֵ������Ҫʱ����Ϊ�������Ը�ƥ��ֵ�����£�</p>
	/// <code>
	/// <![CDATA[
	///     string actCode = "";
	///     if(Request["ActCode"] != null)
	///         actCode = Request["ActCode"].ToString();
	///     this.WorkFlowOpinionControl1.ActCode = actCode;
	///     this.WorkFlowOpinionControl1.Title = " ��ǩ���";
	///     this.WorkFlowOpinionControl1.SelectRouterUrl = "../WorkFlowContral/";
	///     this.WorkFlowOpinionControl1.TaskCode = "100098";
	///     this.WorkFlowOpinionControl1.TaskActorID = "1";
	/// ]]>
	/// </code>
	/// </example>
	/// *******************************************************************************************
	public partial class WorkFlowOpinionControl : System.Web.UI.UserControl
	{
		#region ********** ҳ��Ԫ�� **********
		// *****************************************************************************

		/// <summary>
		/// ������ʾ span ��ǩ
		/// </summary>
		/// <summary>
		/// ������ʾ span ��ǩ
		/// </summary>

		// *****************************************************************************
		#endregion

		#region  ********** ˽�ж��� **********
		// *****************************************************************************

		/// <summary>
		/// ��ǰ��¼�û�
		/// </summary>
		private User _User = null;
		/// <summary>
		/// ��������
		/// </summary>
		private string _ActCode = null;
		/// <summary>
		/// ѡ��·��·��
		/// </summary>
		private string _SelectRouterUrl = null;
		/// <summary>
		/// �������
		/// </summary>
		private string _TaskCode = null;
		/// <summary>
		/// ��ǩ��ɫID
		/// </summary>
		private string _TaskActorID = null;
		/// <summary>
		/// ��ǩ��Ա�б�
		/// </summary>
		private Hashtable _TaskActorHashtable = new Hashtable();



		// *****************************************************************************
		#endregion

		#region ********** ���� **********
		// *****************************************************************************

		/// <summary>
		/// ��ʾ���⣨ʼ����ʾ��
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
		/// ��ǰϵͳ�û���ֻ�ṩ get ������
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
		/// ��������
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
		/// ѡ��·��·����Ĭ��Ϊ "../../WorkFlowContral/" ��
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
		/// �������
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
		/// ��ǩ��ɫID����ǩʱʹ�ã��������ֻ�ǩ��ɫ
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
		/// ��ǩ��Ա�б���Ҫѡ���ǩ��Աʱʹ�á��ṩ��ʽΪ��ϣ��value ֵΪ�û����롣
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
		/// ҳ�����
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
		/// ���г�ʼ����ʾ���̡���������״̬�����ݵ�ˢ�¡�
		/// </summary>
		/// ****************************************************************************
		public void ControlDataBind()
		{
			LoadData();
		}
		/// ****************************************************************************
		/// <summary>
		/// ���ݼ���
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

		#region ********** ��ʾ��ʽ�����ݵ���֯ ********** 
		// *****************************************************************************

		/// ****************************************************************************
		/// <summary>
		/// ������ʾ�ű�
		/// </summary>
		/// <param name="workCase">����ʵ��</param>
		/// <param name="currentTask">����</param>
		/// <param name="currentAct">�����ж�</param>
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

			// ����ǻ�ǩ�ڵ� 
			if ( taskType == 5 )
			{
				/*******************  �ж��Ƿ�Ϊ��ǩ���һ��������  **********************/
				bool SendFlag = false;
				if(StatusEndCount(this.ViewState["_CaseCode"].ToString(),this.TaskCode) == 1)
					SendFlag = true;

				/*******************  ��ȡ��ǰ��ǩ����  **********************/
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
			else // �ǻ�ǩ�ڵ�
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
			sb.Append("        alert( '��ѡ������ ��' );");
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
			sb.Append("    OpenMiddleWindow('"+this.SelectRouterUrl+"WorkFlowSelectRouter.aspx?ProcedureCode='+ProcedureCode+'&CaseCode="+this.ViewState["_CaseCode"].ToString()+"&ApplicationCode="+workCase.ApplicationCode+"&UserCode=' + UserCode + '&ActCode="+this.ActCode+"&Control=1'   ,'ѡ���Ͷ���');");
			sb.Append("}");
			sb.Append("</script>");

			pE.Dispose();
			wE.Dispose();
			return sb.ToString();;
		}

		/// ****************************************************************************
		/// <summary>
		/// �ɱ༭��������Ϣ
		/// </summary>
		/// <param name="sb">��ʾ�ַ����ű�</param>
		/// <param name="drOpinion">��������ж���</param>
		/// <param name="isSign">������ʶ</param>
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
				sb.Append("<font color=\"#660066\">��&nbsp;&nbsp;&nbsp;��&nbsp;��&nbsp;</font><br>");
				sb.Append( @"<textarea id=txtOpinion rows=4 style='width:80%;' >" + opinionText  + @"</textarea>"  );
				sb.Append("<br>");
			}
			sb.Append( @"<input id=""divRouterName"" type=""hidden"" name=""divRouterName"" runat=""server"">" );
			sb.Append( @"<input id=""divUserNames"" type=""hidden"" name=""divUserNames"" runat=""server"">" );
			sb.Append("<font color=\"#660066\">��&nbsp;&nbsp;&nbsp;��&nbsp;��&nbsp;</font>");
			sb.Append( IUser.UserName ) ;
			sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
			sb.Append("<font color=\"#660066\">��&nbsp;&nbsp;&nbsp;��&nbsp;��&nbsp;</font>");
			sb.Append( DateTime.Now.ToString("yyyy-MM-dd")  );
			sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
			if ( isSign && HasOpinion == 1)
			{
				sb.Append( @"&nbsp;&nbsp;" );
				sb.Append( @"<input type=button class=button-small id=btnSaveOpinion onclick='saveOpinion();' value='��ǩ���' >"  );
			}
			sb.Append( @"&nbsp;&nbsp;" );

			if(SendFlag)
				sb.Append( @"<input type=button class=button-small id=btnSelectRouter onclick='selectRouterControl("+this.ViewState["_ProcedureCode"].ToString()+","+this.IUser.UserCode+");' value=' �� �� ' >"  );
			else
				sb.Append( @"<input type=button class=button-small id=btnSelectRouter onclick='endOpinionControl();' value=' �� �� ' >"  );

			sb.Append("<br>");
		}

		/// ****************************************************************************
		/// <summary>
		/// ���ɱ༭��������Ϣ
		/// </summary>
		/// <param name="sb">��ʾ�ַ����ű�</param>
		/// <param name="drOpinion">��������ж���</param>
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
			sb.Append("<font color=\"#660066\">��&nbsp;&nbsp;&nbsp;��&nbsp;��&nbsp;</font>");
			sb.Append( BLL.StringRule.FormartInput( opinionText ) ) ;
			sb.Append("<br>");
			sb.Append("<font color=\"#660066\">��&nbsp;&nbsp;&nbsp;��&nbsp;��&nbsp;</font>");
			sb.Append( BLL.SystemRule.GetUserName ( opinionUserCode ) );
			sb.Append("<br>");
			sb.Append("<font color=\"#660066\">��&nbsp;&nbsp;&nbsp;��&nbsp;��&nbsp;</font>");
			sb.Append( opinionDate );
			sb.Append("<br>");
		}
		/// ****************************************************************************
		/// <summary>
		/// ��ȡ��ǩ�л�δ��ɵ�����
		/// </summary>
		/// <param name="CaseCode">����ʵ�����</param>
		/// <param name="ToTaskCode">Ŀ�������������</param>
		/// <returns>��ǩ�л�δ��ɵ�����</returns>
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
		/// �ж��Ƿ�Ϊ��ǩ��Ա
		/// </summary>
		/// <returns>�Ƿ�Ϊ��ǩ��Ա��true Ϊ�ǣ�false Ϊ���ǣ�</returns>
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region ********** ��ť�¼����� ********** 
		// *****************************************************************************

		/// ****************************************************************************
		/// <summary>
		/// �ύ�����ť�¼�
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
		/// ���Ͱ�ť�¼�
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
		/// ��ɰ�ť�¼�
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
