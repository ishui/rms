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
	///		UCAttention ：我的关注的公用控件，开发人员拖入页面后给此控件的Module和KeyCode即可
	///		使用方法：1,拖入一个UserControls目录下的UCAttention到目的位置
	///		2，加入成员变量： protected RmsPM.Web.UserControls.UCAttention myUCAttention;
	///		3，为控件赋值
	///		this.myUCAttention.Module = "工作信息";
	///		this.myUCAttention.Title = this.lblTaskName.Text;
	///		this.myUCAttention.ImportantLevel = int.Prise(this.strImportantLevel);现在已经取消此属性
	///		注意：控件不能放入IsPostBack中
	///	
	/// </summary>
	/// <author>unm</author>
	///	<date>2004/10/22</date>
	///	<version>1.0</version>
	///	<modify>
	///	修改为主动关注和被动关注,设定属性如下
	///	myAttention.Module = "工作信息";
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
		/// 当前使用的模块，例如：工作管理-当前工作
		/// </summary>
		public string Module
		{
			set	{	this.strModule = value;	}
		}		

		// 关注的标题
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
		/// 指定的关注是否存在
		/// </summary>
		private bool isExistAttention = false;
		private bool isExistRule = false;

		/// <summary>
		/// 关注表主键
		/// </summary>
		private string strTaskAttentionCode = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				this.CheckExist();
				this.btAttention.Text = (this.isExistAttention&&this.isExistRule)?"取消关注":"加入关注";
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 检测当前Url是否已经存在
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


		protected void btAttention_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(this.isExistAttention)
				{					
					if(this.isExistRule)
					{ // 有权则取消
						DelAttention();
						this.btAttention.Text = "加入关注";
					}
					else
					{ 
						// 无权则加入自己的关注权限
						this.SaveRS(this.strMasterCode,this.strCurUser,"","070110");
						this.btAttention.Text = "取消关注";
					}					
				}
				else
				{	
					// 加入关注记录
					this.AddAttentionRecord();
					// 加入权限,070110关注查看权限
					this.SaveRS(this.strMasterCode,this.strCurUser,"","070110");
					this.btAttention.Text = "取消关注";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		/// <summary>
		/// 删除关注主表记录
		/// </summary> 
		private void DelAttentionRecord()
		{
			EntityData entity = WBSDAO.GetTaskAttentionByCode(this.strTaskAttentionCode);
			WBSDAO.DeleteTaskAttention(entity);
			entity.Dispose();		
		}
		/// <summary>
		/// 新增关注主表记录
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
		/// 取消关注
		/// </summary>
		private void DelAttention()
		{
			//取消自己的关注权限,针对主表的070110权限
			WBSDAO.DelAttentionRule(this.strMasterCode,this.strCurUser);
			
		}

		public void AttentionProcess(string strUsers,string strStations)
		{
			try
			{
				this.CheckExist();
				if(!this.isExistAttention)
					this.AddAttentionRecord();

				// 加入权限,070110关注查看权限
				this.SaveRS(this.strMasterCode,strUsers,strStations,"070110");// 关注比较特殊，还需要使用原来的权限
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		
		/// <summary>
		/// 添加权限资源
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
