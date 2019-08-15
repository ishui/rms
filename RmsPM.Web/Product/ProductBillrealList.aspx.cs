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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;


namespace RmsPM.Web.Product
{
	/// <summary>
	/// ProductBillrealList 的摘要说明。
	/// </summary>
	public partial class ProductBillrealList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadData(""+Request["Code"]);
			}
		}
			
		private void LoadData(string code)
		{
			try
			{
				string outState="";
				
				EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetTempRoomOutByCode(code);
				if (!entity.HasRecord())
				{
					return;
				}

				//				fontOutlistName.InnerHtml="<B>"+entity.GetString("OutlistName")+"</B>";
				fontOutAspect.InnerHtml="<B>"+entity.GetString("OutAspect")+"</B>";
				fontCodeName.InnerHtml="<B>"+entity.GetString("OutlistName")+"</B>";
//				fontCodeName.InnerHtml="<B>"+entity.GetString("CodeName")+"&nbsp;<"+entity.GetInt("CurYear").ToString()+">"+entity.GetInt("SumNo").ToString()+"</B>";
				fontOutDate.InnerHtml = entity.GetDateTimeOnlyDate("Out_Date");
				outState=entity.GetString("Out_State");
				this.tdConferMark.InnerHtml=entity.GetString("ConferMark")+"&nbsp;";
				this.tdRemark.InnerHtml=entity.GetString("Remark")+"&nbsp;";

				string ProjectCode = entity.GetString("ProjectCode");
				string ProjectName = BLL.ProjectRule.GetProjectName(ProjectCode);

				entity.Dispose();

				//明细
				EntityData entityDtl = DAL.EntityDAO.ProductDAO.GetRoomByOutListCode(code, "V_ROOM");

				DataTable tbDtl = new DataTable();
				tbDtl.Columns.Add(new DataColumn("ProjectName", typeof(String)));
				tbDtl.Columns.Add(new DataColumn("BuildingName", typeof(String)));
				tbDtl.Columns.Add(new DataColumn("ChamberName", typeof(String)));
				tbDtl.Columns.Add(new DataColumn("RoomName", typeof(String)));
				tbDtl.Columns.Add(new DataColumn("BuildArea", typeof(decimal)));

				decimal TotalBuildArea = 0;
				foreach(DataRow dr in entityDtl.CurrentTable.Rows) 
				{
					DataRow drNew = tbDtl.NewRow();

					drNew["ProjectName"] = ProjectName;
					drNew["BuildingName"] = BLL.ConvertRule.ToString(dr["BuildingName"]);
					drNew["ChamberName"] = BLL.ConvertRule.ToString(dr["ChamberName"]);
					drNew["RoomName"] = BLL.ConvertRule.ToString(dr["RoomName"]);

					decimal BuildArea;

					if (outState=="调拨")
					{
						BuildArea = BLL.ConvertRule.ToDecimal(dr["PreBuildArea"]);
					}
					else
					{
						BuildArea = BLL.ConvertRule.ToDecimal(dr["BuildArea"]);
					}

					drNew["BuildArea"] = BuildArea;

					tbDtl.Rows.Add(drNew);

					TotalBuildArea = TotalBuildArea + BuildArea;
				}

				/*
				DataTable dt1=new DataTable();	

				dt1.Columns.Add(new DataColumn("BuildingName", typeof(String)));
				dt1.Columns.Add(new DataColumn("ChamberName", typeof(String)));
				dt1.Columns.Add(new DataColumn("RoomAdress", typeof(String)));
				dt1.Columns.Add(new DataColumn("RoomName", typeof(String)));
				dt1.Columns.Add(new DataColumn("BuildIndex", typeof(String)));
				
				DataRow dr1=null;
				DataView dv1=new DataView();
				int roomNum=0;
				decimal buildIndex=0;
				
				EntityData entity1=RmsPM.DAL.EntityDAO.ProductDAO.GetTempRoomStructureByOutListCode(code);
				if (entity1.HasRecord())
				{
					for(int i=0;i<=entity1.CurrentTable.Rows.Count-1;i++)
					{
						dr1 = dt1.NewRow();
						roomNum=roomNum+1;
						
						dr1[0]=RmsPM.BLL.ProjectRule.GetProjectName(entity1.CurrentTable.Rows[i]["ProjectCode"].ToString());
						dr1[1]=RmsPM.BLL.ProductRule.GetAllBuildingNameByCode(entity1.CurrentTable.Rows[i]["TempBuildingCode"].ToString());
						dr1[2]=RmsPM.BLL.ProductRule.GetChamberNameByCode(entity1.CurrentTable.Rows[i]["TempChamberCode"].ToString());
						
						string roomName="";
						string roomdim="";

						EntityData entity2=RmsPM.DAL.EntityDAO.ProductDAO.GetRoomByCode(entity1.CurrentTable.Rows[i]["TempRoomCode"].ToString());
						if (entity2.HasRecord())
						{
							roomName=entity2.GetString("RoomName");

							if (outState=="调拨")
							{
								roomdim=entity2.GetDecimal("PreBuildArea").ToString();
							
								buildIndex=buildIndex+entity2.GetDecimal("PreBuildArea");
							}
							else
							{
								roomdim=entity2.GetDecimal("BuildArea").ToString();
							
								buildIndex=buildIndex+entity2.GetDecimal("BuildArea");
							}
						}
						entity2.Dispose();
						
						dr1[3]=roomName;
						dr1[4]=roomdim;						

						dt1.Rows.Add(dr1);
					}
					
					this.tdTotalRoomNum.InnerHtml=roomNum.ToString();
					this.tdTotalRoomBuildIndex.InnerHtml=buildIndex.ToString("N");
					dv1 = new DataView(dt1);
				}
				entity1.Dispose();
*/

				this.tdTotalRoomNum.InnerHtml = tbDtl.Rows.Count.ToString();
				this.tdTotalRoomBuildIndex.InnerHtml = TotalBuildArea.ToString("0.00");

				this.repeat1.DataSource = tbDtl;
				this.repeat1.DataBind();
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
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
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
