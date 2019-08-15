namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class DocumentDAO
    {
        public static void DeleteDocument(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Document"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteDocumentConfig(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentConfig"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteDocumentType(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentType"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeletePic_Store(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("PIC_STORE"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_Document(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Document"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetAllDocument()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Document"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllDocumentConfig()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentConfig"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllDocumentType()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentType"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllDocumentType(int Fixed)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DocumentType");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentType"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("DocumentType", "SelectByFixed").SqlString, "@Fixed", Fixed, entitydata, "DocumentType");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDocumentAllInfoByMainCode(string DocumentTypeCode, string Code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DocumentConfig");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentConfig"))
                {
                    string[] Params = new string[] { "@DocumentTypeCode", "@Code" };
                    object[] values = new object[] { DocumentTypeCode, Code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("DocumentConfig", "SelectDocumentAllInfoByMainCode").SqlString, Params, values, entitydata, "DocumentConfig");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDocumentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Document"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDocumentConfigByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentConfig"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDocumentConfigByDocumentCode(string DocumentCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DocumentConfig");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentConfig"))
                {
                    string[] Params = new string[] { "@DocumentCode" };
                    object[] values = new object[] { DocumentCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("DocumentConfig", "SelectByDocumentCode").SqlString, Params, values, entitydata, "DocumentConfig");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDocumentConfigByDocumentEx(string DocumentCode, string DocumentTypeCode, string Code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DocumentConfig");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentConfig"))
                {
                    string[] Params = new string[] { "@DocumentCode", "@DocumentTypeCode", "@Code" };
                    object[] values = new object[] { DocumentCode, DocumentTypeCode, Code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("DocumentConfig", "SelectByDocumentEx").SqlString, Params, values, entitydata, "DocumentConfig");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDocumentIDByGroupCode(string groupcode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Document");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Document"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Document", "SelectByGroupCode").SqlString, "@GroupCode", groupcode, entitydata, "Document");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDocumentTypeAllChildByParentCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DocumentType");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentType"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("DocumentType", "SelectAllChildByParentCode").SqlString, "@ParentCode", code, entitydata, "DocumentType");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDocumentTypeByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentType"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDocumentTypeChildByParentCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DocumentType");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentType"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("DocumentType", "SelectChildByParentCode").SqlString, "@ParentCode", code, entitydata, "DocumentType");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetPic_StoreByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (StandardEntityDAO ydao = new StandardEntityDAO("PIC_STORE"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_DocumentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Document");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Document"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Document", "Select").SqlString, "@DocumentCode", code, entitydata, "Document");
                    ydao.FillEntity(SqlManager.GetSqlStruct("DocumentConfig", "SelectByDocumentCode").SqlString, "@DocumentCode", code, entitydata, "DocumentConfig");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_DocumentByCode(string code)
        {
            EntityData data2;
            try
            {
                DocumentStrategyBuilder builder = new DocumentStrategyBuilder();
                builder.AddStrategy(new Strategy(DocumentStrategyName.DocumentCode, code));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Document", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void InsertDocument(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Document"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertDocumentConfig(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentConfig"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertDocumentType(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentType"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertPic_Store(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PIC_STORE"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_Document(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Document"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllDocumentConfig(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentConfig"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllStandard_Document(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Document"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void UpdateDocument(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Document"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateDocumentConfig(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentConfig"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateDocumentType(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DocumentType"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePic_Store(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PIC_STORE"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_Document(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Document"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

