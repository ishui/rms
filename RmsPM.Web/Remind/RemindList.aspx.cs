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
	/// RemindList ��ժҪ˵����
	/// </summary>
	public partial class RemindList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// �ڴ˴������û������Գ�ʼ��ҳ��
				InitPage();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"����������Ϣʧ�ܣ�");
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
				// �����룬������ƣ�������ȼ�			
				entity.CurrentTable.Columns.Add("TypeName");
				entity.CurrentTable.Columns.Add("ActiveName");
				entity.CurrentTable.Columns.Add("ObjectName");
				entity.CurrentTable.Columns.Add("RemindDayType");
				entity.CurrentTable.Columns.Add("RemindRand");
				foreach(DataRow dr in entity.CurrentTable.Rows)
				{
					// ���ݱ��ȡ��������ƺ�������ȼ�
					for(int i=0;i<ComSource.arRemind.Length;i++)
					{
						if(dr["Type"].ToString()==ComSource.arRemind[i][0])
						{
							dr["TypeName"] = ComSource.arRemind[i][1];
							dr["RemindRand"] = ComSource.arRemind[i][2];
						}
					}
					//0���룬1�ල��2����
					if(dr["Type"].ToString()=="0"||dr["Type"].ToString()=="3")
					{
						string strTmp = dr["ObjectCode"].ToString();
						string strObjectName = "";
						if(strTmp.IndexOf('0')>-1)  strObjectName +=" ������";
						if(strTmp.IndexOf('1')>-1)  strObjectName +=" �ල��";
						if(strTmp.IndexOf('2')>-1)  strObjectName +=" ������";
						dr["ObjectName"] = strObjectName;
					}
					else
					{
						try
						{
							// ȡ�ø�λ
							EntityData entityObject = DAL.EntityDAO.OBSDAO.GetStationByCode(dr["ObjectCode"].ToString());
							if(entityObject.HasRecord())
								dr["ObjectName"] = entityObject.CurrentTable.Rows[0]["StationName"].ToString();
						}
						catch(Exception ex)
						{
							throw new Exception(ex.Message+"��ȡ���û���λʧ��",ex);
						}
					}
					//��Ч
					if (dr["IsActive"].ToString() == "1")
					{
						dr["ActiveName"] = "��Ч";
					}
					else
					{
						dr["ActiveName"] = "δ��Ч";
					}

					// ��ǰ���Ӻ�
					if (int.Parse(dr["RemindDay"].ToString())>0)
					{
						dr["RemindDayType"] = "��ǰ"+dr["RemindDay"].ToString()+"��";
					}
					else
					{
						dr["RemindDayType"] = "�ͺ�"+dr["RemindDay"].ToString().Substring(1)+"��";
					}

					//dr["ObjectName"] = BLL.RemindRule.GetObjectName(dr["ObjectCode"].ToString());
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"����������Ϣʧ�ܣ�");
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
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ��������Ϣʧ�ܣ�");
			}
		}
	}
}
