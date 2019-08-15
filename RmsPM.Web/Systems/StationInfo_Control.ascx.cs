namespace RmsPM.Web.Systems
{
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
	using RmsPM.DAL.EntityDAO;
	using RmsPM.DAL.QueryStrategy;

	/// <summary>
	///		StationInfo_Control 的摘要说明。
	/// </summary>
	/// public class StationInfo_Control : System.Web.UI.UserControl
	public partial class StationInfo_Control : RmsPM.Web.Components.BaseControl
	{
		protected System.Web.UI.HtmlControls.HtmlTable table5;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnModify0;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnDelete0;
		protected System.Web.UI.HtmlControls.HtmlTable table9;	
		//protected System.Web.UI.WebControls.Panel Pl_StShow;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnClose0;
		//protected string StationCode



		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			
			if (!IsPostBack)
			{
				DefaultSet();
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
			this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);

		}
		#endregion	
		#region 索引器
		protected string StationCode
		{
			get
			{
				return Request["StationCode"] + "";
			}			
		}
		protected string UnitCode
		{
			get
			{
				return Request["UnitCode"] + "";
			}
		} 
		#endregion
		#region 初始,及共用函数
		private void DefaultSet()
		{
			ShowMode();
			AllowChange();
			btnSave.Visible=false;
			btnClose.Visible=false;
		}
		private void EditMode()
		{
			Pl_StShow.Visible = false;
			Pl_StEdit.Visible = true;
			configUser.Visible = false;
			IniPage();
			LoadEditData();
			
		}
		private void ShowMode()
		{
			Pl_StShow.Visible = true;
			Pl_StEdit.Visible = false;
			configUser.Visible = true;
			LoadData();
		}
		private void AllowChange()
		{
			if(UnitCode=="")
			{
				btnModify.Visible=false;
				configUser.Visible=false;
				btnDelete.Visible=false;
			}
		}
		#endregion


		#region 加载数据
		private void IniPage()
		{
			this.txtRefreshScript.Value = Request["RefreshScript"] + "";
			try 
			{
				string UnitCode = Request.QueryString["UnitCode"];
				string fullCode = DAL.EntityDAO.OBSDAO.GetUnitFullCode(UnitCode);
				this.txtStationCode.Value = Request.QueryString["StationCode"];
				this.txthUnit.Value = UnitCode;
				this.txtUnitName.Value = BLL.SystemRule.GetUnitName(UnitCode);
				EntityData units = DAL.EntityDAO.OBSDAO.GetAllUnit();
				foreach ( string tempCode in fullCode.Split(new char[]{'-'}))
				{
					if ( tempCode != "" )
					{
						DataRow[] drsSelect = units.CurrentTable.Select( String.Format("UnitCode='{0}'" ,tempCode ));
						if ( drsSelect.Length>0)
						{
							this.sltAccessRangeUnit.Items.Add( new ListItem( (string)drsSelect[0]["UnitName"],(string)drsSelect[0]["UnitCode"] ));
						}
					}
				}
				units.Dispose();
				BLL.PageFacade.LoadAllRoleSelect(this.sltRole,"");

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载页面错误。"));
			}
		}

		private void LoadData()
		{
			try
			{
				bool isNew = (StationCode=="");
				EntityData ds = null;

				if ( isNew )
				{
					ds = new EntityData("Standard_Station");
					DataRow dr = ds.GetNewRecord();
					dr["StationCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("StationCode");
					dr["UnitCode"] = this.txthUnit.Value;
					ds.AddNewRecord(dr);
					ds.SetCurrentRow(0);
				}
				else
					ds = OBSDAO.GetStandard_StationByCode(StationCode);		
			
				if (ds.HasRecord())
				{
					this.lblStationName.Text=ds.GetString("StationName");
					this.lblDescription.Text = ds.GetString("Description");
					this.lblUnit.Text = BLL.SystemRule.GetUnitName(ds.GetString("UnitCode"));
					this.lblRoleName.Text = BLL.SystemRule.GetRoleName(ds.GetString("RoleCode"));
					int roleLevel = ds.GetInt("RoleLevel");
					//Session["roleLevel"]=roleLevel;
					ViewState["roleLevel"]=roleLevel;
					string accessRangeUnitCode = ds.GetString("AccessRangeUnitCode");
					if ( roleLevel == 0 )
						this.lblRoleLevelName.Text ="集团";
					else if ( roleLevel == 4 )
						this.lblRoleLevelName.Text = "个人";
					else
						this.lblRoleLevelName.Text = "部门";

					//this.lblRoleLevelName.Text = BLL.SystemRule.GetUnitName(accessRangeUnitCode);

				}
				this.txtUnitName.Value = BLL.SystemRule.GetUnitName(this.txthUnit.Value);

				//记录当前的用户代码
				this.txtReturnUserCodes.Value = BLL.ConvertRule.Concat(ds.Tables["UserRole"], "UserCode", ",");				
				this.dgList.DataSource=ds.Tables["UserRole"];
				this.dgList.DataBind();
				Session["StationEntityData"] = ds;							
				ds.Dispose();			
				//entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错"));
			}
		}

		private void LoadEditData()
		{
			EntityData ds = (EntityData)Session["StationEntityData"];
			if (ds.HasRecord())
			{
				this.txtStationName.Text = ds.GetString("StationName");
				this.txtDescription.Text = ds.GetString("Description");
				this.txthUnit.Value = ds.GetString("UnitCode");
				try
				{
					this.rblRoleLevel.SelectedValue = ds.GetInt("RoleLevel").ToString();
				}
				catch
				{}
				this.sltRole.Value = ds.GetString("RoleCode");
				this.sltAccessRangeUnit.Value = ds.GetString("AccessRangeUnitCode");
			}
		}
		private void DataBindDataList(DataTable dt)
		{
			this.dgList.DataSource=dt;
			this.dgList.DataBind();
		}
		#endregion 
		#region 更新 	
		private void SaveUserStationData()
		{
			try
			{
				EntityData ds = (EntityData)Session["StationEntityData"];
				//string stationCode=ds.GetString("StationCode");
				//string unitCode = this.txthUnit.Value;
				/*DataRow dr = ds.CurrentRow;
				dr["UnitCode"] = UnitCode;
				dr["StationName"] = this.lblStationName.Text;
				dr["RoleCode"] = this.sltRole.Value;
				dr["Description"] = this.lblDescription.Text;
				int roleLevel = int.Parse(ViewState["roleLevel"].ToString());
				dr["RoleLevel"] = roleLevel;
				if ( roleLevel == 3 )
				{
					dr["AccessRangeUnitCode"]=this.sltAccessRangeUnit.Value;
					dr["AccessRangeUnitFullCode"]=DAL.EntityDAO.OBSDAO.GetUnitFullCode(this.sltAccessRangeUnit.Value);
				}
				else
				{
					dr["AccessRangeUnitCode"]="";
					dr["AccessRangeUnitFullCode"]="";
				}*/

				DAL.EntityDAO.OBSDAO.SubmitAllStandard_Station(ds);
				Session["StationEntityData"] = null;
				ds.Dispose();				
			}
			catch( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错"));
				return;
			}
	
		//	WriteRefreshScript( StationCode  );
		}
		/// <summary>
		/// 更新岗位用户信息
		/// </summary>
		private void SaveStationData()
		{
			
			if( this.txtStationName.Text.Trim() == "" )
			{
				Response.Write(Rms.Web.JavaScript.Alert( true, "请输入岗位名称 ！" ));
				return;
			}

			if ( this.txthUnit.Value.Trim() == "" )
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"请选择部门 ！"));
				return;
			}
			string stationCode = "";

			try
			{
				EntityData ds = (EntityData)Session["StationEntityData"];
				stationCode=ds.GetString("StationCode");
				string unitCode = this.txthUnit.Value;
				DataRow dr = ds.CurrentRow;
				dr["UnitCode"] = unitCode;
				dr["StationName"] = this.txtStationName.Text;
				dr["RoleCode"] = this.sltRole.Value;
				dr["Description"] = this.txtDescription.Text;
				int roleLevel = int.Parse( this.rblRoleLevel.SelectedValue);
				dr["RoleLevel"] = roleLevel;
				if ( roleLevel == 3 )
				{
					dr["AccessRangeUnitCode"]=this.sltAccessRangeUnit.Value;
					dr["AccessRangeUnitFullCode"]=DAL.EntityDAO.OBSDAO.GetUnitFullCode(this.sltAccessRangeUnit.Value);
				}
				else
				{
					dr["AccessRangeUnitCode"]="";
					dr["AccessRangeUnitFullCode"]="";
				}

				DAL.EntityDAO.OBSDAO.SubmitAllStandard_Station(ds);
				Session["StationEntityData"] = null;
				ds.Dispose();
			}
			catch( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错"));
				return;
			}
	
			WriteRefreshScript( stationCode  );
		}
		#endregion

		private void WriteRefreshScript( string StationCode  ) 
		{

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() != "") 
			{
				Response.Write( "window.opener." + this.txtRefreshScript.Value.Trim());
				Response.Write(Rms.Web.JavaScript.WinClose(false));
			}
			else 
			{
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
			}
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}


		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			SaveUserStationData();
			DefaultSet();
			btnClose.Visible=false;
			btnSave.Visible=false;
		}

		protected void btnRefreshConfigUser_ServerClick(object sender, System.EventArgs e)
		{
			string[] codes = this.txtReturnUserCodes.Value.Split(new char[]{','});

			try
			{

				EntityData ds = (EntityData)Session["StationEntityData"];

				//清空原表记录
				int c = ds.Tables["UserRole"].Rows.Count;
				for(int i=c-1;i>=0;i--)
				{
					ds.Tables["UserRole"].Rows[i].Delete();
				}

				string StationCode=ds.GetString("StationCode");

				foreach ( string code in codes )
				{
					if ( code!="")
					{
						if ( ds.Tables["UserRole"].Select(String.Format( "UserCode='{0}'" ,code)).Length==0)
						{
							DataRow newDr = ds.GetNewRecord("UserRole");
							newDr["UserRoleCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("UserRoleCode");
							newDr["UserCode"]=code;
							newDr["StationCode"]=StationCode;
							ds.AddNewRecord(newDr,"UserRole");
						}
					}
				}

				//记录当前的用户代码
				this.txtReturnUserCodes.Value = BLL.ConvertRule.Concat(ds.Tables["UserRole"], "UserCode", ",");

				this.dgList.DataSource=ds.Tables["UserRole"];
				this.dgList.DataBind();
				Session["StationEntityData"] = ds;
				ds.Dispose();
				btnSave.Visible=true;
				btnClose.Visible=true;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错"));
			}
		}

		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				EntityData ds = (EntityData)Session["StationEntityData"];
				string userCode = e.Item.Cells[0].Text;
				foreach ( DataRow dr in ds.Tables["UserRole"].Select(String.Format( "UserCode='{0}'" ,userCode)))
				{
					dr.Delete();
				}

				//记录当前的用户代码
				this.txtReturnUserCodes.Value = BLL.ConvertRule.Concat(ds.Tables["UserRole"], "UserCode", ",");

				this.dgList.DataSource=ds.Tables["UserRole"];
				this.dgList.DataBind();
				Session["StationEntityData"] = ds;
				ds.Dispose();
				btnSave.Visible=true;
				btnClose.Visible=true;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错"));
			}
		
		}

		protected void btnModify_ServerClick(object sender, System.EventArgs e)
		{
			sltRole.Items.Clear();
			sltAccessRangeUnit.Items.Clear();
			EditMode();
		}			

		protected void Bt_SaveStation_ServerClick(object sender, System.EventArgs e)
		{
			SaveStationData();
			DefaultSet();
		}

		protected void Bt_stationCancel_ServerClick(object sender, System.EventArgs e)
		{
			ShowMode();
		}

		protected void btnClose_ServerClick(object sender, System.EventArgs e)
		{
			DefaultSet();
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				bool canDelete = true;
				EntityData users = BLL.SystemRule.GetUserByStation(StationCode);
				if ( users.HasRecord())
					canDelete = false;
				users.Dispose();

				if ( canDelete )
				{

					EntityData entity = DAL.EntityDAO.OBSDAO.GetStationByCode(StationCode);
					DAL.EntityDAO.OBSDAO.DeleteStation(entity);
					entity.Dispose();
					//WriteRefreshScript();
					DefaultSet();
				}
				else
				{
					Response.Write(Rms.Web.JavaScript.Alert( true,"这个岗位还配置有人员，不能删除" ));
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除失败"));
			}

		}
	}

}
