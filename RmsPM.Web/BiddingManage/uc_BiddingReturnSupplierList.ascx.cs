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
using RmsPM.BLL;
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;
using RmsPM.Web;



public partial class BiddingManage_uc_BiddingReturnSupplierList :System.Web.UI.UserControl
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面

    }



    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。

        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    ///		设计器支持所需的方法 - 不要使用代码编辑器

    ///		修改此方法的内容。

    /// </summary>
    private void InitializeComponent()
    {
      

    }
    #endregion


    #region --- 私有属性 -----------------------------------------------------------------
    /// <summary>
    /// 事务对象
    /// </summary>
    private StandardEntityDAO _dao;
    #endregion -----------------------------------------------------------------

    #region --- 私有方法 -----------------------------------------------------------------

    /// <summary>
    /// DataGrid 事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    public void dgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            if ("Delete" == e.CommandName)
            {
                string strCode = e.Item.Cells[0].Text.Trim();
               

                if ("" == strCode)
                {
                    return;
                }

                RmsPM.BLL.BiddingSupplier cbs = new RmsPM.BLL.BiddingSupplier();
                cbs.BiddingSupplierCode = strCode;
                cbs.BiddingSupplierDelete();

                this.LoadData();
            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }


    #endregion -----------------------------------------------------------------

    #region --- 公共属性 -----------------------------------------------------------------

    /// <summary>
    /// 事务对象
    /// </summary>
    public StandardEntityDAO dao
    {
        get
        {
            return this._dao;
        }
        set
        {
            _dao = value;
        }
    }
    /// <summary>
    /// 获取供应商数量

    /// </summary>
    public int SupplierCode
    {
        get
        {
            return this.dgList.Items.Count;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public string BiddingPrejudicationCode
    {
        get
        {
            if (null != this.ViewState["BiddingPrejudicationCode"])
            {
                return this.ViewState["BiddingPrejudicationCode"].ToString();
            }
            return "";
        }
        set { this.ViewState["BiddingPrejudicationCode"] = value; }
    }

    /// <summary>
    /// 显示议价次数，默认全部显示 ，即3次全不显示

    /// </summary>
    public int DiscussNumber
    {
        get
        {
            if (null != this.ViewState["DiscussNumber"])
            {
                return System.Convert.ToInt32(this.ViewState["DiscussNumber"]);
            }
            return 3;
        }
        set { this.ViewState["DiscussNumber"] = value; }
    }


    /// <summary>
    /// 
    /// </summary>
    public string BiddingCode
    {
        get
        {
            if (null != this.ViewState["BiddingCode"])
            {
                return this.ViewState["BiddingCode"].ToString();
            }
            return "";
        }
        set { this.ViewState["BiddingCode"] = value; }
    }

    /// <summary>
    /// 是否显示选择列

    /// </summary>
    public bool CanSelect
    {
        get
        {
            if (null != this.ViewState["CanSelect"])
            {
                return (bool)this.ViewState["CanSelect"];
            }
            return false;
        }
        set { this.ViewState["CanSelect"] = value; }
    }

    /// <summary>
    /// 是否可以编辑
    /// </summary>
    public bool CanModify
    {
        get
        {
            if (null != this.ViewState["CanModify"])
            {
                return (bool)this.ViewState["CanModify"];
            }
            return false;
        }
        set { this.ViewState["CanModify"] = value; }
    }

   
    /// <summary>
    /// 
    /// </summary>
    private ModuleState _State = ModuleState.Unbeknown;
    /// <summary>
    /// 是否显示所有的选项信息
    /// </summary>
    public ModuleState State2
    {
        get
        {
            if (_State == ModuleState.Unbeknown)
            {
                if (this.ViewState["_DepartState"] != null)
                    return (ModuleState)this.ViewState["_DepartState"];
                return ModuleState.Unbeknown;
            }
            return _State;
        }
        set
        {
            _State = value;
            this.ViewState["_DepartState"] = value;
        }
    }


    #endregion -----------------------------------------------------------------

    #region --- 公共方法 -----------------------------------------------------------------

    /// <summary>
    /// 控件初始化

    /// </summary>
    public void IniControl()
    {
        try
        {
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }


    /// <summary>
    /// 装载控件数据
    /// </summary>
    public void LoadData()
    {
        try
        {
          
            
            DataTable dt = RmsPM.BLL.BiddingSupplier.GetBiddingSupplierByPrejudicationCode(this.BiddingPrejudicationCode);
            this.GetResourseData(dt);
            this.dgList.DataSource = dt;
            this.dgList.DataBind();
            dgListState();
            
        }
        catch (Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }

    /// <summary>
    /// 为表格中字段加控制

    /// </summary>
    public void dgListState()
    {
        //this.dgList.Columns[3].Visible = this.CanSelect;
        //this.dgList.Columns[4].Visible = !this.CanSelect;


        if (!this.CanModify)
        {
            foreach (DataGridItem dg in dgList.Items)
            {
                //将允许出现的列定义为不可用

                for (int i = 1; i <= DiscussNumber; i++)
                {
                    string id = "TxtReturn" + System.Convert.ToString(i);
                    System.Web.UI.HtmlControls.HtmlInputText txtreturn = dg.FindControl(id) as System.Web.UI.HtmlControls.HtmlInputText;
                    txtreturn.Disabled = true;
                }



                dg.Cells[8].Visible = false;
            }
        }

        //隐藏不允许出现的列

        foreach (DataGridColumn dg in dgList.Columns)
        {
            for (int i = 3; i > DiscussNumber; i--)
            {
                this.dgList.Columns[4 + i].Visible = false;
            }
        }

      


    }

    /// <summary>
    /// 将BiddingSupplier数据结构的数据集增加第一次议价，第二次议价和第三次议价

    /// </summary>
    /// <param name="dt"></param>
    public void GetResourseData( DataTable dt)
    {


        dt.Columns.Add("BiddingReturnMondey1", System.Type.GetType("System.String"));//第一次议价

        dt.Columns.Add("BiddingReturnMondey2", System.Type.GetType("System.String"));//第二次议价

        dt.Columns.Add("BiddingReturnMondey3", System.Type.GetType("System.String"));//第三次议价


        string company = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower();

        switch (company)
        {
            case "zhudingpm":
                this.dgList.Columns[4].HeaderText = "提名人";
                break;
            default:
                this.dgList.Columns[4].HeaderText = "厂商报价";

                break;
        }

       
        
        BiddingEmit cbiddingEmit = new BiddingEmit();
        cbiddingEmit.BiddingCode = this.BiddingCode;
        DataTable dtBiddingEmit=cbiddingEmit.GetBiddingEmits();

        BiddingReturn cbiddingReturn = new BiddingReturn();
        int i = 0;
      
        if (dtBiddingEmit != null)
        {
            foreach (DataRow drBiddingEmit in dtBiddingEmit.Select())
            {
                int j = 0;
                cbiddingReturn.BiddingEmitCode = drBiddingEmit["BiddingEmitCode"].ToString();
                DataTable dtBiddingReturn=cbiddingReturn.GetBiddingReturns();
                int columnIndex = dt.Columns.IndexOf("BiddingReturnMondey1") + i;
                if (dtBiddingReturn != null)
                {
                    foreach (DataRow drBiddingReturn in dtBiddingReturn.Select())
                    {
                        dt.Rows[j][columnIndex] = drBiddingReturn["Remark"].ToString();

                        j++;

                    }
                }
                i++;
            }
        }
    }


    /// <summary>
    /// 保存数据(状态修改)
    /// </summary>
    public void SaveData()
    {
        try
        {
            //删除所有发标回标信息

            BiddingEmit cbiddingEmit = new BiddingEmit();
            cbiddingEmit.BiddingCode = this.BiddingCode;
            cbiddingEmit.dao = this.dao;
            BiddingReturn cbiddingReturn = new BiddingReturn();
            cbiddingReturn.dao = this.dao;

            
            EntityData entityEmit = cbiddingEmit.GetBiddingEmitEntitys();



            if (entityEmit != null)
            {
                foreach (DataRow drBiddingEmit in entityEmit.Tables["BiddingEmit"].Select())
                {
            
                    cbiddingReturn.BiddingEmitCode=drBiddingEmit["BiddingEmitCode"].ToString();
                    EntityData entityReturn = cbiddingReturn.GetBiddingReturnEntitys();
                    if (entityReturn != null)
                    {
                        foreach (DataRow drBiddingReturn in entityReturn.Tables["BiddingReturn"].Select())
                        {
                            drBiddingReturn.Delete();
                        }
                        cbiddingReturn.SubmitAllBiddingReturn(entityReturn);
                    }
                    entityReturn.Clear();
                  
                    drBiddingEmit.Delete(); 
                }
                cbiddingEmit.SubmitAllBiddingEmit(entityEmit);
                entityEmit.Clear();
               
            }


            //添加发标回标信息
            for (int i = 0; i < DiscussNumber; i++)
            {
                BiddingEmit biddingEmitTemp = new BiddingEmit();
                biddingEmitTemp.dao = this.dao;
                biddingEmitTemp.BiddingEmitCode = "";
                biddingEmitTemp.BiddingCode = this.BiddingCode;
                biddingEmitTemp.CreatDate = System.DateTime.Now.ToString();
                biddingEmitTemp.BiddingEmitAdd();
                foreach (DataGridItem dg in dgList.Items)
                {
                    BiddingReturn biddingReturnTemp = new BiddingReturn();
                    biddingReturnTemp.dao = this.dao;
                    biddingReturnTemp.BiddingReturnCode = "";
                    biddingReturnTemp.BiddingEmitCode = biddingEmitTemp.BiddingEmitCode;
                    biddingReturnTemp.SupplierCode=dg.Cells[1].Text.ToString();
                    string id="TxtReturn"+System.Convert.ToString(i+1);
                    biddingReturnTemp.Remark = ((System.Web.UI.HtmlControls.HtmlInputText)dg.FindControl(id)).Value.ToString();
                    biddingReturnTemp.BiddingDtlCode = "";
                    biddingReturnTemp.State = "1";
                    biddingReturnTemp.Flag = "1";
                    biddingReturnTemp.BiddingReturnAdd();
                }
            }

            //if (this.CanSelect)
            //{
            //    string[] chkCodes = { };
            //    ArrayList chkAL = new ArrayList();
            //    string strchkSelect = Request.Form["chkSelect"] + "";

            //    if ("" != strchkSelect)
            //    {
            //        chkCodes = Request.Form.GetValues("chkSelect");
            //        int chkCount = chkCodes.Length;
            //        for (int i = 0; i < chkCount; i++)
            //        {
            //            chkAL.Add(chkCodes[i]);
            //        }
            //    }

            //    BiddingSupplier cbs = new BiddingSupplier();
            //    cbs.BiddingPrejudicationCode = this.BiddingPrejudicationCode;
            //    cbs.dao = dao;
            //    DataTable myDT = cbs.GetBiddingSuppliers();
            //    int dtCount = myDT.Rows.Count;
            //    for (int r = 0; r < dtCount; r++)
            //    {
            //        string strCode = myDT.Rows[r]["BiddingSupplierCode"].ToString();

            //        cbs.BiddingSupplierCode = strCode;
            //        if (chkAL.Contains(strCode))
            //        {
            //            cbs.Flag = "1";
            //        }
            //        else
            //        {
            //            cbs.Flag = "0";
            //        }

            //        cbs.BiddingSupplierUpdate();
            //    }
            //    myDT.Dispose();
            //}

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }

   
    
   


   
    #endregion -----------------------------------------------------------------



}