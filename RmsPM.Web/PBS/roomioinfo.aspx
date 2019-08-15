<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.RoomIOInfo" CodeFile="RoomIOInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RoomIOInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnSelectRoomReturn" type="button" value="SelectRoomReturn" name="btnSelectRoomReturn"
					runat="server"> <input id="btnModifyAreaReturn" type="button" value="ModifyAreaReturn" name="btnModifyAreaReturn"
					runat="server">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ʒ���� 
									- <span id="spanTitle" runat="server"></span>����Ϣ</td>
								<td style="CURSOR: hand" onclick="GoBack();" width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle" id="tdToolSep1" runat="server">
						<input class="button" id="btnPrint" onclick="Print();" type="button" value="�� ӡ" name="btnPrint"
							runat="server"> <input class="button" id="btnExcel" onclick="if (!Excel()) return false;" type="button"
							value="Excel" name="btnExcel" runat="server" onserverclick="btnExcel_ServerClick"> <IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" onclick="Modify();" type="button" value="�� ��" name="btnModify"
							runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;Delete();"
							type="button" value="ɾ ��" name="btnDelete" runat="server"> <input class="button" id="btnCheck" onclick="javascript:if(!window.confirm('��˺󽫲������޸ģ�ȷʵҪ�����')) return false;DoCheck();"
							type="button" value="�� ��" name="btnCheck" runat="server"> <input class="button" id="btnCancelCheck" onclick="javascript:if(!window.confirm('ȷʵҪȡ�������')) return false;DoCancelCheck();"
							type="button" value="ȡ�����" name="btnCancelCheck" runat="server">
					</TD>
				</TR>
				<tr>
					<td class="table" valign="top">
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TBODY>
								<TR>
									<TD class="form-item" width="100">�� �ţ�</TD>
									<TD><asp:label id="lblOutListName" Runat="server"></asp:label></TD>
									<TD class="form-item" width="100"><span id="spanOutDate" runat="server"></span>���ڣ�</TD>
									<TD><asp:label id="lblOutDate" Runat="server"></asp:label></TD>
									<TD class="form-item">��Ʒ���ʣ�</TD>
									<TD><asp:label id="lblCodeName" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item"><span id="spanOutAspect" runat="server">ȥ ��</span></TD>
									<TD><asp:label id="lblOutAspect" Runat="server"></asp:label></TD>
									<TD class="form-item">Э���ĺţ�</TD>
									<TD colSpan="3"><asp:label id="lblConferMark" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">�� �� �ˣ�</TD>
									<TD><asp:label id="lblInputPersonName" Runat="server"></asp:label></TD>
									<TD class="form-item">�������ڣ�</TD>
									<TD><asp:label id="lblInputDate" Runat="server"></asp:label></TD>
									<TD class="form-item">���״̬��</TD>
									<TD><asp:label id="lblCheckState" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">�� �� �ˣ�</TD>
									<TD><asp:label id="lblCheckPerson" Runat="server"></asp:label></TD>
									<TD class="form-item">������ڣ�</TD>
									<TD colSpan="3"><asp:label id="lblCheckDate" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">�� ע��</TD>
									<TD colSpan="5"><asp:label id="lblRemark" Runat="server"></asp:label></TD>
								</TR>
							</TBODY>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td class="table">
						<table cellSpacing="0" cellPadding="0" height="100%" width="100%">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">��Դ�б�</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
										<asp:datagrid id="dgList" runat="server" ShowFooter="True" PageSize="15" AutoGenerateColumns="False"
											AllowSorting="True" CellPadding="0" CssClass="list" Width="100%" DataKeyField="RoomCode">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
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
												<asp:BoundColumn DataField="RoomArea" HeaderText="�������(ƽ��)" DataFormatString="{0:0.####}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
						</table>
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
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtOutListCode" type="hidden" name="txtOutListCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input id="txtOutState" type="hidden" name="txtOutState" runat="server"> <input id="txtUserCode" type="hidden" name="txtUserCode" runat="server">
			<input id="txtCheckState" type="hidden" name="txtCheckState" runat="server"> <input id="txtSelectRoomCode" type="hidden" name="txtSelectRoomCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

function GoBack()
{
	if (Form1.txtFromUrl.value == "")
	{
		window.location.href = "RoomIOList.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&IOType=" + escape(Form1.txtOutState.value);
	}
	else
	{
		window.location.href = Form1.txtFromUrl.value;
	}
}

//�޸�
function Modify()
{
	document.all.divHintSave.style.display = "";
	window.location.href = "RoomIOModify.aspx?FromUrl=" + escape(window.location.href) + "&OutListCode=" + Form1.txtOutListCode.value + "&act=Modify";
}

//ɾ��
function Delete()
{
	document.all.divHintSave.style.display = "";
	window.location.href = "RoomIOModify.aspx?FromUrl=" + escape(Form1.txtFromUrl.value) + "&OutListCode=" + Form1.txtOutListCode.value + "&act=delete";
}

//��ӡ
function Print()
{
	OpenPrintWindow("../Product/ProductBillrealList.aspx?Code="+Form1.txtOutListCode.value, "��ӡ");
}

//���
function DoCheck()
{
	document.all.divHintSave.style.display = "";
	window.location.href = "RoomIOModify.aspx?FromUrl=" + escape(window.location.href) + "&OutListCode=" + Form1.txtOutListCode.value + "&act=check";
}

//ȡ�����
function DoCancelCheck()
{
	document.all.divHintSave.style.display = "";
	window.location.href = "RoomIOModify.aspx?FromUrl=" + escape(window.location.href) + "&OutListCode=" + Form1.txtOutListCode.value + "&act=cancelcheck";
}

//Excel
function Excel()
{
	document.all.divHintLoad.style.display = "";
	return true;
}

//-->
		</SCRIPT>
	</body>
</HTML>
