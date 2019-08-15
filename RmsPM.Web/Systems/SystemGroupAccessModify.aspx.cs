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
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// SystemGroupAccessModify 的摘要说明。
	/// </summary>
	public partial class SystemGroupAccessModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnHiddenBatchDelete;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadData();
			}
			else 
			{
				SaveScreenToSession(true);
			}
		}

		private void IniPage() 
		{
			try 
			{
//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtGroupCode.Value = Request.QueryString["GroupCode"];
				this.txtClassCode.Value = Request.QueryString["ClassCode"];
				this.txtAct.Value = Request.QueryString["Action"];
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
				string GroupCode = this.txtGroupCode.Value;
				string ClassCode = this.txtClassCode.Value;

				if (GroupCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入类别代码"));
					return;
				}

				if (ClassCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入大类代码"));
					return;
				}

				if (ClassCode == "0401") 
				{
					//费用项
					this.txtIsResource.Value = "1";
					EntityData entity = DAL.EntityDAO.CBSDAO.GetCBSByCode(GroupCode);
					if (entity.HasRecord())
					{
						this.lblGroupName.Text = entity.GetString("SortID") + " " + entity.GetString("CostName") ;
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "费用项不存在"));
						return;
					}
					entity.Dispose();

					string ClassName = BLL.SystemRule.GetFunctionStructureName(this.txtClassCode.Value);
					this.lblParentName.Text = BLL.SystemGroupRule.GetSystemGroupFullNameEx("", ClassName);
				}
				else if (ClassCode == "1603") 
				{
					//档案
					EntityData entity = DAL.EntityDAO.OADAO.GetOAFileTypeByCode(GroupCode);
					if (entity.HasRecord())
					{
						this.lblGroupName.Text =entity.GetString("TypeName") ;
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "档案类别项不存在"));
						return;
					}
					entity.Dispose();

					string ClassName = BLL.SystemRule.GetFunctionStructureName(this.txtClassCode.Value);
					this.lblParentName.Text = BLL.SystemGroupRule.GetSystemGroupFullNameEx("", ClassName);
				}
				else 
				{
					//显示类别信息
					EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByCode(GroupCode);
					if (entity.HasRecord())
					{
						this.lblGroupName.Text = entity.GetString("GroupName");
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "类别不存在"));
						return;
					}
					entity.Dispose();

					/*
					//显示该类别的所有用户
					EntityData entityAccess = DAL.EntityDAO.ResourceDAO.GetAccessRangeByGroupCode(GroupCode);
					DataView dv = new DataView(entityAccess.CurrentTable);
					entityAccess.Dispose();

					string UserCodes = "";
					string StationCodes = "";
					BLL.SystemRule.GetStationUserGroup(dv, "AccessRangeType", "RelationCode", "0", "1", ref UserCodes, ref StationCodes);

					this.ucRelation.UserCodes = UserCodes;
					this.ucRelation.StationCodes = StationCodes;
					*/

					string ClassName = BLL.SystemRule.GetFunctionStructureName(this.txtClassCode.Value);
					string ParentName = BLL.SystemGroupRule.GetSystemGroupFullName(this.txtParentCode.Value);
					this.lblParentName.Text = BLL.SystemGroupRule.GetSystemGroupFullNameEx(ParentName, ClassName);
				}

				LoadOperationList();
				LoadAccessRange();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// 显示权限列表
		/// </summary>
		private void LoadAccessRange() 
		{
			try 
			{
				string GroupCode = this.txtGroupCode.Value;
				EntityData entity = null;

				//所有权限范围-操作
				if (this.txtIsResource.Value == "1") 
				{
					entity = DAL.EntityDAO.ResourceDAO.GetAccessRangeByResourceCode(GroupCode);
				}
				else
				{
					entity = DAL.EntityDAO.ResourceDAO.GetAccessRangeByGroupCode(GroupCode);
				}
				DataTable tb = entity.CurrentTable;

				//按权限范围汇总
				DataTable tbRela = BLL.SystemGroupRule.GetSystemAccessDistinctRelation(tb);

				BLL.SystemGroupRule.AddSystemGroupAccessRelationName(tbRela);
				BLL.SystemGroupRule.AddSystemGroupAccessRelationImage(tbRela);

				Session["entity"] = entity;
				Session["tbRela"] = tbRela;

				BindDataGrid();

				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示岗位权限出错：" + ex.Message));
			}
		}

		private void BindDataGrid()
		{
			try 
			{
				DataTable tb = (DataTable)Session["tbRela"];

				DataView dv = new DataView(tb, "", "AccessRangeType, RelationName", DataViewRowState.CurrentRows);
				this.txtDetailCount.Value = dv.Count.ToString();

				this.dgList.DataSource = dv;
				this.dgList.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示岗位权限列表出错：" + ex.Message));
			}
		}

		private void LoadOperationList() 
		{
			try 
			{
				string ClassCode = this.txtClassCode.Value;
				EntityData entity = null;

				if (ClassCode == "0401") //费用项
				{
					/*
					FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
					sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsAvailable, "0" )  );
					sb.AddOrder("FunctionStructureCode", true);
					string sql = sb.BuildMainQueryString();
					*/

					string sql = "select * from FunctionStructure where FunctionStructureCode like '040%' and Deep = 3 and IsAvailable = 0 and IsRightControlPoint = 0";

					QueryAgent qa = new QueryAgent();
					entity = qa.FillEntityData("FunctionStructure", sql);
					qa.Dispose();

//					entity = DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByParentCode(ClassCode.Substring(0, 2));
				}
				else if (ClassCode == "0411") //预算表
				{
					/*
					FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
					sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsAvailable, "0" )  );
					sb.AddOrder("FunctionStructureCode", true);
					string sql = sb.BuildMainQueryString();
					*/

					string sql = "select * from FunctionStructure where FunctionStructureCode like '041%' and Deep = 3 and IsAvailable = 0 and IsRightControlPoint = 0";

					QueryAgent qa = new QueryAgent();
					entity = qa.FillEntityData("FunctionStructure", sql);
					qa.Dispose();

					//					entity = DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByParentCode(ClassCode.Substring(0, 2));
				}
				else 
				{
					FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
					sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.ParentCode, ClassCode )  );
					sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsAvailable, "0" )  );
					sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsRightControlPoint, "0" )  );
					sb.AddOrder("FunctionStructureCode", true);
					string sql = sb.BuildMainQueryString();

					QueryAgent qa = new QueryAgent();
					entity = qa.FillEntityData("FunctionStructure", sql);
					qa.Dispose();

