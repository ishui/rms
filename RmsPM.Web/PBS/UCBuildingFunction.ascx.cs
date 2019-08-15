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
	///		UCBuildingFunction ��ժҪ˵����
	/// </summary>
	public partial class UCBuildingFunction : System.Web.UI.UserControl
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
		/// ¥�����
		/// </summary>
		private string _BuildingCode = "";

		/// <summary>
		/// ¥�����ܱ��
		/// </summary>
		private string _BuildingFunctionCode = "";

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
		/// ¥�����ܱ��
		/// </summary>
		public string BuildingFunctionCode
		{
			get{return this._BuildingFunctionCode;}
			set{this._BuildingFunctionCode=value;}
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
				this.HideBuildingFunctionCode.Value = this._BuildingFunctionCode;
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
		/// ��������
		/// </summary>
		public void SaveData()
		{
			try
			{
				if ( ""==this.TextFunctionName.Value.Trim() )
				{
					Response.Write( JavaScript.Alert(true,"���Ʊ�����д��") );
					return;
				}

				if ( ""==this.HideBuildingFunctionCode.Value.Trim() )
				{
					#region --- ���� -------------------------------------------------------------------------

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
					#region --- �޸� -------------------------------------------------------------------------

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
		/// ɾ������
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
					Response.Write( JavaScript.Alert(true,"�˹�����¥����������ʹ�ã�����ɾ����") );
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
