using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// GroundWorkChart
	/// </summary>
	public partial class GroundWorkChart : PageBase
	{

//		protected decimal MonthPixel = 50;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadChart();
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void IniPage() 
		{
			try
			{
				this.txtGroundWorkCode.Value = Request.QueryString["GroundWorkCode"];	
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		private void LoadChart() 
		{
			try 
			{
				string GroundWorkCode = this.txtGroundWorkCode.Value;

				if (GroundWorkCode != "") 
				{
					EntityData entity = DAL.EntityDAO.ConstructDAO.GetGroundWorkByCode(GroundWorkCode);
					if (entity.HasRecord()) 
					{
						this.txtWBSCode.Value = entity.GetString("WBSCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						//显示背景图
						EntityData entityA = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode("GroundWork", this.txtWBSCode.Value);
						if (entityA.HasRecord()) 
						{
							string AttachMentCode = entityA.GetString("AttachMentCode");
							this.imgBg.Src = "../Project/ShowAttachPicture.aspx?AttachMentCode=" + AttachMentCode;
						}
						entityA.Dispose();
					}
					entity.Dispose();
				}

				LoadChartDtl();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"显示图表失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示图表失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示区域图
		/// </summary>
		private void LoadChartDtl()
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value;

				DataTable tb = new DataTable();
				tb.Columns.Add("WBSCode");
				tb.Columns.Add("TaskName");
				tb.Columns.Add("AttachMentCode");
				tb.Columns.Add("ImageName");
				tb.Columns.Add("Status", typeof(int));
				tb.Columns.Add("CompletePercent", typeof(int));
				tb.Columns.Add("CompleteFlag", typeof(int));
				tb.Columns.Add("Color");

				tb.Columns.Add("ObjectX", typeof(int));
				tb.Columns.Add("ObjectY", typeof(int));

				int defaultX = 20;
				int defaultY = 20;

				int[] arrCount = new int[7];
				foreach(int c in arrCount)
				{
					arrCount[c] = 0;
				}

				//第1级工作子项为区域名称
				EntityData entity = DAL.EntityDAO.WBSDAO.GetChildTask(WBSCode);
				foreach(DataRow dr in entity.CurrentTable.Rows)
				{
					string aWBSCode = dr["WBSCode"].ToString();

					DataRow drNew = tb.NewRow();

					drNew["WBSCode"] = dr["WBSCode"];
					drNew["TaskName"] = dr["TaskName"];

					//状态
					int status = BLL.ConvertRule.ToInt(dr["Status"]);
					int CompletePercent = BLL.ConvertRule.ToInt(dr["CompletePercent"]);
					drNew["Status"] = dr["Status"];
					drNew["CompletePercent"] = dr["CompletePercent"];

					//计算当前阶段
					int flag = BLL.ConstructProgRule.GetGroundWorkChartState(dr);
					arrCount[flag]++;
					drNew["CompleteFlag"] = flag;

					drNew["Color"] = BLL.ConstructProgRule.GetGroundWorkChartColorByState(flag);

					//取图片
                    EntityData entityAttach = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode("GroundWork", aWBSCode);
					if (entityAttach.HasRecord()) 
					{
						drNew["AttachMentCode"] = entityAttach.GetString("AttachMentCode");
						drNew["ImageName"] = entityAttach.GetString("FileName");
					}

					int x = 0;
					int y = 0;

					//取区域的位置
					EntityData entityG = DAL.EntityDAO.ConstructDAO.GetGroundWorkByWBSCode(aWBSCode);
					if (entityG.HasRecord()) 
					{
						x = entityG.GetInt("ObjectX");
						y = entityG.GetInt("ObjectY");
					}
					else 
					{
						//生成缺省位置
						x = defaultX;
						y = defaultY;

						defaultX = x;
						defaultY = y + 40;
					}
					entityG.Dispose();

					drNew["ObjectX"] = x;
					drNew["ObjectY"] = y;

					tb.Rows.Add(drNew);
				}
				entity.Dispose();

				this.dgList.DataSource = tb;
				this.dgList.DataBind();

				this.dgList2.DataSource = tb;
				this.dgList2.DataBind();

				//图例
				DataTable tbLegend = BLL.ConstructProgRule.GetGroundWorkChartLegend();
				foreach(DataRow dr in tbLegend.Rows) 
				{
					int i = BLL.ConvertRule.ToInt(dr["State"]);
					dr["Count"] = arrCount[i];
				}

				this.dgLegend.DataSource = tbLegend;
				this.dgLegend.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		/*
		/// <summary>
		/// 显示区域图
		/// </summary>
		private void LoadChartDtl()
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value;

				DataTable tb = new DataTable();
				tb.Columns.Add("WBSCode");
				tb.Columns.Add("TaskName");
				tb.Columns.Add("AttachMentCode");
				tb.Columns.Add("ImageName");
				tb.Columns.Add("ChildWBSCode");
				tb.Columns.Add("ChildTaskName");
				tb.Columns.Add("ChildStatus", typeof(int));
				tb.Columns.Add("ChildCompletePercent", typeof(int));

				tb.Columns.Add("ObjectX", typeof(int));
				tb.Columns.Add("ObjectY", typeof(int));

				int defaultX = 20;
				int defaultY = 20;

				//第1级工作子项为区域名称
				EntityData entity = DAL.EntityDAO.WBSDAO.GetChildTask(WBSCode);
				foreach(DataRow dr in entity.CurrentTable.Rows)
				{
					string aWBSCode = dr["WBSCode"].ToString();

					DataRow drNew = tb.NewRow();

					drNew["WBSCode"] = dr["WBSCode"];
					drNew["TaskName"] = dr["TaskName"];

					//取区域的当前阶段
					DataRow drCurr = GetCurrentState(aWBSCode);
					if (drCurr != null) 
					{
						string bWBSCode = drCurr["WBSCode"].ToString();

						drNew["ChildWBSCode"] = drCurr["WBSCode"];
						drNew["ChildTaskName"] = drCurr["TaskName"];
						drNew["ChildStatus"] = drCurr["Status"];
						drNew["ChildCompletePercent"] = drCurr["CompletePercent"];

						//取图片
						EntityData entityAttach = DAL.EntityDAO.WBSDAO.GetAttachMentByMasterCode("GroundWork", bWBSCode);
						if (entityAttach.HasRecord()) 
						{
							drNew["AttachMentCode"] = entityAttach.GetString("AttachMentCode");
							drNew["ImageName"] = entityAttach.GetString("FileName");
						}
						entityAttach.Dispose();
					}

					int x = 0;
					int y = 0;

					//取区域的位置
					EntityData entityG = DAL.EntityDAO.ConstructDAO.GetGroundWorkByWBSCode(aWBSCode);
					if (entityG.HasRecord()) 
					{
						x = entityG.GetInt("ObjectX");
						y = entityG.GetInt("ObjectY");
					}
					else 
					{
						//生成缺省位置
						x = defaultX;
						y = defaultY;

						defaultX = x;
						defaultY = y + 40;
					}
					entityG.Dispose();

					drNew["ObjectX"] = x;
					drNew["ObjectY"] = y;

					tb.Rows.Add(drNew);
				}
				entity.Dispose();

				this.dgList.DataSource = tb;
				this.dgList.DataBind();

				this.dgList2.DataSource = tb;
				this.dgList2.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 取区域的当前阶段
		/// </summary>
		/// <param name="WBSCode"></param>
		/// <returns></returns>
		private DataRow GetCurrentState(string WBSCode) 
		{
			try 
			{
				DataRow dr = null;

				//取所有子项，倒排序
				EntityData entityB = DAL.EntityDAO.WBSDAO.GetChildTask(WBSCode);
				DataView dvB = new DataView(entityB.CurrentTable, "", "SortID desc", DataViewRowState.CurrentRows);

				//取最后一条进行中的子项作为当前状态项
				foreach(DataRow drB in entityB.CurrentTable.Rows)
				{
					string bWBSCode = drB["WBSCode"].ToString();

					int Status = BLL.ConvertRule.ToInt(drB["Status"]);
					int CompletePercent = BLL.ConvertRule.ToInt(drB["CompletePercent"]);

					if ((Status == 1) || (Status == 4))
					{
						dr = drB;
						return dr;
					}
				}
				entityB.Dispose();

				return dr;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}
*/

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			try 
			{
				string AttachMentCode = ((HtmlInputHidden)e.Item.FindControl("txtAttachMentCode")).Value;
				string Color = ((HtmlInputHidden)e.Item.FindControl("txtColor")).Value;

				HtmlImage img = (HtmlImage)e.Item.FindControl("imgItem");

				if (AttachMentCode != "") 
				{
					img.Src = "../Project/ShowAttachPicture.aspx?AttachMentCode=" + AttachMentCode;
					img.Style["background-color"] = Color;
					img.Style["display"] = "";
				}
				else 
				{
					img.Style["display"] = "none";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}
	}
}
