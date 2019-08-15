<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiddingConditionFileManage.aspx.cs" Inherits="RmsPM.Web.BiddingManage.BiddingConditionFileManage" %>

<%@ Register Src="ControlBiddingConditionFileModigy.ascx" TagName="ControlBiddingConditionFileModigy"
    TagPrefix="uc2" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>BiddingFileManage</title>
    <LINK href="../Images/index.css" type="text/css" rel="stylesheet">
    <SCRIPT language="javascript" src="../Rms.js"></SCRIPT>

    <link href="../Images/infra.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="10">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                招标技术条件维护</td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <input name="btnModify"  id="btnModify" type="button" value=" 修改 " class="button"
                                    runat="server" onserverclick="btnModify_ServerClick" >
                                <input name="btnSave" id="btnSave" type="button" value=" 保存 " class="button" runat="server" onserverclick="btnSave_ServerClick">
                                
                                <input name="btnDel" id="btnDel" type="button" onserverclick="btnDel_ServerClick"  value=" 删除 " class="button" runat="server">
                                <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                    onclick="javascript:window.close();"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            	<tr>
					<td class="table" vAlign="top">
					<div>
						<TABLE class="blackbordertable" cellSpacing="0" cellPadding="0" width="100%" border="0">
							
								<tr>
									<td colspan="5">
                                        &nbsp;<uc2:ControlBiddingConditionFileModigy id="BiddingConditionFileModigy1" runat="server"></uc2:ControlBiddingConditionFileModigy></td>
								</tr>
								
								</table>
								</div>
                        </td>
					</tr>
            </table>
    </form>
</body>
</html>
