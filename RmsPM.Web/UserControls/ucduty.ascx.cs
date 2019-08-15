//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�����޸ĵġ�
// �����Ѹ��ģ��������޸�Ϊ���ļ���App_Code\Migrated\usercontrols\Stub_ucduty_ascx_cs.cs���ĳ������ 
// �̳С�
// ������ʱ�������������� Web Ӧ�ó����е�������ʹ�øó������󶨺ͷ��� 
// ��������ҳ��
// ����������ҳ��usercontrols\ucduty.ascx��Ҳ���޸ģ��������µ�������
// �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
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
	///		UCDuty ��ժҪ˵����
	/// </summary>
	public partial class Migrated_UCDuty : UCDuty
	{
		protected string hClientID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			try
			{
				hClientID = this.ID;				

				if(!this.IsPostBack)
				{					
					User user = (User)Session["User"];
					EntityData entity = DAL.EntityDAO.SystemManageDAO.GetUnitByUserCode(user.UserCode);
					if(entity.HasRecord())
					{
						DataTable dt = entity.CurrentTable;
						for(int i=0;i<dt.Rows.Count;i++)
						{
							ListItem li = new ListItem(dt.Rows[i]["UnitName"].ToString(),dt.Rows[i]["UnitCode"].ToString());
							this.SelectDuty.Items.Add(li);
						}
					}					
				}
				else
				{
					// ȡ��ddl�е�����
					this.SelectDuty.Items.Clear();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		protected string ctrlPath = "../UserControl/";
//		public string CtrlPath
//		{
		override public string CtrlPath
		{
			set
			{
				this.ctrlPath = value;
			}
		}

//		public string Value
//		{
		override public string Value
		{
			get
			{
				return Request.Form[this.SelectDuty.UniqueID];
			}
			set
			{
				try
				{
					this.SelectDuty.Items.Clear();

					User user = (User)Session["User"];
					EntityData entity = DAL.EntityDAO.SystemManageDAO.GetUnitByUserCode(user.UserCode);
					if(entity.HasRecord())
					{
						DataTable dt = entity.CurrentTable;
						for(int i=0;i<dt.Rows.Count;i++)
						{
							this.SelectDuty.SelectedIndex = -1;
							ListItem li = new ListItem(dt.Rows[i]["UnitName"].ToString(),dt.Rows[i]["UnitCode"].ToString());
							if(dt.Rows[i]["UnitCode"].ToString()==value) li.Selected = true;
							this.SelectDuty.Items.Add(li);														
						}
					}
				}
				catch(Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"");
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
	}
}
