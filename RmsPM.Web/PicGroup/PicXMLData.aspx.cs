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
	/// PicXMLData ��ժҪ˵����
	/// </summary>
	public partial class PicXMLData : PageBase
	{
		/// <summary>
		/// WebĿ¼��·��
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
