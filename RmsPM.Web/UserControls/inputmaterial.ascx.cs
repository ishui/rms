
namespace RmsPM.Web.UserControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// InputMaterial的摘要说明。
    /// </summary>
    public partial class InputMaterial : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.divName.InnerText = this.txtName.Value;
            this.divHint.InnerText = this.txtHint.Value;

            if (this.Visible)
            {
                //this.txtInput.Attributes["ClientID"] = this.ClientID;

                //				string reload = Rms.Web.JavaScript.ScriptStart;
                //				reload += @"var ClientID = '" + this.ClientID + "';" + "\n" ;
                //				reload += Rms.Web.JavaScript.ScriptEnd;
                //				Response.Write(reload);

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
        private void SetCode(string code)
        {
            try
            {

                this.txtCode.Value = code;
                TiannuoPM.MODEL.MaterialModel Model = RmsPM.BFL.MaterialBFL.GetMaterial(RmsPM.BLL.ConvertRule.ToInt(code));
                string name = Model.MaterialName;
                this.txtName.Value = name;
                this.txtHint.Value = "";
               // this.txtInput.Value = name;
                this.divName.Attributes["title"] = "规格" + Model.Spec + " 单位" + Model.Unit;
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
        public string Value
        {
            get { return this.txtCode.Value; }
            set { SetCode(value); }
        }

        //		public string Text 
        //		{
         public string Text
        {
            get { return this.txtName.Value; }
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
        public string UnitValue
        {
            get { return this.txtunit.Value; }
        }
        
        public string OutQty
        {
            set { this.txtOutQty.Value = value; }
            
        }
    }
}
