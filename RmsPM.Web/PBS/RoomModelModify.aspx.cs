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
	/// RoomModelModify ��ժҪ˵����
	/// </summary>
	public partial class RoomModelModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtStructure;
		protected System.Web.UI.HtmlControls.HtmlInputText txtPBSTypeName;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtModelCode.Value = Request["ModelCode"];

				if (this.txtProjectCode.Value == "") 
				{
					Response.Write("����Ŀ����");
					Response.End();
				}

				PageFacade.LoadPBSTypeSelect(sltPBSTypeCode,"",this.txtProjectCode.Value);
				PageFacade.LoadDictionarySelect(this.sltStructure, "���͹���", "", "");
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string ModelCode = this.txtModelCode.Value;

				EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByCode(ModelCode);
				if (entity.HasRecord())
				{
					this.txtModelName.Value = entity.GetString("ModelName");
					this.txtBuildArea.Value = BLL.MathRule.GetDecimalShowString(entity.GetDecimal("BuildArea"));
					this.txtRoomArea.Value = BLL.MathRule.GetDecimalShowString(entity.GetDecimal("RoomArea"));
					this.sltStructure.Value = entity.GetString("Structure");
					this.txtRemark.Value = entity.GetString("Remark");
					this.sltPBSTypeCode.Value = entity.GetString("HouseType");
					this.txtPicCode.Value = entity.GetString("ImageCode");
					this.txtModelCode.Value = entity.GetString("ModelCode");
				}
				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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

		private void SavaData()
		{
			try
			{
				string ModelCode = this.txtModelCode.Value;
				string picCode = "";

				EntityData entity1=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByCode(ModelCode);
				if (entity1.HasRecord())
				{
					picCode=entity1.GetString("ImageCode");
				}
				entity1.Dispose();
				
				bool isNew = false;
			
				int length = 0;
				if (this.FileUpload.PostedFile != null) 
				{
					length = this.FileUpload.PostedFile.ContentLength;
				}			

				if (length>0)
				{
					string uploadFileName = FileUpload.PostedFile.FileName;
					uploadFileName = uploadFileName.Substring(uploadFileName.LastIndexOf("\\")+1);
					EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetPhotosByCode(picCode);
					
					DataRow dr = null;
					if ( entity.HasRecord())
					{
						isNew = false;
						dr = entity.CurrentRow;
					}
					else
					{
						isNew = true;
						picCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PictureCode");
						dr = entity.GetNewRecord();
						dr["PictureCode"]=picCode;
					}					

					if ( length>0)
					{
						dr["PictrueName"]=uploadFileName;
						dr["PictureSize"]=length;
						System.IO.Stream myStream=FileUpload.PostedFile.InputStream;
						string imgType=FileUpload.PostedFile.ContentType;
						byte[] imgData=new byte[length];
						int n=myStream.Read(imgData,0,length);
						dr["PicContent"] = imgData;
					}

					if ( isNew )
					{
						entity.AddNewRecord(dr);
						ProductDAO.InsertPhotos(entity);					
					}
					else
					{	
						ProductDAO.UpdatePhotos(entity);					
					}
					entity.Dispose();
				}

				isNew = false;

				EntityData entityModel=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByCode(ModelCode);

				DataRow drs = null;
				if (entityModel.HasRecord())
				{
					isNew = false;
					drs = entityModel.CurrentRow;
				}
				else
				{
					isNew = true;
					ModelCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ModelCode");
					drs = entityModel.GetNewRecord();
					drs["ModelCode"] = ModelCode;
					drs["ProjectCode"] = this.txtProjectCode.Value;
				}

				drs["ModelName"] = this.txtModelName.Value;
				drs["Structure"] = this.sltStructure.Value;
				drs["ImageCode"] = picCode;
				drs["BuildArea"] = this.txtBuildArea.ValueDecimal;
				drs["RoomArea"] = this.txtRoomArea.ValueDecimal;
				drs["Remark"] = this.txtRemark.Value;
				drs["HouseType"] = this.sltPBSTypeCode.Value;				
				
				if ( isNew )
				{
					entityModel.AddNewRecord(drs);
					ProductDAO.InsertModel(entityModel);					
				}
				else
				{	
					ProductDAO.UpdateModel(entityModel);					
				}

				entityModel.Dispose();

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtModelName.Value.Trim() == "") 
			{
				Hint = "�����뻧������";
				return false;
			}

			if (this.sltStructure.Value.Trim() == "") 
			{
				Hint = "�����뷿�͹���";
				return false;
			}

			if (this.sltPBSTypeCode.Value.Trim() == "") 
			{
				Hint = "�������Ʒ����";
				return false;
			}

			//�������Ʋ����ظ�
			if (BLL.ProductRule.IsModelNameExists(this.txtModelName.Value, this.txtModelCode.Value, this.txtProjectCode.Value))
			{
				Hint = "��ͬ�Ļ��������Ѵ��� �� ";
				return false;
			}

			return true;
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			//Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

				SavaData();

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
