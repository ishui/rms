<%@ Page language="c#" Inherits="RmsPM.Web.Supplier.Supplier" CodeFile="Supplier.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputDictItem" Src="../UserControls/InputDictItem.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>厂商列表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
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
									厂商管理 - 厂商列表</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><input class="button" id="btnNew" onclick="doNewSupplier('');return false;" type="button"
							value="新增" name="btnNew" runat="server"> <input class="button" id="btnInputSupplier" onclick="ImportSupl('');return false;" type="button"
							value="导入厂商" name="btnNew" runat="server">
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
														<td>名称：<input class="input" id="txtSupplierName" type="text" size="12" name="txtSupplierName"
																runat="server"> &nbsp;&nbsp;地区：<input class="input" id="txtAreaCode" type="text" size="8" name="txtAreaCode" runat="server">
															&nbsp;&nbsp;资质：<input class="input" id="txtQuality" type="text" size="8" name="txtQuality" runat="server">
															&nbsp;&nbsp;<input id="chkSearch" type="checkbox" value="0" runat="server" checked> 在分类中查找 
															&nbsp;&nbsp;&nbsp; <INPUT class="submit" id="btnSearch" type="button" value="搜 索" runat="server" onclick="doSearch();">
															&nbsp;<img src="../images/search_more.gif" title="高级查询" style="CURSOR:hand" id="imgAdvSearch"
																onclick="ShowAdvSearch();">
														</td>
													</tr>
												</table>
												<table style="DISPLAY:none" id="divAdvSearch">
													<tr>
														<TD>简称：<input class="input" id="txtAbbreviation" type="text" size="12" name="txtAbbreviation"
																runat="server"> &nbsp;&nbsp;注册地址：<INPUT class="input" id="txtRegisteredAddress" type="text" size="12" name="txtRegisteredAddress"
																runat="server"> &nbsp;&nbsp;联系人：<INPUT class="input" id="txtContractPerson" type="text" size="8" name="txtContractPerson"
																runat="server">&nbsp;&nbsp;银行信用等级：<INPUT class="input" id="TxtCreditLevel" type="text" size="8" name="TxtCreditLevel"
																runat="server">
														</TD>
													</tr>
													<tr>
														<td>行业性质：<INPUT class="input" id="txtIndustryType" type="text" size="12" name="txtIndustryType"
																runat="server"> &nbsp;&nbsp;业绩：<INPUT class="input" id="txtAchievement" type="text" size="12" name="txtAchievement" runat="server">
															&nbsp;&nbsp;评价意见：<INPUT class="input" id="txtCheckOpinion" type="text" size="12" name="txtCheckOpinion"
																runat="server">&nbsp;&nbsp;品质类别：<INPUT class="input" id="TxtcharacterType" type="text" size="8" name="TxtcharacterType"
																runat="server">
														</td>
													</tr>
													<tr>
														<td>销售形式： <uc1:InputDictItem id="txtSellForm" runat="server" DictName="销售形式"></uc1:InputDictItem>&nbsp;&nbsp;ccc认证：<select class="select" runat="server" id="cccAttestation" name="cccAttestation">
													        <option value="" selected>--请选择--</option>
													        <option value="1">是</option>
													        <option value="0">否</option>
													        </select>
															&nbsp;&nbsp;iso认证：<select class="select" runat="server" id="isoAttestation" name="isoAttestation">
													        <option value="" selected>--请选择--</option>
													        <option value="1">是</option>
													        <option value="0">否</option>
													        </select>&nbsp;&nbsp;资质等级：<uc1:InputDictItem id="QualityLevel" runat="server" DictName="资质等级"></uc1:InputDictItem>
														</td>
													</tr>
													
													<tr>
													    <td>合作与否：<select class="select" runat="server" id="sltIsExistsContract" name="sltIsExistsContract">
													        <option value="" selected>--请选择--</option>
													        <option value="1">是</option>
													        <option value="0">否</option>
													        </select></td>
													</tr>
                                                    
													<tr runat="server" id="TrIsAuditted">
													    <td>审核状态：<input id="IsnotAuditted" type="checkbox" value="0"/>未审
													    <input id="IsAuditted" type="checkbox" />已审
													    <input id="Auditting" type="checkbox" />审核中</td>
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
											<td vAlign="top" width="28%">
												<iframe id="frameLeft" src="../Systems/ShowSystemGroupTree.aspx?ClassCode=1401&amp;MainFunc=GotoList&amp;ShowSortID=0"
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
			<input id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
			<input id="txtSelectSupplierTypeCode" type="hidden" runat="server"> <input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
		</form>
		<script language="javascript">
	var subjectSetCode = Form1.txtSubjectSetCode.value;
	var supplierTypeCode = '<%=Request["supplierTypeCode"]%>';
	var IsEmpty = "1";

	document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode + '&IsEmpty=' + IsEmpty  ;
	
	function doNewSupplier()
	{
		document.all("frameMain").contentWindow.doNewSupplier(supplierTypeCode,subjectSetCode);
		//var supplierTypeCode = Form1.txtSelectSupplierTypeCode.value;
		//OpenLargeWindow('SupplierModify.aspx?SupplierCode=&SubjectSetCode='+subjectSetCode + '&supplierTypeCode=' + supplierTypeCode ,"新增厂商")
	}



	function ImportSupl()
	{
		OpenCustomWindow("ImportSupplierDlg.aspx?SubjectSetCode=" + subjectSetCode,"导入厂商", 600, 480);
	}
	
	function GotoList(typeCode)
	{
		Form1.txtSelectSupplierTypeCode.value = typeCode;
		supplierTypeCode = typeCode;
	
		if (Form1.chkSearch.checked) //按分类＋查询条件查
		{
			doSearch();
		}
		else //只按分类查
		{
			document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode + '&chkSearch=1' ;
		}
	}

	function doSearch()
	{
		var supplierTypeCode = Form1.txtSelectSupplierTypeCode.value  ;
//       if(Form1.IsAuditted.checked==true)
//       alert(Form1.IsAuditted.value);
        var IsAuditted="";
        if(document.all("IsAuditted")!=null)
        {
            if(Form1.IsAuditted.checked==true)
            IsAuditted += "1";
            if(Form1.IsnotAuditted.checked==true&&Form1.IsAuditted.checked==true)
            IsAuditted += ",0";
            else if(Form1.IsnotAuditted.checked==true)
            IsAuditted +="0";
            if(Form1.Auditting.checked==true&&IsAuditted!="")
            IsAuditted += ",2";
            else if(Form1.Auditting.checked==true)
            IsAuditted +=  "2";
        }
		var chk = Form1.chkSearch.checked ;
		var sChk = "";
		if ( chk )
			sChk = "1";
			
		if (Form1.txtAdvSearch.value == "none") //简单查询
		{
			document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode
				+ '&SupplierName=' +escape(Form1.txtSupplierName.value)
				+ '&AreaCode=' +escape(Form1.txtAreaCode.value)
				+ '&Quality=' +escape(Form1.txtQuality.value)
				+ '&chkSearch=' +sChk;
		}
		else //高级查询
		{
		   var txtSellForm = GetObjectNameInControl("txtSellForm","txtSearchPlace");
		   var QualityLevel=GetObjectNameInControl("QualityLevel","txtSearchPlace");
			document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode
				+ '&SupplierName=' +escape(Form1.txtSupplierName.value)
				+ '&Abbreviation=' +escape(Form1.txtAbbreviation.value)
				+ '&AreaCode=' +escape(Form1.txtAreaCode.value)
				+ '&Quality=' +escape(Form1.txtQuality.value)
				+ '&RegisteredAddress=' +escape(Form1.txtRegisteredAddress.value)
				+ '&ContractPerson=' +escape(Form1.txtContractPerson.value)
				+ '&IndustryType=' +escape(Form1.txtIndustryType.value)
				+ '&Achievement=' +escape(Form1.txtAchievement.value)
				+ '&CheckOpinion=' +escape(Form1.txtCheckOpinion.value)
				+ '&CreditLevel=' +escape(Form1.TxtCreditLevel.value)
				+ '&CharacterType=' +escape(Form1.TxtcharacterType.value)
				+ '&IsExistsContract=' +escape(Form1.sltIsExistsContract.value)
				+ '&IsAuditted=' +IsAuditted
				+ '&chkSearch=' +sChk
				+ '&SellForm=' +escape(document.all(txtSellForm).value)
				+ '&cccAttestation=' +escape(Form1.cccAttestation.value)
				+ '&isoAttestation=' +escape(Form1.isoAttestation.value)
				+ '&QualityLevel=' +escape(document.all(QualityLevel).value)
				
				;
		}
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
