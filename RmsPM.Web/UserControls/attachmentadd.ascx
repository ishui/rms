<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.AttachMentAdd" CodeFile="AttachMentAdd.ascx.cs" %>
<LINK href="<%=ctrlPath%>/../Images/index.css" type="text/css" rel="stylesheet">
<script language="javascript" src="<%=ctrlPath%>../UserControls/ContentMenu.js"></script>
<SCRIPT language="javascript" src="<%=ctrlPath%>../Rms.js"></SCRIPT>
<script language="javascript">
/*
	function SetTmpFileName()
	{
		filename = GetLastRight(document.all.<%=FileNameClientID%>.value, "\\");
		document.all.<%=tmpFileNameClientID%>.value = filename;
	}
	
	function GetLastRight(str, val) {
		var i = str.lastIndexOf(val);
		if (i > 0)
			return str.substring(i + 1);
		else
			return str;
	}
*/

	document.write("<div onmouseout=\"ContentMenuPanelOnMouseOut(this);\" id=\"<%=ClientID%>ContentMenuDiv\" style=\"position:absolute;display:none; z-index:10;background-color:#333333\"><iframe id=\"<%=ClientID%>ContentMenuIFrame\" frameborder=\"0\" src=\"about:blank\"></iframe></div>");
	var <%=ClientID%>_ContentMenuDiv=eval("<%=ClientID%>ContentMenuDiv");
	var <%=ClientID%>_ContentMenuIFrames=document.all("<%=ClientID%>ContentMenuIFrame");
	var <%=ClientID%>_ContentMenuIFrame=eval("<%=ClientID%>ContentMenuIFrame");


	function <%=ClientID%>ShowEditMenu(obj,code)
	{
		var cssFile="<%=ctrlPath%>../Images/ContentMenu.css";	
		var strUrl1 = "<%=ctrlPath%>../Project/WBSAttachMentView.aspx?Action=View&AttachMentCode="+code+"&ProjectCode=<%=Request["ProjectCode"]%>";
		var strUrl2 = "<%=ctrlPath%>../Project/WBSAttachMentView.aspx?Action=Del&AttachMentCode="+code+"&ProjectCode=<%=Request["ProjectCode"]%>";
		var Items = new Array(2);
		Items[0] = new Array(3);
		Items[0][0] = "查看";
		Items[0][1] = "";
		Items[0][2] = "OpenCustomWindow('" + strUrl1 + "',null,-10000,-10000);";
		Items[1] = new Array(3);
		Items[1][0] = "删除";
		Items[1][1] = "";
		Items[1][2] = "OpenCustomWindow('" + strUrl2 + "',null,-10000,-10000);";	
		CreateContentMenu('<%=ClientID%>',Items,cssFile,event.x-1,event.y-1);
	}
	function AttachMentRefresh()
	{
		window.document.forms[0].submit();
	}
	
	function <%=ClientID%>DoAdd()
	{
		OpenCustomWindow("<%=ctrlPath%>SaveAttach.aspx?strMasterCode=<%=MasterCode%>&strAttachMentType=<%=(ViewState["TempTypeHead"]+AttachMentType)%>&ProjectCode=<%=Request["ProjectCode"]%>",null,400,300);
	}
	
	function Refresh()
	{
		window.document.forms[0].submit();
	}
</script>
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td width="10%">
			<input id="btSavePage" type="button" NAME="btSavePage" class="button-small" onclick="<%=ClientID%>DoAdd();"
				value="上传附件">
		</td>
		<TD id="tdDeleteList" runat="server"></TD>
	</tr>
</TABLE>
