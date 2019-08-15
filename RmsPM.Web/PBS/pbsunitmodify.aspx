<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSUnitModify" CodeFile="PBSUnitModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��λ����</title>
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
			<div style="DISPLAY: none"><input id="btnSelectBuildingReturn" type="button" name="btnSelectBuildingReturn" runat="server" onserverclick="btnSelectBuildingReturn_ServerClick"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��λ����</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table class="tab-aera" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td>
									<table cellSpacing="6" cellPadding="0" width="100%" border="0">
										<tr>
											<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px">
												<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
													<TBODY>
														<TR>
															<TD class="form-item">��λ�������ƣ�</TD>
															<TD colSpan="3"><input class="input" id="txtPBSUnitName" type="text" name="txtPBSUnitName" runat="server"><font color="red">*</font></TD>
														</TR>
														<TR>
															<!--																<TD class="form-item">���赥λ��</TD>
															<TD><SELECT id="sltDevelopUnit" style="WIDTH: 136px" name="sltDevelopUnit" runat="server">
																	<OPTION value="" selected>--------��ѡ��--------</OPTION>
																</SELECT></TD>-->
															<TD class="form-item">ʩ����λ��</TD>
															<TD colSpan="3"><SELECT id="sltConstructUnit" name="sltConstructUnit" runat="server">
																	<OPTION value="" selected>----��ѡ��----</OPTION>
																</SELECT></TD>
														</TR>
														<tr>
															<td class="form-item">�� �� �ˣ�</td>
															<td colspan="3"><uc1:InputUser id="ucManager" runat="server"></uc1:InputUser>
															</td>
														</tr>
														<TR>
															<TD class="form-item" noWrap>�ƻ�Ͷ���ܶ</TD>
															<TD width="30%"><input class="input-nember" id="txtPInvest" type="text" size="12" name="txtPInvest" runat="server">��Ԫ</TD>
															<TD class="form-item" noWrap>ʵ��Ͷ���ܶ</TD>
															<TD width="30%"><input class="input-nember" id="txtInvest" type="text" size="12" name="txtInvest" runat="server">��Ԫ</TD>
														</TR>
														<TR>
															<TD class="form-item">�ƻ�����ʱ�䣺</TD>
															<TD noWrap><cc3:calendar id="txtPStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
																	ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
															<TD class="form-item">�ƻ�����ʱ�䣺</TD>
															<TD noWrap><cc3:calendar id="txtPEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
																	Display="True" Value=""></cc3:calendar></TD>
														</TR>
														<TR>
															<TD class="form-item">������룺</TD>
															<TD colSpan="3"><input class="input" id="txtUFCode" type="text" name="txtUFCode" runat="server"></TD>
														</TR>
														<TR>
															<TD class="form-item">��ע��</TD>
															<TD colSpan="3"><textarea id="txtRemark" style="WIDTH: 100%" name="txtRemark" rows="5" runat="server"></textarea></TD>
														</TR>
														<tr style="DISPLAY: none">
															<TD class="form-item">������ȣ�</TD>
															<TD colSpan="3"><SELECT id="sltVisualProgress" name="sltVisualProgress" runat="server">
																	<OPTION value="" selected>----��ѡ��----</OPTION>
																</SELECT><font color="red">*</font></TD>
														</tr>
														<TR style="DISPLAY: none">
															<TD class="form-item">ʵ�ʿ���ʱ�䣺</TD>
															<TD noWrap><cc3:calendar id="txtStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
																	ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
															<TD class="form-item">ʵ�ʿ���ʱ�䣺</TD>
															<TD noWrap><cc3:calendar id="txtEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
																	Display="True" Value=""></cc3:calendar></TD>
														</TR>
													</TBODY>
												</TABLE>
											</td>
											<td>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" noWrap>¥���б�</td>
														<td><input class="button-small" id="btnSelectBuilding" onclick="SelectBuilding();" type="button"
																value="ѡ��¥��" name="btnSelectBuilding" runat="server"></td>
													</tr>
												</table>
												<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
													<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<tr vAlign="top">
															<td><asp:datagrid id="dgList" runat="server" ShowFooter="True" PageSize="15" AutoGenerateColumns="False"
																	AllowSorting="True" CellPadding="0" CssClass="list" Width="100%" DataKeyField="BuildingCode">
																	<HeaderStyle CssClass="list-title"></HeaderStyle>
																	<FooterStyle CssClass="list-title"></FooterStyle>
																	<Columns>
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="¥������" FooterText="�ϼ�">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenBuildingInfo(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "BuildingCode") %>'><%#  DataBinder.Eval(Container.DataItem, "BuildingName") %></a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="�������(ƽ��)">
																			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Right"></ItemStyle>
																			<FooterStyle HorizontalAlign="Right"></FooterStyle>
																			<ItemTemplate>
																				<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container.DataItem, "Area")) %>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="ɾ��">
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																			<ItemTemplate>
																				<asp:LinkButton runat="server" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
																					CommandName="Delete" CausesValidation="false" ID="btnDelete"></asp:LinkButton>
																			</ItemTemplate>
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
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
									<input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="ȡ ��"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtPBSUnitCode" type="hidden" name="txtPBSUnitCode" runat="server">
			<input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server"> <input id="txtSelectBuildingCode" type="hidden" name="txtSelectBuildingCode" runat="server">
			<input id="txtManager" type="hidden" name="txtManager" runat="server"><input id="txtManagerName" type="hidden" name="txtManagerName" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

function GoBack()
{
	window.location.href = Form1.txtFromUrl.value;
}

//ѡ��¥��
function SelectBuilding()
{
	var w = 400;
	var h = 540;
	var code = "";
	
	code = Form1.txtSelectBuildingCode.value;
	
	window.open("SelectBuilding.aspx?CanSelectArea=0&ProjectCode=" + Form1.txtProjectCode.value + "&SelectCode=" + escape(code) + "&SelectName=" + escape(code) + "&ReturnFunc=SelectBuildingReturn", "" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
}

//ѡ��¥������
function SelectBuildingReturn(code, name)
{
	Form1.txtSelectBuildingCode.value = code;
	document.all.btnSelectBuildingReturn.click();
}
//-->
		</SCRIPT>
	</body>
</HTML>
