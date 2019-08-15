<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BiddingSupplierList.ascx.cs" Inherits="BiddingManage_BiddingSupplierList" %>
<script language="javascript" src="../Rms.js" type="text/javascript"></script>
<script language="javascript" src="Rms.js" type="text/javascript"></script>
<script type="text/javascript">
function doViewSupplierInfo(code)
{
   OpenFullWindow('../Supplier/SupplierInfo.aspx?SupplierCode=' + code   ,"供应商信息");
}
function openSelectSupplier(){
	var strURL = '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=((RmsPM.Web.ProjectInfo)Session["Project"]).ProjectCode%>';
	
	var theWin = OpenMiddleWindow(strURL,'选择供应商' );
	theWin.focus();
}

function DoSelectSupplierReturn(strCode,strName){
	document.all('<%=HideSupplierCode.ClientID%>').value = strCode;
    __doPostBack('<%=btnAdd.ClientID.Replace('_', '$') %>','')
}

</script>

<asp:Button ID="btnAdd" runat="server" Text="添加单位"  OnClientClick="javascript:openSelectSupplier();return false;" OnClick="btnAdd_Click"  CssClass="button" />
<asp:Button ID="btnRemove" runat="server" Text="移除单位"  CssClass="button" OnClick="btnRemove_Click" />
<asp:HiddenField ID="HideSupplierCode" runat="server" />
<asp:Button ID="btnApprove" runat="server" Text="审核通过" OnClick="btnApprove_Click"  CssClass="button" />
<asp:Button ID="btnCancelApprove" runat="server" Text="撤销审核" OnClick="btnCancelApprove_Click"  CssClass="button" />

<asp:GridView ID="GridView1"  runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" CssClass="list" Width="100%">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="isSelected" runat="server" />
                <asp:HiddenField ID="BiddingSupplierCode" runat="server" Value='<%# Eval("BiddingSupplierCode") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="预审" SortExpression="Flag">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# ((string)(Eval("Flag")+"")=="1"?"<font color=green>通过</font>":"") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="BiddingCode" HeaderText="BiddingCode" SortExpression="BiddingCode"
            Visible="False" />
        <asp:TemplateField HeaderText="投标单位" SortExpression="SupplierCode" >
            <ItemTemplate>
                <a href="#" onclick='javascript:doViewSupplierInfo("<%# (string)Eval("SupplierCode") %>");' >
                <%# RmsPM.BLL.ProjectRule.GetSupplierName((string)Eval("SupplierCode")) %></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ContractPerson" HeaderText="联系人" SortExpression="ContractPerson" />
        <asp:BoundField DataField="OfficePhone" HeaderText="电话" SortExpression="OfficePhone" />
        <asp:BoundField DataField="EMail" HeaderText="Email" SortExpression="EMail" />        
    </Columns>
    <HeaderStyle CssClass="list-title"></HeaderStyle>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"  
SelectCommand=
"select bs.biddingsuppliercode,bp.BiddingCode,bs.SupplierCode,bs.NominateUser,bs.NominateDate,bs.State,bs.Flag,sp.ContractPerson,sp.OfficePhone,sp.EMail from 
BiddingSupplier bs,BiddingPrejudication bp,Supplier sp 
where bs.BiddingPrejudicationCode=bp.BiddingPrejudicationCode and bs.SupplierCode=sp.SupplierCode 
and bp.BiddingCode=@BiddingCode" >
    <SelectParameters>
        <asp:Parameter Name="BiddingCode" />
    </SelectParameters>
</asp:SqlDataSource>

