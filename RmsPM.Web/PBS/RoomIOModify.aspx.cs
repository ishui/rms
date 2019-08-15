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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// RoomIOModify ��ժҪ˵����
	/// </summary>
	public partial class RoomIOModify : PageBase
	{
		protected System.Web.UI.WebControls.Label lblOutState;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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
			this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtAct.Value = Request.QueryString["Act"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtOutListCode.Value = Request.QueryString["OutListCode"];
				this.txtOutState.Value = Request.QueryString["OutState"];

				RmsPM.BLL.PageFacade.LoadPBSTypeSelectFirstLevel(this.sltCodeName,"");
//				RmsPM.BLL.PageFacade.LoadDictionarySelect(this.sltOutAspect,"ȥ��","");

				LoadData();
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
				string OutListCode = this.txtOutListCode.Value.Trim();
				EntityData entityDtl;

				if (OutListCode != "") 
				{
					EntityData entity = DAL.EntityDAO.ProductDAO.GetTempRoomOutByCode(OutListCode);
					if (entity.HasRecord()) 
					{
						DataRow dr = entity.CurrentRow;

						this.txtOutState.Value = entity.GetString("Out_State");
						this.lblOutListName.Text = entity.GetString("OutListName");
						this.txtOutDate.Value = entity.GetDateTimeOnlyDate("Out_Date");
						this.txtConferMark.Value = entity.GetString("ConferMark");
						this.txtRemark.Value = entity.GetString("Remark");

						this.sltCodeName.Value = entity.GetString("CodeName");
						this.txtOutAspect.Value = entity.GetString("OutAspect");
//						this.sltOutAspect.Value = entity.GetString("OutAspect");

						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						//��¼�ϵ�ֵ
						this.txtOldCodeName.Value = entity.GetString("CodeName");
						this.txtOldCurYear.Value = entity.GetInt("CurYear").ToString();

					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "���ݲ�����"));
						return;
					}
					entity.Dispose();

					//ȡ��Դ�б�
					entityDtl = DAL.EntityDAO.ProductDAO.GetRoomByOutListCode(OutListCode, "V_ROOM");
				}
				else 
				{
					//����ʱ��ȱʡֵ
					this.txtOutDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

					//����ʱ��Դ�б�Ϊ��
					entityDtl = new EntityData("V_ROOM");
				}

				//ɾ�������
				switch (this.txtAct.Value.ToLower())
				{
					case "delete":
						Delete(this.txtOutListCode.Value);
						return;

					case "check":
						Check(this.txtOutListCode.Value);
						return;

					case "cancelcheck":
						CancelCheck(this.txtOutListCode.Value);
						return;

				}

				this.spanTitle.InnerText = this.txtOutState.Value;
				this.spanOutDate.InnerText = this.txtOutState.Value;

				//���ء�ȥ��
				if ((this.txtOutState.Value != "����") && (this.txtOutState.Value != "����") && (this.txtOutState.Value != "Ԥ��") )
				{
					this.spanOutAspect.Style["display"] = "none";
					this.spanOutAspect2.Style["display"] = "none";
				}

				//��ʾ¥���б�
				Session["tbRoom"] = entityDtl.CurrentTable;
				entityDtl.Dispose();
				BindDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��Ϣ����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ¥���б�
		/// </summary>
		private void BindDataGrid() 
		{
			try 
			{
				DataTable tb = (DataTable)Session["tbRoom"];

				//��¼����¥�����룬�á�,���ָ�
				string codes = "";
				foreach(DataRow dr in tb.Rows) 
				{
					if (codes != "") 
					{
						codes = codes + ",";
					}
					codes = codes + dr["RoomCode"].ToString();
				}
				this.txtSelectRoomCode.Value = codes;

				string[] arrField = {"BuildArea", "RoomArea"};
				decimal[] arrValue = BLL.MathRule.SumColumn(tb, arrField);
				this.dgList.Columns[2].FooterText = "������" + tb.Rows.Count;
				this.dgList.Columns[5].FooterText = arrValue[0].ToString("0.####");
				this.dgList.Columns[6].FooterText = arrValue[1].ToString("0.####");
				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ɾ������
		/// </summary>
		private void Delete(string OutListCode)
		{
			try 
			{
				if (OutListCode != "") 
				{
					BLL.ProductRule.DeleteTempRoomOut(OutListCode);
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
			}

			GoBack();
		}

		/// <summary>
		/// ��˵���
		/// </summary>
		private void Check(string OutListCode)
		{
			try 
			{
				if (OutListCode != "") 
				{
					BLL.ProductRule.CheckTempRoomOut(OutListCode, base.user.UserCode);
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��˳���" + ex.Message));
			}

			GoBack();
		}

		/// <summary>
		/// ȡ����˵���
		/// </summary>
		private void CancelCheck(string OutListCode)
		{
			try 
			{
				if (OutListCode != "") 
				{
					BLL.ProductRule.CancelCheckTempRoomOut(OutListCode, base.user.UserCode);
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ȡ����˳���" + ex.Message));
			}

			GoBack();
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.sltCodeName.Value.Trim() == "") 
			{
				Hint = "�������Ʒ����";
				return false;
			}

			if (this.txtOutDate.Value.Trim() == "") 
			{
				Hint = "������" + this.spanOutDate.InnerText + "����";
				return false;
			}

			if (this.spanOutAspect2.Style["display"] != "none") 
			{
				if (this.txtOutAspect.Value.Trim() == "") 
				{
					Hint = "������ȥ��";
					return false;
				}
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

				string OutListCode = this.txtOutListCode.Value.Trim();
				EntityData entity = DAL.EntityDAO.ProductDAO.GetTempRoomOutByCode(OutListCode);
				DataRow dr;

				if (entity.HasRecord()) 
				{
					dr = entity.CurrentRow;
					dr["ModiPerson"] = base.user.UserCode;
					dr["ModiDate"] = DateTime.Now;
				}
				else 
				{
					dr = entity.CurrentTable.NewRow();
					OutListCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TempRoomOut");
					dr["OutListCode"] = OutListCode;
					dr["ProjectCode"] = this.txtProjectCode.Value;
					dr["Out_State"] = this.txtOutState.Value;
					dr["UserCode"] = base.user.UserCode;
					dr["InputDate"] = DateTime.Now;
				}

				dr["CodeName"] = this.sltCodeName.Value;
				dr["Out_Date"] = BLL.ConvertRule.ToDate(this.txtOutDate.Value.Trim());
				dr["CurYear"] = ((DateTime)dr["Out_Date"]).Year;

				//����ʱ�����Ʒ���ʡ���ȸı�ʱ���Զ����ɱ��
				if ((this.lblOutListName.Text == "") || (this.sltCodeName.Value != this.txtOldCodeName.Value) || (BLL.ConvertRule.ToString(dr["CurYear"]) != this.txtOldCurYear.Value))
				{
					int SumNo = 0;
					string OutListName = BLL.ProductRule.GenerateOutListName(dr["ProjectCode"].ToString(), dr["CodeName"].ToString(), dr["Out_State"].ToString(), int.Parse(dr["CurYear"].ToString()), BLL.ConvertRule.ToString(dr["OutListName"]), ref SumNo);
					dr["OutListName"] = OutListName;
					dr["SumNo"] = SumNo;
				}

				dr["Remark"] = this.txtRemark.Value.Trim();
				dr["ConferMark"]=this.txtConferMark.Value;
				dr["OutAspect"] = this.txtOutAspect.Value;

				if (entity.HasRecord()) 
				{
					DAL.EntityDAO.ProductDAO.UpdateTempRoomOut(entity);
				}
				else 
				{
					entity.CurrentTable.Rows.Add(dr);
					DAL.EntityDAO.ProductDAO.InsertTempRoomOut(entity);
				}

				entity.Dispose();

				SaveRoom(OutListCode);

				//�������޸ı����ʼ�ջص���ϸҳ��
				this.txtFromUrl.Value = "RoomIOInfo.aspx?OutListCode=" + OutListCode;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}

			GoBack();
		}

		/// <summary>
		/// ���淿Դ��ϸ
		/// </summary>
		private void SaveRoom(string OutListCode) 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;

				//�ɵ���ϸ
				EntityData entity = DAL.EntityDAO.ProductDAO.GetTempRoomStructureByOutListCode(OutListCode);

				//�µ���ϸ
				DataTable tb = (DataTable)Session["tbRoom"];

				//ɾ��ԭ��������û�е�
				foreach(DataRow dr in entity.CurrentTable.Rows) 
				{
					string code = dr["TempRoomCode"].ToString();
					if (tb.Select("RoomCode='" + code + "'").Length == 0) 
					{
						EntityData entityDtl = DAL.EntityDAO.ProductDAO.GetTempRoomStructureByCode(dr["TempCode"].ToString());
						DAL.EntityDAO.ProductDAO.DeleteTempRoomStructure(entityDtl);
						entityDtl.Dispose();
					}

				}

				//���
				foreach(DataRow dr in tb.Rows) 
				{
					string code = dr["RoomCode"].ToString();
					if (entity.CurrentTable.Select("TempRoomCode='" + code + "'").Length == 0) 
					{
						EntityData entityDtl = new EntityData("TempRoomStructure");
						DataRow drNew = entityDtl.CurrentTable.NewRow();

						drNew["TempCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TempRoomStructure");
						drNew["TempRoomCode"] = code;
						drNew["OutListCode"] = OutListCode;
						drNew["TempBuildingCode"] = dr["BuildingCode"].ToString();
						drNew["TempChamberCode"] = dr["ChamberCode"].ToString();
						drNew["ProjectCode"] = ProjectCode;

						entityDtl.CurrentTable.Rows.Add(drNew);
						DAL.EntityDAO.ProductDAO.InsertTempRoomStructure(entityDtl);
						entityDtl.Dispose();
					}
				}

				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				throw ex;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			string FromUrl = this.txtFromUrl.Value.Trim();
			if (FromUrl != "") 
			{
				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			}
			else 
			{
				//ȱʡ�����б�ҳ��
				Response.Write(string.Format("window.location = '{0}';", "RoomIOList.aspx?ProjectCode=" + this.txtProjectCode.Value + "&IOType=" + BLL.ProductRule.TransTempRoomOutIOType(this.txtOutState.Value, false)));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}

		/// <summary>
		/// ɾ���б�ķ�Դ
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try 
			{
				string code = this.dgList.DataKeys[e.Item.ItemIndex].ToString();

				DataTable dt = (DataTable)Session["tbRoom"];
				DataRow[] drs = dt.Select("RoomCode='" + code + "'");
				if (drs.Length > 0) 
				{
					dt.Rows.Remove(drs[0]);
				}

//				Session["tbRoom"] = dt;
				BindDataGrid();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ɾ��ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// ѡ��Դ���غ󣬽���ѡ�ļ�¼��ӵ���Դ��ϸ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSelectRoomReturn_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string codes = this.txtSelectRoomCode.Value.Trim();
				string[] arrcode = codes.Split(",".ToCharArray());

				DataTable tb = (DataTable)Session["tbRoom"];

				foreach(string code in arrcode) 
				{
					//��鵱ǰ��ϸ���Ƿ����иü�¼
					if (tb.Select("RoomCode='" + code + "'").Length == 0) 
					{
						//������ϸ
						EntityData entity = DAL.EntityDAO.ProductDAO.GetV_ROOMByCode(code);
						if (entity.HasRecord()) 
						{
							DataRow dr = entity.CurrentTable.Rows[0];
							DataRow drNew = tb.NewRow();

							int iColumnCount = tb.Columns.Count;
							for ( int i =0 ; i<iColumnCount; i++)
							{
								string columnName= tb.Columns[i].ColumnName;
								if ( entity.CurrentTable.Columns.Contains(columnName))
									drNew[columnName] = dr[columnName];
							}
							tb.Rows.Add(drNew);
						}
						entity.Dispose();
					}
				}

				Session["tbRoom"] = tb;

				BindDataGrid();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// �޸������ˢ�·�Դ��ϸ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnModifyAreaReturn_ServerClick(object sender, System.EventArgs e)
		{
			DataTable tb = (DataTable)Session["tbRoom"];
			tb.Rows.Clear();

			btnSelectRoomReturn_ServerClick(sender, e);
			BindDataGrid();
		}

		/// <summary>
		/// ����ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnHiddenBatchDelete_ServerClick(object sender, System.EventArgs e)
		{
			string codes = this.txtSelectRoomCode.Value.Trim();
			string[] arrcode = codes.Split(",".ToCharArray());

			DataTable dt = (DataTable)Session["tbRoom"];
			foreach(string code in arrcode) 
			{
				DataRow[] drs = dt.Select("RoomCode='" + code + "'");
				if (drs.Length > 0) 
				{
					dt.Rows.Remove(drs[0]);
				}
			}

			Session["tbRoom"] = dt;
			BindDataGrid();
		}

	}
}
