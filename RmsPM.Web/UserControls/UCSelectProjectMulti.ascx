<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.UCSelectProjectMulti" CodeFile="UCSelectProjectMulti.ascx.cs" %>
<SCRIPT language="javascript" src="../UserControls/UCSelectProjectMulti.js" charset="gb2312"></SCRIPT>
<span id="divName" runat="server"></span>
<A href="#" onclick="UCSelectProjectMulti_Select('<%=ClientID%>');return false;"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
<input id="txtCode" type="hidden" name="txtCode" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server">
<input id="txtAccess" type="hidden" name="txtAccess" value="CanAccess" runat="server">
