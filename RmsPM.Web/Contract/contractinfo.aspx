<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>

<%@ Page Language="c#" Inherits="RmsPM.Web.Contract.ContractInfo" CodeFile="ContractInfo.aspx.cs" %>

<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="uc1" TagName="WorkFlowList" Src="../WorkFlowControl/WorkFlowList.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ContractCostPlanView" Src="../UserControls/ContractCostPlanView.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputAccessRange" Src="../UserControls/InputAccessRange.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>合同信息</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript" src="../images/JoyBox.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" bgcolor="#ffffff"
            border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    合同概况</td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
                        type="button" value="删除" name="btnDelete" runat="server" onserverclick="btnDelete_Click">
                    <input class="button" id="btnModify" onclick="javascript:DoModify();return false; "
                        type="button" value="修改" name="btnModify" runat="server">
                    <input class="button" id="btnAuditingModify" onclick="javascript:DoModify();return false; "
                        type="button" value="审核后修改" name="btnAuditingModify" runat="server">
                    <input class="button" id="btnCheck" onclick="javascript:doCheck();return false; "
                        type="button" value="提交" name="btnCheck" runat="server">
                    <input class="button" id="btnOldCheck" onclick="javascript:doOldCheck();return false; "
                        type="button" value="审核" name="btnOldCheck" runat="server">
                    &nbsp;
                    <input class="button" id="btnContractID" onclick="doModifyContractID();return false;"
                        type="button" value="修改合同编号" name="btnContractID" runat="server">
                    <input class="button" id="btnChange" onclick="javascript:DoChange();return false;"
                        type="button" value="合同变更" name="btnChange" runat="server">
                    <input class="button" id="btnAccount" onclick="javascript:if(!window.confirm('合同结算后将不能再进行处理，确实要结算吗 ?')) return false;"
                        type="button" value="合同结算" name="btnAccount" runat="server">
                    <input class="button" id="btnAccountManage" onclick="javascript:DoAccount();return false;"
                        type="button" value="合同结算" name="btnAccountManage" runat="server" onserverclick="btnAccount_Click">
                    <input class="button" id="btnHistory" onclick="ShowHistory();return false;" type="button"
                        value="变更记录" name="btnAccount" runat="server">
                    <input class="button" id="btnPrint" onclick="DoPrint();return false;" type="button"
                        value="合同审批表打印" runat="server">
                    <input class="button" id="btnAccontPrint" onclick="DoAccountPrint();return false;"
                        type="button" value="结算审批表打印" runat="server">
                </td>
            </tr>
            <tr height="100%">
                <td valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <table style="padding-right: 7px" cellspacing="7" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td valign="top">
                                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="form-item" width="15%">
                                                合同名称：</td>
                                            <td width="18%">
                                                <asp:Label ID="LabelContractName" runat="server"></asp:Label></td>
                                            <td class="form-item" width="15%">
                                                合同编号：</td>
                                            <td width="18%">
                                                <asp:Label ID="LabelContractID" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                                    ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
                                            <td class="form-item" width="15%">
                                                项目名称：</td>
                                            <td width="19%">
                                                <asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                原合同金额：</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblBudgetMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                类型：</td>
                                            <td>
                                                <asp:Label ID="LabelType" runat="server"></asp:Label></td>
                                            <td class="form-item">
                                                招标项目：</td>
                                            <td id="tdBidding" runat="server">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                <div id="divAdjustMoney" runat="server">暂定金额/指定金额：</div>
                                            </td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType2"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblAdjustMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                承包人：</td>
                                            <td>
                                                <a id="hrefSupplier" onclick="doViewSupplier(); return false;" href="##" runat="server">
                                                </a>
                                                <input id="txtSupplierCode" type="hidden" name="txtSupplierCode" runat="server" />
                                            </td>
                                            <td class="form-item">
                                                总承包：</td>
                                            <td>
                                                <a id="hrefSupplier2" onclick="doViewSupplier2(); return false;" href="##" runat="server">
                                                </a>
                                                <input id="txtSupplier2Code" type="hidden" name="txtSupplier2Code" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                实际金额：</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType3"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblOriginalMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                第 三 方：</td>
                                            <td>
                                                <asp:Label ID="lblThirdParty" runat="server"></asp:Label></td>
                                            <td class="form-item">
                                                工期：</td>
                                            <td>
                                                <asp:Label ID="lblWorkTime" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                累计变更：</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType4"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblTotalChangeMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                标段：</td>
                                            <td>
                                                <asp:Label ID="lblMostly" runat="server"></asp:Label></td>
                                            <td class="form-item">
                                                部门：</td>
                                            <td>
                                                <asp:Label ID="lblUnitName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                估计最终价：</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType5"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblTotalMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                经 办 人：</td>
                                            <td>
                                                <asp:Label ID="LabelContractPerson" runat="server"></asp:Label></td>
                                            <td class="form-item">
                                                生效日期：</td>
                                            <td>
                                                <asp:Label ID="LabelContractDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                <asp:label runat="server" ID="lblBaohanTitle">保函</asp:label>：</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType6"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblBaoHan" runat="Server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                <asp:Label runat="server" ID="st1">印花税税目：</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStampDutyID" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                            <td class="form-item">
                                                <asp:Label runat="server" ID="st2">印花税金额：</asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblStampDuty" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                        </tr>
                                        <tr id="trAdIssueDate" runat="server">
                                            <td class="form-item">广告发布日期：</td>
                                            <td colspan="5"><asp:Label runat="server" ID="lblAdIssueDate"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                开发单位：</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblDevelopUnit" runat="Server"></asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr id="trPerformingCircs" runat="server">
                                            <td class="form-item">
                                                履行情况：</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblPerformingCircs" runat="Server"></asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                承包范围：</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblContractArea" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                合同概述：</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblContractObject" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                付款方式：</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblPayMode" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                质量要求&nbsp;&nbsp;<br>
                                                及保修约定：</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblQualityRequire" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                上传附件：</td>
                                            <td colspan="5">
                                                <uc1:AttachMentList ID="myAttachMentList" runat="server"></uc1:AttachMentList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                备注：</td>
                                            <td colspan="5">
                                                <asp:Label ID="LabelRemark" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                审 核 人：</td>
                                            <td>
                                                <asp:Label ID="lblCheckPerson" runat="Server"></asp:Label>&nbsp;</td>
                                            <td class="form-item">
                                                审核日期：</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblCheckDate" runat="Server"></asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                审核意见：</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblCheckOpinion" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr id="trAccessRange" runat="server" visible="false">
                                            <td class="form-item">
                                                参 与 人：
                                            </td>
                                            <td colspan="5">
                                                <uc1:inputaccessrange id="ucInputAccessRange" runat="server" ClassCode="0501" OperationCodes="050101">
                                                </uc1:inputaccessrange>
                                            </td>
                                        </tr>
                                    </table>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                款项明细</td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="dgCostList" runat="server" CssClass="list" Width="100%" CellPadding="0"
                                        GridLines="Horizontal" AllowPaging="false" PageSize="15" AutoGenerateColumns="False"
                                        AllowSorting="True" ShowFooter="True">
                                        <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                        <ItemStyle CssClass="list-i"></ItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="序号">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="费用项" Visible="False">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <uc1:InputCostBudgetDtl ID="ucCostBudgetDtl" runat="server" Visable="False"></uc1:InputCostBudgetDtl>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="单位工程">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPBSName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="费用项">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCostName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="币种金额">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <uc1:ExchangeRate ID="ucExchangeRate" runat="server"></uc1:ExchangeRate>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    合计金额（RMB）：
                                                    <asp:Label ID="lblSumCostMoney" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="已批/已批%">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAHCash" runat="server"></asp:Label>&nbsp;/
                                                    <asp:Label ID="lblAHCashPer" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSumAHMoney" runat="server"></asp:Label>&nbsp;/
                                                    <asp:Label ID="lblSumAHMoneyPer" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="已付">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAPCash" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSumAPMoney" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="未付">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUPCash" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSumUPMoney" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="已批/已批%（RMB）" Visible="false">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAHMoney" runat="server"></asp:Label>&nbsp;/
                                                    <asp:Label ID="lblAHMoneyPer" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="已付（RMB）" Visible="false">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAPMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="未付（RMB）" Visible="false">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUPMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="说明" Visible="False">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    <uc1:ContractCostPlanView ID="ucContractCostPlanView" runat="server"></uc1:ContractCostPlanView>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <div id="div_ChangeList" runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    变更记录</td>
                                            </tr>
                                        </table>
                                        <asp:DataGrid ID="dgChangeList" runat="server" CssClass="list" Width="100%" CellPadding="0"
                                            GridLines="Horizontal" AllowPaging="false" PageSize="15" AutoGenerateColumns="False"
                                            AllowSorting="True" OnItemDataBound="dgChangeList_ItemDataBound">
                                            <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                            <ItemStyle CssClass=""></ItemStyle>
                                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                                            <FooterStyle CssClass="list-title"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="编号"
                                                    ItemStyle-Width="60">
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href="#"  id="ALink" runat="server" onclick="DoViewChange(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "ContractChangeCode") %>'>
                                                            <%#  DataBinder.Eval(Container.DataItem, "ContractChangeCode") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="序号" ItemStyle-Width="30" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# Container.ItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="OriginalMoney" HeaderText="原实际金额（元）" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="TotalChangeMoney" HeaderText="累计已批变更金额（元）" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="期后累计已批变更（元）">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAuditedTotalChangeMoney" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="ChangeMoney" HeaderText="本次变更金额（元）" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="NewMoney" HeaderText="预计金额（元）" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="状态">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                    <ItemTemplate>
                                                        <%# RmsPM.BLL.ContractRule.GetContractChangeStatusName(  DataBinder.Eval(Container.DataItem, "Status").ToString() )%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="变更人">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                    <ItemTemplate>
                                                        <%# RmsPM.BLL.SystemRule.GetUserName(  DataBinder.Eval(Container.DataItem, "ChangePerson").ToString() )%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="ChangeDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                </asp:BoundColumn>
                                            </Columns>
                                            <PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
                                                CssClass="ListHeadTr"></PagerStyle>
                                        </asp:DataGrid><br>
                                    </div>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                付款累计表</td>
                                            <td>
                                                <input class="button-small" id="btnNewPaymentApply" onclick="DoAddNewPaymentApply();return false;"
                                                    type="button" value="合同请款" name="btnNewPaymentApply" runat="server">
                                                <input class="button-small" id="btnNewProductionPaymentApply" onclick="DoAddNewProductionPaymentApply();return false;"
                                                    type="button" value="工程量请款" name="btnNewProductionPaymentApply" runat="server">
                                                <input class="button-small" id="btnPaymentList" onclick="ViewPaymentList();return false;"
                                                    type="button" value="厂商请款记录表" name="btnPaymentList" runat="server">
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="dgPaymentList" runat="server" CssClass="list" Width="100%" CellPadding="2"
                                        GridLines="Horizontal" AllowPaging="False" PageSize="15" AutoGenerateColumns="False"
                                        AllowSorting="True" ShowFooter="True">
                                        <ItemStyle CssClass=""></ItemStyle>
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="编号"
                                                ItemStyle-Width="60" FooterText="合计：">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <a href="#" onclick="DoViewPayment(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "PaymentCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "PaymentID") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="序号" ItemStyle-Width="40" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="期" ItemStyle-Width="40">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Issue") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="本期请款" ItemStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.OldMoney", "{0:N}") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSumMoney" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="累计已批" ItemStyle-Width="100" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.AHMoney", "{0:N}") %>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="累计已批%" ItemStyle-Width="80" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPercentAHMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="累计应付" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalPaymentMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="累计应付%" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalPaymentMoneyPercent" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="结余" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUHMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="累计已付" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center" CssClass="LeftDoubleBorderTD"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right" CssClass="LeftDoubleBorderTD"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAPMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" CssClass="LeftDoubleBorderTD"></FooterStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="累计已付%" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPercentAPMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="本期付款" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.PayoutMoney", "{0:N}")%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="PayDate" HeaderText="最晚付款日期" DataFormatString="{0:yyyy-MM-dd}">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" Width="80" HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="StatusName" HeaderText="状态" ItemStyle-Width="50">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ApplyDate" HeaderText="申请日期" DataFormatString="{0:yyyy-MM-dd}">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" Width="80" HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="备注" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Remark" HeaderText="备注" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="已付（元）" Visible="False">
                                                <HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.TotalPayoutMoney", "{0:N}") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSumPayOutMoney" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
                                            CssClass="list-title"></PagerStyle>
                                    </asp:DataGrid>
                                    <div id="divContractBill" runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    收到发票</td>
                                                <td>
                                                    <input class="button-small" id="Button1" onclick="DoAddNewContractBillApply();return false;"
                                                        type="button" value="新增发票" name="btnNewContractBillApply" runat="server" /></td>
                                            </tr>
                                        </table>
                                        <asp:DataGrid ID="dgContractBillList" runat="server" AutoGenerateColumns="False"
                                            PageSize="14" AllowPaging="True" GridLines="Horizontal" CellPadding="0" Width="100%"
                                            CssClass="list" AllowSorting="true" ShowFooter="True" OnItemDataBound="dgContractBillList_ItemDataBound">
                                            <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                                            <FooterStyle CssClass="list-title"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="序号" FooterText="合计">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# Container.ItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="发票号">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href="##" onclick="doViewContractBillInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "Code") %>'>
                                                            <%# RmsPM.BLL.StringRule.TruncateString(  DataBinder.Eval(Container.DataItem, "BillNo"),8) %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="发票金额（元）">
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.BillMoney", "{0:N}") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblSumTotalBillMoney"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页"
                                                HorizontalAlign="Right" CssClass="ListHeadTr"></PagerStyle>
                                        </asp:DataGrid>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <cc1:GridPagination ID="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/"
                                                        DataGridId="dgContractBillList" OnPageIndexChange="GridPagination1_PageIndexChange">
                                                    </cc1:GridPagination>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divWorkFlowList" runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    相关流程</td>
                                            </tr>
                                        </table>
                                        <uc1:WorkFlowList ID="ucWorkFlowList" runat="server"></uc1:WorkFlowList>
                                    </div>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                合同文档</td>
                                            <td>
                                                <input class="button-small" id="btnDocument" onclick="DoAddNewDocument();return false;"
                                                    type="button" value="新增合同文档" name="btnDocument" runat="server">&nbsp;<input class="button-small"
                                                        id="btnRefreshDocument" style="display: none" type="button" value="加载文档触发" name="btnRefreshDocument"
                                                        runat="server" onserverclick="btnRefreshDocument_ServerClick"></td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="dgDocumentList" runat="server" CssClass="list" Width="100%" CellPadding="2"
                                        GridLines="Horizontal" AllowPaging="False" PageSize="15" AutoGenerateColumns="False"
                                        AllowSorting="True">
                                        <ItemStyle CssClass=""></ItemStyle>
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn DataField="DocumentCode" Visible="false"></asp:BoundColumn>
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="标题">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="#" id="Alink" runat="server" onclick="ShowDocument(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "DocumentCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "Title") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Author" HeaderText="作者"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="附件">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.DocumentRule.Instance().GetAttachListHtml("DocumentAttach", DataBinder.Eval(Container.DataItem, "DocumentCode").ToString()) %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:ButtonColumn HeaderText="删除" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                CommandName="Delete"></asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
                                            CssClass="list-title"></PagerStyle>
                                    </asp:DataGrid>
                                    <div id="divTaskList" runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    相关工作</td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:DataGrid ID="dgTaskList" runat="server" CssClass="list" Width="100%" CellPadding="2"
                                                        GridLines="Horizontal" AllowPaging="False" PageSize="15" AutoGenerateColumns="False"
                                                        AllowSorting="True" DataKeyField="TaskContractCode">
                                                        <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="序号">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="工作名称">
                                                                <ItemTemplate>
                                                                    <a style="cursor: hand" onclick="OpenTask(this.val)" val='<%#  DataBinder.Eval(Container.DataItem, "WBSCode")%>'>
                                                                        <%#  DataBinder.Eval(Container.DataItem, "TaskName")%>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="当前进度">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.StringRule.AddUnit(DataBinder.Eval(Container.DataItem, "CompletePercent"), "%")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="负责人">
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "UserNames")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:ButtonColumn Visible="False" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                                HeaderText="删除" CommandName="Delete"></asp:ButtonColumn>
                                                        </Columns>
                                                        <PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
                                                            CssClass="ListHeadTr"></PagerStyle>
                                                    </asp:DataGrid></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divContractProduction" runat="server">
                                        <br>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td width="50%" valign="top" style="border-right1: #ededed 3px dotted; padding-right: 14px">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td class="intopic" width="200">
                                                                约定产值</td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid ID="dgValueList" runat="server" CssClass="list" AllowSorting="True"
                                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" ShowFooter="False">
                                                                    <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                                                    <HeaderStyle CssClass="list-title"></HeaderStyle>
                                                                    <FooterStyle CssClass="list-title"></FooterStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn Visible="False" DataField="ContractProductionCode"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="序号">
                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# Container.ItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="日期" FooterText="合计">
                                                                            <ItemTemplate>
                                                                                <%#  DataBinder.Eval(Container.DataItem, "ProductionDate","{0:d}")  %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="约定产值">
                                                                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label Width="100" ID="lblValue" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.ProductionValue","{0:N}") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblSumValue" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="累计约定产值">
                                                                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label Width="100" ID="lblValue2" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.TotalProductionValue","{0:N}") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:ButtonColumn Visible="False" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                                            HeaderText="删除" CommandName="Delete"></asp:ButtonColumn>
                                                                    </Columns>
                                                                </asp:DataGrid></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="50%" valign="top">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td class="intopic" width="200">
                                                                实际产值</td>
                                                            <td>
                                                                <input class="button-small" id="btnModifyFactValue" onclick="doModifyFactValue();return false;"
                                                                    type="button" value="修改实际产值" name="btnModifyFactValue" runat="server">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid ID="dgFactValueList" runat="server" CssClass="list" AllowSorting="True"
                                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" ShowFooter="False">
                                                                    <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                                                    <HeaderStyle CssClass="list-title"></HeaderStyle>
                                                                    <FooterStyle CssClass="list-title"></FooterStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn Visible="False" DataField="ContractProductionCode"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="序号">
                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# Container.ItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="日期" FooterText="合计">
                                                                            <ItemTemplate>
                                                                                <%#  DataBinder.Eval(Container.DataItem, "ProductionDate","{0:d}")  %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="实际产值">
                                                                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label Width="100" ID="lblFactValue" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.ProductionValue","{0:N}") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblSumFactValue" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="累计实际产值">
                                                                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label Width="100" ID="lblFactValue2" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.TotalProductionValue","{0:N}") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:ButtonColumn Visible="False" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                                            HeaderText="删除" CommandName="Delete"></asp:ButtonColumn>
                                                                    </Columns>
                                                                </asp:DataGrid></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divMaterial" runat="server">
                                        <br>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    材料需求</td>
                                                <td>
                                                    <input class="button-small" id="btnModifyMaterial" onclick="doModifyMaterial();return false;"
                                                        type="button" value="修改材料需求" name="btnModifyContractMaterial" runat="server">
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                            <tr>
                                                <td>
                                                    <asp:DataGrid ID="dgMaterialList" runat="server" CssClass="list" AllowSorting="True"
                                                        AutoGenerateColumns="False" PageSize="15" Width="100%" ShowFooter="True" OnItemDataBound="dgMaterialList_ItemDataBound">
                                                        <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                                        <Columns>
                                                            <asp:BoundColumn Visible="False" DataField="ContractMaterialCode"></asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="序号" FooterText="合计">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="材料名称">
                                                                <ItemTemplate>
                                                                    <a href="#" onclick="javascript:OpenMaterialInfo('<%# Eval("MaterialCode") %>');return false;">
                                                                        <%#  DataBinder.Eval(Container.DataItem, "MaterialName")  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="规格">
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Spec")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="单位">
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Unit")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="需求数量">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                <ItemTemplate>
                                                                    <a href="#" title="显示月度计划" onclick="javascript:doModifyMaterialMonth('<%# Eval("MaterialCode") %>');return false;">
                                                                        <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "Qty"))  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumQty" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="单价">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "Price"))  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="金额">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "Money"))  %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumMoney" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="已入数量">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InQty"))  %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumInQty" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="已领数量">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutQty"))  %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumOutQty" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divMaterialIn" runat="server">
                                        <br>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    材料入库明细</td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                            <tr>
                                                <td>
                                                    <asp:DataGrid ID="dgMaterialInList" runat="server" DataKeyField="MaterialInDtlCode"
                                                        CellPadding="0" GridLines="Horizontal" AutoGenerateColumns="False" PageSize="15"
                                                        Width="100%" CssClass="list" ShowFooter="true" OnItemDataBound="dgMaterialInList_ItemDataBound">
                                                        <HeaderStyle CssClass="list-title" />
                                                        <FooterStyle CssClass="list-title" />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="序号" FooterText="合计">
                                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                <FooterStyle Wrap="False" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="材料名称">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <a href="#" onclick="javascript:OpenMaterialInfo('<%# Eval("MaterialCode") %>');return false;">
                                                                        <%#  DataBinder.Eval(Container.DataItem, "MaterialName")  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="规格">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Spec")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="单位">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Unit")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="入库数量">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                                <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InQty"))  %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumInQty" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="入库单价">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InPrice"))  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="入库金额">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                                <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InMoney"))  %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumInMoney" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="入库日期">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "InDate"))  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="入库单号">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <a href="#" onclick="javascript:OpenMaterialInInfo('<%# Eval("MaterialInCode") %>');return false;">
                                                                        <%#  DataBinder.Eval(Container.DataItem, "MaterialInID")  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divMaterialOut" runat="server">
                                        <br>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    材料领用明细</td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                            <tr>
                                                <td>
                                                    <asp:DataGrid ID="dgMaterialOutList" runat="server" DataKeyField="MaterialOutDtlCode"
                                                        CellPadding="0" GridLines="Horizontal" AutoGenerateColumns="False" PageSize="15"
                                                        Width="100%" CssClass="list" ShowFooter="true" OnItemDataBound="dgMaterialOutList_ItemDataBound">
                                                        <HeaderStyle CssClass="list-title" />
                                                        <FooterStyle CssClass="list-title" />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="序号" FooterText="合计">
                                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                <FooterStyle Wrap="False" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="材料名称">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <a href="#" onclick="javascript:OpenMaterialInfo('<%# Eval("MaterialCode") %>');return false;">
                                                                        <%#  DataBinder.Eval(Container.DataItem, "MaterialName")  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="规格">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Spec")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="单位">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Unit")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="领料数量">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                                <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutQty"))  %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumOutQty" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="领料单价">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutPrice"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="领料金额">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                                <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutMoney"))%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumOutMoney" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="领料日期">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "OutDate"))  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="领料单号">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <a href="#" onclick="javascript:OpenMaterialOutInfo('<%# Eval("MaterialOutCode") %>');return false;">
                                                                        <%#  DataBinder.Eval(Container.DataItem, "MaterialOutID")  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table style="display: none" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                合同执行</td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="dgExecuteList" Style="display: none" runat="server" CssClass="list"
                                        Width="100%" CellPadding="2" GridLines="Horizontal" AllowPaging="False" PageSize="15"
                                        AutoGenerateColumns="False" AllowSorting="True">
                                        <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="经办人">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.ExecutorName") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ExecuteDate" HeaderText="执行时间" DataFormatString="{0:yyyy-MM-dd}">
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="工作">
                                                <ItemTemplate>
                                                    <a href="##" onclick='DoViewExecutePlan(code);' code='<%# DataBinder.Eval(Container, "DataItem.ContractExecutePlanCode") %>'>
                                                        <%#  RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container, "DataItem.ExecuteDetail"),15) %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <a href="##" onclick='FactExecutePlan(code);' code='<%# DataBinder.Eval(Container, "DataItem.ContractExecutePlanCode") %>'>
                                                        执行报告 </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
                                            CssClass="list-title"></PagerStyle>
                                    </asp:DataGrid></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div>
        </div>
        <div id="myjoybox" style="display: none; left: 10px; position: absolute; top: 200px;
            height: 120px" myoffsettop="0px">
            <table id="joyboxTable" height="120" cellspacing="0" cellpadding="0" width="220"
                border="0">
                <tbody>
                    <tr>
                        <td width="8%" bgcolor="#ffffcc">
                            <td width="92%" bgcolor="#ffffcc">
                                <label id="linktitle">
                                </label>
                            </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <input id="txtChangeContractCode" type="hidden" name="txtChangeContractCode" runat="server">
    </form>

    <script language="javascript">
