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
    <title>��ͬ��Ϣ</title>
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
                    ��ͬ�ſ�</td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
                        type="button" value="ɾ��" name="btnDelete" runat="server" onserverclick="btnDelete_Click">
                    <input class="button" id="btnModify" onclick="javascript:DoModify();return false; "
                        type="button" value="�޸�" name="btnModify" runat="server">
                    <input class="button" id="btnAuditingModify" onclick="javascript:DoModify();return false; "
                        type="button" value="��˺��޸�" name="btnAuditingModify" runat="server">
                    <input class="button" id="btnCheck" onclick="javascript:doCheck();return false; "
                        type="button" value="�ύ" name="btnCheck" runat="server">
                    <input class="button" id="btnOldCheck" onclick="javascript:doOldCheck();return false; "
                        type="button" value="���" name="btnOldCheck" runat="server">
                    &nbsp;
                    <input class="button" id="btnContractID" onclick="doModifyContractID();return false;"
                        type="button" value="�޸ĺ�ͬ���" name="btnContractID" runat="server">
                    <input class="button" id="btnChange" onclick="javascript:DoChange();return false;"
                        type="button" value="��ͬ���" name="btnChange" runat="server">
                    <input class="button" id="btnAccount" onclick="javascript:if(!window.confirm('��ͬ����󽫲����ٽ��д���ȷʵҪ������ ?')) return false;"
                        type="button" value="��ͬ����" name="btnAccount" runat="server">
                    <input class="button" id="btnAccountManage" onclick="javascript:DoAccount();return false;"
                        type="button" value="��ͬ����" name="btnAccountManage" runat="server" onserverclick="btnAccount_Click">
                    <input class="button" id="btnHistory" onclick="ShowHistory();return false;" type="button"
                        value="�����¼" name="btnAccount" runat="server">
                    <input class="button" id="btnPrint" onclick="DoPrint();return false;" type="button"
                        value="��ͬ�������ӡ" runat="server">
                    <input class="button" id="btnAccontPrint" onclick="DoAccountPrint();return false;"
                        type="button" value="�����������ӡ" runat="server">
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
                                                ��ͬ���ƣ�</td>
                                            <td width="18%">
                                                <asp:Label ID="LabelContractName" runat="server"></asp:Label></td>
                                            <td class="form-item" width="15%">
                                                ��ͬ��ţ�</td>
                                            <td width="18%">
                                                <asp:Label ID="LabelContractID" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                                    ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
                                            <td class="form-item" width="15%">
                                                ��Ŀ���ƣ�</td>
                                            <td width="19%">
                                                <asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                ԭ��ͬ��</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblBudgetMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                ���ͣ�</td>
                                            <td>
                                                <asp:Label ID="LabelType" runat="server"></asp:Label></td>
                                            <td class="form-item">
                                                �б���Ŀ��</td>
                                            <td id="tdBidding" runat="server">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                <div id="divAdjustMoney" runat="server">�ݶ����/ָ����</div>
                                            </td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType2"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblAdjustMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                �а��ˣ�</td>
                                            <td>
                                                <a id="hrefSupplier" onclick="doViewSupplier(); return false;" href="##" runat="server">
                                                </a>
                                                <input id="txtSupplierCode" type="hidden" name="txtSupplierCode" runat="server" />
                                            </td>
                                            <td class="form-item">
                                                �ܳа���</td>
                                            <td>
                                                <a id="hrefSupplier2" onclick="doViewSupplier2(); return false;" href="##" runat="server">
                                                </a>
                                                <input id="txtSupplier2Code" type="hidden" name="txtSupplier2Code" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                ʵ�ʽ�</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType3"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblOriginalMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                �� �� ����</td>
                                            <td>
                                                <asp:Label ID="lblThirdParty" runat="server"></asp:Label></td>
                                            <td class="form-item">
                                                ���ڣ�</td>
                                            <td>
                                                <asp:Label ID="lblWorkTime" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                �ۼƱ����</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType4"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblTotalChangeMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                ��Σ�</td>
                                            <td>
                                                <asp:Label ID="lblMostly" runat="server"></asp:Label></td>
                                            <td class="form-item">
                                                ���ţ�</td>
                                            <td>
                                                <asp:Label ID="lblUnitName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                �������ռۣ�</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType5"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblTotalMoney" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                �� �� �ˣ�</td>
                                            <td>
                                                <asp:Label ID="LabelContractPerson" runat="server"></asp:Label></td>
                                            <td class="form-item">
                                                ��Ч���ڣ�</td>
                                            <td>
                                                <asp:Label ID="LabelContractDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                <asp:label runat="server" ID="lblBaohanTitle">����</asp:label>��</td>
                                            <td align="right">
                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                                    <td width="100%" style="border-bottom:0;padding:0" nowrap><asp:Label runat="server" ID="lblForeignMoneyType6"></asp:Label></td>
                                                    <td align="right" style="border-bottom:0;padding:0" nowrap><asp:Label ID="lblBaoHan" runat="Server"></asp:Label>&nbsp;&nbsp;</td>
                                                </tr></table></td>
                                            <td class="form-item">
                                                <asp:Label runat="server" ID="st1">ӡ��˰˰Ŀ��</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStampDutyID" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                            <td class="form-item">
                                                <asp:Label runat="server" ID="st2">ӡ��˰��</asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblStampDuty" runat="server"></asp:Label>&nbsp;&nbsp;</td>
                                        </tr>
                                        <tr id="trAdIssueDate" runat="server">
                                            <td class="form-item">��淢�����ڣ�</td>
                                            <td colspan="5"><asp:Label runat="server" ID="lblAdIssueDate"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                ������λ��</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblDevelopUnit" runat="Server"></asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr id="trPerformingCircs" runat="server">
                                            <td class="form-item">
                                                ���������</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblPerformingCircs" runat="Server"></asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                �а���Χ��</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblContractArea" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                ��ͬ������</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblContractObject" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                ���ʽ��</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblPayMode" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                ����Ҫ��&nbsp;&nbsp;<br>
                                                ������Լ����</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblQualityRequire" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                �ϴ�������</td>
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
                                                ��ע��</td>
                                            <td colspan="5">
                                                <asp:Label ID="LabelRemark" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                �� �� �ˣ�</td>
                                            <td>
                                                <asp:Label ID="lblCheckPerson" runat="Server"></asp:Label>&nbsp;</td>
                                            <td class="form-item">
                                                ������ڣ�</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblCheckDate" runat="Server"></asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="form-item">
                                                ��������</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblCheckOpinion" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr id="trAccessRange" runat="server" visible="false">
                                            <td class="form-item">
                                                �� �� �ˣ�
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
                                                ������ϸ</td>
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
                                            <asp:TemplateColumn HeaderText="���">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="������" Visible="False">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <uc1:InputCostBudgetDtl ID="ucCostBudgetDtl" runat="server" Visable="False"></uc1:InputCostBudgetDtl>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="��λ����">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPBSName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="������">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCostName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="���ֽ��">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <uc1:ExchangeRate ID="ucExchangeRate" runat="server"></uc1:ExchangeRate>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    �ϼƽ�RMB����
                                                    <asp:Label ID="lblSumCostMoney" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="����/����%">
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
                                            <asp:TemplateColumn HeaderText="�Ѹ�">
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
                                            <asp:TemplateColumn HeaderText="δ��">
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
                                            <asp:TemplateColumn HeaderText="����/����%��RMB��" Visible="false">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAHMoney" runat="server"></asp:Label>&nbsp;/
                                                    <asp:Label ID="lblAHMoneyPer" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="�Ѹ���RMB��" Visible="false">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAPMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="δ����RMB��" Visible="false">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUPMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="˵��" Visible="False">
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
                                                    �����¼</td>
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
                                                <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="���"
                                                    ItemStyle-Width="60">
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href="#"  id="ALink" runat="server" onclick="DoViewChange(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "ContractChangeCode") %>'>
                                                            <%#  DataBinder.Eval(Container.DataItem, "ContractChangeCode") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="���" ItemStyle-Width="30" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# Container.ItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="OriginalMoney" HeaderText="ԭʵ�ʽ�Ԫ��" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="TotalChangeMoney" HeaderText="�ۼ����������Ԫ��" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="�ں��ۼ����������Ԫ��">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAuditedTotalChangeMoney" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="ChangeMoney" HeaderText="���α����Ԫ��" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="NewMoney" HeaderText="Ԥ�ƽ�Ԫ��" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="״̬">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                    <ItemTemplate>
                                                        <%# RmsPM.BLL.ContractRule.GetContractChangeStatusName(  DataBinder.Eval(Container.DataItem, "Status").ToString() )%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="�����">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                    <ItemTemplate>
                                                        <%# RmsPM.BLL.SystemRule.GetUserName(  DataBinder.Eval(Container.DataItem, "ChangePerson").ToString() )%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="ChangeDate" HeaderText="����" DataFormatString="{0:yyyy-MM-dd}">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                </asp:BoundColumn>
                                            </Columns>
                                            <PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
                                                CssClass="ListHeadTr"></PagerStyle>
                                        </asp:DataGrid><br>
                                    </div>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                �����ۼƱ�</td>
                                            <td>
                                                <input class="button-small" id="btnNewPaymentApply" onclick="DoAddNewPaymentApply();return false;"
                                                    type="button" value="��ͬ���" name="btnNewPaymentApply" runat="server">
                                                <input class="button-small" id="btnNewProductionPaymentApply" onclick="DoAddNewProductionPaymentApply();return false;"
                                                    type="button" value="���������" name="btnNewProductionPaymentApply" runat="server">
                                                <input class="button-small" id="btnPaymentList" onclick="ViewPaymentList();return false;"
                                                    type="button" value="��������¼��" name="btnPaymentList" runat="server">
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
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="���"
                                                ItemStyle-Width="60" FooterText="�ϼƣ�">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <a href="#" onclick="DoViewPayment(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "PaymentCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "PaymentID") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="���" ItemStyle-Width="40" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="��" ItemStyle-Width="40">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Issue") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="�������" ItemStyle-Width="100">
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
                                            <asp:TemplateColumn HeaderText="�ۼ�����" ItemStyle-Width="100" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.AHMoney", "{0:N}") %>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="�ۼ�����%" ItemStyle-Width="80" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPercentAHMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="�ۼ�Ӧ��" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalPaymentMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="�ۼ�Ӧ��%" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalPaymentMoneyPercent" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="����" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUHMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="�ۼ��Ѹ�" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center" CssClass="LeftDoubleBorderTD"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right" CssClass="LeftDoubleBorderTD"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAPMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" CssClass="LeftDoubleBorderTD"></FooterStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="�ۼ��Ѹ�%" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPercentAPMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="���ڸ���" ItemStyle-Width="80">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.PayoutMoney", "{0:N}")%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="PayDate" HeaderText="����������" DataFormatString="{0:yyyy-MM-dd}">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" Width="80" HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="StatusName" HeaderText="״̬" ItemStyle-Width="50">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ApplyDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" Width="80" HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="��ע" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Remark" HeaderText="��ע" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="�Ѹ���Ԫ��" Visible="False">
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
                                        <PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
                                            CssClass="list-title"></PagerStyle>
                                    </asp:DataGrid>
                                    <div id="divContractBill" runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    �յ���Ʊ</td>
                                                <td>
                                                    <input class="button-small" id="Button1" onclick="DoAddNewContractBillApply();return false;"
                                                        type="button" value="������Ʊ" name="btnNewContractBillApply" runat="server" /></td>
                                            </tr>
                                        </table>
                                        <asp:DataGrid ID="dgContractBillList" runat="server" AutoGenerateColumns="False"
                                            PageSize="14" AllowPaging="True" GridLines="Horizontal" CellPadding="0" Width="100%"
                                            CssClass="list" AllowSorting="true" ShowFooter="True" OnItemDataBound="dgContractBillList_ItemDataBound">
                                            <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                                            <FooterStyle CssClass="list-title"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="���" FooterText="�ϼ�">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# Container.ItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="��Ʊ��">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href="##" onclick="doViewContractBillInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "Code") %>'>
                                                            <%# RmsPM.BLL.StringRule.TruncateString(  DataBinder.Eval(Container.DataItem, "BillNo"),8) %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="��Ʊ��Ԫ��">
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
                                            <PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ"
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
                                                    �������</td>
                                            </tr>
                                        </table>
                                        <uc1:WorkFlowList ID="ucWorkFlowList" runat="server"></uc1:WorkFlowList>
                                    </div>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                ��ͬ�ĵ�</td>
                                            <td>
                                                <input class="button-small" id="btnDocument" onclick="DoAddNewDocument();return false;"
                                                    type="button" value="������ͬ�ĵ�" name="btnDocument" runat="server">&nbsp;<input class="button-small"
                                                        id="btnRefreshDocument" style="display: none" type="button" value="�����ĵ�����" name="btnRefreshDocument"
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
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="����">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="#" id="Alink" runat="server" onclick="ShowDocument(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "DocumentCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "Title") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Author" HeaderText="����"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="����">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.DocumentRule.Instance().GetAttachListHtml("DocumentAttach", DataBinder.Eval(Container.DataItem, "DocumentCode").ToString()) %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:ButtonColumn HeaderText="ɾ��" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                CommandName="Delete"></asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
                                            CssClass="list-title"></PagerStyle>
                                    </asp:DataGrid>
                                    <div id="divTaskList" runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    ��ع���</td>
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
                                                            <asp:ButtonColumn Visible="False" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                                HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
                                                        </Columns>
                                                        <PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
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
                                                                Լ����ֵ</td>
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
                                                                        <asp:TemplateColumn HeaderText="���">
                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# Container.ItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="����" FooterText="�ϼ�">
                                                                            <ItemTemplate>
                                                                                <%#  DataBinder.Eval(Container.DataItem, "ProductionDate","{0:d}")  %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Լ����ֵ">
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
                                                                        <asp:TemplateColumn HeaderText="�ۼ�Լ����ֵ">
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
                                                                <input class="button-small" id="btnModifyFactValue" onclick="doModifyFactValue();return false;"
                                                                    type="button" value="�޸�ʵ�ʲ�ֵ" name="btnModifyFactValue" runat="server">
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
                                                                        <asp:TemplateColumn HeaderText="���">
                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# Container.ItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="����" FooterText="�ϼ�">
                                                                            <ItemTemplate>
                                                                                <%#  DataBinder.Eval(Container.DataItem, "ProductionDate","{0:d}")  %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="ʵ�ʲ�ֵ">
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
                                                                        <asp:TemplateColumn HeaderText="�ۼ�ʵ�ʲ�ֵ">
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
                                                                            HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
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
                                                    ��������</td>
                                                <td>
                                                    <input class="button-small" id="btnModifyMaterial" onclick="doModifyMaterial();return false;"
                                                        type="button" value="�޸Ĳ�������" name="btnModifyContractMaterial" runat="server">
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
                                                            <asp:TemplateColumn HeaderText="���" FooterText="�ϼ�">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="��������">
                                                                <ItemTemplate>
                                                                    <a href="#" onclick="javascript:OpenMaterialInfo('<%# Eval("MaterialCode") %>');return false;">
                                                                        <%#  DataBinder.Eval(Container.DataItem, "MaterialName")  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="���">
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Spec")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="��λ">
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Unit")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="��������">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                <ItemTemplate>
                                                                    <a href="#" title="��ʾ�¶ȼƻ�" onclick="javascript:doModifyMaterialMonth('<%# Eval("MaterialCode") %>');return false;">
                                                                        <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "Qty"))  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblSumQty" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="����">
                                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "Price"))  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="���">
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
                                                            <asp:TemplateColumn HeaderText="��������">
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
                                                            <asp:TemplateColumn HeaderText="��������">
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
                                                    ���������ϸ</td>
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
                                                            <asp:TemplateColumn HeaderText="���" FooterText="�ϼ�">
                                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                <FooterStyle Wrap="False" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="��������">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <a href="#" onclick="javascript:OpenMaterialInfo('<%# Eval("MaterialCode") %>');return false;">
                                                                        <%#  DataBinder.Eval(Container.DataItem, "MaterialName")  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="���">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Spec")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="��λ">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Unit")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="�������">
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
                                                            <asp:TemplateColumn HeaderText="��ⵥ��">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "InPrice"))  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="�����">
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
                                                            <asp:TemplateColumn HeaderText="�������">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "InDate"))  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="��ⵥ��">
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
                                                    ����������ϸ</td>
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
                                                            <asp:TemplateColumn HeaderText="���" FooterText="�ϼ�">
                                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                <FooterStyle Wrap="False" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="��������">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <a href="#" onclick="javascript:OpenMaterialInfo('<%# Eval("MaterialCode") %>');return false;">
                                                                        <%#  DataBinder.Eval(Container.DataItem, "MaterialName")  %>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="���">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Spec")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="��λ">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  DataBinder.Eval(Container.DataItem, "Unit")  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="��������">
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
                                                            <asp:TemplateColumn HeaderText="���ϵ���">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "OutPrice"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="���Ͻ��">
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
                                                            <asp:TemplateColumn HeaderText="��������">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                <ItemTemplate>
                                                                    <%#  RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "OutDate"))  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="���ϵ���">
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
                                                ��ִͬ��</td>
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
                                            <asp:TemplateColumn HeaderText="������">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.ExecutorName") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ExecuteDate" HeaderText="ִ��ʱ��" DataFormatString="{0:yyyy-MM-dd}">
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="����">
                                                <ItemTemplate>
                                                    <a href="##" onclick='DoViewExecutePlan(code);' code='<%# DataBinder.Eval(Container, "DataItem.ContractExecutePlanCode") %>'>
                                                        <%#  RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container, "DataItem.ExecuteDetail"),15) %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <a href="##" onclick='FactExecutePlan(code);' code='<%# DataBinder.Eval(Container, "DataItem.ContractExecutePlanCode") %>'>
                                                        ִ�б��� </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
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
		OpenLargeWindow('ContractAuditingInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>','��ͬ�����Ϣ');
	}

	function doViewUnitProject()
	{
		var code = Form1.txtUnitProjectCode.value;
		OpenLargeWindow( '../Construct/ConstructPlanInfo.aspx?PBSUnitCode=' + code , '��λ����' ) ;
	}

	function doViewCostCode(code)
	{
		OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&CostCode=' + code ,'��̬��������Ϣ');
	}

	function doViewSupplier()
	{
		var code = Form1.txtSupplierCode.value;
		OpenFullWindow('../Supplier/SupplierInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&SupplierCode=' + code,'��Ӧ�̷ְ���Ϣ');
	}
	
	function doViewSupplier2()
	{
		var code = Form1.txtSupplier2Code.value;
		OpenFullWindow('../Supplier/SupplierInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&SupplierCode=' + code,'��Ӧ���ܰ���Ϣ');
	}	

	function DoModify()
	{
		window.location.href='ContractModify.aspx?Act=Edit&ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>';
	}

	function DoModifyPaymentPlan()
	{
		OpenLargeWindow('ContractPaymentPlanModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&Act=EditPlan','�޸ĸ���ƻ�');
	}

	function doCheck()
	{
		OpenFullWindow('<%=ViewState["_AuditingURL"]%>?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ApplicationCode1=<%=Request["ContractCode"]%>','��ͬ���_<%=Request["ContractCode"]%>');
	}
	
	function doOldCheck()
	{
		OpenLargeWindow('ContractAuditingModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>','��ͬ���');
	}
		
	
	function FactExecutePlan(ContractExecutePlanCode)
	{
		OpenMiddleWindow('ContractFactExecutePlan.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ContractExecutePlanCode='+ContractExecutePlanCode,'ʵ��ִ�мƻ�');
	}
	
	function DoViewExecutePlan(ContractExecutePlanCode)
	{
		OpenMiddleWindow('ContractExecutePlanInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ContractExecutePlanCode='+ContractExecutePlanCode,'�鿴ִ�мƻ�');
	}

    
	function DoAddNewDocument()
	{
		OpenLargeWindow("../Document/DocumentModify.aspx?Action=Insert&DocumentTypeCode=000001&Code=<%=Request["ContractCode"]%>&RefreshScript=RefreshDocument();","������ͬ�ĵ�"); 
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
		OpenFullWindow('../Finance/PaymentDetailModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>','��ͬ���');
	}
	
	function ViewPaymentList()
	{
		OpenFullWindow('../Contract/ContractPaymentList.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&Issue=<%=ViewState["NewIssue"]%>','��ͬ���');
	}	
	//������ͬ��Ʊ
	function DoAddNewContractBillApply()
	{
		OpenSmallWindow('ContractBill.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&Status=New','��ͬ��Ʊ');
	}
	
	
	function DoAddNewProductionPaymentApply()
	{
		OpenLargeWindow('../Finance/PaymentProductionModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>','��ͬ���');
	}
	
	function doViewPaymentPlan( code )
	{
		OpenLargeWindow('ContractPaymentPlanView.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ContractPaymentPlanCode=' + code  ,'������ͬ��Ͷ��');
	}

	function DoViewPayment( Code )
	{
		OpenLargeWindow('../Finance/PaymentInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&PaymentCode='+Code,'����Ϣ');
	}

	function DoViewChange(ContractChangeCode)
	{
		OpenFullWindow('../Contract/ContractChangeInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ContractChangeCode='+ContractChangeCode,'��ͬ�����Ϣ');
	}

	function DoChange()
	{
		OpenFullWindow('../Contract/ContractChange.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&Act=Change&ContractCode=<%=Request["ContractCode"]%>','��ͬ���');
	}
	
	function DoAccount()
	{
		if(window.confirm('��ͬ����󽫲����ٽ��д���ȷʵҪ������ ?')) 
		{
		<% 
		    switch (up_sPMNameLower)
                    {
                        case "shimaopm":
                            %>
                            OpenFullWindow('ContracStrikeModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&Act=Change&ContractCode=<%=Request["ContractCode"]%>','��ͬ���');
                            <%
                       break; 
                       default:
                        %>
                        OpenFullWindow('<%=ViewState["_AccountAuditingURL"]%>?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ApplicationCode1=<%=Request["ContractCode"]%>','��ͬ����_<%=Request["ContractCode"]%>');
                        
                        <%
                            break;
                    }
             %>       
//		if(window.confirm('��[ȷ��]������ͨ���㣬[ȡ��]�����������')){
//			OpenFullWindow('<%=ViewState["_AccountAuditingURL"]%>?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&ApplicationCode1=<%=Request["ContractCode"]%>','��ͬ����_<%=Request["ContractCode"]%>');
//			window.location.href="ContractAccountManage.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>";
//}
//		else{
//			OpenFullWindow('ContracStrikeModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&Act=Change&ContractCode=<%=Request["ContractCode"]%>','��ͬ���');
//		}
		}
			
	}	
	function ShowHistory()
	{
		window.navigate('ContractChangingHistory.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>' ,'��ͬ�����ʷ��¼');
	}

	function showPBS(code,alloType)
	{
		if ( alloType == 'U'  )
			OpenLargeWindow( '../PBS/PBSUnitInfo.aspx?action=view&PBSUnitCode=' + code,'��λ������Ϣ' );
		else
			OpenLargeWindow( '../PBS/PBSBuildInfo.aspx?OpenModal=open&action=view&BuildingCode=' + code,'¥����Ϣ' );
		
	}
	
	function DoPrint()
	{
		var PrintUrl = '<%=ViewState["_PrintURL"] %>';
		
		OpenFullWindow( PrintUrl ,'��ӡԤ��');
	}
	
	function DoAccountPrint()
	{
		var PrintUrl = '<%=ViewState["_AccountPrintURL"] %>';
		OpenFullWindow( PrintUrl ,'��ӡԤ��');
	}
			
