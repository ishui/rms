<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.UCSelectContract" CodeFile="UCSelectContract.ascx.cs" %>
<input class="input" id="txtInput" type="text" size="12" name="txtInput" disabled runat="server">
<A href="#" onclick="OpenSelectContract();return false;"><IMG src="<%=imagePath%>ToolsItemSearch.gif" border="0"></A>
<input type="hidden" id="txtInputHidden" runat="server">
<input type="hidden" id="txtProjectHidden" NAME="txtProjectHidden">
<script>
	function SelectContract(code,name)
	{
		if(code.indexOf(',')<0)
		{
			document.all.<%=txtInputHidden.ClientID%>.value=code;
			document.all.<%=txtInput.ClientID%>.value=name;
		}
		else
		{
			document.all.<%=txtInputHidden.ClientID%>.value=code.substring(0,code.indexOf(','));
			document.all.<%=txtInput.ClientID%>.value=name.substring(0,name.indexOf(','));;
		}
	}
	function OpenSelectContract()
	{
		var tmp = document.all.txtProjectHidden.value;
		if(tmp.length<1)
		{
			alert('请选择工程项目');
			return ;
		}
		OpenMiddleWindow( '<%=imagePath%>../SelectBox/SelectContract.aspx?Flag=CodeAndName&ProjectCode='+document.all.txtProjectHidden.value,'选择合同' );
	}
	function <%=ID%>ProjectCodeOnChange(projectCode)
	{
		document.all.txtProjectHidden.value=projectCode;
	}

</script>
