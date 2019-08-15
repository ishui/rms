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
	///		UCBuildingStation 的摘要说明。
	/// </summary>
	public partial class UCBuildingStation : System.Web.UI.UserControl
	{
		/// <summary>
		/// 显示单元用表格
		/// </summary>

		/// <summary>
		/// 编辑单元用表格
		/// </summary>

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
		/// 楼栋位置编号
		/// </summary>
		private string _BuildingStationCode = "";

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
		/// 楼栋位置编号
		/// </summary>
		public string BuildingStationCode
		{
			get{return this._BuildingStationCode;}
			set{this._BuildingStationCode=value;}
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
				this.HideBuildingStationCode.Value = this._BuildingStationCode;
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
				if ( ""==this._BuildingStationCode )
				{
					return;
				}

				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingStationByCode( this._BuildingStationCode );
				if ( entity.HasRecord() )
				{
					string StationName = entity.GetString("StationName");
					string StationNum = entity.GetIntString("StationNum");
					string StationArea = entity.GetDecimalString("StationArea");
					string AreaForVolumeRate = entity.GetDecimalString("AreaForVolumeRate");
					string StationRemark = entity.GetString("StationRemark");

					this.LabelStationName.Text = StationName;
					this.LabelStationNum.Text = StationNum;
					this.LabelStationArea.Text = StationArea;
					this.LabelAreaForVolumeRate.Text = AreaForVolumeRate;
					this.LabelStationRemark.Text = StationRemark.Replace("\n","<br>");

					this.TextStationName.Value = StationName;
					this.TextStationNum.Value = StationNum;
					this.TextStationArea.Value = StationArea;
					this.TextAreaForVolumeRate.Value = AreaForVolumeRate;
					this.TextAreaStationRemark.Value = StationRemark;
				}
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
				if ( ""==this.TextStationName.Value.Trim() )
				{
					Response.Write( JavaScript.Alert(true,"名称必须填写！") );
					return;
				}

				if ( ""==this.HideBuildingStationCode.Value.Trim() )
				{
					#region --- 新增 -------------------------------------------------------------------------

					EntityData entity = new EntityData("BuildingStation");

					DataRow dr = entity.GetNewRecord();

					dr["BuildingStationCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BuildingStationCode");
					dr["BuildingCode"] = this.HideBuildingCode.Value;

					dr["StationName"] = this.TextStationName.Value;

					if ( StringCheck.IsInt(this.TextStationNum.Value) )
					{
						dr["StationNum"] = this.TextStationNum.Value;
					}
					else
					{
						dr["StationNum"] = DBNull.Value;
					}

					if ( StringCheck.IsNumber(this.TextStationArea.Value) )
					{
						dr["StationArea"] = this.TextStationArea.Value;
					}
					else
					{
						dr["StationArea"] = DBNull.Value;
					}

					if ( StringCheck.IsNumber(this.TextAreaForVolumeRate.Value) )
					{
						dr["AreaForVolumeRate"] = this.TextAreaForVolumeRate.Value;
					}
					else
					{
						dr["AreaForVolumeRate"] = DBNull.Value;
					}

					dr["StationRemark"] = this.TextAreaStationRemark.Value;

					entity.AddNewRecord( dr );
					DAL.EntityDAO.ProductDAO.SubmitAllBuildingStation( entity );
					entity.Dispose();

					#endregion -------------------------------------------------------------------------
				}
				else
				{
					#region --- 修改 -------------------------------------------------------------------------

					EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingStationByCode( this.HideBuildingStationCode.Value.Trim() );

					DataRow dr = entity.CurrentRow;

					dr["StationName"] = this.TextStationName.Value;

					if ( StringCheck.IsInt(this.TextStationNum.Value) )
					{
						dr["StationNum"] = this.TextStationNum.Value;
					}
					else
					{
						dr["StationNum"] = DBNull.Value;
					}

					if ( StringCheck.IsNumber(this.TextStationArea.Value) )
					{
						dr["StationArea"] = this.TextStationArea.Value;
					}
					else
					{
						dr["StationArea"] = DBNull.Value;
					}

					if ( StringCheck.IsNumber(this.TextAreaForVolumeRate.Value) )
					{
						dr["AreaForVolumeRate"] = this.TextAreaForVolumeRate.Value;
					}
					else
					{
						dr["AreaForVolumeRate"] = DBNull.Value;
					}

					dr["StationRemark"] = this.TextAreaStationRemark.Value;

					DAL.EntityDAO.ProductDAO.SubmitAllBuildingStation( entity );
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
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingModelByBuildingStationCode(this.HideBuildingStationCode.Value);
				bool canPass = !entity.HasRecord();
				entity.Dispose();

				if ( canPass )
				{
					DAL.EntityDAO.ProductDAO.DeleteBuildingStation( DAL.EntityDAO.ProductDAO.GetBuildingStationByCode(this.HideBuildingStationCode.Value) );
				}
				else
				{
					Response.Write( JavaScript.Alert(true,"此位置有楼栋户型正在使用，不能删除！") );
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
