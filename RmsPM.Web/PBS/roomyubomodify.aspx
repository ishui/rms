<%@ Page language="c#" Inherits="RmsPM.Web.PBS.RoomYuBoModify" CodeFile="RoomYuBoModify.aspx.cs" %>
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
			<div style="DISPLAY: none"><input id="btnSelectBuildingReturn" type="button" value="btnSelectBuildingReturn" name="btnSelectBuildingReturn"
					runat="server" onserverclick="btnSelectBuildingReturn_ServerClick">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- <span id="spanTitle" runat="server"></span>单修改</td>
								<td style="CURSOR: hand" onclick="GoBack();" width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnSave" onclick="if (!Save()) return false;" type="button" value="保 存"
							name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
					</TD>
				</TR>
				<tr>
					<td class="table" valign="top">
						<TABLE class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item">编 号：</TD>
								<TD><asp:label id="lblOutListName" Runat="server"></asp:label></TD>
								<TD class="form-item"><span id="spanOutDate" runat="server"></span>日期：</TD>
								<TD><cc3:calendar id="txtOutDate" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar><font color="red">*</font></TD>
								<TD class="form-item">产品性质：</TD>
								<TD><SELECT id="sltCodeName" name="sltCodeName" runat="server">
										<OPTION value="" selected>------请选择------</OPTION>
									</SELECT><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item"><span id="spanOutAspect" runat="server">去 向：</span></TD>
								<TD><span id="spanOutAspect2" runat="server"><input class="input" id="txtOutAspect" type="text" name="txtOutAspect" runat="server">
									<font color="red">*</font>
										<a href="#" onclick="SelectOutAspect()" title="选择去向"><img src="../images/ToolsItemSearch.gif" border="0"></a>
										<SELECT style="DISPLAY:none" id="sltOutAspect" name="sltOutAspect" runat="server">
											<OPTION value="" selected>------请选择------</OPTION>
										</SELECT>
										 </span>
								</TD>
								<TD class="form-item">协议文号：</TD>
								<TD colspan="3"><input class="input" id="txtConferMark" type="text" name="txtConferMark" runat="server" size="30"></TD>
							</TR>
							<TR>
								<TD class="form-item">备 注：</TD>
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
											<td class="intopic" width="200">楼栋列表</td>
											<td><input class="button-small" id="btnSelectBuilding" onclick="SelectBuilding();" type="button"
													value="选择楼栋" name="btnSelectBuilding" runat="server"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
										<asp:datagrid id="dgList" runat="server" DataKeyField="BuildingCode" width="100%" CssClass="list"
											CellPadding="4" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" ShowFooter="True">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="False" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="20"></ItemStyle>
													<ItemTemplate>
														<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.BuildingCode")%>'>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="楼栋名称" FooterText="合计">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="OpenBuildingInfo(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>'><%# DataBinder.Eval(Container, "DataItem.BuildingName") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="预拨面积(平米)">
													<ItemTemplate>
														<input name="txtYuBoArea" type="text" id="txtYuBoArea" onkeydown="if(event.keyCode==13) event.keyCode=9" class="input-nember" runat="server" size="12" value='<%# DataBinder.Eval(Container, "DataItem.YuBoArea", "{0:0.####}") %>'>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
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
			<input id="txtOutState" type="hidden" name="txtOutState" runat="server"> <input id="txtOldCodeName" type="hidden" name="txtOldCodeName" runat="server">
			<input id="txtOldCurYear" type="hidden" name="txtOldCurYear" runat="server"> <input id="txtSelectBuildingCode" type="hidden" name="txtSelectBuildingCode" runat="server"><input id="txtSelectBuildingName" type="hidden" name="txtSelectBuildingName" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

function GoBack()
{
	window.location.href = Form1.txtFromUrl.value;
}

//选择楼栋
function SelectBuilding()
{
	var code = Form1.txtSelectBuildingCode.value;

	var ProjectCode = Form1.txtProjectCode.value;
	var PBSTypeCode = Form1.sltCodeName.value;
	OpenCustomWindow("SelectBuilding.aspx?CanSelectArea=0&PBSTypeCode=" + PBSTypeCode + "&ProjectCode=" + ProjectCode + "&SelectCode=" + escape(code) + "&ReturnFunc=SelectBuildingReturn", "选择楼栋", 400, 540);
}

//选择楼栋返回
function SelectBuildingReturn(code, name)
{
	document.all.divHintSave.style.display = "block";

	Form1.txtSelectBuildingCode.value = code;
	Form1.txtSelectBuildingName.value = name;
	document.all.btnSelectBuildingReturn.click();
}

//保存
function Save()
{
	document.all.divHintSave.style.display = "block";

	return true;
}

//选择去向
function SelectOutAspect()
{
	OpenCustomWindow("../SelectBox/SelectDictItem.aspx?DictionaryName=" + escape("去向"), "选择去向", 500, 560);
}

//选择去向返回
function SelectDictItemReturn(code, name)
{
	Form1.txtOutAspect.value = name;
}

//-->
		</SCRIPT>
	</body>
</HTML>
