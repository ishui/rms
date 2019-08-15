<%@ Register TagPrefix="uc1" TagName="InputGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Material.SupplierMaterialModify" CodeFile="SupplierMaterialModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>厂商材料</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
<!--

//选择供应商
function SelectSupplier()
{
	OpenLargeWindow("../SelectBox/SelectSupplier.aspx", "选择供应商");
}

//选择供应商返回
function DoSelectSupplierReturn(code, name)
{
	Form1.txtSupplierCode.value = code;
	Form1.txtSupplierName.value = name;
	document.all.spanSupplierName.innerText = name;
}

//-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<div style="display:none"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">厂商材料</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD class="form-item" nowrap>材料类型：</TD>
								<TD colspan="3"><uc1:inputgroup id="ucGroup" runat="server" ClassCode="1413"></uc1:inputgroup><font color="red">*</font></TD>
							</tr>
							<TR>
								<TD class="form-item" nowrap>厂商：</TD>
								<TD><span id="spanSupplierName" runat="server"></span><A id="hrefSelectSupply" title="选择厂商" onclick="SelectSupplier()" href="#" runat="server"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
								</TD>
								<TD class="form-item" nowrap>品牌：</TD>
								<TD><INPUT id="txtBrand" type="text" class="input" size="20" name="txtBrand"
										runat="server"></TD>
							</TR>
							<TR>
								<TD class="form-item" nowrap>型号：</TD>
								<TD><INPUT id="txtModel" type="text" class="input" size="20" name="txtModel"
										runat="server"></TD>
								<TD class="form-item" nowrap>规格：</TD>
								<TD><INPUT id="txtSpec" type="text" class="input" size="20" name="txtSpec"
										runat="server"></TD>
							</TR>
							<tr>
								<TD class="form-item" nowrap>进口/国产：</TD>
								<TD><INPUT id="txtNation" type="text" class="input" size="20" name="txtNation"
										runat="server"></TD>
								<TD class="form-item" nowrap>产地：</TD>
								<TD><INPUT id="txtAreaCode" type="text" class="input" size="20" name="txtAreaCode"
										runat="server"></TD>
							</tr>
							<tr>
								<TD class="form-item" nowrap>样品序号：</TD>
								<TD colspan="3"><INPUT id="txtSampleID" type="text" class="input" size="20" name="txtSampleID"
										runat="server"></TD>
							</tr>
							<TR>
								<TD width="100" class="form-item" nowrap>单位：</TD>
								<TD><INPUT id="txtUnit" type="text" class="input" size="20" name="txtUnit"
										runat="server"></TD>
								<TD class="form-item" nowrap>单价：</TD>
								<TD><igtxt:webnumericedit Width="120" id="txtPrice" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
											ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit></TD>
							</TR>
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
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtSupplierMaterialCode" type="hidden" name="txtSupplierMaterialCode" runat="server">
			<INPUT id="txtSupplierCode" type="hidden" name="txtSupplierCode" runat="server"><INPUT id="txtSupplierName" type="hidden" name="txtSupplierName" runat="server">
		</form>
	</body>
</HTML>
