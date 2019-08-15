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
	using RmsPM.BLL;
	using System.Collections;
	
	/// <summary>
	/// 
	///		UCAttention ���ҵĹ�ע�Ĺ��ÿؼ���������Ա����ҳ�����˿ؼ���Module��KeyCode����
	///		ʹ�÷�����1,����һ��UserControlsĿ¼�µ�UCAttention��Ŀ��λ��
	///		2�������Ա������ protected RmsPM.Web.UserControls.UCAttention myUCAttention;
	///		3��Ϊ�ؼ���ֵ
	///		this.myUCAttention.Module = "������Ϣ";
	///		this.myUCAttention.Title = this.lblTaskName.Text;
	///		this.myUCAttention.ImportantLevel = int.Prise(this.strImportantLevel);�����Ѿ�ȡ��������
	///		ע�⣺�ؼ����ܷ���IsPostBack��
	///	
	/// </summary>
	/// <author>unm</author>
	///	<date>2004/10/22</date>
	///	<version>1.0</version>
	///	<modify>
	///	�޸�Ϊ������ע�ͱ�����ע,�趨��������
	///	myAttention.Module = "������Ϣ";
	/// myAttention.Title = this.lblTaskName.Text;	
	/// myAttention.MasterCode = this.strWBSCode;
	/// myAttention.Url = Request.RawUrl;
	/// myAttention.CurUser = base.user.UserCode;
	/// <date>2004/11/29</date>
	///	</modify>
	public partial class UCAttention : System.Web.UI.UserControl
	{

		
		private string strModule = "";
		/// <summary>
		/// ��ǰʹ�õ�ģ�飬���磺��������-��ǰ����
		/// </summary>
		public string Module
		{
			set	{	this.strModule = value;	}
		}		

		// ��ע�ı���
		private string strTitle = "";
		public string Title
		{
			set {	this.strTitle = value;	}
		}		

		private string strMasterCode = "";
		public string MasterCode
		{
			set	{	this.strMasterCode = value;	}
		}
		private string strUrl = "";
		public string Url
		{
			set	{	this.strUrl = value;	}
		}
		private string strCurUser = "";
		public string CurUser
		{
			set {	this.strCurUser = value; }
		}
		private string strProjectCode= "";
		public string ProjectCode
		{
			set {	this.strProjectCode = value; }
		}
		/// <summary>
		/// ָ���Ĺ�ע�Ƿ����
		/// </summary>
		private bool isExistAttention = false;
		private bool isExistRule = false;

		/// <summary>
		/// ��ע������
		/// </summary>
		private string strTaskAttentionCode = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				this.CheckExist();
				this.btAttention.Text = (this.isExistAttention&&this.isExistRule)?"ȡ����ע":"�����ע";
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// ��⵱ǰUrl�Ƿ��Ѿ�����
		/// </summary>
		private void CheckExist()
		{			
			try
			{
				EntityData entity = WBSDAO.GetAllTaskAttention();
				if (entity.HasRecord())
				{					
					DataView dv = new DataView(entity.CurrentTable," Url ='" + this.strUrl + "'","",DataViewRowState.CurrentRows);
					entity.Dispose();
					if(dv.Count>0)
					{
						this.isExistAttention = true;
						this.strTaskAttentionCode = dv.Table.Rows[0]["TaskAttentionCode"].ToString();
						User myUser = new User(this.strCurUser);
						if(myUser.HasResourceRight(this.strMasterCode,"070110"))
							this.isExistRule = true;
					}
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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


		protected void btAttention_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(this.isExistAttention)
				{					
					if(this.isExistRule)
					{ // ��Ȩ��ȡ��
						DelAttention();
						this.btAttention.Text = "�����ע";
					}
					else
					{ 
						// ��Ȩ������Լ��Ĺ�עȨ��
						this.SaveRS(this.strMasterCode,this.strCurUser,"","070110");
						this.btAttention.Text = "ȡ����ע";
					}					
				}
				else
				{	
					// �����ע��¼
					this.AddAttentionRecord();
					// ����Ȩ��,070110��ע�鿴Ȩ��
					this.SaveRS(this.strMasterCode,this.strCurUser,"","070110");
					this.btAttention.Text = "ȡ����ע";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		/// <summary>
		/// ɾ����ע�����¼
		/// </summary> 
		private void DelAttentionRecord()
		{
			EntityData entity = WBSDAO.GetTaskAttentionByCode(this.strTaskAttentionCode);
			WBSDAO.DeleteTaskAttention(entity);
			entity.Dispose();		
		}
		/// <summary>
		/// ������ע�����¼
		/// </summary>
		private void AddAttentionRecord()
		{
			this.strTaskAttentionCode = SystemManageDAO.GetNewSysCode("TaskAttentionCode");
			DateTime dt = DateTime.Now;
			EntityData entityExecute = new EntityData("TaskAttention");			
			DataRow dr = entityExecute.GetNewRecord();
			dr["TaskAttentionCode"] = this.strTaskAttentionCode;
			dr["AddModule"] = this.strModule;
			dr["MasterCode"] = this.strMasterCode;
			dr["UserCode"] = this.strCurUser;
			dr["AddTitle"] = this.strTitle;
			dr["Url"] = this.strUrl;
			dr["AddTime"] = dt.ToString();
			dr["ProjectCode"] = this.strProjectCode;
			entityExecute.AddNewRecord(dr);
			WBSDAO.InsertTaskAttention(entityExecute);
			entityExecute.Dispose();
		}

		/// <summary>
		/// ȡ����ע
		/// </summary>
		private void DelAttention()
		{
			//ȡ���Լ��Ĺ�עȨ��,��������070110Ȩ��
			WBSDAO.DelAttentionRule(this.strMasterCode,this.strCurUser);
			
		}

		public void AttentionProcess(string strUsers,string strStations)
		{
			try
			{
				this.CheckExist();
				if(!this.isExistAttention)
					this.AddAttentionRecord();

				// ����Ȩ��,070110��ע�鿴Ȩ��
				this.SaveRS(this.strMasterCode,strUsers,strStations,"070110");// ��ע�Ƚ����⣬����Ҫʹ��ԭ����Ȩ��
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		
		/// <summary>
		/// ���Ȩ����Դ
		/// </summary>
		private void SaveRS(string strMasterCode,string strUser,string strStation,string strOption)
		{			
			ArrayList arOperator = new ArrayList();
			if(strUser.Length>0)
			{
				foreach(string strTUser in strUser.Split(','))
				{
					if(strTUser=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 0;
					acRang.RelationCode = strTUser;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}			
			if(strStation.Length>0)
			{
				foreach(string strTStation in strStation.Split(','))
				{
					if(strTStation=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 1;
					acRang.RelationCode = strStation;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}
			
//			if(arOperator.Count>0)
				BLL.ResourceRule.SetResourceAccessRange(strMasterCode,strOption.Substring(0,4),"",arOperator,true);
		}
	}
}
