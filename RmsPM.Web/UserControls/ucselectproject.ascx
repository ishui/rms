<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.UCSelectProject" CodeFile="UCSelectProject.ascx.cs" %>
<FONT face="ËÎÌו">
	<asp:DropDownList id="ddlSelProject" runat="server" onselectedindexchanged="ddlSelProject_SelectedIndexChanged"></asp:DropDownList></FONT>
<script>
	function <%=ClientID%>ChangeProjectEvent(objValue)
	{
		<%=changeTarget%>		
	}
 </script>