namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using RmsPM.DAL.EntityDAO;

    public class ExchangeRateRule
    {
        public static DataTable GetMoneyTypeDataSource()
        {
            return ExchangeRateDAO.GetMoneyTypeDataSource().CurrentTable;
        }
    }
}

