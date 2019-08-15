namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using Rms.Web;
	using Rms.Check;

	/// <summary>
	///		UCBuildingFunction 的摘要说明。
	/// </summary>
	public partial class UCBuildingFunction : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion


		#region --- 私有成员 -------------------------------------------------------------------------

		/// <summary>
		/// 楼栋编号
		/// </summary>
		private string _BuildingCode = "";

		/// <summary>
		/// 楼栋功能编号
		/// </summary>
		private string _BuildingFunctionCode = "";

		/// <summary>
		/// 使用类型
		/// </summary>
		private string _DoType = "";

		#endregion -------------------------------------------------------------------------

		#region --- 公用属性 -------------------------------------------------------------------------

		/// <summary>
		/// 楼栋编号
		/// </summary>
		public string BuildingCode
		{
			get{return this._BuildingCode;}
			set{this._BuildingCode=value;}
		}

		/// <summary>
		/// 楼栋功能编号
		/// </summary>
		public string BuildingFunctionCode
		{
			get{return this._BuildingFunctionCode;}
			set{this._BuildingFunctionCode=value;}
		}

		/// <summary>
		/// 使用类型
		/// </summary>
		public string DoType
		{
			get{return this._DoType;}
			set{this._DoType=value;}
		}

		#endregion -------------------------------------------------------------------------

		#region --- 公用方法 -------------------------------------------------------------------------

		/// <summary>
		/// 控件初始化
		/// </summary>
		public void IniControl()
		{
			try
			{
				this.ViewSingleTable.Visible = false;
				this.ModifySingleTableTable.Visible = false;

				this.HideBuildingCode.Value = this._BuildingCode;
				this.HideBuildingFunctionCode.Value = this._BuildingFunctionCode;
				this.HideDoType.Value = this._DoType;

				if ( "SingleView"==this._DoType )
				{
					//显示
					this.ViewSingleTable.Visible = true;
				}
				else if ( "SingleModify"==this._DoType )
				{
					//编辑
					this.ModifySingleTableTable.Visible = true;
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 装载控件数据
		/// </summary>
		public void LoadData()
		{
			try
			{
				if ( ""==this._BuildingFunctionCode )
				{
					return;
				}

				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFunctionByCode( this._BuildingFunctionCode );

				string FunctionName = entity.GetString("FunctionName");
				string FunctionNum = entity.GetIntString("FunctionNum");
				string FunctionArea = entity.GetDecimalString("FunctionArea");
				string FunctionRemark = entity.GetString("FunctionRemark");

				this.LabelFunctionName.Text = FunctionName;
				this.LabelFunctionNum.Text = FunctionNum;
				this.LabelFunctionArea.Text = FunctionArea;
				this.LabelFunctionRemark.Text = FunctionRemark.Replace("\n","<br>");

				this.TextFunctionName.Value = FunctionName;
				this.TextFunctionNum.Value = FunctionNum;
				this.TextFunctionArea.Value = FunctionArea;
				this.TextAreaFunctionRemark.Value = FunctionRemark;

				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 保存数据
		/// </summary>
		public void SaveData()
		{
			try
			{
				if ( ""==this.TextFunctionName.Value.Trim() )
				{
					Response.Write( JavaScript.Alert(true,"名称必须填写！") );
					return;
				}

				if ( ""==this.HideBuildingFunctionCode.Value.Trim() )
				{
					#region --- 新增 -------------------------------------------------------------------------

					EntityData entity = new EntityData("BuildingFunction");
					DataRow dr = entity.GetNewRecord();

					dr["BuildingFunctionCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BuildingFunctionCode");
					dr["BuildingCode"] = this.HideBuildingCode.Value;
					dr["FunctionName"] = this.TextFunctionName.Value;

					if ( StringCheck.IsInt(this.TextFunctionNum.Value) )
					{
						dr["FunctionNum"] = this.TextFunctionNum.Value;
					}
					else
					{
						dr["FunctionNum"] = DBNull.Value;
					}

					if ( StringCheck.IsNumber(this.TextFunctionArea.Value) )
					{
						dr["FunctionArea"] = this.TextFunctionArea.Value;
					}
					else
					{
						dr["FunctionArea"] = DBNull.Value;
					}

					dr["FunctionRemark"] = this.TextAreaFunctionRemark.Value;

					entity.AddNewRecord(dr);
					DAL.EntityDAO.ProductDAO.SubmitAllBuildingFunction(entity);
					entity.Dispose();

					#endregion -------------------------------------------------------------------------
				}
				else
				{
					#region --- 修改 -------------------------------------------------------------------------

					EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFunctionByCode(this.HideBuildingFunctionCode.Value.Trim());
					DataRow dr = entity.CurrentRow;

					dr["FunctionName"] = this.TextFunctionName.Value;

					if ( StringCheck.IsInt(this.TextFunctionNum.Value) )
					{
						dr["FunctionNum"] = this.TextFunctionNum.Value;
					}
					else
					{
						dr["FunctionNum"] = DBNull.Value;
					}

					if ( StringCheck.IsNumber(this.TextFunctionArea.Value) )
					{
						dr["FunctionArea"] = this.TextFunctionArea.Value;
					}
					else
					{
						dr["FunctionArea"] = DBNull.Value;
					}

					dr["FunctionRemark"] = this.TextAreaFunctionRemark.Value;

					DAL.EntityDAO.ProductDAO.SubmitAllBuildingFunction(entity);
					entity.Dispose();

					#endregion -------------------------------------------------------------------------
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 删除数据
		/// </summary>
		public void DeleteData()
		{
			try
			{
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingModelByBuildingFunctionCode(this.HideBuildingFunctionCode.Value);
				bool canPass = !entity.HasRecord();
				entity.Dispose();

				if ( canPass )
				{
					DAL.EntityDAO.ProductDAO.DeleteBuildingFunction( DAL.EntityDAO.ProductDAO.GetBuildingFunctionByCode(this.HideBuildingFunctionCode.Value.Trim()) );
				}
				else
				{
					Response.Write( JavaScript.Alert(true,"此功能有楼栋户型正在使用，不能删除！") );
					return;
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#endregion -------------------------------------------------------------------------
	}
}
