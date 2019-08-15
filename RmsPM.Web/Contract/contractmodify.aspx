<%@ Reference Control="~/usercontrols/contractcostmodify.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc3" TagName="CostModify" Src="../UserControls/ContractCostModify.ascx" %>
<%@ Register TagPrefix="uc3" TagName="CostPlanModify" Src="../UserControls/ContractCostPlanModify.ascx" %>
<%@ Register TagPrefix="uc3" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>

<%@ Page Language="c#" Inherits="RmsPM.Web.Contract.ContractModify" CodeFile="ContractModify.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>��ͬ��Ϣ</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript" src="../images/convert.js"></script>

    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript">
    var rptCostListCount=<%=ViewState["rptCostListCount"] %>;
<!--
function InfraMoneyValueChange(oEdit, oldValue, oEvent)
{
	InfraCalcSum();
}

function CalcStampDuty(){
   var sum=0;
   var i;
    for(i=0;i<=rptCostListCount-1;i++){
		sum= eval( "rptCostList__ctl"+i+"_ucCostModifySumRMB()")+sum;
//		alert(i+':'+sum);
    }
//    alert(sum);
if ( document.getElementById("StampDuty_t")!=null){     ///�жϿؼ��Ƿ���� 
	 document.getElementById("StampDuty_t").value=sum*get2(gettext(document.getElementById( "DropDownList1")));		}

}
function gettext(obj){
    return obj.options[obj.selectedIndex].text;
}

function get2(txt){
//	alert(txt);

	 var t= ConvertFloat(txt.split("|")[1]);
//	alert(t);
	return t;
	}
//����ϼ�
function InfraCostSum( sClintID )
{
	alert(sum);
	var c = parseInt(document.all(sClintID + "_dgCostList").rows.length) - 2;
	var tempMoney = 0;
	var sum = 0;
	
	for(i=0;i<c;i++)
	{
		tempMoney = ConvertFloat(document.all( sClintID + ":dgCostList:_ctl" + (i + 2) + ":txtMoney").value);
		sum = sum + tempMoney;
	}

	//��ʽ��
	sum = formatNumber(sum, "#,###.00");

	//document.all( sClintID + "_dgCostList__ctl" + (c + 2) + "_lblSumMoney").innerText = sum;
document.getElementById("StampDuty").value=sum*get2(gettext(document.getElementById("DropDownList1").value));
	//alert(sum);
}

function InfraValueChange(oEdit, oldValue, oEvent)
{
	InfraValueSum(0);
}

function InfraFactValueChange(oEdit, oldValue, oEvent)
{
	InfraValueSum(1);
}

//����ϼ�
function InfraValueSum(IsFact)
{
	alert(sum);
	var dgName;
	var lblName;
	var txtName;
	var c;
	var tempValue = 0;
	var sum = 0;
	
	if ( IsFact == 0 )
	{
		dgName = "dgValueList";
		lblName = "_lblSumValue";
		txtName = ":txtValue";
		c = parseInt(document.all.dgValueList.rows.length) - 2;
	}
	else
	{
		dgName = "dgFactValueList";
		lblName = "_lblSumFactValue";
		txtName = ":txtFactValue";
		c = parseInt(document.all.dgFactValueList.rows.length) - 2;
	}

	for(i=0;i<c;i++)
	{
		tempValue = ConvertFloat(document.all(dgName + ":_ctl" + (i + 2) + txtName).value);
		sum = sum + tempValue;
	}

	//��ʽ��
	sum = formatNumber(sum, "#,###.00");

	document.all(dgName + "__ctl" + (c + 2) + lblName).innerText = sum;
	
	//document.getElementById("StampDuty").value=sum*get2(gettext(document.getElementById("DropDownList1").value));

	//alert(sum);
}

function undoHidden()
{
	document.all("iframeSave").style.display = "none";
	document.all("tableMain").style.display = "";
}

function doSelectSupplier()
{
	var supplierCode = Form1.txtSupplierCode.value;
	OpenLargeWindow('../SelectBox/SelectSupplier.aspx?SupplierCode=' + supplierCode ,'ѡ��Ӧ��');
}

function doSelectSupplier2()
{
	var supplierCode = Form1.txtSupplier2Code.value;
	OpenLargeWindow('../SelectBox/SelectSupplier.aspx?SupplierCode=' + supplierCode + '&returnFunctionName=1' ,'ѡ��Ӧ��');
}

