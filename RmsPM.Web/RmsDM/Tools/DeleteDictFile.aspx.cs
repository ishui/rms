using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rms.DBUtility;


public partial class RmsDM_Tools_DeleteDictFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void DeleteDictFiles(string DictCode)
    {
        string ConnStr = SqlHelper.DBConnString;
        SqlConnection conn=new SqlConnection(ConnStr);
        conn.Open();
        SqlTransaction Trans =conn.BeginTransaction();
        try
        {
            string Sql = "select wfc.CaseCode from DocumentFile df";
            Sql = Sql + " join DocumentDirectory dd on dd.FileTemplateCode=df.FileTemplateCode and dd.DepartmentCode=df.ApplyDepartmentCode";
            Sql = Sql + " join FileTemplateVersion ftv on  df.FileTemplateCode=ftv.FileTemplateCode and df.VersionNumber=ftv.VersionNumber";
            Sql = Sql + " join WorkFlowProcedure wfp on ftv.WorkFlowProcedureName=wfp.ProcedureName";
            Sql = Sql + " join WorkFlowCase wfc on wfp.ProcedureCode=wfc.ProcedureCode and wfc.ApplicationCode=df.Code";
            Sql = Sql + " where dd.ParentCode=@ParentCode";
            SqlParameter[] parameters = {
					new SqlParameter("@ParentCode", SqlDbType.VarChar,50)
            };
            parameters[0].Value = DictCode;
            //删除流程
            SqlDataReader reader = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, Sql, parameters);
            while (reader.Read())
            {
                SqlParameter[] parameters1 = {
					new SqlParameter("@CaseCode", SqlDbType.VarChar,50)
                };
                parameters1[0].Value = reader["CaseCode"].ToString();
                
                Sql = "delete from dbo.WorkFlowAct where CaseCode=@CaseCode";
                SqlHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, parameters1);

                Sql = "delete from dbo.WorkFlowActUser where CaseCode=@CaseCode";
                SqlHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, parameters1);

                Sql = "delete from dbo.WorkFlowCase where CaseCode=@CaseCode";
                SqlHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, parameters1);

                Sql = "delete from dbo.WorkFlowCaseProperty where CaseCode=@CaseCode";
                SqlHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, parameters1);

                Sql = "delete from dbo.WorkFlowOpinion where CaseCode=@CaseCode";
                SqlHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, parameters1);
            };
            //删除文档目录
            Sql = "delete from dbo.DocumentFile where Code in (";
            Sql = Sql + "select df.Code from DocumentDirectory dd";
            Sql = Sql + " join documentFile df on dd.FileTemplateCode=df.FileTemplateCode and dd.DepartmentCode=df.ApplyDepartmentCode ";
            Sql = Sql + " where dd.ParentCode=@ParentCode";
            Sql = Sql + " )";
            SqlHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, parameters);

            Sql = "delete from dbo.DocumentDirectory where DirectoryNodeCode is not null and DirectoryNodeCode<>'' and ParentCode=@ParentCode";
            SqlHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, parameters);

            Trans.Commit();
        }
        catch(Exception ex)
        {
            Trans.Rollback();
            throw ex;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DeleteDictFiles(TextBox1.Text);
    }
}
