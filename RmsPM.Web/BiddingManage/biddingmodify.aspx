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
    <title>招投标信息</title>
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
                                招标计划</td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"/></td>
                            <td class="tools-area">
                                <input name="btnModify" id="btnModify" type="button" value=" 修改 " class="button"
                                    runat="server" onserverclick="btnModify_ServerClick"/>
                                <input name="btnSave" id="btnSave" type="button" value=" 保存 " class="button" runat="server"
                                    onserverclick="btnSave_ServerClick"/>
                                <input name="btnAddDtl" id="btnAddDtl" type="button" value=" 增加分标段 " class="button"
                                    runat="server" onserverclick="btnAddDtl_ServerClick"/>
                                <input name="btnDel" id="btnDel" type="button" value=" 删除 " class="button" runat="server"
                                    onserverclick="btnDel_ServerClick"/>
                                 <input name="btnOldCheckBiddingDiscuss" id="btnOldCheckBiddingDiscuss"  type="button" value="招标议标审核" class="button"
                                    runat="server"/>
                                 <input name="btnCheckBiddingDiscuss" id="btnCheckBiddingDiscuss" type="button" value="提交招标议标评审" class="button"
                                    runat="server"/>
                                <input name="btnCheckDiscuss" id="btnCheckDiscuss" type="button" value="提交议标评审" class="button"
                                    runat="server"/>   
                                <input name="btnNewSupply" id="btnNewSupply" type="button" value="新增投标单位评审" class="button"
                                    runat="server"/>
                                <input name="btnNewSupply" visible="false" id="btnBiddingStart" type="button" value="招标执行" class="button"
                                    runat="server"/>
                               
                                <input name="btnEmit" id="btnEmit" type="button" value=" 发标 " class="button" runat="server"/>
                                <input name="btnReturn" id="btnReturn" type="button" value=" 回标 " class="button"
                                    runat="server"/>
                                 
                                <input name="btnAuditing" id="btnAuditing" type="button" value=" 评标评审 " class="button"
                                    runat="server"/>
                               
                                <input class="button" id="Bt_LowOfPrice" type="button" value=" 压价 " name="btnAuditing"
                                    runat="server"/>
                                <input class="button" id="Bt_ReturnOfPrice" type="button" value=" 回价 "
                                        name="btnAuditing" runat="server"/>
                                <input name="btnMessage" id="btnMessage" type="button" value=" 中标通知书评审 " class="button"
                                    runat="server"/>
                               
                                <input name="btnContract" id="btnContract" type="button" value=" 创建合同 " class="button"
                                    runat="server"/>
                                <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
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
                                            标段信息</td>
                                        
                                        <td class="TabDisplay"  id="BiddingCondition" runat="server" nowrap  onclick="EventClickTab(1);">
                                            技术条件</td>
                                         <td class="TabDisplay" id="trPrejudication" runat="server"   nowrap onclick="EventClickTab(2);">
                                            单位预审</td>
                                         <td class="TabDisplay" id="trZBGC" runat="server" nowrap onclick="EventClickTab(3);">
                                            招标过程</td>
                                        <td class="TabDisplay" id="trHBGC" runat="server"   nowrap onclick="EventClickTab(4);">
                                            回标过程</td>
                                       
                                        <!-- <td class="TabDisplay" id="PrejudicationListdiv"   runat=server width="100" nowrap onclick="EventClickTab(4);">
                                            审批单列表</td>
                                        <td class="TabDisplay" id="ReturnListdiv"   runat=server width="100" nowrap onclick="EventClickTab(5);">
                                            评标列表</td>-->
                                        <td class="TabDisplay" id="tdBiddingMessage"  runat="server" nowrap onclick="EventClickTab(5);">
                                            中标通知书</td>
                                         <td class="TabDisplay" id="workflowmsg" runat="server"  nowrap onclick="EventClickTab(6);">
                                            相关流程</td>
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
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
		OpenLargeWindow('<%=BiddingDtlAddMoneyUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'增资预审');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
	}
   
     function OpenDiscuss(code,ApplicationCode,projectcode)
	{
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
		OpenLargeWindow('<%=DiscussAuditingUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode+'&ApplicationCode='+ApplicationCode,'投标单位预审');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
	}
    
    function OpenBiddingDiscuss(code,projectcode)
	{
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
		OpenLargeWindow('<%=BiddingDiscussAuditingUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'投标单位预审');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
	}

	function OpenNewSupply(code,projectcode)
	{
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
		OpenLargeWindow('<%=PrejudicationAuditingUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'投标单位预审');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
	}
	
	function OpenBiddingStart(code,projectcode)
	{
		//OpenLargeWindow('BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
		OpenLargeWindow('<%=PrejudicationAuditingUrl%>?ApplicationCode='+code+'&ProjectCode='+projectcode,'投标单位预审');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingPrejudicationManage.aspx?BiddingCode='+code,'投标单位预审');
	}
	function OpenNewEmit(code,projectcode)
	{
		OpenLargeWindow('<%=BiddingEmitManageUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode+'&State=edit','发标');
	}
	function OpenLowOfPrice(code,projectcode)
	{
		OpenLargeWindow('BiddingEmitManage.aspx?BiddingCode='+code+'&ProjectCode='+projectcode+'&State=edit&NowState=5','压价');
	}
	function ReturnLowOfPrice(code,projectcode)
	{
		//OpenLargeWindow('BiddingReturnQuery.aspx?BiddingCode='+code+'&NowState=5',回价');
		OpenFullWindow('BiddingReturnModifyPage.aspx?BiddingEmitCode='+code+'&ProjectCode='+projectcode+'&State=edit&NowState=6','回价');
	}
	function OpenReturnList(code,projectcode)
	{
		OpenFullWindow('BiddingReturnQuery.aspx?BiddingCode='+code+'&ProjectCode='+projectcode,'回标');
	}
	function OpenAuditing(code,projectcode)
	{
		OpenLargeWindow('<%=BiddingAuditingUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'评标');
		//OpenLargeWindow('../WorkFlowPage/YX_BiddingAuditingManage.aspx?BiddingCode='+code,'评标');
	}
function BiddingEmitListReturnModify(code,projectcode)
{
	OpenFullWindow('BiddingReturnModifyPage.aspx?BiddingEmitCode='+code+'&ProjectCode='+projectcode+'&State=edit','会标操作');
}
function BiddingMessage(code,projectcode)
{
	OpenLargeWindow('<%=BiddingMessageManageUrl%>?BiddingCode='+code+'&ProjectCode='+projectcode,'中标通知书');
}
function BiddingContract(code,projectCode)
{
	//OpenFullWindow('../Contract/ContractModify.aspx?BiddingCode='+code+"&act=Bidding&ProjectCode="+projectCode,'创建合同');
	OpenLargeWindow('BiddingContractCreate.aspx?BiddingCode='+code+"&act=Bidding&ProjectCode="+projectCode,'创建合同');
}
function BiddingLog(code)
{

	OpenLargeWindow('BiddingLogModif.aspx?Viewlast=1&BiddingCode=<%= Request["ApplicationCode"]+"" %>','招投标日志');
}

function OpenBiddingType(code)
{

	OpenSmallWindow('BiddingTypeModify.aspx?BiddingCode=<%= Request["ApplicationCode"]+"" %>','招投标类型');
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
