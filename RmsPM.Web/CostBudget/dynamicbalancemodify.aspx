<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.DynamicBalanceModify" CodeFile="DynamicBalanceModify.aspx.cs" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>修改预留金额</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/convert.js" charset="gb2312"></SCRIPT>
		<SCRIPT language="javascript" src="CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">修改预留金额</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="100" class="form-item">费 用 项：</TD>
								<td><asp:Label Runat="server" ID="lblSortID"></asp:Label>&nbsp;<asp:Label Runat="server" ID="lblCostName"></asp:Label></td>
							</TR>
							<TR>
								<TD class="form-item">预留金额：</TD>
								<TD><input type="text" runat="server" id="txtMoney" name="txtMoney" class="input-nember" size="12"
										onfocus="CBTree_RevertMoneyObject(this);" onblur="CBTree_FormatMoneyObject(this);"><igtxt:webnumericedit Visible="false" id="txtMoney1" runat="server" MinDecimalPlaces="Four" CssClass="infra-input-nember"
										ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" Width="80px"></igtxt:webnumericedit>元
								</TD>
							</TR>
							<tr>
								<TD class="form-item">说 明：</TD>
								<TD><input class="input" type="text" runat="server" id="txtDescription" name="txtDescription"
										style="WIDTH:100%"></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
									<input style="DISPLAY:none" id="btnDelete" name="btnDelete" type="button" class="submit"
										value="删 除" runat="server" onclick="if (!confirm('确实要删除吗？')) return false;" onserverclick="btnDelete_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
			<input type="hidden" name="txtProjectCode" id="txtProjectCode" runat="server"><input type="hidden" name="txtCostCode" id="txtCostCode" runat="server">
			<input type="hidden" name="txtContractMoney" id="txtContractMoney" runat="server">
		</form>
	</body>
</HTML>
