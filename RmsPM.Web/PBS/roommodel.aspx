<%@ Page language="c#" Inherits="RmsPM.Web.PBS.RoomModel" CodeFile="RoomModel.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>户型信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">产品管理 
									- 户型信息</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR id="trTool" runat="server">
					<TD class="tools-area" valign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnModify" onclick="Modify()" type="button" value="修 改" name="btnModify"
							runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
							type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="15%" class="form-item">户型</TD>
								<TD width="35%"><asp:Label Runat="server" ID="lblModelName"></asp:Label></TD>
								<TD width="15%" class="form-item">房型</TD>
								<TD width="35%"><asp:Label Runat="server" ID="lblStructure"></asp:Label></TD>
							</TR>
							<tr>
								<TD class="form-item">产品类型</TD>
								<TD colspan="3"><asp:Label Runat="server" ID="lblPBSTypeName"></asp:Label></TD>
							</tr>
							<TR>
								<TD class="form-item">建筑面积</TD>
								<TD><asp:Label Runat="server" ID="lblBuildArea"></asp:Label></TD>
								<TD class="form-item">套内面积</TD>
								<TD><asp:Label Runat="server" ID="lblRoomArea"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="form-item">备注</TD>
								<TD colspan="3"><asp:Label Runat="server" ID="lblRemark"></asp:Label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="note">户型图</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top" align="left">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<IMG src="" id="imgMain" name="imgMain" runat="server" style="DISPLAY:none">
						</div>
					</td>
				</tr>
				<tr id="trBottom1" runat="server">
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trBottom2" runat="server">
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr id="trClose" style="DISPLAY:none" runat="server">
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnClose" name="btnClose" type="button" class="submit" value="关 闭" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtModelCode" type="hidden" name="txtModelCode" runat="server">
			<input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
		</form>
		<SCRIPT language="javascript">
	
	//修改
	function Modify(){
		var code = Form1.txtModelCode.value;
		OpenCustomWindow("RoomModelModify.aspx?Action=Modify&ProjectCode=" + Form1.txtProjectCode.value + "&ModelCode=" + code, "户型修改" , 500, 300);
	}
	
		</SCRIPT>
	</body>
</HTML>
