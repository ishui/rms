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
using Infragistics.WebUI.WebDataInput;
namespace RmsPM.Web.Contract
{
    public partial class Contract_ContractMaterialMonth : PageBase
    {
        //string materialcode = Request.QueryString["MaterialCode"] + "";

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //public EntityData entityall;
                IniPage();

            }
        }
       
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            base.OnInit(e);
        }
        private void IniPage()
        {
            try
            {
                string contractCode = Request.QueryString["ContractCode"] + "";
                this.txtContractCode.Value = contractCode;
               
                string projectCode = Request["ProjectCode"] + "";
                string materialcode = Request.QueryString["MaterialCode"] + "";
                ViewState["Materialcode"] = materialcode;
                this.txtMaterialCode.Value = materialcode;
                EntityData entity = null;

                entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
                // ������Ϣ
                entity.SetCurrentTable("Contract");
                this.lblRemark.Text = entity.GetString("Remark");
                this.lblContractID.Text = entity.GetString("ContractID");
                this.lblContractName.Text = entity.GetString("ContractName");
                this.lblContractPersonName.Text = RmsPM.BLL.SystemRule.GetUserName(entity.GetString("ContractPerson"));
                this.lblSystemGroup.Text = BLL.SystemGroupRule.GetSystemGroupName(entity.GetString("Type"));
                this.lblUnit.Text = BLL.SystemRule.GetUnitName(entity.GetString("UnitCode"));
                this.lblThirdParty.Text = entity.GetString("ThirdParty");
                this.PanelEdit.Visible = false;
                this.PanelItem.Visible = true;
                LoadData();
                entity.Dispose();


            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ͬ���ݴ���");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ͬ���ݳ���" + ex.Message));
            }
        }

        private void LoadData()
        {
            try
            {
                string contractCode = Request.QueryString["ContractCode"] + "";
                this.txtContractCode.Value = contractCode;
                string projectCode = Request["ProjectCode"] + "";
                string materialcode = Request.QueryString["MaterialCode"] + "";
                EntityData entity = null;
                entity = RmsPM.DAL.EntityDAO.ContractDAO.GetContractMaterialMonthByContractCode(contractCode);
                //�¶ȼƻ�
                //entity.SetCurrentTable("ContractMaterialMonth");
                //ʵ��
                //BindEditList(entity.CurrentTable, materialcode);
                BindItemList(entity.CurrentTable, materialcode);
                Session["ContractMaterialMonthEntity"] = entity;
                entity.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "���غ�ͬ���ݴ���");
                Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ���ݳ���" + ex.Message));
            }

        }

        private void AddNewValueEmptyRow(EntityData entity, string contractCode, string tableName, string keyColumnName, int rows, int Is, string materialcode)
        {
            for (int i = 0; i < rows; i++)
            {
                DataRow dr = entity.GetNewRecord(tableName);
                dr[keyColumnName] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyColumnName);
                dr["ContractCode"] = contractCode;
                dr["MaterialCode"] = materialcode;
                entity.AddNewRecord(dr, tableName);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveClick(object sender, System.EventArgs e)
        {
            string contractCode = this.txtContractCode.Value;
            string projectCode = Request["ProjectCode"] + "";
            string materialcode = Request.QueryString["MaterialCode"] + "";
            try
            {
                string msg = SaveToSession();
                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                    return;
                }

                string ClearMsg = ClearData();
                if (ClearMsg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, ClearMsg));
                    return;
                }

                EntityData entity = (EntityData)Session["ContractMaterialMonthEntity"];
                //entity.SetCurrentTable("ContractMaterialMonth");
                DAL.EntityDAO.ContractDAO.SubmitAllContractMaterialMonth(entity);
                entity.Dispose();

                this.PanelItem.Visible = true;
                this.PanelEdit.Visible = false;
                //Response.Write("<script>window.location=window.location;</script>");

                //Session["ContractMaterialMonthEntity"] = null;
                //Session["ContractAct"] = null;
                //BindEditList(entity.CurrentTable, materialcode);

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "�������1��" + ex.Message));
                return;
            }
            GoBack();

        }

        /// <summary>
        /// ����ռ�¼
        /// </summary>
        private string ClearData()
        {
            try
            {
                string ErrMsg = "";
                string contractCode = this.txtContractCode.Value;
                EntityData entity = (EntityData)Session["ContractMaterialMonthEntity"];

                // ���Ϊ�յļ�¼
                foreach (DataRow dr in entity.Tables["ContractMaterialMonth"].Select("", "", DataViewRowState.CurrentRows))
                {
                    if (BLL.ConvertRule.ToString(dr["Qty"]) == "0"||BLL.ConvertRule.ToString(dr["Qty"]) == "" && BLL.ConvertRule.ToString(dr["ContractMaterialMonth"]) == "")
                    {
                        dr.Delete();
                        continue;
                    }
                    if (BLL.ConvertRule.ToString(dr["Qty"]) == "0" || BLL.ConvertRule.ToString(dr["ContractMaterialMonth"]) == "")
                    {
                        ErrMsg = "�뽫�¶ȼƻ���д������";
                        break;
                    }
                }

                Session["ContractMaterialMonthEntity"] = entity;
                entity.Dispose();
                return ErrMsg;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "����ռ�¼����" + ex.Message));
                return "����ռ�¼����" + ex.Message;
            }
        }

        private string SaveToSession()
        {

            string alertMsg = "";
            try
            {
                string contractCode = this.txtContractCode.Value;
                string projectCode = Request["ProjectCode"] + "";


                EntityData entity = (EntityData)Session["ContractMaterialMonthEntity"];
                foreach (DataGridItem li in this.dgDtl.Items)
                {
                    string ContractMaterialMonthCode = li.Cells[0].Text;

                    string ContractMaterialMonth = ((AspWebControl.Calendar)li.FindControl("dtContractMaterialMonth")).Value;
                    WebNumericEdit txtQty = (WebNumericEdit)li.FindControl("txtQty");


                    foreach (DataRow dr in entity.Tables["ContractMaterialMonth"].Select(String.Format("ContractMaterialMonthCode='{0}'", ContractMaterialMonthCode)))
                    {
                        dr["Qty"] = txtQty.ValueDecimal;

                        if (ContractMaterialMonth != "")
                            dr["ContractMaterialMonth"] = ContractMaterialMonth;
                        else
                            dr["ContractMaterialMonth"] = System.DBNull.Value;
                    }
                }

                Session["ContractMaterialMonthEntity"] = entity;
                entity.Dispose();

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
            }
            return alertMsg;

        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewMaterialMonthItem_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                string materialcode = Request.QueryString["MaterialCode"] + "";
                string contractCode = this.txtContractCode.Value;
                string msg = SaveToSession();
                EntityData entity = (EntityData)Session["ContractMaterialMonthEntity"];

                AddNewValueEmptyRow(entity, contractCode, "ContractMaterialMonth", "ContractMaterialMonthCode", 5, 1, materialcode);

                BindEditList(entity.Tables["ContractMaterialMonth"], materialcode);

                Session["ContractMaterialMonthEntity"] = entity;
                entity.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "�����¶ȼƻ�����" + ex.Message));
            }
        }

        /// <summary>
        /// ���޸���ϸ 
        /// </summary>
        private void BindEditList(DataTable tb,string materialcode)
        {
            try
            {
                DataView dv = new DataView(tb, "MaterialCode=" + materialcode, "", DataViewRowState.CurrentRows);
                //Session["SumValue"] = BLL.MathRule.SumColumn(tb, "Qty");
                
                ViewState["SumEdtQty"] = BLL.MathRule.SumColumn(dv.Table, "Qty");
                DataGrid dg = this.dgDtl;
                if (dg != null)
                {
                    dg.DataSource = dv;
                    dg.DataBind();
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�¶ȼƻ�����" + ex.Message));
            }
        }

        /// <summary>
        /// ��ֻ����ϸ 
        /// </summary>
        private void BindItemList(DataTable tb, string materialcode)
        {
            try
            {
                DataView dv = new DataView(tb, "MaterialCode=" + materialcode, "", DataViewRowState.CurrentRows);
                //Session["SumValue"] = BLL.MathRule.SumColumn(tb, "Qty");
                GridView gv = this.GridView1;
                ViewState["SumItmQty"] = BLL.MathRule.SumColumn(dv.ToTable(), "Qty");

                if (gv != null)
                {
                    gv.DataSource = dv;
                    gv.DataBind();
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�¶ȼƻ�����" + ex.Message));
            }
        }

        /// <summary>
        /// ɾ���¶ȼƻ�
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void dgDtl_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                string materialcode = Request.QueryString["MaterialCode"] + "";
                string msg = SaveToSession();
                ClearData();
                string contractCode = this.txtContractCode.Value;
                string projectCode = Request["ProjectCode"] + "";
                string codeTemp = e.Item.Cells[0].Text;
                EntityData entity = (EntityData)Session["ContractMaterialMonthEntity"];
                foreach (DataRow dr in entity.Tables["ContractMaterialMonth"].Select(String.Format("ContractMaterialMonthCode='{0}'", codeTemp)))
                {
                    dr.Delete();
                }

                BindEditList(entity.Tables["ContractMaterialMonth"], materialcode);
               // ViewState["EntityContractMaterial"] = entity;
                Session["ContractMaterialMonthEntity"] =entity;
                entity.Dispose();

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
            }
        }

        protected void dgDtl_DataBinding(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //��ʾ�ϼ��¶ȼƻ�
                ((Label)e.Item.FindControl("lblSumValue")).Text = ViewState["SumEdtQty"].ToString();
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void GoBack()
        {
            Session["ContractMaterialMonthEntity"] = null;
            Response.Write(Rms.Web.JavaScript.ScriptStart);

            Response.Write("window.opener.location = window.opener.location;");
            //			Response.Write("window.parent.location.href='../Contract/ContractInfo.aspx?ContractCode="+contractCode+"&projectCode=" + projectCode + "';");
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }


        protected void btnEdit_ServerClick(object sender, EventArgs e)
        {
            string contractCode = Request.QueryString["ContractCode"] + "";
            EntityData entity = null;
            entity = RmsPM.DAL.EntityDAO.ContractDAO.GetContractMaterialMonthByContractCode(contractCode);
            BindEditList(entity.CurrentTable, ViewState["Materialcode"].ToString());
            this.PanelEdit.Visible = true;
            this.PanelItem.Visible = false;
        }
        protected void btnCancelClick(object sender, EventArgs e)
        {
            this.PanelEdit.Visible = false;
            this.PanelItem.Visible = true;

            string contractCode = Request.QueryString["ContractCode"] + "";
            EntityData entity = null;
            entity = RmsPM.DAL.EntityDAO.ContractDAO.GetContractMaterialMonthByContractCode(contractCode);
            Session["ContractMaterialMonthEntity"] = entity;
            BindItemList(entity.CurrentTable, ViewState["Materialcode"].ToString());
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("SumQty")).Text = ViewState["SumItmQty"].ToString();
            }
        }
}
}