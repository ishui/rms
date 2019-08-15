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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// RoomList ��ժҪ˵����
	/// </summary>
	public partial class RoomList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlSelect sltModelCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltPBSTypeCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltInvState;
		protected System.Web.UI.HtmlControls.HtmlSelect sltOutState;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadDataGrid(true);
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//Ȩ��
				this.btnModifyArea.Visible = base.user.HasRight("010315");

				//���Ź������ʱ��ֻ�ɲ�ѯ
				if (this.txtProjectCode.Value == "") 
				{
					//���Ź���
					this.btnModifyArea.Style["display"] = "none";

					this.dgList.Columns[1].Visible = true;
					this.dgList.Columns[2].Visible = true;

					//��ҳ
					this.dgList.AllowPaging = true;

					((SearchRoom)this.tbSearchRoom).Visible = false;
					((SearchRoomAll)this.tbSearchRoomAll).SetProject(this.txtProjectCode.Value);
				}
				else 
				{
					//��Ŀ
					((SearchRoomAll)this.tbSearchRoomAll).Visible = false;
					((SearchRoom)this.tbSearchRoom).SetProject(this.txtProjectCode.Value);

					//����ҳ
					this.GridPagination1.DataGridId = "";
					this.GridPagination1.Visible = false;
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadDataGrid(bool isEmpty) 
		{
			try 
			{
				RoomStrategyBuilder sb = new RoomStrategyBuilder("V_ROOM");

				if (isEmpty)
					sb.AddStrategy(new Strategy( RoomStrategyName.False));

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( RoomStrategyName.ProjectCode, ProjectCode));

				if (ProjectCode == "") 
				{
					if (this.tbSearchRoomAll.Visible) 
					{
						((SearchRoomAll)this.tbSearchRoomAll).AddSearch(sb);
					}
				}
				else 
				{
					if (this.tbSearchRoom.Visible) 
					{
						((SearchRoom)this.tbSearchRoom).AddSearch(sb);
					}
				}

				//����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//ȱʡ����
					if (ProjectCode == "") 
					{
						sb.AddOrder("ProjectName", true);
					}

					sb.AddOrder("BuildingName", true);
					sb.AddOrder("ChamberCode", true);
					sb.AddOrder("FloorIndex", true);
					sb.AddOrder("RoomName", true);
				}

				string sql = sb.BuildMainQueryString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_ROOM",sql );
				qa.Dispose();

				string[] arrField = {"BuildArea", "Cost", "TotalPayMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);

				ViewState["SumCount"] = entity.CurrentTable.Rows.Count.ToString() + "��";
				ViewState["SumBuildArea"] = BLL.StringRule.BuildShowNumberString(arrSum[0]);
				ViewState["SumCost"] = BLL.StringRule.BuildShowNumberString(arrSum[1]);
				ViewState["SumTotalPayMoney"] = BLL.StringRule.BuildShowNumberString(arrSum[2]);

				dgList.DataSource = entity;
				dgList.DataBind();

				if (this.GridPagination1.Visible)
				{
					this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();
				}

				entity.Dispose();

				SetSelectRoomCode();

				//��ʾ������
				if (entity.HasRecord())
					this.trToolBar.Style["display"] = "";
				else
					this.trToolBar.Style["display"] = "none";
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void SetSelectRoomCode() 
		{
			//��¼�б��¼�ؼ��֣��á�,���ָ�
			string[] arr = new string[dgList.DataKeys.Count];
			dgList.DataKeys.CopyTo(arr, 0);
			this.txtSelectRoomCode.Value = BLL.ConvertRule.JoinArray(arr, ",");
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			//���Ź���ʱ���б��ҳ
			if (this.txtProjectCode.Value == "") 
			{
				this.dgList.AllowPaging = true;
				this.btnPrint.Value = "��ӡ��ҳ";
				this.btnPrintAll.Style["display"] = "";
				this.btnAllowPaging.Style["display"] = "";

				this.GridPagination1.DataGridId = "dgList";
				this.GridPagination1.Visible = true;
			}

			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid(false);
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid(false);
		}

		protected void btnAllowPaging_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.AllowPaging = false;
			this.btnPrint.Value = "�� ӡ";
			this.btnPrintAll.Style["display"] = "none";
			this.btnAllowPaging.Style["display"] = "none";

			this.GridPagination1.DataGridId = "";
			this.GridPagination1.Visible = false;

			this.dgList.CurrentPageIndex = 0;
			this.LoadDataGrid(false);
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
			((DataGrid)source).CurrentPageIndex = 0;
			LoadDataGrid(false);
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//��ʾ�ϼ�
				((Label)e.Item.FindControl("lblSumCount")).Text = BLL.ConvertRule.ToString(ViewState["SumCount"]);
				((Label)e.Item.FindControl("lblSumBuildArea")).Text = BLL.ConvertRule.ToString(ViewState["SumBuildArea"]);
				((Label)e.Item.FindControl("lblSumCost")).Text = BLL.ConvertRule.ToString(ViewState["SumCost"]);
				((Label)e.Item.FindControl("lblSumTotalPayMoney")).Text = BLL.ConvertRule.ToString(ViewState["SumTotalPayMoney"]);
			}
		}

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadDataGrid(false);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ӡȫ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrintAll_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				btnAllowPaging_ServerClick(sender, e);
				this.txtIsLoadPrint.Value = "1";
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ӡ����" + ex.Message));
			}
		}
	}
}
