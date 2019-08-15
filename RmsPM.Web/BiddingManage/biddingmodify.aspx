<%@ Page Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingModify" CodeFile="BiddingModify.aspx.cs" %>
<%@ Register Src="Control_BiddingEmitHistory.ascx" TagPrefix="uc1" TagName="Control_BiddingEmitHistory" %>
<%@ Register Src="BiddingSupplierList.ascx" TagName="BiddingSupplierList" TagPrefix="uc7" %>
<%@ Register Src="BiddingPrejudicationSupplierList.ascx" TagName="BiddingPrejudicationSupplierList" TagPrefix="uc6" %>
<%@ Register Src="BiddingConditionFileInfo.ascx" TagName="BiddingConditionFileInfo" TagPrefix="uc5" %>
<%@ Register Src="BiddingDtlModify.ascx" TagName="BiddingDtlModify" TagPrefix="uc4" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc3" %>
<%@ Register Src="ucBiddingWorkFlowLink.ascx" TagName="ucBiddingWorkFlowLink" TagPrefix="uc2" %>
<%@ Register Src="BiddingProcess.ascx" TagPrefix="uc1" TagName="BiddingProcess" %>
<%@ Register Src="BiddingPrejudicationList.ascx" TagPrefix="uc1" TagName="BiddingPrejudicationList" %>
<%@ Register Src="BiddingReturnList.ascx" TagPrefix="uc1" TagName="BiddingReturnList" %>
<%@ Register Src="Bidding.ascx"  TagPrefix="uc1" TagName="Bidding" %>
<%@ Register Src="BiddingMessageList.ascx"  TagPrefix="uc1" TagName="BiddingMessageList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>��Ͷ����Ϣ</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
    <meta name="CODE_LANGUAGE" content="C#"/>
    <meta name="vs_defaultClientScript" content="JavaScript"/>
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
    <link href="../Images/index.css" type="text/css" rel="stylesheet"/>
    <link href="../Images/infra.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../Rms.js"></script>
