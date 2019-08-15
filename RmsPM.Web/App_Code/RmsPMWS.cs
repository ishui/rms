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
using System.Web.Services;
using System.Web.Services.Protocols;


namespace RmsPM.Web{
/// <summary>
/// Summary description for RmsPMWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class RmsPMWS : System.Web.Services.WebService
{

    public RmsPMWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public string HelloWorld()
    {
        string result = "Hello,";
        if (Session["Times"] == null)
        {
            Session["Times"] = 1;
        }
        else
        {
            Session["Times"] = ((int)Session["Times"])+1;
        }
        result += (" Visit Times:" + Session["Times"].ToString());
       
        return result;
    }
    [WebMethod(EnableSession = true)]
    public string GetUser()
    {
        string result = "Hello";
        if (Session["User"] != null)
        {
            result = result + ("," + ((User)Session["User"]).UserName);
        }
        else
        {
            result = result + ",you have not log in .";
        }
        return result;
    }
    [WebMethod]
    public string AutoRunViseID(string ProjCode,string contractCode)
    {
        if (contractCode == "" || contractCode == null) return "";

        string strFirstTemp = "";
        string strNextTemp = "";
        int iFirstTemp = 1;
        int iNextTemp = 1;
        string contractID = "";

        RmsPM.BFL.LocaleViseBFL bfl = new RmsPM.BFL.LocaleViseBFL();
        TiannuoPM.MODEL.LocaleViseQueryModel querymodel = new TiannuoPM.MODEL.LocaleViseQueryModel();
        List<TiannuoPM.MODEL.LocaleViseModel> models = new List<TiannuoPM.MODEL.LocaleViseModel>();


        querymodel.ViseProject = ProjCode;
        querymodel.ViseContractCode = contractCode;
        models = bfl.GetLocalVises(querymodel);        
        if (models.Count > 0)
        {           
            for (int k = 0; k < models.Count; k++)
            {
                strNextTemp = models[k].ToString().Substring(models[k].ViseId.ToString().Length - 3, 3); ;
                try
                {
                    iNextTemp = Convert.ToInt32(strNextTemp);
                    if (iFirstTemp <= iNextTemp)
                {
                    iFirstTemp = iNextTemp+1;
                }
                }
                catch
                {
                    ; //如果是非数字，则滤过
                }                                
            }
            strFirstTemp = Convert.ToString(iFirstTemp ).PadLeft(3, '0');
        }
        else
        {
            strFirstTemp = "001";
        }
        contractID = RmsPM.BLL.ContractRule.GetContractID(contractCode);  //合同编号可能是空字符串，要注意

        return contractID + strFirstTemp;
    }
    [WebMethod(EnableSession = true)]
    public SupplierInfo GetSupplierByContract(string ContractCode)
    {
      //  RmsPM.BLL.
        SupplierInfo si = new SupplierInfo();
        try
        {
            Rms.ORMap.EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetSupplierByContractCode(ContractCode);
            if (entity.HasRecord())
            {
                si.Name = entity.CurrentRow["SupplierName"].ToString();
                si.Code = entity.CurrentRow["SupplierCode"].ToString();
            }
            else
            {
                si.Code = "0";
                si.Name = "未知";
            }
            entity.Dispose();
        }
        catch (Exception ex) { LogHelper.WriteLog("ws", ex); }
        return si;
    }
    
}
    public struct SupplierInfo{        
        public string Code;
        public string Name;
    }
}

