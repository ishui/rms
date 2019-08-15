namespace RmsPM.BLL
{
    using System;

    public class SalCostClass
    {
        public string m_BuildingName = "";
        public decimal m_CostPrice = 0M;
        public string m_ProjectCode = "";
        public decimal m_TotalArea = 0M;
        public decimal m_TotalCost = 0M;

        public SalCostClass(string ProjectCode, string BuildingName)
        {
            this.m_ProjectCode = ProjectCode;
            this.m_BuildingName = BuildingName;
            this.m_TotalCost = SalRule.GetSalTotalCostByProjectBuilding(this.m_ProjectCode, this.m_BuildingName);
            this.m_TotalArea = SalRule.GetSalTotalAreaByProjectBuilding(this.m_ProjectCode, this.m_BuildingName);
            if (this.m_TotalArea == 0M)
            {
                this.m_CostPrice = 0M;
            }
            else
            {
                this.m_CostPrice = Math.Round((decimal) (this.m_TotalCost / this.m_TotalArea), 2);
            }
        }
    }
}

