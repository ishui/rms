using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;


namespace RmsPM.Web.SelectBox
{
    /// <summary>
    /// SelectContract 的摘要说明。

    /// </summary>
    public partial class SelectViseBox : PageBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面

            if (!this.IsPostBack)
            {
                IniPage();
                LoadDataGrid(SearchVise());
            }
        }

        private void IniPage()
        {
            this.inputSystemGroup.ClassCode = "2201";
        }
        override protected void InitEventHandler()
        {
            base.InitEventHandler();
            this.dgViseList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);
        }

        private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    CheckBox cb = (CheckBox)e.Item.FindControl("selectViseCheckBox");
                    if (((DataRowView)e.Item.DataItem)["ViseUpdateContract"].ToString() == "1")
                        cb.Checked = true;
                    break;
                case ListItemType.Footer:
                    break;
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
        private void LoadDataGrid(EntityData entity)
        {
            if (entity != null)
            {
                DataView dv = new DataView(entity.CurrentTable);
                dv.Sort = "ViseCreateTime Desc";
                dgViseList.DataSource = dv;
                dgViseList.DataBind();
                gpControl.RowsCount = entity.CurrentTable.Rows.Count.ToString();
            }
        }

        private EntityData  SearchVise()
        {
            try
            {

                Hashtable ht = new Hashtable();
                ht.Add("ProjectCode", Request["ProjectCode"].ToString());
                ht.Add("ViseContractCode", Request["ContractCode"].ToString());
                
                if (txtViseName.Value.Trim() != "")
                    ht.Add("ViseName", "%" + txtViseName.Value + "%");
                if (txtViseID.Value.Trim() != "")
                    ht.Add("ViseID", "%" + this.txtViseID.Value + "%");
                if (this.InputStationUser.UserCodes != "")
                    ht.Add("VisePersonCode", InputStationUser.UserCodes);
                if (this.InputSupplier.Value != "")
                    ht.Add("ViseSupplierCode", InputSupplier.Value);
                if (this.InputUnit.Value != "")
                    ht.Add("ViseDepartmentCode", InputUnit.Value);

                ht.Add("EffectiveStatus", "1");

                EntityData entity = null;// RmsPM.DAL.EntityDAO.ViseDAO.GetViseMessages(ht);2008-9-28孙权修改

                if (entity != null)
                    return entity;
                else
                    return null;

                
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "加载签证列表失败");
                return null;
            }
        }

        protected void btnSearch_ServerClick(object sender, System.EventArgs e)
        {
            LoadDataGrid(SearchVise());
        }

        
        private void WriteViseEntitySession(EntityData entity)
        {
            string action = Request.QueryString["Act"] + "";
            Session["ViseEntity" + action] = entity;
        }
        
        private EntityData ReadViseEntitySession()
        {
            string action = Request.QueryString["Act"] + "";
            return (EntityData)Session["ViseEntity" + action];
        }

        private void WriteContractEntitySession(EntityData entity)
        {
            string action = Request.QueryString["Act"] + "";
            Session["ContractEntity" + action] = entity;
        }

        private EntityData ReadContractEntitySession()
        {
            string action = Request.QueryString["Act"] + "";
            return (EntityData)Session["ContractEntity" + action];
        }

        protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
        {
           
            EntityData entityVise = ReadViseEntitySession();
            EntityData entityContract = ReadContractEntitySession();
            
            foreach (DataGridItem dgItem in this.dgViseList.Items)
            {
                CheckBox viseCheckBox = (CheckBox)dgItem.FindControl("selectViseCheckBox");
                HtmlInputHidden viseCode=(HtmlInputHidden)dgItem.FindControl("ViseCode");
                
                if (viseCheckBox.Checked)
                {
                    bool isExistVise = false;
                    DataRow drOldVise = null;
                    foreach (DataRow drVise in entityVise.CurrentTable.Rows)
                    {
                        if (drVise["ViseCode"].ToString() == viseCode.Value)
                        {
                            isExistVise = true; drOldVise = drVise; break;
                        }

                    }
                    if (!isExistVise)
                    {
                        EntityData entity = null;// RmsPM.DAL.EntityDAO.ViseDAO.GetViseByCode(viseCode.Value); 孙权2007-9-28修改
                        DataRow drNew = entityVise.GetNewRecord();
                        for (int i = 0; i < drNew.ItemArray.Length; i++)
                            drNew[i] = entity.CurrentRow[i];
                        drNew["ViseUpdateContract"] = 1;
                        entityVise.AddNewRecord(drNew);

                        Hashtable ht = new Hashtable();
                        ht.Add("ViseCode", viseCode.Value);
                        DataTable entityViseCash = null;// RmsPM.DAL.EntityDAO.ViseDAO.GetViseCashByViseCode(ht); 孙权2007-9-28修改
                        foreach (DataRow drViseCash in entityViseCash.Rows)
                        {
                            if (drViseCash["ContractCostChangeCode"] != System.DBNull.Value)
                            {
                                DataRow[] drContractCostChanges = entityContract.Tables["ContractCostChange"].Select("ContractCostChangeCode='" + drOldVise["ContractCostChangeCode"].ToString() + "'");
                                foreach (DataRow drContractCostChange in drContractCostChanges)
                                {
                                    decimal CostMoney = drContractCostChange["Money"] != DBNull.Value ? (decimal)drContractCostChange["Money"] : Decimal.Zero;
                                    drContractCostChange["ChangeMoney"] = drViseCash["ViseRMB"];
                                    drContractCostChange["NewMoney"] = CostMoney;

                                    decimal CostCash = drContractCostChange["Cash"] != DBNull.Value ? (decimal)drContractCostChange["Cash"] : Decimal.Zero;
                                    drContractCostChange["ChangeCash"] = drViseCash["ViseCash"];
                                    drContractCostChange["NewCash"] = CostCash;
                                }
                            }
                            else
                            { 
                            DataRow drContractCostChange=GetNewContractCostChangeRow(entityContract,drViseCash,Request["ContractCode"],Request["ContractChangeCode"]);
                            UpdateContractCostChange(drContractCostChange, drViseCash);
                            drViseCash["ContractCostChangeCode"]=drContractCostChange["ContractCostChangeCode"];
                            }
                        }
                    }
                }
            }

            WriteViseEntitySession(entityVise);
            WriteContractEntitySession(entityContract);

            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
      
        public void gpControl_PageIndexChange(object sender, System.EventArgs e)
        {
            try
            {
                LoadDataGrid(SearchVise());
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
            }
        }

        private void UpdateContractCostChange(DataRow drContractCostChange,DataRow drViseCostCash)
        {
          
                decimal ud_deCostCash, ud_deCostOriginalCash, ud_deCostTotalChangeCash, ud_deCostChangeCash, ud_deCostNewCash, ud_deExchangeRate;
                decimal ud_deCostMoney, ud_deCostOriginalMoney, ud_deCostTotalChangeMoney, ud_deCostChangeMoney, ud_deCostNewMoney;

                ud_deCostCash = (decimal)drContractCostChange["Cash"];
                ud_deCostOriginalCash = (decimal)drContractCostChange["OriginalCash"];
                ud_deCostTotalChangeCash = (decimal)drContractCostChange["TotalChangeCash"];
            
                ud_deCostMoney = (decimal)drContractCostChange["Money"];
                ud_deCostOriginalMoney = (decimal)drContractCostChange["OriginalMoney"];
                ud_deCostTotalChangeMoney = (decimal)drContractCostChange["TotalChangeMoney"];

                ud_deCostChangeCash = (decimal)drViseCostCash["ViseCash"];
                ud_deExchangeRate = (decimal)drViseCostCash["ViseExchangeRate"];

                ud_deCostNewCash = ud_deCostOriginalCash + ud_deCostTotalChangeCash + ud_deCostChangeCash;

                ud_deCostNewMoney = ud_deCostNewCash * ud_deExchangeRate;
                ud_deCostChangeMoney = ud_deCostChangeCash * ud_deExchangeRate;

                drContractCostChange["ChangeCash"] = ud_deCostChangeCash;
                drContractCostChange["NewCash"] = ud_deCostNewCash;

                drContractCostChange["ChangeMoney"] = ud_deCostChangeMoney;
                drContractCostChange["NewMoney"] = ud_deCostNewMoney;
                drContractCostChange["Description"] = "导入签证";

                    drContractCostChange["CostCode"] = drViseCostCash["CostCode"];
                    drContractCostChange["CostBudgetSetCode"] = drViseCostCash["ViseCostBudgetSetCode"];
                    drContractCostChange["PBSType"] = drViseCostCash["VisePBSType"];
                    drContractCostChange["PBSCode"] = drViseCostCash["VisePBSCode"];

                    drContractCostChange["MoneyType"] = drViseCostCash["ViseCashType"];
                    drContractCostChange["ExchangeRate"] = drViseCostCash["ViseExchangeRate"];
       
        }
        private DataRow GetNewContractCostChangeRow(EntityData entity, DataRow drViseCostCash, string contractCode, string contractChangeCode)
        {
            string contractCostChangeCode = "";

                DataRow drContractCostChange = entity.GetNewRecord("ContractCostChange");
                contractCostChangeCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostChangeCode");

                drContractCostChange["ContractCostChangeCode"] = contractCostChangeCode;
                drContractCostChange["ContractCode"] = contractCode;
                drContractCostChange["ContractChangeCode"] = contractChangeCode;
                drContractCostChange["TotalChangeMoney"] = Decimal.Zero;
                drContractCostChange["NewMoney"] = Decimal.Zero;
                drContractCostChange["ChangeMoney"] = Decimal.Zero;

                drContractCostChange["TotalChangeCash"] = Decimal.Zero;
                drContractCostChange["NewCash"] = Decimal.Zero;
                drContractCostChange["ChangeCash"] = Decimal.Zero;

                drContractCostChange["Status"] = 1;

                bool isCostExist=false;
                DataRow drOldContractCostCash = null;
                foreach (DataRow drContractCostCash in entity.Tables["ContractCostCash"].Rows)
                    if (drContractCostCash["CostCode"].ToString() == drViseCostCash["CostCode"].ToString()&&
                        drContractCostCash["MoneyType"].ToString() == drViseCostCash["ViseCashType"].ToString())
                    {
                        isCostExist = true; drOldContractCostCash = drContractCostCash; break;
                    }
                if (isCostExist)
                {
                    drContractCostChange["Money"] = drOldContractCostCash["Money"];
                    drContractCostChange["OriginalMoney"] = drOldContractCostCash["OriginalMoney"];
                    drContractCostChange["Cash"] = drOldContractCostCash["Cash"];
                    drContractCostChange["OriginalCash"] = drOldContractCostCash["OriginalCash"];
                }
                else
                {
                    drContractCostChange["Money"] = Decimal.Zero;
                    drContractCostChange["OriginalMoney"] = Decimal.Zero;
                    drContractCostChange["Cash"] = Decimal.Zero;
                    drContractCostChange["OriginalCash"] = Decimal.Zero;
                }
                return drContractCostChange;
        }

    }
}