function DoSelectSupplierReturn ( code,name )
{
	Form1.txtSupplierCode.value = code;
	Form1.txtSupplierName.value = name;
}

function DoSelectSupplierReturn1 ( code,name )
{
	Form1.txtSupplier2Code.value = code;
	Form1.txtSupplier2Name.value = name;
}

function DoSelectUser(userCode,userName,flag)
{
	// ѡ��ͬ������
	if ( flag == 0 )
	{
		Form1.txtContractPerson.value = userCode;
		Form1.txtContractPersonName.value = userName;
	}
}

function SelectContractPerson()
{
	OpenMiddleWindow('../SelectBox/SelectPerson.aspx?Flag=0&Type=Single','ѡ���û�');
}

//�޸ĸ�������
function ModifyPayCondition(ConditionCode, AllocateCode)
{
	Form1.txtConditionAllocateCode.value = AllocateCode;
	OpenCustomWindow("ContractPayConditionModify.aspx?ContractCode=" + Form1.txtContractCode.value + "&AllocateCode=" + AllocateCode + "&ConditionCode=" + ConditionCode, "�޸ĸ�������", 400, 260);
}

//������������
function AddPayCondition(AllocateCode)
{
	Form1.txtConditionAllocateCode.value = AllocateCode;
	OpenCustomWindow("ContractPayConditionModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=" + Form1.txtContractCode.value + "&AllocateCode=" + AllocateCode, "�޸ĸ�������", 400, 260);
}

//�޸ĸ�����������
function PayConditionReturn(sPayDate)
{
	Form1.txtConditionPayDate.value = sPayDate;
	Form1.btnPayConditionReturn.click();
}
	
//��ʾ������Ϣ
function OpenTask(WBSCode)
{
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}

//ѡ������
function SelectDetailTask()
{
	Form1.txtSelectTaskFlag.value = "1";
	OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=1&WBSCode=");
}

//ѡ������
function SelectTaskReturn(code, name)
{
	if (Form1.txtSelectTaskFlag.value == "1")
	{
		Form1.txtAddTaskCode.value = code;
		Form1.btnAddTaskReturn.click();;
	}
	else
	{
	}
}

//���ɸ���ƻ�
function BuildPlan( code )
{
	var ContractCode = Form1.txtContractCode.value ;
	var url = "../Contract/ContractPaymentPlanModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&Act=<%=Request["Act"]%>";
	
	url += "&ContractCode=" + ContractCode;
	url += "&ContractCostCode=" + code;
	
	OpenMiddleWindow(url);
}

