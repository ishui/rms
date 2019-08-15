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

public partial class BiddingManage_BiddingPrejudicationSupplierList : System.Web.UI.UserControl
{
    private string _BiddingCode = "";

    /// <summary>
    /// 业务代码
    /// </summary>
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

        DataTable dt = new DataTable();
      
        

        RmsPM.BLL.BiddingPrejudication cBiddingPrejudication = new RmsPM.BLL.BiddingPrejudication();
        cBiddingPrejudication.BiddingCode = this.BiddingCode;
        DataTable dtPrejudication = cBiddingPrejudication.GetBiddingPrejudications();
        for (int i = 0; i < dtPrejudication.Rows.Count; i++)
        {
            RmsPM.BLL.BiddingSupplier cBiddingSupplier = new RmsPM.BLL.BiddingSupplier();
            cBiddingSupplier.BiddingPrejudicationCode = dtPrejudication.Rows[i][0].ToString();

            DataTable dtSupplier = cBiddingSupplier.GetBiddingSuppliers();
            if (i == 0)
            {

                dt = dtSupplier.Clone();
                dt.Columns.Add("Remark", System.Type.GetType("System.String"));
            }
            for (int j = 0; j < dtSupplier.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                dr.ItemArray = dtSupplier.Rows[j].ItemArray;
                dr["Remark"] = dtPrejudication.Rows[i]["Remark"].ToString();
                dt.Rows.Add(dr);
            }
            dtSupplier.Dispose();

        }

        this.dgList.DataSource = dt;
        this.dgList.DataBind();
    }

}
