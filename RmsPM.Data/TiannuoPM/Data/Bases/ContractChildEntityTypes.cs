namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ContractChildEntityTypes
    {
        [ChildEntityType(typeof(TList<ContractAccount>))]
        ContractAccountCollection = 6,
        [ChildEntityType(typeof(TList<ContractBill>))]
        ContractBillCollection = 5,
        [ChildEntityType(typeof(TList<ContractChange>))]
        ContractChangeCollection = 3,
        [ChildEntityType(typeof(TList<ContractCost>))]
        ContractCostCollection = 1,
        [ChildEntityType(typeof(TList<ContractCostPlan>))]
        ContractCostPlanCollection = 4,
        [ChildEntityType(typeof(TList<ContractMaterial>))]
        ContractMaterialCollection = 7,
        [ChildEntityType(typeof(TList<Payment>))]
        PaymentCollection = 2,
        [ChildEntityType(typeof(TiannuoPM.Entities.Project))]
        Project = 0
    }
}

