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
	///		UCBuildingStation ��ժҪ˵����
	/// </summary>
	public partial class UCBuildingStation : System.Web.UI.UserControl
	{
		/// <summary>
		/// ��ʾ��Ԫ�ñ��
		/// </summary>

		/// <summary>
		/// �༭��Ԫ�ñ��
		/// </summary>

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
		/// ¥�����
		/// </summary>
		private string _BuildingCode = "";

		/// <summary>
		/// ¥��λ�ñ��
		/// </summary>
		private string _BuildingStationCode = "";

		/// <summary>
		/// ʹ������
		/// </summary>
		private string _DoType = "";

		#endregion -------------------------------------------------------------------------

		#region --- �������� -------------------------------------------------------------------------

		/// <summary>
		/// ¥�����
		/// </summary>
		public string BuildingCode
		{
			get{return this._BuildingCode;}
			set{this._BuildingCode=value;}
		}

		/// <summary>
		/// ¥��λ�ñ��
		/// </summary>
		public string BuildingStationCode
		{
			get{return this._BuildingStationCode;}
			set{this._BuildingStationCode=value;}
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

				this.HideBuildingCode.Value = this._BuildingCode;
				this.HideBuildingStationCode.Value = this._BuildingStationCode;
				this.HideDoType.Value = this._DoType;

				if ( "SingleView"==this._DoType )
				{
					//��ʾ
					this.ViewSingleTable.Visible = true;
				}
				else if ( "SingleModify"==this._DoType )
				{
					//�༭
					this.ModifySingleTableTable.Visible = true;
				}
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
		/// ��������
		/// </summary>
		public void SaveData()
		{
			try
			{
				if ( ""==this.TextStationName.Value.Trim() )
				{
					Response.Write( JavaScript.Alert(true,"���Ʊ�����д��") );
					return;
				}

				if ( ""==this.HideBuildingStationCode.Value.Trim() )
				{
					#region --- ���� -------------------------------------------------------------------------

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
					#region --- �޸� -------------------------------------------------------------------------

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
		/// ɾ������
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
					Response.Write( JavaScript.Alert(true,"��λ����¥����������ʹ�ã�����ɾ����") );
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
