using System;
using System.Data;
using System.Collections;
using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web
{
	/// <summary>
	/// �û��࣬���л���Ŀʱ���������¼��ظ��û�����Ŀ�е�Ȩ�ޣ���ɫ����λ���ȵ���Ϣ��
	/// </summary>
	public class User
	{
		private string m_UserCode = "";
		private string m_UserName = "";
        private string m_UserShortName = "";
        private string m_UserID = "";
		private string m_WorkNO = "";

		private int m_Status = 0;
		private string m_CurrentProjectCode = "";
		private bool m_IsSystemManager = false;

		/// <summary>
		/// �û��ĸ�λ
		/// </summary>
		private DataTable m_DataTableStation = null;

//		/// <summary>
//		/// �û��ĸ�λ�������ܷ��ʵ���Դ��
//		/// </summary>
//		private ArrayList m_ClassArray = new ArrayList();

		/// <summary>
		/// �û��ĸ�λ�������ܽ��еĲ���
		/// </summary>
		private ArrayList m_OperationArray = new ArrayList();

//		/// <summary>
//		/// �û��ĸ�λ�������ܷ��ʵ�ģ��
//		/// </summary>
//		private ArrayList m_ModuleArray = new ArrayList();

		/// <summary>
		/// ��һ�����ż�����û�
		/// </summary>
		public bool m_IsGroupUser = false;
		/// <summary>
		/// ��һ����˾������û�
		/// </summary>
		public bool m_IsCompanyUser = false;

		/// <summary>
		/// ���û��ܹ����ʵ�������Ŀ
		/// </summary>
		public EntityData m_EntityDataAccessProject = new EntityData("Project") ;

		/// <summary>
		/// �û��ܹ����ʵ����й�˾
		/// </summary>
		public DataTable m_DataTableAccessCompany = new DataTable("Company") ;

        /// <summary>
        /// �û��ܹ����ʵ����в��ţ������Ӳ��ţ� 
        /// </summary>
        public EntityData m_EntityDataAccessUnit = new EntityData("Unit");

        /// <summary>
        /// �Ƿ���windows�����û��Զ���¼ 2007.2.24
        /// </summary>
        public bool IsWindowsAuthenticated = false;

        ///<summary>
		///���캯��
		///</summary>
		public User()
		{
			IniUser();
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="userCode">�û����</param>
		public User( string userCode  )
		{
			m_UserCode = userCode;
			IniUser();
			LoadUserByUserCode();
		}

		private void IniUser()
		{
			this.m_DataTableAccessCompany.Columns.Add("UnitCode");
			this.m_DataTableAccessCompany.Columns.Add("UnitName");
		}

		/// <summary>
		/// �л���Ŀ�����¼����û�Ȩ��
		/// </summary>
		/// <param name="projectCode"></param>
		public void ResetUser ( string projectCode )
		{
			m_CurrentProjectCode = projectCode;
			LoadUserByUserCode();
		}

		///<summary>
		///�û�Ȩ�޼��ϣ���ǰ��Ŀ
		///</summary>
		public Hashtable m_Rights = new Hashtable();

		///<summary>
		///�û��μӵĽ�ɫ���ϣ���ǰ��Ŀ
		///</summary>
		public Hashtable m_Roles = new Hashtable();

		private string CurrentProjectCode
		{
			get{return m_CurrentProjectCode;}
			set{m_CurrentProjectCode=value;}
		}

		/// <summary>
		/// �û�ID
		/// </summary>
		public string UserCode
		{
			get{return m_UserCode;}
			set{m_UserCode=value;}
		}

		/// <summary>
		/// �û���¼��
		/// </summary>
		public string UserID
		{
            get { return m_UserID; }
            set { m_UserID = value; }
		}

        /// <summary>
        /// �û�����
        /// </summary>
        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }
         /// <summary>
        /// �û�����
        /// </summary>
        public string UserShortName
        {
            get { return m_UserShortName; }
            set { m_UserShortName = value; }
        }
        
        public string WorkNO
        {
            get { return m_WorkNO; }
            set { m_WorkNO = value; }
        }


		/// <summary>
		/// �Ƿ����,0 ������1 ����
		/// </summary>
		public int Status 
		{
			get{return m_Status;}
		}


		/// <summary>
		/// �Ƿ�ϵͳ����Ա
		/// </summary>
		public bool IsSystemManager
		{
			get{return m_IsSystemManager;}
		}

        ///<summary>
        ///��֤�û���������
        ///</summary>
        public bool ConfirmUserOwnName(String OwnName)
        {
            try
            {
                EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByCode(m_UserCode);
                bool isOK = false;
                if (entity.HasRecord())
                {
                    if (OwnName == entity.GetString("OwnName"))
                        isOK = true;
                }
                entity.Dispose();
                return isOK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		///<summary>
		///�����û�����,ʹ���û�ID
		///</summary>
		public bool LoadUserByUserCode()
		{
			try
			{
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByCode(m_UserCode);
				bool isOK = true;
				if ( entity.HasRecord())
				{
					m_UserName = entity.GetString("UserName");
                    m_UserShortName = entity.GetString("ShortUserName");
                    m_UserID = entity.GetString("UserID");
					m_Status = entity.GetInt("Status");
					m_WorkNO= entity.GetString("SortID");
					LoadUserRight();
				}
				else
					isOK = false;
				entity.Dispose();
				return isOK;
			}
			catch( Exception ex )
			{
				throw ex;
			}
		}

        ///<summary>
        ///�����û�����,ʹ���û�ID
        ///</summary>
        public bool LoadUserByUserID(string UserID)
        {
            try
            {
                EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByUserID(UserID);
                bool isOK = true;
                if (entity.HasRecord())
                {
                    m_UserCode = entity.GetString("UserCode");
                    m_UserName = entity.GetString("UserName");
                    m_UserID = entity.GetString("UserID");
                    m_Status = entity.GetInt("Status"); ;
                    m_WorkNO = entity.GetString("SortID");
                    LoadUserRight();
                }
                else
                    isOK = false;
                entity.Dispose();
                return isOK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadUserRight()
		{
			try
			{
				EntityData user = DAL.EntityDAO.SystemManageDAO.GetStandard_SystemUserByCode( this.m_UserCode);
				EntityData stations = DAL.EntityDAO.OBSDAO.GetStationByUserCode(this.m_UserCode);
				this.m_DataTableStation = stations.Tables["Station"];
				foreach ( DataRow drStation in stations.CurrentTable.Rows)
				{
					string stationCode = (string) drStation["StationCode"];
					string roleCode = (string) drStation["RoleCode"];
					string unitCode = (string) drStation["UnitCode"];
					int iRoleLevel = (int) drStation["RoleLevel"];
					if ( iRoleLevel <= 1 )
						this.m_IsCompanyUser = true;
					if ( iRoleLevel == 0 )
						this.m_IsGroupUser = true;

					// �ӽ�ɫ���濴����ЩȨ��
					EntityData role = DAL.EntityDAO.SystemManageDAO.GetStandard_RoleByCode(roleCode);
					foreach ( DataRow drOperation in role.Tables["RoleOperation"].Rows)
					{
						string operationCode = (string)drOperation["OperationCode"];
						if ( ! this.m_OperationArray.Contains(operationCode))
							this.m_OperationArray.Add(operationCode);

//						string classCode = operationCode.Substring(0,4);
//						if ( ! this.m_ClassArray.Contains(classCode))
//							this.m_ClassArray.Add( classCode);
//
//						string moduleCode = operationCode.Substring(0,2);
//						if ( ! this.m_ModuleArray.Contains(moduleCode))
//							this.m_ModuleArray.Add(moduleCode);

					}
					role.Dispose();

					EntityData projects = null;

					if ( iRoleLevel == 0 ) //���ż���
					{
						this.m_IsGroupUser = true;

						/******************* �޸�ǰ *************************/
						//projects = DAL.EntityDAO.ProjectDAO.GetAllProject();
						// �޸���clm 20050927
						/******************* �޸ĺ� *************************/
						string companyUnitName = "";
						string companyUnitFullCode = "";
						string projectUnitName = "";
						string projectUnitFullCode = "";
						string companyUnitCode = BLL.SystemRule.GetUnitParentSpecailUnitCode(unitCode,"��˾", ref companyUnitName, ref companyUnitFullCode);
						string projectUnitCode = BLL.SystemRule.GetUnitParentSpecailUnitCode(unitCode,"��Ŀ", ref projectUnitName, ref projectUnitFullCode);
						string targetUnitCode = ( companyUnitFullCode.Length > projectUnitFullCode.Length ) ? companyUnitCode : projectUnitCode;
						string targetUnitName = ( companyUnitFullCode.Length > projectUnitFullCode.Length ) ? companyUnitName : projectUnitName;
						projects = BLL.ProjectRule.GetProjectByUnit( targetUnitCode );
						/****************************************************/

						AddRowToProject(projects);
                        projects.Dispose();

                        EntityData companys = BLL.SystemRule.GetAllCompanyUnit();
                        AddRowToCompany(companys);
                        companys.Dispose();

                        //���ż���ʱ���û��ܷ������в��� 
                        EntityData units = DAL.EntityDAO.OBSDAO.GetAllUnit();
                        AddRowToUnit(units);
                        units.Dispose();
					}
					else if ( iRoleLevel == 3 ) //���ż���
					{
						this.m_IsCompanyUser = true;
						this.m_IsCompanyUser = true;
						string companyUnitName = "";
						string companyUnitFullCode = "";
						string projectUnitName = "";
						string projectUnitFullCode = "";
						string companyUnitCode = BLL.SystemRule.GetUnitParentSpecailUnitCode(unitCode,"��˾", ref companyUnitName, ref companyUnitFullCode);
						string projectUnitCode = BLL.SystemRule.GetUnitParentSpecailUnitCode(unitCode,"��Ŀ", ref projectUnitName, ref projectUnitFullCode);
						string targetUnitCode = ( companyUnitFullCode.Length > projectUnitFullCode.Length ) ? companyUnitCode : projectUnitCode;
						string targetUnitName = ( companyUnitFullCode.Length > projectUnitFullCode.Length ) ? companyUnitName : projectUnitName;

						projects = BLL.ProjectRule.GetProjectByUnit( targetUnitCode );
						AddRowToProject(projects);
                        projects.Dispose();

						if ( companyUnitCode == targetUnitCode )
							AddRowToCompany(companyUnitCode,companyUnitName);

                        //���ż���ʱ���û��ܷ��ʵ�ǰ���ż��������Ӳ��� 
                        if (!this.m_IsGroupUser)
                        {
                            EntityData units = DAL.EntityDAO.OBSDAO.GetUnitAllChildAndSelf(unitCode);
                            AddRowToUnit(units);
                            units.Dispose();
                        }
                    }
					else //����
					{
						/******************************************************
						string projectUnitName = "";
						string projectUnitFullCode = "";
						string projectUnitCode = BLL.SystemRule.GetUnitParentSpecailUnitCode(unitCode,"��Ŀ", ref projectUnitName, ref projectUnitFullCode);
						projects = BLL.ProjectRule.GetProjectByUnit( projectUnitCode );
						AddRowToProject(projects);
						projects.Dispose();
						/******************************************************/
						//clm �޸� ����Ϊ�޸�ǰ����
						projects = BLL.ProjectRule.GetProjectByUnit( unitCode );
						AddRowToProject(projects);
						projects.Dispose();

                        //���ż���ʱ���û��ܷ��ʵ�ǰ���ż��������Ӳ��� 
                        if (!this.m_IsGroupUser)
                        {
                            EntityData units = DAL.EntityDAO.OBSDAO.GetUnitAllChildAndSelf(unitCode);
                            AddRowToUnit(units);
                            units.Dispose();
                        }
                    }
				}
				stations.Dispose();
				user.Dispose();


				// ����ԴȨ�޵ĽǶȿ�ӵ�е�Ȩ��
				string stationCodes = BuildStationCodeString(this.BuildStationCodes());
				string s0 = String.Format ( " ( AccessRange.AccessRangeType=0 and AccessRange.relationCode = '{0}' ) "
					,this.UserCode );
				string s1 = "";
				if ( stationCodes != "" )
				{
					s1= String.Format ( " or ( AccessRange.AccessRangeType=1 and AccessRange.relationCode in ( {0} ) ) "
						, stationCodes);
				}
				string sss = String.Format( "select distinct(operationCode) from accessrange where  (  {0}  {1}  )"
					, new object[]{ s0,s1 } );
				QueryAgent qa = new QueryAgent();
				DataSet ds = qa.ExecSqlForDataSet(sss);
				foreach ( DataRow drOp in ds.Tables[0].Rows )
				{
					if ( ! drOp.IsNull("OperationCode"))
					{
						string operationCode = (string)drOp["OperationCode"];
						if ( ! this.m_OperationArray.Contains(operationCode))
							this.m_OperationArray.Add(operationCode);

//						string classCode = operationCode.Substring(0,4);
//						if ( ! this.m_ClassArray.Contains(classCode))
//							this.m_ClassArray.Add( classCode);
//
//						string moduleCode = operationCode.Substring(0,2);
//						if ( ! this.m_ModuleArray.Contains(moduleCode))
//							this.m_ModuleArray.Add(moduleCode);
					}
				}
				ds.Dispose();
				qa.Dispose();


			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void AddRowToCompany( string companyCode, string companyName )
		{
			if ( this.m_DataTableAccessCompany.Select( String.Format("UnitCode='{0}'",companyCode) ).Length==0)
			{
				DataRow dr = this.m_DataTableAccessCompany.NewRow();
				dr["UnitCode"]=companyCode;
				dr["UnitName"]=companyName;
				this.m_DataTableAccessCompany.Rows.Add(dr);
			}
		}

		private void AddRowToCompany( EntityData units )
		{
			foreach ( DataRow drUnit in units.CurrentTable.Rows)
			{
				string unitCode=drUnit["UnitCode"].ToString();
				string unitName=drUnit["UnitName"].ToString();
				if ( this.m_DataTableAccessCompany.Select( String.Format("UnitCode='{0}'",unitCode) ).Length==0)
				{
					DataRow dr = this.m_DataTableAccessCompany.NewRow();
					dr["UnitCode"]=unitCode;
					dr["UnitName"]=unitName;
					this.m_DataTableAccessCompany.Rows.Add(dr);
				}
			}
		}

		private void AddRowToProject( EntityData projects )
		{
			foreach ( DataRow drProject in projects.CurrentTable.Rows)
			{
				string projectCode=drProject["ProjectCode"].ToString();
				if ( this.m_EntityDataAccessProject.CurrentTable.Select( String.Format("ProjectCode='{0}'",projectCode) ).Length==0)
				{
					this.m_EntityDataAccessProject.CurrentTable.ImportRow(drProject);
				}
			}
		}

        private void AddRowToUnit(EntityData units)
        {
            foreach (DataRow drUnit in units.CurrentTable.Rows)
            {
                string UnitCode = drUnit["UnitCode"].ToString();
                if (this.m_EntityDataAccessUnit.CurrentTable.Select(String.Format("UnitCode='{0}'", UnitCode)).Length == 0)
                {
                    this.m_EntityDataAccessUnit.CurrentTable.ImportRow(drUnit);
                }
            }
        }



//		///<summary>
////		///�û���Դ���ж�
//		///</summary>
//		public bool HasClassRight( string classCode)
//		{
//			if ( this.m_ClassArray.Contains(classCode))
//				return true;
//			else
//				return false;
//		}

//		/// <summary>
//		/// ģ��Ȩ��
//		/// </summary>
//		/// <param name="moduleCode"></param>
//		/// <returns></returns>
//		public bool HasModuleRight( string moduleCode)
//		{
//			if ( this.m_ModuleArray.Contains(moduleCode))
//				return true;
//			else
//				return false;
//		}

		/// <summary>
		/// ����Ȩ��
		/// </summary>
		/// <param name="operationCode"></param>
		/// <returns></returns>
		public bool HasOperationRight( string operationCode)
		{
//			if ( this.m_OperationArray.Contains(operationCode))
//				return true;
//			else
//				return false;
			return HasRight(operationCode);
		}
		public string GetOperationType()
		{
			string temp = "";
			/*if(HasOperationRight("200101"))
				temp += "Y1";
			if(HasOperationRight("200102"))
				temp += "Y2";
			if(HasOperationRight("200103"))
				temp += "Y3";
			if(HasOperationRight("200201"))
				temp += "N1";
			if(HasOperationRight("200202"))
				temp += "N2";*/

			AccessRangeStrategyBuilder sb = new AccessRangeStrategyBuilder();
			ArrayList ar = new ArrayList();
			ar.Add( this.m_UserCode);
			ar.Add( BuildStationCodes() );
			sb.AddStrategy( new Strategy( AccessRangeStrategyName.AccessRelation1,ar));
            sb.AddStrategy(new Strategy(AccessRangeStrategyName.OperationCodeIn, "'Y1','Y2','Y3','N1','N2'"));
			QueryAgent qa = new QueryAgent();
			string sql = sb.BuildMainQueryString();
			EntityData entity = qa.FillEntityData("AccessRange",sql);
			qa.Dispose();

			for(int i=0;i<entity.CurrentTable.Rows.Count;i++)
			{
				temp+=","+entity.CurrentTable.Rows[i]["OperationCode"].ToString();
			}
			return temp;
			
		}

		/// <summary>
		/// �û�Ȩ��
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public bool HasRight ( string code )
		{
//			if ( code.Length == 2 )
//				return HasModuleRight(code);
//			else if ( code.Length== 4 )
//				return HasClassRight(code);
//			else if ( code.Length == 6 )
//				return HasOperationRight(code);
//			else
//				return false;

			if ( ! AvailableFunction.isAvailableFunction(code))
				return false;

			return m_OperationArray.Contains(code);

		}


		/// <summary>
		/// �жϸ��û��Ƿ��ܷ��������Դ, ����Ҳ�������Դ���� true;
		/// </summary>
		/// <param name="code"></param>
		/// <param name="operationCode"></param>
		/// <returns></returns>
		public bool HasResourceRight ( string code,  string operationCode )
		{
			if ( ! AvailableFunction.isAvailableFunction(operationCode))
				return false;

			string classCode = operationCode.Substring( 0,4);
			string resourceCode = BLL.ResourceRule.GetResourceCode( code, classCode);
			if ( resourceCode == "" )
				return true;
			AccessRangeStrategyBuilder sb = new AccessRangeStrategyBuilder();
			sb.AddStrategy( new Strategy( AccessRangeStrategyName.ResourceCode,resourceCode));
			ArrayList ar = new ArrayList();
			ar.Add( this.m_UserCode);
			ar.Add( BuildStationCodes() );
			sb.AddStrategy( new Strategy( AccessRangeStrategyName.AccessRelation1,ar));
			sb.AddStrategy( new Strategy( AccessRangeStrategyName.OperationCode,operationCode));
			QueryAgent qa = new QueryAgent();
			string sql = sb.BuildMainQueryString();
			EntityData entity = qa.FillEntityData("AccessRange",sql);
			qa.Dispose();
			bool canAccess = entity.HasRecord();
			entity.Dispose();
			return canAccess;
		}


		/// <summary>
		/// ��ȡ�û���ĳ����Դ���Ȩ�ޱ�
		/// </summary>
		/// <param name="className"></param>
		/// <returns></returns>
		public ArrayList GetClassRight ( string className )
		{
			try
			{
				string stationCodes = BuildStationCodeString(this.BuildStationCodes());

				object[] ooo = new object[]{   SystemClassDescription.GetItemClassCode(className)
											   , this.UserCode
											   , stationCodes
										   };

				string s0 = String.Format ( " ( AccessRange.AccessRangeType=0 and AccessRange.relationCode = '{1}' ) "
					,ooo );
				string s1 = "";
				if ( stationCodes != "" )
				{
					s1= String.Format ( " or ( AccessRange.AccessRangeType=1 and AccessRange.relationCode in ( {2} ) ) "
						, ooo);
				}
				
				string sss = String.Format( "select distinct(operationCode) from accessrange where substring(operationcode,1,4)='{0}' and  isnull( groupCode ,'') <> '' and  (  {1}  {2}  )"
					, new object[]{ SystemClassDescription.GetItemClassCode(className),  s0,s1 } );
				QueryAgent qa = new QueryAgent();
				DataSet entity = qa.ExecSqlForDataSet( sss );
				qa.Dispose();

				ArrayList ar = new ArrayList();
				foreach ( DataRow dr in entity.Tables[0].Rows )
				{
					if ( ! dr.IsNull("OperationCode"))
					{
						string oCode = (string)dr["OperationCode"];
						if ( !ar.Contains(oCode))
							ar.Add(oCode);
					}
				}
				entity.Dispose();
				return ar;
			}
			catch ( Exception ex )
			{throw ex;}
		}


		/// <summary>
		/// �Ը�������������Ƿ���Ȩ��
		/// </summary>
		/// <param name="operationCode">�������</param>
		/// <param name="typeCode">���ͱ��</param>
		/// <returns></returns>
		public bool HasTypeOperationRight( string operationCode, string typeCode )
		{
			if ( ! AvailableFunction.isAvailableFunction(operationCode))
				return false;

			try
			{
				string inputFullID = RmsPM.BLL.SystemGroupRule.GetSystemGroupFullID(typeCode);
				string stationCodes = BuildStationCodeString(this.BuildStationCodes());
				string s0 = String.Format ( " ( AccessRange.AccessRangeType=0 and AccessRange.relationCode = '{0}' ) "
					,this.UserCode );
				string s1 = "";
				if ( stationCodes != "" )
				{
					s1= String.Format ( " or ( AccessRange.AccessRangeType=1 and AccessRange.relationCode in ( {0} ) ) "
						, stationCodes);
				}
				
				string sss = String.Format( "select  dbo.GetSystemGroupFullID ( groupCode) as GroupFullID  from accessrange where operationcode='{0}' and  isnull( groupCode ,'') <> '' and  (  {1}  {2}  )"
					, new object[]{ operationCode , s0,s1 } );
				QueryAgent qa = new QueryAgent();
				DataSet entity = qa.ExecSqlForDataSet( sss );
				qa.Dispose();

				bool isFounded = false;
				foreach ( DataRow dr in entity.Tables[0].Rows )
				{
					string groupFullID = (string)dr["GroupFullID"];
					if ( inputFullID.IndexOf(groupFullID ) == 0 )
					{
						isFounded = true;
						break;
					}
				}
				entity.Dispose();
				return isFounded;
			}
			catch ( Exception ex )
			{throw ex;}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <param name="className"></param>
		/// <returns></returns>
		public ArrayList GetResourceRight( string code, string className )
		{

			try
			{

				EntityData entity = GetOperationEntityData(code,className) ;

				ArrayList ar = new ArrayList();
				int iCount = entity.CurrentTable.Rows.Count;
				for ( int  i =0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					string operationCode = entity.GetString("OperationCode");
					if ( !ar.Contains(operationCode) && operationCode != "" )
						ar.Add(operationCode);
				}
				entity.Dispose();
				return ar;
			}
			catch ( Exception ex )
			{throw ex;}
		}
  

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <param name="className"></param>
		/// <param name="operationCode"></param>
		/// <returns></returns>
		public bool HasResourceRight ( string code, string className, string operationCode )
		{
			if ( ! AvailableFunction.isAvailableFunction(operationCode))
				return false;

			try
			{
				bool hasRight = false;
				EntityData entity = GetOperationEntityData(code,className) ;
				if ( entity.CurrentTable.Select( String.Format( "OperationCode='{0}'" ,operationCode ) ).Length > 0 )
					hasRight = true;
				return hasRight;
			}
			catch ( Exception ex )
			{throw ex;}
		}

		public EntityData GetOperationEntityData( string code, string className )
		{

			try
			{
				string stationCodes = BuildStationCodeString(this.BuildStationCodes());

				object[] ooo = new object[]{     code
											   , SystemClassDescription.GetItemClassCode(className)
											   , this.UserCode
											   , stationCodes
											   , SystemClassDescription.GetItemTableName(className)
											   , SystemClassDescription.GetItemKeyColumnName(className)
											   , SystemClassDescription.GetItemTypeColumnName(className)
											   , SystemClassDescription.GetItemCreateUserColumnName(className)
										   };

				string selfAccessString = String.Format ( " ( AccessRange.ResourceCode = ( Select ResourceCode from Resource where RelationCode='{0}' and ClassCode='{1}' ) and ( ( AccessRange.AccessRangeType=0 and AccessRange.relationCode = '{2}' )  or ( AccessRange.AccessRangeType=1 and AccessRange.relationCode in ( {3} ) and ( RoleLevel <> 1 or RoleLevel is null ) )   or ( AccessRange.AccessRangeType=1 and AccessRange.relationCode in ( {3} ) and  RoleLevel = 1 and {4}.{7}='{2}'  )  )  ) "
					, ooo ) ;
			
				string typeAccessString = "";
				if ( stationCodes != "" )
					typeAccessString = String.Format ( " or ( substring( dbo.GetSystemGroupFullID({4}.{6}),1, len(dbo.GetSystemGroupFullID(AccessRange.GroupCode)))=dbo.GetSystemGroupFullID(AccessRange.GroupCode) and (  ( AccessRange.AccessRangeType=0 and AccessRange.relationCode = '{2}' and AccessRange.RoleLevel=0 ) or ( AccessRange.AccessRangeType=1 and AccessRange.relationCode in ( {3} ) and ( RoleLevel <> 1 or RoleLevel is null )  ) or ( AccessRange.AccessRangeType=1 and AccessRange.relationCode in ( {3} ) and AccessRange.RoleLevel=1 and {4}.{7}='{2}' ) ) ) "
						,ooo);

				string sss = String.Format( "select accessrange.* from accessrange , {4} where substring (AccessRange.operationCode,1,4 )='{1}' and {4}.{5} = '{0}' and ( " + selfAccessString  + typeAccessString  + " )" 
					,ooo) ;

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("AccessRange" , sss );
				qa.Dispose();
				return entity;
			}
			catch ( Exception ex )
			{throw ex;}
		}

        /// <summary>
        /// ȡ��Ȩ�޵Ĳ����б�����ԴȨ�ޣ�
        /// </summary>
        /// <param name="code"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public EntityData GetOperationEntityDataOfResource(string code, string className)
        {

            try
            {
                string stationCodes = BuildStationCodeString(this.BuildStationCodes());

                object[] ooo = new object[]{     code
											   , SystemClassDescription.GetItemClassCode(className)
											   , this.UserCode
											   , stationCodes
											   , SystemClassDescription.GetItemTableName(className)
											   , SystemClassDescription.GetItemKeyColumnName(className)
											   , SystemClassDescription.GetItemTypeColumnName(className)
											   , SystemClassDescription.GetItemCreateUserColumnName(className)
										   };

                string selfAccessString = String.Format(" ( AccessRange.ResourceCode = ( Select ResourceCode from Resource where RelationCode='{0}' and ClassCode='{1}' ) and ( ( AccessRange.AccessRangeType=0 and AccessRange.relationCode = '{2}' )  or ( AccessRange.AccessRangeType=1 and AccessRange.relationCode in ( {3} ) and ( RoleLevel <> 1 or RoleLevel is null ) )   or ( AccessRange.AccessRangeType=1 and AccessRange.relationCode in ( {3} ) and  RoleLevel = 1 and {4}.{7}='{2}'  )  )  ) "
                    , ooo);

                string sss = String.Format("select accessrange.* from accessrange , {4} where substring (AccessRange.operationCode,1,4 )='{1}' and {4}.{5} = '{0}' and ( " + selfAccessString + " )"
                    , ooo);

                QueryAgent qa = new QueryAgent();
                EntityData entity = qa.FillEntityData("AccessRange", sss);
                qa.Dispose();
                return entity;
            }
            catch (Exception ex)
            { throw ex; }
        }

        public static string BuildStationCodeString(string stationCodes)
		{
			string codes = "";
			foreach ( string code in stationCodes.Split(new char[]{','}))
			{
				if ( codes != "" )
					codes += ",";
				codes += "'" + code + "'";
			}
			return codes;
		}


		/// <summary>
		/// ȡ������Ĳ����б�
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public DataSet GetCBSOperationEntityData( string code )
		{

			try
			{
				string sql = String.Format( "select distinct( accessRange.OperationCode )  from cbs,accessRange where cbs.costCode = '{0}' and substring ( operationCode , 1,2)='04' and substring(FullCode,1,len( dbo.GetCBSFullCode(AccessRange.ResourceCode )))=dbo.GetCBSFullCode(AccessRange.ResourceCode ) and (  (( AccessRangeType=0 and relationCode = '{1}' ) or ( AccessRangeType=1 and relationCode in ( {2} ) ))   )"
					,code,this.UserCode,this.BuildStationCodes());
				QueryAgent qa = new QueryAgent();
				DataSet ds = qa.ExecSqlForDataSet( sql ) ;
				qa.Dispose();
				return ds;
			}
			catch ( Exception ex )
			{throw ex;}
		}


		/// <summary>
		/// ȡ������Ĳ����б�
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public ArrayList GetCBSResourceRight( string code )
		{

			try
			{

				DataSet ds = GetCBSOperationEntityData(code);
				ArrayList ar = new ArrayList();
				int iCount = ds.Tables[0].Rows.Count;
				foreach ( DataRow dr in ds.Tables[0].Rows)
				for ( int  i =0;i<iCount;i++)
				{
					string operationCode = dr["OperationCode"].ToString();
					if ( !ar.Contains(operationCode) && operationCode != "" )
						ar.Add(operationCode);
				}
				ds.Dispose();
				return ar;
			}
			catch ( Exception ex )
			{throw ex;}
		}


		/// <summary>
		/// �жϷ�����Ȩ��
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public bool GetCBSResourceRight( string code , string operationCode )
		{

			if ( ! AvailableFunction.isAvailableFunction(operationCode))
				return false;

			try
			{
				bool canAccess = false;
				DataSet ds = GetCBSOperationEntityData(code);
				if ( ds.Tables[0].Select( String.Format("OperationCode='{0}'" ,operationCode ) ).Length > 0 )
					canAccess = true;
				ds.Dispose();
				return canAccess;
			}
			catch ( Exception ex )
			{throw ex;}
		}




		/// <summary>
		/// ���ɸ�λ�ַ���
		/// </summary>
		/// <returns></returns>
		public string BuildStationCodes ( )
		{
			string codes = "";
			foreach ( DataRow dr in this.m_DataTableStation.Rows)
			{
				string stationCode = (string)dr["StationCode"];
				if ( codes != "" )
					codes += ",";

				codes += stationCode;
			}
			return codes;
		}

        /// <summary>
        /// ȡ��ͬ��Ȩ��
        /// </summary>
        /// <param name="code"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public ArrayList GetContractResourceRight(string code, string className, ArrayList arActor)
        {

            try
            {
                ArrayList ar = GetResourceRight(code, className);

                //���Ϻ�ͬ�����˵�Ȩ��
                if (IsContractActor(code))
                {
//                    ArrayList arActor = (ArrayList)Application["ContractActorOperationList"];
                    foreach (string s in arActor)
                    {
                        if (!ar.Contains(s))
                            ar.Add(s);
                    }
                }

                return ar;
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// �Ƿ��ͬ������
        /// </summary>
        /// <returns></returns>
        public bool IsContractActor(string ContractCode)
        {
            try
            {
                EntityData entity = GetOperationEntityDataOfResource(ContractCode, "Contract");
                bool r = (entity.CurrentTable.Select("OperationCode = '050101'").Length > 0);
                entity.Dispose();

                return r;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


	}
}
