using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;
using System.Configuration;

namespace RmsPM.Web.BiddingManage
{

    public partial class BiddingManage_BiddingContractCreate : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            //BLL.BiddingManage.GetContractDefaultValue("100027");
        }
        /// ****************************************************************************
        /// <summary>
        /// 数据加载
        /// </summary>
        /// ****************************************************************************
        private void LoadData()
        {
            try
            {
                if (Request["ProjectCode"] != null)
                    this.ProjectCode = Request["ProjectCode"].ToString();
                BLL.BiddingMessage cBiddingMessage = new BLL.BiddingMessage();
                cBiddingMessage.BiddingCode = Request["BiddingCode"] + "";
                cBiddingMessage.State = "0";
                DataTable dt = cBiddingMessage.GetBiddingMessages();
                this.dgList.DataSource = dt;
                this.dgList.DataBind();
                string company = this.up_sPMName.ToLower();
                switch (company)
                {
                   
                    case "zhudingpm":
                        this.dgList.Columns[7].Visible = true;
                        break;
                    case "tangchenpm":
                        this.dgList.Columns[5].Visible = false;
                        break;
                }
                
                this.gpControl.RowsCount = dt.Rows.Count.ToString();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        private void gpControl_PageIndexChange(object sender, System.EventArgs e)
        {
            LoadData();
        }



    }
}
