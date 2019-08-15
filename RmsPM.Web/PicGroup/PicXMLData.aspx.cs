using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.PicGroup
{
	/// <summary>
	/// PicXMLData 的摘要说明。
	/// </summary>
	public partial class PicXMLData : PageBase
	{
		/// <summary>
		/// Web目录根路径
		/// </summary>
		protected string strWebRootPath = "";

		/// <summary>
		/// /data/slideData/slides.end
		/// </summary>
		protected string strSlidesEnd = "-1";

		protected string strSlides ="";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !Page.IsPostBack )
			{
				this.IniPage();
				this.LoadData();
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
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion

		private void IniPage()
		{
			try
			{
				string server = Request.ServerVariables["server_name"];
				string dir = ConfigurationSettings.AppSettings["VirtualDirectory"].Replace("\\","");

				this.strWebRootPath = string.Format("http://{0}/{1}/", server, dir);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void LoadData()
		{
			try
			{
				string strMasterCode = Request.QueryString["MasterCode"] + "";
				string tmpstr = "";

				PBSPicStrategyBuilder sb = new PBSPicStrategyBuilder();
				sb.AddStrategy( new Strategy(PBSPicStrategyName.MasterTypeEq,"PicGroupLarge") );
				sb.AddStrategy( new Strategy(PBSPicStrategyName.MasterCodeEq,strMasterCode) );
				sb.AddOrder("PBSPicCode",true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData enp = qa.FillEntityData("PBSPic",sql);
				qa.Dispose();

				if ( enp.HasRecord() )
				{
					DataTable dt = enp.CurrentTable;
					int rCount = dt.Rows.Count;

					for(int r=0;r<rCount;r++)
					{
						DataRow dr = dt.Rows[r];

						tmpstr += "<slide>";
						tmpstr += "<images>";
						tmpstr += "<image size=\"" + dr["Length"].ToString() + "\" width=\"96\" height=\"72\">" + this.strWebRootPath + "PicGroup/PicShow.aspx?PicCode=" + dr["PBSPicCode"].ToString() + "</image>";
						tmpstr += "<image size=\"" + dr["Length"].ToString() + "\" width=\"" + dr["PicWidth"].ToString() + "\" height=\"" + dr["PicHeight"].ToString() + "\">" + this.strWebRootPath + "PicGroup/PicShow.aspx?PicCode=" + dr["PBSPicCode"].ToString() + "</image>";
						tmpstr += "</images>";
						tmpstr += "<title>";
						tmpstr += dr["PicTitle"].ToString();
						tmpstr += "</title>";
						tmpstr += "<details>";
						tmpstr += dr["PicRemark"].ToString();
						tmpstr += "</details>";
						tmpstr += "</slide>";
					}
					this.strSlidesEnd = (rCount-1).ToString();
				}

				enp.Dispose();

				this.strSlides = tmpstr;
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
	}
}
