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
using RmsPM.DAL;
using RmsPM.BLL;
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectDictItem ��ժҪ˵����
	/// </summary>
	public partial class SelectDictItem : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton Button2;
		protected System.Web.UI.HtmlControls.HtmlInputText txtKGYear;
		protected System.Web.UI.HtmlControls.HtmlSelect SelectStatus;
		protected System.Web.UI.HtmlControls.HtmlInputText txtJGYear;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtDictionaryName.Value = Request.QueryString["DictionaryName"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtType.Value = Request.QueryString["Type"];
				this.txtType.Value = this.txtType.Value.ToLower();

				this.spanTitle.InnerText = this.txtDictionaryName.Value;
				this.spanName.InnerText = this.txtDictionaryName.Value;
				this.dgList.Columns[1].HeaderText = this.txtDictionaryName.Value;
				this.dgList.Columns[2].HeaderText = this.txtDictionaryName.Value;

				switch (this.txtType.Value) 
				{
					case "multi":
						//��ѡ
						this.dgList.Columns[0].Visible = true;
						this.dgList.Columns[1].Visible = true;
						this.dgList.Columns[2].Visible = false;

						this.trMulti1.Style["display"] = "block";
						this.trSingle1.Style["display"] = "none";
						break;

					default:
						//��ѡ
						break;
				}
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"�����ֵ�ѡ���б�ҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�����ֵ�ѡ���б�ҳ�����"));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				RmsPM.DAL.QueryStrategy.DictionaryItemStrategyBuilder ssb= new RmsPM.DAL.QueryStrategy.DictionaryItemStrategyBuilder();

				ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.DictionaryItemStrategyName.DictionaryName,this.txtDictionaryName.Value));

				if ( this.txtProjectCode.Value.Length > 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.DictionaryItemStrategyName.ProjectCode,this.txtProjectCode.Value));
				if (this.txtSearchValue.Value.Trim().Length > 0)
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.DictionaryItemStrategyName.NameLike,this.txtSearchValue.Value));
				
				//����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//ȱʡ����
					ssb.AddOrder("SortID",true);
				}

				string sql = ssb.BuildQueryViewString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				DataTable tb = qa.ExecSqlForDataSet(sql).Tables[0];
				qa.Dispose();
				
				this.dgList.DataSource = tb;
				this.dgList.DataBind();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"�����ֵ�ѡ���б����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�����ֵ�ѡ���б����"));
			}
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);

		}
		#endregion

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
			((DataGrid)source).CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
		}

		
	}
}
