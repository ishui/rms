namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using RmsPM.DAL.EntityDAO;

	/// <summary>
	///		AttachMentList:显示附件的列表，点击后转入TaskAttachMent.aspx界面。
	///		使用方法：
	///		1，拖入控件到指定位置
	///		2，声明成员变量：protected RmsPM.Web.UserControls.AttachMentList myAttachMentList;
	///		3，为控件赋值
	///		this.myAttachMentList.AttachMentType = "TaskExecute";
	///		this.myAttachMentList.MasterCode = "1";
	///		
	/// </summary>
	/// <author>unm</author>
	/// <date>2004/11/9</date>
	/// <version>1.0</version>
	/// 
	/// <modify></modify>
	/// <description>对于目录结构不是同一层的控件使用，加入了路径属性
	///  例如：OA下的News使用了控件，则指定控件相对于当前路径属性为：../../UserControls/
	/// </description>
	/// <date>2005、2、23</date>
	/// <version></version>
	public partial class AttachMentList : System.Web.UI.UserControl
	{
		//private string AttachMentType = "";
		//private string MasterCode = "";

		/// <summary>
		/// 上传附件的类型，例如：TaskExecute(工作报告)等
		/// </summary>
        public string AttachMentType
        {
            set
            {
                this.ViewState["AttachMentType"] = value;
                //this.AttachMentType = value;
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
                this.ViewState["MasterCode"] = value;
                this.LoadData();
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
		/// 根据MasterCode删除附件，注意需要设定AttachMentType的属性
		/// </summary>
		/// <param name="MasterCode"></param>
		public void DelAttachMentByMasterCode(string MasterCode)
		{
            EntityData entityAttachMent = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(this.AttachMentType, this.MasterCode);
			if(entityAttachMent.HasRecord())
                RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().DeleteAttachMent(entityAttachMent);
			entityAttachMent.Dispose();
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.LoadData();
		}

		private void LoadData()
		{
			if(AttachMentType.Length<1||MasterCode.Length<1) return;
            this.plAttachMentList.Controls.Clear();
            EntityData entityAttachMent = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(this.AttachMentType, this.MasterCode);
			if(entityAttachMent.HasRecord())
			{
				DataTable dt = entityAttachMent.CurrentTable;
				for(int i=0;i<dt.Rows.Count;i++)
				{
					string strUrl = ctrlPath+"../Project/WBSAttachMentView.aspx?Action=View&AttachMentCode="+dt.Rows[i]["AttachMentCode"].ToString();
                    Label lbl = new Label();					
                    lbl.Text = "&nbsp; &nbsp;<a href="+strUrl+" Title='点击下载' Target='_blank'>"+dt.Rows[i]["FileName"].ToString()+"</a>";
                    this.plAttachMentList.Controls.Add(lbl);

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
	}
}
