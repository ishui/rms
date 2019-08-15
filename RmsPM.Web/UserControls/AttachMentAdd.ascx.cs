namespace RmsPM.Web.UserControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using RmsPM.DAL.EntityDAO;
    using Rms.ORMap;
    using Rms.Web;

    /// <summary>
    /// 
    ///		AttachMentAdd ��������ɾ���ϴ�������ʵ��
    ///		ʹ�÷�����
    ///		1������ؼ���ָ��λ��
    ///		2��������Ա������protected RmsPM.Web.UserControls.AttachMentAdd myAttachMentAdd;
    ///		3��Ϊ�ؼ���ֵ
    ///		this.myAttachMentAdd.AttachMentType = "TaskExecute";
    ///		this.myAttachMentAdd.MasterCode = "1";
    ///		ע�⣺1,��ͬ�ĸ�������AttachMentType�����ظ�����ע����
    ///			  2,�ؼ����ܷ���IsPostBack��
    ///			  3,�������ڵĴ���ʽΪ����ҳ��ͬʱ����ϵͳΨһ��ţ����ҳ��ˢ��ʱ����Ҫ�±��
    ///				if(ViewState["TaskExecuteCode"]==null)
    ///				{
    ///					this.TaskExecuteCode = SystemManageDAO.GetNewSysCode("TaskExecuteCode");
    ///					ViewState["TaskExecuteCode"] = this.TaskExecuteCode ;
    ///				}
    ///				else
    ///					this.TaskExecuteCode = (string)ViewState["TaskExecuteCode"];
    ///		
    /// </summary>
    /// <author>unm</author>
    /// <date>2004/11/9</date>
    /// <version>1.0</version>
    /// 
    /// <modify>unm</modify>
    /// <description>
    /// �����Ĵ���ʽ�ı䣺����ҳ�治��Ҫ������ϵͳ��ţ�����ֱ��ʹ�ü��ɣ������ڱ���ʱ�����һ��������������
    /// ���÷�����SaveAttachMent(string strNewMasterCode)
    /// </description>
    /// <date>2004/11/14</date>
    /// <version>1.3</version>
    /// 
    /// <modify>unm</modify>
    /// <description>����Ŀ¼�ṹ����ͬһ��Ŀؼ�ʹ�ã�������·������
    ///  ���磺OA�µ�Newsʹ���˿ؼ�����ָ���ؼ�����ڵ�ǰ·������Ϊ��../../UserControls/
    /// </description>
    /// <date>2005��2��23</date>
    /// <version>1.3</version>
    public partial class AttachMentAdd : System.Web.UI.UserControl
    {
        //protected string strAttachMentType = "";
        //protected string strMasterCode = "";
        protected string FileNameClientID = "";
        protected System.Web.UI.WebControls.TextBox TextBox1;
        protected System.Web.UI.WebControls.Button btUpF;
        protected System.Web.UI.WebControls.Button btSelectFile;
        protected string tmpFileNameClientID = "";



        /// <summary>
        /// �ϴ����������ͣ����磺TaskExecute(��������)��
        /// </summary>
        public string AttachMentType
        {
            set
            {
                this.ViewState["AttachMentType"] = value;
                //this.strAttachMentType = value;
            }
            get
            {
                if (this.ViewState["AttachMentType"] == null)
                    return "";
                return this.ViewState["AttachMentType"].ToString();
            }
        }




        /// <summary>
        /// ���������������¼�ı���
        /// </summary>
        public string MasterCode
        {
            
            set
            {
                if (value!=null&&value.Length > 0)
                {
                    this.ViewState["MasterCode"] = value;
                    this.ViewState["TempTypeHead"] = "";
                    this.LoadData();
                }
                else
                {
                    // ȷ��ҳ��ˢ��û������ϵͳ����
                    if (ViewState["TmpAttachMent"] == null)
                    {
                        //modi by simon �޸�һ��Ǳ��bug
                        this.ViewState["MasterCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TmpAttachMent");//����tmpattachment���ܻ������������mastercode��ͬ
                        ViewState["TmpAttachMent"] = this.ViewState["MasterCode"];

                    }
                    else
                    {
                        this.ViewState["MasterCode"] = (string)ViewState["TmpAttachMent"];
                        // ViewState["TempTypeHead"] = "";
                    }
                    ViewState["TempTypeHead"] = BLL.DocumentRule.tmpTypeHead;//δ����tmpattachment��ֵ������������mastercode��ͬ����type���ó�һ����ʱ��ֵ��֮����޸�mastercodeʱ����ֻ�޸�Ӧ���޸ĵ�tmpmastercode
                }

            }
            get
            {
                if (this.ViewState["MasterCode"] == null)
                    return "";
                return this.ViewState["MasterCode"].ToString();
            }
        }

        protected string ctrlPath = "../UserControls/";
        public string CtrlPath
        {
            set
            {
                this.ctrlPath = value;
            }
        }

        public int Count
        {
            get
            {
                EntityData entityAttachMent = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(this.AttachMentType, this.MasterCode);
                return entityAttachMent.CurrentTable.Rows.Count;
            }
        }

        /// <summary>
        /// ���ݸ������ɾ������
        /// </summary>
        /// <param name="strCode"></param>
        public void DelAttachMent(string strCode)
        {
            EntityData entityAttachMent = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByCode(strCode);
            if (entityAttachMent.HasRecord())
                RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().DeleteAttachMent(entityAttachMent);
        }

        //		/// <summary>
        //		/// ���ݴ����Array���͵ĸ������ɾ������,���뷽ʽ�磺Array arDelCode;
        //		/// </summary>
        //		/// <param name="arDelCode"></param>
        //		public void DelAttachMent(Array arDelCode)
        //		{
        //			foreach(string strCode in arDelCode)
        //			{
        //				this.DelAttachMent(strCode);
        //			}
        //		}
        //
        //		/// <summary>
        //		/// ���ݴ���Ķ��ŷָ��ĸ������ɾ������,���뷽ʽ�磺"1,2,3,4,5"
        //		/// </summary>
        //		/// <param name="strCommaString"></param>
        //		public void DelAttachMentByCommaString(string strCommaString)
        //		{
        //			string[] arTmp = strCommaString.Split(',');
        //			foreach(string strCode in arTmp)
        //			{
        //				this.DelAttachMent(strCode);
        //			}
        //		}

        /// <summary>
        /// ����MasterCodeɾ��������ע����Ҫ�趨AttachMentType������
        /// </summary>
        /// <param name="strMasterCode"></param>
        public void DelAttachMentByMasterCode(string MasterCode)
        {
            EntityData entityAttachMent = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(this.AttachMentType, this.MasterCode);
            if (entityAttachMent.HasRecord())
                RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().DeleteAttachMent(entityAttachMent);
            entityAttachMent.Dispose();
        }

        /// <summary>
        /// ������������ʱ�򱣴���Ҫ���ô˷�������ʱЯ����������������µı�ţ��޸Ĳ�������Ҫ���ô˷���
        /// ����ҵ��ҳ��û�д��� MasterCodeʱ����ҵ�������������棬��ʱȡ��mastercode������ô˷���
        /// </summary>
        /// <param name="strNewMasterCode">�µ������¼KeyCode</param>
        public void SaveAttachMent(string strNewMasterCode)//����Ӧ��ȡΪupdateMasterCode ������� by Simon
        {
            ViewState["TempTypeHead"] = "";
            BLL.DocumentRule.Instance().UpdateMasterCodeAttachmentType(strNewMasterCode, this.MasterCode, this.AttachMentType);
            this.MasterCode = strNewMasterCode;
            /*
            EntityData entityAttachMent = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(this.AttachMentType, this.MasterCode);
			if(entityAttachMent.HasRecord())
			{
				DataTable dt = entityAttachMent.CurrentTable;
				for(int i=0;i<dt.Rows.Count;i++)
				{
					dt.Rows[i]["MasterCode"] = strNewMasterCode;
				}
                RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().UpdateAttachMent(entityAttachMent);
			}
			entityAttachMent.Dispose();
            */
        }


        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                // �ڴ˴������û������Գ�ʼ��ҳ��
                //this.FileNameClientID = this.txtFileName.ClientID;
                //this.tmpFileNameClientID = this.tmpFileName.ClientID;

                if (this.MasterCode.Length < 1)
                    this.MasterCode = "";

                this.LoadData();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��������ʧ��");
            }
        }


        /// <summary>
        /// �������ݣ���̬���ɴ�ɾ���Ŀؼ�
        /// </summary>
        private void LoadData()
        {
            if (AttachMentType.Length < 1 || MasterCode.Length < 1) return;
            this.tdDeleteList.Controls.Clear();
            if (this.ViewState["TempTypeHead"] == null) this.ViewState["TempTypeHead"] = string.Empty;
            EntityData entityAttachMent = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(this.ViewState["TempTypeHead"] + this.AttachMentType, this.MasterCode);
            if (entityAttachMent.HasRecord())
            {
                DataTable dt = entityAttachMent.CurrentTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label lbl = new Label();
                    lbl.Text = "&nbsp; &nbsp;<A onclick=\"" + this.ClientID + "ShowEditMenu(this,code);\" href=\"#\" code=" + dt.Rows[i]["AttachMentCode"].ToString() + ">" + dt.Rows[i]["FileName"].ToString() + "</a>";
                    this.tdDeleteList.Controls.Add(lbl);
                }
            }
        }
        public void ControlLoadData()
        {
            this.LoadData();
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
        //
        //		/// <summary>
        //		/// �Զ����¼���ɾ��ѡ��ĸ���
        //		/// </summary>
        //		/// <param name="sender"></param>
        //		/// <param name="e"></param>
        //		private void btnAttach_Click(object sender, System.EventArgs e)
        //		{
        //			Button btnAttach = (Button)sender;	
        //			string strCode = btnAttach.Attributes["AttachMentCode"].ToString();
        //			this.DelAttachMent(strCode);
        //			// ɾ���������µ�����
        //			this.LoadData();
        //		}		
        /*
                /// <summary>
                /// �����ϴ��ĸ���
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                private void btUpFile_Click(object sender, System.EventArgs e)
                {
                    try
                    {
                        this.SaveFile();
                    }
                    catch (Exception ex)
                    {
                        ApplicationLog.WriteLog(this.ToString(),ex,"�����ϴ�ʧ��");
                    }
                }

                /// <summary>
                /// �����ϴ��ĸ�����ʵ��
                /// </summary>
                private void SaveFile()
                {


 
        <div style="VISIBILITY: hidden"><!--��һ�ַ�ʽ���ϴ��ļ� -->
            <INPUT class="button-small" id="txtFileName" style="WIDTH: 0px" type="file" onchange="SetTmpFileName();"
                name="txtFileName" runat="server">&nbsp; <input id="tmpFileName" disabled type="text" runat="server" NAME="tmpFileName">
            <asp:button class="button-small" id="btUpFile" runat="server" Text="�� ��"></asp:button>
        </div>



                    string strFileName = this.GetFileName(this.txtFileName.Value.Trim());
                    if(strFileName.Length<1)
                    {
                        this.JSAlert("��ѡ��Ҫ�ϴ��ĸ�����");
                        return;
                    }
                    User myUser = (User)Session["User"];
                    EntityData entityAttachMent = WBSDAO.GetAllAttachMent();
                    if(entityAttachMent == null)
                    {
                        entityAttachMent = new EntityData("AttachMent");
                    }									
                    DataRow dr = entityAttachMent.GetNewRecord();

                    System.IO.Stream imgStream;
                    imgStream = this.txtFileName.PostedFile.InputStream;
                    int imgStreamLen = this.txtFileName.PostedFile.ContentLength;
                    byte[] imgData = new byte[imgStreamLen];
                    int n = imgStream.Read(imgData,0,imgStreamLen);
                    dr["AttachMentCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("AttachMentCode");;
                    dr["AttachMentType"] = this.strAttachMentType;
                    dr["MasterCode"] = this.strMasterCode;
                    dr["FileName"] = strFileName;
                    dr["CreatePerson"] = myUser.UserCode;
                    dr["CreateDate"] = DateTime.Now;
                    dr["Content_Type"] = this.txtFileName.PostedFile.ContentType;
                    dr["Content"] = imgData;
                    dr["Length"] = imgStreamLen;

                    entityAttachMent.AddNewRecord(dr);
                    if( entityAttachMent != null && entityAttachMent.HasRecord())
                    {
                        WBSDAO.InsertAttachMent(entityAttachMent);
                    }
                    entityAttachMent.Dispose();

                    // ����������µ�����
                    this.LoadData();
                }

                /// <summary>
                /// ȡ������·���е��ļ���
                /// </summary>
                private string GetFileName(string strFile)
                {
                    int i = strFile.LastIndexOf('\\');
                    if(i>0)
                        return strFile.Substring(i+1);
                    else
                        return strFile;
                }

                /// <summary>
                /// javascript��ʾ��Ϣ
                /// </summary>
                /// <param name="strInfo"></param>
                private void JSAlert(string strInfo)
                {
                    Response.Write(JavaScript.ScriptStart);
                    Response.Write("alert('"+strInfo+"');");
                    Response.Write(JavaScript.ScriptEnd);
                }
        */
    }
}
