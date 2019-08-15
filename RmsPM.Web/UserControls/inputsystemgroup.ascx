<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputSystemGroup" CodeFile="InputSystemGroup.ascx.cs" %>
<SCRIPT language="javascript" src="<%=imagePath%>XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>../UserControls/InputSystemGroup.js" charset="gb2312"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>../Rms.js"></SCRIPT>
<input class="input" id="txtInput" type="text" size="12" name="txtInput" onfocus="InputSystemGroup_CodeFocus(this, this.ClientID);"
	onblur="InputSystemGroup_CodeBlur(this, this.ClientID);" onkeydown="if(event.keyCode==13) InputSystemGroup_CodeBlur(this, this.ClientID);"
	runat="server" title="输入编号按回车"> 
<span id="div_SearchButton" runat="server">
    <A href="#" onclick="javascript:InputSystemGroup_Select('<%=ClientID%>','<%=imagePath%>','<%=ProjectCode%>');return false;">
	<IMG src="<%=imagePath%>ToolsItemSearch.gif" border="0"></A></span> 
&nbsp;<span id="divName" runat="server"></span>
&nbsp;<span id="divHint" style="COLOR:red" runat="server"></span> 
<input id="txtOldValue" type="hidden" name="txtOldValue" runat="server">
<input id="txtHint" type="hidden" name="txtHint" runat="server"> 
<input id="txtCode" type="hidden" name="txtCode" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server">
<input id="txtClassCode" type="hidden" name="txtClassCode" runat="server">
<input id="txtSortID" type="hidden" name="txtSortID" runat="server">
<input id="txtFullID" type="hidden" name="txtFullID" runat="server">
<input id="txtSelectAllLeaf" type="hidden" name="txtSelectAllLeaf" runat="server">
<asp:CheckBox ID="hid_Enable" Runat="server" Visible="False" Checked="True"></asp:CheckBox>
<input id="btnChange" name="btnChange" type="button" value="button" runat="server" visible="false" style="display: none" onserverclick="btnChange_ServerClick" />