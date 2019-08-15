<%@ Control Language="C#" AutoEventWireup="true" CodeFile="inputExchangeRate.ascx.cs" Inherits="UserControls_inputExchangeRate" %>
<%@ Register Assembly="Infragistics.WebUI.WebDataInput.vT1" Namespace="Infragistics.WebUI.WebDataInputT1" TagPrefix="igtxt" %><script language="javascript" src="../Rms.js"></script>
<script language="javascript" src="../images/convert.js"></script>
<script language="javascript" src="../images/infra.js"></script>
<script language="javascript">
//计算本币金额
function <% =this.UniqueID %>InfraCashMoneyChange()
{
    var UniqueID = "<%=this.UniqueID%>";
    var fvExchangeRateName = GetObjectNameInControl(UniqueID,"fvExchangeRate");
    
    var txtCashName = GetObjectNameInControl(fvExchangeRateName,"txtCash");
    var txtExchangeRateName = GetObjectNameInControl(fvExchangeRateName,"txtExchangeRate");
    var lblMoney = GetObjectInControl(fvExchangeRateName,"lblMoney"); 
    
    var Cash = igedit_getById(txtCashName).getValue();
    var ExchangeRate = igedit_getById(txtExchangeRateName).getValue();
    
    var Money = Cash * ExchangeRate;
    
    Money = formatNumber(Money, "#,###.00");
         
    lblMoney.innerText = Money;
    
    <%=this.ValueChange%>
    
}
//初始化汇率默认值
function <% =this.UniqueID %>InfraMoneyTypeChange()
{
    var UniqueID = "<%=this.UniqueID%>";
    var fvExchangeRateName = GetObjectNameInControl(UniqueID,"fvExchangeRate");

    var ddlMoneyTypeObject = GetObjectByName(GetObjectNameInControl(fvExchangeRateName,"ddlMoneyType"));
    var txtExchangeRateName = GetObjectNameInControl(fvExchangeRateName,"txtExchangeRate");
    
    var ExchangeRate = formatNumber(ConvertFloat(ddlMoneyTypeObject.value), "#,###.00");
    

    
    var ud_bExchangeRateModify = true;
    
    for ( i=0;i<ddlMoneyTypeObject.options.length;i++ )
    {
        if ( ddlMoneyTypeObject.options[i].value == ddlMoneyTypeObject.value 
        && ddlMoneyTypeObject.options[i].text == "人民币 (RMB)" )
        {
            ud_bExchangeRateModify = false;
            ExchangeRate = 1.00;
            break;
        }
    }
    
    igedit_getById(txtExchangeRateName).setValue(ExchangeRate);
    igedit_getById(txtExchangeRateName).setEnabled(ud_bExchangeRateModify);
    
    
    
    
    <% =this.UniqueID %>InfraCashMoneyChange();
}

</script>
<asp:FormView ID="fvExchangeRate" runat="server" DefaultMode="Edit" OnDataBound="fvExchangeRate_DataBound">
    <EditItemTemplate>
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td nowrap>金额：</td>
                <td nowrap>
                    <igtxt:webnumericedit Width="80" id="txtCash" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# Bind("Cash") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                    </igtxt:webnumericedit>&nbsp;&nbsp;
                </td>
                <td nowrap>
                    <asp:DropDownList ID="ddlMoneyType" runat="server" DataTextField="MoneyType" DataValueField="ExchangeRate" onchange='<%# this.UniqueID + "InfraMoneyTypeChange();" %>'></asp:DropDownList>&nbsp;&nbsp;
                </td>
                <td nowrap>汇率：</td>
                <td nowrap>
                    <igtxt:webnumericedit Width="75" id="txtExchangeRate" runat="server" MinDecimalPlaces="8" CssClass="infra-input-nember" Value='<%# Bind("ExchangeRate") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                    </igtxt:webnumericedit>&nbsp;&nbsp;
                </td>
                <td nowrap>本币：</td>
                <td nowrap><asp:Label ID="lblMoney" runat="server" Text='<%# Bind("Money","{0:N}") %>'></asp:Label></td>
            </tr>
        </table>
    </EditItemTemplate>
    <ItemTemplate>
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td><asp:Label ID="lblMoneyType" runat="server" Text='<%# Bind("MoneyType") %>'></asp:Label>:<asp:Label ID="lblCash" runat="server" Text='<%# Bind("Cash","{0:N}") %>'></asp:Label></td>
                <td id="tdForeignCurrency" runat="server">&nbsp;&nbsp;汇率：<asp:Label ID="lblExchangeRate" runat="server" Text='<%# Bind("ExchangeRate","{0:N}") %>'></asp:Label>&nbsp;&nbsp;本币：<asp:Label ID="lblMoney" runat="server" Text='<%# Bind("Money","{0:N}") %>'></asp:Label></td>
            </tr>
        </table>
    </ItemTemplate>
    
</asp:FormView>
<asp:HiddenField ID="hidCash" runat="server" />
<asp:HiddenField ID="hidMoneyType" runat="server" />
<asp:HiddenField ID="hidExchangeRate" runat="server" />
<asp:HiddenField ID="hidMoney" runat="server" />
<asp:HiddenField ID="hidIsBind" runat="server" />
<asp:HiddenField ID="hidValueChange" runat="server" />
