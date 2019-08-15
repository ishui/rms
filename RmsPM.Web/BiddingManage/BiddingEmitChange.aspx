<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiddingEmitChange.aspx.cs" Inherits="BiddingManage_BiddingEmitChange" %>
<%@ Register Src="../BiddingControl/BiddingEmit.ascx" TagName="BiddingEmit" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改发标内容</title>
    <link href="../images/index.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25" valign="top">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                修改发标信息</td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"/></td>
                            <td class="tools-area">
                                <input name="btnSave" id="btnSave" type="button" value=" 保存 " class="button" runat="server" onserverclick="btnSave_ServerClick"
                                     />
                                <input name="btnModify" id="btnModify" type="button" value=" 取消 " class="button"
                                    runat="server" onclick="window.close();return false;"/>
                            </td>
                        </tr>
                     </table>
                    <table class="table" id="table1" width="100%">
                        <tr>
                        <td><uc1:BiddingEmit ID="BiddingEmit1" runat="server" /></td>
                        </tr>
                     </table>
                </td>
             </tr>
          </table>
    </form>
</body>
</html>
