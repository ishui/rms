<%@ Page language="c#" Inherits="RmsPM.Web.PBS.RoomInfo" CodeFile="RoomInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������Ϣ</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">������Ϣ</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="100" class="form-item">¥����</TD>
								<TD><asp:Label Runat="server" ID="lblBuildingName"></asp:Label></TD>
								<TD width="100" class="form-item">���ƺţ�</TD>
								<TD><asp:Label Runat="server" ID="lblChamberName"></asp:Label></TD>
								<TD width="100" class="form-item">�Һţ�</TD>
								<TD><asp:Label Runat="server" ID="lblRoomName"></asp:Label></TD>
							</TR>
							<tr>
								<td class="form-item">���ͣ�</td>
								<td><asp:Label Runat="server" ID="lblModelName"></asp:Label></td>
								<TD class="form-item">Ԥ�⽨�棺</TD>
								<TD><asp:Label Runat="server" ID="lblPreBuildArea"></asp:Label></TD>
								<TD class="form-item">Ԥ�����棺</TD>
								<TD><asp:Label Runat="server" ID="lblPreRoomArea"></asp:Label></TD>
							</tr>
							<tr>
								<td class="form-item">��Ʒ���ͣ�</td>
								<td><asp:Label Runat="server" ID="lblPBSTypeName"></asp:Label></td>
								<TD class="form-item">ʵ�⽨�棺</TD>
								<TD><asp:Label Runat="server" ID="lblBuildArea"></asp:Label></TD>
								<TD class="form-item">ʵ�����棺</TD>
								<TD><asp:Label Runat="server" ID="lblRoomArea"></asp:Label></TD>
							</tr>
							<tr>
								<TD class="form-item">���״̬��</TD>
								<TD><asp:Label Runat="server" ID="lblInvState"></asp:Label></TD>
								<TD class="form-item">������ڣ�</TD>
								<TD><asp:Label Runat="server" ID="lblInDate"></asp:Label></TD>
								<TD class="form-item">�������ڣ�</TD>
								<TD><asp:Label Runat="server" ID="lblOutDate"></asp:Label></TD>
							</tr>
							<tr>
								<TD class="form-item">����״̬��</TD>
								<TD><asp:Label Runat="server" ID="lblOutState"></asp:Label></TD>
								<TD class="form-item">ȥ��</TD>
								<TD colspan="3"><asp:Label Runat="server" ID="lblOutAspect"></asp:Label></TD>
							</tr>
							<tr>
								<TD class="form-item">����״̬��</TD>
								<TD><asp:Label Runat="server" ID="lblSalState"></asp:Label></TD>
								<TD class="form-item">���ۺ�ͬ�ţ�</TD>
								<TD><a href="#" onclick="ViewSalContract();return false;"><asp:Label Runat="server" ID="lblContractID"></asp:Label></a></TD>
								<TD class="form-item">�������룺</TD>
								<TD><asp:Label Runat="server" ID="lblTotalPayMoney"></asp:Label></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic" width="200">�������ϸ</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<asp:datagrid id="dgList" runat="server" ShowFooter="False" PageSize="15" AutoGenerateColumns="False"
								AllowSorting="True" CellPadding="0" CssClass="list" Width="100%" AllowPaging="False">
								<ItemStyle CssClass=""></ItemStyle>
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="����">
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.Out_Date", "{0:yyyy-MM-dd}") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="����">
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.Out_State") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="���ݱ��" FooterText="�ϼ�">
										<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<a href="#" onclick="OpenRoomIO(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.OutListCode") %>'><%# DataBinder.Eval(Container, "DataItem.OutListName") %></a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="״̬">
										<ItemTemplate>
											<%# RmsPM.BLL.ProductRule.GetTempRoomOutCheckStateName(DataBinder.Eval(Container, "DataItem.CheckState")) %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="ȥ��">
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.OutAspect") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Э���ĺ�">
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.ConferMark") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="������">
										<ItemTemplate>
											<%# RmsPM.BLL.SystemRule.GetUserName( RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.UserCode")) ) %>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnClose" name="btnClose" type="button" class="submit" value="�� ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtRoomCode" name="txtRoomCode" runat="server"><input type="hidden" id="txtModelCode" name="txtModelCode" runat="server">
			<input type="hidden" id="txtContractCode" name="txtContractCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

//�鿴����
function OpenRoomIO(code)
{
	OpenCustomWindow("RoomIOInfo.aspx?FromUrl=" + escape(window.location) + "&OutListCode=" + code, "", 760, 540);
}

//�鿴���ۺ�ͬ
function ViewSalContract()
{
	OpenCustomWindow("../Sal/SalContractView.aspx?Action=view&FromUrl=" + escape(window.location.href) + "&ContractCode=" + Form1.txtContractCode.value, "��ͬ��ϸ", 650, 560);
}
	
//-->
		</SCRIPT>
	</body>
</HTML>
