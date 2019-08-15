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
using RmsPM.BLL;
using Rms.ORMap;
using Rms.Web;


namespace RmsPM.Web.BiddingManage
{
    public partial class BiddingManage_ucBiddingWorkFlowLink : System.Web.UI.UserControl
    {
        private string biddingcode;
        private string state;
        public int i;
        protected void Page_Load(object sender, EventArgs e)
        {
            //用户代码以初始化页面
            if (!IsPostBack) 
            {
                IniPage();

               
              //  LoadData();
            }
        }
       
        public string BiddingCode 
        {
            get 
            {
                return biddingcode;
            }
            set 
            {
                biddingcode = value;
            }
        }
        public string State 
        {
            get 
            {
                return state;
            }
            set 
            {
                state = value;
            }
        }
        private void IniPage()
        {
            
        }
       public void LoadData()
        {
            try
            {
               if (this.biddingcode=="" || this.biddingcode==null)
               {
                    return;
               }
                BLL.BiddingPrejudication bp = new BiddingPrejudication();
                bp.BiddingCode = biddingcode;
                this.gvlist.DataSource = bp.GetBiddingPrejudications();
                this.gvlist.DataBind();
            }
            catch (Exception ex) 
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
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
        protected void gvlist_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
}
}