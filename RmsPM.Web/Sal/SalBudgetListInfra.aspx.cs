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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.Sal
{
	/// <summary>
	/// SalBudgetListInfra 的摘要说明。
	/// </summary>
    public partial class SalBudgetListInfra : PageBase
	{
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				IniYear();
				LoadData();
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
			this.UltraWebGrid1.InitializeRow += new Infragistics.WebUI.UltraWebGrid.InitializeRowEventHandler(this.UltraWebGrid1_InitializeRow);
			this.btnHiddenYear.ServerClick += new System.EventHandler(this.btnHiddenYear_ServerClick);
			this.btnAddBudget.ServerClick += new System.EventHandler(this.btnAddBudget_ServerClick);
			this.btnModifyBudget.ServerClick += new System.EventHandler(this.btnModifyBudget_ServerClick);
			this.btnSave.ServerClick += new System.EventHandler(this.btnSave_ServerClick);
			this.btnCancel.ServerClick += new System.EventHandler(this.btnCancel_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtProjectName.Value = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);

				//权限
				this.btnAddBudget.Visible = user.HasRight("020402");
				this.btnModifyBudget.Visible = user.HasRight("020403");
				this.btnModifyAct.Visible = user.HasRight("020403");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void IniYear() 
		{
			try 
			{
				string CurrYear = this.sltYear.Value.Trim();
				int iCurrYear;

				if (CurrYear == "") 
				{
					iCurrYear = DateTime.Today.Year;
				}
				else 
				{
					iCurrYear = BLL.ConvertRule.ToInt(CurrYear);
				}

				this.sltYear.Items.Clear();

				//前几年
				for (int i=-5;i<0;i++) 
				{
					int year = iCurrYear + i;
					this.sltYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
				}

				//当年、后几年
				for (int i=0;i<6;i++) 
				{
					int year = iCurrYear + i;
					this.sltYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
				}

				this.sltYear.Value = iCurrYear.ToString();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化年度出错：" + ex.Message));
			}
		}

		private void ClearAll() 
		{
			try 
			{
				this.lblPeriodMonthDesc.Text = "";
				this.lblAfterPeriodDesc.Text = "";
				this.lblModiPersonName.Text = "";
				this.lblModiDate.Text = "";

				DataTable tb = BLL.SalBudgetRule.GenerateSalBudgetDtlTableInfra("", 0);
				BindDataGrid(tb);
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		private void LoadData() 
		{
			try 
			{
				ClearAll();

				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.sltYear.Value);

				this.lblBudgetName.Text = string.Format("{1}{0}年度销售计划", IYear, this.txtProjectName.Value);

				EntityData entity = DAL.EntityDAO.SalDAO.GetSalBudgetByProjectYear(ProjectCode, IYear);
				bool isExists = entity.HasRecord();

				if (entity.HasRecord()) 
				{
					DataRow dr = entity.CurrentRow;
					this.lblPeriodMonthDesc.Text = BLL.SalRule.GetSalBudgetPeriodMonthDesc(dr);
					this.lblAfterPeriodDesc.Text = BLL.SalRule.GetSalBudgetAfterPeriodDesc(dr);

					this.lblModiPersonName.Text = BLL.SystemRule.GetUserName( entity.GetString("ModiPerson"));
					this.lblModiDate.Text = entity.GetDateTimeOnlyDate("ModiDate");
				}

				if (isExists) 
				{
					this.btnAddBudget.Style["display"] = "none";
					this.btnModifyBudget.Style["display"] = "";
				}
				else 
				{
					this.btnAddBudget.Style["display"] = "";
					this.btnModifyBudget.Style["display"] = "none";
				}

				entity.Dispose();

				if (isExists) 
				{
					LoadDataGrid();
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度计划出错：" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.sltYear.Value);

				DataTable tb = BLL.SalBudgetRule.GenerateSalBudgetDtlTableInfra(ProjectCode, IYear);
				BindDataGrid(tb);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度计划明细出错：" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
				DataView dv = new DataView(tb, "", "sno, IsAct", DataViewRowState.CurrentRows);

				this.UltraWebGrid1.DataSource = dv;
				this.UltraWebGrid1.DataBind();

				string year = this.sltYear.Value;
				int iyear = BLL.ConvertRule.ToInt(this.sltYear.Value);

				//标题显示年份
				for(int i=1;i<=12;i++)
				{
					int pos = this.UltraWebGrid1.Columns.IndexOf("m" + i.ToString());
					if (pos >= 0)
					{
						this.UltraWebGrid1.Columns[pos].Header.Caption = year + "<br>" + i.ToString();
					}

				}

				this.UltraWebGrid1.Columns.FromKey("m0").Header.Caption = year + "<br>年度计划";
				
				for(int i=1;i<=2;i++)
				{
					int pos = this.UltraWebGrid1.Columns.IndexOf("y" + i.ToString());
					if (pos >= 0)
					{
						this.UltraWebGrid1.Columns[pos].Header.Caption = string.Format("{0}<br>年度计划", iyear + i);
					}

				}
				

//				this.UltraWebGrid1.Rows[0].Cells[4].ColSpan=2;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示年度计划明细出错：" + ex.Message));
			}
		}

		private void btnHiddenYear_ServerClick(object sender, System.EventArgs e)
		{
			IniYear();
			LoadData();
		}

		private void Save()
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.sltYear.Value);
				string BudgetCode = "";

				//保存主表
				EntityData entity = DAL.EntityDAO.SalDAO.GetSalBudgetByProjectYear(ProjectCode, IYear);
				DataRow dr = null;
				bool isNew = !entity.HasRecord();

				if (isNew) 
				{
					dr = entity.CurrentTable.NewRow();

					BudgetCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SalBudgetCode");
					dr["BudgetCode"] = BudgetCode;
					dr["ProjectCode"] = ProjectCode;
					dr["IYear"] = IYear;
					dr["StartMonth"] = 1;
					dr["PeriodMonth"] = 12;
					dr["AfterPeriod"] = 2;

					entity.CurrentTable.Rows.Add(dr);
				}
				else 
				{
					dr = entity.CurrentRow;
					BudgetCode = dr["BudgetCode"].ToString();
				}

				dr["ModiDate"] = DateTime.Now;
				dr["ModiPerson"] = base.user.UserCode;

				DAL.EntityDAO.SalDAO.SubmitAllSalBudget(entity);

				entity.Dispose();

				//保存明细
				//保存当年
				EntityData entityDtl = DAL.EntityDAO.SalDAO.GetSalBudgetDtlByProjectYear(ProjectCode, IYear);
				for(int m=0;m<=12;m++) 
				{
					SaveOneYm(BudgetCode, entityDtl, IYear, m, "m" + m.ToString());
				}
				DAL.EntityDAO.SalDAO.SubmitAllSalBudgetDtl(entityDtl);
				entityDtl.Dispose();


				//保存后几年
				for(int i=1;i<=2;i++) 
				{
					entityDtl = DAL.EntityDAO.SalDAO.GetSalBudgetDtlByProjectYear(ProjectCode, IYear);
					int y = IYear + i;

					SaveOneYm(BudgetCode, entityDtl, y, 0, "y" + i.ToString());

					DAL.EntityDAO.SalDAO.SubmitAllSalBudgetDtl(entityDtl);
					entityDtl.Dispose();
				}

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void SaveOneYm(string BudgetCode, EntityData entity, int y, int m, string ColumnName) 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;

				DataRow[] drs = entity.CurrentTable.Select("IYear=" + y.ToString() + " and IMonth=" + m.ToString());
				DataRow dr;
				bool isNew = (drs.Length == 0);

				if (isNew) 
				{
					dr = entity.CurrentTable.NewRow();
					dr["BudgetCode"] = BudgetCode;
					dr["SystemID"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SalBudgetSystemID");
					dr["ProjectCode"] = ProjectCode;
					dr["IYear"] = y;
					dr["IMonth"] = m;

					entity.CurrentTable.Rows.Add(dr);
				}
				else 
				{
					dr = drs[0];
				}

				int iCount = this.UltraWebGrid1.Rows.Count;
				for(int i=0;i<iCount;i++) 
				{
					int IsAct = BLL.ConvertRule.ToInt(this.UltraWebGrid1.Rows[i].Cells[this.UltraWebGrid1.Columns.IndexOf("IsAct")].Value);

					if (IsAct == 0)
					{
						string FieldName = (string)this.UltraWebGrid1.Rows[i].Cells[this.UltraWebGrid1.Columns.IndexOf("FieldName")].Value;
						string val = (string)this.UltraWebGrid1.Rows[i].Cells[this.UltraWebGrid1.Columns.IndexOf(ColumnName)].Value;

						dr[FieldName] = BLL.ConvertRule.ToDecimalObj(val);
					}
				}
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		private string CheckValidVal(string val, string FieldName)
		{
			string Hint = "";

			if ( val != "" )
			{
				if (FieldName.ToLower() == "housecount")
				{
					if ( ! Rms.Check.StringCheck.IsInt(val))
					{
						Hint = string.Format("“{0}”不是有效的整数，请检查 ！ ", val);
						return Hint;
					}
				}
				else 
				{
					if ( ! Rms.Check.StringCheck.IsNumber(val))
					{
						Hint = string.Format("“{0}”不是有效的数值，请检查 ！ ", val);
						return Hint;
					}
				}
			}

			return Hint;
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			int iCount = this.UltraWebGrid1.Rows.Count;
			for(int i=0;i<iCount;i++) 
			{
				int IsAct = BLL.ConvertRule.ToInt(this.UltraWebGrid1.Rows[i].Cells[this.UltraWebGrid1.Columns.IndexOf("IsAct")].Value);

				if (IsAct == 0)
				{
					string FieldName = (string)this.UltraWebGrid1.Rows[i].Cells[this.UltraWebGrid1.Columns.IndexOf("FieldName")].Value;

					for(int k=0;k<=12;k++)
					{
						string val = ((string)this.UltraWebGrid1.Rows[i].Cells[this.UltraWebGrid1.Columns.IndexOf("m" + k.ToString())].Value).Trim();

						Hint = CheckValidVal(val, FieldName);
						if (Hint != "") return false;
					}

					for(int k=1;k<=2;k++)
					{
						string val = ((string)this.UltraWebGrid1.Rows[i].Cells[this.UltraWebGrid1.Columns.IndexOf("y" + k.ToString())].Value).Trim();

						Hint = CheckValidVal(val, FieldName);
						if (Hint != "") return false;
					}
				}

			}

			return true;
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				Save();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存计划出错：" + ex.Message));
				return;
			}

			this.UltraWebGrid1.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			LoadDataGrid();
			SetToolbar();
		}

		private void SetToolbar()
		{
			if (this.UltraWebGrid1.DisplayLayout.AllowUpdateDefault == Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes) 
			{
				this.trToolbarView.Style["display"] = "none";
				this.trToolbarSave.Style["display"] = "";
			}
			else
			{
				this.trToolbarView.Style["display"] = "";
				this.trToolbarSave.Style["display"] = "none";
			}
		}

		/// <summary>
		/// 修改计划
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnModifyBudget_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				this.UltraWebGrid1.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
				LoadDataGrid();

				SetToolbar();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "修改计划出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 返回
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				this.UltraWebGrid1.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
				SetToolbar();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "返回出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 新增计划
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddBudget_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				this.UltraWebGrid1.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
				LoadDataGrid();

				SetToolbar();

				//新增时的缺省值
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.sltYear.Value);

				EntityData entity = DAL.EntityDAO.SalDAO.GetSalBudgetByProjectYear(ProjectCode, IYear);
				DataRow dr = entity.CurrentTable.NewRow();
				dr["IYear"] = IYear;
				dr["StartMonth"] = 1;
				dr["PeriodMonth"] = 12;
				dr["AfterPeriod"] = 2;

				this.lblPeriodMonthDesc.Text = BLL.SalRule.GetSalBudgetPeriodMonthDesc(dr);
				this.lblAfterPeriodDesc.Text = BLL.SalRule.GetSalBudgetAfterPeriodDesc(dr);

				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "新增计划出错：" + ex.Message));
			}
		}

		private void UltraWebGrid1_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
		{
			if (this.UltraWebGrid1.DisplayLayout.AllowUpdateDefault == Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes)
			{
				//可修改了单元格变颜色
				int IsAct = BLL.ConvertRule.ToInt(e.Row.Cells.FromKey("IsAct").Value);
				if (IsAct == 0)
				{
					for(int i=0;i<=12;i++)
					{
						e.Row.Cells.FromKey("m" + i.ToString()).Style.BackColor = Color.FromArgb(255, 255, 205);
					}

					for(int i=1;i<=2;i++)
					{
						e.Row.Cells.FromKey("y" + i.ToString()).Style.BackColor = Color.FromArgb(255, 255, 205);
					}
				}
			}
		}

	}
}
