using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using Rms.WorkFlow;
using System.Data;


/// <summary>
/// Summary description for WorkFlow
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WorkFlow : System.Web.Services.WebService
{

    public WorkFlow()
    {
    }
    [WebMethod(EnableSession = true)]
    public bool Login(string userID, string pwd)
    {
        try
        {
            bool OK = false;
            UserStrategyBuilder sb = new UserStrategyBuilder();
            sb.AddStrategy(new Strategy(UserStrategyName.UserID, userID));
            string sql = sb.BuildMainQueryString();

            Rms.ORMap.QueryAgent qa = new QueryAgent();
            EntityData entity = qa.FillEntityData("SystemUser", sql);
            qa.Dispose();
            string workNO = "";


            if (!entity.HasRecord())
            {

            }
            else
            {
                string RealPwd = entity.GetString("Password");
                if (RealPwd != pwd)
                {
                    OK = false;
                }
                else
                {

                    int status = entity.GetInt("Status");
                    // 0－正常，1 禁用
                    if (status == 0)
                    {

                        string userCode = entity.GetString("UserCode");
                        RmsPM.Web.User user = new RmsPM.Web.User(userCode);
                        //						user.ResetUser("P1010");
                        Session["User"] = user;
                        workNO = user.WorkNO;
                        OK = true;
                       
                        /***************************************************************/
                    }
                }

            }
            entity.Dispose();

            if (OK)
            {
                //记录最后操作时间
                Session["LastOperTime"] = DateTime.Now;
            }
            return OK;

        }
        catch (Exception ex)
        {            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "用户登录失败");
           
            return false;
        }
    }
    [WebMethod(EnableSession = true)]
    public WorkFlowItem[] GetAllCase(string type)
    {
        if (Session["User"] == null)
        {
            WorkFlowItem[] items=new WorkFlowItem[2];
            items[0]=WorkFlowItem.GetHeadName();
            items[1]=new WorkFlowItem();
            items[1].Title="未登录，不能取得流程清单";
            return items;
        }
        if (type == string.Empty) type = "Begin";
        return LoadWorkFlowList(((RmsPM.Web.User)Session["User"]).UserCode,type);
        
        
    }
    [WebMethod(EnableSession = true)]
    public WorkFlowItem[] GetAllCase2(string userID, string pwd, string type)
    {
        if (!Login(userID,pwd))
        {
            WorkFlowItem[] items = new WorkFlowItem[2];
            items[0] = WorkFlowItem.GetHeadName();
            items[1] = new WorkFlowItem();
            items[1].Title = "未登录，不能取得流程清单";
            return items;
        }
        if (type == string.Empty) type = "Begin";
        return LoadWorkFlowList(((RmsPM.Web.User)Session["User"]).UserCode,type);


    }
    public class WorkFlowItem
    {
        public string CaseCode="";
        public string ProjectName="";
        public string FlowName="";
        public string TaskName="";
        public string Title="";
        public string FromUser="";
        public string FromDate="";
        public string Url="";
        public static WorkFlowItem GetHeadName()
        {
            WorkFlowItem i = new WorkFlowItem();
            i.CaseCode="流水号";
            i.ProjectName="项目简称";
            i.FlowName="流程名称";
            i.TaskName="任务";
            i.Title="主题";
            i.FromUser="发件人";
            i.FromDate = "发件时间";
            i.Url="链接地址";
            return i;
        }
    }
    private WorkFlowItem[] LoadWorkFlowList(string usercode,string type)
    {
        try
        {
            string sql = BuildWorkFlowSqlString(usercode,type);
            QueryAgent qa = new QueryAgent();
            EntityData entity = qa.FillEntityData("WorkFlowAct", sql);
            qa.Dispose();
           
            DataTable dt = entity.CurrentTable;
            int rowcount=dt.Rows.Count;     
            WorkFlowItem[] list=new WorkFlowItem[rowcount+1];
            list[0]=WorkFlowItem.GetHeadName();     


            for (int i = 1; i <= rowcount;i++ )
            {
                DataRow dr=dt.Rows[i-1];
                list[i]=new WorkFlowItem();
                list[i].CaseCode=RmsPM.BLL.WorkFlowRule.GetWorkFlowNumber(dr["CaseCode"].ToString());//流水号
                list[i].ProjectName=RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseProjectName(dr["CaseCode"].ToString());//项目简称
                list[i].FlowName=dr["ProcedureName"].ToString();//流程名称
                list[i].TaskName=dr["ToTaskName"].ToString();//任务
                list[i].Title=RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTitle(dr["CaseCode"].ToString());//主题
                list[i].FromUser=RmsPM.BLL.SystemRule.GetUserName(dr["FromUserCode"].ToString());//发件人
                list[i].FromDate=RmsPM.BLL.WorkFlowRule.GetFormatExcedableDate(DateTime.Parse(dr["FromDate"].ToString()));//发件时间  
                string url=dr["ApplicationPath"].ToString()+"?action=Sign";
                url+="&CaseCode="+list[i].CaseCode;
                url+="&ActCode="+dr["ActCode"].ToString();
                url+="&ApplicationCode="+dr["ApplicationCode"].ToString();
                list[i].Url=url;

            }          

            entity.Dispose();
            return list;

        }
        catch (Exception ex)
        {
            WorkFlowItem[] items=new WorkFlowItem[2];
            items[0]=WorkFlowItem.GetHeadName();
            items[1]=new WorkFlowItem();
            items[1].Title = "出现异常，不能取得流程清单";
            
            return items;
        }
    }

    private string BuildWorkFlowSqlString(string usercode,string type)
    {
        WorkFlowActStrategyBuilder sb = new WorkFlowActStrategyBuilder();
        switch (type)
        {
            case "Begin":
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.StatusBegin, "Begin"));
                break;
            case "DealWith":
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.Status, "'DealWith'"));
                break;
            case "BeginAndDealWith":
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.StatusBeginAndDealWith, "Begin"));
                break; 
            default:
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.StatusBegin, "Begin"));
                break;

        }
        sb.AddStrategy(new Strategy(WorkFlowActStrategyName.StatusBegin, "Begin"));
        sb.AddStrategy(new Strategy(WorkFlowActStrategyName.InActUser, usercode));
        sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ActMeetOrder, ""));

        string sql = sb.BuildMainQueryString();
        //排序  
            sql = sql + " order by fromdate ";

        return sql;
    }
    

}

