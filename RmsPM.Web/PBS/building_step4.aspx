<%@ Page language="c#" Inherits="RmsPM.Web.PBS.Building_Step4" CodeFile="Building_Step4.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Building_Step4</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
	</HEAD>
	<BODY bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">��Ʒ���� 
									- ����ͼ
								</td>
								<td style="CURSOR: hand" onclick="GoBack();" width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" align="center">
						<table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
							<tr> 
								<td height="25" class="note" align="center">�½�¥���ṹ���Ĳ�</td>
							</tr>
							<tr>
								<td align="center" class="note"><asp:label id="lblBuildingName" Runat="server"></asp:label>(<asp:label id="lblPBSTypeName" Runat="server"></asp:label>)</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
						<table class="list" cellSpacing="0" cellPadding="0" width="300" border="0">
							<tr>
								<td align="center" width="70" rowSpan="2" class="list-a">
									<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr align="center">
											<td noWrap rowSpan="3">¥��</td>
											<td noWrap height="33%">���ƺ�
											</td>
										</tr>
										<tr align="center">
											<td noWrap height="33%">�Һ�</td>
										</tr>
										<tr align="center">
											<td noWrap height="33%">����</td>
										</tr>
									</table>
								</td>
								<asp:repeater id="rpDoorName" runat="server">
									<ItemTemplate>
										<td align="center" colspan='<%# DataBinder.Eval(Container.DataItem, "RoomCount") %>' class="list-c">
											<%# DataBinder.Eval(Container.DataItem, "DoorName") %>
										</td>
									</ItemTemplate>
								</asp:repeater>
							</tr>
							<tr>
								<asp:repeater id="rpRoomCount" runat="server">
									<ItemTemplate>
										<td class="list-c">
											<input name="txtRoomName" type="text" class="input" id="txtRoomName" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "RoomName") %>' size="6" maxlength="4">
											<br>
											<%# DataBinder.Eval(Container.DataItem, "ColHtml") %>
										</td>
									</ItemTemplate>
								</asp:repeater></tr>
							<asp:repeater id="dlBuild1" runat="server">
								<ItemTemplate>
									<tr>
										<td align="center" class="list-c"><%# DataBinder.Eval(Container.DataItem, "FloorName") %></td>
										<%# DataBinder.Eval(Container.DataItem, "No3") %>
									</tr>
								</ItemTemplate>
							</asp:repeater>
						</table>
						</div>
					</td>
				</tr>
				<tr align="center">
					<td class="table">
						<table>
							<tr>
								<td><input class="submit" id="btnBack" onclick="GoBack();" type="button" value="��һ��" name="btnBack" onclick="document.all.divHintLoad.style.display='';">
									<input class="submit" id="btnSubmit" type="button" value="��һ��" name="btnSubmit" runat="server" onclick="document.all.divHintSave.style.display='';" onserverclick="btnSubmit_ServerClick">
									<input class="submit" id="btnSave" style="DISPLAY: none" type="button" value="����" name="btnSave"
										runat="server"> <input class="submit" onclick="GotoBuildingPart(Form1.txtBuildingCode.value);" type="button"
										value="ȡ ��">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table">&nbsp;</td>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtParentCode" type="hidden" name="txtParentCode" runat="server">
			<input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server"><input id="txtAct" type="hidden" name="txtAct" runat="server">
			<input id="txtIsArea" type="hidden" name="txtIsArea" runat="server"> <input id="txtBuildingName" type="hidden" size="1" name="txtBuildingName" runat="server">
			<input id="txtDoorCount" type="hidden" size="1" name="txtDoorCount" runat="server">
		</form>
		<script language="javascript">
		<!--
			function GoBack(){
				window.location.href = "Building_Step3.aspx?BuildingCode=" + Form1.txtBuildingCode.value + "&ProjectCode=" + Form1.txtProjectCode.value + "&DoorCount=" + Form1.txtDoorCount.value + "&action=prev";
			}
		//-->
		</script>
	</BODY>
</HTML>
