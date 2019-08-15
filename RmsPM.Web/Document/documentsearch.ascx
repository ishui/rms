<%@ Control Language="c#" Inherits="RmsPM.Web.Document.DocumentSearch" CodeFile="DocumentSearch.ascx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td>���⣺</td>
		<td><INPUT class="input" id="txtSearchTitle" type="text" size="20" name="txtSearchTitle" runat="server"></td>
		<td>��ţ�</td>
		<td><INPUT class="input" id="txtSearchDocumentID" type="text" size="16" name="txtSearchDocumentID"
				runat="server"></td>
		<td>���ߣ�</td>
		<td><INPUT class="input" id="txtSearchAuthor" type="text" size="16" name="txtSearchAuthor"
				runat="server"></td>
	</tr>
	<tr>
		<TD>�����ˣ�</TD>
		<TD><uc1:InputUser id="ucCreatePerson" runat="server"></uc1:InputUser></TD>
		<td>�������ڣ�</td>
		<TD><cc3:calendar id="dtCreateDate_begin" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
		<td>����</td>
		<TD><cc3:calendar id="dtCreateDate_end" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
	</tr>
	<tr>
		<TD>�޸��ˣ�</TD>
		<TD><uc1:InputUser id="ucModifyPerson" runat="server"></uc1:InputUser></TD>
		<td>�޸����ڣ�</td>
		<TD><cc3:calendar id="dtModifyDate_begin" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
		<td>����</td>
		<TD><cc3:calendar id="dtModifyDate_end" runat="server" CalendarResource="../Images/CalendarResource/"
				ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
	</tr>
	<tr>
		<td>������ݣ�</td>
		<td colspan="5"><SELECT class="select" id="sltFixedType" name="sltFixedType" runat="server">
				<OPTION value="" selected>------��ѡ��------</OPTION>
			</SELECT>
			<INPUT class="input" id="txtCode" type="text" size="30" name="txtCode" runat="server"></td>
	</tr>
</table>
<INPUT id="txtHasIniPage" type="hidden" name="txtHasIniPage" runat="server">
<script language="javascript">
<!--

//-->
</script>
