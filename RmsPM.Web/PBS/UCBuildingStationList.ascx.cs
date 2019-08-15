namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;

	/// <summary>
	///		UCBuildingStationList ��ժҪ˵����
	/// </summary>
	public partial class UCBuildingStationList : System.Web.UI.UserControl
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion


		#region --- ˽������ -------------------------------------------------------------

		/// <summary>
		/// ¥�����
		/// </summary>
		private string _BuildingCode = "";

		#endregion -------------------------------------------------------------

		#region --- ˽�з��� -------------------------------------------------------------

		/// <summary>
		/// ¥��λ���б�ϼ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				if ( ListItemType.Footer==e.Item.ItemType )
				{
					((Label)e.Item.FindControl("ftTotalStationNum")).Text = BLL.ConvertRule.ToString(ViewState["ftTotalStationNum"]);
					((Label)e.Item.FindControl("ftTotalStationArea")).Text = BLL.ConvertRule.ToString(ViewState["ftTotalStationArea"]);
					((Label)e.Item.FindControl("ftTotalAreaForVolumeRate")).Text = BLL.ConvertRule.ToString(ViewState["ftTotalAreaForVolumeRate"]);
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#endregion -------------------------------------------------------------

		#region --- �������� -------------------------------------------------------------

		/// <summary>
		/// ¥�����
		/// </summary>
		public string BuildingCode
		{
			get{return this._BuildingCode;}
			set{this._BuildingCode=value;}
		}

		#endregion -------------------------------------------------------------

		#region --- �������� -------------------------------------------------------------

		/// <summary>
		/// ��ʼ���ؼ�
		/// </summary>
		public void IniControl()
		{
			try
			{
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// װ�ؿؼ�����
		/// </summary>
		public void LoadDataList()
		{
			try
			{
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingStationByBuildingCode(this._BuildingCode);

				#region --- �ϼ� --------------------------------------------------------------

				string[] arrField = {"StationArea","AreaForVolumeRate"};
				int[] arrSumInt = BLL.MathRule.SumIntColumn( entity.CurrentTable, new string[] {"StationNum"});
				decimal[] arrSumDec = BLL.MathRule.SumColumn( entity.CurrentTable, arrField);

				ViewState["ftTotalStationNum"] = arrSumInt[0];
				ViewState["ftTotalStationArea"] = arrSumDec[0];
				ViewState["ftTotalAreaForVolumeRate"] = arrSumDec[1];

				#endregion --------------------------------------------------------------

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#endregion -------------------------------------------------------------


	}
}
