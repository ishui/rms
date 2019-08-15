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
    /// InputSystemGroup ��ժҪ˵����
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

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
        ///		�޸Ĵ˷��������ݡ�
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
                ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ҳ��ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
            }
        }

        /// <summary>
        /// ���ô�����룬��ʼ��
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
        /// ���ò������루���ŷָ���
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
        /// ����ʵ����룬��ʼ��
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
        /// ��ʾ
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
                LogHelper.Error("��������ʧ��", ex);
                Response.Write(Rms.Web.JavaScript.Alert(true, "��������ʧ�ܣ�" + ex.Message));
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (this.RelationCode == "") throw new Exception("��ر�Ų���Ϊ��");
                if (this.ClassCode == "") throw new Exception("�����Ų���Ϊ��");
                if (this.OperationCodes == "") throw new Exception("������Ų���Ϊ��");

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
                LogHelper.Error("����ʧ��", ex);
                Response.Write(Rms.Web.JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
            }
        }

        /// <summary>
        /// ȡ��
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
                LogHelper.Error("ȡ��ʧ��", ex);
                Response.Write(Rms.Web.JavaScript.Alert(true, "ȡ��ʧ�ܣ�" + ex.Message));
            }
        }
}
}
