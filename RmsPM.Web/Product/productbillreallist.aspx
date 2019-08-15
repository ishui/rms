<%@ Page language="c#" Inherits="RmsPM.Web.Product.ProductBillrealList" CodeFile="ProductBillrealList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>房屋调拨通知单</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="3" width="95%" align="center" border="0">
				<tr>
					<td align="center"><font id="fontOutlistName" runat="server" size="6"><strong>房屋调拨通知单</strong>
						</font>
					</td>
				</tr>
				<tr>
					<td><font id="fontOutAspect" runat="server"><strong>仲盛营销部</strong>&nbsp;</font>
					</td>
				</tr>
				<tr>
					<td align="right">编号：<strong>仲盛<font id="fontCodeName" runat="server">住宅&lt;2003&gt;1</strong>
						</FONT>
					</td>
				</tr>
				<tr>
					<td align="right">下列房屋交付给你单位，请及时向有关物业管理部门办理手续。</td>
				</tr>
			</table>
			<table style="BORDER-COLLAPSE: collapse" borderColor="#31659c" cellSpacing="0" cellPadding="3"
				width="95%" align="center" border="1">
				<tr align="center">
					<td>基地</td>
					<td>幢号</td>
					<td>地址</td>
					<td>室号</td>
					<td>面积</td>
				</tr>
				<asp:repeater id="repeat1" runat="server">
					<ItemTemplate>
						<tr>
							<td><%# DataBinder.Eval(Container.DataItem, "ProjectName") %></td>
							<td><%# DataBinder.Eval(Container.DataItem, "BuildingName") %></td>
							<td><%# DataBinder.Eval(Container.DataItem, "ChamberName") %></td>
							<td><%# DataBinder.Eval(Container.DataItem, "RoomName") %></td>
							<td align="right"><%# DataBinder.Eval(Container.DataItem, "BuildArea", "{0:0.00}") %></td>
						</tr>
					</ItemTemplate>
				</asp:repeater>
				<tr>
					<td colSpan="3">合计</td>
					<td id="tdTotalRoomNum" runat="server" align="right">&nbsp;</td>
					<td id="tdTotalRoomBuildIndex" runat="server" align="right">&nbsp;</td>
				</tr>
			</table>
			<table style="BORDER-COLLAPSE: collapse" borderColor="#31659c" cellSpacing="0" cellPadding="3"
				width="95%" align="center" border="1">
				<tr>
					<td width="3%">协<br>
						议<br>
						文<br>
						号</td>
					<td id="tdConferMark" width="37%" runat="server">&nbsp;</td>
					<td width="3%">备<br>
						<br>
						注</td>
					<td id="tdRemark" width="57%" runat="server">&nbsp;</td>
				</tr>
			</table>
			<br>
			<br>
			<table cellSpacing="3" width="95%" align="center" border="0">
				<tr>
					<td align="right">鼎耀test(集团)有限公司</td>
				</tr>
				<tr>
					<td align="right"><font id="fontOutDate" runat="server"><strong>2003/10/3</strong> </font>
						&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
