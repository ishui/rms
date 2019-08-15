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
	///		UCBuildingFunctionList ��ժҪ˵����
	/// </summary>
	public partial class UCBuildingFunctionList : System.Web.UI.UserControl
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
		/// ¥�������б�ϼ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				if ( ListItemType.Footer==e.Item.ItemType )
				{
					((Label)e.Item.FindControl("ftTotalFunctionNum")).Text = BLL.ConvertRule.ToString(ViewState["ftTotalFunctionNum"]);
					((Label)e.Item.FindControl("ftTotalFunctionArea")).Text = BLL.ConvertRule.ToString(ViewState["ftTotalFunctionArea"]);
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
		/// �ؼ���ʼ��
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
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFunctionByBuildingCode(this._BuildingCode);

				#region --- �ϼ� --------------------------------------------------------------

				int[] arrSumInt = BLL.MathRule.SumIntColumn( entity.CurrentTable, new string[] {"FunctionNum"});
				decimal[] arrSumDec = BLL.MathRule.SumColumn( entity.CurrentTable, new string[] {"FunctionArea"});

				ViewState["ftTotalFunctionNum"] = arrSumInt[0];
				ViewState["ftTotalFunctionArea"] = arrSumDec[0];

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