<!--


	function doViewCheckInfo()
	{
		OpenLargeWindow('ContractAuditingInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>','合同审核信息');
	}

	function doViewUnitProject()
	{
		var code = Form1.txtUnitProjectCode.value;
		OpenLargeWindow( '../Construct/ConstructPlanInfo.aspx?PBSUnitCode=' + code , '单位工程' ) ;
	}

	function doViewCostCode(code)
	{
		OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&CostCode=' + code ,'动态费用项信息');
	}

	function doViewSupplier()
	{
		var code = Form1.txtSupplierCode.value;
		OpenFullWindow('../Supplier/SupplierInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&SupplierCode=' + code,'供应商分包信息');
	}
	
	function doViewSupplier2()
	{
		var code = Form1.txtSupplier2Code.value;
		OpenFullWindow('../Supplier/SupplierInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&SupplierCode=' + code,'供应商总包信息');
	}	

	function DoModify()
	{
		window.location.href='ContractModify.aspx?Act=Edit&ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>';
	}

	function DoModifyPaymentPlan()
	{
		OpenLargeWindow('ContractPaymentPlanModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&Act=EditPlan','修改付款计划');
	}

	function doCheck()
	{
		OpenFullWindow('<%=ViewState["_AuditingURL"]%>?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ApplicationCode1=<%=Request["ContractCode"]%>','合同审核_<%=Request["ContractCode"]%>');
	}
	
	function doOldCheck()
	{
		OpenLargeWindow('ContractAuditingModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>','合同审核');
	}
		
	
	function FactExecutePlan(ContractExecutePlanCode)
	{
		OpenMiddleWindow('ContractFactExecutePlan.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ContractExecutePlanCode='+ContractExecutePlanCode,'实际执行计划');
	}
	
	function DoViewExecutePlan(ContractExecutePlanCode)
	{
		OpenMiddleWindow('ContractExecutePlanInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ContractExecutePlanCode='+ContractExecutePlanCode,'查看执行计划');
	}

    
	function DoAddNewDocument()
	{
		OpenLargeWindow("../Document/DocumentModify.aspx?Action=Insert&DocumentTypeCode=000001&Code=<%=Request["ContractCode"]%>&RefreshScript=RefreshDocument();","新增合同文档"); 
	}
	function ShowDocument(DocumentCode)
	{
		OpenLargeWindow("../Document/DocumentInfo.aspx?DocumentCode=" + DocumentCode );
	}

	function RefreshDocument()
	{
		Form1.btnRefreshDocument.click();
	}

	function AttachView(ContractDocumentCode)
	{
		window.open('../Document/AttachView.aspx?ContractDocumentCode=' + ContractDocumentCode);
	}

	function DoAddNewPaymentApply()
	{
		OpenFullWindow('../Finance/PaymentDetailModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>','合同请款');
	}
	
	function ViewPaymentList()
	{
		OpenFullWindow('../Contract/ContractPaymentList.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&Issue=<%=ViewState["NewIssue"]%>','合同请款');
	}	
	//新增合同发票
	function DoAddNewContractBillApply()
	{
		OpenSmallWindow('ContractBill.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&Status=New','合同发票');
	}
	
	
	function DoAddNewProductionPaymentApply()
	{
		OpenLargeWindow('../Finance/PaymentProductionModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>','合同请款');
	}
	
	function doViewPaymentPlan( code )
	{
		OpenLargeWindow('ContractPaymentPlanView.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ContractPaymentPlanCode=' + code  ,'新增合同招投标');
	}

	function DoViewPayment( Code )
	{
		OpenLargeWindow('../Finance/PaymentInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&PaymentCode='+Code,'请款单信息');
	}

	function DoViewChange(ContractChangeCode)
	{
		OpenFullWindow('../Contract/ContractChangeInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ContractChangeCode='+ContractChangeCode,'合同变更信息');
	}

	function DoChange()
	{
		OpenFullWindow('../Contract/ContractChange.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&Act=Change&ContractCode=<%=Request["ContractCode"]%>','合同变更');
	}
	
	function DoAccount()
	{
		if(window.confirm('合同结算后将不能再进行处理，确实要结算吗 ?')) 
		{
		<% 
		    switch (up_sPMNameLower)
                    {
                        case "shimaopm":
                            %>
                            OpenFullWindow('ContracStrikeModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&Act=Change&ContractCode=<%=Request["ContractCode"]%>','合同变更');
                            <%
                       break; 
                       default:
                        %>
                        OpenFullWindow('<%=ViewState["_AccountAuditingURL"]%>?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ApplicationCode1=<%=Request["ContractCode"]%>','合同结算_<%=Request["ContractCode"]%>');
                        
                        <%
                            break;
                    }
             %>       
//		if(window.confirm('按[确定]进入普通结算，[取消]进入结算变更！')){
//			OpenFullWindow('<%=ViewState["_AccountAuditingURL"]%>?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ApplicationCode1=<%=Request["ContractCode"]%>','合同结算_<%=Request["ContractCode"]%>');
//			window.location.href="ContractAccountManage.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>";
//}
//		else{
//			OpenFullWindow('ContracStrikeModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&Act=Change&ContractCode=<%=Request["ContractCode"]%>','合同变更');
//		}
		}
			
	}	
	function ShowHistory()
	{
		window.navigate('ContractChangingHistory.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>' ,'合同变更历史记录');
	}

	function showPBS(code,alloType)
	{
		if ( alloType == 'U'  )
			OpenLargeWindow( '../PBS/PBSUnitInfo.aspx?action=view&PBSUnitCode=' + code,'单位工程信息' );
		else
			OpenLargeWindow( '../PBS/PBSBuildInfo.aspx?OpenModal=open&action=view&BuildingCode=' + code,'楼栋信息' );
		
	}
	
	function DoPrint()
	{
		var PrintUrl = '<%=ViewState["_PrintURL"] %>';
		
		OpenFullWindow( PrintUrl ,'打印预览');
	}
	
	function DoAccountPrint()
	{
		var PrintUrl = '<%=ViewState["_AccountPrintURL"] %>';
		OpenFullWindow( PrintUrl ,'打印预览');
	}
			
