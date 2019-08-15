namespace RmsPM.Web.UserControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Rms.ORMap;
    using System.Collections;

    /// <summary>
    /// InputSystemGroup 的摘要说明。
    /// </summary>
    public partial class InputAccessRange : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (this.Visible)
            {
                if (!Page.IsPostBack)
                {
                    IniPage();
                }
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
        ///		设计器支持所需的方法 - 不要使用代码编辑器
        ///		修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        public Boolean Readonly
        {
            set
            {
                this.chkReadonly.Checked = value;
                this.ucPerson.Readonly = value;
            }
            get
            {
                return chkReadonly.Checked;
            }
        }

        private void IniPage()
        {
            try
            {
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
            }
        }

        /// <summary>
        /// 设置大类代码，初始化
        /// </summary>
        /// <param name="ClassCode"></param>
        private void SetClass(string ClassCode)
        {
            if (this.txtClassCode.Value != ClassCode)
            {
                this.txtClassCode.Value = ClassCode;
                LoadData();
            }
        }

        public string ClassCode
        {
            get { return this.txtClassCode.Value; }
            set { SetClass(value); }
        }

        /// <summary>
        /// 设置操作代码（逗号分隔）
        /// </summary>
        /// <param name="OperationCodes"></param>
        private void SetOperationCodes(string OperationCodes)
        {
            if (this.txtOperationCodes.Value != OperationCodes)
            {
                this.txtOperationCodes.Value = OperationCodes;
                LoadData();
            }
        }

        public string OperationCodes
        {
            get { return this.txtOperationCodes.Value; }
            set { SetOperationCodes(value); }
        }

        /// <summary>
        /// 设置实体代码，初始化
        /// </summary>
        /// <param name="RelationCode"></param>
        private void SetRelationCode(string RelationCode)
        {
            if (this.txtRelationCode.Value != RelationCode)
            {
                this.txtRelationCode.Value = RelationCode;
                LoadData();
            }
        }

        public string RelationCode
        {
            get { return this.txtRelationCode.Value; }
            set { SetRelationCode(value); }
        }

        public string Hint
        {
            get { return this.txtHint.Value; }
        }

        /// <summary>
        /// 显示
        /// </summary>
        private void LoadData()
        {
            try
            {
                this.divSave.Style["display"] = "none";

                if (this.RelationCode == "") return;
                if (this.ClassCode == "") return;
                if (this.OperationCodes == "") return;

                string AllUserCodes = "";
                string AllStationCodes = "";

                string[] arrOperationCode = this.OperationCodes.Split(","[0]);
                foreach (string OperationCode in arrOperationCode)
                {
                    string strUsers = "";
                    string strUserNames = "";
                    string strStations = "";
                    string strStationNames = "";
                    BLL.ResourceRule.GetAccessRange(this.RelationCode, this.ClassCode, OperationCode, ref strUsers, ref strUserNames, ref strStations, ref strStationNames);

                    AllUserCodes += (AllUserCodes == "") ? "" : ",";
                    AllUserCodes += strUsers;

                    AllStationCodes += (AllStationCodes == "") ? "" : ",";
                    AllStationCodes += strStations;
                }

                this.ucPerson.UserCodes = AllUserCodes;
                this.ucPerson.StationCodes = AllStationCodes;
            }
            catch (Exception ex)
            {
                LogHelper.Error("加载数据失败", ex);
                Response.Write(Rms.Web.JavaScript.Alert(true, "加载数据失败：" + ex.Message));
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (this.RelationCode == "") throw new Exception("相关编号不能为空");
                if (this.ClassCode == "") throw new Exception("大类编号不能为空");
                if (this.OperationCodes == "") throw new Exception("操作编号不能为空");

                string[] arrUserCode = this.ucPerson.UserCodes.Split(","[0]);
                string[] arrStationCode = this.ucPerson.StationCodes.Split(","[0]);

                ArrayList alOperation = new ArrayList();
                string[] arrOperationCode = this.OperationCodes.Split(","[0]);
                foreach (string OperationCode in arrOperationCode)
                {
                    foreach (string UserCode in arrUserCode)
                    {
                        if (UserCode.Trim() == "") continue;

                        BLL.AccessRange acRang = new BLL.AccessRange();
                        acRang.AccessRangeType = 0;
                        acRang.RelationCode = UserCode;
                        acRang.Operations = OperationCode;
                        alOperation.Add(acRang);
                    }

                    foreach (string StationCode in arrStationCode)
                    {
                        if (StationCode.Trim() == "") continue;

                        BLL.AccessRange acRang = new BLL.AccessRange();
                        acRang.AccessRangeType = 1;
                        acRang.RelationCode = StationCode;
                        acRang.Operations = OperationCode;
                        alOperation.Add(acRang);
                    }
                }

                BLL.ResourceRule.SetResourceAccessRange(this.RelationCode, this.ClassCode, "", alOperation, true);

                this.divSave.Style["display"] = "none";
            }
            catch(Exception ex)
            {
                LogHelper.Error("保存失败", ex);
                Response.Write(Rms.Web.JavaScript.Alert(true, "保存失败：" + ex.Message));
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                LogHelper.Error("取消失败", ex);
                Response.Write(Rms.Web.JavaScript.Alert(true, "取消失败：" + ex.Message));
            }
        }
}
}
