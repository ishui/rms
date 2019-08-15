
namespace RmsPM.Web.UserControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// InputMaterial��ժҪ˵����
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

        private void IniPage()
        {
            try
            {
                //this.txtInput.Attributes.Add("ImagePath", this.ImagePath);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ҳ��ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
            }
        }

        /// <summary>
        /// ������Ŀ���룬��ʼ��
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
                ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ҳ��ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
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
        /// ���ô���
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
                this.divName.Attributes["title"] = "���" + Model.Spec + " ��λ" + Model.Unit;
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