//���ɸ���ƻ�
function Page_Reload( )
{
	Form1.btnReload.click();
}
//-->
    </script>

    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <input id="btnPayConditionReturn" type="button" value="btnPayConditionReturn" name="btnPayConditionReturn"
                runat="server" onserverclick="btnPayConditionReturn_ServerClick">
            <input id="btnAddTaskReturn" type="button" value="btnAddTaskReturn" name="btnAddTaskReturn"
                runat="server" onserverclick="btnAddTaskReturn_ServerClick">
            <input id="btnReload" type="button" value="btnReload" name="btnReload" runat="server"
                onserverclick="btnReload_ServerClick">
        </div>
        <table id="tableMain" height="100%" cellspacing="0" cellpadding="0" width="100%"
            bgcolor="#ffffff" border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    <asp:Label ID="lblTitle" runat="server" BackColor="Transparent">��ͬ��Ϣ</asp:Label></td>
            </tr>
            <tr height="100%">
                <td valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="form-item">
                                    ��Ŀ��</td>
                                <td>
                                    <input class="input" id="txtProjectName" readonly type="text" name="txtProjectName"
                                        runat="server" /></td>
                                <td class="form-item">
                                    <asp:label runat="server" ID="lblMarkSegmentTitle">��</asp:label>��</td>
                                <td>
                                    <input class="input" id="txtMarkSegment" type="text" name="txtMarkSegment" runat="server" /></td>
                                <td class="form-item" id="tdGroupNameHint" runat="server">
                                    <asp:label runat="server" ID="lblGroupNameTitle">���ţ����Σ�</asp:label>��</td>
                                <td>
                                    <input class="input" id="txtGroupName" type="text" name="txtGroupName" runat="server"></td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    ����¥���ţ���</td>
                                <td>
                                    <input class="input" id="txtBuilding" type="text" name="txtBuilding" runat="server"></td>
                                <td class="form-item">
                                    ��Ч���ڣ�</td>
                                <td>
                                    <cc3:Calendar ID="dtContractDate" runat="server" Value="" Display="True" ReadOnly="False"
                                        CalendarResource="../Images/CalendarResource/">
                                    </cc3:Calendar>
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkMostly" runat="server" Text="��Ҫ���"></asp:CheckBox></td>
                            </tr>
                            <tr>
                                <td class="form-item" width="120">
                                    ��ͬ���ƣ�</td>
                                <td>
                                    <input class="input" id="txtContractName" type="text" size="32" name="txtContractName"
                                        runat="server"><font color="red">*</font></td>
                                <td class="form-item" width="120">
                                    ��ͬ��ţ�</td>
                                <td>
                                    <input class="input" id="txtContractID" type="text" name="txtContractID" runat="server"></td>
                                <td class="form-item" width="120">
                                    ���ţ�</td>
                                <td>
                                    <uc2:InputUnit ID="ucUnit" runat="server"></uc2:InputUnit>
                                    <font color="red">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    ������λ��</td>
                                <td>
                                    <input class="input" id="txtDevelopUnit" type="text" size="32" name="txtDevelopUnit"
                                        runat="server">&nbsp;
                                </td>
                                <td class="form-item">
                                    ��ͬ���ͣ�</td>
                                <td colspan="3">
                                    <uc1:InputSystemGroup ID="inputSystemGroup" runat="server"></uc1:InputSystemGroup>
                                    <font color="red">*</font>
                                </td>
                            </tr>
                            <tr runat="server" id="st">
                                <td class="form-item">
                                    ˰Ŀ��</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" Style="width: 136px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="StampDutyData" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="DropDownListSource" TypeName="RmsPM.BLL.StampDuty"></asp:ObjectDataSource>
                                </td>
                                <td class="form-item">
                                    ӡ �� ˰
                                </td>
                                <td colspan="3">
                                    <igtxt:WebNumericEdit ID="StampDuty" runat="server" CssClass="infra-input-nember"
                                        JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                        MinDecimalPlaces="Two">
                                    </igtxt:WebNumericEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    �а��ˣ�</td>
                                <td>
                                    <uc1:InputSupplier ID="inputSupplier" runat="server" Visible="false"></uc1:InputSupplier>
                                    <input class="input" id="txtSupplierName" readonly type="text" size="32" name="txtSupplierName"
                                        runat="server"><font color="#ff0000">*</font>
                                    <input id="txtSupplierCode" style="width: 18px; height: 22px" type="hidden" name="txtSupplierCode"
                                        runat="server">
                                    <a onclick="doSelectSupplier();return false;" href="##">
                                        <img src="../images/ToolsItemSearch.gif" border="0"></a></td>
                                <td class="form-item">
                                    �ܳа���</td>
                                <td>
                                    <input class="input" id="txtSupplier2Name" readonly type="text" size="32" name="txtSupplier2Name"
                                        runat="server" />
                                    <input id="txtSupplier2Code" style="width: 18px; height: 22px" type="hidden" name="txtSupplier2Code"
                                        runat="server">
                                    <a onclick="doSelectSupplier2();return false;" href="##">
                                        <img src="../images/ToolsItemSearch.gif" border="0"></a>
                                </td>
                                <td class="form-item">
                                    �� �� �ˣ�</td>
                                <td>
                                    <input class="input-readonly" id="txtContractPersonName" readonly type="text" name="txtContractPersonName"
                                        runat="server">
                                    <input id="txtContractPerson" readonly type="hidden" name="txtContractPerson" runat="server">
                                    <a onclick="SelectContractPerson();return false;" href="#">
                                        <img src="../images/ToolsItemSearch.gif" border="0"></a>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    �� �� ����</td>
                                <td>
                                    <input class="input" id="txtThirdParty" type="text" size="32" name="txtThirdParty"
                                        runat="server"></td>
                                <td class="form-item">
                                    <asp:label runat="server" ID="lblBaohanTitle">����</asp:label>��</td>
                                <td>
                                    <igtxt:WebNumericEdit ID="txtBaohan" runat="server" CssClass="infra-input-nember"
                                        Width="100" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                        MinDecimalPlaces="Two">
                                    </igtxt:WebNumericEdit>
                                    &nbsp;(ԭ��)
                                </td>
                                <td class="form-item">
                                    <div id="divAdjustMoney" runat="server">�ݶ����/ָ����</div>
                                <td>
                                    <igtxt:WebNumericEdit ID="txtAdjustMoney" runat="server" CssClass="infra-input-nember"
                                        Width="100" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                        MinDecimalPlaces="Two">
                                    </igtxt:WebNumericEdit>
                                    <div id="divBudgetMoney" runat="server" style="display: none">
                                        <igtxt:WebNumericEdit ID="txtBudgetMoney" runat="server" CssClass="infra-input-nember"
                                            Width="100" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                            MinDecimalPlaces="Two">
                                        </igtxt:WebNumericEdit>
                                    </div>
                                    <span id="divAdjustMoney2" runat="server">&nbsp;(ԭ��)</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    �γɷ�ʽ��</td>
                                <td>
                                    <select id="sltCreateMode" style="width: 136px" name="sltCreateMode" runat="server">
                                        <option value="" selected>--------��ѡ��--------</option>
                                    </select>
                                    <font id="fotCreateMode" runat="server" visible="false" color="red">*</font>
                                </td>
                                <td class="form-item">
                                    ���ڣ�</td>
                                <td>
                                    <input class="input" id="txtWorkTime" type="text" size="28" name="txtWorkTime" runat="server"></td>
                                <td class="form-item">
                                    <div id="divPerformingCircs" runat="server">���������</div>
                                </td>
                                <td>
                                    <select id="sltPerformingCircs" style="width: 136px" runat="server">
                                        <option value="" selected>--------��ѡ��--------</option>
                                    </select>
                                </td>
                            </tr>
                            <tr id="trAdIssueDate" runat="server">
                                <td class="form-item">��淢�����ڣ�</td>
                                <td colspan="5">
                                    <cc3:Calendar ID="dtAdIssueDate" runat="server" Value="" Display="True" ReadOnly="False"
                                        CalendarResource="../Images/CalendarResource/">
                                    </cc3:Calendar>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    �а���Χ��</td>
                                <td colspan="5">
                                    <textarea id="txtContractArea" style="width: 100%" name="txtContractArea" rows="2"
                                        runat="server"></textarea>
                                    <asp:Label ID="lblTextAreaHint0" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    ��ͬ������</td>
                                <td colspan="5">
                                    <textarea id="txtContractObject" style="width: 100%" name="txtContractObject" rows="2"
                                        runat="server"></textarea>
                                    <asp:Label ID="lblTextAreaHint1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="lblOldMoney" runat="server">
                                </td>
                                <td colspan="3">
                                    <igtxt:WebNumericEdit ID="txtOldMoney" runat="server" CssClass="infra-input-nember"
                                        Width="100" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                        MinDecimalPlaces="Two" Visible="False">
                                    </igtxt:WebNumericEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    �����ĵ���</td>
                                <td colspan="5">
                                    <uc1:AttachMentAdd ID="myAttachMentAdd" runat="server"></uc1:AttachMentAdd>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    ���ʽ��</td>
                                <td colspan="5">
                                    <textarea id="txtPayMode" style="width: 100%" name="txtPayMode" rows="2" runat="server"></textarea>
                                    <asp:Label ID="lblTextAreaHint2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    ����Ҫ��&nbsp;&nbsp;<br>
                                    ������Լ����</td>
                                <td colspan="5">
                                    <textarea id="txtQualityRequire" style="width: 100%" name="txtQualityRequire" rows="2"
                                        runat="server"></textarea>
                                    <asp:Label ID="lblTextAreaHint3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    ��ע��</td>
                                <td colspan="5">
                                    <textarea id="txtRemark" style="width: 100%" name="Textarea1" rows="2" runat="server"></textarea>
                                    <asp:Label ID="lblTextAreaHint4" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr height="5">
                                <td colspan="6">
                                </td>
                            </tr>
                            <tr id="trChange1" runat="server">
                                <td class="form-item">
                                    ���ԭ��</td>
                                <td colspan="5">
                                    <textarea id="txtChangeReason" style="width: 100%" name="txtChangeReason" rows="2"
                                        runat="server"></textarea>
                                </td>
                            </tr>
                            <tr id="trChange2" runat="server">
                                <td class="form-item">
                                    ������ݣ�</td>
                                <td colspan="5">
                                    <textarea id="txtChangeRemark" style="width: 100%" name="txtChangeRemark" rows="2"
                                        runat="server"></textarea></td>
                            </tr>
                        </table>

                        <%--�����￪ʼ--%>
