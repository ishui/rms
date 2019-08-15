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
	///	UCPicGroup：图片组功能的实现
	///	
	///	使用方法：
	///	1、把控件拖入到指定位置。
	///	2、声明成员变量：protected RmsPM.Web.PicGroup.UCPicGroup myUCPicGroup;
	///	3、为控件赋值：
	///		this.myUCPicGroup.RootPath = "../";	当前页面相对系统Web根目录的路经
	///		this.myUCPicGroup.MasterType = "";	当前实体类型
	///		this.myUCPicGroup.MasterCode ="";	当前实体编号
	///	
	///	注意：
	///		1、同一页面不可以有多个 UCPicGroup 。
	///		2、不同的实体 MasterType 不能重复，必须仔细检查。
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
		/// 系统 Web 目录路径，必须以 "/" 结尾
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
		/// 系统 Web 目录路径，必须以 "/" 结尾
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
