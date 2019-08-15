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
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
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

						//��ʾ����ͼ
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
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾͼ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼ��ʧ�ܣ�" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ����ͼ
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

				//��1����������Ϊ��������
				EntityData entity = DAL.EntityDAO.WBSDAO.GetChildTask(WBSCode);
				foreach(DataRow dr in entity.CurrentTable.Rows)
				{
					string aWBSCode = dr["WBSCode"].ToString();

					DataRow drNew = tb.NewRow();

					drNew["WBSCode"] = dr["WBSCode"];
					drNew["TaskName"] = dr["TaskName"];

					//״̬
					int status = BLL.ConvertRule.ToInt(dr["Status"]);
					int CompletePercent = BLL.ConvertRule.ToInt(dr["CompletePercent"]);
					drNew["Status"] = dr["Status"];
					drNew["CompletePercent"] = dr["CompletePercent"];

					//���㵱ǰ�׶�
					int flag = BLL.ConstructProgRule.GetGroundWorkChartState(dr);
					arrCount[flag]++;
					drNew["CompleteFlag"] = flag;

					drNew["Color"] = BLL.ConstructProgRule.GetGroundWorkChartColorByState(flag);

					//ȡͼƬ
                    EntityData entityAttach = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode("GroundWork", aWBSCode);
					if (entityAttach.HasRecord()) 
					{
						drNew["AttachMentCode"] = entityAttach.GetString("AttachMentCode");
						drNew["ImageName"] = entityAttach.GetString("FileName");
					}

					int x = 0;
					int y = 0;

					//ȡ�����λ��
					EntityData entityG = DAL.EntityDAO.ConstructDAO.GetGroundWorkByWBSCode(aWBSCode);
					if (entityG.HasRecord()) 
					{
						x = entityG.GetInt("ObjectX");
						y = entityG.GetInt("ObjectY");
					}
					else 
					{
						//����ȱʡλ��
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

				//ͼ��
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/*
		/// <summary>
		/// ��ʾ����ͼ
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

				//��1����������Ϊ��������
				EntityData entity = DAL.EntityDAO.WBSDAO.GetChildTask(WBSCode);
				foreach(DataRow dr in entity.CurrentTable.Rows)
				{
					string aWBSCode = dr["WBSCode"].ToString();

					DataRow drNew = tb.NewRow();

					drNew["WBSCode"] = dr["WBSCode"];
					drNew["TaskName"] = dr["TaskName"];

					//ȡ����ĵ�ǰ�׶�
					DataRow drCurr = GetCurrentState(aWBSCode);
					if (drCurr != null) 
					{
						string bWBSCode = drCurr["WBSCode"].ToString();

						drNew["ChildWBSCode"] = drCurr["WBSCode"];
						drNew["ChildTaskName"] = drCurr["TaskName"];
						drNew["ChildStatus"] = drCurr["Status"];
						drNew["ChildCompletePercent"] = drCurr["CompletePercent"];

						//ȡͼƬ
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

					//ȡ�����λ��
					EntityData entityG = DAL.EntityDAO.ConstructDAO.GetGroundWorkByWBSCode(aWBSCode);
					if (entityG.HasRecord()) 
					{
						x = entityG.GetInt("ObjectX");
						y = entityG.GetInt("ObjectY");
					}
					else 
					{
						//����ȱʡλ��
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ȡ����ĵ�ǰ�׶�
		/// </summary>
		/// <param name="WBSCode"></param>
		/// <returns></returns>
		private DataRow GetCurrentState(string WBSCode) 
		{
			try 
			{
				DataRow dr = null;

				//ȡ�������������
				EntityData entityB = DAL.EntityDAO.WBSDAO.GetChildTask(WBSCode);
				DataView dvB = new DataView(entityB.CurrentTable, "", "SortID desc", DataViewRowState.CurrentRows);

				//ȡ���һ�������е�������Ϊ��ǰ״̬��
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}
	}
}