<%--                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="13">
                                </td>
                                <td>
                                    <input class="submit" id="btnNewCostItem" type="button" value="����������ϸ" name="btnNewCostItem"
                                        runat="server" onserverclick="btnNewCostItem_ServerClick">
                                </td>
                            </tr>
                        </table>
                        <asp:Repeater ID="rptCostList" runat="server">
                            <ItemTemplate>
                                <uc3:CostModify ID="ucCostModify" runat="server" Index="<%# Container.ItemIndex + 1 %>"
                                    ContractCode='<%# DataBinder.Eval(Container, "DataItem.ContractCode") %>' ContractCostCode='<%# DataBinder.Eval(Container, "DataItem.ContractCostCode") %>'>
                                </uc3:CostModify>
                            </ItemTemplate>
                        </asp:Repeater>
                        <uc3:CostPlanModify ID="ucCostPlanModify" runat="server"></uc3:CostPlanModify>
                        <div id="divPaymentDefine" runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        ����Լ��</td>
                                </tr>
                            </table>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item">
                                        �������ڣ�</td>
                                    <td>
                                        <cc3:Calendar ID="dtWorkStartDate" runat="server" Value="" Display="True" ReadOnly="False"
                                            CalendarResource="../Images/CalendarResource/">
                                        </cc3:Calendar>
                                    </td>
                                    <td class="form-item">
                                        �������ڣ�</td>
                                    <td colspan="3">
                                        <cc3:Calendar ID="dtWorkEndDate" runat="server" Value="" Display="True" ReadOnly="False"
                                            CalendarResource="../Images/CalendarResource/">
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        Ԥ���</td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <igtxt:WebNumericEdit ID="txtPerCash1" runat="server" CssClass="infra-input-nember"
                                                        Width="50" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        MinDecimalPlaces="One">
                                                    </igtxt:WebNumericEdit>
                                                </td>
                                                <td>
                                                    %</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="form-item" width="15%">
                                        ���տ</td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <igtxt:WebNumericEdit ID="txtPerCash2" runat="server" CssClass="infra-input-nember"
                                                        Width="50" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        MinDecimalPlaces="One">
                                                    </igtxt:WebNumericEdit>
                                                </td>
                                                <td>
                                                    %</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="form-item" width="15%">
                                        ���޿</td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <igtxt:WebNumericEdit ID="txtPerCash3" runat="server" CssClass="infra-input-nember"
                                                        Width="50" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        MinDecimalPlaces="One">
                                                    </igtxt:WebNumericEdit>
                                                </td>
                                                <td>
                                                    %</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                        <div id="divContractProduction" runat="server">
                            <br>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="50%" valign="top" style="border-right1: #ededed 3px dotted; padding-right: 14px">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    Լ����ֵ</td>
                                                <td>
                                                    <input class="submit" id="btnNewValueItem" type="button" value="����Լ����ֵ" name="btnNewValueItem"
                                                        runat="server" onserverclick="btnNewValueItem_ServerClick">
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                            <tr>
                                                <td>
                                                    <asp:DataGrid ID="dgValueList" onkeydown="if(event.keyCode==13) event.keyCode=9"
                                                        runat="server" CssClass="list" Width="100%" ShowFooter="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" PageSize="15">
                                                        <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                                        <Columns>
                                                            <asp:BoundColumn Visible="False" DataField="ContractProductionCode"></asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="���" ItemStyle-Width="80">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="����&lt;font color=red&gt;*&lt;/font&gt;" ItemStyle-Width="150"
                                                                FooterText="�ϼ�">
                                                                <ItemTemplate>
                                                                    <cc3:Calendar ID="dtProductionDate" runat="server" Display="True" ReadOnly="False"
                                                                        CalendarResource="../Images/CalendarResource/" Value='<%#  DataBinder.Eval(Container.DataItem, "ProductionDate")  %>'>
                                                                    </cc3:Calendar>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Լ����ֵ&lt;font color=red&gt;*&lt;/font&gt;" ItemStyle-Width="150">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                <ItemTemplate>
                                                                    <igtxt:WebNumericEdit Width="100" ID="txtValue" runat="server" MinDecimalPlaces="Two"
                                                                        CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.ProductionValue") %>'
                                                                        ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                                                        JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                                                                        <ClientSideEvents ValueChange="InfraValueChange"></ClientSideEvents>
                                                                    </igtxt:WebNumericEdit>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumValue" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                                HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
                                                        </Columns>
                                                    </asp:DataGrid></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="50%" valign="top">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    ʵ�ʲ�ֵ</td>
                                                <td>
                                                    <input class="submit" id="btnNewFactValueItem" type="button" value="����ʵ�ʲ�ֵ" name="btnNewFactValueItem"
                                                        runat="server" onserverclick="btnNewFactValueItem_ServerClick"></td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                            <tr>
                                                <td>
                                                    <asp:DataGrid ID="dgFactValueList" onkeydown="if(event.keyCode==13) event.keyCode=9"
                                                        runat="server" CssClass="list" Width="100%" ShowFooter="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" PageSize="15">
                                                        <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                                        <Columns>
                                                            <asp:BoundColumn Visible="False" DataField="ContractProductionCode"></asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="���" ItemStyle-Width="80">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="����&lt;font color=red&gt;*&lt;/font&gt;" ItemStyle-Width="150"
                                                                FooterText="�ϼ�">
                                                                <ItemTemplate>
                                                                    <cc3:Calendar ID="dtFactProductionDate" runat="server" Display="True" ReadOnly="False"
                                                                        CalendarResource="../Images/CalendarResource/" Value='<%#  DataBinder.Eval(Container.DataItem, "ProductionDate")  %>'>
                                                                    </cc3:Calendar>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="ʵ�ʲ�ֵ&lt;font color=red&gt;*&lt;/font&gt;" ItemStyle-Width="150">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                <ItemTemplate>
                                                                    <igtxt:WebNumericEdit Width="100" ID="txtFactValue" runat="server" MinDecimalPlaces="Two"
                                                                        CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.ProductionValue") %>'
                                                                        ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                                                        JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                                                                        <ClientSideEvents ValueChange="InfraFactValueChange"></ClientSideEvents>
                                                                    </igtxt:WebNumericEdit>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumFactValue" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                                HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="intopic" width="200">
                                    ��ع���</td>
                                <td>
                                    <input class="submit" id="btnAddTask" onclick="SelectDetailTask();" type="button"
                                        value="�����ع���" name="btnAddTask" runat="server"></td>
                            </tr>
                        </table>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td colspan="2">
                                    <asp:DataGrid ID="dgTaskList" runat="server" CssClass="list" Width="100%" AllowSorting="True"
                                        AutoGenerateColumns="False" PageSize="15" CellPadding="2" GridLines="Horizontal"
                                        AllowPaging="False" DataKeyField="TaskContractCode">
                                        <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn DataField="WBSCode" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="���">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="��������">
                                                <ItemTemplate>
                                                    <a style="cursor: hand" onclick="OpenTask(this.val)" val='<%#  DataBinder.Eval(Container.DataItem, "WBSCode")%>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "TaskName")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="��ǰ����">
                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  RmsPM.BLL.StringRule.AddUnit(DataBinder.Eval(Container.DataItem, "CompletePercent"), "%")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="������">
                                                <ItemTemplate>
                                                    <%#  DataBinder.Eval(Container.DataItem, "UserNames")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid></td>
                            </tr>
                        </table>--%> 
                        <%--���������--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="9" width="100%">
                        <tr>
                            <td align="center">
                                <input class="submit" id="btnSave" type="button" value="�� ��" name="btnSave" runat="server"
                                    onserverclick="btnSave_ServerClick">
                                &nbsp;
                                <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button"
                                    value="ȡ ��" name="btnCancel" runat="server">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <iframe id="iframeSave" style="display: none" src="../Cost/SavingWating.htm" frameborder="no"
            width="100%" scrolling="auto" height="70%"></iframe>
        <input id="txtSelectCostItemIndex" type="hidden" name="txtSelectCostItemIndex" runat="server">
        <input id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
        <input id="txtConditionAllocateCode" type="hidden" name="txtConditionAllocateCode"
            runat="server"><input id="txtConditionPayDate" type="hidden" name="txtConditionPayDate"
                runat="server">
        <input id="txtAddTaskCode" type="hidden" name="txtAddTaskCode" runat="server">
        <input id="txtSelectTaskFlag" type="hidden" name="txtSelectTaskFlag" runat="server">
        <input id="hBeforeAccountTotalMoney" type="hidden" name="hBeforeAccountTotalMoney"
            runat="server"><input id="oldSupplier" type="hidden" name="oldSupplier" runat="server">
    </form>

    <script language="javascript">
