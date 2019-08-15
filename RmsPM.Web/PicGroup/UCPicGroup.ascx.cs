namespace RmsPM.Web.PicGroup
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using Rms.Web;
	using RmsPM.DAL;
	using RmsPM.DAL.QueryStrategy;

	/// <summary>
	///	UCPicGroup��ͼƬ�鹦�ܵ�ʵ��
	///	
	///	ʹ�÷�����
	///	1���ѿؼ����뵽ָ��λ�á�
	///	2��������Ա������protected RmsPM.Web.PicGroup.UCPicGroup myUCPicGroup;
	///	3��Ϊ�ؼ���ֵ��
	///		this.myUCPicGroup.RootPath = "../";	��ǰҳ�����ϵͳWeb��Ŀ¼��·��
	///		this.myUCPicGroup.MasterType = "";	��ǰʵ������
	///		this.myUCPicGroup.MasterCode ="";	��ǰʵ����
	///	
	///	ע�⣺
	///		1��ͬһҳ�治�����ж�� UCPicGroup ��
	///		2����ͬ��ʵ�� MasterType �����ظ���������ϸ��顣
	///	
	/// </summary>
	///	<author>GODDESS</author>
	///	<date>2005-10-12</date>
	///	<version>0.1</version>
	///	
	///	
	///	<modify></modify>
	///	<description></description>
	///	<date></date>
	///	<version></version>
	///	
	public partial class UCPicGroup : System.Web.UI.UserControl
	{

		protected string strClientJS = "var arrPicGroup = new Array();";

		/// <summary>
		/// ϵͳ Web Ŀ¼·���������� "/" ��β
		/// </summary>
		protected string strRootPath = "../";

		/// <summary>
		/// PicGroup.MasterType
		/// </summary>
		protected string strMasterType = "";

		/// <summary>
		/// PicGroup.MasterCode
		/// </summary>
		protected string strMasterCode = "";


		/// <summary>
		/// ϵͳ Web Ŀ¼·���������� "/" ��β
		/// </summary>
		public string RootPath
		{
			get{return this.strRootPath;}
			set{this.strRootPath=value;}
		}

		/// <summary>
		/// PicGroup.MasterType
		/// </summary>
		public string MasterType
		{
			get{return this.strMasterType;}
			set{this.strMasterType=value;}
		}

		/// <summary>
		/// PicGroup.MasterCode
		/// </summary>
		public string MasterCode
		{
			get{return this.strMasterCode;}
			set{this.strMasterCode=value;}
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				this._IniPage();
				this._LoadData();
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


		private void _IniPage()
		{
			try
			{
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		private void _LoadData()
		{
			try
			{
				string tmpstr = "";

				PBSPicGroupStrategyBuilder sb = new PBSPicGroupStrategyBuilder();
				
				if ( ""!=this.strMasterType )
				{
					sb.AddStrategy( new Strategy(PBSPicGroupStrategyName.MasterTypeEq,this.strMasterType) );
				}

				if ( ""!=this.strMasterCode )
				{
					sb.AddStrategy( new Strategy(PBSPicGroupStrategyName.MasterCodeEq,this.strMasterCode) );
				}

				sb.AddOrder("PBSPicGroupCode",false);

				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();

				EntityData eng = qa.FillEntityData("PBSPicGroup",sql);
				qa.Dispose();
				if ( eng.HasRecord() )
				{
					#region --- Build Client JavaScript Array String ---------------------------------------------------------------

					int rCount = eng.CurrentTable.Rows.Count;

					tmpstr = "arrPicGroup = [";

					for(int r=0;r<rCount;r++)
					{
						if ( 0!=r )
						{
							tmpstr += ",";
						}

						tmpstr += "['" + eng.CurrentTable.Rows[r]["PBSPicGroupCode"].ToString() + "',[";

						#region --- Get Pic Item ---------------------------------------------------------------

						PBSPicStrategyBuilder sbp = new PBSPicStrategyBuilder();
						sbp.AddStrategy( new Strategy(PBSPicStrategyName.MasterTypeEq,"PicGroupLarge") );
						sbp.AddStrategy( new Strategy(PBSPicStrategyName.MasterCodeEq,eng.CurrentTable.Rows[r]["PBSPicGroupCode"].ToString()) );
						sbp.AddOrder("PBSPicCode",true);

						string sqlp = sbp.BuildMainQueryString();

						QueryAgent qap = new QueryAgent();
						EntityData enp = qa.FillEntityData("PBSPic",sqlp);
						qap.Dispose();
						int tmpNum = enp.CurrentTable.Rows.Count;

						eng.CurrentTable.Rows[r]["PicNumber"] = tmpNum;

						if ( 0<tmpNum )
						{
							for(int x=0;x<tmpNum;x++)
							{
								if ( 0!=x )
								{
									tmpstr += ",";
								}

								tmpstr += "['" + enp.CurrentTable.Rows[x]["PBSPicCode"].ToString() + "'," + enp.CurrentTable.Rows[x]["PicWidth"].ToString() + "," + enp.CurrentTable.Rows[x]["PicHeight"].ToString() + "]";
							}
						}

						enp.Dispose();

						#endregion ---------------------------------------------------------------

						tmpstr += "]]";
					}

					tmpstr += "];";

					this.strClientJS += "\r\n\r\n" + tmpstr;

					#endregion ---------------------------------------------------------------

					this.dgList.DataSource = eng.CurrentTable;
					this.dgList.DataBind();
				}
				eng.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
