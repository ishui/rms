<%@ Page language="c#" CodeFile="SupplierType.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Supplier.SupplierType" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>供应商类型</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
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
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
									合同管理 -&nbsp;供应商类型
								</td>
								<td style="CURSOR: hand" onclick="window.navigate('../Desktop.aspx'); return false;"
									width="79"><IMG height="25" src="../images/btn_back.jpg" width="79"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnNew" onclick="doNewSupplierType('');return false;" type="button"
							value="新增类型"  runat="server">
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top">
						<TABLE id="TableMain" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0"
							height="100%">
							<TR>
								<TD vAlign="top" align="left" HEIGHT="90%"><iframe id="TreeSplitTop" src="SupplierTypeTree.aspx" frameBorder="no" width="100%" scrolling="auto"
										height="100%"></iframe>
								</TD>
							</TR>
						</TABLE>
					</td>
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
		</form>
		<script language="javascript">
<!--
	function SelectNode(supplierTypeCode)
	{
		OpenMiddleWindow( '../Supplier/SupplierTypeModify.aspx?Action=Modify&supplierTypeCode=' + supplierTypeCode ,'修改供应商类型');
	}
	
	function doNewSupplierType()
	{
		OpenMiddleWindow( '../Supplier/SupplierTypeModify.aspx?Action=AddChild' ,'新增供应商类型');
	}
	
	
//-->
		</script>
	</body>
</HTML>
