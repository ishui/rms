namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;

	/// <summary>
	///		UCSelectProject ��ժҪ˵����
	/// </summary>
	public partial class UCSelectProject : System.Web.UI.UserControl
	{

		/// <summary>
		/// �Ƿ񴥷����������¼�
		/// </summary>
		public bool ProjectAutoPostBack
		{
			get
			{
				return this.ddlSelProject.AutoPostBack;
			}
			set
			{
				this.ddlSelProject.AutoPostBack = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				this.ddlSelProject.Attributes.Add("onchange",this.ClientID+"ChangeProjectEvent(this.value);");

				if(!this.IsPostBack)
				{
					// �ڴ˴������û������Գ�ʼ��ҳ��
					EntityData entity = new EntityData("Project");
					DataTable dt = entity.CurrentTable;
			
					if(this.access=="CanAccess")
					{
						if ( Session["User"] != null )
						{
							User user = (User)Session["User"];
							dt = user.m_EntityDataAccessProject.CurrentTable.Copy();
						}					
					}
					else
						dt = DAL.EntityDAO.ProjectDAO.GetAllProject().CurrentTable.Copy();

					DataRow dr = dt.NewRow();
					dr["projectName"] = "--��ѡ��--";
					dr["projectCode"] = "";				
					dt.Rows.Add(dr);
					DataView dv = dt.DefaultView;
					dv.Sort = "projectCode";
			
					this.ddlSelProject.DataSource = dv;
					this.ddlSelProject.DataTextField = "projectName";
					this.ddlSelProject.DataValueField = "projectCode";
					this.ddlSelProject.DataBind();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		public string Value
		{
			get
			{
				return this.ddlSelProject.SelectedValue;
			}
			set
			{
				this.ddlSelProject.SelectedValue = value;
			}
		}

		private string access = "CanAccess"; // ��Ȩ�޵���Ŀ all ȫ����Ŀ
		public string Access
		{
			set
			{
				this.access = value;
			}
		}

		protected string changeTarget="";
		public string ChangeTaget
		{
			set
			{
				string[] artmp = value.Split(',');
				foreach(string tmp in artmp)
				{
					if(tmp.Length<1) continue;
					this.changeTarget += tmp+"ProjectCodeOnChange(objValue);\n";
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

		public event EventHandler SelectProject;
		protected void ddlSelProject_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SelectProject(this,EventArgs.Empty);		
		}
	}
}
