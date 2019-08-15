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
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// ConfigFunction ��ժҪ˵����
	/// </summary>
	public partial class ConfigFunction : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtInputCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtRightCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtRoleCode;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				string mainCode = Request["MainCode"]+"";
				IniPage();

				if ( mainCode != "" )
					LoadData();
			}
		}

		private void IniPage()
		{
			string mainCode = Request["MainCode"]+"";
			try
			{
				FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.Deep,"1" )  );
				QueryAgent qa = new QueryAgent();
				EntityData mainFunction = qa.FillEntityData("FunctionStructure",sb.BuildMainQueryString());
				qa.Dispose();
				this.dtlMainFunction.DataSource = mainFunction;
				this.dtlMainFunction.DataBind();
				mainFunction.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void LoadData()
		{
			string mainCode = Request["MainCode"]+"";
			try
			{
				FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.ChildTreeNode,mainCode )  );
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("FunctionStructure",sb.BuildMainQueryString());
				entity.CurrentTable.Columns.Add( "ShowCode" );
				qa.Dispose();

				string allCode = "";
				string aCode = "";

				int iCount = entity.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					string code = entity.GetString( "FunctionStructureCode" );
					int deep = entity.GetInt("Deep");
					entity.CurrentRow["ShowCode"] = BuildShowCode(deep);

					int iA = entity.GetInt( "IsAvailable" );
					if ( allCode != "" )
					{
						allCode+=",";
						aCode+=",";
					}

					allCode += code;
					if ( iA != 1 )
						aCode += code;
				}
				
				this.rptFunction.DataSource = entity;
				this.rptFunction.DataBind();
				entity.Dispose();

				this.txtAllCode.Value = allCode;
				this.txtACode.Value = aCode;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

		}


		private string BuildShowCode( int deep )
		{
			string s = "";
			for( int i=0;i<4*deep;i++)
				s += "&nbsp;";

			return s;
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

		}
		#endregion

		private void CloseWindow(  )
		{
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{

				string mainCode = Request["MainCode"]+"";
				string[] checkCodes = this.txtACode.Value.Split( new char[]{','} );

				FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.ChildTreeNode,mainCode )  );
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("FunctionStructure",sb.BuildMainQueryString());
				qa.Dispose();

				int iCount = entity.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					string code = entity.GetString("FunctionStructureCode");
					bool isFounded = false;

					foreach ( string c in checkCodes)
					{
						if ( c==code)
						{
							isFounded = true;
							break;
						}
					}
					if ( isFounded )
						entity.CurrentRow["IsAvailable"] = 0;
					else
						entity.CurrentRow["IsAvailable"] = 1;
				}

				DAL.EntityDAO.SystemManageDAO.SubmitAllFunctionStructure(entity);
				entity.Dispose();

				Response.Write( Rms.Web.JavaScript.ScriptStart );
				Response.Write( " window.navigate( 'ConfigFunction.aspx' ) " );
				Response.Write( Rms.Web.JavaScript.ScriptEnd );

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"�������");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������"));
				return;
			}
			
		}

	}
}
