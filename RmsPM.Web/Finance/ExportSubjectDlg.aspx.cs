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
    /// Finance_ExportSubjectDlg ��ժҪ˵����
    /// </summary>
    public partial class ExportSubjectDlg : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // �ڴ˴������û������Գ�ʼ��ҳ��
           // this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];
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

        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                string projectCode = Request["ProjectCode"] + "";
                string subjectsetcode = Request.QueryString["SubjectSetCode"];
                EntityData entity = DAL.EntityDAO.SubjectDAO.GetSubjectBySubjectSet(subjectsetcode);

                string vPath = Server.MapPath(ConfigurationSettings.AppSettings["VirtualDirectory"]);
                string path = vPath + @"\temp\";
                string fileName = "��ƿ�Ŀ����" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";

                StreamWriter w = new StreamWriter(path + fileName, false, System.Text.Encoding.Default);
            
                w.WriteLine("����,����,��Ŀ����,��Ŀ����,������,��ұ���,������λ,����������,��ҳ��ʽ,����,�Ƿ�跽��Ŀ,�Ƿ������Ŀ");
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
                        
                        sTemp += (dr[i]["IsDebit"].ToString() == "1" ? "��" : "��") + ",";
                        sTemp += (dr[i]["IsCrebit"].ToString() == "1" ? "��" : "��") + ",";
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
                Response.Write(JavaScript.Alert(true, "��������" + ex.Message));
                return;
            }
        }

    }
}

