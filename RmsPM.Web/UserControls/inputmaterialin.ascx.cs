
namespace RmsPM.Web.UserControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// InputMaterialin的摘要说明。
    /// </summary>
    public partial class InputMaterialin : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //			this.divName.InnerText = this.txtName.Value;
            this.divHint.InnerText = this.txtHint.Value;
            this.divName.InnerText = this.txtName.Value;
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

        private void IniPage()
        {
            try
            {
                //this.txtInput.Attributes.Add("ImagePath", this.ImagePath);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
            }
        }

        /// <summary>
        /// 设置项目代码，初始化
        /// </summary>
        /// <param name="ProjectCode"></param>
        private void SetProject(string ProjectCode)
        {
            try
            {
                this.txtProjectCode.Value = ProjectCode;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
            }
        }

        //		public string ProjectCode
        //		{
        public string ProjectCode
        {
            get { return this.txtProjectCode.Value; }
            set { SetProject(value); }
        }

        /// <summary>
        /// 设置代码
        /// </summary>
        /// <param name="code"></param>
        protected void SetCode(string code)
        {
            try
            {
                this.txtCode.Value = code;

               // Response.Write(Rms.Web.JavaScript.Alert(true, "出错：" + Request["ProjectCode"]));
                TiannuoPM.MODEL.MaterialInDtlModel Model = RmsPM.BFL.MaterialInBFL.GetMaterialInDtl(RmsPM.BLL.ConvertRule.ToInt(code));
                this.txtMaterialCode.Value = Model.MaterialCode.ToString();
                this.txtOutPrice.Value = Model.InPrice.ToString();
                string InQty = Model.InQty.ToString();
                string InDate = RmsPM.BLL.StringRule.ShowDate(Model.InDate);
                string InPrice = Model.InPrice.ToString();
                this.divName.Attributes["title"] = "规格" + Model.Spec + " 单位" + Model.Unit + " 库存量" + InQty + " 入库日期" + InDate + " 入库单价" + InPrice;

                string name = Model.MaterialName;
                this.txtName.Value = name;
                this.txtHint.Value = "";
                this.divName.InnerText = this.txtName.Value;
                this.divHint.InnerText = this.txtHint.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //		public string Value 
        //		{
        public string MaterialInDtlCode
        {
            get { return this.txtCode.Value; }
            set { SetCode(value); }
        }

        //		public string Text 
        //		{
        public string MaterialName
        {
            get { return this.txtName.Value; }
        }
        public string MaterialCode
        {
            get { return this.txtMaterialCode.Value; }
        }

        //		public string Hint 
        //		{
        public string Hint
        {
            get { return this.txtHint.Value; }
        }

        public string imagePath = "../Images/";

        //		public string ImagePath
        //		{
        public string ImagePath
        {
            get { return this.imagePath; }
            set { this.imagePath = value; }
        }

        public string OutPrice
        {
            get { return this.txtOutPrice.Value; }
        }
    }
}
