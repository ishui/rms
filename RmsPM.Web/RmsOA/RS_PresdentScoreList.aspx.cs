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
using System.Collections.Generic;
using System.Xml;
using System.IO;

using RmsOA.MODEL;
using RmsOA.BFL;
using RmsPM.Web;



public partial class RmsOA_RS_PresdentScoreList : PageBase
{

    private List<EmployScoreMode> lsModel;
    private RS_ScoreManageBFL smBFl = new RS_ScoreManageBFL();
    private DateTime dtNow;
    private string deptName;
    bool hasScored;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(this.HasFiles())
        {
            this.GridView1.RowDataBound += new GridViewRowEventHandler(GridView1_RowDataBound);
        }
        else
        {
            this.GridView1.RowDataBound += new GridViewRowEventHandler(GridView1_RowDataBound2);
        }
        if (!Page.IsPostBack)
        {
            lsModel = smBFl.GetDeptScores(user.UserCode, RS_ScoreExtend.CheckMonth, "Load");
            if (lsModel != null && lsModel.Count != 0)
            {
                dtNow = DateTime.Parse(lsModel[0].MarkTime);
                ViewState["Time"] = dtNow;
                if (dtNow == smBFl.GetMonthFirstDate(RS_ScoreExtend.CheckMonth))
                {
                    hasScored = true;
                }
                else
                {
                    hasScored = false;
                }
            }
            else
            {
                //dtNow = DateTime.Now;
                ViewState["Time"] = RS_ScoreExtend.CheckMonth;
                hasScored = false;
            }
            this.GridView1.DataSource = lsModel;
            this.GridView1.DataBind();
            if (hasScored.Equals(true))
            {
                this.AddButton.Enabled = false;
            }
        }//添加新记录后的页面刷新
        else
        {
            if (ScorceMonth.Value.Equals(string.Empty))
            {
                lsModel = smBFl.GetDeptScores(user.UserCode, RS_ScoreExtend.CheckMonth, "");
                this.GridView1.DataSource = lsModel;
                this.GridView1.DataBind();
                this.AddButton.Enabled = false;
            }
        }
        this.InitNavigator();
    }

    void GridView1_RowDataBound2(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = index.ToString();
        }
    }

    public void InitNavigator()
    {
        if(user.HasRight(ScoreRight.ScoreForEmploy))
        {
            Master.DeptManage = true;
        }
        if (user.HasRight(ScoreRight.ScoreForManager))
        {
           Master.VicePresident = true;
        }
        //为部门打分-总经理
        if (user.HasRight(ScoreRight.ScoreForDept))
        {
            Master.President = true;
        }
        if(user.HasRight(ScoreRight.ViewStat))
        {
           Master.MonthState = true;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = index.ToString();
            if(String.IsNullOrEmpty(e.Row.Cells[1].Text))
            {
                HtmlInputHidden hidDeptCode = (HtmlInputHidden)(e.Row.FindControl("hidDeptCode"));
                string deptCode = hidDeptCode.Value;
                List<DeptSupplyModel> lsSupplyModel = new List<DeptSupplyModel>();
                lsSupplyModel = this.GetSpecialDeptName();
                foreach(DeptSupplyModel sm in lsSupplyModel)
                {
                    if(deptCode.Contains(sm.DeptCode))
                    {
                        if(deptCode.Contains("A"))
                        {
                            e.Row.Cells[1].Text = sm.DeptName;
                            e.Row.Cells[2].Text = sm.SupplyName[0] +"：  " + e.Row.Cells[2].Text;
                        }
                        else if(deptCode.Contains("B"))
                        {
                            e.Row.Cells[1].Text = sm.DeptName;
                            e.Row.Cells[2].Text = sm.SupplyName[1] +"：  " + e.Row.Cells[2].Text;
                        }
                    }
                }
            }
        }

    }

    public void SearchButton_ServerClick(object sender, EventArgs e)
    {
        if (ScorceMonth.Value != "")
        {
            dtNow = DateTime.Parse(ScorceMonth.Value);
        }
        else
        {
            dtNow = RS_ScoreExtend.CheckMonth;
        }

        ViewState["Time"] = dtNow;
        lsModel = smBFl.GetDeptScores(user.UserCode, dtNow,"");
        this.GridView1.DataSource = lsModel;
        this.GridView1.DataBind();
    }

    public void GetYear()
    {
        Response.Write(DateTime.Parse(ViewState["Time"].ToString()).Year.ToString());
    }

    public void GetMonth()
    {
       Response.Write(DateTime.Parse(ViewState["Time"].ToString()).Month.ToString());
    }

    public bool HasFiles()
    {
        string path = Server.MapPath("/") + @"RmsOA\DeptSupply.xml";
        //string path = @"D:\RmsPM20\RmsPM20\RmsOA\DeptSupply.xml";
        bool exsit = File.Exists(path);
        return exsit;
    }
    public List<DeptSupplyModel> GetSpecialDeptName()
    {
        List<DeptSupplyModel> lsDept = new List<DeptSupplyModel>();
        DeptSupplyModel dept;
        string path = Server.MapPath("/") + @"RmsOA\DeptSupply.xml";
        //string path = @"D:\RmsPM20\RmsPM20\RmsOA\DeptSupply.xml";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path);
        XmlNodeList xmlList = xmlDoc.DocumentElement.ChildNodes;
        if(xmlList.Count > 0)
        {
            foreach(XmlElement xmlEle in xmlList)
            {
                if(xmlEle.Name.Equals("Dept"))
                {
                    dept = new DeptSupplyModel();
                    XmlNodeList nodeList = xmlEle.ChildNodes;
                    foreach(XmlElement xmlEleIN in nodeList)
                    {
                        if(xmlEleIN.Name.Equals("DeptName"))
                        {
                            if(!String.IsNullOrEmpty(xmlEleIN.InnerText))
                            {
                                dept.DeptName = xmlEleIN.InnerText;
                            }
                        }
                        if(xmlEleIN.Name.Equals("DeptCode"))
                        {
                            if(!string.IsNullOrEmpty(xmlEleIN.InnerText))
                            {
                               dept.DeptCode = xmlEleIN.InnerText;
                            }
                        }
                        if(xmlEleIN.Name.Equals("Supply"))
                        {
                            XmlNodeList nameList = xmlEleIN.ChildNodes;
                            List<string> names = new List<string>();
                            foreach(XmlElement xe in nameList)
                            {
                                names.Add(xe.InnerText);
                            }
                            dept.SupplyName = names;
                        }
                    }
                    lsDept.Add(dept);
                }
            }
        }
        return lsDept;
    }
}
