<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectSupplier" CodeFile="SelectSupplier.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputDictItem" Src="../UserControls/InputDictItem.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择厂商</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择厂商</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table class="search-area" height="1" cellSpacing="0" cellPadding="0" border="0" onkeydown="SearchKeyDown();">
							<tr>
								<td>
									<table>
										<tr>
											<td>名称：<input class="input" id="txtSupplierName" type="text" size="12" name="txtSupplierName"
													runat="server"> &nbsp;&nbsp;地区：<input class="input" id="txtAreaCode" type="text" size="8" name="txtAreaCode" runat="server">
												&nbsp;&nbsp;资质：<input class="input" id="txtQuality" type="text" size="8" name="txtQuality" runat="server">
												&nbsp;&nbsp;<input id="chkSearch" type="checkbox" value="0" runat="server" NAME="chkSearch" checked>
												在分类中查找 &nbsp;&nbsp;&nbsp; <INPUT class="submit" id="btnSearch" type="button" value="搜 索" runat="server" onclick="doSearch();"
													NAME="btnSearch"> &nbsp;<img src="../images/search_more.gif" title="高级查询" style="CURSOR:hand" id="imgAdvSearch"
													onclick="ShowAdvSearch();">  <INPUT class="submit" id="btnAddNewSupplier" type="button" value="新增供应商" name="Button1"
													onclick="doNewSupplier('');" runat="server"> <INPUT class="submit" id="btnResetSupplier" type="button" value="清空" name="btnResetSupplier"
													onclick="ResetSupplier();" runat="server">
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
								<td vAlign="top" width="200">
									<iframe id="frameLeft" src="../Systems/ShowSystemGroupTree.aspx?ClassCode=1401&amp;MainFunc=GotoList&amp;ShowSortID=0"
										frameBorder="no" width="100%" scrolling="auto" height="100%"></iframe>
								</td>
								<td vAlign="top" class="table">
									<iframe id="frameMain" src="" frameBorder="no" width="100%" height="100%" scrolling="no">
									</iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
			<input id="txtSelectSupplierTypeCode" type="hidden" runat="server" NAME="txtSelectSupplierTypeCode">
			<input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
		</form>
		<script language="javascript">
		var subjectSetCode = document.all("txtSubjectSetCode").value;
		var supplierTypeCode = '<%=Request["supplierTypeCode"]%>';
		var IsEmpty = "1";
		/*
		更改说明，此方法添加的115-132行是为了配合在选供应商时候提供，开户银行、银行帐号、受款人
		添加了方法JustForContratPayment 311行，为了扩展使用可以在此扩展。
		*/
		var helpPage = '<%=Request["HelpPage"] %>';
		var fromPayment;
		if(helpPage=="PayMent")
		{
		    fromPayment = true;
		}
		else
		{
		    fromPayment = false;
		}
		if(fromPayment)
		{
		    document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?HelpPage=PayMent&From=Select&supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode + '&IsEmpty=' + IsEmpty + '&SupplierName=' + escape(Form1.txtSupplierName.value)+'&IsAuditted=1';
		}
		else
		{
		    document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?From=Select&supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode + '&IsEmpty=' + IsEmpty + '&SupplierName=' + escape(Form1.txtSupplierName.value)+'&IsAuditted=1';
		}
		
        function DoSelectSupplier(supplierCode,supplierName,ContractPerson,OfficePhone,WorkAddress)
        {
            if('<%=Request["returnFunctionName"]%>' == "")
            {
			    window.opener.DoSelectSupplierReturn(supplierCode,supplierName,ContractPerson,OfficePhone,WorkAddress);
			}
			else
			{
			    window.opener.DoSelectSupplierReturn1(supplierCode,supplierName,ContractPerson,OfficePhone,WorkAddress);
			
			}
			window.close();
        }
        function ResetSupplier()
        {
            if('<%=Request["returnFunctionName"]%>' == "")
            {
			    window.opener.DoSelectSupplierReturn("","","","","");
			}
			else
			{
			    window.opener.DoSelectSupplierReturn1("","","","","");
			
			}
			window.close();
        }
        
		function doNewSupplier(supplierTypeCode)
		{
			OpenLargeWindow('../Supplier/SupplierModify.aspx?supplierTypeCode=' + supplierTypeCode ,"新增供应商")
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
		if(!fromPayment)
		{
			document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?From=Select&supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode + '&chkSearch=1'+'&IsAuditted=1' ;
			}
			else
			{
			document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?HelpPage=PayMent&From=Select&supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode + '&chkSearch=1'+'&IsAuditted=1' ;
			}
		}
	}
		
	function doSearch()
	{
		var supplierTypeCode = Form1.txtSelectSupplierTypeCode.value  ;

		var chk = Form1.chkSearch.checked ;
		var sChk = "";
		if ( chk )
			sChk = "1";
				
		//Form1.btnRefreshSelectType.click();
		if (Form1.txtAdvSearch.value == "none") //简单查询
		{
		if(!fromPayment)
		{
			document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?From=Select&supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode+'&IsAuditted=1'
				+ '&SupplierName=' +escape(Form1.txtSupplierName.value)
				+ '&AreaCode=' +escape(Form1.txtAreaCode.value)
				+ '&Quality=' +escape(Form1.txtQuality.value)
				+ '&chkSearch=' +sChk;
				}
				else
				{
				document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?HelpPage=PayMent&From=Select&supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode+'&IsAuditted=1'
				+ '&SupplierName=' +escape(Form1.txtSupplierName.value)
				+ '&AreaCode=' +escape(Form1.txtAreaCode.value)
				+ '&Quality=' +escape(Form1.txtQuality.value)
				+ '&chkSearch=' +sChk;
				}
		}
		else //高级查询
		{
		    var txtSellForm = GetObjectNameInControl("txtSellForm","txtSearchPlace");
		    var QualityLevel=GetObjectNameInControl("QualityLevel","txtSearchPlace");
		    if(!fromPayment)
		    {
			document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?From=Select&supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode+'&IsAuditted=1'
				+ '&SupplierName=' +escape(Form1.txtSupplierName.value)
				+ '&Abbreviation=' +escape(Form1.txtAbbreviation.value)
				+ '&AreaCode=' +escape(Form1.txtAreaCode.value)
				+ '&Quality=' +escape(Form1.txtQuality.value)
				+ '&RegisteredAddress=' +escape(Form1.txtRegisteredAddress.value)
				+ '&ContractPerson=' +escape(Form1.txtContractPerson.value)
				+ '&IndustryType=' +escape(Form1.txtIndustryType.value)
				+ '&Achievement=' +escape(Form1.txtAchievement.value)
				+ '&CheckOpinion=' +escape(Form1.txtCheckOpinion.value)
				+ '&chkSearch=' +sChk
				+ '&CharacterType=' +escape(Form1.TxtcharacterType.value)
				+ '&CreditLevel=' +escape(Form1.TxtCreditLevel.value)
				+ '&IsExistsContract='+escape(Form1.sltIsExistsContract.value)
				+ '&SellForm=' +escape(document.all(txtSellForm).value)
				+ '&cccAttestation=' +escape(Form1.cccAttestation.value)
				+ '&isoAttestation=' +escape(Form1.isoAttestation.value)
				+ '&QualityLevel=' +escape(document.all(QualityLevel).value);
				}
				else
				{
				document.all("frameMain").src = '../Supplier/SupplierListFrame.aspx?PageHelp=PayMent&From=Select&supplierTypeCode='+supplierTypeCode + '&SubjectSetCode=' + subjectSetCode+'&IsAuditted=1'
				+ '&SupplierName=' +escape(Form1.txtSupplierName.value)
				+ '&Abbreviation=' +escape(Form1.txtAbbreviation.value)
				+ '&AreaCode=' +escape(Form1.txtAreaCode.value)
				+ '&Quality=' +escape(Form1.txtQuality.value)
				+ '&RegisteredAddress=' +escape(Form1.txtRegisteredAddress.value)
				+ '&ContractPerson=' +escape(Form1.txtContractPerson.value)
				+ '&IndustryType=' +escape(Form1.txtIndustryType.value)
				+ '&Achievement=' +escape(Form1.txtAchievement.value)
				+ '&CheckOpinion=' +escape(Form1.txtCheckOpinion.value)
				+ '&chkSearch=' +sChk
				+ '&CharacterType=' +escape(Form1.TxtcharacterType.value)
				+ '&CreditLevel=' +escape(Form1.TxtCreditLevel.value)
				+ '&IsExistsContract='+escape(Form1.sltIsExistsContract.value)
				+ '&SellForm=' +escape(document.all(txtSellForm).value)
				+ '&cccAttestation=' +escape(Form1.cccAttestation.value)
				+ '&isoAttestation=' +escape(Form1.isoAttestation.value)
				+ '&QualityLevel=' +escape(document.all(QualityLevel).value);
				}
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

function JustForContratPayment(code,name,bank,account,reciver)
{
    window.opener.SelectSupplierAddInitValue(code,name,bank,account,reciver);
    window.close();
}
		</script>
	</body>
</HTML>
