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
	/// RoomIOModify 的摘要说明。
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
//				RmsPM.BLL.PageFacade.LoadDictionarySelect(this.sltOutAspect,"去向","");

				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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

						//记录老的值
						this.txtOldCodeName.Value = entity.GetString("CodeName");
						this.txtOldCurYear.Value = entity.GetInt("CurYear").ToString();

					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "单据不存在"));
						return;
					}
					entity.Dispose();

					//取房源列表
					entityDtl = DAL.EntityDAO.ProductDAO.GetRoomByOutListCode(OutListCode, "V_ROOM");
				}
				else 
				{
					//新增时的缺省值
					this.txtOutDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

					//新增时房源列表为空
					entityDtl = new EntityData("V_ROOM");
				}

				//删除、审核
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

				//隐藏“去向”
				if ((this.txtOutState.Value != "出库") && (this.txtOutState.Value != "调拨") && (this.txtOutState.Value != "预拨") )
				{
					this.spanOutAspect.Style["display"] = "none";
					this.spanOutAspect2.Style["display"] = "none";
				}

				//显示楼栋列表
				Session["tbRoom"] = entityDtl.CurrentTable;
				entityDtl.Dispose();
				BindDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示信息出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示楼栋列表
		/// </summary>
		private void BindDataGrid() 
		{
			try 
			{
				DataTable tb = (DataTable)Session["tbRoom"];

				//记录多条楼栋代码，用“,”分隔
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
				this.dgList.Columns[2].FooterText = "套数：" + tb.Rows.Count;
				this.dgList.Columns[5].FooterText = arrValue[0].ToString("0.####");
				this.dgList.Columns[6].FooterText = arrValue[1].ToString("0.####");
				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 删除单据
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
			}

			GoBack();
		}

		/// <summary>
		/// 审核单据
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "审核出错：" + ex.Message));
			}

			GoBack();
		}

		/// <summary>
		/// 取消审核单据
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "取消审核出错：" + ex.Message));
			}

			GoBack();
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.sltCodeName.Value.Trim() == "") 
			{
				Hint = "请输入产品性质";
				return false;
			}

			if (this.txtOutDate.Value.Trim() == "") 
			{
				Hint = "请输入" + this.spanOutDate.InnerText + "日期";
				return false;
			}

			if (this.spanOutAspect2.Style["display"] != "none") 
			{
				if (this.txtOutAspect.Value.Trim() == "") 
				{
					Hint = "请输入去向";
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 保存
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

				//新增时，或产品性质、年度改变时，自动生成编号
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

				//新增、修改保存后始终回到详细页面
				this.txtFromUrl.Value = "RoomIOInfo.aspx?OutListCode=" + OutListCode;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}

			GoBack();
		}

		/// <summary>
		/// 保存房源明细
		/// </summary>
		private void SaveRoom(string OutListCode) 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;

				//旧的明细
				EntityData entity = DAL.EntityDAO.ProductDAO.GetTempRoomStructureByOutListCode(OutListCode);

				//新的明细
				DataTable tb = (DataTable)Session["tbRoom"];

				//删除原先有现在没有的
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

				//添加
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
		/// 返回
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
				//缺省返回列表页面
				Response.Write(string.Format("window.location = '{0}';", "RoomIOList.aspx?ProjectCode=" + this.txtProjectCode.Value + "&IOType=" + BLL.ProductRule.TransTempRoomOutIOType(this.txtOutState.Value, false)));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}

		/// <summary>
		/// 删除列表的房源
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
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 选择房源返回后，将所选的记录添加到房源明细中
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
					//检查当前明细中是否已有该记录
					if (tb.Select("RoomCode='" + code + "'").Length == 0) 
					{
						//新增明细
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
				Response.Write(JavaScript.Alert(true, "失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 修改面积后，刷新房源明细
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
		/// 批量删除
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