//��ʾ������Ϣ
function OpenTask(WBSCode)
{
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}
	//��ʾ�޸�ʵ�ʲ�ֵ
	function doModifyFactValue()
	{
		OpenLargeWindow('ContractModifyFactValue.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>' ,'�޸�ʵ�ʲ�ֵ');
	}
	
	//��ʾ�¶ȼƻ�
	function doModifyMaterialMonth( materialcode)
	{
		OpenLargeWindow('ContractMaterialMonth.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&MaterialCode=' + materialcode ,'��ʾ�¶ȼƻ���');
	}
	
	//��ʾ�����¼��ͬ
	function doViewContractInfo( code )
	{
		window.navigate('ContractAuditingPrint.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'��ͬ��Ϣ');
	}
	
	//��ʾ��ͬ��Ʊ
	function doViewContractBillInfo(code)
	{
		OpenMiddleWindow('ContractBill.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&Code='+code,'��ͬ��Ʊ');
	}
	
	//��ʾ�޸ĺ�ͬ���
	function doModifyContractID()
	{
		OpenMiddleWindow('ContractID.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>' ,'�޸ĺ�ͬ���');
	}
	
	//�޸Ĳ�������
	function doModifyMaterial()
	{
		OpenLargeWindow('ContractMaterialModify.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>' ,'�޸Ĳ�������');
	}

    //��ʾ������Ϣ	
    function OpenMaterialInfo(Code)
    {   
        OpenLargeWindow('../Material/MaterialInfo.aspx?MaterialCode='+Code,'������Ϣ');

    }

    //��ʾ��ⵥ	
    function OpenMaterialInInfo(Code)
    {   
        OpenLargeWindow('../Material/MaterialInInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialInCode='+Code,'���������Ϣ');

    }

    //��ʾ���ϵ�	
    function OpenMaterialOutInfo(Code)
    {   
        OpenLargeWindow('../Material/MaterialOutInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialOutCode='+Code,'���ϵ���Ϣ');

    }

//-->
    </script>

</body>
</html>
