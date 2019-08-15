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
/// WorkFlowDeputize 的摘要说明


/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WorkFlowDeputize : System.Web.Services.WebService
{
    /// <summary>
    /// 动作实例代码
    /// </summary>
    private string _ActCode = null;
    private string _CaseCode = null;
    private string _FlowName = null;

    /// <summary>
    /// 动作实例代码
    /// </summary>
     public string ActCode
    {
        get
        {
            if (_ActCode == null)
            {
                
                return "";
            }
            return _ActCode;
        }
        set
        {
            _ActCode = value;
           
        }
    }
    /// <summary>
    /// 动作实例代码
    /// </summary>
     public string FlowName
    {
        get
        {
            if (_FlowName == null)
            {
              
                return "";
            }
            return _FlowName;
        }
        set
        {
            _FlowName = value;
          
        }
    }


    /// <summary>
    /// 动作实例代码
    /// </summary>
     public string CaseCode
    {
        get
        {
            if (_CaseCode == null)
            {
              
                return "";
            }
            return _CaseCode;
        }
        set
        {
            _CaseCode = value;
         
        }
    }
    public WorkFlowDeputize()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 创建流程
    /// </summary>
    /// <param name="ProcudeID">流程索引名</param>
    /// <param name="ApplicationCode">业务编号</param>
    /// <param name="ApplicationTitle">业务标题</param>
    /// <param name="XmlData">业务数据</param>
    /// <param name="Username">发起者登陆名</param>
    /// <param name="ToTask">任务</param>
    /// <param name="ToUser">接收人</param>
    /// <returns></returns>
    [WebMethod]
    public string CreateCase(String ProcudeID,string ApplicationCode,string ApplicationTitle,string XmlData,string Username,String ToTask,string ToUser)
    {
        try
        {
            ProcudeID = System.Web.HttpUtility.UrlDecode(ProcudeID);
            ApplicationTitle = System.Web.HttpUtility.UrlDecode(ApplicationTitle);
            XmlData = System.Web.HttpContext.Current.Server.HtmlDecode(System.Web.HttpUtility.UrlDecode(XmlData)).Replace("<br>", "\n");
            Username = System.Web.HttpUtility.UrlDecode(Username);
            ToTask = System.Web.HttpUtility.UrlDecode(ToTask);
            ToUser = System.Web.HttpUtility.UrlDecode(ToUser);

            string ProjectName = "";
            RmsPM.BLL.ConvertRule cConvertRule = new RmsPM.BLL.ConvertRule();
            System.Xml.XmlNode xmlnode = cConvertRule.GetXmlDate(XmlData.ToLower());
            if (xmlnode.SelectSingleNode("projectname") != null)
            {
                ProjectName = xmlnode.SelectSingleNode("projectname").InnerText.Trim();
            }
            string SystemUserCode = "";
            string SystemUnitCode = "";

            string Transactor = "";
            string TransactUnit = "";
            this.FlowName = ProcudeID;
            UserStrategyBuilder sb = new UserStrategyBuilder();

            sb.AddStrategy(new Strategy(UserStrategyName.UserID, Username));

            string sql = sb.BuildMainQueryString();

            Rms.ORMap.QueryAgent qa = new QueryAgent();
            EntityData entity = qa.FillEntityData("SystemUser", sql);
            qa.Dispose();
            if (entity.HasRecord())
            {
                SystemUserCode = entity.GetString("UserCode");
            }

            if (SystemUserCode == "")
            {
                return "UserNotFound";
            }


            string ProjectCode = "";
            if (ProjectName != "")
            {
                ProjectCode = RmsPM.BLL.ProjectRule.GetProjectCodeByName(ProjectName.Trim());
            }
            string actCode = "";
            string ProcedureCode = RmsPM.BLL.WorkFlowRule.GetProcedureCodeByName(ProcudeID, ProjectCode);
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.StartNewWorkCase(ApplicationCode, ProcedureCode, SystemUserCode, SystemUnitCode, ref actCode, Transactor, TransactUnit);
            System.Collections.IDictionaryEnumerator ie = workCase.GetActEnumerator();
            while (ie.MoveNext())
            {
                Act act = (Act)ie.Value;
                if (act.Copy != 1)
                    act.IsSleep = 1;
            }
            DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
            RmsPM.BLL.WorkFlowApplicationRule.SaveWorkCaseEx(ds, workCase.CaseCode);
            this.CaseCode = workCase.CaseCode;
            this.ActCode = actCode;
            this.SaveCasePropertyValue("主题", ApplicationTitle, ProcedureCode);
            this.SaveCasePropertyValue("申请人", SystemUserCode, ProcedureCode);
            this.SaveCasePropertyValue("项目代码", ProjectCode, ProcedureCode);
            this.SaveCasePropertyValue("项目部门", RmsPM.BLL.ProjectRule.GetUnitByProject(ProjectCode), ProcedureCode);
            this.SaveCasePropertyValue("业务类别", "", ProcedureCode);
            this.SaveCasePropertyValue("业务部门", "", ProcedureCode);
            this.SaveCasePropertyValue("业务数据", XmlData, ProcedureCode);
            this.SaveCasePropertyValue("表单部门", "", ProcedureCode);
            this.SaveCasePropertyValue("手送资料", "", ProcedureCode);

            string NumberString = RmsPM.BLL.WorkFlowRule.GetProcedureNumberByName(this.FlowName) + DateTime.Now.Year.ToString().Substring(2, 2);

            int FlowNumberLenth = (RmsPM.BLL.SystemRule.GetProjectConfigValue("FlowNumberLength") == "") ? 4 : int.Parse(RmsPM.BLL.SystemRule.GetProjectConfigValue("FlowNumberLength"));

            NumberString += RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode(NumberString).Substring(6 - FlowNumberLenth, FlowNumberLenth);

            this.SaveCasePropertyValue("流水号", NumberString, ProcedureCode);

            /////////////////////////////////////////////////////////////////////



            if (actCode != "")
                return "success";
            else
                return "faild";
        }
        catch (System.Exception ec)
        {
            return ec.Message;
        }
    }


    private void SaveCasePropertyValue(string PropertyName, string PropertyValue,string ProcedureCode)
    {

        string PropertyCode = "";
        string WorkFlowCasePropertyCode = "";
        Procedure procedure = DefinitionManager.GetProcedureDifinition(ProcedureCode, true);
        System.Collections.IDictionaryEnumerator ie = procedure.GetPropertyEnumerator();
        while (ie.MoveNext())
        {
            Property PropertyCase = (Property)ie.Value;
            if (PropertyCase.ProcedurePropertyName == PropertyName)
            {
                PropertyCode = PropertyCase.WorkFlowProcedurePropertyCode;
            }
        }

        if (this.CaseCode + "" != "")
        {
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.CaseCode);

            ie = workCase.GetCasePropertyEnumerator();
            while (ie.MoveNext())
            {
                CaseProperty CasePropertyCase = (CaseProperty)ie.Value;
                if (CasePropertyCase.ProcedurePropertyCode == PropertyCode)
                {
                    WorkFlowCasePropertyCode = CasePropertyCase.WorkFlowCasePropertyCode;
                }
            }
        }
        if (PropertyCode != "")
        {
            CaseProperty CasePropertyCaseM = new CaseProperty();
            CasePropertyCaseM.WorkFlowCasePropertyCode = WorkFlowCasePropertyCode;
            CasePropertyCaseM.CaseCode = this.CaseCode;
            CasePropertyCaseM.ProcedurePropertyCode = PropertyCode;
            CasePropertyCaseM.ProcedurePropertyValue = PropertyValue;

            RmsPM.BLL.WorkFlowRule.SaveCaseProperty(CasePropertyCaseM);
        }
      
    }

}

