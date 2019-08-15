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

using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.Web;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Remind
{
	/// <summary>
	/// RemindList 的摘要说明。
	/// </summary>
	public partial class RemindList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// 在此处放置用户代码以初始化页面
				InitPage();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"载入提醒信息失败！");
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
			this.dgRemindList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgRemindList_DeleteCommand);

		}
		#endregion

		private void InitPage()
		{
			EntityData entityRemind = RemindDAO.GetRemindStrategyByProjectCode(Session["ProjectCode"].ToString());
			DisposeRemind(entityRemind);
			this.dgRemindList.DataSource= entityRemind.CurrentTable;
			this.dgRemindList.DataBind();
			this.trNoRemind.Visible = entityRemind.CurrentTable.Rows.Count>0?false:true;
			entityRemind.Dispose();
		}

		private void DisposeRemind(EntityData entity)
		{
			try
			{
				// 类别编码，类别名称，类别优先级			
				entity.CurrentTable.Columns.Add("TypeName");
				entity.CurrentTable.Columns.Add("ActiveName");
				entity.CurrentTable.Columns.Add("ObjectName");
				entity.CurrentTable.Columns.Add("RemindDayType");
				entity.CurrentTable.Columns.Add("RemindRand");
				foreach(DataRow dr in entity.CurrentTable.Rows)
				{
					// 根据编号取得类别名称和类别优先级
					for(int i=0;i<ComSource.arRemind.Length;i++)
					{
						if(dr["Type"].ToString()==ComSource.arRemind[i][0])
						{
							dr["TypeName"] = ComSource.arRemind[i][1];
							dr["RemindRand"] = ComSource.arRemind[i][2];
						}
					}
					//0参与，1监督，2负责
					if(dr["Type"].ToString()=="0"||dr["Type"].ToString()=="3")
					{
						string strTmp = dr["ObjectCode"].ToString();
						string strObjectName = "";
						if(strTmp.IndexOf('0')>-1)  strObjectName +=" 参与人";
						if(strTmp.IndexOf('1')>-1)  strObjectName +=" 监督人";
						if(strTmp.IndexOf('2')>-1)  strObjectName +=" 责任人";
						dr["ObjectName"] = strObjectName;
					}
					else
					{
						try
						{
							// 取得岗位
							EntityData entityObject = DAL.EntityDAO.OBSDAO.GetStationByCode(dr["ObjectCode"].ToString());
							if(entityObject.HasRecord())
								dr["ObjectName"] = entityObject.CurrentTable.Rows[0]["StationName"].ToString();
						}
						catch(Exception ex)
						{
							throw new Exception(ex.Message+"：取得用户岗位失败",ex);
						}
					}
					//生效
					if (dr["IsActive"].ToString() == "1")
					{
						dr["ActiveName"] = "生效";
					}
					else
					{
						dr["ActiveName"] = "未生效";
					}

					// 提前或延后
					if (int.Parse(dr["RemindDay"].ToString())>0)
					{
						dr["RemindDayType"] = "提前"+dr["RemindDay"].ToString()+"天";
					}
					else
					{
						dr["RemindDayType"] = "滞后"+dr["RemindDay"].ToString().Substring(1)+"天";
					}

					//dr["ObjectName"] = BLL.RemindRule.GetObjectName(dr["ObjectCode"].ToString());
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"处理提醒信息失败！");
			}
		}

		private void dgRemindList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string strCode = this.dgRemindList.DataKeys[(int)e.Item.ItemIndex].ToString();
				EntityData entity = RemindDAO.GetRemindStrategyByCode(strCode);
				RemindDAO.DeleteRemindStrategy(entity);

				this.InitPage();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"删除提醒信息失败！");
			}
		}
	}
}
