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
using Rms.ORMap;

public partial class Material_MaterialOutInfo : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["MaterialOutCode"] + "" != "")
            {
                this.ViewState["MaterialOutCode"] = Request["MaterialOutCode"] + "";
            }
           // Response.Write(Rms.Web.JavaScript.Alert(true, "删除材料出错：" + this.ViewState["MaterialOutCode"]));
            if (this.ViewState["MaterialOutCode"] == null)
            {
                FormView1.ChangeMode(FormViewMode.Insert);
            }

            else
            {
                string MaterialOutCode = Request.QueryString["MaterialOutCode"] + "";
                ArrayList ar = user.GetResourceRight(MaterialOutCode, "MaterialOut");
                //Response.Write(Rms.Web.JavaScript.Alert(true, MaterialOutCode));
 /*
                if (!ar.Contains("150501"))
                {
                    Response.Redirect("../RejectAccess.aspx");
                    Response.End();
                }
               
                 if (!ar.Contains("150102"))
                 {
                     ((Button)this.FormView1.Row.FindControl("btnModify")).Visible = false;
                 }
                 if (!ar.Contains("150105"))
                 {
                     ((Button)this.FormView1.Row.FindControl("btnModify")).Visible = false;
                 }
                if (!ar.Contains("150503"))
                {
                    ((Button)this.FormView1.Row.FindControl("btnModify")).Visible = false;
                }
                if (!ar.Contains("150504"))
                {
                    ((Button)this.FormView1.Row.FindControl("btnDelete")).Visible = false;
                }
*/
            }

        }
    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            UpdateMaterialOutDtl();
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
            UpdateMaterialOutDtl();
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
        e.Values["MaterialOutCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("MaterialOutCode");
        e.Values["MaterialOutID"] = e.Values["MaterialOutCode"];
        //e.Values["MaterialCode"] = this.InputMaterialType.Value;
        e.Cancel = InvalidInput();
        //Response.Write("<script>alert(\"this.InputMaterialType.Value\");</script>");
        //e.Values["MaterialCode"] = this.InputMaterialType.Value;
        e.Values["ProjectCode"] = Request["ProjectCode"] + "";
        if (!user.HasTypeOperationRight("150502", ((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.Row.FindControl("InputSystemGroup")).Value) && !e.Cancel)
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
        ObjectDataSource2.SelectParameters["MaterialOutCode"].DefaultValue = e.ReturnValue.ToString();
        this.ViewState["MaterialOutCode"] = e.ReturnValue.ToString();
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
           

        }
        else
        {
          if (FormView1.CurrentMode == FormViewMode.Insert)
         ((RmsPM.Web.UserControls.InputUser)this.FormView1.Row.FindControl("OutPersonBox")).Value = user.UserCode;

            DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
            if (dgDtl != null)
            {
                RmsPM.BFL.MaterialOutBFL bfl = new RmsPM.BFL.MaterialOutBFL();
                //Response.Write("<script>alert(this.ViewState['MaterialOutCode']);</script>");
                List<MaterialOutDtlModel> Models = bfl.GetMaterialOutDtlList(RmsPM.BLL.ConvertRule.ToInt(this.ViewState["MaterialOutCode"]));
                ViewState["Models"] = Models;
                BindDtl(Models);
            }
        }
    }

    private bool InvalidInput()
    {
        bool ReturnCancel = false;
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        EntityData entity=null;
       
        RmsPM.Web.UserControls.InputContract contract = (RmsPM.Web.UserControls.InputContract)this.FormView1.Row.FindControl("ContractCode");
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

            if (RmsPM.BLL.ConvertRule.ToInt(((RmsPM.Web.UserControls.InputMaterialin)item.FindControl("InputMaterialin")).MaterialInDtlCode) == 0)
            {
                ReturnCancel = true;
                Response.Write("<script>alert(\"领用材料名称必需填写\");</script>");
                return ReturnCancel;
            }
            if (RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)item.FindControl("txtOutQty")).ValueDecimal) == 0)
            {
                ReturnCancel = true;
                Response.Write("<script>alert(\"领用数量必需填写\");</script>");
                return ReturnCancel;
            }
            //判断领用数量不能超过合同需求数量
            if (contract != null && !string.IsNullOrEmpty(contract.Value))
            {
               
                RmsPM.Web.UserControls.InputMaterialin materialin=(RmsPM.Web.UserControls.InputMaterialin)item.FindControl("InputMaterialin");
                decimal outqty = 0;
                
                entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contract.Value);
                entity.SetCurrentTable("ContractMaterial");
                foreach (DataRow dr in entity.CurrentTable.Select(String.Format("MaterialCode={0}", RmsPM.BLL.ConvertRule.ToInt(materialin.MaterialCode), "", DataViewRowState.CurrentRows)))
                {
                    outqty = RmsPM.BFL.MaterialOutBFL.GetMaterialOutQtyByContract(contract.Value, RmsPM.BLL.ConvertRule.ToInt(materialin.MaterialCode));
                    if (outqty+((WebNumericEdit)item.FindControl("txtOutQty")).ValueDecimal > RmsPM.BLL.ConvertRule.ToDecimal(dr["Qty"]))
                    {
                        ReturnCancel = true;
                        Response.Write("<script>alert(\"领用数量超过合同需求数量\");</script>");
                        return ReturnCancel;
                    }
                }

            }
        }

        return ReturnCancel;
    }

    /// <summary>
    /// 取屏幕明细的List
    /// </summary>
    /// <returns></returns>
    private List<MaterialOutDtlModel> GetScreenDtl()
    {
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        List<MaterialOutDtlModel> Models = (List<MaterialOutDtlModel>)ViewState["Models"];

        int i = -1;
        foreach (DataGridItem item in dgDtl.Items)
        {
            i++;
            string DtlCode = dgDtl.DataKeys[i].ToString();

            MaterialOutDtlModel mObj = Models[i];
            mObj.OutQty = RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)item.FindControl("txtOutQty")).ValueDecimal);
            mObj.OutPrice = RmsPM.BLL.ConvertRule.ToDecimal(((RmsPM.Web.UserControls.InputMaterialin)item.FindControl("InputMaterialin")).OutPrice);
            mObj.MaterialCode = RmsPM.BLL.ConvertRule.ToInt(((RmsPM.Web.UserControls.InputMaterialin)item.FindControl("InputMaterialin")).MaterialCode);
            mObj.MaterialInDtlCode = RmsPM.BLL.ConvertRule.ToInt(((RmsPM.Web.UserControls.InputMaterialin)item.FindControl("InputMaterialin")).MaterialInDtlCode);
            mObj.OutMoney = Math.Round(RmsPM.BLL.ConvertRule.ToDecimal((double)mObj.OutQty * (double)mObj.OutPrice), 2);

        }

        return Models;
    }

    /// <summary>
    /// 绑定明细
    /// </summary>
    /// <param name="Models"></param>
    private void BindDtl(List<MaterialOutDtlModel> Models)
    {
        DataGrid dgDtl = (DataGrid)this.FormView1.Row.FindControl("dgDtl");
        dgDtl.DataSource = Models;
        dgDtl.DataBind();
    }

    /// <summary>
    /// 保存明细
    /// </summary>
    private void UpdateMaterialOutDtl()
    {
        List<MaterialOutDtlModel> Models = GetScreenDtl();
        RmsPM.BFL.MaterialOutBFL bfl = new RmsPM.BFL.MaterialOutBFL();
        bfl.UpdateMaterialOutDtlList(Models, RmsPM.BLL.ConvertRule.ToInt(this.ViewState["MaterialOutCode"]));

    }
    /// <summary>
    /// 新增明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddDtl_ServerClick(object sender, EventArgs e)
    {
        List<MaterialOutDtlModel> Models = GetScreenDtl();

        MaterialOutDtlModel mObj = new MaterialOutDtlModel();
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
        List<MaterialOutDtlModel> Models = GetScreenDtl();
        Models.RemoveAt(e.Item.ItemIndex);

        BindDtl(Models);
    }

    protected void ObjectDataSource2_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        decimal OutQty = 0;
        decimal OutMoney = 0;
        System.Collections.Generic.List<TiannuoPM.MODEL.MaterialOutDtlModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.MaterialOutDtlModel>)e.ReturnValue;
        foreach (TiannuoPM.MODEL.MaterialOutDtlModel Model in lst)
        {
           
            OutQty += Model.OutQty;
            OutMoney += Model.OutMoney;
        }
        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[6].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(OutMoney);
        ((GridView)this.FormView1.Row.FindControl("GridView1")).Columns[4].FooterText = RmsPM.BLL.MathRule.GetDecimalShowString(OutQty);

    }

}

