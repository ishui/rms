<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractID.aspx.cs" Inherits="Contract_ContractID" %>
<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>合同编号</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../Images/index.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="../Rms.js"></script>
	</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">合同编号</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="20%">合同名称：</TD>
								<TD><asp:label id="lblContractName" runat="server"></asp:label></TD>
								<TD class="form-item" width="20%">合同编号：</TD>
								<TD>
								    <input class="input" id="txtContractID" type="text" name="txtContractID" runat="server">&nbsp;&nbsp;&nbsp;
									<asp:label id="lblContractStatus" runat="server" ForeColor="red"></asp:label><font color="red">*</font>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table id="tableList" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<tr>
									<td align="center">
									    <br /><br />
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td class="intopic" width="200">合同款项明细</td>
												<td></td>
											</tr>
										</table>
                                        <asp:DataGrid ID="dgCostList" Runat="server" CssClass="list" Width="100%" CellPadding="0" GridLines="Horizontal"
											AllowPaging="false" PageSize="15" AutoGenerateColumns="False" AllowSorting="True" ShowFooter="True">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<ItemStyle CssClass="list-i"></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="序号">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项" Visible="False">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<uc1:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server" Visable="False"></uc1:InputCostBudgetDtl>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="单位工程">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblPBSName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="费用项">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblCostName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="币种金额">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<uc1:ExchangeRate id="ucExchangeRate" runat="server"></uc1:ExchangeRate>
													</ItemTemplate>
													<FooterTemplate>
														合计金额（RMB）：
														<asp:Label ID="lblSumCostMoney" Runat="server"></asp:Label>&nbsp;&nbsp;
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="已批/已批%（RMB）" Visible="false">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:Label ID="lblAHMoney" Runat="server"></asp:Label>&nbsp;/
														<asp:Label ID="lblAHMoneyPer" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label ID="lblSumAHMoney" Runat="server"></asp:Label>&nbsp;/
														<asp:Label ID="lblSumAHMoneyPer" Runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="已付（RMB）" Visible="false">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:Label ID="lblAPMoney" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label ID="lblSumAPMoney" Runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="未付（RMB）" Visible="false">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:Label ID="lblUPMoney" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label ID="lblSumUPMoney" Runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="说明" Visible="False">
													<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblDescription" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:DataGrid>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="9" width="100%">
							<tr>
								<td align="center">
								    <input class="submit" id="btnSave" type="button" value="保 存" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"/>&nbsp;
								    <input class="submit" id="btnClose" onclick="javascript:self.close()" type="button" value="关 闭" name="btnCancel" runat="server" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>

	</body>
</html>
