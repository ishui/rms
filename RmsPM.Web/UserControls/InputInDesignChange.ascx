<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InputInDesignChange.ascx.cs"
    Inherits="RmsPM.Web.UserControls.UserControls_InputInDesignChange" %>
    <SCRIPT language="javascript" src="../Images/XmlCom.js"></SCRIPT>
<script language="javascript" type="text/javascript">
function SelectInDesignChange(ClientID, ProjectCode)
{
    OpenLargeWindow("../SelectBox/selectindesignchange.aspx?ReturnFunc=selectindesignchangeReturn&Flag=" + ClientID+"&ProjectCode="+ProjectCode,'选择内部变更');
}
function GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}
function selectindesignchangeReturn( Code,Type, Name, ClientID)
{
    var hideValue = GetControl(ClientID, "hideValue");
    var divName = GetControl(ClientID,"divName");
    
    //alert("212121");
    hideValue.value = Code;
    divName.innerText = Name;
}

</script>
    
    
<span id="divName" runat="server"></span>
<a href="#" onclick="SelectInDesignChange('<%=ClientID%>',<%=Request["ProjectCode"]%>);return false;">
    <img src="../Images/ToolsItemSearch.gif" border="0"></a>

<input type="hidden" name="hideValue" id="hideValue" runat="server"/>