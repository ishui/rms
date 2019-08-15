using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RmsPM.Web.DesignDocumentManage
{

    /// <summary>
    ///		DesignDocumentList 的摘要说明。

    /// </summary>
    public partial class DesignDocumentManage_DesignDocumentList : System.Web.UI.UserControl
    {

        #region --- 私有成员集合 ----------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        private string _DesignDocumentCode = null;
        /// <summary>
        /// 
        /// </summary>
        private string _Title = null;
        /// <summary>
        /// 
        /// </summary>
        private string _ProjectCode = null;
        /// <summary>
        /// 
        /// </summary>
        private string _UnitCode = null;
        /// <summary>
        /// 
        /// </summary>
        private string _Context = null;
        /// <summary>
        /// 
        /// </summary>
        private string _CreateDate = null;
        /// <summary>
        /// 
        /// </summary>
        private string _CreateUser = null;
        /// <summary>
        /// 
        /// </summary>
        private string _State = null;
        /// <summary>
        /// 
        /// </summary>
        private string _Flag = null;
        private string _Type = null;

        #endregion -------------------------------------------------------------------------------------

        #region --- 属性集合 ----------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        public string DesignDocumentCode
        {
            get { return _DesignDocumentCode; }
            set { _DesignDocumentCode = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectCode
        {
            get { return _ProjectCode; }
            set { _ProjectCode = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string PageTitle
        {
            get { return this.ViewState["_PageTitle"] + ""; }
            set { this.ViewState["_PageTitle"] = value; }
        }
        #endregion -------------------------------------------------------------------------------------

        /// ****************************************************************************
        /// <summary>
        /// 数据加载
        /// </summary>
        /// ****************************************************************************
        private void LoadData()
        {
            try
            {
                BLL.DesignDocument cDesignDocument = new BLL.DesignDocument();

                if (_DesignDocumentCode != null)
                    cDesignDocument.DesignDocumentCode = _DesignDocumentCode;
                if (_Title != null)
                    cDesignDocument.Title = _Title;
                if (_ProjectCode != null)
                    cDesignDocument.ProjectCode = _ProjectCode;
                if (_UnitCode != null)
                    cDesignDocument.UnitCode = _UnitCode;
                if (_Context != null)
                    cDesignDocument.Context = _Context;
                if (_CreateDate != null)
                    cDesignDocument.CreateDate = _CreateDate;
                if (_CreateUser != null)
                    cDesignDocument.CreateUser = _CreateUser;
                if (_State != null)
                    cDesignDocument.State = _State;
                if (_Flag != null)
                    cDesignDocument.Flag = _Flag;
                if (_Type != null)
                    cDesignDocument.Type = _Type;

                DataTable dt = cDesignDocument.GetDesignDocuments();
                this.dgList.DataSource = dt;
                this.dgList.DataBind();
                this.gpControl.RowsCount = dt.Rows.Count.ToString();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        private void gpControl_PageIndexChange(object sender, System.EventArgs e)
        {
            LoadData();
        }
        /// ****************************************************************************
        /// <summary>
        /// 数据帮定显示
        /// </summary>
        /// ****************************************************************************
        public void DataBound()
        {
            LoadData();
        }
    }
}

