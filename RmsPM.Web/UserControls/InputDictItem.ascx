<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InputDictItem.ascx.cs" Inherits="RmsPM.Web.UserControls.UserControls_InputDictItem" %>
<Script language="javascript">

function InputDictItem_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}
//ѡ���ֵ�
function SelectDict(ClientID)
{
    var txtDictName = InputDictItem_GetControl(ClientID, "txtDictName");
    //var flag = InputDictItem_GetControl(ClientID, "txtSearchPlace");
    var flag = ClientID + "_" +"txtSearchPlace";
    var DictName = txtDictName.value;
    
    //alert(ClientID);
    if(DictName=="")
    {
        alert("û������");
        return;
    }
	OpenCustomWindow("../SelectBox/SelectDictItem.aspx?DictionaryName=" + escape(DictName) + "&type=single&flag=" + flag, "ѡ���ֵ�" + DictName, 500, 560);
	
	
}

//ѡ���ֵ䷵��
function SelectDictItemReturn(code, name, flag)
{
    
	document.all(flag).value = name;
}
</Script>


<input class="input" id="txtSearchPlace" type="text" size="10" name="txtSearchPlace"
    runat="server"> <a href="#" onclick="SelectDict('<%=ClientID%>')">
    <img src="../images/ToolsItemSearch.gif" border="0"></a>

<input id="txtDictName" type="hidden" name="txtDictName" runat="server">
        