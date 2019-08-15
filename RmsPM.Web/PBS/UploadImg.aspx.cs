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


namespace RmsPM.Web.PBS
{
	/// <summary>
	/// UploadImg ��ժҪ˵����
	/// </summary>
	public partial class UploadImg : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				this.txtParentCode.Value = Request["ParentCode"];
				this.txtProjectCode.Value = Request["ProjectCode"];
				LoadData();
			}
			
		}

		private void LoadData()
		{
			try
			{
				if (this.txtParentCode.Value.Trim().Length>0)
				{
					EntityData entity = RmsPM.DAL.EntityDAO.ProductDAO.GetBuildingByCode(this.txtParentCode.Value);

					if (entity.HasRecord())
					{
						this.lblBuildingName.Text = entity.GetString("BuildingName");

						if (entity.GetInt("IsArea") == 1) 
						{
							this.tdBuildingName.InnerText = "����";
							this.tdTitle.InnerText = "�ϴ�����ƽ��ͼ";
						}
						else 
						{
							this.tdTitle.InnerText = "�ϴ�¥��ƽ��ͼ";
						}

						this.txtProjectCode.Value = entity.GetString("ProjectCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��¥��������"));
					}

					entity.Dispose();
				}
				else
				{
					this.tdTitle.InnerText = "�ϴ�С��ƽ��ͼ";
					this.trBuildingName.Style["display"] = "none";
				}
			}
			catch(Exception ex)
			{
				throw ex;
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

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if ((this.File1.PostedFile == null) || (this.File1.PostedFile.FileName == ""))
			{
				Hint = "��ѡ��ͼƬ";
				return false;
			}

			return true;
		}

		private void SavaData(string parentCode,string projectCode)
		{
			try
			{
				int length=0;
				string picCode="";
				if (this.File1.PostedFile != null) 
				{
					length = this.File1.PostedFile.ContentLength;
				}

				if (length>0)
				{
					
//					string fileName=File1.PostedFile.FileName;
//					fileName = fileName.Substring(fileName.LastIndexOf("\\")+1);
//					pathName=pathLoad+"/"+DateTime.Now.Year+DateTime.Now.Year+DateTime.Now.Month+DateTime.Now.Day+DateTime.Now.Hour+DateTime.Now.Minute+DateTime.Now.Second+fileName;					
//					File1.PostedFile.SaveAs(Server.MapPath(pathName));

					string uploadFileName = File1.PostedFile.FileName;
					uploadFileName = uploadFileName.Substring(uploadFileName.LastIndexOf("\\")+1);
					EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetPhotosByCode(picCode);
					
					DataRow dr = null;

					picCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PictureCode");
					dr = entity.GetNewRecord();
					dr["PictureCode"]=picCode;								

					if ( length>0)
					{
						dr["PictrueName"]=uploadFileName;
						dr["PictureSize"]=length;
						System.IO.Stream myStream=File1.PostedFile.InputStream;
						string imgType=File1.PostedFile.ContentType;
						byte[] imgData=new byte[length];
						int n=myStream.Read(imgData,0,length);
						dr["PicContent"] = imgData;
					}

					entity.AddNewRecord(dr);
					ProductDAO.InsertPhotos(entity);				
					
					entity.Dispose();

					if (parentCode.Trim().Length>0)
					{
						EntityData entity1=RmsPM.DAL.EntityDAO.ProductDAO.GetBuildingByCode(parentCode);
						if (entity1.HasRecord())
						{
							DataRow dr1=entity1.CurrentRow;
							dr1["AreaImageCode"]=picCode;
							ProductDAO.UpdateBuilding(entity1);						
						}
						entity1.Dispose();
					}
					else
					{
						EntityData entity2=RmsPM.DAL.EntityDAO.ProjectDAO.GetProjectByCode(projectCode);
						if (entity2.HasRecord())
						{
							DataRow dr2=entity2.CurrentRow;
							dr2["ImagePath"]=picCode;
							ProjectDAO.UpdateProject(entity2);							
						}
						entity2.Dispose();
					}
					
				}
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				SavaData(this.txtParentCode.Value, this.txtProjectCode.Value);

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

		}

		private void GoBack() 
		{
			Response.Write(JavaScript.ScriptStart);
//			Response.Write("window.opener.location = window.opener.location;");
//			Response.Write("window.parent.location.href='Building_l.aspx?ProjectCode="+this.txtProjectCode.Value+"';");
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
		}
	}
}
