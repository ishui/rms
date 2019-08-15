<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PAFrame" CodeFile="PAFrame.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>成本分摊框架</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
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
									财务管理 - 成本分摊</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">
						<input class="button" id="btnApportion" onclick="doPayoutApportion();" type="button" value="批量分摊"
							name="btnApportion" runat="server">
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<table cellpadding="0" cellpadding="0" height="100%" width="100%">
							<tr>
								<td>
									<table class="search-area" height="1" cellSpacing="0" cellPadding="0" border="0" onkeydown="SearchKeyDown();">
										<tr>
											<td>
												<table>
													<TR>
														<TD id="tdSearchStatus" noWrap runat="server" colspan="2">
															&nbsp;<input id="chkNotApportioned" type="checkbox" value="1" name="chkNotApportioned" runat="server"
																CHECKED>&nbsp;未分摊&nbsp;<input id="chkIsApportioned" type="checkbox" value="1" name="chkIsApportioned" runat="server"
																CHECKED> &nbsp;已分摊&nbsp;&nbsp;&nbsp;
														</TD>
														<TD colspan="2">付款类型：
															<uc1:InputSystemGroup id="inputSystemGroup" runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
														</TD>
														<td colspan="2">
															<input id="chkSearch" type="checkbox" value="0" runat="server" NAME="chkSearch" CHECKED>
															在分类中查找 &nbsp;&nbsp;&nbsp;
														</td>
														<TD noWrap vAlign="middle"><input class="submit" id="btnSearch" type="button" value="搜索" name="btnSearch" runat="server"
																onclick="doSearch(); return false;">
														</TD>
													</TR>
													<tr>
														<TD>合同编号：</TD>
														<TD><INPUT class="input" id="txtContractID" style="WIDTH: 130px" type="text" size="12" name="txtContractID"
																runat="server"></TD>
														<TD>合同名称：</TD>
														<TD><INPUT class="input" id="txtContractName" style="WIDTH: 171px; HEIGHT: 18px" type="text"
																size="23" name="txtContractName" runat="server"></TD>
														<td></td>
														<td></td>
														<td></td>
													</tr>
													<TR>
														<TD>受款单位：</TD>
														<TD colspan="3"><INPUT class="input" id="txtSupplyName" type="text" size="42" name="txtSupplyName" runat="server"
																style="WIDTH: 288px; HEIGHT: 18px"><a href="#" onclick="SelectSupplier();return false;" title="选择供应商">
																<img src="../images/ToolsItemSearch.gif" border="0"></a></TD>
														<TD>受 款 人：</TD>
														<TD><INPUT class="input" id="txtPayer" style="WIDTH: 130px" type="text" size="12" name="txtPayer"
																runat="server"></TD>
														<td></td>
													</TR>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr height="100%">
											<td vAlign="top" width="20%">
												<iframe id="TreeSplitTop" src='../Finance/PATree.aspx?ProjectCode=<%=Request["ProjectCode"]%>' width="100%" scrolling="no" frameborder="no"
													height="100%"></iframe>
											</td>
											<td vAlign="top" style="padding: 0 0 0 9px;">
												<iframe id="PAListFrame" src="" frameBorder="no" width="100%" height="100%" scrolling="auto">
												</iframe>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
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
			<input id="txtBuildingCode" type="hidden" runat="server"> <input id="txtAlloType" runat="server" type="hidden">
			<input id="txtBuildingName" runat="server" type="hidden">
		</form>
		<script language="javascript">

	//var CurrUrl = window.location.href;


	//document.all("PAListFrame").src = '../Finance/PAFrame.aspx?AlloType='+alloType + '&BuildingCode=' + buildingCode  ;
	
	//选择供应商
	function SelectSupplier()
	{
		OpenLargeWindow("../SelectBox/SelectSupplier.aspx", "选择供应商");
	}

	//选择供应商返回
	function DoSelectSupplierReturn(code, name)
	{
		Form1.txtSupplyName.value = name;
	}

	//分摊
	function doSearch()
	{
		
		var payoutTypeCode =  document.all("inputSystemGroup:txtInput").value ;
		var alloType = Form1.txtAlloType.value;
		var buildingCode = Form1.txtBuildingCode.value;
		var buildingName = Form1.txtBuildingName.value;
		var contractName = Form1.txtContractName.value;
		var contractID = Form1.txtContractID.value;
		var isApportioned = "";
		var notIsApportioned = "";
		if ( Form1.chkIsApportioned.checked )
			isApportioned = "1";
		if ( Form1.chkNotApportioned.checked )
			notIsApportioned = "1";
			
		var isInType = "";
		if ( Form1.chkSearch.checked )
			isInType = "1";
		
		var supplierName = Form1.txtSupplyName.value;
		var payer = Form1.txtPayer.value;
		var projectCode='<%=Request["ProjectCode"]%>';
		document.all("PAListFrame").src = '../Finance/PayoutApportion.aspx?ProjectCode='+projectCode+'&AlloType='+alloType + '&BuildingCode=' + buildingCode + '&BuildingName=' + escape(buildingName) + '&PayoutTypeCode='+payoutTypeCode +'&contractName=' + escape(contractName) +'&ContractID=' + escape(contractID) +'&SupplierName='+escape(supplierName) + '&Payer=' + escape(payer) + '&isApportioned='+isApportioned+'&notIsApportioned=' + notIsApportioned + '&isInType='+isInType ;

	}

	function selectBuildingNode(inputAlloType,inputBuildingCode,inputBuildingName)
	{
		//alloType=inputAlloType;
		//buildingCode = inputBuildingCode;
		
		Form1.txtAlloType.value = inputAlloType;
		Form1.txtBuildingCode.value = inputBuildingCode;
		Form1.txtBuildingName.value = inputBuildingName;
		
		doSearch();
	}

	function doPayoutApportion()
	{
		document.all("PAListFrame").contentWindow.doPayoutApportion();
	}

//搜索条件按回车
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		Form1.btnSearch.click();
	}
}
		</script>
	</body>
</HTML>
