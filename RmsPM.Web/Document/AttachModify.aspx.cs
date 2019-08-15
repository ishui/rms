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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.Web;

namespace RmsPM.Web.Document
{
	/// <summary>
	/// AttachModify 的摘要说明。
	/// </summary>
	public partial class AttachModify : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				IniPage();
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
			if (Session["entityDocument"] == null) 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "读取Session文档信息失败"));
				return;
			}

			this.txtAttachmentCode.Value = Request.QueryString["AttachmentCode"]+"";
			this.txtDocumentCode.Value = Request.QueryString["DocumentCode"]+"";
			this.txtAct.Value = Request["Action"] + "";
			
			try
			{
				switch (this.txtAct.Value)
				{
					case "Insert":
						this.tdTitle.InnerText = "新增附件";
						break;

					case "Modify":
						this.tdTitle.InnerText="修改附件";
						break;
				}

				LoadData();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化附件页面错误。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			if ( this.txtAct.Value != "Modify" ) 
				return;

			string AttachmentCode = this.txtAttachmentCode.Value;

			EntityData entity = (EntityData)Session["entityDocument"];
			entity.SetCurrentTable("Attachment");

			if(entity.CurrentTable.Rows.Count>0)
			{
				foreach(DataRow dr in entity.CurrentTable.Rows) 
				{
					if (dr["AttachmentCode"].ToString() == AttachmentCode) 
					{ 
						this.txtTitle.Value = dr["Title"].ToString();
						this.txtBody.Text=dr["FileName"].ToString();
						this.txtDocumentCode.Value = dr["DocumentCode"].ToString();

						break;
					}
				}
			}
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtTitle.Value.Trim() == "") 
			{
				Hint = "请输入标题";
				return false;
			}

			if ((this.txtBody.Text == "") && (this.txtFileName.Value == "")) 
			{
				Hint = "请添加附件";
				return false;
			}

			return true;
		}

		private bool SaveData(bool isNew)
		{
			string Hint = "";
			if (!CheckValid(ref Hint)) 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
				return false;
			}

			string AttachmentCode = this.txtAttachmentCode.Value;

			EntityData entity = (EntityData)Session["entityDocument"];
			entity.SetCurrentTable("Attachment");
			DataRow dr = null;

			if (isNew) 
			{
				dr = entity.GetNewRecord();
				AttachmentCode=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("Attachment");
				dr["AttachmentCode"] = AttachmentCode;
				dr["DocumentCode"] = this.txtDocumentCode.Value;
				dr["CreatePerson"] = base.user.UserCode;
				dr["CreateDate"] = DateTime.Now;
			}
			else 
			{
				foreach(DataRow drTemp in entity.CurrentTable.Rows) 
				{
					if (drTemp["AttachmentCode"].ToString() == AttachmentCode) 
					{ 
						dr = drTemp;
						break;
					}
				}

				dr["ModifyPerson"] = base.user.UserCode;
				dr["ModifyDate"] = DateTime.Now;
			}

			dr["Title"]=this.txtTitle.Value;

			//保存附件
			if (txtFileName.Value != "") 
			{
				System.IO.Stream imgStream;
				int imgLen;
				string imgContentType;
   
				imgStream  = txtFileName.PostedFile.InputStream;
				imgLen =  txtFileName.PostedFile.ContentLength;
				imgContentType = txtFileName.PostedFile.ContentType;
				byte[] imgBinaryData = new byte[imgLen];

				int n = imgStream.Read(imgBinaryData, 0, imgLen);

				dr["Content_Type"] = imgContentType;
				dr["Content"] = imgBinaryData;
				dr["FileName"] = this.txtBody2.Value;
				dr["Length"] = imgLen;
			}

			if (isNew) 
			{
				entity.AddNewRecord(dr);
			}

			Session["entityDocument"] = entity;

			return true;
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string act = this.txtAct.Value;
			string DocumentCode = this.txtDocumentCode.Value;

			try
			{
				switch(act) 
				{
					case "Insert":
						if (!SaveData(true))
							return;

						break;

					case "Modify":
						if (!SaveData(false))
							return;

						break;
				
				}

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.doRefreshAttach();");
				Response.Write("window.close()");
				Response.Write(JavaScript.ScriptEnd);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
		}
	}
}
