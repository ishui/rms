

namespace RmsPM.Web.BiddingManage
{
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
    using System.Data;

    /// <summary>
    /// 投标单位预审
    /// </summary>

    public partial class BiddingSupplierGrade : RmsPM.Web.BiddingManage.BiddingControlBase
    {
        private string _BiddingPrejudicationCode = "";
        private string _BiddingGradeTypeCode = "";

        private string[] BiddingSupplierCode;

        public DataTable dtBiddingSupplier;


        /// <summary>
        /// 
        /// </summary>
        public string BiddingPrejudicationCode
        {
            get
            {
                if (_BiddingPrejudicationCode == "")
                {
                    if (this.ViewState["_BiddingPrejudicationCode"] != null)
                        return this.ViewState["_BiddingPrejudicationCode"].ToString();
                    return "";
                }
                return _BiddingPrejudicationCode;
            }
            set
            {
                _BiddingPrejudicationCode = value;
                this.ViewState["_BiddingPrejudicationCode"] = value;
            }
        }

        /// <summary>
        /// 评分类型 用来区别其是投标单位预审，中标单位评审等等的评分
        /// </summary>
        public string BiddingGradeTypeCode
        {
            get
            {
                if (_BiddingGradeTypeCode == "")
                {
                    if (this.ViewState["_BiddingGradeTypeCode"] != null)
                        return this.ViewState["_BiddingGradeTypeCode"].ToString();
                    return "";
                }
                return _BiddingGradeTypeCode;
            }
            set
            {
                _BiddingGradeTypeCode = value;
                this.ViewState["_BiddingGradeTypeCode"] = value;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
         
        }



        /// <summary>
        /// 组件初始化

        /// </summary>
        override public void InitControl()
        {
            if (this.State == ModuleState.Sightless)//不可见的
            {
                this.Visible = false;
            }
            else if (this.State == ModuleState.Operable)//可操作的
            {

                LoadData(true);
                this.EyeableDiv.Visible = false;
                this.OperableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Eyeable)//可见的

            {

                LoadData(false);
                this.EyeableDiv.Visible = true;
                this.OperableDiv.Visible = false;
            }
            else if (this.State == ModuleState.Begin)//可见的

            {

                LoadData(false);
                this.EyeableDiv.Visible = true;
                this.OperableDiv.Visible = false;
            }
            else if (this.State == ModuleState.End)//可见的

            {

                LoadData(false);
                this.EyeableDiv.Visible = true;
                this.OperableDiv.Visible = false;
            }
            else
            {
                this.Visible = false;
            }
        }

        /// <summary>
        /// 投标单位预审 评分审核 组织树型主表
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="returndt">返回树型数据集</param>
        /// <param name="CodeName">数据源主键名</param>
        /// <param name="ParentCodeName">数据源父级名</param>
        /// <param name="ParentCode">数据源父级值</param>
        /// <param name="Code">开发人员使用（值必须为空字符串）</param>
        /// <param name="LeftStr">开发人员使用（值必须为空字符串）</param>
        /// <param name="Deep">深度（顶级为 1）</param>
        /// <returns>子集节点数量</returns>
        public void GetSHTreeDataSource(DataTable dtConsiderDiathesis, DataTable dtBiddingSupplier, DataTable returndt, string GradeMessageCode, string CodeName, string ParentCodeName, string ParentCode, string Code, string LeftStr, int Deep, decimal PercentageValue, string ConsiderDiathesisCode)
        {
            if (Code == "")
            {
                returndt.Columns.Add("code", System.Type.GetType("System.String"));

                returndt.Columns.Add("freeflag", System.Type.GetType("System.String"));
                returndt.Columns.Add("issubtotal", System.Type.GetType("System.String"));//判断是否是小计


                returndt.Columns.Add("ColumnCount", System.Type.GetType("System.Int32"));
                returndt.Clear();


                dtConsiderDiathesis.Columns.Add("code", System.Type.GetType("System.String"));

                dtConsiderDiathesis.Columns.Add("freeflag", System.Type.GetType("System.String"));
                dtConsiderDiathesis.Columns.Add("issubtotal", System.Type.GetType("System.String"));//判断是否是小计

                dtConsiderDiathesis.Columns.Add("ColumnCount", System.Type.GetType("System.Int32"));

                for (int i = 0; i < dtBiddingSupplier.Rows.Count; i++)
                {

                    returndt.Columns.Add("Point" + (i + 1), System.Type.GetType("System.String"));
                    returndt.Columns.Add("Code" + (i + 1), System.Type.GetType("System.String"));
                    returndt.Columns.Add("GradeMessageCode" + (i + 1), System.Type.GetType("System.String"));
                    //赋标题

                    returndt.Columns["Point" + (i + 1)].Caption = dtBiddingSupplier.Rows[i]["SupplierName"].ToString();

                    dtConsiderDiathesis.Columns.Add("Point" + (i + 1), System.Type.GetType("System.String"));
                    dtConsiderDiathesis.Columns.Add("Code" + (i + 1), System.Type.GetType("System.String"));
                    dtConsiderDiathesis.Columns.Add("GradeMessageCode" + (i + 1), System.Type.GetType("System.String"));
                    //赋标题

                    dtConsiderDiathesis.Columns["Point" + (i + 1)].Caption = dtBiddingSupplier.Rows[i]["SupplierName"].ToString();
                }
            }

            DataRow[] drw = dtConsiderDiathesis.Select(ParentCodeName + "='" + ParentCode.ToString() + "' and BiddingGradeTypeCode='100001'");

            //招投标评分主表

            DataTable dtBiddingGradeMessage = RmsPM.BLL.BiddingGradeMessage.GetAllBiddingGradeMessage().CurrentTable;
            //招投标分数表

            string strfilter = "";
            //取的所有供应商CODE的规则

            for (int tempi = 0; tempi < dtBiddingSupplier.Rows.Count; tempi++)
            {
                if (tempi != dtBiddingSupplier.Rows.Count - 1)
                {
                    strfilter = strfilter + "'" + dtBiddingSupplier.Rows[tempi]["BiddingSupplierCode"] + "',";
                }
                else
                {
                    strfilter = strfilter + "'" + dtBiddingSupplier.Rows[tempi]["BiddingSupplierCode"] + "'";
                }
            }

            //取得所有GradeMessageCode 的规则

            int GradeMessageLenth = 0;
            string GradeMessageFilter = "";
            DataTable dtGrade = new DataTable();
            if (strfilter != "")
            {
                foreach (DataRow tempGradedr in dtBiddingGradeMessage.Select("ApplicationCode in (" + strfilter + ") and BiddingGradeTypeCode='100001'"))
                {
                    if (GradeMessageLenth != dtBiddingGradeMessage.Select("ApplicationCode in (" + strfilter + ") and BiddingGradeTypeCode='100001'").Length - 1)
                    {
                        GradeMessageFilter = GradeMessageFilter + "'" + tempGradedr["BiddingGradeMessageCode"] + "',";
                    }
                    else
                    {
                        GradeMessageFilter = GradeMessageFilter + "'" + tempGradedr["BiddingGradeMessageCode"] + "'";
                    }
                    GradeMessageLenth++;

                }

                RmsPM.BLL.BiddingGrade cbiddingGrade = new BiddingGrade();
                cbiddingGrade.BiddingGradeMessageCode = GradeMessageFilter;
                dtGrade = cbiddingGrade.GetBiddings();
            }
            int tempCode = 1;

            foreach (DataRow dr in drw)
            {

                if (tempCode == 1)
                {
                    dr["freeflag"] = "1";

                }
                else
                {
                    dr["freeflag"] = "0";
                }

                dr["ColumnCount"] = dtBiddingSupplier.Rows.Count;
                dr["code"] = Code + ((tempCode.ToString().Length < 2) ? "0" + tempCode.ToString() : tempCode.ToString());

                dr["Percentage"] = System.Convert.ToDecimal(dr["Percentage"]) * 100;
                dr["issubtotal"] = "0";
                DataRow rdr = returndt.NewRow();
                rdr.ItemArray = dr.ItemArray;
                returndt.Rows.Add(rdr);


                //给评分项初始化

                for (int j = 0; j < dtBiddingSupplier.Rows.Count; j++)
                {
                    rdr["Point" + (j + 1)] = 0;
                    rdr["Code" + (j + 1)] = "";
                    rdr["GradeMessageCode" + (j + 1)] = "";
                }


                //给评分项赋值

                int GradeMessageindex = 0;
                if (strfilter != "")
                {
                    foreach (DataRow tempdr in dtBiddingGradeMessage.Select("ApplicationCode in (" + strfilter + ") and BiddingGradeTypeCode='100001'"))
                    {
                        GradeMessageindex++;
                        rdr["GradeMessageCode" + GradeMessageindex] = tempdr["BiddingGradeMessageCode"];
                        foreach (DataRow tempGradedr1 in dtGrade.Select("BiddingGradeMessageCode='" + tempdr["BiddingGradeMessageCode"] + "' and BiddingConsiderDiathesisCode='" + dr["BiddingConsiderDiathesisCode"] + "'"))
                        {
                            rdr["Code" + GradeMessageindex] = tempGradedr1["BiddingGradeCode"];
                            rdr["Point" + GradeMessageindex] = tempGradedr1["GradePoint"];
                        }

                    }
                }

            }

            //增加小计行

            DataRow rdrSubtotal = returndt.NewRow();
            rdrSubtotal["BiddingConsiderDiathesisCode"] = "";

            rdrSubtotal["BiddingConsiderDiathesis"] = "总计";
            rdrSubtotal["GradeGuideline"] = "";
            rdrSubtotal["freeflag"] = "0";

            rdrSubtotal["ColumnCount"] = dtBiddingSupplier.Rows.Count;
            rdrSubtotal["code"] = Code + ((tempCode.ToString().Length < 2) ? "0" + tempCode.ToString() : tempCode.ToString());

            rdrSubtotal["Percentage"] = System.Convert.ToDecimal(1) * 100;
            rdrSubtotal["issubtotal"] = "1";

            returndt.Rows.Add(rdrSubtotal);


            
           


            //给评分项初始化

            for (int j = 0; j < dtBiddingSupplier.Rows.Count; j++)
            {
                rdrSubtotal["Point" + (j + 1)] = 0;
                rdrSubtotal["Code" + (j + 1)] = "";
                rdrSubtotal["GradeMessageCode" + (j + 1)] = "";
            }


            //给评分项赋值


            for (int GradeRowIndex = 0; GradeRowIndex < returndt.Rows.Count-1; GradeRowIndex++)
            {
                for (int GradeColumnIndex = 0; GradeColumnIndex < System.Convert.ToInt32(returndt.Rows[0]["ColumnCount"]); GradeColumnIndex++)
                {
                    // * System.Convert.ToDecimal(returndt.Rows[GradeRowIndex]["Percentage"])/100
                    rdrSubtotal["Point" + (GradeColumnIndex+1)] = System.Convert.ToString(System.Convert.ToDecimal(rdrSubtotal["Point" + (GradeColumnIndex+1)]) + System.Convert.ToDecimal(returndt.Rows[GradeRowIndex]["Point" + (GradeColumnIndex+1)]));
                }
            }


        }


        private void LoadData(bool Flag)
        {



            DataTable dt = RmsPM.BLL.BiddingGradeConsiderDiathesis.GetAllBiddingGradeConsiderDiathesis().CurrentTable;
            DataTable returndt = RmsPM.BLL.BiddingGradeConsiderDiathesis.GetAllBiddingGradeConsiderDiathesis().CurrentTable;

            dtBiddingSupplier = BLL.BiddingSystem.Get_AllMessage(this.ApplicationCode);

           RmsPM.BLL.BiddingPrejudication.GetSHTreeDataSource(dt, dtBiddingSupplier, returndt, "", "ConsiderDiathesisCode", "ParentCode", "", "", "", 1, 0, "");
            



            if (Flag)
            {
                this.DynamicColumnName.InnerHtml = "";
                for (int tempi = 0; tempi < dtBiddingSupplier.Rows.Count; tempi++)
                {
                    this.DynamicColumnName.InnerHtml += "<td class=\"blackbordertd\">"+returndt.Columns["Point"+(tempi+1)].Caption+"</td>";

                }

                this.Repeater1.DataSource = returndt;
                this.Repeater1.DataBind();
            }
            else
            {
                this.DynamicColumnName1.InnerHtml = "";
                for (int tempi = 0; tempi < dtBiddingSupplier.Rows.Count; tempi++)
                {
                    this.DynamicColumnName1.InnerHtml += "<td class=\"blackbordertd\">" + returndt.Columns["Point" + (tempi + 1)].Caption + "</td>";
                }
                this.Repeater2.DataSource = returndt;
                this.Repeater2.DataBind();
            }
        }



        public string SubmitGradeData()
        {
            using (StandardEntityDAO dao = new StandardEntityDAO("GradeMessage"))
            {
                try
                {
                   
                    //string GradeMessageCode = Request["GradeMessageCode"] + "";
                    //string act = Request["act"] + "";
                    string msg = "";
                    
                    if (msg != "")
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                        return msg;
                    }
                    dao.BeginTrans();

                   
                   



                    //Grade gv = new Grade();
                    //gv.GradeMessageCode = this.GradeMessageCode;
                    //DataTable Gradedt = gv.GetGrades();
                    string GradeMessageFilter = "";
                    
                    //取的所有使用到的MessageCode
                    for (int i = 0; i < this.Repeater1.Items.Count; i++)
                    {

                        //if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text == "1")
                        //{
                        //    continue;
                        //}
                        if (((Label)Repeater1.Items[i].FindControl("LabelFlag")).Text == "1")
                        {


                            int ColumnCount = System.Convert.ToInt32(((Label)Repeater1.Items[i].FindControl("LblColumnCount")).Text);

                            for (int ColumnIndex = 0; ColumnIndex < ColumnCount; ColumnIndex++)
                            {

                                if (ColumnIndex != ColumnCount - 1)
                                {
                                    GradeMessageFilter = GradeMessageFilter + "'" + ((Label)Repeater1.Items[i].FindControl("GradeMessageCode"+(ColumnIndex+1))).Text + "',";
                                }
                                else
                                {
                                    GradeMessageFilter = GradeMessageFilter + "'" + ((Label)Repeater1.Items[i].FindControl("GradeMessageCode"+(ColumnIndex+1))).Text + "'";
                                }
                            }

                        }
                        break;
                    }

                    if (GradeMessageFilter != "")
                    {
                       
                        BiddingGrade cBiddingGrade = new BiddingGrade();
                        cBiddingGrade.BiddingGradeMessageCode = GradeMessageFilter;
                        DataTable dtGrade = cBiddingGrade.GetBiddings();

                        for (int i = 0; i < Repeater1.Items.Count; i++)
                        {

                            if (((Label)Repeater1.Items[i].FindControl("LabelFlag")).Text == "0")
                            {
                                continue;
                            }
                            int ColumnCount = System.Convert.ToInt32(((Label)Repeater1.Items[i].FindControl("LblColumnCount")).Text);

                            for (int ColumnIndex = 0; ColumnIndex < ColumnCount; ColumnIndex++)
                            {
                                string GradeMessageCode = ((Label)Repeater1.Items[i].FindControl("GradeMessageCode" + (ColumnIndex + 1))).Text;
                                if (dtGrade.Select("BiddingGradeMessageCode='" + GradeMessageCode + "'").Length != 0)
                                {
                                    cBiddingGrade.BiddingGradeCode = ((Label)Repeater1.Items[i].FindControl("Code" + (ColumnIndex + 1))).Text.Trim();
                                    cBiddingGrade.GradePoint = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("Point" + (ColumnIndex + 1))).Text.Trim());
                                    if (cBiddingGrade.BiddingGradeCode != "")
                                        cBiddingGrade.BiddingGradeUpdate();

                                }
                                else
                                {
                                    cBiddingGrade.BiddingGradeCode = "";
                                    cBiddingGrade.BiddingConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text.Trim();
                                    cBiddingGrade.BiddingGradeMessageCode = GradeMessageCode;
                                    cBiddingGrade.GradePoint = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("Point" + (ColumnIndex + 1))).Text.Trim());
                                    cBiddingGrade.BiddingGradeAdd();
                                }
                            }

                        }
                    }
                    dao.CommitTrans();
                    return "";
                }
                catch (Exception ex)
                {
                    dao.RollBackTrans();
                    Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
                    throw ex;
                    return ex.Message;
                }
            }
        }


    }
}