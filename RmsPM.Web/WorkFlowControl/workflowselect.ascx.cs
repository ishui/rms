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
    ///		Workflowselect 的摘要说明。流程监控数据列表组件
    /// </summary>
    /// *******************************************************************************************
    public partial class Workflowselect : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置代码
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
        /// 组件加载
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
                    Titles.Append("<a href=\"#\" id=\"btnDelete\" onclick=\"if(confirm('是否移除选中流程信息？'))PrebtnDelete('" + Link + "','" + ClientID + "');");
                    Titles.Append("else return false; ");
                    Titles.Append(" __doPostBack('" + ClientID.Replace("_", "$") + "$LinkButton1','')\"  name=\"btnDelete\"  >  ");

                    Titles.Append(TempTitle + "</a>");
                    Title += Titles.ToString();
                }
            }
            this.divName.InnerHtml = System.Web.HttpContext.Current.Server.HtmlDecode(Title);
        
        
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
        ///		设计器支持所需的方法 - 不要使用代码编辑器
        ///		修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

    }
}