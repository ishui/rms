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
using Rms.Web;
using RmsPM.DAL;
using RmsReport;

public partial class RmsOA_GK_OA_OutFileSignReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void Button1_ServerClick(object sender, EventArgs e)
    {
        LoadData();
    }

    private DataTable LoadData()
    {
        string strSql = "select code,filecode,filetitle,registerdate from GK_OA_OutFileSign  where status='3'";


        //文件编号
        if (this.FileCodeTextBox.Text != "")
        {
            strSql += " and filecode like '%" + this.FileCodeTextBox.Text + "%'";
        }

        //文件标题
        if (this.FileTitleTextBox.Text != "")
        {
            strSql += " and filetitle like '%" + this.FileTitleTextBox.Text + "%'";
        }

        //部门
        if (this.txtUnit.Value != "")
        {
            strSql += " and NB_UnitCode = '%" + this.txtUnit.Value + "%'";
        }

        //登记日期
        if (this.dtDateBegin.Value != "")
        {
            strSql += " and RegisterDate >='" + DateTime.Parse(this.dtDateBegin.Value) + "'";

        }
        if (this.dtDateEnd.Value != "")
        {
            strSql += " and RegisterDate <='" + DateTime.Parse(this.dtDateEnd.Value) + "'";

        }

        strSql += " order by  code ";

        DataTable tb= null ;
        DataTable tbCopy = null ;
        int j = 1;
        QueryAgent qa = new QueryAgent();
        try
        {
            tb = qa.ExecSqlForDataSet(strSql).Tables[0];

            DataTable tbNew = tb.Clone();

            tbNew.Columns.Add("DteSN", typeof(string));
            tbNew.Columns.Add("RegisterDate1", typeof(string));
            tbNew.Columns.Add("UserCode", typeof(string));
            tbNew.Columns.Add("Status", typeof(string));
            tbNew.Columns.Add("SignDate", typeof(string));

            foreach (DataRow dr in tb.Rows)
            {
                string strCopy = "select a.* ,b.usercode as usercode from workflowact a , WorkFlowActUser b, workflowcase c,workflowprocedure d"
                                   +" where  a.copy='1' and  a.actcode= b.actcode and c.procedurecode =d.procedurecode "
                                   +" and a.casecode= c.casecode and d.procedurename='文件签发单' and d.activity=1 and c.applicationcode='"+dr["Code"]+"'";
                tbCopy = qa.ExecSqlForDataSet(strCopy).Tables[0];
                if (tbCopy.Rows.Count > 0)
                {
                    for(int i= 0;i<tbCopy.Rows.Count;i++)
                    {
                        DataRow drNew = tbNew.NewRow();
                       
                        if (i == 0)

                        {   int DteSN = j++;
                            drNew["DteSN"] = DteSN;
                            drNew["filecode"] = dr["FileCode"];
                            drNew["filetitle"] = dr["filetitle"];
                            drNew["RegisterDate1"] = dr["RegisterDate"].ToString().Substring(0,10);

                            drNew["usercode"] = tbCopy.Rows[i]["usercode"];
                            drNew["status"] = ChangeStatus(tbCopy.Rows[i]["status"]);
                            drNew["signdate"] = tbCopy.Rows[i]["signdate"];

                        }
                        else
                        {
                            drNew["filecode"] = System.DBNull.Value;
                            drNew["filetitle"] = System.DBNull.Value;
                            drNew["RegisterDate1"] = System.DBNull.Value;

                            drNew["usercode"] = tbCopy.Rows[i]["usercode"];
                            drNew["status"] = ChangeStatus(tbCopy.Rows[i]["status"]);
                            drNew["signdate"] = tbCopy.Rows[i]["signdate"];
                        }

                        tbNew.Rows.Add(drNew);
                    }
                }

            }

            this.GridView1.DataSource = tbNew;
            this.GridView1.DataBind();
        }
        finally
        {
            qa.Dispose();
        }

        return tb;
    }

    private string  ChangeStatus(object str)
    {
        string strStatus = "";

        if (str.ToString() == "Begin")
        {
            strStatus = "未签收";
        }

        if (str.ToString() == "DealWith")
        {
            strStatus = "处理中";
        }

        if (str.ToString() == "End")
        {
            strStatus = "已完成";
        }
        return strStatus;
    }
}
