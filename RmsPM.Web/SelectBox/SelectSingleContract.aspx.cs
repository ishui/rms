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
    public partial class SelectSingleContract : PageBase
    {
        protected System.Web.UI.HtmlControls.HtmlInputText txtUnitName;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtUnitCode;
        protected System.Web.UI.HtmlControls.HtmlInputText txtUserName;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtUserCode;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面

            if (!this.IsPostBack)
            {
                IniPage();
                BuildSearchString();
                LoadDataGrid();
            }
        }

        private void IniPage()
        {
            string projectCode = Request["ProjectCode"] + "";
            string status = Request["Status"] + "";
            this.inputSystemGroup.ClassCode = "0501";
            BLL.PageFacade.SetListGroupSelectedValues(this.cblStatus, status);
            BLL.PageFacade.LoadUnitSelect(this.sltUnit, "", projectCode);

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
        private void LoadDataGrid()
        {

            try
            {
                string sql = (string)this.ViewState["SearchString"];
                QueryAgent QA = new QueryAgent();
                EntityData ds = QA.FillEntityData("Contract", sql);
                QA.Dispose();

                this.dgContractList.DataSource = new DataView(ds.CurrentTable);
                this.dgContractList.DataBind();
                this.gpControl.RowsCount = ds.CurrentTable.Rows.Count.ToString();

              
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "加载合同列表失败");
            }
        }

        private void BuildSearchString()
        {
            string projectCode = Request["ProjectCode"] + "";
            RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB = new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();
            string status = BLL.PageFacade.GetListGroupSelectedValues(this.cblStatus);
            if (status != "")
                CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.Status, status));

            if (projectCode != "") //材料库要选合同，不分项目 2006.11.2
                CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.ProjectCode, projectCode));


            if (this.txtContractName.Value.Trim() != "")
                CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.ContractName, "%" + this.txtContractName.Value.Trim() + "%"));

            if (this.txtContractID.Value.Trim() != "")
                CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.ContractID, "%" + this.txtContractID.Value.Trim() + "%"));

            ArrayList arA = new ArrayList();
            arA.Add("050101");
            arA.Add(user.UserCode);
            arA.Add(user.BuildStationCodes());
            CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.AccessRange, arA));

            if (this.sltUnit.Value != "")
                CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.UnitCode, this.sltUnit.Value));

            if (this.txtSupplierCode.Value != "")
                CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.SupplierCode, this.txtSupplierCode.Value));

            if (this.inputSystemGroup.Value != "")
                CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.Type, this.inputSystemGroup.Value));


            CSB.AddOrder("ContractDate", false);
            string sql = CSB.BuildMainQueryString();
            this.ViewState.Add("SearchString", sql);
        }


        protected void btnSearch_ServerClick(object sender, System.EventArgs e)
        {
            BuildSearchString();
            LoadDataGrid();
        }


        protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
        {
            //string contractCode = "";
            //string contractName = "";
            //if (((RadioButtonList)dgContractList.Items[0].Cells[5].Controls[1]).SelectedIndex >= 0)
            //{
            //    contractCode = dgContractList.DataKeys[((RadioButtonList)dgContractList.Items[0].Cells[5].Controls[1]).SelectedIndex].ToString();
            //    contractName = RmsPM.BLL.ContractRule.GetContractName(contractCode);
               
            //}
            
            //Response.Write(JavaScript.ScriptStart);
            //Response.Write("window.opener.InputContract_GetReturnValue('" + contractName + "','" + contractCode + "','" + Request["ID"] + "');");
            //Response.Write("window.close();");
            //Response.Write(JavaScript.ScriptEnd);
        }

        public void gpControl_PageIndexChange(object sender, System.EventArgs e)
        {
            try
            {
                BuildSearchString();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
            }
        }

    }
}

