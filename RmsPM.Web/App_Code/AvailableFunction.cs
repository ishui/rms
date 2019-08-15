using System;
using System.Collections;
using System.Data;

using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web
{
	/// <summary>
	/// 使用xml定义文件建立树节点
	/// </summary>
	public sealed class AvailableFunction
	{

		private static ArrayList m_AvailableFunction = new ArrayList();

		//构造函数
		private AvailableFunction( )
		{
		}

		public static void LoadAvailableFunction()
		{
			try
			{
				FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsAvailable,"0" )  );

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("FunctionStructure",sb.BuildMainQueryString());
				qa.Dispose();

				foreach ( DataRow dr in entity.CurrentTable.Rows)
				{
					string code = (string)dr["FunctionStructureCode"];
					m_AvailableFunction.Add(code);
				}
				entity.Dispose();
			}
			catch (Exception ex )
			{
				ApplicationLog.WriteLog("AvailableFunction",ex,"");
            }

		}


		public static bool isAvailableFunction( string functionCode )
		{
			return m_AvailableFunction.Contains(functionCode);
		}

	}

}
