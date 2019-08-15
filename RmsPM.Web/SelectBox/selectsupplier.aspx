<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectSupplier" CodeFile="SelectSupplier.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputDictItem" Src="../UserControls/InputDictItem.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ����</title>
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ����</td>
				</tr>
				<tr>
					<td class="table" vAlign="top">
						<table class="search-area" height="1" cellSpacing="0" cellPadding="0" border="0" onkeydown="SearchKeyDown();">
							<tr>
								<td>
									<table>
										<tr>
											<td>���ƣ�<input class="input" id="txtSupplierName" type="text" size="12" name="txtSupplierName"
													runat="server"> &nbsp;&nbsp;������<input class="input" id="txtAreaCode" type="text" size="8" name="txtAreaCode" runat="server">
												&nbsp;&nbsp;���ʣ�<input class="input" id="txtQuality" type="text" size="8" name="txtQuality" runat="server">
												&nbsp;&nbsp;<input id="chkSearch" type="checkbox" value="0" runat="server" NAME="chkSearch" checked>
												�ڷ����в��� &nbsp;&nbsp;&nbsp; <INPUT class="submit" id="btnSearch" type="button" value="�� ��" runat="server" onclick="doSearch();"
													NAME="btnSearch"> &nbsp;<img src="../images/search_more.gif" title="�߼���ѯ" style="CURSOR:hand" id="imgAdvSearch"
													onclick="ShowAdvSearch();">  <INPUT class="submit" id="btnAddNewSupplier" type="button" value="������Ӧ��" name="Button1"
													onclick="doNewSupplier('');" runat="server"> <INPUT class="submit" id="btnResetSupplier" type="button" value="���" name="btnResetSupplier"
													onclick="ResetSupplier();" runat="server">
											</td>
										</tr>
									</table>
									<table style="DISPLAY:none" id="divAdvSearch">
										<tr>
											<TD>��ƣ�<input class="input" id="txtAbbreviation" type="text" size="12" name="txtAbbreviation"
													runat="server"> &nbsp;&nbsp;ע���ַ��<INPUT class="input" id="txtRegisteredAddress" type="text" size="12" name="txtRegisteredAddress"
													runat="server"> &nbsp;&nbsp;��ϵ�ˣ�<INPUT class="input" id="txtContractPerson" type="text" size="8" name="txtContractPerson"
													runat="server">&nbsp;&nbsp;�������õȼ���<INPUT class="input" id="TxtCreditLevel" type="text" size="8" name="TxtCreditLevel"
																runat="server">
											</TD>
										</tr>
										<tr>
											<td>��ҵ���ʣ�<INPUT class="input" id="txtIndustryType" type="text" size="12" name="txtIndustryType"
													runat="server"> &nbsp;&nbsp;ҵ����<INPUT class="input" id="txtAchievement" type="text" size="12" name="txtAchievement" runat="server">
												&nbsp;&nbsp;���������<INPUT class="input" id="txtCheckOpinion" type="text" size="12" name="txtCheckOpinion"
													runat="server">&nbsp;&nbsp;Ʒ�����<INPUT class="input" id="TxtcharacterType" type="text" size="8" name="TxtcharacterType"
																runat="server">
											</td>
										</tr>
									    <tr>
										    <td>������ʽ�� <uc1:InputDictItem id="txtSellForm" runat="server" DictName="������ʽ"></uc1:InputDictItem>&nbsp;&nbsp;ccc��֤��<select class="select" runat="server" id="cccAttestation" name="cccAttestation">
									            <option value="" selected>--��ѡ��--</option>
									            <option value="1">��</option>
									            <option value="0">��</option>
									            </select>
											    &nbsp;&nbsp;iso��֤��<select class="select" runat="server" id="isoAttestation" name="isoAttestation">
									            <option value="" selected>--��ѡ��--</option>
									            <option value="1">��</option>
									            <option value="0">��</option>
									            </select>&nbsp;&nbsp;���ʵȼ���<uc1:InputDictItem id="QualityLevel" runat="server" DictName="���ʵȼ�"></uc1:InputDictItem>
										    </td>
									    </tr>
										
									    <tr>
									        <td>�������<select class="select" runat="server" id="sltIsExistsContract" name="sltIsExistsContract">
									            <option value="" selected>--��ѡ��--</option>
									            <option value="1">��</option>
									            <option value="0">��</option>
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
		����˵�����˷�����ӵ�115-132����Ϊ�������ѡ��Ӧ��ʱ���ṩ���������С������ʺš��ܿ���
		����˷���JustForContratPayment 311�У�Ϊ����չʹ�ÿ����ڴ���չ��
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
			OpenLargeWindow('../Supplier/SupplierModify.aspx?supplierTypeCode=' + supplierTypeCode ,"������Ӧ��")
		}
		
	function GotoList(typeCode)
	{
		Form1.txtSelectSupplierTypeCode.value = typeCode;
		supplierTypeCode = typeCode;
		
		if (Form1.chkSearch.checked) //�����࣫��ѯ������
		{
			doSearch();
		}
		else //ֻ�������
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
		if (Form1.txtAdvSearch.value == "none") //�򵥲�ѯ
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
		else //�߼���ѯ
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

//�����������س�
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		Form1.btnSearch.click();
	}
}

//�߼���ѯ
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
		Form1.imgAdvSearch.title = "�߼���ѯ";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		Form1.imgAdvSearch.title = "���ظ߼���ѯ";
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
