<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputTask" CodeFile="InputTask.ascx.cs" %>
<SCRIPT language="javascript" src="../images/XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="../UserControls/InputTask.js"></SCRIPT>
<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
<input class="input" id="txtInput" type="text" size="12" name="txtInput" onfocus="InputTask_CodeFocus(this, this.ClientID);"
	onblur="InputTask_CodeBlur(this, this.ClientID);" onkeydown="if(event.keyCode==13) InputTask_CodeBlur(this, this.ClientID);"
	runat="server" title="输入工作项编号后按回车"> <A href="#" onclick="InputTask_SelectTask('<%=ClientID%>');return false;">
	<IMG src="../images/ToolsItemSearch.gif" border="0"></A>&nbsp;<span id="divName" runat="server"></span>&nbsp;<span id="divHint" style="COLOR:red" runat="server"></span>
<input id="txtOldValue" type="hidden" name="txtOldValue" runat="server"> <input id="txtHint" type="hidden" name="txtHint" runat="server">
<input id="txtCode" type="hidden" name="txtCode" runat="server"> <input id="txtName" type="hidden" name="txtName" runat="server">
<input id="txtSortID" type="hidden" name="txtSortID" runat="server"> <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">

<script language="javascript">
<!--


function InputTask_OnChange(ClientID)
{
	<%=m_LoadOnChange%>;
}

//-->
</script>
