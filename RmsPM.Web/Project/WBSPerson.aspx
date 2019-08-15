<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" CodeFile="WBSPerson.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSPerson" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工作项属性</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
		//打开人员属性窗体
		function OpenUser(userCode)
		{
			OpenMiddleWindow('../Systems/UserModify.aspx?act=view&UserCode='+userCode , '管理用户');
		}
		
		function DoCancel()
		{
			window.close();
		}
		
		function AddUser(Flag)
		{
			document.all.hFlag.value = Flag;
			
			if (parseInt(Flag)>0)
				OpenMiddleWindow("../SelectBox/SelectPerson.aspx?Flag=1","选择用户");
			else
				OpenMiddleWindow("../SelectBox/SelectPerson.aspx?Flag=0","选择用户");
		}
		
		function DoSelectUser(UserCode,Flag)
		{
		//	if (document.all.hFlag.value == "2")
		//	{
		//		document.all.hMasterCode.value = UserCode;
		//	}
		//	if (document.all.hFlag.value == "1")
		//	{
		//		document.all.hMonitorCode.value = UserCode;
		//	}
		//	else
		//	{
				document.all.hCode.value = UserCode;
		//	}
			document.forms[0].submit();
		}
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">工作人员</td>
				</tr>
				<tr>
					<td vAlign="top" bgColor="#ffffff">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="80%">
							<tr>
								<td vAlign="top">
									<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr id="trMaster" runat="server">
											<td class="form-item" style="HEIGHT: 25px" width="100">责任人：</td>
											<td style="HEIGHT: 25px" width="80"><asp:label id="lblMaster" runat="server"></asp:label>
											</td>
											<td><input class="button" onclick="AddUser('2');return false;" type="button" name="btnMaster"
													id="btnMaster" runat="server"> <input type="hidden" id="hMasterCode" runat="server" NAME="hMasterCode"></td>
											<td class="form-item">工作描述：</td>
											<td>&nbsp;&nbsp;<input type="text" class="input" id="txtMasterDetail" runat="server" NAME="txtMasterDetail"
													style="WIDTH:200px">
											</td>
										</tr>
										<tr id="trMonitor" runat="server">
											<td class="form-item" style="HEIGHT: 25px" width="100">监督人：</td>
											<td style="HEIGHT: 25px" width="80"><asp:label id="lblMonitor" runat="server"></asp:label>
											</td>
											<td><input class="button" onclick="AddUser('1');return false;" type="button" name="btnMonitor"
													id="btnMonitor" runat="server"> <input type="hidden" id="hMonitorCode" runat="server" NAME="hMonitorCode"></td>
											<td class="form-item">工作描述：</td>
											<td>&nbsp;&nbsp;<input type="text" class="input" id="txtMonitorDetail" runat="server" NAME="txtMonitorDetail"
													style="WIDTH:200px">
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr id="trExecuter" runat="server" valign="top">
								<td>
									<br>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">执行人</td>
											<td><input class="button-small" onclick="AddUser(0);return false;" type="button" name="btnExecuter"
													id="btnExecuter" runat="server" value="新增执行人"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr width="100%">
											<td>
												<asp:DataGrid id="dgUserList" runat="server" DataKeyField="UserCode" AutoGenerateColumns="False"
													Width="100%" CellPadding="2" CssClass="list">
													<HeaderStyle CssClass="list-title"></HeaderStyle>
													<Columns>
														<asp:HyperLinkColumn DataNavigateUrlField="UserCode" DataNavigateUrlFormatString="javascript:OpenUser('{0}')"
															DataTextField="UserName" HeaderText="用户名">
															<ItemStyle Width="40%"></ItemStyle>
														</asp:HyperLinkColumn>
														<asp:TemplateColumn HeaderText="工作描述">
															<ItemTemplate>
																<FONT face="宋体">
																	<asp:TextBox id=txtExecuterDetail runat="server" Width="200px" CssClass="input" Text='<%# DataBinder.Eval(Container, "DataItem.MainTask") %>'>
																	</asp:TextBox></FONT>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:ButtonColumn Text="删除" CommandName="Delete"></asp:ButtonColumn>
													</Columns>
												</asp:DataGrid>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table id="tbButton" align="center" runat="server">
							<tr align="center" width="100%" height="100%" valign="bottom">
								<td><input class="submit" id="SaveToolsButton" type="button" value="确 定" name="SaveToolsButton"
										runat="server">&nbsp;&nbsp;<input class="submit" id="CancelToolsButton" type="button" value="取 消" name="CancelToolsButton"
										onclick="DoCancel();return false;"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="hFlag" runat="server"> <input type="hidden" id="hCode" runat="server">
			<br>
		</form>
		</FORM> &nbsp;
	</body>
</HTML>