</head>
<body  onload="javascript:EventClickTab(document.getElementById('selectedTab').value);">
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                �б�ƻ�</td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"/></td>
                            <td class="tools-area">
                                <input name="btnModify" id="btnModify" type="button" value=" �޸� " class="button"
                                    runat="server" onserverclick="btnModify_ServerClick"/>
                                <input name="btnSave" id="btnSave" type="button" value=" ���� " class="button" runat="server"
                                    onserverclick="btnSave_ServerClick"/>
                                <input name="btnAddDtl" id="btnAddDtl" type="button" value=" ���ӷֱ�� " class="button"
                                    runat="server" onserverclick="btnAddDtl_ServerClick"/>
                                <input name="btnDel" id="btnDel" type="button" value=" ɾ�� " class="button" runat="server"
                                    onserverclick="btnDel_ServerClick"/>
                                 <input name="btnOldCheckBiddingDiscuss" id="btnOldCheckBiddingDiscuss"  type="button" value="�б�������" class="button"
                                    runat="server"/>
                                 <input name="btnCheckBiddingDiscuss" id="btnCheckBiddingDiscuss" type="button" value="�ύ�б��������" class="button"
                                    runat="server"/>
                                <input name="btnCheckDiscuss" id="btnCheckDiscuss" type="button" value="�ύ�������" class="button"
                                    runat="server"/>   
                                <input name="btnNewSupply" id="btnNewSupply" type="button" value="����Ͷ�굥λ����" class="button"
                                    runat="server"/>
                                <input name="btnNewSupply" visible="false" id="btnBiddingStart" type="button" value="�б�ִ��" class="button"
                                    runat="server"/>
                               
                                <input name="btnEmit" id="btnEmit" type="button" value=" ���� " class="button" runat="server"/>
                                <input name="btnReturn" id="btnReturn" type="button" value=" �ر� " class="button"
                                    runat="server"/>
                                 
                                <input name="btnAuditing" id="btnAuditing" type="button" value=" �������� " class="button"
                                    runat="server"/>
                               
                                <input class="button" id="Bt_LowOfPrice" type="button" value=" ѹ�� " name="btnAuditing"
                                    runat="server"/>
                                <input class="button" id="Bt_ReturnOfPrice" type="button" value=" �ؼ� "
                                        name="btnAuditing" runat="server"/>
                                <input name="btnMessage" id="btnMessage" type="button" value=" �б�֪ͨ������ " class="button"
                                    runat="server"/>
                               
                                <input name="btnContract" id="btnContract" type="button" value=" ������ͬ " class="button"
                                    runat="server"/>
                                <input name="btnClose" id="btnClose" type="button" value=" �ر� " class="button" runat="server"
                                    onclick="javascript:window.close();"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="table" valign="top">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                <uc1:Bidding ID="Bidding1" runat="server"></uc1:Bidding>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" >
                                    <tr id="webtabs">
                                        <td width="0">
                                        </td>
                                        <td class="TabShow" runat="server" id="BiddingDtl" nowrap onclick="EventClickTab(0);">
                                            �����Ϣ</td>
                                        
                                        <td class="TabDisplay"  id="BiddingCondition" runat="server" nowrap  onclick="EventClickTab(1);">
                                            ��������</td>
                                         <td class="TabDisplay" id="trPrejudication" runat="server"   nowrap onclick="EventClickTab(2);">
                                            ��λԤ��</td>
                                         <td class="TabDisplay" id="trZBGC" runat="server" nowrap onclick="EventClickTab(3);">
                                            �б����</td>
                                        <td class="TabDisplay" id="trHBGC" runat="server"   nowrap onclick="EventClickTab(4);">
                                            �ر����</td>
                                       
                                        <!-- <td class="TabDisplay" id="PrejudicationListdiv"   runat=server width="100" nowrap onclick="EventClickTab(4);">
                                            �������б�</td>
                                        <td class="TabDisplay" id="ReturnListdiv"   runat=server width="100" nowrap onclick="EventClickTab(5);">
                                            �����б�</td>-->
                                        <td class="TabDisplay" id="tdBiddingMessage"  runat="server" nowrap onclick="EventClickTab(5);">
                                            �б�֪ͨ��</td>
                                         <td class="TabDisplay" id="workflowmsg" runat="server"  nowrap onclick="EventClickTab(6);">
                                            �������</td>
                                    </tr>
                                </table>
                                <input type="hidden" runat="server" id="selectedTab" />
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                                   
                                    <tr style="display: block;">
                                        <td>
                                            <uc4:BiddingDtlModify ID="BiddingDtlModify1" runat="server"></uc4:BiddingDtlModify>
                                        </td>
                                    </tr>
                                    <tr id="BiddingContidionFile" runat="server" style="display: none;">
                                        <td>
                                            <uc5:BiddingConditionFileInfo id="BiddingConditionFileInfo1" runat="server">
                                            </uc5:BiddingConditionFileInfo>
                                        </td>
                                    </tr>
                                    
                                     <tr id="Tr1" runat="server" style="display: none;">
                                        <td>
                                            <uc7:BiddingSupplierList ID="BiddingSupplierList1" runat="server"  EnableViewState="true" />
                                        </td>
                                    </tr>
                                    <tr id="BiddingProcessDiv" runat="server" style="display: none;">
                                        <td>
                                            <uc1:BiddingProcess ID="BiddingProcess1" runat="server"></uc1:BiddingProcess>
                                        </td>
                                    </tr>
                                    <tr id="BiddingEmitDiv" runat="server" style="display: none;">
                                        <td>
                                            <uc1:Control_BiddingEmitHistory ID="Control_BiddingEmitHistory1" runat="server" Visible="False">
                                            </uc1:Control_BiddingEmitHistory>
                                        </td>
                                    </tr>
                                      <tr id="BiddingMessageDiv" runat="server" style="display: none;">
                                        <td>
                                            <uc1:BiddingMessageList  ID="BiddingMessageList1" runat="server" EnableViewState="true" ></uc1:BiddingMessageList>
                                        </td>
                                    </tr>
                                    <tr id="WorkFlowDiv" runat="server" style="display: none;">
                                        <td>
                                            <uc3:WorkFlowList ID="WorkFlowList1" runat="server" />
                                        </td>
                                    </tr>
                                   
                                   
                                    <!-- <tr id="BiddingPrejudicationDiv" runat=server style="display: none;">
                                        <td>
                                            <uc1:BiddingPrejudicationList ID="BiddingPrejudicationList1" runat=server />
                                        </td>
                                    </tr>
                                    <tr id="BiddingReturnDiv" runat=server style="display: none;">
                                        <td>
                                            <uc1:BiddingReturnList runat=server ID="BiddingReturnList1" />
                                        </td>
                                    </tr>
                                   <tr id="Tr3" visible=false runat=server style="display: none;">
                                        <td>
                                            <uc3:WorkFlowList ID="WorkFlowList4" runat="server" />
                                        </td>
                                    </tr>-->
                                </table>
                            </td>
                        </tr>
                      
                    </table>
               </td>
               </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="6" bgcolor="#e4eff6">
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">

     

     function OpenBiddingDtlAddMoney(code,projectcode)
	{
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
		OpenLargeWindow('<%=BiddingDtlAddMoneyUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'����Ԥ��');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
	}
   
     function OpenDiscuss(code,ApplicationCode,projectcode)
	{
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
		OpenLargeWindow('<%=DiscussAuditingUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode+'&ApplicationCode='+ApplicationCode,'Ͷ�굥λԤ��');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
	}
    
    function OpenBiddingDiscuss(code,projectcode)
	{
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
		OpenLargeWindow('<%=BiddingDiscussAuditingUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'Ͷ�굥λԤ��');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
	}

	function OpenNewSupply(code,projectcode)
	{
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
		OpenLargeWindow('<%=PrejudicationAuditingUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'Ͷ�굥λԤ��');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
	}
	
	function OpenBiddingStart(code,projectcode)
	{
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
		OpenLargeWindow('<%=PrejudicationAuditingUrl%>?ApplicationCode='+code+'&ProjectCode='+projectcode,'Ͷ�굥λԤ��');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'Ͷ�굥λԤ��');
	}
	function OpenNewEmit(code,projectcode)
	{
		OpenLargeWindow('<%=BiddingEmitManageUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode+'&State=edit','����');
	}
	function OpenLowOfPrice(code,projectcode)
	{
		OpenLargeWindow('BiddingEmitManage.aspx?BiddingCode='+code+'&ProjectCode='+projectcode+'&State=edit&NowState=5','ѹ��');
	}
	function ReturnLowOfPrice(code,projectcode)
	{
		//OpenLargeWindow('BiddingReturnQuery.aspx?BiddingCode='+code+'&NowState=5',�ؼ�');
		OpenFullWindow('BiddingReturnModifyPage.aspx?BiddingEmitCode='+code+'&ProjectCode='+projectcode+'&State=edit&NowState=6','�ؼ�');
	}
	function OpenReturnList(code,projectcode)
	{
		OpenFullWindow('BiddingReturnQuery.aspx?BiddingCode='+code+'&ProjectCode='+projectcode,'�ر�');
	}
	function OpenAuditing(code,projectcode)
	{
		OpenLargeWindow('<%=BiddingAuditingUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'����');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingAuditingManage.aspx?BiddingCode='+code,'����');
	}
function BiddingEmitListReturnModify(code,projectcode)
{
	OpenFullWindow('BiddingReturnModifyPage.aspx?BiddingEmitCode='+code+'&ProjectCode='+projectcode+'&State=edit','������');
}
function BiddingMessage(code,projectcode)
{
	OpenLargeWindow('<%=BiddingMessageManageUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'�б�֪ͨ��');
}
function BiddingContract(code,projectCode)
{
	//OpenFullWindow('../Contract/ContractModify.aspx?BiddingCode='+code+"&act=Bidding&ProjectCode="+projectCode,'������ͬ');
	OpenLargeWindow('BiddingContractCreate.aspx?BiddingCode='+code+"&act=Bidding&ProjectCode="+projectCode,'������ͬ');
}
function BiddingLog(code)
{

	OpenLargeWindow('BiddingLogModif.aspx?Viewlast=1&BiddingCode=<%= Request["ApplicationCode"]+"" %>','��Ͷ����־');
}

function OpenBiddingType(code)
{

	OpenSmallWindow('BiddingTypeModify.aspx?BiddingCode=<%= Request["ApplicationCode"]+"" %>','��Ͷ������');
}
function EventClickTab(tabindex)
{
    var objTable = document.all("tabdiv");
    var TabTr = document.all("webtabs");
    for(var i=0;i<objTable.rows.length;i++)
    {
        var objTableTr = objTable.rows[i];
        if(i==tabindex)
        {
            objTableTr.style.display = "block";
            TabTr.cells[i+1].className = "TabShow";
        }
        else
        {
            objTableTr.style.display = "none";
            TabTr.cells[i+1].className = "TabDisplay";
        }
    }
    var obj=document.getElementById("selectedTab");
    if(obj!=null)
    {
        obj.value=tabindex;
    }
    
}
    </script>

</body>
</html>
