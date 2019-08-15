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
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// Building_l 的摘要说明。
	/// </summary>
	public partial class Building_l : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtParentCode.Value = Request.QueryString["ParentCode"];
				
				LoadData(this.txtProjectCode.Value,this.txtParentCode.Value);
			}
		}

		private void LoadData(string projectCode,string parentCode)
		{
			try
			{
				if (parentCode != "") 
				{
					this.trArea.Style["display"] = "";
					this.lblAreaName.Text = BLL.ProductRule.GetBuildingName(parentCode);
				}

              

				string CurrUrl = "Building_l.aspx?ProjectCode=" + this.txtProjectCode.Value;

				DataTable dt=new DataTable();	
				
				dt.Columns.Add(new DataColumn("ObjLeft", typeof(String)));
				dt.Columns.Add(new DataColumn("ObjTop", typeof(String)));
				dt.Columns.Add(new DataColumn("Events", typeof(String)));
				dt.Columns.Add(new DataColumn("BuildingName", typeof(String)));
				dt.Columns.Add(new DataColumn("BuildingCode", typeof(String)));
				dt.Columns.Add(new DataColumn("IsArea", typeof(String)));	
				dt.Columns.Add("Color", typeof(string));
				dt.Columns.Add("Desc", typeof(string));
				dt.Columns.Add("RoomCount", typeof(int));
				dt.Columns.Add("ChildCount", typeof(int));
				
				DataRow dr=null;

				DataView dv=new DataView();

				//查询房间套数
				BuildingStrategyBuilder sb = new BuildingStrategyBuilder();
				sb.AddStrategy( new Strategy(  BuildingStrategyName.ProjectCode,projectCode ));
				sb.AddStrategy( new Strategy(  BuildingStrategyName.ParentCode,parentCode ));
				string sql = sb.BuildQueryRoomCountString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Building",sql );
				qa.Dispose();

//				EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetBuildingByProjectParentCode(projectCode,parentCode);

				if (entity.HasRecord())
				{
					int x=10;
					int y=20;
					for(int i=0;i<=entity.CurrentTable.Rows.Count-1;i++)
					{
						entity.SetCurrentRow(i);
						dr = dt.NewRow();
						
						int objX=0;
						int objY=0;


						
						if (i%5==0)
						{
							y=y+20;
							x=x;
						}
						string strX=entity.CurrentTable.Rows[i]["ObjectX"].ToString();
						string strY=entity.CurrentTable.Rows[i]["ObjectY"].ToString();
						if (strX.Length>0)
						{
							if (int.Parse(strX)==0)
							{		
								objX=x;
							}
							else
							{
								objX=int.Parse(entity.CurrentTable.Rows[i]["ObjectX"].ToString());
								
							}
						}
						else
						{
							objX=x;
						}
						if (strY.Length>0)
						{
							if (int.Parse(strY)==0)
							{
								objY=y+40;
							}
							else
							{							
								objY=int.Parse(entity.CurrentTable.Rows[i]["ObjectY"].ToString());
							}
						}
						else
						{
							objY=y+40;
						}
						x=x+80;

						dr[0]=objX;
						dr[1]=objY;
						
						dr[3] = entity.CurrentTable.Rows[i]["BuildingName"];
						dr[4] = entity.CurrentTable.Rows[i]["BuildingCode"];
						dr[5] = entity.CurrentTable.Rows[i]["IsArea"];
						dr["RoomCount"] = entity.GetInt("RoomCount");
						dr["ChildCount"] = entity.GetInt("ChildCount");

						//用颜色区分区域、楼栋
						if (entity.CurrentTable.Rows[i]["IsArea"].ToString() == "1")
						{
							//区域
							dr[2]="doMapArea(\""+entity.CurrentTable.Rows[i]["ProjectCode"]+"\",\""+entity.CurrentTable.Rows[i]["BuildingCode"]+"\")";
							dr["Color"] = "yellow";
							dr["Desc"] = dr["BuildingName"].ToString() + " (" + BLL.ConvertRule.ToInt(dr["ChildCount"]).ToString() + "幢)";
						}
						else
						{
							//楼栋
							dr[2]="doMapBuilding(\""+entity.CurrentTable.Rows[i]["BuildingCode"]+"\",\"" + CurrUrl + "\")";
							dr["Color"] = "";
							dr["Desc"] = dr["BuildingName"].ToString() + " (" + BLL.ConvertRule.ToInt(dr["RoomCount"]).ToString() + "套)";
						}
						dt.Rows.Add(dr);
					}

					dv = new DataView(dt);
				}
				entity.Dispose();

				this.dlBuild.DataSource=dv;
				this.dlBuild.DataBind();

				string fieldNameSrc="";
				if (parentCode.Length>0)
				{
					EntityData entity1=RmsPM.DAL.EntityDAO.ProductDAO.GetBuildingByCode(parentCode);
					fieldNameSrc="AreaImageCode";
					if (entity1.HasRecord())
					{
						if ( ""!=entity1.GetString(fieldNameSrc)+"" )
						{
							this.imgMain.Src="ShowPicture.aspx?FileID="+entity1.GetString(fieldNameSrc);
						}
						else
						{
							this.imgMain.Visible=false;
						}
					}
					entity1.Dispose();

				}
				else
				{
					EntityData entity2=RmsPM.DAL.EntityDAO.ProjectDAO.GetProjectByCode(projectCode);
					fieldNameSrc="ImagePath";
					if (entity2.HasRecord())
					{
						if ( ""!=entity2.GetString(fieldNameSrc)+"" )
						{
							this.imgMain.Src="ShowPicture.aspx?FileID="+entity2.GetString(fieldNameSrc);
						}
						else
						{
							this.imgMain.Visible=false;
						}
					}
					entity2.Dispose();

				}
				
				//小区平面图汇总图例
				//DataTable tbLegend = BLL.ProductRule.GetBuildingLocationLegend(entity.CurrentTable);
				//this.repLegend.DataSource = tbLegend;
				//this.repLegend.DataBind();
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
