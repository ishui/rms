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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL;
using Rms.Web;
using RmsPM.BLL;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// LocationSava ��ժҪ˵����
	/// </summary>
	public partial class LocationSava : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				SavaLocation();
			}
		}

		private void SavaLocation()
		{
			try
			{
				string locationData=""+Request["str"];
				string projectCode=""+Request["ProjectCode"];

				if (locationData.Length>0)
				{
					string[] arrL=locationData.Split("$".ToCharArray());
					for (int i=0;i<arrL.Length;i++)
					{
						if (arrL[i].Length>0)
						{
							string[] arrB=arrL[i].Split("|".ToCharArray());

							EntityData entity=ProductDAO.GetBuildingByCode(arrB[1]);
							if (entity.HasRecord())
							{
								DataRow dr=entity.CurrentRow;

								dr["ObjectX"]=int.Parse(arrB[2]);
								dr["ObjectY"]=int.Parse(arrB[3]);

								ProductDAO.UpdateBuilding(entity);

							}
							entity.Dispose();
						}
					}
				}

				
				Response.Redirect("../PBS/Building_l.aspx?ProjectCode="+projectCode);
				
				
				

			}
			catch(Exception ex)
			{
				throw ex;
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
		}
		#endregion
	}
}
