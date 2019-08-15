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
            string Sql = "select * from DocuemntDictionary where ParentCode=@ParentCode";
            SqlParameter[] parameters = {
					new SqlParameter("@ParentCode", SqlDbType.VarChar,50)
            };
            parameters[0].Value = DictCode;
            SqlDataReader read = SqlHelper.ExecuteReader(conn, CommandType.Text, Sql, parameters);
            while (reader.Read())
            {
                attach.AttachmentCode = reader["AttachmentCode"].ToString();

                SqlHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, parameters);
            };

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
