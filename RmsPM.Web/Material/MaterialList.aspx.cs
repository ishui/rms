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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;

public partial class Material_MaterialList : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string sql = (string)this.ViewState["SqlString"];

            //                Response.Write(sql);

           // QueryAgent qa = new QueryAgent();
           // EntityData entity = qa.FillEntityData("Material", sql);
           // qa.Dispose();
           // dgList.DataSource = entity.CurrentTable;
           // dgList.DataBind();

            //int RecordCount = entity.CurrentTable.Rows.Count;
           // this.GridPagination1.RowsCount = RecordCount.ToString();
            //this.lblRecordCount.Text = RecordCount.ToString();

           //entity.Dispose();
           // this.lblRecordCount.Text = e.AffectedRows.ToString();
           // this.lblRecordCount.Text = GridView1.Rows.Count.ToString();
            ObjectDataSource1.SelectParameters["AccessRange"].DefaultValue = "150101" + "\n" + user.UserCode + "\n" + user.BuildStationCodes();


        }
    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (this.GridView1.PageIndex == 0)
        {
            System.Collections.Generic.List<TiannuoPM.MODEL.MaterialModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.MaterialModel>)e.ReturnValue;
            this.lblRecordCount.Text = lst.Count.ToString();
        }
    
    }
}
