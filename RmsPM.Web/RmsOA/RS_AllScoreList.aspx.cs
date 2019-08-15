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
using System.Text;
using System.Xml;
using System.IO;

using RmsPM.Web;
using RmsOA.MODEL;
using RmsOA.BFL;

public partial class RmsOA_RS_AllScoreList : PageBase
{
    DateTime dt;
    List<UnitScoreModel> lsUSModel = new List<UnitScoreModel>();
    List<EmployScoreMode> lsESModel = new List<EmployScoreMode>();
    RS_ScoreManageBFL bfl = new RS_ScoreManageBFL();
    SearchType st;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dt = RS_ScoreExtend.CheckMonth;
            st = SearchType.Load;
        }

        this.InitNavigator();
    }

    public void InitNavigator()
    {
           //为部门员工打分-部门经理
        if (user.HasRight(ScoreRight.ScoreForEmploy))
        {
            Master.DeptManage =true;
        }
        //为部门经理打分-副总经理
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
    public void WriteTBody()
    {
        lsUSModel = bfl.GetUnitScoreModel(dt);
        if (lsUSModel == null || lsUSModel.Count == 0)
        {
            Response.Write("<tr><td colspan='5'><font style='font-weight:bolder;'>对不起,没有本月的统计结果!</font></td></tr>");
        }
        else
        {
            if(!this.HasFiles())
            {
                for (int i = 0; i < lsUSModel.Count; i++)
                {
                    string[] tempArray = this.WriteEmployScore(lsUSModel[i].DeptCode);
                    Response.Write(" <tr><td align=\"center\">"
                        +this.ChangeStrings(lsUSModel[i].DeptName)+"</td><td align=\"center\">"
                        + lsUSModel[i].Score + "</td><td align=\"center\" style='padding-left: 0px;padding-right: 0px;'><table height='100%' width='100%' border='0' cellspacing='0' cellpadding='0'>"
                        + tempArray[0] + "</table></td><td align=\"center\" style='padding-left: 0px;padding-right: 0px;'><table height='100%' width='100%' border='0' cellspacing='0' cellpadding='0'>"
                        + tempArray[1] + "</table></td><td align=\"center\">"
                        +this.ChangeStrings(lsUSModel[i].Marker)+"</td></tr>");
                }
            }
            else
            {
                for(int i=0; i<lsUSModel.Count; i++)
                {
                    string[] tempArray = new string[2];
                    if(!String.IsNullOrEmpty(lsUSModel[i].DeptName))
                    {
                        tempArray= this.WriteEmployScore(lsUSModel[i].DeptCode);
                        Response.Write(" <tr><td align=\"center\">"
                            +this.ChangeStrings(lsUSModel[i].DeptName)+"</td><td align=\"center\">"
                            + lsUSModel[i].Score + "</td><td align=\"center\" style='padding-left: 0px;padding-right: 0px;'><table height='100%' width='100%' border='0' cellspacing='0' cellpadding='0'>"
                            + tempArray[0] + "</table></td><td align=\"center\" style='padding-left: 0px;padding-right: 0px;'><table height='100%' width='100%' border='0' cellspacing='0' cellpadding='0'>"
                            + tempArray[1] + "</table></td><td align=\"center\">"
                            +this.ChangeStrings(lsUSModel[i].Marker)+"</td></tr>");
                    }
                    else
                    {
                        string deptCode = lsUSModel[i].DeptCode;
                        List<DeptSupplyModel> lsDSModel = this.GetSpecialDeptName();
                        foreach(DeptSupplyModel ds in lsDSModel)
                        {
                            tempArray = this.WriteEmployScore(ds.DeptCode);
                            StringBuilder sbSupplyName = new StringBuilder();
                            if(deptCode.Contains("A"))
                            {
                                sbSupplyName.Append(ds.SupplyName[0] + "：  " + lsUSModel[i].Score.ToString() + "</br>");
                                sbSupplyName.Append(ds.SupplyName[1] + "：  " + lsUSModel[i+1].Score.ToString());
                            }
                            else
                            {
                                sbSupplyName.Append(ds.SupplyName[1] + "：  " + lsUSModel[i].Score.ToString() + "</br>");
                                sbSupplyName.Append(ds.SupplyName[0] + "：  " + lsUSModel[i+1].Score.ToString());

                            }
                            Response.Write(" <tr><td align=\"center\">"
                            +this.ChangeStrings(ds.DeptName)+"</td><td align=\"center\">"
                            + sbSupplyName.ToString() + "</td><td align=\"center\" style='padding-left: 0px;padding-right: 0px;'><table height='100%' width='100%' border='0' cellspacing='0' cellpadding='0'>"
                            + tempArray[0] + "</table></td><td align=\"center\" style='padding-left: 0px;padding-right: 0px;'><table height='100%' width='100%' border='0' cellspacing='0' cellpadding='0'>"
                            + tempArray[1] + "</table></td><td align=\"center\">"
                            +this.ChangeStrings(lsUSModel[i].Marker)+"</td></tr>");
                            i++;
                        }
                    }
                }
            }
        }     
    }

    public string[] WriteEmployScore(string dept)
    {
        string[] tempStrArray = new string[2];
        StringBuilder sbUserName = new StringBuilder();
        StringBuilder sbUserScore = new StringBuilder();
        lsESModel = bfl.GetEmployScore(dept,dt,st);
        if (lsESModel == null || lsESModel.Count == 0)
        {
            tempStrArray[0] = "";
            tempStrArray[1] = "";
        }
        else
        {
            for (int i = 0; i < lsESModel.Count; i++)
            {
                sbUserName.Append("<tr><td style=\"border-right:0px;\" align=\"center\">" + lsESModel[i].UserName + "</td></tr>");
                sbUserScore.Append("<tr><td style=\"border-right:0px;\" align=\"center\">" + lsESModel[i].Score + "</td></tr>");
            }
        }
        tempStrArray[0] = sbUserName.ToString();
        tempStrArray[1] = sbUserScore.ToString();
        return tempStrArray;
    }

    public string ChangeStrings(string str)
    {
        if(!String.IsNullOrEmpty(str))
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                sb.Append(str.Substring(i,1));
                sb.Append("<br/>");
            }
            return sb.ToString();
        }
        else
        {
            return "";
        }
    }

    public void SearchButton_ServerClick(object sender, EventArgs e)
    {
        if (ScorceMonth.Value != "")
        {
            dt = DateTime.Parse(ScorceMonth.Value);
            st = SearchType.Search;
        }
        else 
        {
            dt = DateTime.Now;
            st = SearchType.Search;
        }
    }
    public void GetYear()
    {
        Response.Write(dt.Year);
    }

    public void GetMonth()
    {
        Response.Write(dt.Month);
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
