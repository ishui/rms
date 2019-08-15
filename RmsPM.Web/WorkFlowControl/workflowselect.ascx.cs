namespace RmsPM.Web.WorkFlowControl
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using RmsPM.DAL.QueryStrategy;
    using Rms.ORMap;
    using Rms.WorkFlow;
    using System.Collections;
    using System.Text;

    /// *******************************************************************************************
    /// <summary>
    ///		Workflowselect ��ժҪ˵�������̼�������б����
    /// </summary>
    /// *******************************************************************************************
    public partial class Workflowselect : System.Web.UI.UserControl
    {
        /// <summary>
        /// ���ô���
        /// </summary>
        /// <param name="ReferLinks"></param>
       // public string[][] RLinks;
        private void SetCode(string ReferLinks)
        {
           
            try
            {

                SetLinks(ReferLinks);
                //this.divHint.InnerText = this.txtHint.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string Value
        {
            get { return this.divValue.Value; }
            set { SetCode(value); }
        }

        /// ****************************************************************************
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void Page_Load(object sender, System.EventArgs e)
        {
        }
        protected void LinkButton1_Click(object sender, System.EventArgs e)
        {
            string MgrCode="";

            if (((TextBox)this.Parent.Parent.Parent.Parent.Parent.FindControl("MgrCode"))!=null)
            {
                string NewLinks = "";
                string[] OldLinks;
                OldLinks = this.divValue.Value.Split(';');
                foreach(string Link in OldLinks)
                {
                   if(Link.Equals(this.divSingleValue.Value))
                      continue;
                    if(Link!="")
                    NewLinks += Link+";";
                }

                SetLinks(NewLinks);
                
            }
        }

        private void SetLinks(string LinkString)
        {
            this.divValue.Value = LinkString;

            string Title = "";
            string[] Links = LinkString.Split(';');
            int i = -1;
            foreach (string Link in Links)
            {
                i++;
                if (!string.IsNullOrEmpty(Link))
                {
                    string[] RLinks = Link.Split(',');

                    string TempTitle = RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTitle(RLinks[0]) + "  ";
                    StringBuilder Titles = new StringBuilder();
                    Titles.Append("<a href=\"#\" id=\"btnDelete\" onclick=\"if(confirm('�Ƿ��Ƴ�ѡ��������Ϣ��'))PrebtnDelete('" + Link + "','" + ClientID + "');");
                    Titles.Append("else return false; ");
                    Titles.Append(" __doPostBack('" + ClientID.Replace("_", "$") + "$LinkButton1','')\"  name=\"btnDelete\"  >  ");

                    Titles.Append(TempTitle + "</a>");
                    Title += Titles.ToString();
                }
            }
            this.divName.InnerHtml = System.Web.HttpContext.Current.Server.HtmlDecode(Title);
        
        
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
        ///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
        ///		�޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

    }
}