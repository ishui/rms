namespace RmsPM.BLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using Rms.LogHelper;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public sealed class DocumentRule
    {
        private byte[] _content;
        private Stream _contentStream;
        private string _contentType;
        private string _fileName;
        private int _length;
        private static string _Path;
        private static AttachmentSaveMode _SaveMode;
        public static AttachmentSavePathMode _SavePathMode;
        private static bool inited = false;
        public static readonly string tmpTypeHead = "---";

        private DocumentRule()
        {
        }

        public bool AddOrUpdateAttachment(string code, string userCode, string fileName, string contentType, int length, string strAttachMentType, string strMasterCode, Stream fileStream)
        {
            EntityData entity = DAOFactory.GetAttachmentDAO().GetAttachMentByCode(code);
            DataRow newRecord = null;
            if (!entity.HasRecord())
            {
                newRecord = entity.GetNewRecord();
                newRecord["AttachMentCode"] = SystemManageDAO.GetNewSysCode("AttachMentCode");
                newRecord["AttachMentType"] = strAttachMentType;
                newRecord["MasterCode"] = strMasterCode;
                entity.AddNewRecord(newRecord);
            }
            else
            {
                newRecord = entity.CurrentRow;
            }
            Stream stream = fileStream;
            byte[] buffer = new byte[length];
            int num = stream.Read(buffer, 0, length);
            newRecord["FileName"] = fileName;
            newRecord["CreatePerson"] = userCode;
            newRecord["CreateDate"] = DateTime.Now;
            newRecord["Content_Type"] = contentType;
            newRecord["Length"] = length;
            if (_SaveMode == AttachmentSaveMode.database)
            {
                newRecord["Content"] = buffer;
                newRecord["GuidName"] = null;
            }
            else
            {
                string guidname = Guid.NewGuid().ToString();
                newRecord["GuidName"] = guidname;
                newRecord["Content"] = DBNull.Value;
                SaveFile(length, buffer, guidname, newRecord["CreateDate"].ToString());
            }
            DAOFactory.GetAttachmentDAO().SubmitAllAttachMent(entity);
            entity.Dispose();
            return true;
        }

        public void CheckDocument(string DocumentCode, string User)
        {
            try
            {
                EntityData entity = DocumentDAO.GetDocumentByCode(DocumentCode);
                if (entity.HasRecord())
                {
                    DataRow currentRow = entity.CurrentRow;
                    currentRow["Status"] = 1;
                    currentRow["CheckDate"] = DateTime.Now;
                    currentRow["CheckPerson"] = User;
                    DocumentDAO.UpdateDocument(entity);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int ConvertAttachmentToFile(int i)
        {
            EntityData entitydata = new EntityData();
            using (SingleEntityDAO ydao = new SingleEntityDAO("AttachMent"))
            {
                ydao.FillEntity("select top " + i.ToString() + " * from attachment where content is not  null", "", "", entitydata, "AttachMent");
            }
            DataTable currentTable = entitydata.CurrentTable;
            int num = 0;
            Rms.LogHelper.LogHelper.Warn("附件转换 子阶段开始 ");
            try
            {
                foreach (DataRow row in currentTable.Rows)
                {
                    if (row["content"] != DBNull.Value)
                    {
                        string guidname = Guid.NewGuid().ToString();
                        SaveFile(((byte[]) row["content"]).Length, (byte[]) row["content"], guidname, row["createdate"].ToString());
                        row["guidname"] = guidname;
                        Log.WriteLog("Guid", row["guidname"].ToString());
                        row["content"] = DBNull.Value;
                        num++;
                    }
                }
                DAOFactory.GetAttachmentDAO().SubmitAllAttachMent(entitydata);
            }
            catch (Exception exception)
            {
                Log.WriteLog("DocumentRule", exception.ToString());
                entitydata.Dispose();
                throw exception;
            }
            Rms.LogHelper.LogHelper.Warn("附件转换 子阶段结束 ");
            entitydata.Dispose();
            return num;
        }

        public int ConvertFileToAttachment(int i, ref int beginCode)
        {
            EntityData entitydata = new EntityData();
            using (SingleEntityDAO ydao = new SingleEntityDAO("AttachMent"))
            {
                ydao.FillEntity("select top " + i.ToString() + " * from attachment where content is  null and attachmentcode>" + ((int) beginCode).ToString() + " order by attachmentcode", "", "", entitydata, "AttachMent");
            }
            DataTable currentTable = entitydata.CurrentTable;
            int num = 0;
            Rms.LogHelper.LogHelper.Warn("附件转换 子阶段开始 code>" + ((int) beginCode).ToString());
            try
            {
                if (currentTable.Rows.Count == 0)
                {
                    return 0;
                }
                foreach (DataRow row in currentTable.Rows)
                {
                    if (beginCode < int.Parse(row["attachmentcode"].ToString()))
                    {
                        beginCode = int.Parse(row["attachmentcode"].ToString());
                    }
                    if (row["content"] == DBNull.Value)
                    {
                        string fileName = GetFileName(row["guidname"].ToString(), row["CreateDate"].ToString());
                        FileInfo info = new FileInfo(fileName);
                        if (info.Exists)
                        {
                            byte[] buffer = new byte[info.Length];
                            info.OpenRead().Read(buffer, 0, (int) info.Length);
                            row["content"] = buffer;
                            row["guidname"] = DBNull.Value;
                        }
                        else
                        {
                            Rms.LogHelper.LogHelper.Warn("附件转换 文件没找到:" + fileName);
                        }
                        num++;
                    }
                }
                DAOFactory.GetAttachmentDAO().SubmitAllAttachMent(entitydata);
            }
            catch (Exception exception)
            {
                Log.WriteLog("DocumentRule", exception.ToString());
                entitydata.Dispose();
                throw exception;
            }
            Rms.LogHelper.LogHelper.Warn("附件转换 子阶段结束 code>" + ((int) beginCode).ToString());
            entitydata.Dispose();
            return num;
        }

        public bool CopyAttachment(string oldMastCode, string newMasterCode, string AttachmentType)
        {
            EntityData entity = DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(AttachmentType, oldMastCode);
            if (!entity.HasRecord())
            {
                return false;
            }
            DataRow drSrc = entity.CurrentRow;
            DataRow row = entity.GetNewRecord();
            entity.CurrentTable.Rows.Add(row);
            ConvertRule.DataRowCopy(drSrc, row, entity.CurrentTable, entity.CurrentTable);
            row["AttachmentCode"] = SystemManageDAO.GetNewSysCode("AttachMentCode");
            row["MasterCode"] = newMasterCode;
            DAOFactory.GetAttachmentDAO().SubmitAllAttachMent(entity);
            entity.Dispose();
            return true;
        }

        public bool DeleteAttachment(string code)
        {
            EntityData entity = DAOFactory.GetAttachmentDAO().GetAttachMentByCode(code);
            if (entity.HasRecord())
            {
                if (entity.CurrentRow["guidname"] != DBNull.Value)
                {
                    try
                    {
                        DeleteFile(entity.CurrentRow["guidname"].ToString(), entity.CurrentRow["createdate"].ToString());
                    }
                    catch (Exception exception)
                    {
                        Rms.LogHelper.LogHelper.Error("删除附件文件时异常", exception);
                    }
                }
                DAOFactory.GetAttachmentDAO().DeleteAttachMent(entity);
            }
            entity.Dispose();
            return true;
        }

        public void DeleteDocument(string DocumentCode)
        {
            try
            {
                EntityData entity = DocumentDAO.GetStandard_DocumentByCode(DocumentCode);
                if (entity.HasRecord())
                {
                    if (entity.CurrentRow["GuidName"] != DBNull.Value)
                    {
                    }
                    DocumentDAO.DeleteStandard_Document(entity);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void DeleteDocumentConfig(string DocumentCode, string DocumentTypeCode, string Code)
        {
            try
            {
                EntityData entity = DocumentDAO.GetDocumentConfigByDocumentEx(DocumentCode, DocumentTypeCode, Code);
                DocumentDAO.DeleteDocumentConfig(entity);
                entity.Dispose();
                EntityData data2 = DocumentDAO.GetStandard_DocumentByCode(DocumentCode);
                data2.SetCurrentTable("DocumentConfig");
                if (data2.CurrentTable.Select("Fixed=1").Length == 0)
                {
                    DocumentDAO.DeleteStandard_Document(data2);
                }
                data2.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void DeleteDocumentType(string DocumentTypeCode)
        {
            try
            {
                if (DocumentTypeCode != "")
                {
                    DocumentDAO.DeleteDocumentType(DocumentDAO.GetDocumentTypeAllChildByParentCode(DocumentTypeCode));
                    EntityData entity = DocumentDAO.GetDocumentTypeByCode(DocumentTypeCode);
                    DocumentDAO.DeleteDocumentType(entity);
                    entity.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void DeleteFile(string guidname, string date)
        {
            FileInfo info = new FileInfo(GetFileName(guidname, date));
            Rms.LogHelper.LogHelper.Debug("DeleteFile:" + GetFileName(guidname, date));
            if ((info != null) && info.Exists)
            {
                info.Delete();
            }
        }

        public string GetAttachListHtml(string type, string MasterCode)
        {
            string text4;
            try
            {
                string text = "";
                if (MasterCode == "")
                {
                    return text;
                }
                EntityData attachMentByMasterCode = DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(type, MasterCode);
                int count = attachMentByMasterCode.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    attachMentByMasterCode.SetCurrentRow(i);
                    string text2 = attachMentByMasterCode.GetString("FileName");
                    string text3 = "../Project/WBSAttachMentView.aspx?AttachMentCode=" + attachMentByMasterCode.GetString("AttachMentCode") + "&Action=View";
                    if (text != "")
                    {
                        text = text + " ";
                    }
                    text = text + "<a target='_blank' href='" + text3 + "'>" + text2 + "</a>";
                }
                attachMentByMasterCode.Dispose();
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public string GetAttachmentByCode(string code)
        {
            EntityData attachMentByCode = DAOFactory.GetAttachmentDAO().GetAttachMentByCode(code);
            if (attachMentByCode.HasRecord())
            {
                DataRow currentRow = attachMentByCode.CurrentRow;
                if ((currentRow["Content_Type"].ToString() == "") || (currentRow["Content_Type"] == null))
                {
                    return "未定义附件的显示方式";
                }
                this._contentType = currentRow["Content_Type"].ToString();
                this._fileName = currentRow["filename"].ToString();
                this._length = (int) currentRow["length"];
                if ((_SaveMode == AttachmentSaveMode.database) || (currentRow["Content"] != DBNull.Value))
                {
                    if ((currentRow["Content"].ToString() == "") || (currentRow["Content"] == null))
                    {
                        return "该附件无内容";
                    }
                    byte[] buffer = (byte[]) currentRow["content"];
                    this._content = new byte[buffer.Length];
                    for (int i = 0; i < this._content.Length; i++)
                    {
                        this._content[i] = buffer[i];
                    }
                }
                else if (_SaveMode == AttachmentSaveMode.file)
                {
                    try
                    {
                        FileStream stream = new FileStream(GetFileName(currentRow["guidname"].ToString(), currentRow["CreateDate"].ToString()), FileMode.Open);
                        this._content = new byte[stream.Length];
                        stream.Read(this._content, 0, (int) stream.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                    catch (FileNotFoundException)
                    {
                        this._content = new byte[0];
                        return "未能找到或打开附件";
                    }
                }
            }
            attachMentByCode.Dispose();
            return "";
        }

        public EntityData GetDocumentIDByGroupCode(string groupcode)
        {
            EntityData data2;
            try
            {
                EntityData documentIDByGroupCode = DocumentDAO.GetDocumentIDByGroupCode(groupcode);
                documentIDByGroupCode.Dispose();
                data2 = documentIDByGroupCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public string GetDocumentTypeName(string code)
        {
            string text2;
            string text = "";
            try
            {
                if (code == "")
                {
                    return text;
                }
                EntityData documentTypeByCode = DocumentDAO.GetDocumentTypeByCode(code);
                if (documentTypeByCode.CurrentTable.Rows.Count > 0)
                {
                    text = documentTypeByCode.CurrentRow["TypeName"].ToString();
                }
                documentTypeByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static string GetFileName(string filename, string date)
        {
            return Path.Combine(GetPath(date), filename);
        }

        private static string GetPath(string date)
        {
            switch (_SavePathMode)
            {
                case AttachmentSavePathMode.ROOT:
                    return _Path;

                case AttachmentSavePathMode.YYYY:
                    return Path.Combine(_Path, GetYearString(date));

                case AttachmentSavePathMode.YYYYMM:
                    return Path.Combine(_Path, GetYearMonthString(date));

                case AttachmentSavePathMode.YYYYMMDD:
                    return Path.Combine(_Path, GetYearMonthDateString(date));
            }
            return _Path;
        }

        private static string GetYearMonthDateString(string date)
        {
            DateTime result = new DateTime();
            if ((date != string.Empty) && DateTime.TryParse(date, out result))
            {
                return (result.Year.ToString() + result.Month.ToString() + result.Day.ToString().PadLeft(2, '0'));
            }
            return string.Empty;
        }

        private static string GetYearMonthString(string date)
        {
            DateTime result = new DateTime();
            if ((date != string.Empty) && DateTime.TryParse(date, out result))
            {
                return (result.Year.ToString() + result.Month.ToString().PadLeft(2, '0'));
            }
            return string.Empty;
        }

        private static string GetYearString(string date)
        {
            DateTime result = new DateTime();
            if ((date != string.Empty) && DateTime.TryParse(date, out result))
            {
                return result.Year.ToString();
            }
            return string.Empty;
        }

        public static DocumentRule Instance()
        {
            if (!inited)
            {
                inited = true;
                string text = ConfigurationSettings.AppSettings["AttachMentSaveMode"];
                if ((text != null) && (text.ToLower() == "file"))
                {
                    _SaveMode = AttachmentSaveMode.file;
                    _Path = ConfigurationSettings.AppSettings["AttachMentSavePath"];
                    if (_Path == null)
                    {
                        throw new Exception("未定义文档存储目录");
                    }
                    try
                    {
                        if (!Directory.Exists(_Path))
                        {
                            Directory.CreateDirectory(_Path);
                        }
                    }
                    catch
                    {
                        throw new Exception("不能创建文档存储目录");
                    }
                }
                else
                {
                    _SaveMode = AttachmentSaveMode.database;
                }
                string text2 = ConfigurationSettings.AppSettings["AttachMentSavePathMode"];
                try
                {
                    _SavePathMode = (AttachmentSavePathMode) Enum.Parse(typeof(AttachmentSavePathMode), text2.ToUpper());
                }
                catch
                {
                    _SavePathMode = AttachmentSavePathMode.ROOT;
                }
            }
            return new DocumentRule();
        }

        private static void SaveFile(int length, byte[] imgData, string guidname, string date)
        {
            string path = GetPath(date);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileStream stream = new FileStream(Path.Combine(path, guidname), FileMode.Create);
            stream.Write(imgData, 0, length);
            stream.Flush();
            stream.Close();
            stream.Dispose();
        }

        public int UpdateMasterCodeAttachmentType(string newMasterCode, string tmpMasterCode, string correctAttachmentType)
        {
            int num = 0;
            EntityData entity = DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(tmpTypeHead + correctAttachmentType, tmpMasterCode);
            if (entity.HasRecord())
            {
                DataTable currentTable = entity.CurrentTable;
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    currentTable.Rows[i]["MasterCode"] = newMasterCode;
                    currentTable.Rows[i]["AttachmentType"] = correctAttachmentType;
                    num++;
                }
                DAOFactory.GetAttachmentDAO().UpdateAttachMent(entity);
            }
            entity.Dispose();
            return num;
        }

        public byte[] Content
        {
            get
            {
                return this._content;
            }
        }

        public string ContentType
        {
            get
            {
                return this._contentType;
            }
        }

        public string FileName
        {
            get
            {
                return this._fileName;
            }
        }

        public int Length
        {
            get
            {
                return this._length;
            }
        }
    }
}