//显示工作信息
function OpenTask(WBSCode)
{
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}
	//显示修改实际产值
	function doModifyFactValue()
	{
		OpenLargeWindow('ContractModifyFactValue.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>' ,'修改实际产值');
	}
	
	//显示月度计划
	function doModifyMaterialMonth( materialcode)
	{
		OpenLargeWindow('ContractMaterialMonth.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&MaterialCode=' + materialcode ,'显示月度计划表');
	}
	
	//显示变更记录合同
	function doViewContractInfo( code )
	{
		window.navigate('ContractAuditingPrint.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'合同信息');
	}
	
	//显示合同发票
	function doViewContractBillInfo(code)
	{
		OpenMiddleWindow('ContractBill.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&Code='+code,'合同发票');
	}
	
	//显示修改合同编号
	function doModifyContractID()
	{
		OpenMiddleWindow('ContractID.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>' ,'修改合同编号');
	}
	
	//修改材料需求
	function doModifyMaterial()
	{
		OpenLargeWindow('ContractMaterialModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>' ,'修改材料需求');
	}

    //显示材料信息	
    function OpenMaterialInfo(Code)
    {   
        OpenLargeWindow('../Material/MaterialInfo.aspx?MaterialCode='+Code,'材料信息');

    }

    //显示入库单	
    function OpenMaterialInInfo(Code)
    {   
        OpenLargeWindow('../Material/MaterialInInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialInCode='+Code,'材料入库信息');

    }

    //显示领料单	
    function OpenMaterialOutInfo(Code)
    {   
        OpenLargeWindow('../Material/MaterialOutInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialOutCode='+Code,'领料单信息');

    }

//-->
    </script>

</body>
</html>
