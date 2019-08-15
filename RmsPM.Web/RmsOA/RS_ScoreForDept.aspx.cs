using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Xml;

using RmsPM.Web;
using RmsOA.BFL;
using RmsOA.MODEL;

public partial class RmsOA_RS_ScoreForDeptascx : PageBase
{
    RS_ScoreManageBFL bfl = new RS_ScoreManageBFL();
    RS_ScoreExtend sExtend = new RS_ScoreExtend();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
        if(this.HasFiles())
        {
            this.GridView1.DataSource = this.InitDeptExpand();
            this.GridView1.DataBind();
        }
        else
        {
            this.GridView1.DataSource = ChangeUnitModel();
            this.GridView1.DataBind();
        }

        this.SaveButton.Attributes.Add("OnClick", "return window.confirm('确定要保存吗？');");
        }
    }

    public List<UnitModelExpand> InitDeptExpand()
    {
        List<UnitModelExpand> listExpent  = new List<UnitModelExpand>();
        UnitModelExpand unitExpent = new UnitModelExpand();
        List<UnitModel> lsUnit = sExtend.GetAllUnit();
        List<DeptSupplyModel> lsDeptSupply = this.GetSpecialDeptName();
        foreach(UnitModel un in lsUnit)
        {
            unitExpent = new UnitModelExpand();
            foreach(DeptSupplyModel ds in lsDeptSupply)
            {
                if(ds.DeptCode.Equals(un.UnitCode))
                {
                    unitExpent.UnitName = un.UnitName;
                    unitExpent.UnitCode = un.UnitCode + "A";
                    unitExpent.ExpandName = ds.SupplyName[0] +"：  ";
                    listExpent.Add(unitExpent);
                    unitExpent = new UnitModelExpand();
                    unitExpent.UnitName = un.UnitName;
                    unitExpent.UnitCode = un.UnitCode + "B";
                    unitExpent.ExpandName = ds.SupplyName[1] +"：  ";
                    listExpent.Add(unitExpent);
                }
                else
                {
                    unitExpent.UnitCode = un.UnitCode;
                    unitExpent.UnitName = un.UnitName;
                    listExpent.Add(unitExpent);
                }
            }
            
        }
        return listExpent;
            
    }

    public List<UnitModelExpand> ChangeUnitModel()
    {
        List<UnitModelExpand> listExpent  = new List<UnitModelExpand>();
        UnitModelExpand unitExpent = new UnitModelExpand();
        List<UnitModel> lsUnit = sExtend.GetAllUnit();
        foreach(UnitModel um in lsUnit)
        {
            unitExpent = new UnitModelExpand();
            unitExpent.UnitName = um.UnitName;
            unitExpent.UnitCode = um.UnitCode;
            listExpent.Add(unitExpent);
        }
        return listExpent;
    }
    public void SaveButton_Click(object sender, EventArgs e)
    {
        RS_ScoreManageModel smModel = new RS_ScoreManageModel();
        RS_EmployScoreModel esModel = new RS_EmployScoreModel();
        RS_EmployScoreBFL esBFL ;
        smModel.MarkDate = bfl.GetMonthFirstDate(DateTime.Now);
        smModel.Marker = user.UserCode;
        smModel.Status = WorkFlowStatus.Audited.ToString("d");
        smModel.Type = Int32.Parse(ScoreType.Dept.ToString("d"));
        int manageCode = bfl.Insert(smModel);
        foreach (GridViewRow gvRow in GridView1.Rows)
        {
            HtmlInputHidden hidDeptCode = (HtmlInputHidden)(gvRow.FindControl("HidUnitCode"));
            TextBox tbScore = (TextBox)(gvRow.FindControl("ScoreTextBox"));
            esModel.ManageCode = manageCode;
            esModel.UserCode = hidDeptCode.Value;
            int score = 1;
            Int32.TryParse(tbScore.Text.Trim(), out score);
            esModel.Score = score;
            esBFL = new RS_EmployScoreBFL();
            esBFL.Insert(esModel);
        }

        Response.Write("<script> window.opener.location.reload();window.close();</script>");
    }

    public void GetYear()
    {
        Response.Write(RS_ScoreExtend.CheckMonth.Year.ToString());
    }
    public void GetMonth()
    {
        Response.Write(RS_ScoreExtend.CheckMonth.Month.ToString());
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int index = e.Row.RowIndex + 1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = index.ToString();
        }
    }

    protected void GridView1_RowDataBound2(object sender, GridViewRowEventArgs e)
    {
        int index = e.Row.RowIndex + 1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = index.ToString();
        }
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
