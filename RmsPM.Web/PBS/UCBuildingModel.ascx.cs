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
	///		UCBuildingModel ��ժҪ˵����
	/// </summary>
	public partial class UCBuildingModel : System.Web.UI.UserControl
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion


		#region --- ˽�г�Ա -------------------------------------------------------------------------

		/// <summary>
		/// ��Ŀ���
		/// </summary>
		private string _ProjectCode ="";

		/// <summary>
		/// ¥�����
		/// </summary>
		private string _BuildingCode = "";

		/// <summary>
		/// ¥�����ͱ��
		/// </summary>
		private string _BuildingModelCode = "";

		/// <summary>
		/// ʹ������
		/// </summary>
		private string _DoType = "";

		#endregion -------------------------------------------------------------------------

		#region --- �������� -------------------------------------------------------------------------

		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public string ProjectCode
		{
			get{return this._ProjectCode;}
			set{this._ProjectCode=value;}
		}

		/// <summary>
		/// ¥�����
		/// </summary>
		public string BuildingCode
		{
			get{return this._BuildingCode;}
			set{this._BuildingCode=value;}
		}

		/// <summary>
		/// ¥�����ͱ��
		/// </summary>
		public string BuildingModelCode
		{
			get{return this._BuildingModelCode;}
			set{this._BuildingModelCode=value;}
		}

		/// <summary>
		/// ʹ������
		/// </summary>
		public string DoType
		{
			get{return this._DoType;}
			set{this._DoType=value;}
		}

		#endregion -------------------------------------------------------------------------

		#region --- ���÷��� -------------------------------------------------------------------------

		/// <summary>
		/// �ؼ���ʼ��
		/// </summary>
		public void IniControl()
		{
			try
			{
				this.ViewSingleTable.Visible = false;
				this.ModifySingleTableTable.Visible = false;
				this.ViewImgTable.Visible = false;
				this.imgMain.Visible = false;

				this.HideProjectCode.Value = this._ProjectCode;
				this.HideBuildingCode.Value = this._BuildingCode;
				this.HideBuildingModelCode.Value = this._BuildingModelCode;
				this.HideDoType.Value = this._DoType;

				if ( "SingleView"==this._DoType )
				{
					//��ʾ
					this.ViewSingleTable.Visible = true;
					this.ViewImgTable.Visible = true;
				}
				else if ( "SingleModify"==this._DoType )
				{
					//�༭
					this.ModifySingleTableTable.Visible = true;
				}

				BLL.PageFacade.LoadNoUseModelSelect(this.SelectModelCode,"",this._ProjectCode,this._BuildingCode,this._BuildingModelCode);
				BLL.PageFacade.LoadBuildingStationSelect(this.SelectBuildingStationCode,"",this._BuildingCode);
				BLL.PageFacade.LoadBuildingFunctionSelect(this.SelectBuildingFunctionCode,"",this._BuildingCode);

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// װ�ؿؼ�����
		/// </summary>
		public void LoadData()
		{
			try
			{
				if ( ""==this._BuildingModelCode )
				{
					return;
				}

				EntityData entity = DAL.EntityDAO.ProductDAO.GetV_BuildingModelByBuildingModelCode(this._BuildingModelCode);

				string ModelCode = entity.GetString("ModelCode");
				string ProjectCode = entity.GetString("ProjectCode");
				string ModelName = entity.GetString("ModelName");
				string Structure = entity.GetString("Structure");
				string ImageCode = entity.GetString("ImageCode");
				string BuildArea = entity.GetDecimalString("BuildArea");
				string RoomArea = entity.GetDecimalString("RoomArea");
				string Remark = entity.GetString("Remark");
				string HouseType = entity.GetString("HouseType");
				string BuildingModelCode = entity.GetString("BuildingModelCode");
				string BuildingCode = entity.GetString("BuildingCode");
				string BuildingStationCode = entity.GetString("BuildingStationCode");
				string BuildingFunctionCode = entity.GetString("BuildingFunctionCode");
				string BModelNum = entity.GetIntString("BModelNum");
				string BModelArea = entity.GetDecimalString("BModelArea");
				string BModelRemark = entity.GetString("BModelRemark");

				entity.Dispose();

				if ( ""!=ImageCode )
				{
					this.imgMain.Visible = true;
					this.imgMain.Src = "ShowPicture.aspx?FileID=" + ImageCode;
				}

				this.LabelModelName.Text = ModelName;
				this.LabelStationName.Text = BuildingStationCode;
				this.LabelFunctionName.Text = BuildingFunctionCode;
				this.LabelBModelNum.Text = BModelNum;
				this.LabelBModelArea.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(BModelArea), "ƽ��");
				this.LabelStructure.Text = Structure;
				this.LabelHouseTypeName.Text = BLL.PBSRule.GetPBSTypeFullName(HouseType);
				this.LabelBuildArea.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(BuildArea), "ƽ��");
				this.LabelRoomArea.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(RoomArea), "ƽ��");
				this.LabelBModelRemark.Text = BModelRemark.Replace("\n","<br>");

				this.HideBuildingCode.Value = BuildingCode;
				this.HideBuildingModelCode.Value = BuildingModelCode;
				this.HideProjectCode.Value = ProjectCode;

				this.SelectModelCode.Value = ModelCode;
				this.SelectBuildingStationCode.Value = BuildingStationCode;
				this.SelectBuildingFunctionCode.Value = BuildingFunctionCode;
				this.TextBModelNum.Value = BModelNum;
				this.TextBModelArea.Value = BModelArea;
				this.TextAreaBModelRemark.Value = BModelRemark;

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		public void SaveData()
		{
			try
			{
				if ( ""==this.SelectModelCode.Value.Trim() )
				{
					Response.Write( JavaScript.Alert(true,"��ѡ���ͣ�") );
					return;
				}

				if ( ""==this.HideBuildingModelCode.Value.Trim() )
				{
					#region --- ���� -------------------------------------------------------------------------

					EntityData entity = new EntityData("BuildingModel");
					DataRow dr = entity.GetNewRecord();

					dr["BuildingModelCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode(BuildingModelCode);
					dr["BuildingCode"] = this.HideBuildingCode.Value;

					dr["ModelCode"] = this.SelectModelCode.Value;

					if ( ""!=this.SelectBuildingStationCode.Value.Trim() )
					{
						dr["BuildingStationCode"] = this.SelectBuildingStationCode.Value;
					}
					else
					{
						dr["BuildingStationCode"] = DBNull.Value;
					}

					if ( ""!=this.SelectBuildingFunctionCode.Value.Trim() )
					{
						dr["BuildingFunctionCode"] = this.SelectBuildingFunctionCode.Value;
					}
					else
					{
						dr["BuildingFunctionCode"] = DBNull.Value;
					}

					if ( StringCheck.IsInt(this.TextBModelNum.Value) )
					{
						dr["BModelNum"] = this.TextBModelNum.Value;
					}
					else
					{
						dr["BModelNum"] = DBNull.Value;
					}

					if ( StringCheck.IsNumber(this.TextBModelArea.Value) )
					{
						dr["BModelArea"] = this.TextBModelArea.Value;
					}
					else
					{
						dr["BModelArea"] = DBNull.Value;
					}

					dr["BModelRemark"] = this.TextAreaBModelRemark.Value;

					entity.AddNewRecord(dr);
					DAL.EntityDAO.ProductDAO.SubmitAllBuildingModel(entity);
					entity.Dispose();

					#endregion -------------------------------------------------------------------------
				}
				else
				{
					#region --- �޸� -------------------------------------------------------------------------

					EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingModelByCode(this.HideBuildingModelCode.Value.Trim());
					DataRow dr = entity.CurrentRow;

					dr["ModelCode"] = this.SelectModelCode.Value;

					if ( ""!=this.SelectBuildingStationCode.Value.Trim() )
					{
						dr["BuildingStationCode"] = this.SelectBuildingStationCode.Value;
					}
					else
					{
						dr["BuildingStationCode"] = DBNull.Value;
					}

					if ( ""!=this.SelectBuildingFunctionCode.Value.Trim() )
					{
						dr["BuildingFunctionCode"] = this.SelectBuildingFunctionCode.Value;
					}
					else
					{
						dr["BuildingFunctionCode"] = DBNull.Value;
					}

					if ( StringCheck.IsInt(this.TextBModelNum.Value) )
					{
						dr["BModelNum"] = this.TextBModelNum.Value;
					}
					else
					{
						dr["BModelNum"] = DBNull.Value;
					}

					if ( StringCheck.IsNumber(this.TextBModelArea.Value) )
					{
						dr["BModelArea"] = this.TextBModelArea.Value;
					}
					else
					{
						dr["BModelArea"] = DBNull.Value;
					}

					dr["BModelRemark"] = this.TextAreaBModelRemark.Value;

					DAL.EntityDAO.ProductDAO.SubmitAllBuildingModel(entity);
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
		/// ɾ������
		/// </summary>
		public void DeleteData()
		{
			try
			{
				DAL.EntityDAO.ProductDAO.DeleteBuildingModel( DAL.EntityDAO.ProductDAO.GetBuildingModelByCode(this.HideBuildingModelCode.Value.Trim()) );
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#endregion -------------------------------------------------------------------------
	}
}
