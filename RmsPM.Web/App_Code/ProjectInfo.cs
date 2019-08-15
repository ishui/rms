using System;
using Rms.ORMap;
using RmsPM.DAL;

namespace RmsPM.Web
{
	/// <summary>
	/// Project 的摘要说明。
	/// </summary>
	public class ProjectInfo
	{
		private string m_ProjectCode = "";
		private string m_ProjectName = "";
		private string m_SubjectSetCode = "";

		public ProjectInfo()
		{
		}

		public string ProjectCode 
		{
			get{return m_ProjectCode;}
		}

		public string ProjectName 
		{
			get{return m_ProjectName;}
		}

		public string SubjectSetCode 
		{
			get{return m_SubjectSetCode;}
		}

		public void Clear() 
		{
			m_ProjectCode = "";
			m_ProjectName = "";
		}

		public void Reset(string a_ProjectCode) 
		{
			try
			{
				Clear();

				EntityData entity = DAL.EntityDAO.ProjectDAO.GetProjectByCode(a_ProjectCode);
				if ( !entity.HasRecord())
					return;

				m_ProjectCode = entity.GetString("ProjectCode");
				m_ProjectName = entity.GetString("ProjectName");
				m_SubjectSetCode = entity.GetString("SubjectSetCode");

				entity.Dispose();
			}
			catch( Exception ex )
			{
				throw ex;
			}
		}

	}
}
