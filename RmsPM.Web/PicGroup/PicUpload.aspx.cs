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
	/// PicUpload 的摘要说明。
	/// </summary>
	public partial class PicUpload : PageBase
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
				this.HideMasterType.Value = Request.QueryString["MasterType"] + "";
				this.HideMasterCode.Value = Request.QueryString["MasterCode"] + "";
				this.HidePBSPicCode.Value = Request.QueryString["PBSPicCode"] + "";
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
				string strPBSPicCode = Request.QueryString["PBSPicCode"] + "";

                if ("" != strPBSPicCode) //修改
                {
                    this.lblFileUploadHint.Style["display"] = "none";
                    this.btnDel.Visible = true;

                    EntityData enp = DAL.EntityDAO.PBSDAO.GetPBSPicByCode(strPBSPicCode);
                    if (enp.HasRecord())
                    {
                        this.HideMasterType.Value = enp.GetString("MasterType");
                        this.HideMasterCode.Value = enp.GetString("MasterCode");
                        this.HidePBSPicCode.Value = enp.GetString("PBSPicCode");
                        this.HidePicWidth.Value = enp.GetIntString("PicWidth");
                        this.HidePicHeight.Value = enp.GetIntString("PicHeight");

                        this.TxtPicTitle.Value = enp.GetString("PicTitle");
                        this.TxtPicRemark.Value = enp.GetString("PicRemark");

                        this.ShowPicFile.Src = "./PicShow.aspx?PicCode=" + enp.GetString("PBSPicCode");
                    }
                    else
                    {
                        this.btnDel.Visible = false;

                        Response.Write(JavaScript.ScriptStart);
                        Response.Write(JavaScript.Alert(false, "此图片不存在！"));
                        Response.Write(JavaScript.OpenerReload(false));
                        Response.Write(JavaScript.WinClose(false));
                        Response.Write(JavaScript.ScriptEnd);
                    }
                    enp.Dispose();
                }
                else //新增
                {
                    this.lblFileUploadHint.Style["display"] = "";
                }
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		protected void BtnSubmit_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string uploadFileName = "";
				int length = 0;


				//检查新建时必须上传了图片
                if (this.lblFileUploadHint.Style["display"].Trim() == "")
                {
                    if ((this.FileUpload.PostedFile == null) || (this.FileUpload.PostedFile.ContentLength <= 0))
                    {
                        Response.Write(JavaScript.Alert(true, "请选择要上传的图片"));
                        return;
                    }
                }

                EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSPicByCode(this.HidePBSPicCode.Value.Trim());
                DataRow dr = null;

                if (!entity.HasRecord())
                {
                    dr = entity.GetNewRecord();

                    dr["PBSPicCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PBSPicCode"); ;
                    dr["MasterType"] = this.HideMasterType.Value;
                    dr["MasterCode"] = this.HideMasterCode.Value;

                    entity.AddNewRecord(dr);
                }
                else
                {
                    dr = entity.CurrentRow;
                }

                dr["PicTitle"] = this.TxtPicTitle.Value;
                dr["PicRemark"] = this.TxtPicRemark.Value;
                dr["CreatePerson"] = base.user.UserCode;
                dr["CreateDate"] = DateTime.Now;

                if (this.FileUpload.PostedFile != null) 
				{
					//检查字节数
					length = this.FileUpload.PostedFile.ContentLength;

					if ( length > 0 )
					{
						uploadFileName = FileUpload.PostedFile.FileName;
						uploadFileName = uploadFileName.Substring(uploadFileName.LastIndexOf("\\")+1);

						System.IO.Stream imgStream;
						imgStream = FileUpload.PostedFile.InputStream;
						byte[] imgData = new byte[length];
						int n = imgStream.Read(imgData,0,length);

						dr["FileName"] = uploadFileName;
						dr["PicWidth"] = this.HidePicWidth.Value;
						dr["PicHeight"] = this.HidePicHeight.Value;
						dr["Content_Type"] = FileUpload.PostedFile.ContentType;
						dr["Length"] = length;
						dr["Content"] = imgData;
					}
				}

                DAL.EntityDAO.PBSDAO.SubmitAllPBSPic(entity);
                entity.Dispose();

                Response.Write(JavaScript.ScriptStart);
                Response.Write(JavaScript.OpenerReload(false));
                Response.Write(JavaScript.WinClose(false));
                Response.Write(JavaScript.ScriptEnd);
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
				DAL.EntityDAO.PBSDAO.DeletePBSPic( DAL.EntityDAO.PBSDAO.GetPBSPicByCode( this.HidePBSPicCode.Value.Trim() ) );

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
