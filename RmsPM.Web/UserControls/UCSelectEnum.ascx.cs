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
	///		UCSelectEnum ��ժҪ˵����
	/// </summary>
	public partial class UCSelectEnum : System.Web.UI.UserControl
	{
		#region ����
		private bool isCheckBox;
		private string strDictClass = "";
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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
				this.rbl.SelectedValue = arSel[0]; // ѡ���һ��
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

		
		#region ����
		/// <summary>
		/// ��ʾ��ʽ ��ѡ,��ѡ
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
		/// ȡ�û����趨ֵ
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
