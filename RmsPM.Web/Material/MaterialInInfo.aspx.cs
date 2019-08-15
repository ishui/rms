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
using System.Collections.Generic;
using Infragistics.WebUI.WebDataInput;
using RmsPM.DAL;
using TiannuoPM.MODEL;
using RmsPM.Web;
using RmsPM.BLL;

public partial class Material_MaterialInInfo : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["MaterialInCode"] + "" != "")
            {
                this.MaterialInCode.Text = Request["MaterialInCode"] + "";
            }

            if (this.MaterialInCode.Text == "")
            {
                FormView1.ChangeMode(FormViewMode.Insert);
            }

            else
            {
                string MaterialInCode = Request.QueryString["MaterialInCode"] + "";
                ArrayList ar = user.GetResourceRight(MaterialInCode, "MaterialIn");
                if (!ar.Contains("150301"))
                {
                    Response.Redirect("../RejectAccess.aspx");
                    Response.End();
                }
                if (!ar.Contains("150303"))
                {
                    ((Button)this.FormView1.Row.FindControl("btnModify")).Visible = false;
                }
                if (!ar.Contains("150304"))
                {
                    ((Button)this.FormView1.Row.FindControl("btnDelete")).Visible = false;
                }

            }
        }
    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            UpdateMaterialInDtl();
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
            Response.Write(Rms.Web.JavaScript.Alert(true, "删除材料出错：" + e.Exception.Message));
        }
        else
        {
            Response.Write("<script>window.opener.location = window.opener.location;window.close();</script>");
            //this.FormView1.DataBind();
            Response.End();
        }
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            UpdateMaterialInDtl();
        }
        catch (Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存明细出错：" + ex.Message));
        }

        Response.Write("<script>window.opener.location = window.opener.location;</script>");
        //for refresh

    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

        e.Values["InputPerson"] = base.user.UserCode;
        e.Values["MaterialInCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("MaterialInCode");
        e.Values["MaterialInID"] = e.Values["MaterialInCode"];
        //e.Values["MaterialCode"] = this.InputMaterialType.Value;
        e.Cancel = InvalidInput();
        //Response.Write("<script>alert(\"this.InputMaterialType.Value\");</script>");
        //e.Values["MaterialCode"] = this.InputMaterialType.Value;
        e.Values["ProjectCode"] = Request["ProjectCode"] + "";
        if (!user.HasTypeOperationRight("150302", ((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.Row.FindControl("InputSystemGroup")).Value) && !e.Cancel)
        {
            e.Cancel = true;
            Response.Write("<script>alert(\"您不能操作这类材料\");</script>");
            return;

        }
    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        //Response.Write("<script>alert(this.InputMaterialType.Value);</script>");
        e.NewValues["InputPerson"] = base.user.UserCode;

        e.Cancel = InvalidInput();
    }
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
        ObjectDataSource2.SelectParameters["MaterialInCode"].DefaultValue = e.ReturnValue.ToString();
        this.MaterialInCode.Text = e.ReturnValue.ToString();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        this.FormView1.DataBind();
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));
            if (btnDelete != null)
                btnDelete.Attributes["OnClick"] = "javascript:return confirm('确实要删除当前材料吗？')";
            ButtonStatusCtrl();

        }
        else
        {
            if(FormView1.CurrentMode == FormViewMode.Insert)
            ((RmsPM.Web.UserControls.InputUser)this.FormView1.Row.FindControl("InPersonBox")).Value = user.UserCode; 
            DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
            if (dgDtl != null)
            {
                RmsPM.BFL.MaterialInBFL bfl = new RmsPM.BFL.MaterialInBFL();
                List<MaterialInDtlModel> Models = bfl.GetMaterialInDtlList(RmsPM.BLL.ConvertRule.ToInt(this.MaterialInCode.Text));
                ViewState["Models"] = Models;
                BindDtl(Models);
            }
        }
    }
    private void ButtonStatusCtrl()
    {
 
        Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));


        //有领料时不能删除
        if (RmsPM.BLL.ConvertRule.ToDecimal(this.Source3Count.Text) != 0)
        {

            btnDelete.Visible = false;
        }
    }
    private bool InvalidInput()
    {
        bool ReturnCancel = false;
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        if (((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.Row.FindControl("InputSystemGroup")).Value == "")
        {
            ((HtmlGenericControl)this.FormView1.Row.FindControl("GroupSpan")).InnerHtml = "<font color='red'>必填</font>";
            ReturnCancel = true;
        }
        else
        {
            ((HtmlGenericControl)this.FormView1.Row.FindControl("GroupSpan")).InnerHtml = "";
        }
        
       
        foreach (DataGridItem item in dgDtl.Items)
        {
           
            if (RmsPM.BLL.ConvertRule.ToInt(((RmsPM.Web.UserControls.InputMaterial)item.FindControl("InputMaterial")).Value) == 0)
            {
                ReturnCancel = true;
                Response.Write("<script>alert(\"材料名称必需填写\");</script>");
                return ReturnCancel;
            }
            if( RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)item.FindControl("txtInQty")).ValueDecimal) == 0)
            {
                ReturnCancel = true;
                Response.Write("<script>alert(\"入库数量必需填写\");</script>");
                return ReturnCancel;
            }
        }

        return ReturnCancel;
    }

    /// <summary>
    /// 取屏幕明细的List
    /// </summary>
    /// <returns></returns>
    private List<MaterialInDtlModel> GetScreenDtl()
    {
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        List<MaterialInDtlModel> Models = (List<MaterialInDtlModel>)ViewState["Models"];

        int i = -1;
        foreach (DataGridItem item in dgDtl.Items)
        {
            i++;
            string DtlCode = dgDtl.DataKeys[i].ToString();

            MaterialInDtlModel mObj = Models[i];

            /*
            if (mObj == null)
            {
                mObj = new MaterialInDtlModel();
                Models.Add(mObj);
            }
            */

            mObj.InQty = RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)item.FindControl("txtInQty")).ValueDecimal);
            mObj.InPrice = RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)item.FindControl("txtInPrice")).ValueDecimal);
            mObj.MaterialCode = RmsPM.BLL.ConvertRule.ToInt(((RmsPM.Web.UserControls.InputMaterial)item.FindControl("InputMaterial")).Value);
            //mObj.Unit = RmsPM.BLL.ConvertRule.ToString(((RmsPM.Web.UserControls.InputMaterial)item.FindControl("InputMaterial")).UnitValue);
            //mObj.Unit = RmsPM.BLL.ConvertRule.ToString(((HtmlInputText)item.FindControl("UnitBox")).Value);
            mObj.InMoney = Math.Round(RmsPM.BLL.ConvertRule.ToDecimal((double)mObj.InQty * (double)mObj.InPrice),2);

        }

        return Models;
    }

    /// <summary>
    /// 绑定明细
    /// </summary>
    /// <param name="Models"></param>
    private void BindDtl(List<MaterialInDtlModel> Models)
    {
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        dgDtl.DataSource = Models;
        dgDtl.DataBind();
    }

    /// <summary>
    /// 保存明细
    /// </summary>
    private void UpdateMaterialInDtl()
    {
        List<MaterialInDtlModel> Models = GetScreenDtl();
        RmsPM.BFL.MaterialInBFL bfl = new RmsPM.BFL.MaterialInBFL();
        bfl.UpdateMaterialInDtlList(Models, RmsPM.BLL.ConvertRule.ToInt(this.MaterialInCode.Text));

    }
    /// <summary>
    /// 新增明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddDtl_ServerClick(object sender, EventArgs e)
    {
        List<MaterialInDtlModel> Models = GetScreenDtl();

        MaterialInDtlModel mObj = new MaterialInDtlModel();
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
        List<MaterialInDtlModel> Models = GetScreenDtl();
        Models.RemoveAt(e.Item.ItemIndex);

        BindDtl(Models);
    }

    protected void ObjectDataSource2_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        decimal InQty = 0;
        decimal OutQty = 0;
        decimal InvQty = 0;
        decimal InMoney = 0;
        System.Collections.Generic.List<TiannuoPM.MODEL.MaterialInDtlModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.MaterialInDtlModel>)e.ReturnValue;
        foreach (TiannuoPM.MODEL.MaterialInDtlModel Model in lst)
        {
            InQty += Model.InQty;
            OutQty += Model.OutQty;
            InvQty += Model.InvQty;
            InMoney += Model.InMoney;
        }

        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[4].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(InQty);
        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[6].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(InMoney);
        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[7].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(OutQty);
        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[8].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(InvQty);
    }
    protected void ObjectDataSource3_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        decimal OutQty = 0;
        decimal OutMoney = 0;
        System.Collections.Generic.List<TiannuoPM.MODEL.MaterialOutDtlModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.MaterialOutDtlModel>)e.ReturnValue;
        this.Source3Count.Text = lst.Count.ToString();
        foreach (TiannuoPM.MODEL.MaterialOutDtlModel Model in lst)
        {

            OutQty += Model.OutQty;
            OutMoney += Model.OutMoney;
        }
        ((GridView)this.FormView1.Row.FindControl("GridView2")).Columns[6].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(OutMoney);
        ((GridView)this.FormView1.Row.FindControl("GridView2")).Columns[4].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(OutQty);

    }

}

