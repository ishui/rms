/*
上传图片
*/
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




namespace TiannuoPM.Web.HelperScripts
{
	/// <summary>
	/// ftb_uploadimage 的摘要说明。
	/// </summary>
	public partial class ftb_uploadimage : System.Web.UI.Page 
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !Page.IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			//Info3.BusinessFacade.PageFacade.LoadDictionarySelect(this.sltpic_type,"图片类型","");

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

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
//			string title = this.txtTitle.Value.Trim();
//			if ( title.Length == 0)
//			{
//				this.lblMessage.Text = "必须填写图片名称";
//				return;
//			}
			string uploadFileName = "";
			int length = 0;


			//检查是否上传勒图片
			if (this.FileUpload.PostedFile != null) 
			{
				//检查字节数
				length = this.FileUpload.PostedFile.ContentLength;

				if ( length > 0 )
				{
					uploadFileName = FileUpload.PostedFile.FileName;
					uploadFileName = uploadFileName.Substring(uploadFileName.LastIndexOf("\\")+1);

					try
					{
                        /*	EntityData pic = new EntityData("PIC_STORE");
                            string curID=TiannuoPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PictureID");
                            DataRow dr = pic.GetNewRecord();
                            dr["id"]  = curID;
                            //dr["title"] = title;
                            dr["pic_name"] = uploadFileName;
                            //dr["pic_type"] = this.sltpic_type.Value;
                            //dr["explain"] = this.txtexplain.Value;
                            dr["pic_size"] = length;
    //						if ( this.chkphase.Checked )
    //							dr["phase"] = 1;
    //						else
    //							dr["phase"] = 0;

                            System.IO.Stream myStream=FileUpload.PostedFile.InputStream;
                            string imgType=FileUpload.PostedFile.ContentType;
                            byte[] imgData=new byte[length];
                            int n=myStream.Read(imgData,0,length);
                            dr["content"] = imgData;
                            pic.AddNewRecord(dr);
                            TiannuoPM.DAL.EntityDAO.DocumentDAO.InsertPic_Store(pic);
                            pic.Dispose();

                            Response.Write(JavaScript.ScriptStart);
                            Response.Write("window.opener.FTB_onSelectPicture("+curID+");");
                            Response.Write("window.close();");
                            Response.Write(JavaScript.ScriptEnd);*/

                    }
					catch (Exception ex) 
					{
						this.lblMessage.Text = "上传文件失败";
						throw ex;
					}

				}
			}
			else
			{
				this.lblMessage.Text = "没有图片!!!";
			}


		}
	}
}
