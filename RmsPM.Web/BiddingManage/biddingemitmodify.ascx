<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingEmitModify" CodeFile="BiddingEmitModify.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<div id="OperableDiv" runat="server">
	<table class="form" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
		<tr>
			<td class="form-item">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ţ�</td>
			<td><INPUT id="txtEmitNumber" type="text" name="txtEmitNumber" runat="server" class="input-nember"
					onblur="BiddingEmitCheckMoney(this);" style="WIDTH: 104px" size="12" readOnly><FONT face="����">&nbsp;<FONT color="#cc0066">*</FONT></FONT></td>
			<td class="form-item">�������ڣ�</td>
			<td>
				<cc1:calendar id="txtEmitDate" runat="server" CalendarResource="../Images/CalendarResource/" CalendarMode="All"></cc1:calendar></td>
		</tr>
		<tr>
			<td class="form-item">�ر����ڣ�</td>
			<td>
				<cc1:calendar id="txtEndDate" runat="server" CalendarResource="../Images/CalendarResource/" CalendarMode="All"></cc1:calendar><FONT face="����"></FONT></td>
			<td class="form-item">�������ڣ�</td>
			<td>
				<cc1:calendar id="txtPrejudicationDate" runat="server" CalendarResource="../Images/CalendarResource/"
					CalendarMode="All"></cc1:calendar></td>
		</tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<table class="form" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
		<tr>
			<td class="form-item">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ţ�</td>
			<td runat="server" id="tdEmitNumber"></td>
			<td class="form-item">�������ڣ�</td>
			<td runat="server" id="tdEmitDate"></td>
		</tr>
		<tr>
			<td class="form-item">�ر����ڣ�</td>
			<td runat="server" id="tdEndDate"></td>
			<td class="form-item">�������ڣ�</td>
			<td runat="server" id="tdPrejudicationDate"></td>
		</tr>
	</table>
</div>
<script language="javascript">
function BiddingEmitCheckSubmit()
{
	if(document.all("<%=ClientID%>_txtEmitNumber").value == "")
	{
		alert('��ű�����д');
        return false;
	}
	return true;
}
function BiddingEmitCheckMoney(obj)
{
	/*if(obj.value.length>0)
	{
		if(obj.value.match("^[0-9]+(\.[0-9]+)?$")==null)
		{
			obj.select();
			obj.focus();
			alert("�������������");
			obj.select();
			return false;
		}
	}
	return true;*/				
}
</script>
