<%@ Reference Control="~/workflowcontrol/workflowtoolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingAuditing"
    CodeFile="BiddingAuditing.ascx.cs" %>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr runat=server id="trOperableFile" visible=false>
			<td class="blackbordertd" colspan=9>&nbsp;&nbsp;&nbsp;1.�б굥λ��������
			
			<uc1:AttachMentAdd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			</td>
			</tr>
			<TR��runat=server id="trEyeableFile" visible=false>
				<TD class="blackbordertd" colspan=9  >&nbsp;&nbsp;&nbsp;1.�б굥λ��������
				
					<uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList><BR>
					</TD>
					
			</TR>
    <tr>
        <td class="blackbordertdcontent" rowspan="2" >
            �б�</td>
        <td class="blackbordertdcontent" rowspan="2">
            �ֱ��</td>
        <td class="blackbordertdcontent" rowspan="2">
            ��굥λ����</td>
        <td class="blackbordertdcontent" colspan="3">
            ������(������ϡ��򡰲����ϡ�)</td>
        <td class="blackbordertdcontent" rowspan="2">
            ���������</td>
        <td class="blackbordertdcontent" rowspan="2">
            ��󱨼�(Ԫ)</td>
    </tr>
    <tr>
        <td class="blackbordertdcontent">
            ������Ʋ�</td>
        <td class="blackbordertdcontent">
            ���̲�</td>
        <td class="blackbordertdcontent">
            ���ʹ�˾</td>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td class="blackbordertdcontent">
                    <input id="hiddenSupplierCode" style="display: none" type="text" value='<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>'
                        name="hiddenSupplierCode" runat="server"><input id="chkAuditing" type="checkbox"
                            name="chkAuditing" runat="server"><span id="spanAuditing" runat="server"></span></td>
                            <td class="blackbordertdcontent">
                    <%# RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode((string)DataBinder.Eval(Container, "DataItem.BiddingDtlCode")) %></td>
                <td class="blackbordertd">
                    <input class="input" id="txtBiddingReturnCode" style="display: none" type="text"
                        value='<%# DataBinder.Eval(Container, "DataItem.BiddingReturnCode") %>' name="txtBiddingReturnCode"
                        runat="server">&nbsp;&nbsp;<%# RmsPM.BLL.ProjectRule.GetSupplierName((string)DataBinder.Eval(Container, "DataItem.SupplierCode")) %>&nbsp;</td>
                <td class="blackbordertdcontent">
                    <asp:RadioButtonList ID="RadioDesign" runat="server">
                        <asp:ListItem Value="1">����</asp:ListItem>
                        <asp:ListItem Value="0">������</asp:ListItem>
                    </asp:RadioButtonList><span id="spanDesign" runat="server"></span><a>&nbsp;</a>
                </td>
                <td class="blackbordertdcontent">
                    <asp:RadioButtonList ID="RadioProject" runat="server">
                        <asp:ListItem Value="1">����</asp:ListItem>
                        <asp:ListItem Value="0">������</asp:ListItem>
                    </asp:RadioButtonList><span id="spanProject" runat="server"></span><a>&nbsp;</a>
                </td>
                <td class="blackbordertdcontent">
                    <asp:RadioButtonList ID="RadioConsultant" runat="server">
                        <asp:ListItem Value="1">����</asp:ListItem>
                        <asp:ListItem Value="0">������</asp:ListItem>
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
			alert("�������������������");
			obj.select();
			return false;
		}
	}
	return true;				
}
</script>

