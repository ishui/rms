<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectModify.aspx.cs" Inherits="RmsPM.Web.Systems.ProjectModify" %>


<%@ Register TagPrefix="uc1" TagName="InputSubjectSet" Src="../UserControls/InputSubjectSet.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>项目修改</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
</head>
<body scroll="no">

    <script language="javascript" src="../Rms.js"></script>

    <script>
		function SelectUnit()
		{
			OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode=100009");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.parentUnitName.value = name;
			window.document.all.parentUnit.value = code;
		}	
    </script>

    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" bgcolor="white"
            border="0">
            <tbody>
                <tr>
                    <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                        <asp:Label ID="LabelTitle" runat="server" BorderColor="Transparent" BackColor="Transparent"
                            CssClass="TitleText"></asp:Label>&nbsp;项目修改
                        <asp:Label ID="lblParentUnitName" runat="server" CssClass="TitleText"></asp:Label></td>
                </tr>
                <tr height="100%">
                    <td valign="top" align="left">
                        <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="form-item">
                                    上级部门 ：</td>
                                <td>
                                    &nbsp;<input id="parentUnit" runat="server" class="input" name="parentUnit" style="width: 33px;
                                        height: 18px" type="hidden" /><input id="parentUnitName" runat="server" class="input"
                                            name="parentUnitName" style="width: 133px; height: 18px" type="text" readonly="readOnly" /><img
                                                onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif" style="cursor: hand" /></td>
                                <td class="form-item">
                                    帐套：</td>
                                <td>
                                    <select id="sltSubjectSet" style="display:block" name="sltSubjectSet" runat="server">
                                        <option value="" selected>------请选择------</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="10" width="100%" border="0">
                            <tr align="center">
                                <td>
                                    <input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server"
                                        onserverclick="btnSave_ServerClick">&nbsp;&nbsp;&nbsp;
                                    <input class="submit" id="btnClose" onclick="window.close();" type="button" value="取 消"
                                        name="btnClose" runat="server">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <input id="txtInputUnitCode" type="hidden" name="txtInputUnitCode" runat="server"><input
            id="txtAction" type="hidden" name="txtAction" runat="server">
        <input id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
    </form>

<%--    <script language="javascript">
<!--

	showSubjectSet();

	function showSubjectSet()
	{
		var chk = Form1.chkSelfAccount.checked;
		if ( chk )
			Form1.sltSubjectSet.style.display = "";
		else
			Form1.sltSubjectSet.style.display = "none";
	}


//-->
    </script>--%>

</body>
</html>