<!--

undoHidden();

//-->
    </script>

    <script>
function MoneyTypeChanged(id)
{
  //ShowExchange(document.getElementById(id).value);
  //idlength=id.length
	//alert(id);
	myid = document.getElementById(id);   
	ShowExchange(myid.options[myid.selectedIndex].text,id);
	//alert(myid.options[myid.selectedIndex].text);
	//alert(myid.options[myid.selectedIndex].text);
}
function InputChange(id)
{
	CashChange();
}  
function CashChange(id)
{
   // alert("CashChange:");
    //e=window.event;
    //window
     //gaoyuan1.options[gaoyuan1.selectedIndex].
}
//��ʾĬ�ϻ�����Ϣ
function ShowExchange(moneyType,typeId)
{
        var idlength = typeId.length;
       // var str=typeId;  
        id=typeId.substring(0,idlength-2)+"_E";
        id2=typeId.substring(0,idlength-2)+"_F";
    	var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
		xmlDoc.async="false";
		var name = "ExchangeRate.xml";					
		xmlDoc.load(name);
		xmlObj=xmlDoc.documentElement;
		nodes = xmlDoc.documentElement.childNodes;
		lenght = xmlObj.childNodes.length;
		//id=id+"_"+id+"_M";
		//id2=id+"_"+id+"_V";
		
		
		
		for (var i=0;i<lenght;i++)
		{
			if(moneyType==xmlObj.childNodes(i).selectSingleNode("MoneyType").nodeTypedValue)
			{
			     //if(  "�м��"= xmlObj.childNodes(i).selectSingleNode("ExchangeRate").nodeTypedValue;)
			     document.getElementById(id).value = xmlObj.childNodes(i).selectSingleNode("RemittanceAverage").nodeTypedValue;
			    document.getElementById(id2).value = xmlObj.childNodes(i).selectSingleNode("RemittanceAverage").nodeTypedValue;
			     
			     return;
			}			
			else if(i==lenght-1)
			{
				document.getElementById(id).value = "00";	
				document.getElementById(id2).value = "00";			
						
			}
		}
		InputChange(typeId);
		
}
//���ʸı�
function ExchangeChanged(id)
{
}
function SumToRMB(id)
{
		
}

		//�����ڴ��ڵõ������ʱ��
		function Moneyonfocus(id)
		{
			//alert();
			//ResetMoney(pid);
		}
		//ʧȥ�����ʱ��
		function Moneyonblur(id)
		{
			SetMoney(id);
			ResetRMB(id);	
		}
		function JHshNumberText(id)
		{
			if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57)) 
			|| (window.event.keyCode == 13) || (window.event.keyCode == 46) 
			|| (window.event.keyCode == 45)))
			{
				window.event.keyCode = 0;
			}
		}
		function MoneyChange(id)
		{
			//SetMoney(pid);
			//ResetRMB(pid);			
		}
		function ResetRMB(id)
		{
			idlength=id.length;
			myvalue = (document.getElementById(id.substring(0,idlength-2)+"_V").value)*(document.getElementById(id.substring(0,idlength-2)+"_F").value);
			//alert(id);
			//ManyCurrency1_ManyCurrency1_R
			//alert(id+"_"+id+"_R");
			document.getElementById(id.substring(0,idlength-2)+"_R").innerHTML=myvalue;	
			//ManyCurrency1_ManyCurrency1_R.innerHTML						
		}
		function SetMoney(id)
		{
			//alert(id+"_"+id+"_E_"+id+"_"+id+"_E"+"_V");
			idlength=id.length;
			document.getElementById(id.substring(0,idlength-2)+"_V").value=document.getElementById(id.substring(0,idlength-2)+"_C").value;
			document.getElementById(id.substring(0,idlength-2)+"_F").value=document.getElementById(id.substring(0,idlength-2)+"_E").value
		}		
//�������͸ı�

    </script>

</body>
</html>
