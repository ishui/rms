<%@ Page language="c#" Inherits="RmsPM.Web.Systems.ExecuteSql" CodeFile="ExecuteSql.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ExecuteSql</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" border="1" width="100%">
					<TR>
						<TD><TEXTAREA style="WIDTH: 968px; HEIGHT: 400px" rows="12" cols="118" runat="server" id="textareasql"></TEXTAREA></TD>
					</TR>
					<tr>
					    <td>ProcedureCode:&nbsp;&nbsp;<input id="TxtProcedure" runat="server" type="text" />&nbsp;&nbsp;&nbsp;&nbsp;CaseCode:&nbsp;&nbsp;<input id="TxtCaseCode" runat="server" type="text" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
					</tr>
					<TR>
						<TD runat="server" id="msgtd">
							<asp:Button id="Button1" runat="server" Text="执行sql语句" Width="80px" onclick="Button1_Click"></asp:Button> 
							<asp:Button id="Button2" runat="server" Text="生成删除流程数据 sql 语句" Width="150px" OnClick="Button2_Click"></asp:Button>
							<asp:Button id="Button3" runat="server" Text="检查路由条件"  OnClick="Button3_Click"></asp:Button>
							</TD>
					</TR>
				</TABLE>
			</FONT>
			<asp:DataGrid id="DataGrid1" runat="server"></asp:DataGrid>
		</form>
	</body>
</HTML>
