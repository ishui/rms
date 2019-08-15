<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputUsers" CodeFile="InputUsers.ascx.cs" %>
<SCRIPT language="javascript">
	function SelectPerson(type)
　　{
		var UserCodes = document.all("<% =txtUsers.ClientID %>").value;
		var StationCodes = document.all("<% =txtUserStations.ClientID %>").value;
		
　　	if(type==1)
　　	{
　　		OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes=" + UserCodes + "&StationCodes="+ StationCodes +"&Flag="+type,null);
　　	}
　　}

	function getReturnStationUser( userCodes,userNames,stationCodes,stationNames,flag)
	{
		if ( flag == 1)
		{
			document.all("<% =txtUsers.ClientID %>").value = userCodes;
			document.all("<% =txtUserStations.ClientID %>").value = stationCodes;	
			document.all("<% =SelectName.ClientID %>").innerText = getString(userNames,stationNames);
			document.all("<% =hSelect.ClientID %>").value = getString(userNames,stationNames);
		}
	}
	　　
	function getString(str1,str2)
	{
		if(str1.length>0&&str2.length>0)
		{
			return str1+','+str2;
		}
		else
			return str1+str2;
	}
</SCRIPT>
<input id="txtUsers" type="hidden" name="txtUsers" runat="server"> <input id="txtUserStations" type="hidden" name="txtUserStations" runat="server">
<input id="hSelect" type="hidden" name="hSelect" runat="server">
<table cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td id="SelectName" runat="server">&nbsp;</td>
		<td width="50">
			<input class="button-small" id="btnSelectUsers" runat="server" onclick="SelectPerson(1);return false;"
				type="button" value="僉夲喘薩" NAME="btnSelectUsers">
		</td>
		<td width="10" id="td_MustInput" runat="server">
			<font color="red">*</font>
		</td>
	</tr>
</table>
