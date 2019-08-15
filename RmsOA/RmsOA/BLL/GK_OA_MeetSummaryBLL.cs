namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_MeetSummaryBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryDAL ydal = new GK_OA_MeetSummaryDAL(Transaction);
            return ydal.Delete(Code);
        }

        public GK_OA_MeetSummaryModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_MeetSummaryDAL ydal = new GK_OA_MeetSummaryDAL(Connection);
            return ydal.GetModel(Code);
        }

        public GK_OA_MeetSummaryModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryDAL ydal = new GK_OA_MeetSummaryDAL(Transaction);
            return ydal.GetModel(Code);
        }

        public List<GK_OA_MeetSummaryModel> GetModels(SqlConnection Connection)
        {
            GK_OA_MeetSummaryDAL ydal = new GK_OA_MeetSummaryDAL(Connection);
            return ydal.Select();
        }

        public List<GK_OA_MeetSummaryModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryDAL ydal = new GK_OA_MeetSummaryDAL(Transaction);
            return ydal.Select();
        }

        public List<GK_OA_MeetSummaryModel> GetModels(GK_OA_MeetSummaryQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_MeetSummaryDAL ydal = new GK_OA_MeetSummaryDAL(Connection);
            return ydal.Select(ObjQueryModel);
        }

        public List<GK_OA_MeetSummaryModel> GetModels(GK_OA_MeetSummaryQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryDAL ydal = new GK_OA_MeetSummaryDAL(Transaction);
            return ydal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_MeetSummaryModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryDAL ydal = new GK_OA_MeetSummaryDAL(Transaction);
            return ydal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_MeetSummaryModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_MeetSummaryDAL ydal = new GK_OA_MeetSummaryDAL(Transaction);
            return ydal.Update(ObjModel);
        }
    }
}

