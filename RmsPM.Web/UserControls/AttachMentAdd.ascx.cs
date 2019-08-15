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
    ///		AttachMentAdd ：新增和删除上传附件的实现
    ///		使用方法：
    ///		1，拖入控件到指定位置
    ///		2，声明成员变量：protected RmsPM.Web.UserControls.AttachMentAdd myAttachMentAdd;
    ///		3，为控件赋值
    ///		this.myAttachMentAdd.AttachMentType = "TaskExecute";
    ///		this.myAttachMentAdd.MasterCode = "1";
    ///		注意：1,不同的附件类型AttachMentType不能重复，请注意检查
    ///			  2,控件不能放入IsPostBack中
    ///			  3,由于现在的处理方式为新增页面同时生成系统唯一编号，因此页面刷新时不需要新编号
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
    /// 附件的处理方式改变：新增页面不需要先生成系统编号，现在直接使用即可，但是在保存时需调用一个方法更新主键
    /// 调用方法：SaveAttachMent(string strNewMasterCode)
    /// </description>
    /// <date>2004/11/14</date>
    /// <version>1.3</version>
    /// 
    /// <modify>unm</modify>
    /// <description>对于目录结构不是同一层的控件使用，加入了路径属性
    ///  例如：OA下的News使用了控件，则指定控件相对于当前路径属性为：../../UserControls/
    /// </description>
    /// <date>2005、2、23</date>
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
        /// 上传附件的类型，例如：TaskExecute(工作报告)等
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
        /// 附带附件的主表记录的编码
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
                    // 确保页面刷新没有新增系统编码
                    if (ViewState["TmpAttachMent"] == null)
                    {
                        //modi by simon 修改一个潜在bug
                        this.ViewState["MasterCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TmpAttachMent");//这里tmpattachment可能会和其他正常的mastercode相同
                        ViewState["TmpAttachMent"] = this.ViewState["MasterCode"];

                    }
                    else
                    {
                        this.ViewState["MasterCode"] = (string)ViewState["TmpAttachMent"];
                        // ViewState["TempTypeHead"] = "";
                    }
                    ViewState["TempTypeHead"] = BLL.DocumentRule.tmpTypeHead;//未避免tmpattachment的值与其他正常的mastercode相同，将type设置成一个零时的值，之后的修改mastercode时就能只修改应该修改的tmpmastercode
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
        /// 根据附件编号删除附件
        /// </summary>
        /// <param name="strCode"></param>
        public void DelAttachMent(string strCode)
        {
            EntityData entityAttachMent = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByCode(strCode);
            if (entityAttachMent.HasRecord())
                RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().DeleteAttachMent(entityAttachMent);
        }

        //		/// <summary>
        //		/// 根据传入的Array类型的附件编号删除附件,传入方式如：Array arDelCode;
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
        //		/// 根据传入的逗号分隔的附件编号删除附件,传入方式如："1,2,3,4,5"
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
        /// 根据MasterCode删除附件，注意需要设定AttachMentType的属性
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
        /// 当新增操作的时候保存需要调用此方法，此时携带附件的主表产生新的编号，修改操作不需要调用此方法
        /// 仅在业务页面没有传入 MasterCode时，当业务数据正常保存，此时取得mastercode，需调用此方法
        /// </summary>
        /// <param name="strNewMasterCode">新的主表记录KeyCode</param>
        public void SaveAttachMent(string strNewMasterCode)//名字应该取为updateMasterCode 以免误会 by Simon
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
                // 在此处放置用户代码以初始化页面
                //this.FileNameClientID = this.txtFileName.ClientID;
                //this.tmpFileNameClientID = this.tmpFileName.ClientID;

                if (this.MasterCode.Length < 1)
                    this.MasterCode = "";

                this.LoadData();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "附件载入失败");
            }
        }


        /// <summary>
        /// 载入数据，动态生成待删除的控件
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
        //
        //		/// <summary>
        //		/// 自定义事件，删除选择的附件
        //		/// </summary>
        //		/// <param name="sender"></param>
        //		/// <param name="e"></param>
        //		private void btnAttach_Click(object sender, System.EventArgs e)
        //		{
        //			Button btnAttach = (Button)sender;	
        //			string strCode = btnAttach.Attributes["AttachMentCode"].ToString();
        //			this.DelAttachMent(strCode);
        //			// 删除后载入新的数据
        //			this.LoadData();
        //		}		
        /*
                /// <summary>
                /// 保存上传的附件
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
                        ApplicationLog.WriteLog(this.ToString(),ex,"附件上传失败");
                    }
                }

                /// <summary>
                /// 保存上传的附件：实现
                /// </summary>
                private void SaveFile()
                {


 
        <div style="VISIBILITY: hidden"><!--另一种方式的上传文件 -->
            <INPUT class="button-small" id="txtFileName" style="WIDTH: 0px" type="file" onchange="SetTmpFileName();"
                name="txtFileName" runat="server">&nbsp; <input id="tmpFileName" disabled type="text" runat="server" NAME="tmpFileName">
            <asp:button class="button-small" id="btUpFile" runat="server" Text="上 传"></asp:button>
        </div>



                    string strFileName = this.GetFileName(this.txtFileName.Value.Trim());
                    if(strFileName.Length<1)
                    {
                        this.JSAlert("请选择要上传的附件！");
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

                    // 保存后载入新的数据
                    this.LoadData();
                }

                /// <summary>
                /// 取得完整路径中的文件名
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
                /// javascript提示信息
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
