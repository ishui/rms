<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiddingContractCreate.aspx.cs" Inherits="RmsPM.Web.BiddingManage.BiddingManage_BiddingContractCreate" %>

<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>创建合同</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
    <link href="../Images/infra.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td height="25">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                    <asp:Label ID="Lb_Title" runat="server">创建合同</asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="table" valign="top">
                        <asp:DataGrid ID="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal"
                            PageSize="15" Width="100%" AllowPaging="True" runat="server" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                            <FooterStyle CssClass="list-title"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn DataField="BiddingMessageCode" HeaderText="BiddingMessageCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ContractNember" HeaderText="合同编号"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ContractName" HeaderText="合同名称"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="合同类型">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# RmsPM.BLL.ContractRule.GetContractTypeName((string)DataBinder.Eval(Container, "DataItem.ContractType")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="供应商">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# RmsPM.BLL.ProjectRule.GetSupplierName((string)DataBinder.Eval(Container, "DataItem.Supplier")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BiddingReturnCode" HeaderText="BiddingReturnCode" Visible="False"></asp:BoundColumn>
                                
                        
                                <asp:TemplateColumn Visible="false">
                                    <ItemTemplate>
                                    <%#  "<a href=\"#\"  onclick=\"javascript:OpenMessage('" + DataBinder.Eval(Container, "DataItem.BiddingCode") + "','" + this.ProjectCode + "','" + DataBinder.Eval(Container, "DataItem.Supplier") + "','" + DataBinder.Eval(Container, "DataItem.BiddingDtlCode").ToString().Replace("'", "") + "');return false;\">生成通知单</a>"%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:BoundColumn DataField="BiddingDtlCode" HeaderText="BiddingDtlCode" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                    <%# (RmsPM.BLL.ContractRule.GetContractCountByContractDefaultValueCode((string)DataBinder.Eval(Container, "DataItem.BiddingMessageCode"))>0)?"<font color=\"red\">已经生成</font>":
                                        "<a href=\"#\" onclick=\"javascript:OpenContract('"+ DataBinder.Eval(Container, "DataItem.BiddingMessageCode") +"','"+this.ProjectCode+"');return false;\">生成合同</a>"%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页"
                                HorizontalAlign="Right" CssClass="ListHeadTr"></PagerStyle>
                        </asp:DataGrid>
                        <cc1:GridPagination ID="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/">
                        </cc1:GridPagination>
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
        </div>
    </form>
</body>
<script language="javascript">
function OpenContract(code,projectcode)
{
	OpenFullWindow('../Contract/ContractModify.aspx?ContractDefaultValueCode='+code+'&Act=Bidding&ProjectCode='+projectcode,'合同');
}

function OpenMessage(code,projectcode,supplierCode,biddingdtlcode)
{
	OpenMiddleWindow('../BiddingManage/BiddingContractInfo.aspx?BiddingCode='+code+'&ProjectCode='+projectcode+'&SupplierCode='+supplierCode+'&BiddingDtlCode='+biddingdtlcode,'通知单');
}
</script>
</html>
