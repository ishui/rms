<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GK_LocaleViseAuditing.ascx.cs" Inherits="WorkFlowOperation_GK_LocaleViseAuditing" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="ViseCode"
    Width="100%">
    <ItemTemplate>
        <table cellspacing="0" cellpadding="0" width="100%" border="0" style="padding-left:8px;padding-right:8px">
            <tr>
                <td align="left" colspan="2" nowrap width="100%">GKFC-JL-ZY-750601</td>
                <td width="80px" nowrap>标识序号：</td>
                <td nowrap>GC-</td>
            </tr>
            <tr>
                <td align="left" width="80px" nowrap>工程名称：</td>
                <td nowrap width="100%"><%# Eval("ViseName") %></td>
                <td width="80px" nowrap>编号：</td>
                <td nowrap><a href="#" onclick="javascript:OpenModify('<%# Eval("ViseCode") %>');return false;"><%# Eval("ViseId") %></a>
                    &nbsp;&nbsp;<font color="red"><%# RmsPM.BFL.LocaleViseBFL.GetViseStatusName((int)Eval("ViseStatus")) %></font></td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0" style="BORDER-TOP: #000000 1px solid">
            <tr>
                <td rowspan="3" class="blackbordertd" align="center" width="40px">施<br>工<br>单<br>位</td>
                <td class="blackbordertd" width="150px">分部分项工程名称：</td>
                <td class="blackbordertdpaddingcontent"><a href="#" onclick="javascript:doViewContractInfo('<%# Eval("ViseContractCode") %>');return false;">
                    <%# RmsPM.BLL.ContractRule.GetContractName((string)Eval("ViseContractCode")) %></a>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="blackbordertd">签证原因：</td>
                <td class="blackbordertdpaddingcontent"><%# Eval("ViseReason").ToString().Replace("\n","<br>") %>
                    &nbsp;
                    <uc1:AttachMentList ID="AttachMentList1" runat="server" AttachMentType="ViseReason"
                        MasterCode='<%# Eval("ViseCode") %>'></uc1:AttachMentList></td>
            </tr>
            <tr>
                <td class="blackbordertd">现场做法及工程量计算：</td>
                <td class="blackbordertdpaddingcontent"><%# Eval("ViseRemark").ToString().Replace("\n", "<br>")%>
                    &nbsp;
                    <uc1:AttachMentList ID="AttachMentList3" runat="server" AttachMentType="ViseRemark"
                        MasterCode='<%# Eval("ViseCode") %>'></uc1:AttachMentList></td>
            </tr>
            <tr>
                <td class="blackbordertd" align="center">监<br>理<br>单<br>位</td>
                <td class="blackbordertd">实物量计算：</td>
                <td class="blackbordertdpaddingcontent"><%# Eval("ViseScrutiny").ToString().Replace("\n", "<br>")%>
                    &nbsp;
                    <uc1:AttachMentList ID="AttachMentList2" runat="server" AttachMentType="ViseScrutiny"
                        MasterCode='<%# Eval("ViseCode") %>'></uc1:AttachMentList></td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetLocalVise"
    TypeName="RmsPM.BFL.LocaleViseBFL" OldValuesParameterFormatString="original_{0}"
    OnSelected="ObjectDataSource1_Selected">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="ViseCode" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2"
     Width="100%" ShowFooter="True">
    <Columns>
        <asp:TemplateField HeaderText="费用项" SortExpression="CostCode">
            <ItemTemplate>
                <%# RmsPM.BLL.CostBudgetRule.GetCostBudgetSetName((string)Eval("CostBudgetSetCode")) %>
                &nbsp;
                <%# RmsPM.BLL.CBSRule.GetCostFullName((string)Eval("CostCode"))%>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="金额(元)" SortExpression="Money">
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <%# RmsPM.BLL.MathRule.GetDecimalShowString(Eval("Money")) %>
            </ItemTemplate>
            <FooterStyle HorizontalAlign="Center" />
            <FooterTemplate>
                合计：<%# RmsPM.BLL.MathRule.GetDecimalShowString(RmsPM.BFL.LocaleViseBFL.GetViseSumMoney((int)FormView1.DataKey.Value)) %>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="备注" SortExpression="Remark">
            <ItemTemplate>
                <%# Eval("Remark").ToString().Replace("\n","<br>") %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetLocalViseCosts"
    TypeName="RmsPM.BFL.LocaleViseBFL">
    <SelectParameters>
        <asp:ControlParameter Type="int32" ControlID="FormView1" Name="Code" PropertyName="DataKey.Value" />
    </SelectParameters>
</asp:ObjectDataSource>
<script language="javascript">

function OpenModify(Code)
	{
		OpenFullWindow('../LocaleVise/LocaleViseInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&ViseCode='+Code,'现场签证新增');
	}
	function doViewContractInfo( code )
	{
		OpenFullWindow('../Contract/ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'合同信息');
	}
	function doViewSupplierInfo(code)
    {
        OpenFullWindow('../Supplier/SupplierInfo.aspx?SupplierCode=' + code   ,"供应商信息");
    }
	</script>
