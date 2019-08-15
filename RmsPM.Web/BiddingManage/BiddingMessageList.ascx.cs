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

using Rms.Web;



public partial class BiddingManage_BiddingMessageList : System.Web.UI.UserControl
{
    /// <summary>
    /// 添加按钮
    /// </summary>
    public bool UsebtnAdd
    {
        get
        {
            return this.btnAdd.Visible;
        }
        set
        {
            this.btnAdd.Visible = value;
        }
    }
    /// <summary>
    /// 移除按钮
    /// </summary>
    public bool UsebtnRemove
    {
        get
        {
            return this.btnRemove.Visible;
        }
        set
        {
            this.btnRemove.Visible = value;
        }
    }
    /// <summary>
    /// 单步审核
    /// </summary>
    public bool UsebtnApprove
    {
        get
        {
            return this.btnApprove.Visible;
        }
        set
        {
            this.btnApprove.Visible = value;
        }
    }

    /// <summary>
    /// 撤消审核
    /// </summary>
    public bool UsebtnCancelApprove
    {
        get
        {
            return this.btnCancelApprove.Visible;
        }
        set
        {
            this.btnCancelApprove.Visible = value;
        }
    }


    private string _BiddingCode = "";

    public string BiddingCode
    {
        get
        {
            if (_BiddingCode == "")
            {
                if (this.ViewState["_BiddingCode"] != null)
                    return this.ViewState["_BiddingCode"].ToString();
                return "";
            }
            return _BiddingCode;
        }
        set
        {
            _BiddingCode = value;
            this.ViewState["_BiddingCode"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void InitControl()
    {
        this.LoadData();

        //权限控制
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
            RmsPM.BLL.BiddingMessage cBiddingMessage = new RmsPM.BLL.BiddingMessage();
            cBiddingMessage.BiddingCode = this.BiddingCode;
          
            DataTable dt = cBiddingMessage.GetBiddingMessages();
            this.dgList.DataSource = dt;
            this.dgList.DataBind();

            //this.gpControl.RowsCount = dt.Rows.Count.ToString();
            dt.Dispose();
        }
        catch (Exception ex)
        {
            RmsPM.Web.LogHelper.WriteLog(ex.Message);
         
        }
    }
    ///// ****************************************************************************
    ///// <summary>
    ///// 分页事件
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// ****************************************************************************
    //private void gpControl_PageIndexChange(object sender, System.EventArgs e)
    //{
    //    LoadData();
    //}



    protected void btnRemove_Click(object sender, EventArgs e)
    {
        foreach (DataGridItem item in dgList.Items)
        {
            CheckBox isSelected = (CheckBox)item.FindControl("isSelected");
            string BiddingMessageCode = item.Cells[1].Text;
            if (isSelected != null && isSelected.Checked)
            {
                RmsPM.BLL.BiddingMessage cbiddingMessage = new RmsPM.BLL.BiddingMessage();
                cbiddingMessage.BiddingMessageCode = BiddingMessageCode;
             
                cbiddingMessage.BiddingMessageDelete();
            }
        }
        this.LoadData();
        Response.Write(JavaScript.ScriptStart);
        Response.Write("window.Opener.location.reload();");
        Response.Write(JavaScript.ScriptEnd);
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {

        foreach (DataGridItem item in dgList.Items)
        {
            CheckBox isSelected = (CheckBox)item.FindControl("isSelected");
            string BiddingMessageCode = item.Cells[1].Text;
            if (isSelected != null && isSelected.Checked)
            {
                RmsPM.BLL.BiddingMessage cbiddingMessage = new RmsPM.BLL.BiddingMessage();
                cbiddingMessage.BiddingMessageCode = BiddingMessageCode;
                cbiddingMessage.State = "0";
                cbiddingMessage.BiddingMessageSubmit();
            }
        }
        this.LoadData();
    }
    protected void btnCancelApprove_Click(object sender, EventArgs e)
    {
        foreach (DataGridItem item in dgList.Items)
        {
            CheckBox isSelected = (CheckBox)item.FindControl("isSelected");
            string BiddingMessageCode = item.Cells[1].Text;
            if (isSelected != null && isSelected.Checked)
            {
                RmsPM.BLL.BiddingMessage cbiddingMessage = new RmsPM.BLL.BiddingMessage();
                cbiddingMessage.BiddingMessageCode = BiddingMessageCode;
                cbiddingMessage.State = "1";
                cbiddingMessage.BiddingMessageSubmit();
            }
        }
        this.LoadData();
    }
}
