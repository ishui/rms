<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Material.MaterialCost" CodeFile="MaterialCost.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>材料列表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Images/SplitPage.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" bgcolor="#ffffff">
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
									数据资料 - 造价数据库<asp:Label runat="server" ID="lblTitle"></asp:Label></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><input class="button" id="btnNew" onclick="doNewMaterialCost('');return false;" type="button"
							value="新增材料价格" name="btnNew" runat="server"> <input class="button" id="btnInputMaterialCost" onclick="Import('');return false;" type="button"
							value="导入材料价格" name="btnNew" runat="server">
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<table height="100%" width="100%">
							<tr>
								<td>
									<table class="search-area" height="1" cellSpacing="0" cellPadding="0" border="0" onkeydown="SearchKeyDown();">
										<tr>
											<td>
												<table>
													<tr>
														<td>描述：<input class="input" id="txtDescription" type="text" size="16" name="txtDescription"
																runat="server">&nbsp;&nbsp;项目：<input class="input" id="txtProject" type="text" size="12" name="txtProject"
																runat="server">
															&nbsp;&nbsp;<input id="chkSearch" type="checkbox" value="0" runat="server" checked> 在分类中查找 
															&nbsp;&nbsp;&nbsp; <INPUT class="submit" id="btnSearch" type="button" value="搜 索" runat="server" onclick="doSearch();">
															&nbsp;<img src="../images/search_more.gif" title="高级查询" style="CURSOR:hand" id="imgAdvSearch"
																onclick="ShowAdvSearch();">
														</td>
													</tr>
												</table>
												<table style="DISPLAY:none" id="divAdvSearch">
													<tr>
														<td>单价：<igtxt:webnumericedit Width="80" id="txtPrice0" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
											ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit>
										——<igtxt:webnumericedit Width="80" id="txtPrice1" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
											ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"></igtxt:webnumericedit>
											                &nbsp;&nbsp;单位：<input class="input" id="txtUnit" type="text" size="12" name="txtUnit"
																runat="server">&nbsp;&nbsp;地区：<input class="input" id="txtAreaCode" type="text" size="8" name="txtAreaCode" runat="server">
														</td>
													</tr>
													<tr>
													    <td>定标日期：<cc1:Calendar id="dtBiddingDate0" runat="server" CalendarResource="../Images/CalendarResource/"
											Value=""></cc1:Calendar>&nbsp;——
										<cc1:Calendar id="dtBiddingDate1" runat="server" CalendarResource="../Images/CalendarResource/"
											Value=""></cc1:Calendar>
													    </td>
													</tr>
													<tr>
													    <td>category：<input class="input" id="txtCategory" type="text" size="12" name="txtCategory"
																runat="server">&nbsp;&nbsp;description：<input class="input" id="txtDescriptionEn" type="text" size="12" name="txtDescriptionEn"
																runat="server"></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td>
									<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td vAlign="top" width="20%">
												<iframe id="frameLeft" src=""
													frameBorder="no" width="100%" scrolling="auto" height="100%"></iframe>
											</td>
											<td vAlign="top">
												<iframe id="frameMain" src="" frameBorder="no" width="100%" height="100%" scrolling="no">
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
			<input id="txtSelectMaterialTypeCode" type="hidden" runat="server"> <input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
			<input id="txtRootGroupName" type="hidden" name="txtRootGroupName" runat="server"><input id="txtRootGroupCode" type="hidden" name="txtRootGroupCode" runat="server">
		</form>
		<script language="javascript">
	var MaterialTypeCode = '<%=Request["MaterialTypeCode"]%>';
	var IsEmpty = "1";
	
	document.all("frameLeft").src = '../Systems/ShowSystemGroupTree.aspx?ClassCode=1411&MainFunc=GotoList&ShowSortID=0&RootGroupCode=' + Form1.txtRootGroupCode.value;

	document.all("frameMain").src = '../Material/MaterialCostListFrame.aspx?MaterialTypeCode='+MaterialTypeCode + '&IsEmpty=' + IsEmpty + '&RootGroupCode=' + Form1.txtRootGroupCode.value  ;
	
	function doNewMaterialCost()
	{
		document.all("frameMain").contentWindow.doNewMaterialCost(MaterialTypeCode);
	}

	function Import()
	{
		OpenCustomWindow("ImportMaterialCostDlg.aspx","导入材料价格", 600, 480);
	}
	
	function GotoList(typeCode)
	{
		Form1.txtSelectMaterialTypeCode.value = typeCode;
		MaterialTypeCode = typeCode;
	
		if (Form1.chkSearch.checked) //按分类＋查询条件查
		{
			doSearch();
		}
		else //只按分类查
		{
			document.all("frameMain").src = '../Material/MaterialCostListFrame.aspx?MaterialTypeCode='+MaterialTypeCode + '&chkSearch=1' ;
		}
	}

	function doSearch()
	{
		var MaterialTypeCode = Form1.txtSelectMaterialTypeCode.value  ;

		var chk = Form1.chkSearch.checked ;
		var sChk = "";
		if ( chk )
			sChk = "1";
			
		var href = '../Material/MaterialCostListFrame.aspx?MaterialTypeCode='+MaterialTypeCode
			+ '&Description=' +escape(Form1.txtDescription.value)
			+ '&Project=' +escape(Form1.txtProject.value)
			+ '&chkSearch=' +sChk
			+ '&RootGroupCode=' + escape(Form1.txtRootGroupCode.value)
			;

		if (Form1.txtAdvSearch.value != "none") //高级查询
		{
		    href = href + "&DescriptionEn=" + escape(Form1.txtDescriptionEn.value)
		         + "&Category=" + escape(Form1.txtCategory.value)
		         + "&Unit=" + escape(Form1.txtUnit.value)
		         + "&Price0=" + document.all("txtPrice0").value
		         + "&Price1=" + document.all("txtPrice1").value
		         + "&BiddingDate0=" + document.all("dtBiddingDate0").value
		         + "&BiddingDate1=" + document.all("dtBiddingDate1").value
		         + "&AreaCode=" +escape(Form1.txtAreaCode.value)
		        ;
		/*
			document.all("frameMain").src = '../Material/MaterialCostListFrame.aspx?MaterialTypeCode='+MaterialTypeCode
				+ '&Description=' +escape(Form1.txtDescription.value)
				+ '&Abbreviation=' +escape(Form1.txtAbbreviation.value)
				+ '&AreaCode=' +escape(Form1.txtAreaCode.value)
				+ '&Quality=' +escape(Form1.txtQuality.value)
				+ '&RegisteredAddress=' +escape(Form1.txtRegisteredAddress.value)
				+ '&ContractPerson=' +escape(Form1.txtContractPerson.value)
				+ '&IndustryType=' +escape(Form1.txtIndustryType.value)
				+ '&Achievement=' +escape(Form1.txtAchievement.value)
				+ '&CheckOpinion=' +escape(Form1.txtCheckOpinion.value)
				+ '&chkSearch=' +sChk;
				*/
		}
		
		document.all("frameMain").src = href;
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

//高级查询
function ShowAdvSearch()
{
	var display = Form1.txtAdvSearch.value;
	
	if ( display == "none" )
	{
		display = "block";
	}
	else
	{
		display = "none";
	}
	
	Form1.txtAdvSearch.value = display;
	
	SetAdvSearch();;
}

function SetAdvSearch()
{
	document.all("divAdvSearch").style.display = Form1.txtAdvSearch.value;

	if ( Form1.txtAdvSearch.value == "none" )
	{
//		Form1.imgAdvSearch.src = "../images/ArrowDown.gif";
		Form1.imgAdvSearch.title = "高级查询";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		Form1.imgAdvSearch.title = "隐藏高级查询";
	}
}

		</script>
	</body>
</HTML>
