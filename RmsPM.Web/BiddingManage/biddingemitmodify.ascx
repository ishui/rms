<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingEmitModify" CodeFile="BiddingEmitModify.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<div id="OperableDiv" runat="server">
	<table class="form" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
		<tr>
			<td class="form-item">编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：</td>
			<td><INPUT id="txtEmitNumber" type="text" name="txtEmitNumber" runat="server" class="input-nember"
					onblur="BiddingEmitCheckMoney(this);" style="WIDTH: 104px" size="12" readOnly><FONT face="宋体">&nbsp;<FONT color="#cc0066">*</FONT></FONT></td>
			<td class="form-item">发标日期：</td>
			<td>
				<cc1:calendar id="txtEmitDate" runat="server" CalendarResource="../Images/CalendarResource/" CalendarMode="All"></cc1:calendar></td>
		</tr>
		<tr>
			<td class="form-item">截标日期：</td>
			<td>
				<cc1:calendar id="txtEndDate" runat="server" CalendarResource="../Images/CalendarResource/" CalendarMode="All"></cc1:calendar><FONT face="宋体"></FONT></td>
			<td class="form-item">开标日期：</td>
			<td>
				<cc1:calendar id="txtPrejudicationDate" runat="server" CalendarResource="../Images/CalendarResource/"
					CalendarMode="All"></cc1:calendar></td>
		</tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<table class="form" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
		<tr>
			<td class="form-item">编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：</td>
			<td runat="server" id="tdEmitNumber"></td>
			<td class="form-item">发标日期：</td>
			<td runat="server" id="tdEmitDate"></td>
		</tr>
		<tr>
			<td class="form-item">截标日期：</td>
			<td runat="server" id="tdEndDate"></td>
			<td class="form-item">开标日期：</td>
			<td runat="server" id="tdPrejudicationDate"></td>
		</tr>
	</table>
</div>
<script language="javascript">
function BiddingEmitCheckSubmit()
{
	if(document.all("<%=ClientID%>_txtEmitNumber").value == "")
	{
		alert('编号必须填写');
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
			alert("编号请输入数字");
			obj.select();
			return false;
		}
	}
	return true;*/				
}
</script>
