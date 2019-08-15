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
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using RmsPM.BLL;

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// AnnualPlanModify 的摘要说明。
	/// </summary>
	public partial class AnnualPlanModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCurrentFloor;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtAnnualPlanCode.Value = Request.QueryString["AnnualPlanCode"];
				this.txtAct.Value = Request.QueryString["action"];
				this.txtPBSUnitCode.Value = Request.QueryString["PBSUnitCode"];
				this.txtIYear.Value = Request.QueryString["IYear"];
				int year;

				PageFacade.LoadVisualProgressSelect(sltVisualProgress,"");

				switch (this.txtAct.Value.ToLower()) 
				{
					case "insert":  //新增
						LoadPBSUnit();

						//新增时，从最新的年度计划复制
						EntityData entity = DAL.EntityDAO.ConstructDAO.GetCurrConstructAnnualPlanByPBSUnit(this.txtPBSUnitCode.Value);
						if (entity.HasRecord()) 
						{
							DataRow dr = entity.CurrentRow;
							year = entity.GetInt("IYear") + 1;

							//计算上年结转面积
							this.txtLCFArea.Value = BLL.MathRule.GetDecimalShowString(dr["LCFArea"]);

//							this.txtInvestBefore.Value = BLL.MathRule.GetDecimalShowString(dr["InvestBefore"]);

							this.txtPInvest.Value = BLL.MathRule.GetDecimalShowString(dr["PInvest"]);
							this.sltVisualProgress.Value = entity.GetString("VisualProgress");

							LoadDataGrid(entity.GetInt("IYear"), true);
						}
						else 
						{
							//无年度计划时，年度为当前年度 
							year = DateTime.Today.Year;
							LoadDataGrid(year, false);
						}
						entity.Dispose();

						//按单位工程的当前形象进度，计算上年末完成投资
						decimal InvestBefore = BLL.ConstructRule.CalcPBSUnitCompleteInvest(txtPBSUnitCode.Value);
						this.txtInvestBefore.Value = BLL.MathRule.GetDecimalShowString(InvestBefore);

						this.txtIYear.Value = year.ToString();
						this.lblYear.Text  = this.txtIYear.Value;

						break;

					case "modify":  //修改
						LoadData();
						LoadPBSUnit();
						LoadDataGrid(int.Parse(this.txtIYear.Value), false);
						break;

					case "delete":  //删除
						DoDelete();
						break;

					default:
						Response.Write(Rms.Web.JavaScript.Alert(true, "未定义的参数类型"));
						Response.End();
						break;
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 删除
		/// </summary>
		private void DoDelete()
		{
			try
			{
				string code = this.txtAnnualPlanCode.Value;
				BLL.ConstructRule.DeleteConstructAnnualPlan(code);

				GoBack();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"删除年度计划出错");
			}
		}

		private void LoadPBSUnit()
		{
			string code =  this.txtPBSUnitCode.Value;

			try
			{
				if (code != "") 
				{
					EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(code);

					if ( entity.HasRecord())
					{
						this.txtPBSUnitCode.Value = entity.GetString("PBSUnitCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
					}
					else
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "单位工程不存在"));
						return;
					}

					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "单位工程编号不能为空"));
					return;
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载单位工程失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载单位工程失败：" + ex.Message));
			}
		}

		private void LoadData()
		{
			string PBSUnitCode =  this.txtPBSUnitCode.Value;
			int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);

			this.lblYear.Text  = this.txtIYear.Value;

			try
			{
				if ((PBSUnitCode != "") && (IYear != 0))
				{
					EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructAnnualPlanByPBSUnitYear(PBSUnitCode, IYear);

					if ( entity.HasRecord())
					{
						DataRow dr = entity.CurrentRow;

						this.txtAnnualPlanCode.Value = entity.GetString("AnnualPlanCode");
						this.txtPBSUnitCode.Value = entity.GetString("PBSUnitCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.txtIYear.Value = entity.GetInt("IYear").ToString();
						this.lblYear.Text  = this.txtIYear.Value;

						this.txtLCFArea.Value = BLL.MathRule.GetDecimalShowString(dr["LCFArea"]);
						this.txtPInvest.Value = BLL.MathRule.GetDecimalShowString(dr["PInvest"]);
						this.txtInvestBefore.Value = BLL.MathRule.GetDecimalShowString(dr["InvestBefore"]);
						this.sltVisualProgress.Value = entity.GetString("VisualProgress").ToString();

						this.txtCurrentFloor.Value = BLL.MathRule.GetIntShowString(dr["CurrentFloor"]);
					}

					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "单位工程编号和年度不能为空"));
					return;
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载年度计划失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载年度计划失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示计划明细
		/// </summary>
		private void LoadDataGrid(int year, bool isCopy)
		{
			string PBSUnitCode =  this.txtPBSUnitCode.Value;

			try
			{
				DataTable tb = BLL.ConstructRule.GenerateConstructPlanTable(PBSUnitCode, year, isCopy);

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载年度计划失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载年度计划失败：" + ex.Message));
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);

		}
		#endregion

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if(sltVisualProgress.Value=="")
			{
				Hint = "请选择计划形象进度 ！ ";
				return false;
			}

			if ( txtInvestBefore.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsNumber(txtInvestBefore.Value))
				{
					Hint = "上年末完成投资必须是数值 ！ ";
					return false;
				}
			}

			if ( txtLCFArea.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsNumber(txtLCFArea.Value))
				{
					Hint = "上年结转面积必须是数值 ！ ";
					return false;
				}
			}

			if ( txtPInvest.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsNumber(txtPInvest.Value))
				{
					Hint = "本年计划投资必须是数值 ！ ";
					return false;
				}
			}

