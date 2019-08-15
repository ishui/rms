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
	///		AttachMentList:��ʾ�������б������ת��TaskAttachMent.aspx���档
	///		ʹ�÷�����
	///		1������ؼ���ָ��λ��
	///		2��������Ա������protected RmsPM.Web.UserControls.AttachMentList myAttachMentList;
	///		3��Ϊ�ؼ���ֵ
	///		this.myAttachMentList.AttachMentType = "TaskExecute";
	///		this.myAttachMentList.MasterCode = "1";
	///		
	/// </summary>
	/// <author>unm</author>
	/// <date>2004/11/9</date>
	/// <version>1.0</version>
	/// 
	/// <modify></modify>
	/// <description>����Ŀ¼�ṹ����ͬһ��Ŀؼ�ʹ�ã�������·������
	///  ���磺OA�µ�Newsʹ���˿ؼ�����ָ���ؼ�����ڵ�ǰ·������Ϊ��../../UserControls/
	/// </description>
	/// <date>2005��2��23</date>
	/// <version></version>
	public partial class AttachMentList : System.Web.UI.UserControl
	{
		//private string AttachMentType = "";
		//private string MasterCode = "";

		/// <summary>
		/// �ϴ����������ͣ����磺TaskExecute(��������)��
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
		/// ���������������¼�ı���
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
		/// ����MasterCodeɾ��������ע����Ҫ�趨AttachMentType������
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
                    lbl.Text = "&nbsp; &nbsp;<a href="+strUrl+" Title='�������' Target='_blank'>"+dt.Rows[i]["FileName"].ToString()+"</a>";
                    this.plAttachMentList.Controls.Add(lbl);

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
	}
}
