<%@ Page Language="c#" Inherits="RmsPM.Web.WorkPlan.Header" CodeFile="Header.aspx.cs"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Header</title>
    <meta name="vs_defaultClientScript" content="JavaScript">
    <link href="<%=ResolveClientUrl("~/Images/head.css")%>" rel="stylesheet" type="text/css">
    <script language="javascript" src="<% =ResolveClientUrl("~/Rms.js")%>"></script>
    <style type="text/css">
        .auto-style1 {
            width: 417px;
        }
    </style>
</head>
<body background="images/topbj.gif" leftmargin="0" topmargin="0" onLoad="gotoDefault();"
    language="javascript">
    <form id="Form1" method="post" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="auto-style1">
                                <img src="images/logo.jpg"></td>
                          <td width="700" align="left" valign="middle"><table width="650" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td width="100%" align="center" valign="top"><div runat="server" id="MenuDiv"> </div></td>
                            </tr>
                          </table></td>
                            <td width="27" align="center">
                          <iframe frameborder="0" width="27" height="35" scrolling="no" id="iframe" runat="server" style="visibility: hidden"></iframe>                            </td>
                 <%--           <td width="140" align="left">
                                <a href="Picture/shipin.html" target="_blank">ÊÓÆµ¼à¿Ø</a>&nbsp; |
                                &nbsp;<a href="#" onClick="gotoDefault();return false;">×ÀÃæ</a>&nbsp; |
                                &nbsp;
                          <asp:LinkButton ID="btnQuit" runat="server" OnClick="btnQuit_Click">ÍË³ö</asp:LinkButton></td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="1" bgcolor="#505355">
                </td>
            </tr>
            <tr>
                <td height="1" bgcolor="#ffffff">
                </td>
            </tr>
        </table>
<%--        <input type="hidden" name="txtUserName" id="txtUserName" runat="server">
        <input type="hidden" name="txtOAVirtualDirectory" id="txtOAVirtualDirectory" runat="server">
        <input type="hidden" name="txtSelectProjectList" id="txtSelectProjectList" runat="server">
        <input type="hidden" name="txtGroupUser" id="txtGroupUser" runat="server">
--%>    </form>

    <script language="javascript">

	function gotoDefault()
	{
	    var url = window.parent.contents.location.href;
	    if(url.search(/OA/) != -1)
	    {
	        window.open('DeskTop.aspx?DesktopType=OA' , 'main');
	    }
	    else if(url.search(/Project/) != -1)
	    { 
		    window.open('DeskTop.aspx?DesktopType=PM' , 'main');   
		}
		else
		{
		    window.open('DeskTop.aspx?DesktopType=OA' , 'main');
		}
		window.open('Footer.aspx?UserName=' + escape('<%=user.UserName %>') , 'footer');
	}

    </script>

    <span id="spanscript" runat="server"></span>
</body>
</html>
