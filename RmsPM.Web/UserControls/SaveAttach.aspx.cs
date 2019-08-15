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
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;
using Rms.Web;

using System.IO;

namespace RmsPM.Web.UserControls
{
	/// <summary>
	/// SaveAttach 的摘要说明。
	/// </summary>
    public partial class SaveAttach : PageBase
    {
        private string _uploadID = string.Empty;

        public string UploadID
        {
            get
            {
                return this._uploadID;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IniPage();
            }

        }


        private void IniPage()
        {
            try
            {
                this.txtAttachMentCode.Value = Request.QueryString["AttachMentCode"];
                string ud_sSingleFile = Request.QueryString["SingleFile"] + "";
                    string ud_sAttachMentType = Request["strAttachMentType"] + "";
                    string ud_sMasterCod = Request["strMasterCode"] + "";
/* 原有支持单附件的功能 现取消 simon 2007-1-24
                    EntityData entity = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByTypeAndMasterCode(ud_sAttachMentType, ud_sMasterCod);

                    if (entity.HasRecord())
                    {
                        this.txtAttachMentCode.Value = entity.GetString("AttachMentCode");
                    }
                    entity.Dispose();
*/
             

                switch (this.up_sPMNameLower)
                {                     
                    case "yefengpm":
                        fileUpload.AllowedFileExtensions = new string[5] { ".doc", ".xls", ".jpg", ".jpge", ".gif" };
                        break;
                    default:
                        lblHint.Visible = false;
                        break;
                }
                lblHint.Text = "请将文件名控制在40个字符（20个汉字）之内！";
                lblHint.Visible = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
            }
        }       


        protected void btnOK_ServerClick(object sender, System.EventArgs e)
        {
            SaveAttachMentNormal();
        }

        private void SaveAttachMentNormal()
        {
			try 
			{
                if(this.up_sPMNameLower=="yefengpm"){//jpg格式不能超过250k
                    foreach (Telerik.WebControls.UploadedFile uf in fileUpload.UploadedFiles)
                    {
                        string extname = System.IO.Path.GetExtension(uf.FileName.Substring(uf.FileName.LastIndexOf("\\") + 1)).ToLower();
                    
                        if (extname == ".jpg" || extname==".jepg")
                        {
                            if(uf.ContentLength>250000)
                            {
                               Response.Write(Rms.Web.JavaScript.Alert(true, "jpg格式文件不能超过250k" ));
                                return;
                            }
                        }
                    }
                }
                User myUser = (User)Session["User"];
                BLL.DocumentRule documentRule = BLL.DocumentRule.Instance();
                foreach (Telerik.WebControls.UploadedFile uf in fileUpload.UploadedFiles)
                {
                    int length = uf.ContentLength;
                    string uploadFileName = uf.FileName.Substring(uf.FileName.LastIndexOf("\\") + 1);
                    Rms.LogHelper.LogHelper.Debug(this.txtAttachMentCode.Value);
                    documentRule.AddOrUpdateAttachment(this.txtAttachMentCode.Value, user.UserCode, uploadFileName, uf.ContentType, length, Request["strAttachMentType"] + "", Request["strMasterCode"] + "", uf.InputStream);
                        
                }
                Response.Write(JavaScript.ScriptStart);
						Response.Write("window.opener.Refresh();");
						Response.Write("window.close();");
						
						Response.Write(JavaScript.ScriptEnd);
							
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}

        }

      
    }
}
