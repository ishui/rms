namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_EquipmentMaintainApplyBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyDAL ydal = new GK_OA_EquipmentMaintainApplyDAL(Transaction);
            return ydal.Delete(Code);
        }

        public GK_OA_EquipmentMaintainApplyModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_EquipmentMaintainApplyDAL ydal = new GK_OA_EquipmentMaintainApplyDAL(Connection);
            return ydal.GetModel(Code);
        }

        public GK_OA_EquipmentMaintainApplyModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyDAL ydal = new GK_OA_EquipmentMaintainApplyDAL(Transaction);
            return ydal.GetModel(Code);
        }

        public List<GK_OA_EquipmentMaintainApplyModel> GetModels(SqlConnection Connection)
        {
            GK_OA_EquipmentMaintainApplyDAL ydal = new GK_OA_EquipmentMaintainApplyDAL(Connection);
            return ydal.Select();
        }

        public List<GK_OA_EquipmentMaintainApplyModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyDAL ydal = new GK_OA_EquipmentMaintainApplyDAL(Transaction);
            return ydal.Select();
        }

        public List<GK_OA_EquipmentMaintainApplyModel> GetModels(GK_OA_EquipmentMaintainApplyQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_EquipmentMaintainApplyDAL ydal = new GK_OA_EquipmentMaintainApplyDAL(Connection);
            return ydal.Select(ObjQueryModel);
        }

        public List<GK_OA_EquipmentMaintainApplyModel> GetModels(GK_OA_EquipmentMaintainApplyQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyDAL ydal = new GK_OA_EquipmentMaintainApplyDAL(Transaction);
            return ydal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_EquipmentMaintainApplyModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyDAL ydal = new GK_OA_EquipmentMaintainApplyDAL(Transaction);
            return ydal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_EquipmentMaintainApplyModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_EquipmentMaintainApplyDAL ydal = new GK_OA_EquipmentMaintainApplyDAL(Transaction);
            return ydal.Update(ObjModel);
        }
    }
}

