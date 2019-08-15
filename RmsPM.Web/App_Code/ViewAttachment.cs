using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RmsPM.BLL;
namespace RmsPM.Web
{
/// <summary>
/// Summary description for ViewAttachment
/// </summary>
public class ViewAttachment:System.Web.UI.Page
{
    public ViewAttachment()
    {
        //
        // TODO: Add constructor logic here
        //
        
    }public void OutputAttachment(HttpResponse Response,string AttachMentCode, string AttachMent, DocumentRule documentRule)
        {
            documentRule.GetAttachmentByCode(AttachMentCode);
            Response.Clear();
            Response.ContentType = documentRule.ContentType;
            switch (AttachMent)
            {
                case "0":
                    break;
                case "1":
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(documentRule.FileName));
                    break;
                default:
                    //Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(filename));
                    Response.AppendHeader("Content-Disposition", "filename=" + Server.UrlEncode(documentRule.FileName));
                    break;

            }
            Response.BinaryWrite(documentRule.Content);
            Response.Flush();
            Response.End();
        }
}
}
