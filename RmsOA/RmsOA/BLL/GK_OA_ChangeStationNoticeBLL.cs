namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_ChangeStationNoticeBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeDAL edal = new GK_OA_ChangeStationNoticeDAL(Transaction);
            return edal.Delete(Code);
        }

        public GK_OA_ChangeStationNoticeModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_ChangeStationNoticeDAL edal = new GK_OA_ChangeStationNoticeDAL(Connection);
            return edal.GetModel(Code);
        }

        public GK_OA_ChangeStationNoticeModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeDAL edal = new GK_OA_ChangeStationNoticeDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<GK_OA_ChangeStationNoticeModel> GetModels(SqlConnection Connection)
        {
            GK_OA_ChangeStationNoticeDAL edal = new GK_OA_ChangeStationNoticeDAL(Connection);
            return edal.Select();
        }

        public List<GK_OA_ChangeStationNoticeModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeDAL edal = new GK_OA_ChangeStationNoticeDAL(Transaction);
            return edal.Select();
        }

        public List<GK_OA_ChangeStationNoticeModel> GetModels(GK_OA_ChangeStationNoticeQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_ChangeStationNoticeDAL edal = new GK_OA_ChangeStationNoticeDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<GK_OA_ChangeStationNoticeModel> GetModels(GK_OA_ChangeStationNoticeQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeDAL edal = new GK_OA_ChangeStationNoticeDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_ChangeStationNoticeModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeDAL edal = new GK_OA_ChangeStationNoticeDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_ChangeStationNoticeModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_ChangeStationNoticeDAL edal = new GK_OA_ChangeStationNoticeDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

