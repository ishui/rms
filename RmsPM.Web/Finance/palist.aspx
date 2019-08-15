<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PAList" CodeFile="PAList.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�����̯�����б�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">������� 
										- ƾ֤����</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="DoAddNewVoucher()" type="button" value="�� ��"
							name="btnAdd" runat="server"> <input class="button" id="btnDownload" onclick="Download()" type="button" value="�� ��" name="btnDownload"
							runat="server"> <input class="button" id="btnAllowPaging" type="button" value="ȡ����ҳ" name="btnAllowPaging"
							runat="server" onserverclick="btnAllowPaging_ServerClick">
					</td>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table>
										<TR>
											<TD>ƾ֤���ͣ�</TD>
											<TD><SELECT id="sltVoucherType" class="select" name="sltVoucherType" runat="server">
													<OPTION value="" selected>------��ѡ��------</OPTION>
												</SELECT></TD>
											<TD>ƾ֤��ţ�</TD>
											<TD><INPUT class="input" id="txtVoucherID" type="text" name="txtVoucherID" runat="server"></TD>
											<TD>�� �� �ˣ�</TD>
											<TD><uc1:InputUser id="ucAccountant" runat="server"></uc1:InputUser>
											</TD>
											<TD><INPUT class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></TD>
										</TR>
										<TR>
											<TD><FONT face="����">�Ƶ����ڣ�</FONT></TD>
											<TD><cc3:calendar id="dtbMakeDate0" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
											<TD align="center">����</TD>
											<TD>
												<cc3:calendar id="dtbMakeDate1" runat="server" CalendarResource="../Images/CalendarResource/"
													ReadOnly="False" Display="True" Value=""></cc3:calendar>&nbsp;</TD>
											<td>״̬��</td>
											<TD colspan="2"><input id="chkStatus0" type="checkbox" value="1" name="chkStatus0" runat="server">����&nbsp; 
												&nbsp;<input id="chkStatus1" type="checkbox" value="1" name="chkStatus1" runat="server">����&nbsp; 
												&nbsp;<input id="chkStatus2" type="checkbox" value="1" name="chkStatus2" runat="server">�ѵ���&nbsp;
											</TD>
										</TR>
									</table>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td>
										<asp:datagrid id="dgList" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
											PageSize="15" AllowPaging="True" GridLines="Horizontal" CellPadding="0" CssClass="List" ShowFooter="True">
											<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.VoucherCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="ƾ֤���" FooterText="�ϼ�">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="DoViewVoucherInfo(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "VoucherCode") %>'><%#  DataBinder.Eval(Container.DataItem, "VoucherID") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="ƾ֤����">
													<ItemTemplate>
														<%# RmsPM.BLL.VoucherRule.GetVoucherTypeName(DataBinder.Eval(Container, "DataItem.VoucherType").ToString())%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="TotalMoney" HeaderText="���(Ԫ)" DataFormatString="{0:N}">
													<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="AccountantName" HeaderText="�Ƶ���">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="MakeDate" HeaderText="�Ƶ�����" DataFormatString="{0:yyyy-MM-dd}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CheckPersonName" HeaderText="�����">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CheckDate" HeaderText="�������" DataFormatString="{0:yyyy-MM-dd}">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="״̬">
													<ItemTemplate>
														<%# DataBinder.Eval(Container,"DataItem.StatusName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</TABLE>
			<input type="hidden" runat="server" id="txtProjectCode" name="txtProjectCode"><input type="hidden" runat="server" id="txtSelect" name="txtSelect">
			<div style="DISPLAY:none"><input type="button" name="btnDownloadHidden" id="btnDownloadHidden" runat="server"></div>
			<script language="javascript">
<!--
var CurrUrl = window.location.href;

function DoAddNewVoucher()
{
	OpenCustomWindow("../Finance/VoucherModify.aspx?ProjectCode=" + Form1.txtProjectCode.value,"ƾ֤�޸�", 780, 580);
}

function DoViewVoucherInfo(VoucherCode)
{
	window.navigate('../Finance/VoucherInfo.aspx?VoucherCode='+ VoucherCode + "&FromUrl=" + escape(CurrUrl));
}

function ShowAdvanceSearch()
{
	var objDiv = document.all("divAdvanceSearch");
	if ( objDiv.style.display == "none" )
	{
		objDiv.style.display = "";
	}
	else
	{
		objDiv.style.display = "none";
	}
}

//����
function Download()
{
	var s = ChkGetSelected(document.all.chkSelect);

	if (s == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}

	Form1.txtSelect.value = s;

	OpenCustomWindow("../Finance/VoucherFileCheck.aspx?VoucherCode=" + s, "ƾ֤����", 550, 350);

//	document.all.btnDownloadHidden.click();
}

//-->
			</script>
		</form>
	</body>
</HTML>
