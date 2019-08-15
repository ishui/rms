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
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;
using System.IO;
using System.Configuration;

namespace RmsPM.Web.Sal
{
    /// <summary>
    /// Finance_ExportSubjectDlg 的摘要说明。
    /// </summary>
    public partial class ExportSubjectDlg : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
           // this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];
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

        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                string projectCode = Request["ProjectCode"] + "";
                string subjectsetcode = Request.QueryString["SubjectSetCode"];
                EntityData entity = DAL.EntityDAO.SubjectDAO.GetSubjectBySubjectSet(subjectsetcode);

                string vPath = Server.MapPath(ConfigurationSettings.AppSettings["VirtualDirectory"]);
                string path = vPath + @"\temp\";
                string fileName = "会计科目导出" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";

                StreamWriter w = new StreamWriter(path + fileName, false, System.Text.Encoding.Default);
            
                w.WriteLine("类型,级次,科目编码,科目名称,助记码,外币币种,计量单位,辅助账类型,账页格式,余额方向,是否借方科目,是否贷方科目");
                //if (wbs.CurrentTable.Rows.Count > 0)
                //{ SubjectCode SubjectName IsDebit IsCrebit
                if (entity.HasRecord())
                {
                    int i = 0;
                    DataRow[] dr = entity.CurrentTable.Select("SubjectSetCode='" + subjectsetcode + "'");
                    foreach (DataRow singledr in dr)
                    {
                        string sTemp = "";
                        sTemp += dr[i]["SubjectType"].ToString() + ",";
                        sTemp += dr[i]["Layer"].ToString() + ",";
                        sTemp += dr[i]["SubjectCode"].ToString() + ",";

                        sTemp += dr[i]["SubjectName"].ToString() + ",";
                        sTemp += dr[i]["OtherCode"].ToString() + ",";
                        sTemp += dr[i]["Currentcy"].ToString() + ",";
                        sTemp += dr[i]["Unit"].ToString() + ",";
                        sTemp += dr[i]["AssistantType"].ToString() + ",";
                        sTemp += dr[i]["Format"].ToString() + ",";
                        sTemp += dr[i]["Balance"].ToString() + ",";
                        
                        sTemp += (dr[i]["IsDebit"].ToString() == "1" ? "是" : "否") + ",";
                        sTemp += (dr[i]["IsCrebit"].ToString() == "1" ? "是" : "否") + ",";
                        i++;
                        w.WriteLine(sTemp);
                    }
                    w.Flush();
                    w.Close();
                    entity.Dispose();
                    Response.Redirect("../Temp/" + fileName);

                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(JavaScript.Alert(true, "导出出错：" + ex.Message));
                return;
            }
        }

    }
}

