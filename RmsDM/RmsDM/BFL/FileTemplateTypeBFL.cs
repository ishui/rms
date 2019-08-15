namespace RmsDM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;
    using RmsDM.BLL;
    using RmsDM.MODEL;

    public class FileTemplateTypeBFL
    {
        public string ChangeCodeArrayToString(int[] array)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != 0)
                {
                    builder.Append(array[i] + ",");
                }
                else
                {
                    break;
                }
            }
            if (builder.Length > 1)
            {
                return builder.Remove(builder.Length - 1, 1).ToString();
            }
            return "";
        }

        public string ChangeNameArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrEmpty(array[i]))
                {
                    builder.Append(array[i] + ",");
                }
                else
                {
                    break;
                }
            }
            return builder.Remove(builder.Length - 1, 1).ToString();
        }

        public int Delete(FileTemplateTypeModel ObjModel)
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
                        num = new FileTemplateTypeBLL().Delete(ObjModel.Code, transaction);
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

        public void DeleteNodeAndTemplate(FileTemplateTypeModel objModel)
        {
            FileTemplateBFL ebfl = new FileTemplateBFL();
            FileTemplateQueryModel queryModel = new FileTemplateQueryModel();
            queryModel.FileTemplateTypeCodeEqual = objModel.Code;
            List<FileTemplateModel> fileTemplateList = new List<FileTemplateModel>();
            fileTemplateList = ebfl.GetFileTemplateList(queryModel);
            if (fileTemplateList != null)
            {
                foreach (FileTemplateModel model2 in fileTemplateList)
                {
                    this.DeleteVersionByTemplate(model2);
                    ebfl.Delete(model2);
                }
            }
            this.Delete(objModel);
        }

        public void DeleteVersionByTemplate(FileTemplateModel model)
        {
            FileTemplateVersionBFL nbfl = new FileTemplateVersionBFL();
            FileTemplateVersionQueryModel queryModel = new FileTemplateVersionQueryModel();
            queryModel.FileTemplateCodeEqual = model.Code;
            List<FileTemplateVersionModel> fileTemplateVersionList = new List<FileTemplateVersionModel>();
            fileTemplateVersionList = nbfl.GetFileTemplateVersionList(queryModel);
            if (fileTemplateVersionList != null)
            {
                foreach (FileTemplateVersionModel model3 in fileTemplateVersionList)
                {
                    nbfl.Delete(model3);
                }
            }
        }

        private void FillChildrenXml(int ParentCode, XmlNode ParentNode)
        {
            List<FileTemplateTypeModel> childrenFileTemplateType = this.GetChildrenFileTemplateType(ParentCode);
            foreach (FileTemplateTypeModel model in childrenFileTemplateType)
            {
                XmlElement parentNode = ParentNode.OwnerDocument.CreateElement("node");
                XmlAttribute node = ParentNode.OwnerDocument.CreateAttribute("Code");
                node.Value = model.Code.ToString();
                parentNode.Attributes.Append(node);
                XmlAttribute attribute2 = ParentNode.OwnerDocument.CreateAttribute("Text");
                attribute2.Value = model.FileTemplateTypeName;
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

        private void FillChildrenXmlPart(string CodeStr, int ParentCode, XmlNode ParentNode)
        {
            List<FileTemplateTypeModel> childrenFileTemplateTypePart = this.GetChildrenFileTemplateTypePart(CodeStr, ParentCode);
            foreach (FileTemplateTypeModel model in childrenFileTemplateTypePart)
            {
                XmlElement parentNode = ParentNode.OwnerDocument.CreateElement("node");
                XmlAttribute node = ParentNode.OwnerDocument.CreateAttribute("Code");
                node.Value = model.Code.ToString();
                parentNode.Attributes.Append(node);
                XmlAttribute attribute2 = ParentNode.OwnerDocument.CreateAttribute("Text");
                attribute2.Value = model.FileTemplateTypeName;
                parentNode.Attributes.Append(attribute2);
                XmlAttribute attribute3 = ParentNode.OwnerDocument.CreateAttribute("ParentCode");
                attribute3.Value = model.ParentCode.ToString();
                parentNode.Attributes.Append(attribute3);
                XmlAttribute attribute4 = ParentNode.OwnerDocument.CreateAttribute("Isleaf");
                attribute4.Value = "false";
                parentNode.Attributes.Append(attribute4);
                this.FillChildrenXmlPart(CodeStr, model.Code, parentNode);
                this.TemplateChildrenXml(model.Code, parentNode);
                if (!parentNode.HasChildNodes)
                {
                    parentNode.Attributes["Isleaf"].Value = "true";
                }
                ParentNode.AppendChild(parentNode);
            }
        }

        public List<FileTemplateTypeModel> GetChildrenFileTemplateType(int ParentCode)
        {
            FileTemplateTypeQueryModel queryModel = new FileTemplateTypeQueryModel();
            queryModel.ParentCodeEqual = ParentCode;
            queryModel.SortColumns = "Code";
            return this.GetFileTemplateTypeList(queryModel);
        }

        public List<FileTemplateTypeModel> GetChildrenFileTemplateTypePart(string CodeStr, int ParentCode)
        {
            FileTemplateTypeQueryModel queryModel = new FileTemplateTypeQueryModel();
            queryModel.CodeEqual = int.Parse(CodeStr);
            queryModel.ParentCodeEqual = ParentCode;
            queryModel.SortColumns = "Code";
            return this.GetFileTemplateTypeList(queryModel);
        }

        public int GetCodeByIndex(string str, int index)
        {
            return int.Parse(str.Split(new char[] { ',' })[index]);
        }

        public FileTemplateTypeModel GetFileTemplateType(int Code)
        {
            FileTemplateTypeModel model = new FileTemplateTypeModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new FileTemplateTypeBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public XmlDocument GetFileTemplateTypeDataSource(int ParentCode, string RootName)
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

        public XmlDocument GetFileTemplateTypeDataSourcePart(string CodeStr, int ParentCode, string RootName)
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
            this.FillChildrenXmlPart(CodeStr, ParentCode, parentNode);
            document.AppendChild(parentNode);
            return document;
        }

        public List<FileTemplateTypeModel> GetFileTemplateTypeList(FileTemplateTypeQueryModel QueryModel)
        {
            List<FileTemplateTypeModel> models = new List<FileTemplateTypeModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new FileTemplateTypeQueryModel();
                    }
                    models = new FileTemplateTypeBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<FileTemplateTypeModel> GetFileTemplateTypeList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string FileTemplateTypeNameEqual, int ParentCodeEqual, string FullIDEqual, int DeepEqual, int OrderByIDEqual)
        {
            List<FileTemplateTypeModel> models = new List<FileTemplateTypeModel>();
            FileTemplateTypeQueryModel objQueryModel = new FileTemplateTypeQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.FileTemplateTypeNameEqual = FileTemplateTypeNameEqual;
            objQueryModel.ParentCodeEqual = ParentCodeEqual;
            objQueryModel.FullIDEqual = FullIDEqual;
            objQueryModel.DeepEqual = DeepEqual;
            objQueryModel.OrderByIDEqual = OrderByIDEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new FileTemplateTypeBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<FileTemplateTypeModel> GetFileTemplateTypeListOne(int Code)
        {
            List<FileTemplateTypeModel> list = new List<FileTemplateTypeModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    FileTemplateTypeBLL ebll = new FileTemplateTypeBLL();
                    list.Add(ebll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public string[] GetNodeBrother(int parentCode, int code, out bool hasMore)
        {
            int index;
            string[] textArray = new string[2];
            string[] array = new string[30];
            int[] numArray = new int[30];
            int num = 0;
            FileTemplateTypeQueryModel queryModel = new FileTemplateTypeQueryModel();
            queryModel.ParentCodeEqual = parentCode;
            List<FileTemplateTypeModel> fileTemplateTypeList = new List<FileTemplateTypeModel>();
            fileTemplateTypeList = this.GetFileTemplateTypeList(queryModel);
            if (fileTemplateTypeList.Count == 1)
            {
                hasMore = false;
                textArray[0] = "";
                textArray[1] = "";
                return textArray;
            }
            hasMore = true;
            for (index = 0; index < fileTemplateTypeList.Count; index++)
            {
                numArray[index] = fileTemplateTypeList[index].Code;
                array[index] = fileTemplateTypeList[index].FileTemplateTypeName;
            }
            for (index = 0; index < numArray.Length; index++)
            {
                if (numArray[index] == code)
                {
                    num = index;
                    break;
                }
            }
            this.Swap<int>(ref numArray[0], ref numArray[num]);
            this.Swap<string>(ref array[0], ref array[num]);
            textArray[0] = this.ChangeCodeArrayToString(numArray);
            textArray[1] = this.ChangeNameArrayToString(array);
            return textArray;
        }

        public string GetParentPath(int code, out int parentCodeOut, out int typeCode)
        {
            int parentCode = 0;
            FileTemplateBFL ebfl = new FileTemplateBFL();
            FileTemplateModel fileTemplate = new FileTemplateModel();
            FileTemplateTypeModel fileTemplateType = new FileTemplateTypeModel();
            fileTemplate = ebfl.GetFileTemplate(code);
            typeCode = fileTemplate.FileTemplateTypeCode;
            fileTemplateType = this.GetFileTemplateType(typeCode);
            string fileTemplateTypeName = fileTemplateType.FileTemplateTypeName;
            parentCodeOut = fileTemplateType.ParentCode;
            for (parentCode = fileTemplateType.ParentCode; parentCode != 0; parentCode = fileTemplateType.ParentCode)
            {
                fileTemplateType = new FileTemplateTypeModel();
                fileTemplateType = this.GetFileTemplateType(parentCode);
                fileTemplateTypeName = fileTemplateType.FileTemplateTypeName + " -> " + fileTemplateTypeName;
            }
            return fileTemplateTypeName;
        }

        public List<FileTemplateTypeModel> GetRecursionFileTemplateTypeChildNodeByNodeCode(int NodeCode)
        {
            FileTemplateTypeQueryModel queryModel = new FileTemplateTypeQueryModel();
            queryModel.ParentCodeEqual = NodeCode;
            List<FileTemplateTypeModel> fileTemplateTypeList = this.GetFileTemplateTypeList(queryModel);
            List<FileTemplateTypeModel> list2 = new List<FileTemplateTypeModel>();
            foreach (FileTemplateTypeModel model2 in fileTemplateTypeList)
            {
                FileTemplateTypeModel item = new FileTemplateTypeModel();
                item.Code = model2.Code;
                list2.Add(item);
                List<FileTemplateTypeModel> recursionFileTemplateTypeChildNodeByNodeCode = this.GetRecursionFileTemplateTypeChildNodeByNodeCode(model2.Code);
                foreach (FileTemplateTypeModel model4 in recursionFileTemplateTypeChildNodeByNodeCode)
                {
                    FileTemplateTypeModel model5 = new FileTemplateTypeModel();
                    model5.Code = model4.Code;
                    list2.Add(model5);
                }
            }
            return list2;
        }

        public bool HasChild(int code)
        {
            bool flag = false;
            List<FileTemplateTypeModel> list = new List<FileTemplateTypeModel>();
            if (this.GetChildrenFileTemplateType(code).Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        public int Insert(FileTemplateTypeModel ObjModel)
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
                        num = new FileTemplateTypeBLL().Insert(ObjModel, transaction);
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

        public string[] SpiltStringToArray(string str)
        {
            return str.Split(new char[] { ',' });
        }

        public void Swap<T>(ref T t1, ref T t2)
        {
            T local = t1;
            t1 = t2;
            t2 = local;
        }

        public void TemplateChildrenXml(int TemplateTypeCode, XmlNode ParentNode)
        {
            FileTemplateBFL ebfl = new FileTemplateBFL();
            FileTemplateQueryModel queryModel = new FileTemplateQueryModel();
            queryModel.FileTemplateTypeCodeEqual = TemplateTypeCode;
            List<FileTemplateModel> fileTemplateList = ebfl.GetFileTemplateList(queryModel);
            if (fileTemplateList.Count > 0)
            {
                foreach (FileTemplateModel model2 in fileTemplateList)
                {
                    XmlElement newChild = ParentNode.OwnerDocument.CreateElement("node");
                    XmlAttribute node = ParentNode.OwnerDocument.CreateAttribute("Code");
                    node.Value = model2.Code.ToString();
                    newChild.Attributes.Append(node);
                    XmlAttribute attribute2 = ParentNode.OwnerDocument.CreateAttribute("Text");
                    attribute2.Value = model2.FileTemplateName;
                    newChild.Attributes.Append(attribute2);
                    XmlAttribute attribute3 = ParentNode.OwnerDocument.CreateAttribute("ParentCode");
                    attribute3.Value = TemplateTypeCode.ToString();
                    newChild.Attributes.Append(attribute3);
                    XmlAttribute attribute4 = ParentNode.OwnerDocument.CreateAttribute("Isleaf");
                    attribute4.Value = "false";
                    newChild.Attributes.Append(attribute4);
                    if (!newChild.HasChildNodes)
                    {
                        newChild.Attributes["Isleaf"].Value = "true";
                    }
                    ParentNode.AppendChild(newChild);
                }
            }
        }

        public int Update(FileTemplateTypeModel ObjModel)
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
                        num = new FileTemplateTypeBLL().Update(ObjModel, transaction);
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

