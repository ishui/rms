<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.Migrated_InputUser" CodeFile="InputUser.ascx.cs" %>
<SCRIPT language="javascript" src="<%=imagePath%>XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>../UserControls/InputUser.js"></SCRIPT>
<input class="input" id="txtInput" type="text" size="12" name="txtInput" onfocus="InputUser_CodeFocus(this, this.ClientID);"
	onblur="InputUser_CodeBlur(this, this.ClientID, this.ImagePath);" onkeydown="if(event.keyCode==13){InputUser_CodeBlur(this, this.ClientID, this.ImagePath);}" runat="server" title="输入工号、登录名或姓名后按回车"> <A href="#" onclick="InputUser_SelectUser('<%=ClientID%>','<%=imagePath%>');return false;">
	<IMG src="<%=imagePath%>ToolsItemSearch.gif" border="0"></A>&nbsp;<span id="divHint" style="COLOR:red" runat="server"></span>
<input id="txtOldValue" type="hidden" name="txtOldValue" runat="server"> <input id="txtHint" type="hidden" name="txtHint" runat="server">
<input id="txtCode" type="hidden" name="txtCode" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server">
 <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
