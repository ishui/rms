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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// DocumentModify ��ժҪ˵����
	/// ���������
	/// DocumentCode		�ĵ���ţ���=����
	/// DocumentTypeCode	�����ĵ����ͣ��á�,���ָ���
	/// Code				������ر�ţ��á�,���ָ��� 
	/// </summary>
	public partial class DocumentModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtDocumentTypeCode;
		protected System.Web.UI.WebControls.Label lblChkTitle;
		protected System.Web.UI.HtmlControls.HtmlInputButton AddAttach;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdTitle;


//		private string strAct = "";
//		private string strDocumentCode = "";
//		private string strDocumentTypeCode = "";
//		private string strCode = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{

			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}

			this.AttachMentAdd1.AttachMentType = "DocumentAttach";
			this.AttachMentAdd1.MasterCode = this.txtDocumentCode.Value;
			
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
				this.txtDocumentCode.Value = Request.QueryString["DocumentCode"]+"";
				this.txtAct.Value = Request["Action"] + "";
				this.txtRefreshScript.Value = Request.QueryString["RefreshScript"]+"";
				this.txtFromUrl.Value = Request.QueryString["FromUrl"]+"";
				this.txtFixedDocumentTypeCode.Value = Request.QueryString["DocumentTypeCode"]+"";
				this.txtCode.Value = Request.QueryString["Code"]+"";
                PageFacade.LoadDictionarySelect(this.FileKind, "�ļ�����", "");

                switch (this.up_sPMNameLower)
                { 
                    case "tangchenpm":
                        this.btnDocumentID.Visible = true;
                        this.txtDocumentID.Enabled = false;
                        this.txtDocumentOther.Visible = true;
                        break;
                    default:
                        this.btnDocumentID.Visible = false;
                        this.txtDocumentID.Enabled = true;
                        this.txtDocumentOther.Visible = false;
                        break;
                }

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// �����������
		/// </summary>
		private void LoadData()
		{
			try
			{
				string DocumentCode = this.txtDocumentCode.Value;

                if (DocumentCode != "")
                {
                    //Ȩ��
                    ArrayList ar = user.GetResourceRight(DocumentCode, "Document");
                    if (!ar.Contains("100103"))
                    {
                        Response.Redirect("../RejectAccess.aspx?OperationCode=100103");
                        Response.End();
                    }

                    EntityData entity = DocumentDAO.GetStandard_DocumentByCode(DocumentCode);

                    if (entity.HasRecord())
                    {
                        this.txtTitle.Text = entity.GetString("Title");
                        this.txtDocumentID.Text = entity.GetString("DocumentID");
                        this.txtAuthor.Text = entity.GetString("Author");
                        this.FreeTextBox.Text = entity.GetString("HtmlMainText");

                        this.ucGroup.Value = entity.GetString("GroupCode");

                        this.lblFixDocumentTypeName.Text = BLL.ConvertRule.Concat(entity.Tables["DocumentConfig"], "DocumentTypeName", ",", "fixed=1");
                        this.KeeperTextBox.Text = entity.GetString("Keeper");
                        this.FileKind.Value = entity.GetString("FileKind");
                        this.FileDate.Value = entity.GetDateTimeOnlyDate("FileDate");
                        this.Inputunit1.Value = entity.GetString("UnitCode");
                        this.SavePlaceTextBox.Text = entity.GetString("SavePlace");
                        this.Counts.Text = entity.GetString("Counts");
                    }

                    //					LoadDocumentType(entity);
                    entity.Dispose();

                    this.btnDocumentID.Visible = false;
                    this.txtDocumentOther.Visible = false;
                }
                else
                {
                    //����ʱ��ȱʡֵ
                    this.ucGroup.Value = Request.QueryString["GroupCode"] + "";
                    this.Counts.Text = "1";
                }

                //���Ͳ����޸�
                string GroupCodeReadonly = "" + Request.QueryString["GroupCodeReadonly"];
                if (GroupCodeReadonly == "1")
                {
                    this.ucGroup.Enable = false;
                }
			}
			catch (Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
			}
		}

		/// <summary>
		/// �����ĵ�����
		/// </summary>
		/// <param name="entity"></param>
		private void LoadDocumentType(EntityData entity) 
		{
			this.lstDocumentType.Items.Clear();
			DataRow[] drs = entity.Tables["DocumentConfig"].Select("fixed is null or fixed=0");
			foreach(DataRow dr in drs) 
			{
				ListItem li = new ListItem(dr["DocumentTypeName"].ToString(), dr["DocumentTypeCode"].ToString());
				this.lstDocumentType.Items.Add(li);
			}
		}

		/// <summary>
		/// �����ĵ�
		/// </summary>
		/// <param name="isNew"></param>
		private void SaveData()
		{
			try 
			{
				EntityData entity = DAL.EntityDAO.DocumentDAO.GetStandard_DocumentByCode(this.txtDocumentCode.Value);
				bool isNew = !entity.HasRecord();

				DataRow dr;

				if (isNew) 
				{
					dr = entity.GetNewRecord();

					this.txtDocumentCode.Value = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("Document");
					dr["DocumentCode"] = this.txtDocumentCode.Value;
					dr["CreatePerson"] = base.user.UserCode;
					dr["CreateDate"] = DateTime.Now;
                    dr["Status"] = 0;

					entity.AddNewRecord(dr);
				}
				else 
				{
					dr = entity.CurrentRow;

					dr["ModifyPerson"] = base.user.UserCode;
					dr["ModifyDate"] = DateTime.Now;
				}

				dr["Title"] = this.txtTitle.Text;
                if (this.txtDocumentOther.Text != "")
                {
                    dr["DocumentID"] = "(" + this.txtDocumentOther.Text + ")" + this.txtDocumentID.Text;
                }
                else
                {
                    dr["DocumentID"] = this.txtDocumentID.Text;
                }

				dr["Author"] = this.txtAuthor.Text;
				dr["MainText"] = this.FreeTextBox.HtmlStrippedText;
				dr["HtmlMainText"] = this.FreeTextBox.HtmlEncodedText;
				dr["GroupCode"] = this.ucGroup.Value;
                dr["Keeper"] = this.KeeperTextBox.Text;
	            dr["FileKind"] =this.FileKind.Value;
	            dr["FileDate"] =this.FileDate.Value;
                dr["UnitCode"] = this.Inputunit1.Value;
	            dr["SavePlace"] = this.SavePlaceTextBox.Text;
                dr["SaveDate"] = this.SaveDate.Value;
                dr["Remark"] = this.RemarkTextBox.Text;
                dr["Counts"] = this.Counts.Text;
				// �����ĵ�������Ϣ
				SaveDocumentConfig(entity);

				DocumentDAO.SubmitAllStandard_Document(entity);

				if (isNew) 
				{
					// ���渽��
					this.AttachMentAdd1.SaveAttachMent(this.txtDocumentCode.Value);
				}

				entity.Dispose();
			}
			catch (	Exception ex)
			{
				throw ex;
			}
		}

		private bool IsContain(String[] arr, string val) 
		{
			foreach(string s in arr) 
			{
				if (s == val)
					return true;
			}
			return false;
		}

		/// <summary>
		/// �޸��ĵ�����
		/// </summary>
		/// <param name="DocumentCode"></param>
		private void SaveDocumentConfig(EntityData entity) 
		{
//			string[] arrDocumentTypeCode = this.txtlstDocumentTypeCode.Value.Split(",".ToCharArray());
//			string DocumentTypeCode;

			entity.SetCurrentTable("DocumentConfig");

			/*
			//ɾ��
			foreach(DataRow dr in entity.CurrentTable.Rows) 
			{
				if (dr["Fixed"].ToString() != "1") 
				{
					if (dr["DocumentTypeCode"] != null)
						DocumentTypeCode = dr["DocumentTypeCode"].ToString();
					else
						DocumentTypeCode = "";

					if (!IsContain(arrDocumentTypeCode, DocumentTypeCode)) 
					{
						dr.Delete();
					}
				}

			}

			//����Զ����ĵ�����
			foreach(String sTemp in arrDocumentTypeCode) 
			{
				DocumentTypeCode = sTemp;

				if (DocumentTypeCode != "") 
				{
					DataRow[] drs = entity.CurrentTable.Select("DocumentTypeCode=" + DocumentTypeCode);
					DataRow dr;

					if (drs.Length == 0) 
					{
						dr = entity.CurrentTable.NewRow();
						dr["DocumentConfigCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("DocumentConfig");
						dr["DocumentCode"] = DocumentCode;
						dr["DocumentTypeCode"] = DocumentTypeCode;

						entity.CurrentTable.Rows.Add(dr);
					}
				}
			}
*/

			//��ӹ̶��ĵ�����
			string[] arrTypeCode = this.txtFixedDocumentTypeCode.Value.Split(",".ToCharArray());
			string[] arrCode = this.txtCode.Value.Split(",".ToCharArray());
			for (int i = 0;i<arrTypeCode.Length;i++) 
			{
				string TypeCode = arrTypeCode[i].Trim();
				string Code = "";

				if (i < arrCode.Length)
					Code = arrCode[i].Trim();

				if ((TypeCode != "") && (Code != "")) 
				{
					DataRow[] drs = entity.CurrentTable.Select("DocumentTypeCode='" + TypeCode + "' and Code='" + Code + "'");
					if (drs.Length == 0) 
					{
						DataRow dr = entity.CurrentTable.NewRow();
						dr["DocumentConfigCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("DocumentConfig");
						dr["DocumentCode"] = this.txtDocumentCode.Value;
						dr["DocumentTypeCode"] = TypeCode;
						dr["Code"] = Code;
						dr["Fixed"] = 1;

						entity.CurrentTable.Rows.Add(dr);
					}
				}
			}
		}

		protected void btnRefreshDocumentType_ServerClick(object sender, System.EventArgs e)
		{
			this.lstDocumentType.Items.Clear();
			string[] arrDocumentTypeCode = this.txtlstDocumentTypeCode.Value.Split(",".ToCharArray());
			string[] arrDocumentTypeName = this.txtlstDocumentTypeName.Value.Split(",".ToCharArray());
			for (int i=0;i<arrDocumentTypeCode.Length;i++) 
			{
				if (arrDocumentTypeCode[i] != "") 
				{
					string TypeName;

					if (i < arrDocumentTypeName.Length)
						TypeName = arrDocumentTypeName[i];
					else
						TypeName = arrDocumentTypeCode[i];

					ListItem li = new ListItem(TypeName, arrDocumentTypeCode[i]);
					this.lstDocumentType.Items.Add(li);
				}
			}
		}

		public void anDeleteType_Click(object sender, System.EventArgs e) 
		{
			if (this.lstDocumentType.SelectedIndex >= 0) 
			{
				this.lstDocumentType.Items.Remove(this.lstDocumentType.Items[this.lstDocumentType.SelectedIndex]);
			}

		}

		/// <summary>
		/// ���ؽ��ĵ������б������
		/// </summary>
		private string GetDocumentTypeLst(int type) 
		{
			string code = "";
			string name = "";

			for(int i=0;i<this.lstDocumentType.Items.Count-1;i++) 
			{
				if (i > 0) 
				{
					code = code + ",";
					name = name + ",";
				}
				code = code + this.lstDocumentType.Items[i].Value;
				name = name + this.lstDocumentType.Items[i].Text;
			}
			if(type==1)
				return code;
			else
				return name;
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtTitle.Text.Trim() == "")
			{
				Hint = "��������� ��";
				return false;
			}

			if (this.ucGroup.Value.Trim() == "")
			{
				Hint = "�������ĵ����� ��";
				return false;
			}

			return true;
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

				SaveData();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
				return;
			}

			GoBack();
		}

		private void GoBack()
		{
			if(this.txtAct.Value == "Insert")
			{
				string DocumentCode = this.txtDocumentCode.Value;

				if(Request["Return"]+""=="Task"&&DocumentCode!="")
				{
					Response.Write(JavaScript.ScriptStart);
					Response.Write("window.opener.SelectDocument('" + DocumentCode + "');");
					Response.Write("window.close();");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}
			}

			Response.Write(JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() != "") 
			{
				Response.Write("window.opener." + this.txtRefreshScript.Value.Trim());
			}
			else 
			{
				Response.Write("window.opener.location = window.opener.location;");
			}
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(JavaScript.ScriptEnd);
		}

        protected void btnDocumentID_ServerClick(object sender, EventArgs e)
        {
            if (this.ucGroup.Value == "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "����ѡ���ĵ����" ));
                return;
            }
            //ƴ�������ǰ׺
            string strTemp="";
            string strFullSortID = SystemGroupRule.GetSystemGroupFullSortID(this.ucGroup.Value);
            string[] strArray = strFullSortID.Split('-');
            string strFirstTemp="";
            int j = 0;
            for (int i = 0; i < strArray.Length;i ++)
            {
                strFirstTemp = strFirstTemp + strArray[i];
                j++;
                if (j == 2)
                {
                    strFirstTemp = strFirstTemp + "-";
                    j = 0;
                }
            }

            //ƴ��������׺
            string strNextTemp="";
            string strNextTemp2="";
            int iNextTemp;
            int iNextTemp2;

            EntityData entity = DocumentDAO.GetDocumentIDByGroupCode(this.ucGroup.Value);
            if (entity.HasRecord())
            {
                DataRow dr =entity.CurrentRow;
                if (dr["DocumentID"].ToString() == "" || dr["DocumentID"].ToString().Length<4)
                {
                    strNextTemp = "0001";
                    strTemp = strFirstTemp + "-" + strNextTemp;
                    this.txtDocumentID.Text = strTemp;
                    return;
                }
                strNextTemp = dr["DocumentID"].ToString().Substring(dr["DocumentID"].ToString().Length - 4, 4); ;
                iNextTemp = Convert.ToInt32(strNextTemp.TrimStart('0'));
               
                for(int k = 1;k< entity.CurrentTable.Rows.Count;k++)
                {
                    entity.SetCurrentRow(k);
                    DataRow drNext =entity.CurrentRow;
                    strNextTemp2 = drNext["DocumentID"].ToString().Substring(drNext["DocumentID"].ToString().Length - 4, 4); ;
                    iNextTemp2 = Convert.ToInt32(strNextTemp2.TrimStart('0'));
                    if (iNextTemp <= iNextTemp2)
                    {
                        iNextTemp = iNextTemp2;
                    }
                }
                strNextTemp = Convert.ToString(iNextTemp+1).PadLeft(4, '0');
            }
            else
            {
                strNextTemp = "0001";
            }

            strTemp = strFirstTemp + "-" + strNextTemp;
            this.txtDocumentID.Text = strTemp;
        }
}
}
