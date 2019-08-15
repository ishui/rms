using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.PicGroup
{
	/// <summary>
	/// PicGroupModify 的摘要说明。
	/// </summary>
	public partial class PicGroupModify : PageBase
	{
	
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
				string strAct = Request.QueryString["Act"] + "";
				string strPBSPicGroupCode = Request.QueryString["PBSPicGroupCode"] + "";
				this.HideMasterType.Value = Request.QueryString["MasterType"] + "";
				this.HideMasterCode.Value = Request.QueryString["MasterCode"] + "";
				this.HidePBSPicGroupCode.Value = strPBSPicGroupCode;

				if ( "ModifyPicGroup"==strAct && ""!=strPBSPicGroupCode )
				{
					this.btnDel.Visible = true;
					this.btnUploadPic.Visible = true;
				}
				else
				{
					this.btnDel.Visible = false;
					this.btnUploadPic.Visible = false;
				}
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
				string strAct = Request.QueryString["Act"] + "";
				string strPicGroupCode = Request.QueryString["PBSPicGroupCode"] + "";

				if ( "ModifyPicGroup"!=strAct || ""==strPicGroupCode )
				{
					return;
				}

				EntityData eng = DAL.EntityDAO.PBSDAO.GetPBSPicGroupByCode( strPicGroupCode );
				if ( eng.HasRecord() )
				{
					this.HideMasterType.Value = eng.GetString("MasterType");
					this.HideMasterCode.Value = eng.GetString("MasterCode");
					this.HidePBSPicGroupCode.Value = eng.GetString("PBSPicGroupCode");
					this.TxtGroupName.Value = eng.GetString("GroupName");

					this.LoadPicList("PicGroupLarge",eng.GetString("PBSPicGroupCode"));
				}
				else
				{
					Response.Write( JavaScript.ScriptStart );
					Response.Write( JavaScript.Alert(false,"此图片集不存在！") );
					Response.Write( JavaScript.WinClose(false) );
					Response.Write( JavaScript.ScriptEnd );
				}
				eng.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 装载图片列表
		/// </summary>
		/// <param name="_MasterType">MasterType</param>
		/// <param name="_MasterCode">MasterCode</param>
		private void LoadPicList(string _MasterType, string _MasterCode)
		{
			try
			{
				PBSPicStrategyBuilder sb = new PBSPicStrategyBuilder();
			
				sb.AddStrategy( new Strategy(PBSPicStrategyName.MasterTypeEq,_MasterType) );
				sb.AddStrategy( new Strategy(PBSPicStrategyName.MasterCodeEq,_MasterCode) );
				sb.AddOrder("PBSPicCode",true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();

				EntityData enp = qa.FillEntityData("PBSPic",sql);
				qa.Dispose();

				this.dlPicList.DataSource = enp.CurrentTable;
				this.dlPicList.DataBind();
				enp.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		protected void btnSubmit_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				EntityData entity;
				DataRow dr = null;
				string strPicGroupCode = "";

				if ( ""!=this.HidePBSPicGroupCode.Value.Trim() )
				{
					strPicGroupCode = this.HidePBSPicGroupCode.Value.Trim();

					entity = DAL.EntityDAO.PBSDAO.GetPBSPicGroupByCode( this.HidePBSPicGroupCode.Value.Trim() );
					if ( entity.HasRecord() )
					{
						dr = entity.CurrentRow;
					}
				}
				else
				{
					strPicGroupCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PBSPicGroupCode");

					entity = new EntityData("PBSPicGroup");
					dr = entity.GetNewRecord();
					dr["PBSPicGroupCode"] = strPicGroupCode;
					dr["MasterType"] = this.HideMasterType.Value.Trim();
					dr["MasterCode"] = this.HideMasterCode.Value.Trim();;
					dr["PicNumber"] = 0;
					dr["CreatePerson"] = base.user.UserCode;
					dr["CreateDate"] = DateTime.Now;
				}

				dr["GroupName"] = this.TxtGroupName.Value.Trim();

				if ( ""==this.HidePBSPicGroupCode.Value.Trim() )
				{
					entity.AddNewRecord( dr );
				}

				DAL.EntityDAO.PBSDAO.SubmitAllPBSPicGroup( entity );
				entity.Dispose();

				string strURL = "./PicGroupModify.aspx?Act=ModifyPicGroup";
				strURL += "&PBSPicGroupCode=" + strPicGroupCode;
				strURL += "&ct_autoM_" + DateTime.Now.Millisecond.ToString();

				Response.Write( JavaScript.ScriptStart );
				Response.Write( JavaScript.OpenerReload(false) );
				Response.Write( JavaScript.PageTo(false,strURL) );
				Response.Write( JavaScript.ScriptEnd );
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		protected void btnDel_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				PBSPicStrategyBuilder sb = new PBSPicStrategyBuilder();
			
				sb.AddStrategy( new Strategy(PBSPicStrategyName.MasterTypeEq,"PicGroupLarge") );
				sb.AddStrategy( new Strategy(PBSPicStrategyName.MasterCodeEq,this.HidePBSPicGroupCode.Value.Trim()) );
				sb.AddOrder("PBSPicCode",true);
				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData enp = qa.FillEntityData("PBSPic",sql);
				qa.Dispose();

				DAL.EntityDAO.PBSDAO.DeletePBSPic( enp );
				enp.Dispose();

				DAL.EntityDAO.PBSDAO.DeletePBSPicGroup( DAL.EntityDAO.PBSDAO.GetPBSPicGroupByCode( this.HidePBSPicGroupCode.Value.Trim() ) );

				Response.Write( JavaScript.ScriptStart );
				Response.Write( JavaScript.OpenerReload(false) );
				Response.Write( JavaScript.WinClose(false) );
				Response.Write( JavaScript.ScriptEnd );
				return;
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
