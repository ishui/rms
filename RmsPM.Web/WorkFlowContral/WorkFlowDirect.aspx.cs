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
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;
using Rms.WorkFlow;


namespace RmsPM.Web.WorkFlowContral
{
	/// <summary>
	/// 选择是进入到信息页面还是到info页面
	/// </summary>
	public partial class WorkFlowDirect :PageBase
	{


	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//string appliactionCode = Request["ApplicationCode"]+"";
			string actCode = Request["ActCode"]+"";
			string caseCode = Request["CaseCode"] + "";

			if ( actCode == "" || caseCode == "" )
			{
				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.Alert(false,"没有流程编号 ！"));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
				return;
			}

			WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(caseCode );
			if ( workCase == null )
			{
				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.Alert(false,"没有这个流程 ！"));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
				return;
			}

			Act act = workCase.GetAct(actCode);

			if ( act == null )
			{
				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.Alert(false,"没有这个动作 ！"));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
				return;
			}

			string action = Request["Action"]+"";
			if ( action == "DealWith" )
			{
				Procedure procedure = Rms.WorkFlow.DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode,true);
				Task task = procedure.GetTask( act.ToTaskCode);
				if ( task.CanEdit == 1 )
					GotoApplicationEdit(procedure.ApplicationPath,workCase.ApplicationCode,caseCode,actCode,"Info");
				else
                    GotoApplicationInfo(procedure.ApplicationPath, workCase.ApplicationCode, caseCode, actCode, "Info");
			}

		}

		private void GotoApplicationInfo( string applicationPath, string applicationCode , string caseCode , string actCode , string frameType )
		{
			string url = applicationPath + ((applicationPath.IndexOf("?")>0)?"&":"?")+"FrameType="+frameType+"&ApplicationCode=" + applicationCode + "&CaseCode=" + caseCode + "&ActCode=" + actCode;
			Response.Write(Rms.Web.JavaScript.PageTo(true,url) );


			//Response.Write(Rms.Web.JavaScript.ScriptStart);
			//Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			//Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		private void GotoApplicationEdit ( string applicationPath, string applicationCode , string caseCode , string actCode , string frameType )
		{
			string url = applicationPath + ((applicationPath.IndexOf("?")>0)?"&":"?")+"FrameType="+frameType+"&ApplicationCode=" + applicationCode  + "&CaseCode=" + caseCode + "&Act=Edit&ActCode=" + actCode;
			Response.Write(Rms.Web.JavaScript.PageTo(true,url) );
			//Response.Write(Rms.Web.JavaScript.ScriptStart);
			//Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			//Response.Write(Rms.Web.JavaScript.ScriptEnd);
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



	}
}
