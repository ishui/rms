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
using RmsPM.Web;
using System.Collections;
using System.Collections.Generic;

using Infragistics.WebUI.WebDataInput;
using RmsOA.BFL;
using RmsOA.MODEL;

public partial class RmsOA_GK_OA_SubmitAccountEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Code"] + "" != "")
            {
                this.ViewState["Code"] = Request["Code"] + "";
            }
            
            if (Request["Code"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            if (FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                if (user.HasRight("320502"))
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                }
                if (user.HasRight("320503"))
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = false;
                }
                //if (user.HasRight("320504"))
                //{
                //    this.FormView1.Row.FindControl("btnAddDtl").Visible = true;
                //}
                //else
                //{
                //    this.FormView1.Row.FindControl("btnAddDtl").Visible = false;
                //}
            }
        }

    }

 
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            UpdateDtl();
        }
        catch (Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存明细出错：" + ex.Message));
        }

        Response.Write("<script>window.opener.location = window.opener.location;</script>");

    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + e.Exception.Message));
        }
        else
        {
            DeleteDtl();
            Response.Write("<script>window.opener.location = window.opener.location;window.close();</script>");
            Response.End();
        }
    }
    private void DeleteDtl()
    {
        try
        {
            RmsOA.BFL.GK_OA_SubmitAccountDtlBFL bfl = new RmsOA.BFL.GK_OA_SubmitAccountDtlBFL();
            List<GK_OA_SubmitAccountDtlModel> Objs = bfl.GetGK_OA_SubmitAccountDtlList(RmsPM.BLL.ConvertRule.ToString(this.ViewState["Code"]));
            foreach (GK_OA_SubmitAccountDtlModel mObj in Objs)
            {
                bfl.Delete(mObj);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            UpdateDtl();
        }
        catch (Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存明细出错：" + ex.Message));
        }

        Response.Write("<script>window.opener.location = window.opener.location;</script>");

    }

    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));
            if (btnDelete != null)
                btnDelete.Attributes["OnClick"] = "javascript:return confirm('确实要删除当前记录吗？')";


        }
        else
        {

            DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
            if (dgDtl != null)
            {
                RmsOA.BFL.GK_OA_SubmitAccountDtlBFL bfl = new RmsOA.BFL.GK_OA_SubmitAccountDtlBFL();

                List<GK_OA_SubmitAccountDtlModel> Models = bfl.GetGK_OA_SubmitAccountDtlList(RmsPM.BLL.ConvertRule.ToString(this.ViewState["Code"]));
                ViewState["Models"] = Models;
                BindDtl(Models);
            }
        }
    }
    /// <summary>
    /// 绑定明细
    /// </summary>
    /// <param name="Models"></param>
    private void BindDtl(List<GK_OA_SubmitAccountDtlModel> Models)
    {
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        dgDtl.DataSource = Models;
        dgDtl.DataBind();
    }

    /// <summary>
    /// 保存明细
    /// </summary>
 
    private void UpdateDtl()
    {
        List<GK_OA_SubmitAccountDtlModel> mObjs = GetScreenDtl();
        try
        {
           
            RmsOA.BFL.GK_OA_SubmitAccountDtlBFL bfl = new RmsOA.BFL.GK_OA_SubmitAccountDtlBFL();
            Hashtable tbInDtl = new Hashtable();

            //删除
            List<GK_OA_SubmitAccountDtlModel> oldObjs = bfl.GetGK_OA_SubmitAccountDtlList(Convert.ToString(this.ViewState["Code"]));
            foreach (GK_OA_SubmitAccountDtlModel mObj in oldObjs)
            {
                if (!tbInDtl.Contains(mObj.Code))
                    tbInDtl.Add(mObj.Code, mObj.Code);

                if (FindModel(mObjs, mObj.Code.ToString()) == null)
                {
                    bfl.Delete(mObj);
                }
            }

            foreach (GK_OA_SubmitAccountDtlModel mObj in mObjs)
            {
                if (!tbInDtl.Contains(mObj.Code))
                    tbInDtl.Add(mObj.Code, mObj.Code);

                if (mObj.Code <= 0)  //新增
                {

                    mObj.MastCode = Convert.ToString(this.ViewState["Code"]);
                    bfl.Insert(mObj);
                }
                else  //修改
                {
                    bfl.Update(mObj);
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static GK_OA_SubmitAccountDtlModel FindModel(List<GK_OA_SubmitAccountDtlModel> mObjs, string DtlCode)
    {
        foreach (GK_OA_SubmitAccountDtlModel mObj in mObjs)
        {
            if (mObj.Code.ToString() == DtlCode)
                return mObj;
        }

        return null;
    }

    /// <summary>
    /// 新增明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddDtl_ServerClick(object sender, EventArgs e)
    {
        List<GK_OA_SubmitAccountDtlModel> Models = GetScreenDtl();

        GK_OA_SubmitAccountDtlModel mObj = new GK_OA_SubmitAccountDtlModel();
        Models.Add(mObj);

        BindDtl(Models);
    }

    /// <summary>
    /// 删除明细
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgDtl_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        List<GK_OA_SubmitAccountDtlModel> Models = GetScreenDtl();
        Models.RemoveAt(e.Item.ItemIndex);

        BindDtl(Models);
    }

    /// <summary>
    /// 取屏幕明细的List
    /// </summary>
    /// <returns></returns>
    private List<GK_OA_SubmitAccountDtlModel> GetScreenDtl()
    {
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        List<GK_OA_SubmitAccountDtlModel> Models = (List<GK_OA_SubmitAccountDtlModel>)ViewState["Models"];

        int i = -1;
        foreach (DataGridItem item in dgDtl.Items)
        {
            i++;
            GK_OA_SubmitAccountDtlModel mObj = Models[i];

            mObj.StandardCost = RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)item.FindControl("txtStandardCost")).ValueDecimal);
            mObj.RealityCost = RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)item.FindControl("txtRealityCost")).ValueDecimal);
            mObj.Remark = ((TextBox)item.FindControl("txtRemark")).Text;

            mObj.Month = (DateTime)((WebDateTimeEdit)item.FindControl("dtMonth")).Value;
            
            mObj.RemainCost = Math.Round(RmsPM.BLL.ConvertRule.ToDecimal((double)mObj.StandardCost - (double)mObj.RealityCost), 2);
            mObj.SumCost = Math.Round(RmsPM.BLL.ConvertRule.ToDecimal((double)mObj.StandardCost + (double)mObj.RemainCost), 2);
        }

        return Models;
    }

    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
        ObjectDataSource2.SelectParameters["MastCodeEqual"].DefaultValue = e.ReturnValue.ToString();
        this.ViewState["Code"] = e.ReturnValue.ToString();

    }

    protected void ObjectDataSource2_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        decimal RemainCost = 0;
        decimal SumCost = 0;
        System.Collections.Generic.List<GK_OA_SubmitAccountDtlModel> lst = (System.Collections.Generic.List<GK_OA_SubmitAccountDtlModel>)e.ReturnValue;
        foreach (GK_OA_SubmitAccountDtlModel Model in lst)
        {

            RemainCost += Model.RemainCost;
            SumCost += Model.SumCost;
        }
        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[3].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(RemainCost);
        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[4].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(SumCost);

    }

    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["SystemCode"] = "GKFC-ZY-630202";
    }
}
