namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class MeetRoomBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            MeetRoomDAL mdal = new MeetRoomDAL(Transaction);
            return mdal.Delete(Code);
        }

        public MeetRoomModel GetModel(int Code, SqlConnection Connection)
        {
            MeetRoomDAL mdal = new MeetRoomDAL(Connection);
            return mdal.GetModel(Code);
        }

        public MeetRoomModel GetModel(int Code, SqlTransaction Transaction)
        {
            MeetRoomDAL mdal = new MeetRoomDAL(Transaction);
            return mdal.GetModel(Code);
        }

        public List<MeetRoomModel> GetModels(SqlConnection Connection)
        {
            MeetRoomDAL mdal = new MeetRoomDAL(Connection);
            return mdal.Select();
        }

        public List<MeetRoomModel> GetModels(SqlTransaction Transaction)
        {
            MeetRoomDAL mdal = new MeetRoomDAL(Transaction);
            return mdal.Select();
        }

        public List<MeetRoomModel> GetModels(MeetRoomQueryModel ObjQueryModel, SqlConnection Connection)
        {
            MeetRoomDAL mdal = new MeetRoomDAL(Connection);
            return mdal.Select(ObjQueryModel);
        }

        public List<MeetRoomModel> GetModels(MeetRoomQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            MeetRoomDAL mdal = new MeetRoomDAL(Transaction);
            return mdal.Select(ObjQueryModel);
        }

        public int Insert(MeetRoomModel ObjModel, SqlTransaction Transaction)
        {
            MeetRoomDAL mdal = new MeetRoomDAL(Transaction);
            return mdal.Insert(ObjModel);
        }

        public int Update(MeetRoomModel ObjModel, SqlTransaction Transaction)
        {
            MeetRoomDAL mdal = new MeetRoomDAL(Transaction);
            return mdal.Update(ObjModel);
        }
    }
}

