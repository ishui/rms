namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class ConferenceManageBLL
    {
        public static string BuildQueryString(string topic, string startTime, string endTime, string dept, string place, string chaterMember)
        {
            return ConferenceManageDAL.BuildQueryString(topic, startTime, endTime, dept, place, chaterMember);
        }

        public int Delete(int Code, SqlTransaction Transaction)
        {
            ConferenceManageDAL edal = new ConferenceManageDAL(Transaction);
            return edal.Delete(Code);
        }

        public ConferenceManageModel GetModel(int Code, SqlConnection Connection)
        {
            ConferenceManageDAL edal = new ConferenceManageDAL(Connection);
            return edal.GetModel(Code);
        }

        public ConferenceManageModel GetModel(int Code, SqlTransaction Transaction)
        {
            ConferenceManageDAL edal = new ConferenceManageDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<ConferenceManageModel> GetModels(SqlConnection Connection)
        {
            ConferenceManageDAL edal = new ConferenceManageDAL(Connection);
            return edal.Select();
        }

        public List<ConferenceManageModel> GetModels(SqlTransaction Transaction)
        {
            ConferenceManageDAL edal = new ConferenceManageDAL(Transaction);
            return edal.Select();
        }

        public List<ConferenceManageModel> GetModels(ConferenceManageQueryModel ObjQueryModel, SqlConnection Connection)
        {
            ConferenceManageDAL edal = new ConferenceManageDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<ConferenceManageModel> GetModels(ConferenceManageQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            ConferenceManageDAL edal = new ConferenceManageDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(ConferenceManageModel ObjModel, SqlTransaction Transaction)
        {
            ConferenceManageDAL edal = new ConferenceManageDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int Update(ConferenceManageModel ObjModel, SqlTransaction Transaction)
        {
            ConferenceManageDAL edal = new ConferenceManageDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

