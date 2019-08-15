<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BiddingMessageList.ascx.cs" Inherits="BiddingManage_BiddingMessageList" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<script type="text/javascript">
    
    function OpenContract(code)
    {
	    OpenFullWindow('../Contract/ContractModify.aspx?ContractDefaultValueCode='+code+'&Act=Bidding','合同');
    }
    function OpenBiddingMessageModif()
    {
	    OpenFullWindow('BiddingMessageApproveModify.aspx?BiddingCode=<%= this.BiddingCode %>','添加招投标');
    }
    
    function OpenBiddingMessageInfo(code)
    {
	    OpenFullWindow('BiddingMessageInfo.aspx?BiddingMessageCode='+code,'招投标信息');
    }
</script>
<div>
 <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
        <td nowrap>
            <asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="button" OnClientClick="javascript:OpenBiddingMessageModif();return false;"/>
            <asp:Button ID="btnRemove" runat="server" Text="移除"  CssClass="button" OnClick="btnRemove_Click"/>
            <asp:Button ID="btnApprove" runat="server" Text="审核"  CssClass="button" OnClick="btnApprove_Click" />
            <asp:Button ID="btnCancelApprove" runat="server" Text="撤销审核"   CssClass="button" OnClick="btnCancelApprove_Click" />
         
        </td>
    </tr>
    <tr>
        <td class="table" valign="top">
            <asp:DataGrid ID="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal"
                PageSize="15" Width="100%" AllowPaging="True" runat="server" AutoGenerateColumns="False">
                <HeaderStyle CssClass="list-title"></HeaderStyle>
                <FooterStyle CssClass="list-title"></FooterStyle>
                <Columns>
                   <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox Enabled='<%# RmsPM.BLL.ContractRule.GetContractCountByContractDefaultValueCode((string)DataBinder.Eval(Container, "DataItem.BiddingMessageCode"))<=0  %>' ID="isSelected" runat="server" />
                         
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="BiddingMessageCode" HeaderText="BiddingMessageCode" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ContractNember" HeaderText="合同编号"></asp:BoundColumn>
                     <asp:TemplateColumn HeaderText="合同名称">
                        <ItemTemplate>
                           <a href="#" onclick="OpenBiddingMessageInfo( <%# DataBinder.Eval(Container, "DataItem.BiddingMessageCode") %> )"><%# DataBinder.Eval(Container, "DataItem.ContractName") %></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                   
                    <asp:TemplateColumn HeaderText="合同类型">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# RmsPM.BLL.ContractRule.GetContractTypeName((string)DataBinder.Eval(Container, "DataItem.ContractType")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="供应商">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# RmsPM.BLL.ProjectRule.GetSupplierName((string)DataBinder.Eval(Container, "DataItem.Supplier")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <ItemTemplate>
                            <%# (RmsPM.BLL.ContractRule.GetContractCountByContractDefaultValueCode((string)DataBinder.Eval(Container, "DataItem.BiddingMessageCode"))>0)?"<font color=\"red\">已经生成</font>":
                            ((string)DataBinder.Eval(Container, "DataItem.State"))=="0"?"<a href=\"#\" onclick=\"OpenContract("+ DataBinder.Eval(Container, "DataItem.BiddingMessageCode") +")\">生成合同</a>":""%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn>
                   
                                
                    <asp:BoundColumn DataField="BiddingReturnCode" HeaderText="BiddingReturnCode" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="BiddingDtlCode" HeaderText="BiddingDtlCode" Visible="False"></asp:BoundColumn>
                 
                </Columns>
                <PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页"
                    HorizontalAlign="Right" CssClass="ListHeadTr"></PagerStyle>
            </asp:DataGrid>
           
        </td>
    </tr>
</table>
</div>
