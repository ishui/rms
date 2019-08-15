<%@ Control Language="c#" Inherits="RmsPM.Web.Systems.StationInfo_Control" CodeFile="StationInfo_Control.ascx.cs" %>
<TITLE>岗位信息</TITLE>
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
		OpenLargeWindow("../SelectBox/SelectSUMain.aspx?Type=U&UserCodes=" + myUserid.value, "选择用户");	
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
		OpenMiddleWindow( 'UserInfo.aspx?UserCode=' + userCode ,'用户信息' );
	}
	
	function SelectUnit()
	{
		var myid = document.getElementById("StationInfo_Control1_txthUnit");
		OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode="+myid.value);
	}
	
	//选择单位返回
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
							<INPUT class="button" id="btnModify" type="button" value="修 改" name="btnModify" runat="server" onserverclick="btnModify_ServerClick">
							<INPUT class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
								type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <INPUT class="button" id="btnClose1" style="DISPLAY: none" onclick="window.close();" type="button"
								value="关 闭" name="btnClose" runat="server">
						</TD>
					<TR>
						<TD vAlign="top">
							<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="form-item" width="20%">岗位名称：</TD>
									<TD width="30%">
										<asp:label id="lblStationName" runat="server"></asp:label></TD>
									<TD class="form-item" width="20%">单位：</TD>
									<TD width="30%">
										<asp:label id="lblUnit" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">角色名称：</TD>
									<TD>
										<asp:label id="lblRoleName" runat="server"></asp:label></TD>
									<TD class="form-item">权限范围：</TD>
									<TD>
										<asp:label id="lblRoleLevelName" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="form-item">说明：</TD>
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
						<TD class="tools-area" colSpan="5"><IMG src="../images/btn_li.gif" align="absMiddle"><INPUT class="button" id="Bt_SaveStation" type="button" value="保 存" name="btSave" runat="server" onserverclick="Bt_SaveStation_ServerClick">&nbsp; 
							&nbsp;<INPUT class="button" id="Bt_stationCancel" onclick="javascript:if(!window.confirm('确实要取消吗？')) return false;"
								type="button" value="取 消" name="Bt_stationCancel" runat="server" onserverclick="Bt_stationCancel_ServerClick">
						</TD>
					</TR>
					<TR>
						<TD class="form-item" style="WIDTH: 180px" width="180">岗位名称：</TD>
						<TD width="30%">
							<asp:TextBox id="txtStationName" runat="server" CssClass="input"></asp:TextBox><FONT color="red">*</FONT></TD>
						<TD class="form-item" width="20%">部门：</TD>
						<TD width="30%"><INPUT class="input" id="txtUnitName" readOnly type="text" name="txtUnit" runat="server">
							<INPUT class="input" id="txthUnit" type="hidden" name="txthUnit" runat="server">
							<IMG style="CURSOR: hand" onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"><FONT color="red">*</FONT></TD>
					</TR>
					<TR>
						<TD class="form-item" style="WIDTH: 180px">权限范围：</TD>
						<TD colSpan="3">
							<asp:RadioButtonList id="rblRoleLevel" runat="server" Height="45px" RepeatColumns="5" RepeatDirection="Horizontal">
								<asp:ListItem Value="0">集团</asp:ListItem>
								<asp:ListItem Value="3">部门</asp:ListItem>
								<asp:ListItem Value="4">个人</asp:ListItem>
							</asp:RadioButtonList></TD>
					</TR>
					<TR>
						<TD class="form-item" style="WIDTH: 180px">角色：</TD>
						<TD><SELECT id="sltRole" name="sltRole" runat="server"></SELECT></TD>
						<TD class="form-item">工作范围：</TD>
						<TD><SELECT id="sltAccessRangeUnit" name="sltAccessRangeUnit" runat="server"></SELECT></TD>
					</TR>
					<TR>
						<TD class="form-item" style="WIDTH: 180px">说明：</TD>
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
					<td class="intopic" width="200">用 户</td>
					<td><input class="button-small" id="configUser" onclick="doMyConfigUser();" type="button" value="配置用户"
							name="configUser" runat="server"> <input id="btnRefreshConfigUser" style="DISPLAY: none" type="button" value="选择用户返回刷新" name="btnRefreshConfigUser"
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
								<asp:TemplateColumn HeaderText="用户">
									<ItemTemplate>
										<A onclick=OpenUser(code); href="#" code='<%# DataBinder.Eval( Container,"DataItem.UserCode" ) %>'>
											<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container, "DataItem.UserCode").ToString() ) %>
										</A>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn Text="&lt;img border=0 width=15 height=15 src='../Images/del.gif' &gt;&lt;/img&gt;"
									HeaderText="删除" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
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
					<td><input class="submit" id="btnSave" type="button" value="保存用户" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp;&nbsp;&nbsp;
						<input class="submit" id="btnClose" onclick="window.close();" type="button" value="还原信息"
							name="btnClose" runat="server" onserverclick="btnClose_ServerClick">
					</td>
				</tr>
			</table>
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
			<INPUT id="txtStationCode" type="hidden" name="txtStationCode" runat="server">
		</td>
	</tr>
</table>
