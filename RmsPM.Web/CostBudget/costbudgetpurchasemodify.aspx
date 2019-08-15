<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetPurchaseModify" CodeFile="CostBudgetPurchaseModify.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>采购计划</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/convert.js" charset="gb2312"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="CostBudgetDtlTree.js" charset="gb2312"></SCRIPT>
		<script language="javascript">

		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">合同计划</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD class="form-item" width="100">费用项：</TD>
								<td><asp:Label Runat="server" ID="lblSortID"></asp:Label>&nbsp;<asp:Label Runat="server" ID="lblCostName"></asp:Label></td>
							</tr>
							<tr style="DISPLAY:none">
								<TD class="form-item" width="100">费用项：</TD>
								<td><uc1:InputCostBudgetDtl id="ucCostBudgetDtl" runat="server"></uc1:InputCostBudgetDtl></td>
							</tr>
							<tr>
								<TD class="form-item">合同名称：</TD>
								<td><input type="text" runat="server" id="txtPurpose" name="txtPurpose" size="40" class="input"><font color="red">*</font></td>
							</tr>
							<tr>
								<TD class="form-item">物资名称：</TD>
								<td><input type="text" runat="server" id="txtMaterialName" name="txtMaterialName" size="40"
										class="input"><font color="red">*</font></td>
							</tr>
							<TR>
								<TD class="form-item">合同计划金额：</TD>
								<TD><input type="text" runat="server" id="txtMoney" name="txtMoney" class="input-nember" size="12"
										onfocus="CBTree_RevertMoneyObject(this);" onblur="CBTree_FormatMoneyObject(this);"><igtxt:webnumericedit Visible="false" id="txtMoney1" runat="server" MinDecimalPlaces="Four" CssClass="infra-input-nember"
										ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
										Width="80px"></igtxt:webnumericedit>元
								</TD>
							</TR>
							<tr>
								<TD class="form-item">说明：</TD>
								<td><input type="text" runat="server" id="txtDescription" name="txtDescription" width="100%"
										class="input"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" onclick="document.all.divHintSave.style.display = '';"
										type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
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
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
			<input id="txtCostCode" type="hidden" name="txtCostCode" runat="server"><input id="txtPurchaseFlowCode" type="hidden" name="txtPurchaseFlowCode" runat="server"><input id="txtPurchaseFlowDetailCode" type="hidden" name="txtPurchaseFlowDetailCode" runat="server">
		</form>
	</body>
</HTML>
