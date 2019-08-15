<%@ Control Language="C#" AutoEventWireup="true" CodeFile="workflowselect.ascx.cs"
    Inherits="RmsPM.Web.WorkFlowControl.Workflowselect" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>

<script language="javascript" src="../Images/XmlCom.js"></script>

<script language="javascript">
function SelectFlow(ClientID)
{ 
	OpenFullWindow("../SelectBox/SelectFlow.aspx?ReturnFunc=InputFlow_SelectFlowReturn&Flag=" + ClientID,'选择流程');
}

function Input_GetControl(ClientID, objName)
{
	var obj = document.all(ClientID + ":" + objName);
	if (!obj)
		obj = document.all(ClientID + "_" + objName);
		
	return obj
}
function InputFlow_SelectFlowReturn(CaseCode,ActCode,ApplicationPath,ApplicationCode,Status,Name,ClientID)
{

	var divName = Input_GetControl(ClientID, "divName");

	var divValue = Input_GetControl(ClientID, "divValue");

   var  ReferLink = CaseCode+","+ActCode+","+ApplicationPath+","+ApplicationCode+","+Status+";";
   var Link = CaseCode+","+ActCode+","+ApplicationPath+","+ApplicationCode+","+Status;

   divName.innerHTML += "<a href=\"#\" id=\"btnDelete\" onclick=\"if(confirm('是否移除选中流程信息？'))PrebtnDelete('" + Link + "','" + ClientID + "');else return false;__doPostBack('" + ClientID.replace("_", "$") + "$LinkButton1','')\"  name=\"btnDelete\" >" + Name + "</a>";

   divValue.value+=ReferLink;

 
}
function PrebtnDelete(DelLink,ClientID)
{

   var divSingleValue = Input_GetControl(ClientID, "divSingleValue");
   divSingleValue.value = DelLink;

  
}



</script>

<a href="#" onclick="SelectFlow('<%=ClientID%>');return false;">
    <img src="../Images/ToolsItemSearch.gif" border="0"></a> <span id="divName" runat="server">
    </span>
<input id="divValue" type="hidden" runat="server" />
<input id="divSingleValue" type="hidden" runat="server" />
<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="display: none"></asp:LinkButton>
