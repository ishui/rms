using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;


namespace RmsPM.Web.Supplier
{
    /// <summary>
    /// SupplierModify 的摘要说明。
    /// </summary>
    public partial class SupplierModify : PageBase
    {


        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                IniPage();
                LoadData();
            }
        }

        private void IniPage()
        {
            string supplierTypeCode = Request["supplierTypeCode"] + "";
            this.inputSystemGroup.Value = supplierTypeCode;
            this.inputSystemGroup.ClassCode = "1401";

            if (System.Configuration.ConfigurationSettings.AppSettings["PMName"] + "" == "YinRunPM")
            {
                this.SpanPerson.Visible = true;
                this.SpanPhone.Visible = true;
            }
            else
            {
                this.SpanPerson.Visible = false;
                this.SpanPhone.Visible = false;
            }

        }

        private void LoadData()
        {
            string SupplierCode = Request.QueryString["SupplierCode"] + "";

            try
            {



                EntityData entity = ProjectDAO.GetStandard_SupplierByCode(SupplierCode);

                if (entity.HasRecord())
                {
                    this.txtArtificialPerson.Text = entity.GetString("ArtificialPerson");
                    this.txtAbbreviation.Text = entity.GetString("Abbreviation");
                    this.txtCheckOpinion.Text = entity.GetString("CheckOpinion");
                    this.txtContractPerson.Text = entity.GetString("ContractPerson");
                    this.txtEmail.Text = entity.GetString("Email");
                    this.txtIndustrySort.Text = entity.GetString("IndustrySort");
                    this.txtIndustryType.Text = entity.GetString("IndustryType");
                    this.txtLicenseID.Text = entity.GetString("LicenseID");
                    this.txtOfficePhone.Text = entity.GetString("OfficePhone");
                    this.txtPostCode.Text = entity.GetString("PostCode");
                    this.txtProduct.Text = entity.GetString("Product");
                    this.txtQuality.Text = entity.GetString("Quality");
                    this.txtRegisteredAddress.Text = entity.GetString("RegisteredAddress");
                    this.txtRegisteredCapital.Text = entity.GetString("RegisteredCapital");
                    this.txtSJHG.Text = entity.GetString("SJHG");
                    string typeCode = entity.GetString("SupplierTypeCode");
                    this.inputSystemGroup.Value = typeCode;
                    this.txtSupplierName.Text = entity.GetString("SupplierName");
                    this.txtTaxID.Text = entity.GetString("TaxID");
                    this.txtTaxNo.Text = entity.GetString("TaxNo");
                    //					this.txtU8Code.Text = entity.GetString("U8Code");
                    this.txtWebAddress.Text = entity.GetString("WebAddress");
                    this.txtWorkAddress.Text = entity.GetString("WorkAddress");
                    this.txtWorkTimeLimit.Text = entity.GetString("WorkTimeLimit");

                    this.txtAreaCode.Text = entity.GetString("AreaCode");
                    this.txtMobile.Text = entity.GetString("Mobile");
                    this.txtFax.Text = entity.GetString("Fax");
                    this.txtAchievement.Text = entity.GetString("Achievement");
                    this.txtCreditLevel.Text = entity.GetString("CreditLevel");
         
                    this.txtWorkScope.Text = entity.GetString("WorkScope");
                    this.txtStructure.Text = entity.GetString("Structure");
                    this.txtRemark.Text = entity.GetString("Remark");

                    SupplierRule.LoadDictionarySelect(this.selSaleType, "销售形式", entity.GetString("saleType"));
                    SupplierRule.LoadDictionarySelect(this.selCharacterType, "品质类别", entity.GetString("characterType"));
                    this.selCCC.Value = entity.GetString("IsCCC");
                    this.selISO.Value = entity.GetString("IsISO");
                    // Added by yiwl at 2007-03-08
                    this.tbxOpenBank.Text = entity.GetString("OpenBank");
                    this.tbxAccount.Text = entity.GetString("Account");
                    this.tbxReciver.Text = entity.GetString("Reciver");

                    switch (this.up_sPMName.ToLower())
                    {
                        case "tangchenpm":
                            this.selQualityGrade.Visible = false;
                            this.tdQualityGrade.InnerHtml = entity.GetString("QualityGrade") + "&nbsp;";
                            break;
                        default:
                            SupplierRule.LoadDictionarySelect(this.selQualityGrade, "资质等级", entity.GetString("QualityGrade"));
                            break;

                    }
                   
                }
                else
                {
                    SupplierRule.LoadDictionarySelect(this.selSaleType, "销售形式", "");
                    SupplierRule.LoadDictionarySelect(this.selCharacterType, "品质类别", "");
                }

                entity.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "载入页面出错");
                Response.Write(Rms.Web.JavaScript.Alert(true, "载入页面出错：" + ex.Message));
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



        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {


            string supplierCode = Request["SupplierCode"] + "";
            bool isNew = (supplierCode == "");

            if (this.txtSupplierName.Text.Trim() == "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "请填写厂商名称 ！"));
                return;
            }

            if (this.inputSystemGroup.Value == "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "请填写厂商类型 ！"));
                return;
            }

            if (System.Configuration.ConfigurationSettings.AppSettings["PMName"] + "" == "YinRunPM")
            {
                if (this.txtContractPerson.Text.Trim() == "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "请填写联系人 ！"));
                    return;
                }
                if (this.txtOfficePhone.Text.Trim() == "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "请填写联系电话 ！"));
                    return;
                }

            }

            if (isNew)
            {
                if (!user.HasTypeOperationRight("140102", this.inputSystemGroup.Value))
                    Response.Write(Rms.Web.JavaScript.Alert(true, "您没有权限操作这类厂商 ！"));
            }

            //			string subjectSetCode = Request["SubjectSetCode"];

            try
            {
                //检查厂商不能重名
                SupplierStrategyBuilder sb = new SupplierStrategyBuilder();
                ArrayList ar = new ArrayList();

                ar.Add(supplierCode);
                ar.Add(this.txtSupplierName.Text.Trim());
                ar.Add(this.txtAbbreviation.Text.Trim());

                sb.AddStrategy(new Strategy(SupplierStrategyName.CheckName, ar));

                string sql = sb.BuildMainQueryString();
                QueryAgent qa = new QueryAgent();
                DataSet checkName = qa.ExecSqlForDataSet(sql);
                qa.Dispose();
                int checkNameCount = checkName.Tables[0].Rows.Count;

                if (checkNameCount > 0)
                {
                    //检查类别是否也相同（不同类别允许重名） xyq 2006.2.22
                    DataRow[] drs = checkName.Tables[0].Select("SupplierTypeCode = '" + this.inputSystemGroup.Value + "'");

                    if (drs.Length > 0)
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "该类型下发现重名厂商 ！"));
                        return;
                    }
                }

                checkName.Dispose();

                EntityData entity = null;
                DataRow dr = null;

                if (isNew)
                {
                    entity = new EntityData("Standard_Supplier");
                    dr = entity.GetNewRecord();
                    supplierCode = SystemManageDAO.GetNewSysCode("SupplierCode").ToString();
                    dr["SupplierCode"] = supplierCode;
                    dr["Status"] = 0;
                    entity.AddNewRecord(dr);
                    //					dr["SubjectSetCode"]=subjectSetCode;
                }
                else
                {
                    entity = DAL.EntityDAO.ProjectDAO.GetStandard_SupplierByCode(supplierCode);
                    dr = entity.CurrentRow;
                }

                //				dr["U8Code"] = this.txtU8Code.Text;
                dr["SupplierName"] = this.txtSupplierName.Text;
                dr["Quality"] = this.txtQuality.Text;
                dr["RegisteredCapital"] = txtRegisteredCapital.Text;
                dr["Product"] = txtProduct.Text;
                dr["CheckOpinion"] = txtCheckOpinion.Text;
                dr["SupplierTypeCode"] = this.inputSystemGroup.Value;
                dr["ArtificialPerson"] = txtArtificialPerson.Text;
                dr["Abbreviation"] = this.txtAbbreviation.Text;
                dr["ContractPerson"] = txtContractPerson.Text;
                dr["OfficePhone"] = txtOfficePhone.Text;
                dr["RegisteredAddress"] = txtRegisteredAddress.Text;
                dr["IndustryType"] = txtIndustryType.Text;
                dr["IndustrySort"] = txtIndustrySort.Text;
                dr["SJHG"] = txtSJHG.Text;
                dr["LicenseID"] = txtLicenseID.Text;
                dr["TaxID"] = txtTaxID.Text;
                dr["TaxNo"] = txtTaxNo.Text;
                dr["WorkAddress"] = txtWorkAddress.Text;
                dr["WorkTimeLimit"] = txtWorkTimeLimit.Text;
                dr["PostCode"] = txtPostCode.Text;
                dr["EMail"] = this.txtEmail.Text;
                dr["WebAddress"] = txtWebAddress.Text;

                dr["AreaCode"] = this.txtAreaCode.Text;
                dr["Mobile"] = this.txtMobile.Text;
                dr["Fax"] = this.txtFax.Text;
                dr["Achievement"] = this.txtAchievement.Text;
                dr["CreditLevel"] = this.txtCreditLevel.Text;
                dr["Workscope"] = this.txtWorkScope.Text;
                dr["Structure"] = this.txtStructure.Text;
                dr["Remark"] = this.txtRemark.Text;

                dr["saleType"] = this.selSaleType.Value;
                dr["characterType"] = this.selCharacterType.Value;
                dr["IsCCC"] = this.selCCC.Value;
                dr["IsISO"] = this.selISO.Value;
                //Added by yiwl at 2007-03-08
                dr["OpenBank"] = this.tbxOpenBank.Text;
                dr["Account"] = this.tbxAccount.Text;
                dr["Reciver"] = this.tbxReciver.Text;
                switch (this.up_sPMName.ToLower())
                {
                    case "tangchenpm":
                        break;
                    default:
                        dr["QualityGrade"] = this.selQualityGrade.Value;
                        break;

                }

                DAL.EntityDAO.ProjectDAO.SubmitAllStandard_Supplier(entity);

                entity.Dispose();

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
                Response.Write("<script>window.close();</script>");

            }


            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
            }

        }




    }
}
