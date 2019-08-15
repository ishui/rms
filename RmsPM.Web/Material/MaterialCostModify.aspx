<%@ Register TagPrefix="uc1" TagName="InputGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Material.MaterialCostModify" CodeFile="MaterialCostModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���ϼ۸�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--

//-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<div style="display:none"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">���ϼ۸�</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="100" class="form-item" nowrap>��λ��</TD>
								<TD><INPUT id="txtUnit" type="text" class="input" size="20" name="txtUnit"
										runat="server"></TD>
								<TD class="form-item" nowrap><asp:label runat="server" ID="lblPriceTitle">����</asp:label>��</TD>
								<TD><igtxt:webnumericedit Width="120" id="txtPrice" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
											ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit></TD>
							</TR>
							<TR>
								<TD class="form-item" nowrap>��Ŀ��</TD>
								<TD><INPUT id="txtProject" type="text" class="input" size="40" name="txtProject"
										runat="server"></TD>
								<TD class="form-item" nowrap>���ڣ�</TD>
								<TD><cc1:Calendar id="dtBiddingDate" runat="server" CalendarResource="../Images/CalendarResource/"
											Value=""></cc1:Calendar></TD>
							</TR>
							<TR>
								<TD class="form-item" nowrap>���ࣺ</TD>
								<TD><uc1:inputgroup id="ucGroup" runat="server" ClassCode="1411"></uc1:inputgroup><font color="red">*</font></TD>
								<TD class="form-item" nowrap>������</TD>
								<TD><INPUT id="txtAreaCode" type="text" class="input" size="20" name="txtAreaCode"
										runat="server"></TD>
							</tr>
							<tr>
								<TD class="form-item" nowrap>������</TD>
								<td colspan="3"><asp:textbox id="txtDescription" runat="server" Width="100%" CssClass="input" Height="100px" TextMode="MultiLine"></asp:textbox><font color="red">*</font></td>
							</tr>
							<tr>
								<TD class="form-item" nowrap>category��</TD>
								<TD colspan="3"><INPUT id="txtCategory" type="text" class="input" size="40" name="txtCategory"
										runat="server"></TD>
							</tr>
							<tr>
								<TD class="form-item" nowrap>description��</TD>
								<td colspan="3"><asp:textbox id="txtDescriptionEn" runat="server" Width="100%" CssClass="input" Height="100px" TextMode="MultiLine"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server"
										onclick="document.all.divHintSave.style.display = '';" onserverclick="btnSave_ServerClick"> <input id="btnDelete" name="btnDelete" type="button" class="submit" value="ɾ ��" runat="server"
										onclick="if (!confirm('ȷʵҪɾ����')) return false;" onserverclick="btnDelete_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 80px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtMaterialCostCode" type="hidden" name="txtMaterialCostCode" runat="server">
		</form>
	</body>
</HTML>
