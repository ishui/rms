<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SM_LocalBiddingAuditing.ascx.cs" Inherits="WorkFlowOperation_SM_LocalBiddingAuditing" %>

<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>

<table cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
			<td colspan="8">
			    <table cellspacing="0" cellpadding="0" border="0" width="100%">
			             <TR>
		            <TD class="blackbordertdcontent" width="20%">项目名称</TD>
		            <TD class="blackbordertdcontent" runat="server" id="tdProjectNameTop" width="50%"><FONT face="宋体"></FONT></TD>
		            <TD class="blackbordertdcontent" width="15%">合同编号</TD>
		            <TD class="blackbordertdcontent" runat="server" id="tdContractNemberTop" width="15%"><FONT face="宋体">&nbsp;</FONT></TD>
	                </TR>
	                <TR>
		            <TD class="blackbordertdcontent">招标内容/标段名称</TD>
		            <TD class="blackbordertdcontent" colspan="5" runat="server" id="tdBiddingTitleTop"></TD>
		            </TR>
		        </table>
	
	        </td>
	</tr>
	<tr>
		<td class="blackbordertd" colspan="8">&nbsp;&nbsp;&nbsp;1.回标相关附件：

		      <uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
			<uc1:AttachMentAdd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			&nbsp;&nbsp;&nbsp;2.中标单位审批附件：<br>
			<uc1:AttachMentAdd id="AttachMentAdd2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			<uc1:AttachMentList id="AttachMentList2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
			
		</td>
	</tr>
	
	
    <tr>
        <td class="blackbordertdcontent" rowspan="2">
            中标</td>
        <td class="blackbordertdcontent" rowspan="2">
            分标段</td>
        <td class="blackbordertdcontent" rowspan="2">
            议标单位名称</td>
        <td class="blackbordertdcontent" colspan="3">
            技术标(请填“符合”或“不符合”)</td>
        <td class="blackbordertdcontent" rowspan="2">
            商务标排名</td>
        <td class="blackbordertdcontent" rowspan="2">
            最后报价(元)</td>
    </tr>
    <tr>
        <td class="blackbordertdcontent">
            建筑设计部</td>
        <td class="blackbordertdcontent">
            工程部</td>
        <td class="blackbordertdcontent">
            合约部</td>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td class="blackbordertdcontent">
                    <input id="hiddenSupplierCode" style="display: none" type="text" value='<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>'
                        name="hiddenSupplierCode" runat="server"/><input id="chkAuditing" type="checkbox"
                            name="chkAuditing" runat="server"/><span id="spanAuditing" runat="server"></span></td>
                            <td class="blackbordertdcontent">
                    <%# RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode((string)DataBinder.Eval(Container, "DataItem.BiddingDtlCode")) %></td>
                <td class="blackbordertd">
                    <input class="input" id="txtBiddingReturnCode" style="display: none" type="text"
                        value='<%# DataBinder.Eval(Container, "DataItem.BiddingReturnCode") %>' name="txtBiddingReturnCode"
                        runat="server"/>&nbsp;&nbsp;<%# Container.ItemIndex + 1%>.<%# RmsPM.BLL.ProjectRule.GetSupplierName((string)DataBinder.Eval(Container, "DataItem.SupplierCode")) %>&nbsp;</td>
                <td class="blackbordertdcontent">
                    <asp:RadioButtonList ID="RadioDesign" runat="server">
                        <asp:ListItem Value="1">符合</asp:ListItem>
                        <asp:ListItem Value="0">不符合</asp:ListItem>
                    </asp:RadioButtonList><span id="spanDesign" runat="server"></span><a>&nbsp;</a>
                </td>
                <td class="blackbordertdcontent">
                    <asp:RadioButtonList ID="RadioProject" runat="server">
                        <asp:ListItem Value="1">符合</asp:ListItem>
                        <asp:ListItem Value="0">不符合</asp:ListItem>
                    </asp:RadioButtonList><span id="spanProject" runat="server"></span><a>&nbsp;</a>
                </td>
                <td class="blackbordertdcontent">
                    <asp:RadioButtonList ID="RadioConsultant" runat="server">
                        <asp:ListItem Value="1">符合</asp:ListItem>
                        <asp:ListItem Value="0">不符合</asp:ListItem>
                    </asp:RadioButtonList><span id="spanConsultant" runat="server"></span><a>&nbsp;</a>
                </td>
                <td class="blackbordertdcontent">
                    <span id="txtState" runat="server">
                        <%# DataBinder.Eval(Container, "DataItem.myState") %>
                    </span><span id="spanState" runat="server">
                        <%# DataBinder.Eval(Container, "DataItem.myState") %>
                    </span>&nbsp;</td>
                <td class="blackbordertdcontent">
                    <span id="spMoney" runat="server">
                        <%# DataBinder.Eval(Container, "DataItem.Money","{0:n}") %>
                        &nbsp;</span></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    

			         
	
    
</table>
<script language="javascript">
function BiddingAuditingCheckMoney(obj)
{
	if(obj.value.length>0)
	{
		if(obj.value.match("^[0-9]+(\.[0-9]+)?$")==null)
		{
			obj.select();
			obj.focus();
			alert("商务标排名请输入数字");
			obj.select();
			return false;
		}
	}
	return true;				
}
</script>

