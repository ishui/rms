<%@ Page language="c#" Inherits="RmsPM.Web.Sal.SalContractView" CodeFile="SalContractView.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>销售合同信息</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="10" topMargin="10" scroll="no">
		<form id="Form1" method="post" runat="server">
		    <div style="overflow:auto;position:absolute;width:100%;height:100%">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">销售合同信息</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD class="form-item" width="13%">合同编号：</TD>
								<TD width="20%"><asp:label id="lbContractID" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%">客户姓名：</TD>
								<TD width="20%"><asp:label id="lbClientName" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%">签约日期：</TD>
								<TD width="20%"><asp:label id="lbContractDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">门牌号：</TD>
								<TD colspan="3"><asp:label id="lbChamberName" runat="server"></asp:label></TD>
								<TD class="form-item">室号：</TD>
								<TD><asp:label id="lbRoom" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">建筑面积：</TD>
								<TD><asp:label id="lbBuildDim" runat="server"></asp:label>&nbsp;平方米</TD>
								<TD class="form-item">套内面积：</TD>
								<TD><asp:label id="lbRoomDim" runat="server"></asp:label>&nbsp;平方米</TD>
								<TD class="form-item">拨房单号：</TD>
								<TD><asp:label id="lbBofangCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">单价：</TD>
								<TD><asp:label id="lbUnitPrice" runat="server"></asp:label>&nbsp;元</TD>
								<TD class="form-item">总价：</TD>
								<TD><asp:label id="lbTotalPrice" runat="server"></asp:label>&nbsp;元</TD>
								<TD class="form-item">补差后总价：</TD>
								<TD><asp:label id="lbFactPrice" runat="server"></asp:label>&nbsp;元</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr valign="top">
					<td class="topic" vAlign="top">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr> 
							<td width="200" class="intopic">应收款</td>
							</tr>
						</table>
						<asp:datagrid id="dgSalPayPlan" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
							PageSize="12" AllowPaging="False" GridLines="Horizontal" CellPadding="0" CssClass="list"
							ShowFooter="True">
							<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
							<ItemStyle CssClass=""></ItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="ItemName" HeaderText="款项名称" FooterText="合计">
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Prompt" HeaderText="计划收款日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="PlanMoney" HeaderText="计划收款金额" DataFormatString="{0:n}">
									<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PayMoney" HeaderText="已收金额" DataFormatString="{0:n}">
									<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
				<tr valign="top">
					<td class="topic" vAlign="top">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr> 
							<td width="200" class="intopic">实收款</td>
							</tr>
						</table>
						<asp:datagrid id="dgSalPay" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
							PageSize="12" AllowPaging="False" GridLines="Horizontal" CellPadding="0" CssClass="list"
							ShowFooter="True">
							<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
							<ItemStyle CssClass=""></ItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="PayDate" HeaderText="收款日期" FooterText="合计" DataFormatString="{0:yyyy-MM-dd}">
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PayType" HeaderText="收款方式"></asp:BoundColumn>
								<asp:BoundColumn DataField="PayMoney" HeaderText="收款金额" DataFormatString="{0:n}">
									<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Remark" HeaderText="票据号"></asp:BoundColumn>
								<asp:BoundColumn DataField="CheckDate" HeaderText="审核日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="CheckMan" HeaderText="审核人"></asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="关 闭" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
		    </div>
			<input type="hidden" id="txtContractCode" name="txtContractCode" runat="server">
		</form>
	</body>
</HTML>
