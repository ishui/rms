namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;
    using RmsPM.DAL.EntityDAO;

    public class TY_OA_MgrTaskDtlBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Transaction);
            return ldal.Delete(Code);
        }

        public static TY_OA_MgrTaskDtlModel FindModel(List<TY_OA_MgrTaskDtlModel> mObjs, string MgrDtlCode)
        {
            foreach (TY_OA_MgrTaskDtlModel model in mObjs)
            {
                if (model.MgrDtlCode.ToString() == MgrDtlCode)
                {
                    return model;
                }
            }
            return null;
        }

        public static string GetIsfinishName(string isfinish)
        {
            switch (isfinish)
            {
                case "1":
                    return "已完成";

                case "0":
                    return "未完成";
            }
            return "未完成";
        }

        public TY_OA_MgrTaskDtlModel GetModel(int Code, SqlConnection Connection)
        {
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Connection);
            return ldal.GetModel(Code);
        }

        public TY_OA_MgrTaskDtlModel GetModel(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<TY_OA_MgrTaskDtlModel> GetModels(SqlConnection Connection)
        {
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Connection);
            return ldal.Select();
        }

        public List<TY_OA_MgrTaskDtlModel> GetModels(SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Transaction);
            return ldal.Select();
        }

        public List<TY_OA_MgrTaskDtlModel> GetModels(TY_OA_MgrTaskDtlQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<TY_OA_MgrTaskDtlModel> GetModels(TY_OA_MgrTaskDtlQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(TY_OA_MgrTaskDtlModel ObjModel, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Transaction);
            return ldal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "2";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "3";
            return this.Update(objModel, Transaction);
        }

        public int Update(TY_OA_MgrTaskDtlModel ObjModel, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Transaction);
            return ldal.Update(ObjModel);
        }

        public int UpdateTY_OA_MgrTaskDtlList(SqlTransaction Transaction, List<TY_OA_MgrTaskDtlModel> mObjs, int MgrCodeID)
        {
            try
            {
                TY_OA_MgrTaskDAL kdal;
                TY_OA_MgrTaskModel mObj;
                TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Transaction);
                bool flag = false;
                bool flag2 = false;
                List<TY_OA_MgrTaskDtlModel> list = ldal.GetTY_OA_MgrTaskDtlListByMgrCodeID(MgrCodeID);
                foreach (TY_OA_MgrTaskDtlModel model in list)
                {
                    if (FindModel(mObjs, model.MgrDtlCode.ToString()) == null)
                    {
                        flag2 = true;
                        ldal.Delete(model.MgrDtlCode);
                    }
                }
                foreach (TY_OA_MgrTaskDtlModel model in mObjs)
                {
                    if (model.MgrDtlCode <= 0)
                    {
                        model.MgrDtlCode = int.Parse(SystemManageDAO.GetNewSysCode("MgrDtlCode"));
                        flag = true;
                        model.MgrCodeID = MgrCodeID;
                        ldal.Insert(model);
                    }
                    else
                    {
                        ldal.Update(model);
                    }
                }
                if (flag2)
                {
                    bool flag3 = true;
                    List<TY_OA_MgrTaskDtlModel> list2 = ldal.GetTY_OA_MgrTaskDtlListByMgrCodeID(MgrCodeID);
                    foreach (TY_OA_MgrTaskDtlModel model in list2)
                    {
                        if (model.State != "3")
                        {
                            flag3 = false;
                            break;
                        }
                    }
                    if (flag3)
                    {
                        kdal = new TY_OA_MgrTaskDAL(Transaction);
                        mObj = kdal.GetModel(MgrCodeID);
                        mObj.State = "3";
                        kdal.Update(mObj);
                    }
                }
                if (flag)
                {
                    kdal = new TY_OA_MgrTaskDAL(Transaction);
                    mObj = kdal.GetModel(MgrCodeID);
                    mObj.State = "1";
                    kdal.Update(mObj);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return 1;
        }
    }
}

