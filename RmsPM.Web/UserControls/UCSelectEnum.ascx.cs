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
	/// <summary>
	///		UCSelectEnum 的摘要说明。
	/// </summary>
	public partial class UCSelectEnum : System.Web.UI.UserControl
	{
		#region 变量
		private bool isCheckBox;
		private string strDictClass = "";
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			try 
			{					
				this.LoadData();
				if(!this.IsPostBack)
					this.SetViewStateData();
				else
					SetRequestData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			EntityData entity = DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByName(this.strDictClass);
			if(this.isCheckBox)
			{
				this.cbl.Items.Clear();
				this.cbl.DataTextField = "Name";
				this.cbl.DataValueField = "DictionaryItemCode";
				this.cbl.DataSource = entity;
				this.cbl.DataBind();
			}
			else
			{
				this.rbl.Items.Clear();
				this.rbl.DataTextField = "Name";
				this.rbl.DataValueField = "DictionaryItemCode";
				this.rbl.DataSource = entity;
				this.rbl.DataBind();
			}
			entity.Dispose();		
		}

		private void SetViewStateData()
		{
			string tValue = (string)ViewState[this.UniqueID+"SelectValue"];

			if(this.isCheckBox)
			{
				if(tValue==null) return;
				foreach ( string stemp in tValue.Split(new char[]{','}))
				{
					foreach ( ListItem li in cbl.Items)
						if ( li.Value == stemp )
							li.Selected=true;
				}
			}			
			else
			{
				string[] arSel = tValue.Split(',');
				this.rbl.SelectedValue = arSel[0]; // 选择第一个
			}
		}

		private void SetRequestData()
		{
			if(this.isCheckBox)
			{
				for(int i=0;i<cbl.Items.Count;i++)
				{
					if(Request[this.cbl.UniqueID+":"+i.ToString()]=="on")
						this.cbl.Items[i].Selected = true;
				}
			}
			else
				this.rbl.SelectedValue = Request[this.rbl.UniqueID];	
		}

		private string GetSelData()
		{
			string re = "";
			if(this.isCheckBox)
			{		
				foreach ( ListItem li in cbl.Items)
				{
					if ( li.Selected )
					{
						if ( re!="")
							re+=",";
						re+=li.Value;
					}
				}				
			}
			else
				re = Request[this.rbl.UniqueID];	
			return re;
		}

		
		#region 属性
		/// <summary>
		/// 显示方式 单选,多选
		/// </summary>			
		public bool IsCheckBox
		{
			set
			{
				isCheckBox = value;
				if(isCheckBox)
					this.rbl.Visible = false;
				else
					this.cbl.Visible = true;
			}
			get
			{
				return this.isCheckBox;
			}
		}

		
		public string DictClass
		{
			set{ this.strDictClass=value; }
		}
		public string CssClass
		{
			set
			{
				this.cbl.CssClass=value; 
				this.rbl.CssClass=value;
			}
		}

		/// <summary>
		/// 取得或者设定值
		/// </summary>
		public string Value
		{
			set
			{
				ViewState[this.ClientID+"SelectValue"]=value;
				SetViewStateData();
			}
			get
			{
				return this.GetSelData();
			}
		}
		#endregion

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
