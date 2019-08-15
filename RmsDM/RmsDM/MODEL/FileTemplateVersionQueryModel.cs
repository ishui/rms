namespace RmsDM.MODEL
{
    using System;
    using System.Data;

    public class FileTemplateVersionQueryModel : QueryBaseModel
    {
        public int CodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Code=@Code ");
                    base.InsertParameter("@Code", SqlDbType.Int, 4, value);
                }
            }
        }

        public int FileTemplateCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" FileTemplateCode=@FileTemplateCode ");
                    base.InsertParameter("@FileTemplateCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public string IsAvailabilityEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" IsAvailability=@IsAvailability ");
                    base.InsertParameter("@IsAvailability", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string IsPigeonholeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" IsPigeonhole=@IsPigeonhole ");
                    base.InsertParameter("@IsPigeonhole", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string MarkingSNRuleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MarkingSNRule=@MarkingSNRule ");
                    base.InsertParameter("@MarkingSNRule", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string PigeonholeTimeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PigeonholeTime=@PigeonholeTime ");
                    base.InsertParameter("@PigeonholeTime", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string RecordKindEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" RecordKind=@RecordKind ");
                    base.InsertParameter("@RecordKind", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string SaveTermEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SaveTerm=@SaveTerm ");
                    base.InsertParameter("@SaveTerm", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string VersionNumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" VersionNumber=@VersionNumber ");
                    base.InsertParameter("@VersionNumber", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string WorkFlowProcedureNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" WorkFlowProcedureName=@WorkFlowProcedureName ");
                    base.InsertParameter("@WorkFlowProcedureName", SqlDbType.VarChar, 500, value);
                }
            }
        }
    }
}