//					entity = DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByParentCode(ClassCode);
				}

				//功能点代码、名称复制到操作代码、名称
				entity.CurrentTable.Columns.Add("OperationCode", typeof(string));
				entity.CurrentTable.Columns.Add("OperationName", typeof(string));

				foreach(DataRow dr in entity.CurrentTable.Rows) 
				{
					dr["OperationCode"] = dr["FunctionStructureCode"];
					dr["OperationName"] = dr["FunctionStructureName"];
				}

				entity.Dispose();

				Session["entityOperation"] = entity;
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "取操作列表出错：" + ex.Message));
			}
		}

		public DataTable GetOperationDataSource(string SystemID, string AccessRangeType, string RelationCode) 
		{
			try 
			{
				EntityData entity = (EntityData)Session["entityOperation"];

				DataTable tb = entity.CurrentTable.Copy();
				tb.Columns.Add("Sno");
				tb.Columns.Add("SystemID");
				tb.Columns.Add("AccessRangeType");
				tb.Columns.Add("RelationCode");
				tb.Columns.Add("Checked", typeof(bool));
				tb.Columns.Add("RoleLevel", typeof(int));
				tb.Columns.Add("RoleLevelName");

				foreach(DataRow dr in tb.Rows) 
				{
					string OperationCode = dr["OperationCode"].ToString();

					dr["Sno"] = BLL.ConvertRule.GetNextSno(this.txtOperationSno);
					dr["SystemID"] = SystemID;
					dr["AccessRangeType"] = AccessRangeType;
					dr["RelationCode"] = RelationCode;

					DataRow drOp = GetOperationRow(AccessRangeType, RelationCode, OperationCode);
					if (drOp != null) 
					{
						dr["Checked"] = true;
						dr["RoleLevel"] = drOp["RoleLevel"];
						dr["RoleLevelName"] = drOp["RoleLevelName"];
					}
					else 
					{
						dr["Checked"] = false;
					}
				}

				return tb;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		public DataRow GetOperationRow(string AccessRangeType, string RelationCode, string OperationCode) 
		{
			try 
			{
				DataRow dr = null;

				if ((AccessRangeType == "") || (RelationCode == "")) return dr;

				EntityData entity = (EntityData)Session["entity"];
				DataTable tb = entity.CurrentTable;

				string filter = string.Format("AccessRangeType='{0}' and RelationCode='{1}' and OperationCode='{2}'", AccessRangeType, RelationCode, OperationCode);
				DataRow[] drs = tb.Select(filter, "", DataViewRowState.CurrentRows);
				if (drs.Length > 0) 
				{
					dr = drs[0];
				}

				return dr;
			}
			catch 
			{
				return null;
			}
		}

		private DataRow AccessRangeNewRow(DataTable tb, string AccessRangeType, string RelationCode, string OperationCode, string GroupCode, string RoleLevel, bool isTemp) 
		{
			try 
			{
				string ClassCode = this.txtClassCode.Value;
				DataRow drNew = tb.NewRow();

				if (isTemp) 
				{
					drNew["AccessRangeCode"] = "-" + BLL.ConvertRule.GetNextSno(this.txtDetailSno).ToString();
				}
				else 
				{
					drNew["AccessRangeCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("AccessRangeCode");
				}

				drNew["AccessRangeType"] = AccessRangeType;
				drNew["RelationCode"] = RelationCode;

				if (this.txtIsResource.Value == "1") 
				{
					drNew["ResourceCode"] = GroupCode;
				}
				else 
				{
					drNew["GroupCode"] = GroupCode;
					drNew["ResourceCode"] = "";
				}

				drNew["UnitCode"] = "";
				drNew["OperationCode"] = OperationCode;

				drNew["RoleLevel"] = BLL.ConvertRule.ToInt(RoleLevel);

				if (isTemp) 
				{
//					BLL.SystemGroupRule.SetSystemGroupAccessRelationName(drNew);
//					BLL.SystemGroupRule.SetSystemGroupAccessRelationImage(drNew);
				}

				tb.Rows.Add(drNew);

				return drNew;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private DataRow RelationNewRow(DataTable tb, string AccessRangeType, string RelationCode) 
		{
			try 
			{
				DataRow drNew = tb.NewRow();

				drNew["SystemID"] = "-" + BLL.ConvertRule.GetNextSno(this.txtRelationSno).ToString();

				drNew["AccessRangeType"] = AccessRangeType;
				drNew["RelationCode"] = RelationCode;

				BLL.SystemGroupRule.SetSystemGroupAccessRelationName(drNew);
				BLL.SystemGroupRule.SetSystemGroupAccessRelationImage(drNew);

				tb.Rows.Add(drNew);

				return drNew;
			}
			catch(Exception ex)
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

			return true;
		}

		/// <summary>
		/// 保存
		/// </summary>
		private void SaveData()
		{
			try
			{
				string GroupCode = this.txtGroupCode.Value;

				//老的权限
				//				EntityData entity = DAL.EntityDAO.ResourceDAO.GetAccessRangeByGroupCode(GroupCode);
				//				DataTable tb = entity.CurrentTable;
				//
				//				string UserCodes = this.ucRelation.UserCodes;
				//				string StationCodes = this.ucRelation.StationCodes;
				//
				//				string[] arrUserCode = UserCodes.Split(",".ToCharArray());
				//				string[] arrStationCode = StationCodes.Split(",".ToCharArray());
				//
				//				//删除现已没有的权限
				//				foreach(DataRow dr in tb.Rows) 
				//				{
				//					string RelationCode = BLL.ConvertRule.ToString(dr["RelationCode"]);
				//					int AccessRangeType = BLL.ConvertRule.ToInt(dr["AccessRangeType"]);
				//					string[] arr = null;
				//
				//					switch(AccessRangeType) 
				//					{
				//						case 0:
				//							//用户
				//							arr = arrUserCode;
				//							break;
				//
				//						case 1:
				//							//岗位
				//							arr = arrStationCode;
				//							break;
				//					}
				//
				//					if (BLL.ConvertRule.FindArray(arr, RelationCode) < 0) 
				//					{
				//						dr.Delete();
				//					}
				//				}
				//
				//				string OperationCode = "";
				//
				//				//添加新的权限
				//				foreach(string UserCode in arrUserCode) 
				//				{
				//					string AccessRangeType = "0";
				//					if (UserCode != "") 
				//					{
				//						string filter = string.Format("AccessRangeType='{0}' and RelationCode='{1}' and OperationCode='{2}'", AccessRangeType, UserCode, OperationCode);
				//						if (tb.Select(filter, "", DataViewRowState.CurrentRows).Length == 0)
				//						{
				//							AccessRangeNewRow(tb, AccessRangeType, UserCode, OperationCode, GroupCode, false);
				//						}
				//					}
				//				}
				//
				//				foreach(string StationCode in arrStationCode) 
				//				{
				//					string AccessRangeType = "1";
				//					if (StationCode != "") 
				//					{
				//						string filter = string.Format("AccessRangeType='{0}' and RelationCode='{1}' and OperationCode='{2}'", AccessRangeType, StationCode, OperationCode);
				//						if (tb.Select(filter, "", DataViewRowState.CurrentRows).Length == 0)
				//						{
				//							AccessRangeNewRow(tb, AccessRangeType, StationCode, OperationCode, GroupCode, false);
				//						}
				//					}
				//				}

				EntityData entity = (EntityData)Session["entity"];
				DataTable tb = entity.CurrentTable;

				DataView dv = new DataView(tb, "", "", DataViewRowState.CurrentRows);

				//删除未定义操作的记录
                for(int i=dv.Count-1;i>=0;i--)
				{
					DataRow dr = dv[i].Row;

					string OperationCode = BLL.ConvertRule.ToString(dr["OperationCode"]);

					if (OperationCode == "") 
					{
						dr.Delete();
					}
				}
				
				//填关键字
				foreach(DataRowView drv in dv) 
				{
					DataRow dr = drv.Row;

					string AccessRangeCode = dr["AccessRangeCode"].ToString();

					if (AccessRangeCode.Substring(0, 1) == "-") 
					{
						dr["AccessRangeCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("AccessRangeCode");
					}
				}

				DAL.EntityDAO.ResourceDAO.SubmitAllAccessRange(entity);
				entity.Dispose();
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

				SaveData();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
//			Response.Write(string.Format("window.opener.Refresh('{0}');", this.txtAct.Value));
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));

//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 添加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSelectReturn_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string GroupCode = this.txtGroupCode.Value;

				string users = this.txtUsers.Value;
				string stations = this.txtStations.Value;

				string[] arrUser = users.Split(",".ToCharArray());
				string[] arrStation = stations.Split(",".ToCharArray());

				EntityData entity = (EntityData)Session["entity"];
				DataTable tb = entity.CurrentTable;
				DataTable tbRela = (DataTable)Session["tbRela"];

				EntityData entityOp = (EntityData)Session["entityOperation"];
				DataTable tbOp = entityOp.CurrentTable;

				foreach(string code in arrUser) 
				{
					string AccessRangeType = "0";
					if (code != "") 
					{
						string filter = string.Format("AccessRangeType='{0}' and RelationCode='{1}'", AccessRangeType, code);
						if (tbRela.Select(filter, "", DataViewRowState.CurrentRows).Length == 0)
						{
							//增加用户
							RelationNewRow(tbRela, AccessRangeType, code);

                            //增加缺省操作 添加权限 2006-12-22日修改 by iCaca
                            //foreach(DataRow drOp in tbOp.Rows) 
                            //{
                            //    string OperationCode = drOp["OperationCode"].ToString();
                            //    AccessRangeNewRow(tb, AccessRangeType, code, OperationCode, GroupCode, "", false );
                            //}
						}
					}
				}

				foreach(string code in arrStation) 
				{
					string AccessRangeType = "1";
					if (code != "") 
					{
						string filter = string.Format("AccessRangeType='{0}' and RelationCode='{1}'", AccessRangeType, code);
						if (tbRela.Select(filter, "", DataViewRowState.CurrentRows).Length == 0)
						{
							//增加岗位
							RelationNewRow(tbRela, AccessRangeType, code);

							//增加缺省操作 添加权限 2006-12-22日修改 by iCaca
                            //foreach(DataRow drOp in tbOp.Rows) 
                            //{
                            //    string OperationCode = drOp["OperationCode"].ToString();
                            //    AccessRangeNewRow(tb, AccessRangeType, code, OperationCode, GroupCode, "", false );
                            //}
						}
					}
				}

				BindDataGrid();

			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "添加失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 批量删除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnBatchDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string codes = this.txtSelectCode.Value.Trim();
				string[] arrcode = codes.Split(",".ToCharArray());

				EntityData entity = (EntityData)Session["entity"];
				DataTable tb = entity.CurrentTable;
				DataTable tbRela = (DataTable)Session["tbRela"];

				//删除岗位、用户
				foreach(string code in arrcode) 
				{
					DataRow[] drsRela = tbRela.Select("SystemID='" + code + "'", "", DataViewRowState.CurrentRows);
					if (drsRela.Length > 0) 
					{
						DataRow drRela = drsRela[0];
						string AccessRangeType = BLL.ConvertRule.ToString(drRela["AccessRangeType"]);
						string RelationCode = BLL.ConvertRule.ToString(drRela["RelationCode"]);

						//删除该岗位用户的所有操作权限
						string filter = string.Format("AccessRangeType='{0}' and RelationCode='{1}'", AccessRangeType, RelationCode);
						DataRow[] drs = tb.Select(filter, "", DataViewRowState.CurrentRows);
						foreach(DataRow dr in drs) 
						{
							dr.Delete();
						}

						//删除岗位、用户
						drRela.Delete();
					}
				}

				BindDataGrid();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try 
			{
				if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) 
				{
					string SystemID = this.dgList.DataKeys[e.Item.ItemIndex].ToString();
					string AccessRangeType = ((HtmlInputHidden)e.Item.FindControl("txtAccessRangeType")).Value;
					string RelationCode = ((HtmlInputHidden)e.Item.FindControl("txtRelationCode")).Value;

					Repeater dgOperation = (Repeater)e.Item.FindControl("dgOperation");
					dgOperation.DataSource = GetOperationDataSource(SystemID, AccessRangeType, RelationCode);
					dgOperation.DataBind();
				}
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "显示列表失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 屏幕数据保存到临时表
		/// </summary>
		private void SaveScreenToSession(bool isBind) 
		{
			try 
			{
				string GroupCode = this.txtGroupCode.Value;

				EntityData entity = (EntityData)Session["entity"];
				DataTable tb = entity.CurrentTable;

				string CheckedCode = this.txtCheckedCode.Value;

				string[] arrCode = CheckedCode.Split(",".ToCharArray());

				//删除不选的操作
				DataView dv = new DataView(tb, "", "", DataViewRowState.CurrentRows);
                for(int i=dv.Count-1;i>=0;i--)
				{
					DataRow dr = dv[i].Row;

					string AccessRangeType = BLL.ConvertRule.ToString(dr["AccessRangeType"]);
					string RelationCode = BLL.ConvertRule.ToString(dr["RelationCode"]);
					string OperationCode = BLL.ConvertRule.ToString(dr["OperationCode"]);

					string val = AccessRangeType + "|" + RelationCode + "|" + OperationCode + "|";

					int pos = BLL.ConvertRule.FindArrayLike(arrCode, val);

					if (pos < 0) 
					{
						dr.Delete();
					}
					else 
					{
						//修改
						string[] arr = arrCode[pos].Split("|".ToCharArray());
						string RoleLevel = arr[3];
						dr["RoleLevel"] = BLL.ConvertRule.ToInt(arr[3]);
						dr["RoleLevelName"] = BLL.ResourceRule.GetAccessRangeRoleLevelName(dr["RoleLevel"]);
					}
				}

				//添加选中的操作
				foreach(string code in arrCode) 
				{
					if (code.Length > 0) 
					{
						string[] arr = code.Split("|".ToCharArray());
						string AccessRangeType = arr[0];
						string RelationCode = arr[1];
						string OperationCode = arr[2];
						string RoleLevel = arr[3];

						string filter = string.Format("AccessRangeType='{0}' and RelationCode='{1}' and OperationCode='{2}'", AccessRangeType, RelationCode, OperationCode);
						if (tb.Select(filter, "", DataViewRowState.CurrentRows).Length == 0)
						{
							AccessRangeNewRow(tb, AccessRangeType, RelationCode, OperationCode, GroupCode, RoleLevel, true);
						}
					}
				}

				if (isBind)
				{
					BindDataGrid();
				}

//				foreach(DataGridItem item in this.dgList.Items) 
//				{
//					string AccessRangeType = ((HtmlInputHidden)item.FindControl("txtAccessRangeType")).Value;
//					string RelationCode = ((HtmlInputHidden)item.FindControl("txtRelationCode")).Value;
//				}
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存页面列表失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
	}
}
