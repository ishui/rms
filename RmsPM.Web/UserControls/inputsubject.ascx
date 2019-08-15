<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputSubject" CodeFile="InputSubject.ascx.cs" %>
<SCRIPT language="javascript" src="<%=imagePath%>images/XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>Finance/Subject.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>UserControls/InputSubject.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>Rms.js"></SCRIPT>
<input class="input" id="txtCode" type="text" size="20" name="txtCode" onfocus="InputSubject_CodeFocus(this, this.ClientID);"
	onblur="InputSubject_CodeBlur(this, this.ClientID);" onkeydown="if(event.keyCode==13) InputSubject_CodeBlur(this, this.ClientID);" runat="server" title="输入科目代码后按回车"> <A href="#" onclick="InputSubject_SelectSubject('<%=ProjectControl%>','<%=imagePath%>','<%=ClientID%>');return false;">
	<IMG src="<%=imagePath%>images/ToolsItemSearch.gif" border="0"></A>&nbsp;<span id="divName" runat="server"></span>
&nbsp;<span id="divHint" style="COLOR:red" runat="server"></span> <input id="txtOldValue" type="hidden" name="txtOldValue" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server"> <input id="txtHint" type="hidden" name="txtHint" runat="server">
<input id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
<script language=javascript>
	// 当用户变化时
	function <%=ID%>ProjectCodeOnChange(objValue)
	{
		document.all.<%=ClientID%>_txtSubjectSetCode.value=objValue;	
	}
</script>