//			if (sltVisualProgress.Value == "结构")
//			{
//				if (this.txtCurrentFloor.Value == "") 
//				{
//					Hint = "请输入计划施工层数 ！ ";
//					return false;
//				}

			if ( txtCurrentFloor.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(txtCurrentFloor.Value))
				{
					Hint = "计划施工层数必须是整数 ！ ";
					return false;
				}
			}

			string tempHint = BLL.PBSRule.CheckPBSUnitFloorCount(this.txtPBSUnitCode.Value, txtCurrentFloor.Value, "计划施工层数");
			if (tempHint != "") 
			{
				Hint = tempHint;
				return false;
			}

//			}

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
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

				string PBSUnitCode = this.txtPBSUnitCode.Value;
				string code = this.txtAnnualPlanCode.Value;
				EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructAnnualPlanByCode(code);
				bool isNew = ( !entity.HasRecord() );
				
				DataRow dr = null;
				if ( isNew )
				{
					dr = entity.GetNewRecord();
					code = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("AnnualPlanCode");
					dr["AnnualPlanCode"] = code;
					dr["PBSUnitCode"] = PBSUnitCode;
					dr["ProjectCode"] = this.txtProjectCode.Value;
					dr["IYear"] = this.txtIYear.Value;
				}
				else
				{
					dr = entity.CurrentRow; 
				}

				dr["PlanDate"] = DateTime.Now;
				dr["PlanPerson"] = base.user.UserCode;

				string VisualProgress = sltVisualProgress.Value;
				dr["VisualProgress"] = VisualProgress;

				int CurrentFloor = BLL.ConvertRule.ToInt(this.txtCurrentFloor.Value);
				dr["CurrentFloor"] = CurrentFloor;

				decimal InvestBefore = BLL.ConvertRule.ToDecimal(this.txtInvestBefore.Value);
				dr["InvestBefore"] = InvestBefore;

				dr["LCFArea"] = BLL.ConvertRule.ToDecimalObj(this.txtLCFArea.Value);

				//本年计划投资自动计算
				EntityData entityU = DAL.EntityDAO.PBSDAO.GetV_PBSUnitByCode(PBSUnitCode);
				if (entityU.HasRecord()) 
				{
					decimal TotalInvest = entityU.GetDecimal("PInvest");
					int TotalFloorCount = BLL.PBSRule.GetPBSUnitFloorCount(PBSUnitCode);

					//本年末总投资
					decimal CurrTotalInvest = BLL.ConstructRule.CalcInvestByVisualProgress(TotalInvest, VisualProgress, TotalFloorCount, CurrentFloor);

					//本年投资 = 本年末总投资 - 上年末投资
					decimal CurrYearInvest = CurrTotalInvest - InvestBefore;
					dr["PInvest"] = CurrYearInvest;
				}
				entityU.Dispose();

//				dr["PInvest"] = BLL.ConvertRule.ToDecimalObj(this.txtPInvest.Value);

				if ( isNew )
				{
					entity.AddNewRecord(dr);
					DAL.EntityDAO.ConstructDAO.InsertConstructAnnualPlan(entity);
				}
				else
				{
					DAL.EntityDAO.ConstructDAO.UpdateConstructAnnualPlan(entity);
				}

				entity.Dispose();

				SaveDtl();

				GoBack();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 保存计划明细
		/// </summary>
		private void SaveDtl() 
		{
			try 
			{
				string PBSUnitCode = this.txtPBSUnitCode.Value;
				int year = int.Parse(this.txtIYear.Value);

				int iCount = this.dgList.Items.Count;
				for(int i=0;i<iCount;i++) 
				{
					string code = this.dgList.DataKeys[i].ToString();
					HtmlInputHidden txtVisualProgress = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtVisualProgress");
					HtmlInputHidden txtVisualProgressName = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtVisualProgressName");
					HtmlInputHidden txtProgressType = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtProgressType");
					HtmlInputHidden txtIsPoint = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtIsPoint");
					AspWebControl.Calendar txtStartDate = (AspWebControl.Calendar)this.dgList.Items[i].FindControl("txtStartDate");
					AspWebControl.Calendar txtEndDate = (AspWebControl.Calendar)this.dgList.Items[i].FindControl("txtEndDate");

					EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructPlanStepByCode(code);
					DataRow dr;
					bool isNew = !entity.HasRecord();

					if (isNew) 
					{
						dr = entity.CurrentTable.NewRow();
						dr["ConstructPlanStepCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ConstructPlanStepCode");
					}
					else 
					{
						dr = entity.CurrentRow;
					}

					dr["PBSUnitCode"] = PBSUnitCode;
					dr["IYear"] = year;
					dr["VisualProgress"] = txtVisualProgress.Value;
					dr["StartDate"] = BLL.ConvertRule.ToDate(txtStartDate.Value);

					//只填开始日期时，结束日期自动填开始日期
					if (txtIsPoint.Value == "1") 
					{
						dr["EndDate"] = dr["StartDate"];
					}
					else 
					{
						dr["EndDate"] = BLL.ConvertRule.ToDate(txtEndDate.Value);
					}

					if (isNew) 
					{
						entity.CurrentTable.Rows.Add(dr);
						DAL.EntityDAO.ConstructDAO.InsertConstructPlanStep(entity);
					}
					else 
					{
						DAL.EntityDAO.ConstructDAO.UpdateConstructPlanStep(entity);
					}

					entity.Dispose();
				}
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
//			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) 
//			{
//				AspWebControl.Calendar txt = (AspWebControl.Calendar)e.Item.FindControl("txtStartDate");
//				txt.ID = "txtStartDate" + e.Item.ItemIndex.ToString();
//			}
		}
	}
}
