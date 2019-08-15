<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputContract" CodeFile="InputContract.ascx.cs" %>
<SCRIPT language="javascript" src="<%=imagePath%>XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="../UserControls/InputContract.js"></SCRIPT>
<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
<A href="#" onclick="InputContract_SelectContract('<%=ClientID%>','');return false;"><IMG src="<%=imagePath%>ToolsItemSearch.gif" border="0"></A>
<span id="divHint" style="COLOR:black; width:180px" runat="server">гКя║╨ом╛</span>
<input id="txtCode" type="hidden" name="txtCode" runat="server" style="width: 10px">
<input id="txtName" type="hidden" name="txtName" runat="server" style="width: 9px">
<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server" style="width: 8px">

