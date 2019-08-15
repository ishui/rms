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
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;
using RmsPM.Web;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Remind
{
	/// <summary>
	///公告发布、修改
	/// </summary>
	public partial class Notify : PageBase
	{
//		protected AspWebControl.Calendar dtbShowDate;
		private string strAction = "";
		private DataTable dt = new DataTable();
		private string strNoticeCode = "";
	    
		protected void Page_Load(object sender, System.EventArgs e)
		{   
            

			try
			{
               //Response.Write("<script>window.opener.location.reload();</script>");
                
				InitPage();
				if(!this.IsPostBack)
				{
					LoadData();			
				}
				////<%RmsPM.BLL.SystemRule.GetUserName(DataBinder.Eval(Container.DataItem, "{0}").ToString())%>
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
               // Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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

		private void InitPage()	
		{
			strAction = Request.QueryString["Action"] + "";
			this.strNoticeCode = Request.QueryString["Code"] + "";					

			this.myAttachMentAdd.AttachMentType = "NoticeAttachMent"; 
 			this.myAttachMentAdd.MasterCode = this.strNoticeCode;
			if(this.strAction=="Modify")
			{
				// 在此检查是否有权限修改// 080102为通知修改权限
				User myUser = new User(user.UserCode);		
				//if(!myUser.HasResourceRight(this.strNoticeCode,"080102"))
					//Server.Transfer("../Remind/NoticeInfo.aspx?&Code="+this.strNoticeCode);

				//this.btDelete.Visible = myUser.HasOperationRight("080103");// 080103为通知删除权限
				try
				{
					QueryAgent qa = new QueryAgent();
					DAL.QueryStrategy.RoleOperation sb = new RmsPM.DAL.QueryStrategy.RoleOperation();
					sb.AddStrategy( new Strategy( RoleOperationName.UserCode,user.UserCode));
					//sb.AddStrategy(new Strategy( RoleOperationName.UserCode,"080102"));
					string sql = sb.BuildMainQueryString();
					DataSet Ds = qa.ExecSqlForDataSet(sql);
					string str_Edit = "";
					string str_Delete = "";
					for (int i = 0;i < Ds.Tables[0].Rows.Count;i++)
					{
						if (Ds.Tables[0].Rows[i][1].ToString() == "080102")
						{
							str_Edit = Ds.Tables[0].Rows[i][1].ToString();
						}
						if (Ds.Tables[0].Rows[i][1].ToString() == "080103")
						{
							str_Delete = Ds.Tables[0].Rows[i][1].ToString();
						}

					}
                   // if (str_Edit == "" && !myUser.HasResourceRight(this.strNoticeCode, "080102"))
                    //{
                    Server.Transfer("../Remind/NoticeInfo.aspx?&Code=" + this.strNoticeCode + "&Action=" + strAction);
                    //}
                    //else
                    //{
                      //  Server.Transfer("../Remind/noticeupdateinfo.aspx?&Code=" + this.strNoticeCode);
                    //}
                        //if (str_Delete == "" && !myUser.HasResourceRight(this.strNoticeCode, "080103"))
                        //{
                        //    this.btDelete.Visible = myUser.HasOperationRight("080103");// 080103为通知删除权限
                        //    this.btDelete.Visible = false;
                        //}
                        //else
                        //{
                        //    this.btDelete.Visible = true;
                        //}
					
					
				}
				catch(Exception dd)
				{
					string h = dd.Message.ToString();
					string f = h;
				}
			}
			else
				this.btDelete.Visible = false;
			string tmp = this.txtUsers.Value;
		}

		private void LoadData()
		{
            //对通知标题的个性化判断
            if (this.up_sPMNameLower != "tianyangoa")
            {   
                this.trNotice.Visible = false;
            }
            else
            {
               
                if (Request["DocType"].ToString() == "99")
                {
                    this.trNotice.Visible = false;
                }
                else
                {
                    this.trNotice.Visible = true;
                }
                PageFacade.LoadDictionarySelect(this.DDLNoticeClass, "通知类型", "");//通知类别改成通知类型
            }
           
            switch (strAction.ToUpper())
			{
				case "INSERT":
					this.lblTitle.Text = "新增公告";
                    this.htType.Value = Request["DocType"].ToString();
					break;					

				case "MODIFY":
					//标题
					this.lblTitle.Text = "修改公告";
					EntityData entityNotice = RemindDAO.GetNoticeByCode(this.strNoticeCode);
                    this.DDLNoticeClass.Value = System.Web.HttpUtility.HtmlEncode(entityNotice.GetString("NoticeClass"));//测试新增代码
					this.txtTitle.Value = System.Web.HttpUtility.HtmlEncode(entityNotice.GetString("Title"));
					this.taContent.Value = StringRule.FormartOutput(entityNotice.GetString("Content"));
                    //this.htType.Value = entityNotice.GetString("Type");
					this.LoadUser(entityNotice.GetString("IsAll"));
					break;
			}
		}


		private void LoadUser(string strIsAll)
		{
			if(strIsAll=="0") //部分人员
			{
				string strUsers = "";
				string strUserNames = "";
				string strStations = "";
				string strStationNames = "";
				BLL.ResourceRule.GetAccessRange(this.strNoticeCode,"0801","080104",ref strUsers,ref strUserNames,ref strStations,ref strStationNames);
				this.txtUsers.Value = BLL.StringRule.CutRepeat(strUsers);
				this.SelectName.InnerText = BLL.StringRule.CutRepeat(strUserNames);
				this.txtStations.Value = BLL.StringRule.CutRepeat(strStations);
				this.SelectName.InnerText+= "，"+BLL.StringRule.CutRepeat(strStationNames);
			}
			else
			{
				this.txtUsers.Value = this.GetAllUser();
				this.SelectName.InnerText = "全体人员";
			}

			// 转入使用统一权限处理
//			EntityData entityUser = RemindDAO.GetNoticeUserByNoticeCode(this.strNoticeCode);
//			if(entityUser.HasRecord())
//			{
//				for(int i =0;i<entityUser.CurrentTable.Rows.Count;i++)
//				{
//					if(entityUser.CurrentTable.Rows[i]["NoticeType"].ToString()=="1") // 1是通知人
//					{
//						this.txtUsers.Value += (this.txtUsers.Value.Length>0)?",":"";
//						this.txtUsers.Value += entityUser.CurrentTable.Rows[i]["NoticeObject"].ToString();
//						this.SelectName.InnerText += (this.SelectName.InnerText.Length>0)?",":"";
//						this.SelectName.InnerText += BLL.SystemRule.GetUserName(entityUser.CurrentTable.Rows[i]["NoticeObject"].ToString());
//					}
//					if(entityUser.CurrentTable.Rows[i]["NoticeType"].ToString()=="2") // 2是通知岗位
//					{
//						this.txtDepts.Value += (this.txtDepts.Value.Length>0)?",":"";
//						this.txtDepts.Value += entityUser.CurrentTable.Rows[i]["NoticeObject"].ToString();
//						this.SelectName.InnerText += (this.SelectName.InnerText.Length>0)?",":"";
//						this.SelectName.InnerText += BLL.SystemRule.GetUnitName(entityUser.CurrentTable.Rows[i]["NoticeObject"].ToString());
//					}
//				}
//			}
		}

	
		protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{   
			try
			{
                if (strAction == "Insert")
                {
                    if (SaveData())
                    {
                        //if(strAction=="Modify") UpdataData();
                        this.SaveToolsButton.Visible = false;
                        this.CancelToolsButton.Value = "取消";

                        Response.Write(JavaScript.ScriptStart);
                        Response.Write("window.opener.refresh();");
                        Response.Write("window.close();");
                        Response.Write(JavaScript.ScriptEnd);
                    }
                    else
                    {
                         Response.Write(Rms.Web.JavaScript.Alert(true, "类型不能为空，标题不能为空，内容不能问空！"));
                    }
                }
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存公告失败");
			}
		}

		//保存数据
		private bool SaveData()
		{
            this.htType.Value = Request["DocType"].ToString();

            //保存通知				
			EntityData entityNotice = new EntityData("Standard_Notice");
			DataRow drNotice = entityNotice.GetNewRecord();	
			this.strNoticeCode = SystemManageDAO.GetNewSysCode("Notice");
			
             if (this.txtTitle.Value.Trim() == "")
              {
                  return false;
              }
             if (this.taContent.Value.Trim() == "")
              {
                  return false;
              }
            //新加通知类型（测试正确后删除此注释）
            if (this.up_sPMNameLower == "tianyangoa")
            {               
                
                if (this.htType.Value == "1")
                {
                    if (this.DDLNoticeClass.Value.Trim() == string.Empty )
                    {
                        return false;
                    }
                    drNotice["NoticeClass"] = this.DDLNoticeClass.Value.Trim();
                }
               
            }            
            drNotice["NoticeCode"] = this.strNoticeCode;
            drNotice["Title"] = this.txtTitle.Value.Trim();
            //drNotice["EnableDate"] = this.dtbShowDate.Value;
			drNotice["SubmitPerson"] = base.user.UserCode;
			drNotice["SubmitDate"] = DateTime.Now;
            drNotice["UpdateDate"] = DateTime.Now;
            drNotice["UserCode"] = base.user.UserCode;
            drNotice["Content"] = this.taContent.Value;
            
            
            if (this.htType.Value != "")
            {
                drNotice["Type"] = this.htType.Value;
            }
            entityNotice.AddNewRecord(drNotice);

			
//			//保存操作用户 //如果未选择通知用户，面向全体用户发布
//			string strUser = this.txtUsers.Value.Trim();
//			if(strUser.Length>0)
//				this.SaveUser(strUser);
//			// 保存分发岗位
//			string strDept = this.txtDepts.Value.Trim();
//			if(strDept.Length>0)
//				this.SaveDept(strDept);

			// 保存资源，保存权限			
			string strUser = this.txtUsers.Value.Trim();
			string strStation = this.txtStations.Value.Trim();
			// 没有选择人员则向全体发布
			if(strUser.Length<1&&strStation.Length<1)
			{
				strUser = this.GetAllUser();
				drNotice["IsAll"] = "1";
			}
			else
			{
				drNotice["IsAll"] = "0";
			}
            drNotice["status"] = "1";

			ArrayList arOperator = new ArrayList();
			this.SaveRS(arOperator,BLL.StringRule.CutRepeat(strUser),BLL.StringRule.CutRepeat(strStation),"080104"); // 一般通知查看权限
			this.SaveRS(arOperator,base.user.UserCode,"","080102,080103,080104"); // 修改和删除
			if(arOperator.Count>0)  
				BLL.ResourceRule.SetResourceAccessRange(this.strNoticeCode,"0801","",arOperator,false);
			
			RemindDAO.InsertNotice(entityNotice);
			entityNotice.Dispose();

			// 保存附件
			this.myAttachMentAdd.SaveAttachMent(this.strNoticeCode);

			this.btDelete.Visible = false;
			return true;
		
		}

		private string GetAllUser()
		{
			string strUser = "";
			EntityData entity = SystemManageDAO.GetAllSystemUser();
			DataView dv = new DataView(entity.CurrentTable,"isnull(Status,0)='0'","",System.Data.DataViewRowState.CurrentRows);
			foreach(DataRowView drv in dv)
			{
				if(strUser!="") strUser+=",";
				strUser+=drv["UserCode"].ToString();
			}
			return strUser;
		}


		/// <summary>
		/// 添加权限资源
		/// </summary>
		private void SaveRS(ArrayList arOperator,string strUser,string strStation,string strOption)
		{		

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
					acRang.RelationCode = strTStation;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}
		}


		private DataTable ChectRepeat()
		{
			dt = (DataTable)ViewState["SelectedUser"];
//			string tmp = ","+this.hUserCode.Value+",";
//			for(int i=0;i<dt.Rows.Count;i++)
//			{
//				if(tmp.IndexOf(","+dt.Rows[i]["UserCode"].ToString()+",")>0)
//					dt.Rows.Remove(dt.Rows[i]);
//			}
			if(dt==null) dt = new DataTable();
			return dt;
		}

		//更新数据
		private bool UpdataData()
		{	
			//保存通知
			EntityData entityNotice = RemindDAO.GetNoticeByCode(this.strNoticeCode);
			DataRow dr;
			if(entityNotice.HasRecord())
			{
				dr = entityNotice.CurrentRow;
				dr["NoticeCode"] = this.strNoticeCode;
                dr["NoticeClass"] = this.DDLNoticeClass.Value.Trim();//新加通知类型（测试正确后删除此注释）
				dr["Title"] = this.txtTitle.Value.Trim();
				//dr["EnableDate"] = this.dtbShowDate.Value;
				dr["SubmitPerson"] = base.user.UserCode;
				dr["SubmitDate"] = DateTime.Now.ToShortDateString();
				dr["Content"] = this.taContent.Value;

				// 保存资源，保存权限			
				string strUser = this.txtUsers.Value.Trim();
				string strStation = this.txtStations.Value.Trim();
				// 没有选择人员则向全体发布
				if(strUser.Length<1&&strStation.Length<1)
				{
					strUser = this.GetAllUser();
					dr["IsAll"] = "1";
				}
				else
				{
					dr["IsAll"] = "0";
				}
				ArrayList arOperator = new ArrayList();
				this.SaveRS(arOperator,BLL.StringRule.CutRepeat(strUser),BLL.StringRule.CutRepeat(strStation),"080104"); // 一般通知查看权限
				this.SaveRS(arOperator,base.user.UserCode,"","080102,080103,080104"); // 修改和删除
				if(arOperator.Count>0)  
					BLL.ResourceRule.SetResourceAccessRange(this.strNoticeCode,"0801","",arOperator,false);

			}

//			// 待保存新选择的用户范围//如果未选择通知用户，面向全体用户发布
//			string strUser = this.txtUsers.Value;
//			if(strUser.Length>0)
//				this.SaveUser(strUser);
//			// 保存分发岗位
//			string strDept = this.txtDepts.Value;
//			if(strDept.Length>0)
//				this.SaveDept(strDept);
//			// 保存资源，保存权限			
//			string strUser = this.txtUsers.Value.Trim();
//			string strStation = this.txtStations.Value.Trim();
//			this.SaveRS(strUser,strStation,"080104");
				

			RemindDAO.UpdateNotice(entityNotice);
			entityNotice.Dispose();

			// 保存附件
			this.myAttachMentAdd.SaveAttachMent(this.strNoticeCode);

			return true;
		}



		protected void btDelete_ServerClick(object sender, System.EventArgs e)
		{			
            //try
            //{
            //    // 删除附件
            //    this.myAttachMentAdd.DelAttachMentByMasterCode(this.strNoticeCode);
            //    // 删除分发范围
            //    EntityData entityUser = RemindDAO.GetNoticeUserByNoticeCode(this.strNoticeCode);
            //    if(entityUser.HasRecord())
            //    {
            //        RemindDAO.DeleteNoticeUser(entityUser);
            //    }
            //    entityUser.Dispose();

            //    // 是否添加入删除资源和权限的操作

            //    EntityData entityNotice = RemindDAO.GetNoticeByCode(strNoticeCode);
            //    RemindDAO.DeleteNotice(entityNotice);

            Response.Write(JavaScript.ScriptStart);
            Response.Write("window.opener.refresh();");
            Response.Write("window.close();");
            Response.Write(JavaScript.ScriptEnd);
            //}
            //catch (Exception ex)
            //{
            //    ApplicationLog.WriteLog(this.ToString(),ex,"删除失败");
            //}
		}
	}
}

//		// 保存分发人员
//		private void SaveUser(string strUser)
//		{
//			string[] arUser = strUser.Split(',');
//			EntityData entityUser = RemindDAO.GetNoticeUserByNoticeCode(this.strNoticeCode);
//			RemindDAO.DeleteNoticeUser(entityUser);
//			foreach(string strTmp in arUser)
//			{
//				DataRow dr = entityUser.GetNewRecord();
//				dr["NoticeUserCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("NoticeUser");
//				dr["NoticeCode"] = this.strNoticeCode;
//				dr["NoticeType"] = "1";
//				dr["NoticeObject"] = strTmp;
//				entityUser.AddNewRecord(dr);
//				RemindDAO.InsertNoticeUser(entityUser);
//			}
//			this.txtUsers.Value = "";
//			this.SelectName.InnerText = "";
//		}
//
//		// 保存分发岗位
//		private void SaveDept(string strDept)
//		{
//			string[] arDept = strDept.Split(',');
//			EntityData entityUser = RemindDAO.GetNoticeUserByNoticeCode(this.strNoticeCode);
//			RemindDAO.DeleteNoticeUser(entityUser);
//			foreach(string strTmp in arDept)
//			{
//				DataRow dr = entityUser.GetNewRecord();
//				dr["NoticeUserCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("NoticeUser");
//				dr["NoticeCode"] = this.strNoticeCode;
//				dr["NoticeType"] = "2";
//				dr["NoticeObject"] = strTmp;
//				entityUser.AddNewRecord(dr);
//				RemindDAO.InsertNoticeUser(entityUser);
//			}
//			this.txtDepts.Value = "";
//			this.SelectName.InnerText = "";
//		}
#region 以前的代码
//
//using System;
//using System.Collections;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Web;
//using System.Web.SessionState;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls;
//using Rms.ORMap;
//using Rms.Web;
//using RmsPM.DAL.EntityDAO;
//using RmsPM.BLL;
//using RmsPM.Web;
//using RmsPM.DAL.QueryStrategy;
//
//namespace RmsPM.Web.Remind
//{
//	/// <summary>
//	///通知发布、修改
//	/// </summary>
//	public class Notify : PageBase
//	{
//		protected System.Web.UI.WebControls.Label lblTitle;
//		protected System.Web.UI.HtmlControls.HtmlInputButton SaveToolsButton;
//		protected System.Web.UI.HtmlControls.HtmlTextArea taContent;
//		protected AspWebControl.Calendar dtbShowDate;
//		protected System.Web.UI.HtmlControls.HtmlInputText txtTitle;
//		protected System.Web.UI.WebControls.CustomValidator CVTitle;
//		protected System.Web.UI.HtmlControls.HtmlInputHidden hUserFlag;
//		protected System.Web.UI.HtmlControls.HtmlInputHidden hUserCode;
//		protected System.Web.UI.HtmlControls.HtmlInputButton CancelToolsButton;
//		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddNewUser;
//		private string strAction = "";
//		protected System.Web.UI.HtmlControls.HtmlInputButton btDelete;
//		protected System.Web.UI.WebControls.TextBox txtTmpSelectUser;
//		private DataTable dt = new DataTable();
//	
//		private void Page_Load(object sender, System.EventArgs e)
//		{
//			try
//			{
//				// 在此处放置用户代码以初始化页面
//				
//			
//				InitPage();
//				if(!this.IsPostBack)
//				{
//					LoadData();			
//				}
//				////<%RmsPM.BLL.SystemRule.GetUserName(DataBinder.Eval(Container.DataItem, "{0}").ToString())%>
//			}
//			catch (Exception ex)
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"保存通知失败");
//			}
//		}
//
//		#region Web 窗体设计器生成的代码
//		override protected void OnInit(EventArgs e)
//		{
//			//
//			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
//			//
//			InitializeComponent();
//			base.OnInit(e);
//		}
//		
//		/// <summary>
//		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
//		/// 此方法的内容。
//		/// </summary>
//		private void InitializeComponent()
//		{    
//			this.SaveToolsButton.ServerClick += new System.EventHandler(this.SaveToolsButton_ServerClick);
//			this.btDelete.ServerClick += new System.EventHandler(this.btDelete_ServerClick);
//			this.Load += new System.EventHandler(this.Page_Load);
//
//		}
//		#endregion
//
//		private void InitPage()	
//		{
//			strAction = Request.QueryString["Action"] + "";
//			if(!this.IsPostBack)
//			{
//				dt.Columns.Add("UserCode");	
//			}
//			else
//			{
//				dt = (DataTable)ViewState["SelectedUser"];
//			}
//			if(this.hUserFlag.Value.Trim()=="1"&&this.hUserCode.Value.Length>0)
//			{
//				if(dt==null)
//				{
//					dt = new DataTable();
//					dt.Columns.Add("UserCode");	
//				}
//				ViewState["SelectedUser"] = null;
//				dt.Rows.Clear();
//				string[] arUserList = this.hUserCode.Value.Split(',');
//				for(int i = 0; i<arUserList.Length; i++)
//				{
//					DataRow dr = dt.NewRow();
//					dr["UserCode"] = arUserList[i];
//					dt.Rows.Add(dr);
//				}
//				ViewState["SelectedUser"] = dt;
//				this.dgUserList.DataSource = dt.DefaultView ;
//				this.dgUserList.DataBind();
//			}
//		}
//
//		private void LoadData()
//		{						
//			string Code = Request.QueryString["Code"] + "";
//
//			switch (strAction.ToUpper())
//			{
//				case "INSERT":
//					this.lblTitle.Text = "新增通知";
//					break;					
//
//				case "MODIFY":
//					//标题
//					this.lblTitle.Text = "修改通知";
//					EntityData entityNotice = RemindDAO.GetStandard_NoticeByCode(Code);
//					entityNotice.SetCurrentTable("Notice");
//					if ( entityNotice.HasRecord())
//					{
//						this.txtTitle.Value = entityNotice.GetString("Title");
//						//this.txtHref.Value = entityNotice.GetString("RelatedHref");
//						this.taContent.Value = entityNotice.GetString("Content");
//					}
//
//					entityNotice.SetCurrentTable("NoticeAttachment");
//					if( entityNotice.HasRecord())
//					{
//						this.txtAttachMent.Value = entityNotice.GetString("FileName");
//					}
//
//					entityNotice.SetCurrentTable("NoticeUser");
//					if(entityNotice.HasRecord())
//					{						
//						foreach(DataRow dr2 in entityNotice.CurrentTable.Rows)
//						{
//							DataRow dr = dt.NewRow();
//							dr["UserCode"] = dr2["UserCode"].ToString();
//							dt.Rows.Add(dr);
//						}					
//					}							
//					this.dgUserList.DataSource = dt;
//					this.dgUserList.DataBind();
//					ViewState["SelectedUser"] = dt;
//					entityNotice.Dispose();
//					break;
//			}
//		}
//
//	
//		private void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
//		{
//			try
//			{
//				if(strAction=="Insert") SaveData();
//				if(strAction=="Modify") UpdataData();
//				this.SaveToolsButton.Visible = false;
//				this.CancelToolsButton.Value = "关闭";
//			}
//			catch (Exception ex)
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"保存通知失败");
//			}
//		}
//
//		//保存数据
//		private bool SaveData()
//		{
//			string Code = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("NoticeCode");
//			string UserCode = base.user.UserCode;
//			
//			//保存通知				
//			EntityData entityNotice = new EntityData("Standard_Notice");
//			DataRow drNotice = entityNotice.GetNewRecord();				
//			drNotice["NoticeCode"] = Code;
//			drNotice["Title"] = this.txtTitle.Value.Trim();
//			drNotice["EnableDate"] = this.dtbShowDate.Value;
//			//drNotice["EnableDays"] = 30;
//			//drNotice["Type"] = (this.lstImportantLevel.Value == "1")?1:0;
//			drNotice["SubmitPerson"] = UserCode;
//			drNotice["SubmitDate"] = DateTime.Now.ToShortDateString();
//			drNotice["Content"] = this.taContent.Value;
//			entityNotice.AddNewRecord(drNotice);
//			
//			//保存附件
//			if (this.txtAttachMent.Value.Trim() != "")
//			{
//				entityNotice.SetCurrentTable("NoticeAttachMent");
//				DataRow drAttachMent = entityNotice.GetNewRecord();
//				drAttachMent["NoticeAttachMentCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("NoticeAttachMentCode");
//				drAttachMent["NoticeCode"] = Code;
//				drAttachMent["FileName"] = this.txtAttachMent.Value;
//				drAttachMent["CreatePerson"] = UserCode;
//				drAttachMent["CreateDate"] = DateTime.Now.ToShortDateString();
//
//				drAttachMent["Content_Type"] = this.txtFileName.PostedFile.ContentType;
//				int Length = this.txtFileName.PostedFile.ContentLength;
//				drAttachMent["Length"] = Length;
//				byte[] Content = new byte[Length];
//				System.IO.Stream imgStream;
//				imgStream = this.txtFileName.PostedFile.InputStream;
//				int n = imgStream.Read(Content,0,Length);
//				drAttachMent["Content"] = Content;
//				entityNotice.AddNewRecord(drAttachMent);					
//			}
//			//保存操作用户 //如果未选择通知用户，面向全体用户发布
//			entityNotice.SetCurrentTable("NoticeUser");
//			DataRow drUser;
//			
//			drUser = entityNotice.GetNewRecord();
//			DataTable dt1 = ChectRepeat();
//			for(int i=0;i<dt1.Rows.Count;i++)
//			{
//				drUser = entityNotice.GetNewRecord();
//				drUser["NoticeUserCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("NoticeUserCode");
//				drUser["UserCode"] = dt1.Rows[i]["UserCode"];
//				drUser["NoticeCode"] = Code;
//				entityNotice.AddNewRecord(drUser);
//			}
//			
//			ViewState["SelectedUser"] = entityNotice.CurrentTable;
//			this.dgUserList.DataSource = entityNotice.CurrentTable ;
//			this.dgUserList.DataBind();
//							
//			RemindDAO.SubmitAllStandard_Notice(entityNotice);
//			entityNotice.Dispose();
//			return true;
//		
//		}
//
//		private DataTable ChectRepeat()
//		{
//			dt = (DataTable)ViewState["SelectedUser"];
//			//			string tmp = ","+this.hUserCode.Value+",";
//			//			for(int i=0;i<dt.Rows.Count;i++)
//			//			{
//			//				if(tmp.IndexOf(","+dt.Rows[i]["UserCode"].ToString()+",")>0)
//			//					dt.Rows.Remove(dt.Rows[i]);
//			//			}
//			if(dt==null) dt = new DataTable();
//			return dt;
//		}
//
//		//更新数据
//		private bool UpdataData()
//		{
//			string strCode = Request.QueryString["Code"] + "";
//			string UserCode = base.user.UserCode;
//			
//			//保存通知
//			EntityData entityNotice = RemindDAO.GetStandard_NoticeByCode(strCode);
//			DataRow dr;
//			if(entityNotice.HasRecord())
//			{
//				dr = entityNotice.CurrentRow;
//				dr["NoticeCode"] = strCode;
//				dr["Title"] = this.txtTitle.Value.Trim();
//				//dr["Type"] = (this.lstImportantLevel.Value == "1")?1:0;
//				dr["EnableDate"] = this.dtbShowDate.Value;
//				dr["SubmitPerson"] = UserCode;
//				dr["SubmitDate"] = DateTime.Now.ToShortDateString();
//				dr["Content"] = this.taContent.Value;
//			}
//		
//			entityNotice.SetCurrentTable("NoticeAttachMent");
//			entityNotice.DeleteAllTableRow("NoticeAttachMent");
//
//			//保存附件
//			if (this.txtAttachMent.Value.Trim() != "")
//			{					
//				DataRow drAttachMent;
//
//				// 已经删除了老的记录，此处新增
//				drAttachMent = entityNotice.GetNewRecord();
//				drAttachMent["NoticeAttachMentCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("NoticeAttachMentCode");
//				drAttachMent["NoticeCode"] = strCode;
//				drAttachMent["FileName"] = this.txtAttachMent.Value;
//				drAttachMent["CreatePerson"] = UserCode;
//				drAttachMent["CreateDate"] = DateTime.Now.ToShortDateString();
//				drAttachMent["Content_Type"] = this.txtFileName.PostedFile.ContentType;
//				int Length = this.txtFileName.PostedFile.ContentLength;
//				drAttachMent["Length"] = Length;
//				byte[] Content = new byte[Length];
//				System.IO.Stream imgStream;
//				imgStream = this.txtFileName.PostedFile.InputStream;
//				int n = imgStream.Read(Content,0,Length);
//				drAttachMent["Content"] = Content;
//				entityNotice.AddNewRecord(drAttachMent);					
//			}
//
//			//保存操作用户				
//			entityNotice.SetCurrentTable("NoticeUser");
//			entityNotice.DeleteAllTableRow("NoticeUser");
//			DataRow drUser;
//			drUser = entityNotice.GetNewRecord();
//			DataTable dt1 = this.ChectRepeat();
//			for(int i=0;i<dt1.Rows.Count;i++)
//			{
//				drUser = entityNotice.GetNewRecord();
//				drUser["NoticeUserCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("NoticeUserCode");
//				drUser["UserCode"] = dt1.Rows[i]["UserCode"].ToString();
//				drUser["NoticeCode"] = strCode;
//				entityNotice.AddNewRecord(drUser);
//			}
//			ViewState["SelectedUser"] = entityNotice.CurrentTable;
//			this.dgUserList.DataSource = entityNotice.CurrentTable ;
//			this.dgUserList.DataBind();					
//			
//			RemindDAO.SubmitAllStandard_Notice(entityNotice);
//			entityNotice.Dispose();
//			return true;
//		}
//
//		private void dgUserList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
//		{
//			DataTable dt = (DataTable)ViewState["SelectedUser"];
//			dt.Rows.RemoveAt(e.Item.ItemIndex);
//			ViewState["SelectedUser"] = dt;
//			dgUserList.DataSource = dt.DefaultView;
//			dgUserList.DataBind();
//		}
//
//		private void btDelete_ServerClick(object sender, System.EventArgs e)
//		{
//			string strCode = Request.QueryString["Code"] + "";
//			EntityData entityNotice = RemindDAO.GetStandard_NoticeByCode(strCode);
//			entityNotice.SetCurrentTable("Notice");
//			RemindDAO.DeleteNotice(entityNotice);
//
//			Response.Write(JavaScript.ScriptStart);
//			//Response.Write("window.opener.refresh();");
//			Response.Write("window.close();");
//			Response.Write(JavaScript.ScriptEnd);
//		}
//	}
//}
#endregion
