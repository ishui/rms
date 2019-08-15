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
	/// ExecuteSql ��ժҪ˵����
	/// </summary>
	public partial class ExecuteSql : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
       
            if (Request["sql"]==null||Request["sql"].ToLower() != "true") Button1.Visible = false;
		}
		///****************************************************************************************
		/// <summary>
		/// ���� DataSet ���ݼ��ϣ�
		/// �������ݿ������ַ����� sql ��䴴�� DataSet ���ݼ���
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
-- ��ˮ��
set @CaseCode = ''
delete from dbo.WorkFlowAct where CaseCode=@CaseCode
delete from dbo.WorkFlowActUser where CaseCode=@CaseCode
delete from dbo.WorkFlowCase where CaseCode=@CaseCode
delete from dbo.WorkFlowCaseProperty where WorkFlowCaseCode=@CaseCode
delete from dbo.WorkFlowOpinion where CaseCode=@CaseCode

select * from dbo.WorkFlowAct where CaseCode=@CaseCode
-- ע�⣺��ִ����������ɾ��ǰ��ȷ��ҵ�����ݵĻع���
";

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.TxtCaseCode.Value.Trim());
                Procedure procedure = DefinitionManager.GetProcedureDifinition(this.TxtProcedure.Value.Trim(), true);
                DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                /////////////////�������Ա�///////////////////
                DataTable PropertyTable = BLL.WorkFlowRule.GetPropertyTable(workCase, procedure);

                if (PropertyTable.Select(this.textareasql.Value.Trim()).Length >= 0)
                {
                    this.Response.Write("<script>window.alert('�﷨��ȷ��')</script>");
                }
            }
            catch (System.Exception ec)
            {
                this.Response.Write("<script>window.alert('�﷨����"+ec.Message+"')</script>");
            }
            
        }
}
}
