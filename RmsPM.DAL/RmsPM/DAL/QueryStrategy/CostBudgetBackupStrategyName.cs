namespace RmsPM.DAL.QueryStrategy
{
    using System;

    public enum CostBudgetBackupStrategyName
    {
        False,
        CostBudgetBackupCode,
        ProjectCode,
        CostBudgetBackupName,
        CostBudgetBackupNameLike,
        BackupPerson,
        BackupDateRange,
        OnlyBackup
    }
}

