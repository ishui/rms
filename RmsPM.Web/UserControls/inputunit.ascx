<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputUnit" CodeFile="InputUnit.ascx.cs" %>
<SCRIPT language="javascript" src="<%=imagePath%>XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>../UserControls/InputUnit.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>../Rms.js"></SCRIPT>
<input class="input" id="txtInput" type="text" name="txtInput" onfocus="InputUnit_CodeFocus(this, this.ClientID);"
	onblur="InputUnit_CodeBlur(this, this.ClientID);" onkeydown="if(event.keyCode==13){InputUnit_CodeBlur(this, this.ClientID);}" runat="server" title="输入部门编号或名称后按回车" style="width: 98px"> <A href="#" onclick="InputUnit_SelectUnit('<%=ClientID%>','<%=imagePath%>');return false;">
	<IMG src="<%=imagePath%>ToolsItemSearch.gif" border="0"></A>&nbsp;<span id="divHint" style="COLOR:red" runat="server"></span>
<input id="txtOldValue" type="hidden" name="txtOldValue" runat="server"> <input id="txtHint" type="hidden" name="txtHint" runat="server">
<input id="txtCode" type="hidden" name="txtCode" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server">
 <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
