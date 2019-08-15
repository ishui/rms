<%@ Control Language="c#" Inherits="RmsPM.Web.Systems.StationInfo_Control" CodeFile="StationInfo_Control.ascx.cs" %>
<TITLE>��λ��Ϣ</TITLE>
<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
<meta content="C#" name="CODE_LANGUAGE">
<meta content="JavaScript" name="vs_defaultClientScript">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
<SCRIPT>
	function doMyConfigUser()
	{
		var myUserid = document.getElementById("StationInfo_Control1_txtReturnUserCodes");		
		OpenLargeWindow("../SelectBox/SelectSUMain.aspx?Type=U&UserCodes=" + myUserid.value, "ѡ���û�");	
	}

	function getReturnStationUser(userCodes,userNames,stationCodes,stationNames,flag)
	{
		var myid = document.getElementById("StationInfo_Control1_txtReturnUserCodes");	
		//document.write(userCodes);
		//document.write("gy");
		//document.write(myid.value);	
		myid.value = userCodes;
		//document.write("gy");
		//document.write(userCodes+"gyu"+myid.value);
		var myre = document.getElementById("StationInfo_Control1_btnRefreshConfigUser");
		myre.onclick();
	}


	function OpenUser( userCode )
	{
		OpenMiddleWindow( 'UserInfo.aspx?UserCode=' + userCode ,'�û���Ϣ' );
	}
	
	function SelectUnit()
	{
		var myid = document.getElementById("StationInfo_Control1_txthUnit");
		OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode="+myid.value);
	}
	
	//ѡ��λ����
	function SelectUnitReturn(code, name)
	{
		window.document.all.StationInfo_Control1_txtUnitName.value = name;
		window.document.all.StationInfo_Control1_txthUnit.value = code;
	}
</SCRIPT>
<table id="table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td valign="top"><asp:panel id="Pl_StShow" runat="server">
				<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
					<TR>
						<TD class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
							<INPUT class="button" id="btnModify" type="button" value="�� ��" name="btnModify" runat="server" onserverclick="btnModify_ServerClick">
							<INPUT class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
								type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <INPUT class="button" id="btnClose1" style="DISPLAY: none" onclick="window.close();" type="button"
								value="�� ��" name="btnClose" runat="server">
						</TD>
					<TR>
						<TD vAlign="top">
							<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="form-item" width="20%">��λ���ƣ�</TD>
									<TD width="30%">
										<asp:label id="lblStationName" runat="server"></asp:label></TD>
									<TD class="form-item" width="20%">��λ��</TD>
									<TD width="30%">
										<asp:label id="lblUnit" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">��ɫ���ƣ�</TD>
									<TD>
										<asp:label id="lblRoleName" runat="server"></asp:label></TD>
									<TD class="form-item">Ȩ�޷�Χ��</TD>
									<TD>
										<asp:label id="lblRoleLevelName" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">˵����</TD>
									<TD colSpan="3">
										<asp:label id="lblDescription" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:panel></td>
	</tr>
	<tr>
		<td>
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<TR>
					<TD vAlign="top" align="center"></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
	<tr>
		<td valign="top"><asp:panel id="Pl_StEdit" runat="server" Visible="False">
				<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD class="tools-area" colSpan="5"><IMG src="../images/btn_li.gif" align="absMiddle"><INPUT class="button" id="Bt_SaveStation" type="button" value="�� ��" name="btSave" runat="server" onserverclick="Bt_SaveStation_ServerClick">&nbsp; 
							&nbsp;<INPUT class="button" id="Bt_stationCancel" onclick="javascript:if(!window.confirm('ȷʵҪȡ����')) return false;"
								type="button" value="ȡ ��" name="Bt_stationCancel" runat="server" onserverclick="Bt_stationCancel_ServerClick">
						</TD>
					</TR>
					<TR>
						<TD class="form-item" style="WIDTH: 180px" width="180">��λ���ƣ�</TD>
						<TD width="30%">
							<asp:TextBox id="txtStationName" runat="server" CssClass="input"></asp:TextBox><FONT color="red">*</FONT></TD>
						<TD class="form-item" width="20%">���ţ�</TD>
						<TD width="30%"><INPUT class="input" id="txtUnitName" readOnly type="text" name="txtUnit" runat="server">
							<INPUT class="input" id="txthUnit" type="hidden" name="txthUnit" runat="server">
							<IMG style="CURSOR: hand" onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"><FONT color="red">*</FONT></TD>
					</TR>
					<TR>
						<TD class="form-item" style="WIDTH: 180px">Ȩ�޷�Χ��</TD>
						<TD colSpan="3">
							<asp:RadioButtonList id="rblRoleLevel" runat="server" Height="45px" RepeatColumns="5" RepeatDirection="Horizontal">
								<asp:ListItem Value="0">����</asp:ListItem>
								<asp:ListItem Value="3">����</asp:ListItem>
								<asp:ListItem Value="4">����</asp:ListItem>
							</asp:RadioButtonList></TD>
					</TR>
					<TR>
						<TD class="form-item" style="WIDTH: 180px">��ɫ��</TD>
						<TD><SELECT id="sltRole" name="sltRole" runat="server"></SELECT></TD>
						<TD class="form-item">������Χ��</TD>
						<TD><SELECT id="sltAccessRangeUnit" name="sltAccessRangeUnit" runat="server"></SELECT></TD>
					</TR>
					<TR>
						<TD class="form-item" style="WIDTH: 180px">˵����</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtDescription" runat="server" CssClass="input" Width="424px"></asp:TextBox></TD>
					</TR>
				</TABLE>
			</asp:panel></td>
	</tr>
	<tr>
		<td>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="intopic" width="200">�� ��</td>
					<td><input class="button-small" id="configUser" onclick="doMyConfigUser();" type="button" value="�����û�"
							name="configUser" runat="server"> <input id="btnRefreshConfigUser" style="DISPLAY: none" type="button" value="ѡ���û�����ˢ��" name="btnRefreshConfigUser"
							runat="server" onserverclick="btnRefreshConfigUser_ServerClick"> <input id="txtReturnUserCodes" type="hidden" name="txtReturnUserCodes" runat="server">
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr height="100%">
		<td>
			<table id="tableList" height="100%" cellSpacing="0" width="100%" runat="server">
				<tr>
					<td vAlign="top"><asp:datagrid id="dgList" runat="server" CssClass="list" Width="100%" AllowPaging="False" PageSize="15"
							AutoGenerateColumns="False" AllowSorting="True" GridLines="Horizontal" CellPadding="0">
							<FooterStyle CssClass="list-title"></FooterStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="UserCode"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="�û�">
									<ItemTemplate>
										<A onclick=OpenUser(code); href="#" code='<%# DataBinder.Eval( Container,"DataItem.UserCode" ) %>'>
											<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container, "DataItem.UserCode").ToString() ) %>
										</A>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn Text="&lt;img border=0 width=15 height=15 src='../Images/del.gif' &gt;&lt;/img&gt;"
									HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<table height="100%" cellSpacing="0" cellPadding="10" width="100%" border="0">
				<tr align="center">
					<td><input class="submit" id="btnSave" type="button" value="�����û�" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;&nbsp;
						<input class="submit" id="btnClose" onclick="window.close();" type="button" value="��ԭ��Ϣ"
							name="btnClose" runat="server" onserverclick="btnClose_ServerClick">
					</td>
				</tr>
			</table>
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
			<INPUT id="txtStationCode" type="hidden" name="txtStationCode" runat="server">
		</td>
	</tr>
</table>
