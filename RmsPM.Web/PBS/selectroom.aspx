<%@ Reference Control="~/pbs/searchroom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchRoom" Src="SearchRoom.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.SelectRoom" CodeFile="SelectRoom.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ��Դ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table id="tbMain" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
				bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ��Դ</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table cellpadding="0" cellspacing="0" border="0" class="search-area" width="100%">
							<tr>
								<td>
									<table>
										<tr>
											<td width="100%"><uc1:SearchRoom id="tbSearchRoom" runat="server"></uc1:SearchRoom></td>
											<td vAlign="top"><input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0" AllowSorting="True"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="True" DataKeyField="RoomCode">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.RoomCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="¥������" FooterText="�ϼ�">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="OpenBuildingInfo(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>'><%# DataBinder.Eval(Container, "DataItem.BuildingName") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ChamberName" HeaderText="���ƺ�">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="RoomName" HeaderText="�Һ�">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FloorName" HeaderText="¥��">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="BuildArea" HeaderText="�������(ƽ��)" DataFormatString="{0:0.####}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="InvState" HeaderText="���״̬">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="OutState" HeaderText="����״̬">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="InDate" HeaderText="�������" DataFormatString="{0:yyyy-MM-dd}">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="BofangName" HeaderText="��������">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="OutAspect" HeaderText="ȥ��">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SalState" HeaderText="����״̬">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="����">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="ViewModel(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.ModelCode") %>'><%# DataBinder.Eval(Container, "DataItem.ModelName") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��Ʒ����">
													<ItemTemplate>
														<%# RmsPM.BLL.PBSRule.GetPBSTypeFullName(DataBinder.Eval(Container, "DataItem.PBSTypeCode")) %>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnOK" name="btnOK" type="button" class="submit" value="ȷ ��" onclick="btnOKClick();"
										runat="server"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtSelectRoomCode" name="txtSelectRoomCode" runat="server">
			<input type="hidden" id="txtReturnFunc" name="txtReturnFunc" runat="server"> <input type="hidden" id="txtDefaultInvState" name="txtDefaultInvState" runat="server">
			<input type="hidden" id="txtDefaultPBSTypeCode" name="txtDefaultPBSTypeCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

//ȷ��
function btnOKClick()
{
	var s = ChkGetSelected(document.all.chkSelect);

	if (s == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}

	ReturnToParentWindow(s);
	window.close();
}

//�鿴����
function ViewModel(code)
{
	OpenLargeWindow("RoomModel.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&ModelCode=" + code, "");
}

//-->
		</SCRIPT>
	</body>
</HTML>
