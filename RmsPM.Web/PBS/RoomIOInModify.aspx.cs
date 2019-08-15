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
	/// RoomIOInModify ��ժҪ˵����
	/// </summary>
	public partial class RoomIOInModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSelectRoom;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnBatchDelete;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
			}
			else 
			{
				this.spanBuildingName.InnerText = this.txtSelectBuildingName.Value;
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

				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�������" + ex.Message));
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

						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						//��¼�ϵı��
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

				//��ʾ¥���б�
				Session["tbRoom"] = entityDtl.CurrentTable;
				entityDtl.Dispose();
				BindDataGrid();
				DisplayBuildingName();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��Ϣ������" + ex.Message));
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

				//��¼����������룬�á�,���ָ�
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б�������" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ¥�����ƣ��á�,���ָ�
		/// </summary>
		private void DisplayBuildingName()
		{
			DataTable tb = (DataTable)Session["tbRoom"];

			try 
			{
				this.txtSelectBuildingCode.Value = BLL.ConvertRule.GetDistinctStr(tb, "BuildingCode", "", ",");
				this.txtSelectBuildingName.Value = BLL.ConvertRule.GetDistinctStr(tb, "BuildingName", "", ",");
				this.spanBuildingName.InnerText = this.txtSelectBuildingName.Value;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б�������" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ��������" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��˳�����" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "ȡ����˳�����" + ex.Message));
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
				SaveRoomArea();

				//�������޸ı����ʼ�ջص���ϸҳ��
				this.txtFromUrl.Value = "RoomIOInInfo.aspx?OutListCode=" + OutListCode;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���������" + ex.Message));
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

				//����
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
		/// ���淿�����
		/// </summary>
		private void SaveRoomArea()
		{
			try
			{
				DataTable tbBuilding = new DataTable();
				tbBuilding.Columns.Add("BuildingCode", typeof(string));

				for(int i=0;i<this.dgList.Items.Count;i++)
				{
					HtmlInputText txtBuildArea = (HtmlInputText)dgList.Items[i].FindControl("txtBuildArea");
					HtmlInputText txtRoomArea = (HtmlInputText)dgList.Items[i].FindControl("txtRoomArea");

					if ( txtBuildArea != null )
					{
						string RoomCode = this.dgList.DataKeys[i].ToString();

						EntityData entity = DAL.EntityDAO.ProductDAO.GetRoomByCode(RoomCode);
						DataRow dr;

						if (entity.HasRecord())
						{
							dr = entity.CurrentRow;

							//��¼¥�����
							string BuildingCode = BLL.ConvertRule.ToString(dr["BuildingCode"]);
							if (tbBuilding.Select("BuildingCode='" + BuildingCode + "'").Length == 0) 
							{
								DataRow drBuilding = tbBuilding.NewRow();
								drBuilding["BuildingCode"] = BuildingCode;
								tbBuilding.Rows.Add(drBuilding);
							}
						
							dr["BuildArea"] = BLL.ConvertRule.ToDecimalObj(txtBuildArea.Value);
							dr["RoomArea"] = BLL.ConvertRule.ToDecimalObj(txtRoomArea.Value);

							DAL.EntityDAO.ProductDAO.UpdateRoom(entity);							

						}

						entity.Dispose();
					}
					
				}

				//����¥����ʵ�����
				BLL.ProductRule.UpdateBuildingTotalRoomArea(tbBuilding);
			}
			catch(Exception ex)
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
		/// ѡ��¥�����غ󣬽���ѡ�ļ�¼���ӵ���Դ��ϸ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSelectBuildingReturn_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string codes = this.txtSelectBuildingCode.Value.Trim();
				string[] arrcode = codes.Split(",".ToCharArray());

				DataTable tb = (DataTable)Session["tbRoom"];

				//ɾ��ԭ���С���û�еķ�Դ
				int c = tb.Rows.Count;
				for(int i=c-1;i>=0;i--) 
				{
					DataRow dr = tb.Rows[i];
					string BuildingCode = BLL.ConvertRule.ToString(dr["BuildingCode"]);

					int p = BLL.ConvertRule.FindArray(arrcode, BuildingCode);
					if (p < 0) 
					{
						tb.Rows.Remove(dr);
					}
				}

				foreach(string code in arrcode) 
				{
					EntityData entity = DAL.EntityDAO.ProductDAO.GetV_ROOMByBuildingCode(code);

					foreach(DataRow dr in entity.CurrentTable.Rows) 
					{
						string RoomCode = dr["RoomCode"].ToString();

						//��鵱ǰ��ϸ���Ƿ����иü�¼
						if (tb.Select("RoomCode='" + RoomCode + "'").Length == 0) 
						{
							//������ϸ
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
					}

					entity.Dispose();
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

	}
}