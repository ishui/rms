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
	/// RoomIOList ��ժҪ˵����
	/// </summary>
	public partial class RoomIOList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadDataGrid();
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtIOType.Value = Request.QueryString["IOType"];

				RmsPM.BLL.PageFacade.LoadPBSTypeSelectFirstLevel(this.sltSearchCodeName,"");
//				RmsPM.BLL.PageFacade.LoadDictionarySelect(this.sltSearchOutAspect,"ȥ��","");

				string title = "";
				string outstate = "";

				outstate = this.txtIOType.Value;
//				outstate = BLL.ProductRule.TransTempRoomOutIOType(this.txtIOType.Value, true);

				if (outstate == "") 
				{
					throw new Exception("��������������͡�����Ϊ��");
				}

				switch (outstate) 
				{
					case "1":
						outstate = "���";
						break;

					case "2":
						outstate = "����";
						break;

					case "3":
						outstate = "�˿�";
						break;

					case "4":
						outstate = "Ԥ��";
						break;

//					default:
//						throw new Exception("δ֪�ĳ��������");
				}

				title = outstate + "��";
				this.spanTitle.InnerText = title;
				this.txtOutState.Value = outstate;

				this.dgList.Columns[3].HeaderText = this.txtOutState.Value + "����";
				this.spanOutDate.InnerText = this.txtOutState.Value;

				//���ء�ȥ��
				if ((this.txtOutState.Value != "����") && (this.txtOutState.Value != "Ԥ��") && (this.txtOutState.Value != "����") )
				{
					this.spanOutAspect.Style["display"] = "none";
					this.spanOutAspect2.Style["display"] = "none";
					this.dgList.Columns[4].Visible = false;
				}

				//Ȩ��
				switch (this.txtOutState.Value)
				{
					case "���":
						this.btnAdd.Visible = base.user.HasRight("011202");
						break;

					case "����":
						this.btnAdd.Visible = base.user.HasRight("011302");
						break;

					case "�˿�":
						this.btnAdd.Visible = base.user.HasRight("011402");
						break;
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				TempRoomOutStrategyBuilder sb = new TempRoomOutStrategyBuilder();

				sb.AddStrategy( new Strategy(  TempRoomOutStrategyName.Out_State,this.txtOutState.Value ));

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.ProjectCode, ProjectCode));

				if (this.txtSearchCurYear.Value != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.CurYear, this.txtSearchCurYear.Value));

				if ( this.txtSearchOutDateBegin.Value != "" || this.txtSearchOutDateEnd.Value != "" )
				{
					ArrayList ar = new ArrayList();
					ar.Add(this.txtSearchOutDateBegin.Value);
					ar.Add(this.txtSearchOutDateEnd.Value);
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.OutDateRange,ar ));
				}

				if (this.sltSearchCheckState.Value.Trim() != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.CheckState, this.sltSearchCheckState.Value));

				if (this.sltSearchCodeName.Value.Trim() != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.CodeName, this.sltSearchCodeName.Value));

				if (this.txtSearchOutAspect.Value.Trim() != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.OutAspect, this.txtSearchOutAspect.Value));

				string BuildingName = this.txtSearchBuildingName.Value;
				if (BuildingName != "")
					sb.AddStrategy(new Strategy(TempRoomOutStrategyName.InBuildingName, BuildingName));

				string ChamberName = this.txtSearchChamberName.Value;
				if (ChamberName != "")
					sb.AddStrategy(new Strategy(TempRoomOutStrategyName.InChamberName, ChamberName));

				string RoomName = this.txtSearchRoomName.Value;
				if (RoomName != "")
					sb.AddStrategy(new Strategy(TempRoomOutStrategyName.InRoomName, RoomName));

				sb.AddOrder("CodeName",true);
				sb.AddOrder("CurYear",false);
				sb.AddOrder("SumNo",false);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "TempRoomOut",sql );
				qa.Dispose();

				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}
	}
}
