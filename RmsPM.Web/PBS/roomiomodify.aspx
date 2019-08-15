<%@ Page language="c#" Inherits="RmsPM.Web.PBS.RoomIOModify" CodeFile="RoomIOModify.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PBSUnitModify</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<script src="../images/DropYear.js"></script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnSelectRoomReturn" type="button" value="SelectRoomReturn" name="btnSelectRoomReturn"
					runat="server" onserverclick="btnSelectRoomReturn_ServerClick"> <input id="btnModifyAreaReturn" type="button" value="ModifyAreaReturn" name="btnModifyAreaReturn"
					runat="server" onserverclick="btnModifyAreaReturn_ServerClick"> <input id="btnHiddenBatchDelete" style="DISPLAY: none" type="button" value="HiddenBatchDelete"
					name="btnHiddenBatchDelete" runat="server" onserverclick="btnHiddenBatchDelete_ServerClick">
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
									- <span id="spanTitle" runat="server"></span>���޸�</td>
								<td style="CURSOR: hand" onclick="GoBack();" width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnSave" onclick="if (!Save()) return false;" type="button" value="�� ��"
							name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
					</TD>
				</TR>
				<tr>
					<td class="table" valign="top">
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item">�� �ţ�</TD>
								<TD><asp:label id="lblOutListName" Runat="server"></asp:label></TD>
								<TD class="form-item"><span id="spanOutDate" runat="server"></span>���ڣ�</TD>
								<TD><cc3:calendar id="txtOutDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
										Display="True" Value=""></cc3:calendar><font color="red">*</font></TD>
								<TD class="form-item">��Ʒ���ʣ�</TD>
								<TD><SELECT id="sltCodeName" name="sltCodeName" runat="server">
										<OPTION value="" selected>------��ѡ��------</OPTION>
									</SELECT><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item"><span id="spanOutAspect" runat="server">ȥ ��</span></TD>
								<TD><span id="spanOutAspect2" runat="server"><input class="input" id="txtOutAspect" type="text" name="txtOutAspect" runat="server">
									<font color="red">*</font>
										<a href="#" onclick="SelectOutAspect()" title="ѡ��ȥ��"><img src="../images/ToolsItemSearch.gif" border="0"></a>
										<SELECT style="DISPLAY:none" id="sltOutAspect" name="sltOutAspect" runat="server">
											<OPTION value="" selected>------��ѡ��------</OPTION>
										</SELECT>
										 </span>
								</TD>
								<TD class="form-item">Э���ĺţ�</TD>
								<TD colspan="3"><input class="input" id="txtConferMark" type="text" name="txtConferMark" runat="server"></TD>
							</TR>
							<TR>
								<TD class="form-item">�� ע��</TD>
								<TD colSpan="5"><textarea id="txtRemark" style="WIDTH: 100%" name="txtRemark" rows="3" runat="server"></textarea></TD>
							</TR>
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
											<td><input class="button-small" id="btnSelectRoom" onclick="SelectRoom();" type="button" value="��ӷ�Դ"
													name="btnSelectRoom" runat="server"></td>
											<td>&nbsp;<input class="button-small" id="btnBatchDelete" onclick="BatchDelete();" type="button"
													value="����ɾ����Դ" name="btnBatchDelete" runat="server"></td>
											<TD style="DISPLAY:none">&nbsp;<input class="button-small" id="btnModifyArea" onclick="ModifyArea()" type="button" value="�޸ķ�Դ���"
													name="btnModifyArea"></TD>
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
												<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="20"></ItemStyle>
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
												<asp:BoundColumn DataField="RoomArea" HeaderText="�������(ƽ��)" DataFormatString="{0:0.####}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:LinkButton runat="server" Text="ɾ��" CommandName="Delete" CausesValidation="false" ID="btnDelete"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
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
			<input id="txtOutState" type="hidden" name="txtOutState" runat="server"> <input id="txtSelectRoomCode" type="hidden" name="txtSelectRoomCode" runat="server">
			<input id="txtOldCodeName" type="hidden" name="txtOldCodeName" runat="server"> <input id="txtOldCurYear" type="hidden" name="txtOldCurYear" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

function GoBack()
{
	window.location.href = Form1.txtFromUrl.value;
}

//ѡ��Դ
function SelectRoom()
{
	var w = 760;
	var h = 540;
	var code = "";
	var InvState = "";
	
	if (Form1.txtOutState.value == "����")
	{
		InvState = "���";
	}
	if (Form1.txtOutState.value == "�˿�")
	{
		InvState = "����";
	}

	var PBSTypeCode = Form1.sltCodeName.value;
	
	OpenFullWindow("SelectRoom.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&ReturnFunc=SelectRoomReturn&InvState=" + InvState + "&PBSTypeCode=" + PBSTypeCode, "ѡ��Դ");
//	window.open("SelectRoom.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&ReturnFunc=SelectRoomReturn", "" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
}

//ѡ��Դ����
function SelectRoomReturn(code)
{
	Form1.txtSelectRoomCode.value = code;

	document.all.divHintSave.style.display = "block";

	document.all.btnSelectRoomReturn.click();
}

//����ɾ��
function BatchDelete()
{
	var s = ChkGetSelected(document.all.chkSelect);

	if (s == "")
	{
		alert('��ѡ��һ���������¼');
		return false;
	}
	
	Form1.txtSelectRoomCode.value = s;

	document.all.divHintSave.style.display = "block";

	document.all.btnHiddenBatchDelete.click();
	return true;
}

//�޸����
function ModifyArea()
{
	if (Form1.txtSelectRoomCode.value == "")
	{
		alert('����ѡ��Դ');
		return false;
	}
	
	OpenCustomWindow("RoomModifyArea.aspx?action=parent&ReturnFunc=ModifyAreaReturn()", "�޸����" , 760, 540);
}

//�޸��������
function ModifyAreaReturn()
{
	document.all.divHintSave.style.display = "block";

	Form1.btnModifyAreaReturn.click();
}

//����
function Save()
{
	document.all.divHintSave.style.display = "block";

	return true;
}

//ѡ��ȥ��
function SelectOutAspect()
{
	OpenCustomWindow("../SelectBox/SelectDictItem.aspx?DictionaryName=" + escape("ȥ��"), "ѡ��ȥ��", 500, 560);
}

//ѡ��ȥ�򷵻�
function SelectDictItemReturn(code, name)
{
	Form1.txtOutAspect.value = name;
}

//-->
		</SCRIPT>
	</body>
</HTML>
