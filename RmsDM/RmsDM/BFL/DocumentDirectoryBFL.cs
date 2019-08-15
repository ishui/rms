namespace RmsDM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Xml;
    using RmsDM.BLL;
    using RmsDM.MODEL;

    public class DocumentDirectoryBFL
    {
        public int Delete(DocumentDirectoryModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new DocumentDirectoryBLL().Delete(ObjModel.Code, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        private void FillChildrenXml(int ParentCode, XmlNode ParentNode)
        {
            List<DocumentDirectoryModel> childrenDocumentDirectory = this.GetChildrenDocumentDirectory(ParentCode);
            foreach (DocumentDirectoryModel model in childrenDocumentDirectory)
            {
                XmlElement parentNode = ParentNode.OwnerDocument.CreateElement("node");
                XmlAttribute node = ParentNode.OwnerDocument.CreateAttribute("Code");
                node.Value = model.Code.ToString();
                parentNode.Attributes.Append(node);
                XmlAttribute attribute2 = ParentNode.OwnerDocument.CreateAttribute("Text");
                attribute2.Value = model.DirectoryName;
                parentNode.Attributes.Append(attribute2);
                XmlAttribute attribute3 = ParentNode.OwnerDocument.CreateAttribute("ParentCode");
                attribute3.Value = model.ParentCode.ToString();
                parentNode.Attributes.Append(attribute3);
                XmlAttribute attribute4 = ParentNode.OwnerDocument.CreateAttribute("Isleaf");
                attribute4.Value = "false";
                parentNode.Attributes.Append(attribute4);
                this.FillChildrenXml(model.Code, parentNode);
                if (!parentNode.HasChildNodes)
                {
                    parentNode.Attributes["Isleaf"].Value = "true";
                }
                ParentNode.AppendChild(parentNode);
            }
        }

        public List<DocumentDirectoryModel> GetChildrenDocumentDirectory(int ParentCode)
        {
            DocumentDirectoryQueryModel queryModel = new DocumentDirectoryQueryModel();
            queryModel.ParentCodeEqual = ParentCode;
            queryModel.SortColumns = "Code";
            return this.GetDocumentDirectoryList(queryModel);
        }

        public DocumentDirectoryModel GetDocumentDirectory(int Code)
        {
            DocumentDirectoryModel model = new DocumentDirectoryModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new DocumentDirectoryBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public XmlDocument GetDocumentDirectoryDataSource(int ParentCode, string RootName)
        {
            XmlDocument document = new XmlDocument();
            XmlElement parentNode = document.CreateElement("root");
            XmlAttribute node = document.CreateAttribute("Code");
            node.Value = ParentCode.ToString();
            parentNode.Attributes.Append(node);
            XmlAttribute attribute2 = document.CreateAttribute("Text");
            attribute2.Value = RootName;
            parentNode.Attributes.Append(attribute2);
            XmlAttribute attribute3 = document.CreateAttribute("Isleaf");
            attribute3.Value = "false";
            parentNode.Attributes.Append(attribute3);
            this.FillChildrenXml(ParentCode, parentNode);
            document.AppendChild(parentNode);
            return document;
        }

        public List<DocumentDirectoryModel> GetDocumentDirectoryList(DocumentDirectoryQueryModel QueryModel)
        {
            List<DocumentDirectoryModel> models = new List<DocumentDirectoryModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new DocumentDirectoryQueryModel();
                    }
                    models = new DocumentDirectoryBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<DocumentDirectoryModel> GetDocumentDirectoryList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string DirectoryNameEqual, string DirectoryNodeCodeEqual, int FileTemplateCodeEqual, int ParentCodeEqual, string FullIDEqual, int DeepEqual, DateTime CreateDateEqual, int OrderByIDEqual)
        {
            List<DocumentDirectoryModel> models = new List<DocumentDirectoryModel>();
            DocumentDirectoryQueryModel objQueryModel = new DocumentDirectoryQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.DirectoryNameEqual = DirectoryNameEqual;
            objQueryModel.DirectoryNodeCodeEqual = DirectoryNodeCodeEqual;
            objQueryModel.FileTemplateCodeEqual = FileTemplateCodeEqual;
            objQueryModel.ParentCodeEqual = ParentCodeEqual;
            objQueryModel.FullIDEqual = FullIDEqual;
            objQueryModel.DeepEqual = DeepEqual;
            objQueryModel.CreateDateEqual = CreateDateEqual;
            objQueryModel.OrderByIDEqual = OrderByIDEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new DocumentDirectoryBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<DocumentDirectoryModel> GetDocumentDirectoryListOne(int Code)
        {
            List<DocumentDirectoryModel> list = new List<DocumentDirectoryModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    DocumentDirectoryBLL ybll = new DocumentDirectoryBLL();
                    list.Add(ybll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public DataSet GetStartupProcedureListDS()
        {
            DataSet startupProcedureDS = new DataSet();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    startupProcedureDS = new DocumentDirectoryBLL().GetStartupProcedureDS(connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return startupProcedureDS;
        }

        public bool HasChild(string code)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(code))
            {
                int num = int.Parse(code);
                DocumentDirectoryBFL ybfl = new DocumentDirectoryBFL();
                DocumentDirectoryQueryModel queryModel = new DocumentDirectoryQueryModel();
                queryModel.ParentCodeEqual = num;
                List<DocumentDirectoryModel> list = new List<DocumentDirectoryModel>();
                if (ybfl.GetDocumentDirectoryList(queryModel).Count > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool HasPigeonhole(string code)
        {
            return true;
        }

        public int Insert(DocumentDirectoryModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new DocumentDirectoryBLL().Insert(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public void Insert(XmlDocument doc, int ParentCode, XmlNodeList CurrentNodeList, string DepartmentCode, string FullID, int Deep, string DirectoryNodeCode)
        {
            DocumentDirectoryModel objModel = new DocumentDirectoryModel();
            foreach (XmlNode node in CurrentNodeList)
            {
                objModel.DirectoryName = node.Attributes["Text"].InnerText;
                objModel.ParentCode = ParentCode;
                objModel.DepartmentCode = DepartmentCode;
                objModel.DirectoryNodeCode = DirectoryNodeCode;
                objModel.CreateDate = DateTime.Now;
                objModel.Deep = Deep + 1;
                if (!node.HasChildNodes)
                {
                    objModel.FileTemplateCode = int.Parse(node.Attributes["Code"].InnerText);
                }
                objModel.FullID = FullID + "/" + ParentCode;
                this.Insert(doc, this.Insert(objModel), node.ChildNodes, DepartmentCode, FullID + "/" + ParentCode, Deep + 1, DirectoryNodeCode);
            }
        }

        public int Update(DocumentDirectoryModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new DocumentDirectoryBLL().Update(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }
    }
}

