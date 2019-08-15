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
	/// ConfigRoleRight ��ժҪ˵����
	/// </summary>
	public partial class ConfigRoleRight : PageBase
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
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsAvailable,"0" )  );
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsRightControlPoint,"0" )  );
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsRoleControlPoint,"0" )  );

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
			string roleCode = Request["RoleCode"]+"";
			try
			{
				EntityData role = DAL.EntityDAO.SystemManageDAO.GetStandard_RoleByCode(roleCode);

				FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.ChildTreeNode,mainCode )  );
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsAvailable,"0" )  );
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsRightControlPoint,"0" )  );
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsRoleControlPoint,"0" )  );

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
						allCode+=",";

					allCode += code;
				}

				role.SetCurrentTable("RoleOperation");
				foreach ( DataRow dr in role.CurrentTable.Select( String.Format( " OperationCode like '{0}%' ",mainCode ) ))
				{
					string code = (string)dr["OperationCode"];
					if ( aCode != "" )
						aCode += ",";
					aCode += code;
				}
				
				this.rptFunction.DataSource = entity;
				this.rptFunction.DataBind();
				entity.Dispose();
				role.Dispose();

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
				string roleCode = Request["RoleCode"]+"";

				string[] checkCodes = this.txtACode.Value.Split( new char[]{','} );

				EntityData role = DAL.EntityDAO.SystemManageDAO.GetStandard_RoleByCode(roleCode);

				FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.ChildTreeNode,mainCode )  );
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("FunctionStructure",sb.BuildMainQueryString());
				qa.Dispose();

				role.SetCurrentTable("RoleOperation");
				foreach ( DataRow dr in role.CurrentTable.Select( String.Format( " OperationCode like '{0}%' ",mainCode ) ))
					dr.Delete();

				foreach ( string code in checkCodes )
				{
					if ( code != "" )
					{
						DataRow dr = role.GetNewRecord();
						dr["RoleCode"] = roleCode;
						dr["OperationCode"]=code;
						role.AddNewRecord(dr);
					}
				}

				DAL.EntityDAO.SystemManageDAO.SubmitAllStandard_Role(role);

				entity.Dispose();
				role.Dispose();

				Response.Write( Rms.Web.JavaScript.ScriptStart );
                Response.Write(" window.opener.location.reload(); ");
				Response.Write( " window.navigate( 'ConfigRoleRight.aspx?RoleCode="+roleCode+"' ); " );
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
