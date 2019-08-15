<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.RoomYuBoList" CodeFile="RoomYuBoList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RoomYuBoList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ʒ���� 
									- <span id="spanTitle" runat="server"></span>
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="Add()" type="button" value="�� ��" name="btnAdd" runat="server"></TD>
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<table cellSpacing="0" cellPadding="0" class="search-area" border="0">
							<tr>
								<td>
									<table>
										<tr>
											<td noWrap>��ȣ�</td>
											<td noWrap><input class="input" id="txtSearchCurYear" type="text" size="4" name="txtSearchCurYear"
													runat="server"></td>
											<td noWrap><span id="spanOutDate" runat="server"></span>���ڣ�</td>
											<td noWrap><cc3:calendar id="txtSearchOutDateBegin" runat="server" Value="" Display="True" ReadOnly="False"
													CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
											<td noWrap>����</td>
											<td noWrap><cc3:calendar id="txtSearchOutDateEnd" runat="server" Value="" Display="True" ReadOnly="False"
													CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
											<td noWrap>״̬��</td>
											<td noWrap><select id="sltSearchCheckState" name="sltSearchCheckState" runat="server">
													<option value="" selected>��ѡ��</option>
													<option value="0">δ��</option>
													<option value="1">����</option>
												</select></td>
											<td><input class="submit" id="btnSearch" onclick="document.all.divHintLoad.style.display = '';"
													type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
										<tr>
											<td noWrap>��Ʒ���ʣ�</td>
											<TD><SELECT id="sltSearchCodeName" name="sltSearchCodeName" runat="server">
													<OPTION value="" selected>------��ѡ��------</OPTION>
												</SELECT></TD>
											<td noWrap><span id="spanOutAspect" runat="server">ȥ��</span></td>
											<TD><span id="spanOutAspect2" runat="server"><input class="input" id="txtSearchOutAspect" type="text" name="txtSearchOutAspect" runat="server">
												<a href="#" onclick="SelectOutAspect()" title="ѡ��ȥ��"><img src="../images/ToolsItemSearch.gif" border="0"></a>
												<SELECT style="DISPLAY:none" id="sltSearchOutAspect" name="sltSearchOutAspect" runat="server">
													<OPTION value="" selected>------��ѡ��------</OPTION>
												</SELECT></span>
											</TD>
										</tr>
										<tr>
											<td noWrap>¥����</td>
											<td colSpan="3"><input class="input" id="txtSearchBuildingName" type="text" size="30" name="txtSearchBuildingName"
													runat="server"><A href="#" onclick="SelectBuilding();return false;"><IMG src="../images/ToolsItemSearch.gif" border="0"></A></td>
											<td noWrap>���ƺţ�</td>
											<td><input class="input" id="txtSearchChamberName" type="text" size="10" name="txtSearchChamberName"
													runat="server"></td>
											<td noWrap>�Һţ�</td>
											<td><input class="input" id="txtSearchRoomName" type="text" size="10" name="txtSearchRoomName"
													runat="server"></td>
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
									<td><asp:datagrid id="dgList" runat="server" AllowPaging="True" Width="100%" CssClass="list" CellPadding="0"
											AllowSorting="True" AutoGenerateColumns="False" PageSize="15" ShowFooter="False">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="���" FooterText="�ϼ�">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="View(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.OutListCode") %>'><%# DataBinder.Eval(Container, "DataItem.OutListName") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="״̬">
													<ItemTemplate>
														<%# RmsPM.BLL.ProductRule.GetTempRoomOutCheckStateName(DataBinder.Eval(Container, "DataItem.CheckState")) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="���">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.CurYear") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.Out_Date", "{0:yyyy-MM-dd}") %>
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
												<asp:TemplateColumn HeaderText="¥��">
													<ItemTemplate>
														<%# RmsPM.BLL.ProductRule.GetRoomInBuildingName(DataBinder.Eval(Container, "DataItem.OutListCode")) %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="¥����">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.RoomNum") %>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="�����(ƽ��)">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.BuildArea", "{0:0.00}") %>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtIOType" type="hidden" name="txtIOType" runat="server">
			<input id="txtOutState" type="hidden" name="txtOutState" runat="server"><input id="txtSelectBuildingCode" type="hidden" name="txtSelectBuildingCode" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

var CurrUrl = window.location.href;

//����
function Add()
{
	document.all.divHintLoad.style.display = "";
	
	if (Form1.txtOutState.value == "���")
	{
		window.location.href = "RoomIOInModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value + "&OutState=" + escape(Form1.txtOutState.value);
	}
	else
	{
		if (Form1.txtOutState.value == "Ԥ��")
		{
			window.location.href = "RoomYuBoModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value + "&OutState=" + escape(Form1.txtOutState.value);
		}
		else
		{
			window.location.href = "RoomIOModify.aspx?FromUrl=" + escape(CurrUrl) + "&ProjectCode=" + Form1.txtProjectCode.value + "&OutState=" + escape(Form1.txtOutState.value);
		}
	}
}

//�鿴
function View(code)
{
	document.all.divHintLoad.style.display = "";

	if (Form1.txtOutState.value == "���")
	{
		window.location.href = "RoomIOInInfo.aspx?FromUrl=" + escape(CurrUrl) + "&OutListCode=" + code;
	}
	else
	{
		if (Form1.txtOutState.value == "Ԥ��")
		{
			window.location.href = "RoomYuBoInfo.aspx?FromUrl=" + escape(CurrUrl) + "&OutListCode=" + code;
		}
		else
		{
			window.location.href = "RoomIOInfo.aspx?FromUrl=" + escape(CurrUrl) + "&OutListCode=" + code;
		}
	}
}

//ѡ��¥��
function SelectBuilding()
{
	var w = 400;
	var h = 540;
	var code = "";

	var ProjectCode = Form1.txtProjectCode.value;
	
	window.open("SelectBuilding.aspx?CanSelectArea=0&ProjectCode=" + ProjectCode + "&SelectCode=" + escape(code) + "&ReturnFunc=SelectBuildingReturn", "" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
}

//ѡ��¥������
function SelectBuildingReturn(code, name)
{
	Form1.txtSelectBuildingCode.value = code;
	Form1.txtSearchBuildingName.value = name;
//	document.all.btnSelectBuildingReturn.click();
}

//ѡ��ȥ��
function SelectOutAspect()
{
	OpenCustomWindow("../SelectBox/SelectDictItem.aspx?DictionaryName=" + escape("ȥ��"), "ѡ��ȥ��", 500, 560);
}

//ѡ��ȥ�򷵻�
function SelectDictItemReturn(code, name)
{
	Form1.txtSearchOutAspect.value = name;
}

//-->
		</SCRIPT>
	</body>
</HTML>
