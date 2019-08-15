<%@ Page language="c#" Inherits="RmsPM.Web.Systems.StationModify" CodeFile="StationModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��λ</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		</style>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	function doConfigUser()
	{
		OpenLargeWindow("../SelectBox/SelectSUMain.aspx?Type=U&UserCodes=" + Form1.txtReturnUserCodes.value, "ѡ���û�");
	
//		OpenCustomWindow("../SelectBox/SelectPerson.aspx?Flag=1&Type=Multi", "ѡ���û�", 500, 580);
//		OpenMiddleWindow( '../SelectBox/SelectPerson_List.aspx','ѡ���û�' );
	}

	function getReturnStationUser(userCodes,userNames,stationCodes,stationNames,flag)
	{
		Form1.txtReturnUserCodes.value = userCodes;
		Form1.btnRefreshConfigUser.click();
	}

/*	
	function DoSelectUser(codes, names, flag)
	{
		Form1.txtReturnUserCodes.value = codes;
		Form1.btnRefreshConfigUser.click();
	}

	function DoSelectUser(codes,flag)
	{
		Form1.txtReturnUserCodes.value = codes;
		Form1.btnRefreshConfigUser.click();
	}
*/

	function OpenUser( userCode )
	{
		OpenMiddleWindow( 'UserInfo.aspx?UserCode=' + userCode ,'�û���Ϣ' );
	}
	
	function SelectUnit()
	{
		OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode="+window.document.all.txthUnit.value);
	}
	
	//ѡ��λ����
	function SelectUnitReturn(code, name)
	{
		window.document.all.txtUnitName.value = name;
		window.document.all.txthUnit.value = code;
	}
	
	
		</SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��λ��Ϣ</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" width="20%">��λ���ƣ�</TD>
								<TD width="30%"><asp:textbox id="txtStationName" runat="server" CssClass="input"></asp:textbox><font color="red">*</font></TD>
								<TD class="form-item" width="20%">���ţ�</TD>
								<TD width="30%"><input class="input" id="txtUnitName" readOnly type="text" name="txtUnit" runat="server">
									<input class="input" id="txthUnit" type="hidden" name="txthUnit" runat="server">
									<IMG style="CURSOR: hand" onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">Ȩ�޷�Χ��</TD>
								<TD colSpan="3"><asp:radiobuttonlist id="rblRoleLevel" runat="server" RepeatDirection="Horizontal" RepeatColumns="5"
										Height="45px">
										<asp:ListItem Value="0">����</asp:ListItem>
										<asp:ListItem Value="3">����</asp:ListItem>
										<asp:ListItem Value="4">����</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="form-item">��ɫ��</TD>
								<TD valign=middle> <asp:TextBox ID="txtRole" runat="server" CssClass="input" OnTextChanged="txtRole_TextChanged" Width="50px" AutoPostBack=true ></asp:TextBox>&nbsp;<select id="sltRole" name="sltRole" runat="server"></select>
                                   </TD>
								<TD class="form-item">������Χ��</TD>
								<TD><select id="sltAccessRangeUnit" name="sltRole" runat="server"></select></TD>
							</TR>
							<TR>
								<TD class="form-item">˵����</TD>
								<TD colSpan="3"><asp:textbox id="txtDescription" runat="server" CssClass="input" Width="424px"></asp:textbox></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td><br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">�� ��</td>
								<td><input class="button-small" id="configUser" onclick="doConfigUser();" type="button" value="�����û�"
										name="configUser" runat="server"> <input id="btnRefreshConfigUser" style="DISPLAY: none" type="button" value="ѡ���û�����ˢ��" name="btnRefreshConfigUser"
										runat="server" onserverclick="btnRefreshConfigUser_ServerClick"> <input id="txtReturnUserCodes" type="hidden" name="txtReturnUserCodes" runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table id="tableList" height="100%" cellSpacing="0" width="100%" runat="server">
								<tr>
									<td vAlign="top"><asp:datagrid id="dgList" runat="server" CssClass="list" Width="100%" CellPadding="0" GridLines="Horizontal"
											AllowSorting="True" AutoGenerateColumns="False" PageSize="15" AllowPaging="False">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="UserCode" Visible="False"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="�û�">
													<ItemTemplate>
														<a href="#" onclick="OpenUser(code);" code='<%# DataBinder.Eval( Container,"DataItem.UserCode" ) %>' >
															<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container, "DataItem.UserCode").ToString() ) %>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:ButtonColumn Text="&lt;img border=0 width=15 height=15 src='../Images/del.gif' &gt;&lt;/img&gt;"
													CommandName="Delete" HeaderText="ɾ��"></asp:ButtonColumn>
											</Columns>
											<PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table height="100%" cellSpacing="0" cellPadding="10" width="100%" border="0">
							<tr align="center">
								<td><input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;&nbsp;
									<input class="submit" id="btnClose" onclick="window.close();" type="button" value="ȡ ��"
										name="btnClose" runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
			<INPUT id="txtStationCode" type="hidden" name="txtStationCode" runat="server">
		</form>
	</body>
</HTML>
