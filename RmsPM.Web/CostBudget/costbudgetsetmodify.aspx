<%@ Register TagPrefix="uc1" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputPBS" Src="../UserControls/InputPBS.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetSetModify" CodeFile="CostBudgetSetModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>预算表设置</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--

function PBSChange()
{
	Form1.btnPBSChange.click();
}

//-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<div style="display:none"><input type="button" runat="server" id="btnPBSChange" name="btnPBSChange" value="btnPBSChange" onserverclick="btnPBSChange_ServerClick"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">预算表设置</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="80" class="form-item">预算表名称：</TD>
								<TD><INPUT id="txtCostBudgetSetName" type="text" class="input" size="40" name="txtCostBudgetSetName"
										runat="server"><font color="red">*</font></TD>
							</TR>
							<tr>
								<TD class="form-item">预算类别：</TD>
								<TD><uc1:inputgroup id="ucGroup" runat="server" ClassCode="0411"></uc1:inputgroup><font color="red">*</font></TD>
							</tr>
							<tr>
								<TD class="form-item">部门：</TD>
								<TD><uc1:inputunit id="ucUnit" runat="server"></uc1:inputunit><font color="red">*</font></TD>
							</tr>
							<tr>
								<TD class="form-item">单位工程：</TD>
								<TD><uc1:inputpbs id="ucPBS" runat="server" OnChange="PBSChange()"></uc1:inputpbs><font color="red">*</font></TD>
							</tr>
							<tr>
								<td class="form-item">建筑面积：</td>
								<td><igtxt:webnumericedit id="txtBuildingArea" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
										ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
										JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit>平米</td>
							</tr>
							<tr>
								<td class="form-item">单元数：</td>
								<td><igtxt:webnumericedit Width="60" id="txtHouseCount" runat="server" CssClass="infra-input-nember" ImageDirectory="../images/infragistics/images/"
										JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit></td>
							</tr>
							<tr>
								<TD class="form-item">产品类型：</TD>
								<TD><SELECT id="sltPBSTypeCode" name="sltPBSTypeCode" runat="server">
										<OPTION value="" selected>------请选择------</OPTION>
									</SELECT>
								</TD>
						    </tr>
							<tr>
								<td class="form-item">设置类型：</td>
								<td><select id="sltSetType" name="sltSetType" runat="server" class="select">
								    </select><font color="red">*</font></td>
							</tr>
							<tr style="DISPLAY:none">
								<TD class="form-item">费用项：</TD>
								<TD><uc1:inputcost id="ucCost" runat="server" SelectAllLeaf="true"></uc1:inputcost><font color="red">*</font></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server"
										onclick="document.all.divHintSave.style.display = '';" onserverclick="btnSave_ServerClick"> <input id="btnDelete" name="btnDelete" type="button" class="submit" value="删 除" runat="server"
										onclick="if (!confirm('确实要删除吗？')) return false;" onserverclick="btnDelete_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
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
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
			<input type="hidden" name="txtProjectCode" id="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
