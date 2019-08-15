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
using Rms.WorkFlow;

using System.Data.SqlClient;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// ExecuteSql 的摘要说明。
	/// </summary>
	public partial class ExecuteSql : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
       
            if (Request["sql"]==null||Request["sql"].ToLower() != "true") Button1.Visible = false;
		}
		///****************************************************************************************
		/// <summary>
		/// 创建 DataSet 数据集合，
		/// 根据数据库连接字符串和 sql 语句创建 DataSet 数据集合
		/// </summary>
		///****************************************************************************************
		private DataSet GetDataSet(string ConnString,string SqlString)
		{ 
			SqlConnection conn= new SqlConnection(ConnString);
			SqlCommand comm= new SqlCommand(SqlString,conn);
			comm.Connection.Open();

			SqlDataAdapter adapter=new SqlDataAdapter();
			adapter.SelectCommand=comm;

			DataSet ds=new DataSet();
			adapter.Fill(ds,"table");
            
			comm.Connection.Close();
			comm.Dispose();
			conn.Dispose();

			return ds;
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			string ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString ;
			this.DataGrid1.DataSource = GetDataSet(ConnString ,this.textareasql.Value);
			this.DataGrid1.DataBind();

		}
        protected void Button2_Click(object sender, EventArgs e)
        {
            this.textareasql.Value += @"
declare @CaseCode varchar(50)
-- 流水号
set @CaseCode = ''
delete from dbo.WorkFlowAct where CaseCode=@CaseCode
delete from dbo.WorkFlowActUser where CaseCode=@CaseCode
delete from dbo.WorkFlowCase where CaseCode=@CaseCode
delete from dbo.WorkFlowCaseProperty where WorkFlowCaseCode=@CaseCode
delete from dbo.WorkFlowOpinion where CaseCode=@CaseCode

select * from dbo.WorkFlowAct where CaseCode=@CaseCode
-- 注意：在执行流程数据删除前请确认业务数据的回滚！
";

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.TxtCaseCode.Value.Trim());
                Procedure procedure = DefinitionManager.GetProcedureDifinition(this.TxtProcedure.Value.Trim(), true);
                DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                /////////////////创建属性表///////////////////
                DataTable PropertyTable = BLL.WorkFlowRule.GetPropertyTable(workCase, procedure);

                if (PropertyTable.Select(this.textareasql.Value.Trim()).Length >= 0)
                {
                    this.Response.Write("<script>window.alert('语法正确！')</script>");
                }
            }
            catch (System.Exception ec)
            {
                this.Response.Write("<script>window.alert('语法错误！"+ec.Message+"')</script>");
            }
            
        }
}
}
