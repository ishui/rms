<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputStationUser" CodeFile="InputStationUser.ascx.cs" %>
<SCRIPT language="javascript" src="<%=imagePath%>../UserControls/InputStationUser.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>../Rms.js"></SCRIPT>
<span id="divSelect" name="divSelect" runat="server">
<A href="#" onclick="InputStationUser_Select('<%=ClientID%>','<%=imagePath%>');return false;">
	<IMG src="<%=imagePath%>ToolsItemSearch.gif" border="0"></A></span>
<span id="divName" runat="server"></span>
<span id="divHint" style="COLOR:red" runat="server">
</span><input id="txtUserCodes" type="hidden" name="txtUserCodes" runat="server">
<input id="txtStationCodes" type="hidden" name="txtStationCodes" runat="server">
<input id="txtUserNames" type="hidden" name="txtUserNames" runat="server"> <input id="txtStationNames" type="hidden" name="txtStationNames" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server"> <input id="txtHint" type="hidden" name="txtHint" runat="server">
<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
<input type="checkbox" runat="server" id="chkReadonly" visible="false" />
<SCRIPT language="javascript">
<!--

function InputStationUser_MyOnClientPost()
{
	if (!<%=MyOnClientPost%>) return false;
	
	return true;
}

//-->
</SCRIPT>
