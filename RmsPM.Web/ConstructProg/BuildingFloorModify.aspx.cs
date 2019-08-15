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

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// BuildingFloorModify 的摘要说明。
	/// </summary>
	public partial class BuildingFloorModify: PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPBSUnitCode;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanYear;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];
				this.txtAct.Value = Request.QueryString["action"];

				if (this.txtBuildingCode.Value.Trim() != "") 
				{
					EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(txtBuildingCode.Value);
					if (entity.HasRecord()) 
					{
						this.lblBuildingName.Text = entity.GetString("BuildingName");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.lblFloorCount.Text = entity.GetIntString("IFloorCount");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "楼栋不存在"));
					}
					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入楼栋代码"));
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFloorByBuildingCode(this.txtBuildingCode.Value);
				entity.Dispose();

				DataTable tb = entity.CurrentTable;
				if (tb.Rows.Count == 0) 
				{
					//缺省生成
					BLL.ConstructProgRule.MadeDefultBuildingFloor(tb, this.txtBuildingCode.Value);
					this.txtDetailSno.Value = tb.Rows.Count.ToString();
				}

				BindDataGrid(tb);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb) 
		{
			try 
			{
				//按楼层序号倒排序
				DataView dv = new DataView(tb, "", "FloorIndex desc", DataViewRowState.CurrentRows);
				this.dgList.DataSource = dv;
				this.dgList.DataBind();
			}
			catch (Exception ex) 
			{
				throw ex;
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
			this.dgList.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_EditCommand);
			this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);

		}
		#endregion

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
		/// 保存楼层结构
		/// </summary>
		private void SaveDtl() 
		{
			try 
			{
				DataTable tbTemp = ScreenToTable(true);

				ResetFloorIndex(tbTemp);

				string ProjectCode = this.txtProjectCode.Value;
				string BuildingCode = this.txtBuildingCode.Value;

				//旧的明细
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFloorByBuildingCode(BuildingCode);
				DataTable tb = entity.CurrentTable;

				//删除原先有现在没有的
				foreach(DataRow dr in tb.Rows) 
				{
					string BuildingFloorCode = dr["BuildingFloorCode"].ToString();
					if (tbTemp.Select("BuildingFloorCode='" + BuildingFloorCode + "'").Length == 0) 
					{
						dr.Delete();
					}
				}

				//新增或修改
				foreach(DataRow drTemp in tbTemp.Rows)
				{
					string BuildingFloorCode = drTemp["BuildingFloorCode"].ToString();
					DataRow drNew;
					DataRow[] drs;

					drs = tb.Select("BuildingFloorCode='" + BuildingFloorCode + "'");

					if (drs.Length == 0) 
					{
						drNew = tb.NewRow();

						BuildingFloorCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BuildingFloorCode");
						drNew["BuildingFloorCode"] = BuildingFloorCode;
						drNew["BuildingCode"] = BuildingCode;
						drNew["ProjectCode"] = ProjectCode;

						tb.Rows.Add(drNew);
					}
					else 
					{
						drNew = drs[0];
					}

					drNew["FloorIndex"] = drTemp["FloorIndex"];
					drNew["FloorName"] = drTemp["FloorName"];
				}

				DAL.EntityDAO.ProductDAO.SubmitAllBuildingFloor(entity);
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		/// <summary>
		/// 屏幕数据保存到临时表
		/// </summary>
		/// <returns></returns>
		private DataTable ScreenToTable(bool isBindGrid) 
		{
			try 
			{
				string BuildingCode = this.txtBuildingCode.Value;

				EntityData entity = new EntityData("BuildingFloor");
				DataTable tb = entity.CurrentTable;
				entity.Dispose();

				int i = -1;
				foreach (DataGridItem item in this.dgList.Items)
				{
					i++;

					string BuildingFloorCode = this.dgList.DataKeys[i].ToString();
					HtmlInputText txtFloorName = (HtmlInputText)item.FindControl("txtFloorName");

					DataRow dr = tb.NewRow();

					dr["BuildingFloorCode"] = BuildingFloorCode;
					dr["FloorName"] = txtFloorName.Value;

					tb.Rows.Add(dr);
				}

				if (isBindGrid) 
				{
					BindDataGrid(tb);
				}

				return tb;
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			DataTable tbName = new DataTable();
			tbName.Columns.Add("FloorName", typeof(string));

			int i = -1;
			foreach (DataGridItem item in this.dgList.Items)
			{
				i++;

				HtmlInputText txtFloorName = (HtmlInputText)item.FindControl("txtFloorName");

				if ( txtFloorName.Value.Trim() == "" )
				{
					Hint = "请输入楼层名称 ！ ";
					return false;
				}

				if (tbName.Select("FloorName = '" + txtFloorName.Value + "'").Length > 0) 
				{
					Hint = string.Format("楼层名称“{0}”重复", txtFloorName.Value);
					return false;
				}

				DataRow drName = tbName.NewRow();
				drName["FloorName"] = txtFloorName.Value;
				tbName.Rows.Add(drName);
			}

			return true;
		}

		/// <summary>
		/// 按当前数据表重新排楼层序号
		/// </summary>
		/// <param name="tb"></param>
		private void ResetFloorIndex(DataTable tb)
		{
			try 
			{
				int FloorIndex = 0;
				int count = tb.Rows.Count;
				for(int i=count-1;i>=0;i--)
				{
					DataRow dr = tb.Rows[i];
					FloorIndex++;

					dr["FloorIndex"] = FloorIndex;
				}
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// 新增一条明细
		/// </summary>
		/// <param name="tb"></param>
		private void AddDtl(DataTable tb, int pos, bool isResetFloorIndex) 
		{
			try 
			{
				DataRow dr = tb.NewRow();

				int sno = BLL.ConvertRule.ToInt(this.txtDetailSno.Value.Trim()) + 1;
				this.txtDetailSno.Value = sno.ToString();

				dr["BuildingFloorCode"] = -sno;

				if (pos >= 0) 
				{
					tb.Rows.InsertAt(dr, pos);
				}
				else 
				{
					tb.Rows.Add(dr);
				}

				if (isResetFloorIndex) 
				{
					ResetFloorIndex(tb);
				}
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// 新增底层
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnAddDtl_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				DataTable tb = ScreenToTable(false);
				if (tb == null) return;

				AddDtl(tb, -1, true);
				BindDataGrid(tb);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "新增底层失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 删除明细
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try 
			{
				string code = this.dgList.DataKeys[e.Item.ItemIndex].ToString();

				DataTable tb = ScreenToTable(false);
				if (tb == null) return;

				//检查是否可删除
				if (code.Substring(0, 1) != "-") 
				{
					string hint = BLL.ConstructProgRule.CanDeleteBuildingFloorByCode(code);
					if (hint != "") 
					{
						Response.Write(JavaScript.Alert(true, hint));
						return;
					}
				}

				DataRow[] drs = tb.Select("BuildingFloorCode='" + code + "'");
				if (drs.Length > 0) 
				{
					tb.Rows.Remove(drs[0]);
				}

				BindDataGrid(tb);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "删除明细失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 插入楼层
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgList_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try 
			{
				DataTable tb = ScreenToTable(false);
				if (tb == null) return;

				int i = e.Item.ItemIndex;

				AddDtl(tb, i, true);
				BindDataGrid(tb);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "插入楼层失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
