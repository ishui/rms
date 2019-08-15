<%@ Reference Control="~/usercontrols/exchangemoney_control.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingReturnModify"  CodeFile="BiddingReturnModify.ascx.cs" %>
<%@ Register Src="../UserControls/manycurrencycost.ascx" TagName="manycurrencycost" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/attachmentadd.ascx"  TagPrefix="uc1" TagName="attachmentadd"%>
<%@ Register Src="../UserControls/AttachMentList.ascx" TagPrefix="uc1" TagName="AttachMentList" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Src="../UserControls/ExchangeMoney_Control.ascx"  TagPrefix="uc1" TagName="ExchangeMoney_Control"%>
<%@ Register Src="../UserControls/manycurrencycostInfo.ascx" TagName="manycurrencycostInfo" TagPrefix="uc2" %>

<link href="/../Images/index.css" rel="stylesheet" type="text/css" />
<link href="/../Images/infra.css" rel="stylesheet" type="text/css" />

<div id="OperableDiv" runat="server">
    <asp:DataGrid ID="dgListEdit" runat="server" PageSize="100" CssClass="list" CellPadding="0" GridLines="Horizontal"
         Width="100%" AllowPaging="True" AutoGenerateColumns="False" DataKeyField="BiddingReturnCode">
        <FooterStyle CssClass="list-title"></FooterStyle>
        <HeaderStyle CssClass="list-title"></HeaderStyle>
        <Columns>
            <asp:BoundColumn Visible="False" DataField="BiddingReturnCode" HeaderText="BiddingReturnCode">
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="序号">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.OrderCode") %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="分标段">
                <ItemTemplate>
                    <%# RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode((string)DataBinder.Eval(Container, "DataItem.BiddingDtlCode")) %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="投标单位">
                <ItemTemplate>
                    <a href="javascript:doViewSupplierInfo('<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>');">
                        <%# RmsPM.BLL.ProjectRule.GetSupplierName((string)DataBinder.Eval(Container, "DataItem.SupplierCode")) %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="回标金额(元)">
                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                <ItemTemplate>
                    <uc2:manycurrencycost ID="Manycurrencycost1" runat="server"></uc2:manycurrencycost>
                </ItemTemplate>
            </asp:TemplateColumn>
            
            <asp:TemplateColumn HeaderText="回标日期">
                <ItemStyle Wrap="False"></ItemStyle>
                <ItemTemplate>
                    <cc2:Calendar ID="txtReturnDate" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ReturnDate") %>'
                        CalendarMode="All" CalendarResource="../Images/CalendarResource/">
                    </cc2:Calendar>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="备注">
                <ItemTemplate>
                    <textarea id="txtRemark" onblur="ObjectReduce(this)" style="width: 180px; height: 90%"
                        onfocus="ObjectSpread(this)" name="txtRemark" rows="1" cols="20" runat="server"><%# DataBinder.Eval(Container, "DataItem.Remark") %></textarea>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn Visible="False" DataField="Money" HeaderText="Money"></asp:BoundColumn>
            <asp:BoundColumn Visible="False" DataField="BiddingDtlCode" HeaderText="BiddingDtlCode">
            </asp:BoundColumn>
        </Columns>
        <PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页"
            HorizontalAlign="Right" CssClass="ListHeadTr"></PagerStyle>
    </asp:DataGrid>
    <table class="tree" width="100%">
        <tr style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
            border-bottom: 1px solid">
            <td align="left">
                招标内容<br/>及要求 :
            </td>
            <td>
                <asp:TextBox ID="txtTotalRemark" runat="server" Width="100%" Height="80px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 81px" align="left">
                相关附件:
            </td>
            <td>
                <uc1:attachmentadd ID="AttachMentAdd1" runat="server"></uc1:attachmentadd>
            </td>
        </tr>
    </table>
</div>
<div id="EyeableDiv" runat="server">
    <asp:DataGrid ID="dgListView" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
        Width="100%" AllowPaging="False" AutoGenerateColumns="False" DataKeyField="BiddingSupplierCode" PageSize="100">
        <FooterStyle CssClass="list-title"></FooterStyle>
        <HeaderStyle CssClass="list-title"></HeaderStyle>
        <Columns>
            <asp:TemplateColumn HeaderText="分标段">
                <ItemTemplate>
                    <%# RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode((string)DataBinder.Eval(Container, "DataItem.BiddingDtlCode")) %>
                </ItemTemplate>
            </asp:TemplateColumn>
           
            <asp:TemplateColumn HeaderText="发标单位">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="True"></asp:CheckBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn Visible="False" DataField="BiddingReturnCode" HeaderText="BiddingReturnCode">
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="投标单位">
                <ItemTemplate>
                    <a href="javascript:doViewSupplierInfo('<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>');">
                        <%# RmsPM.BLL.ProjectRule.GetSupplierName((string)DataBinder.Eval(Container, "DataItem.SupplierCode")) %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="回标金额(元)">
                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.Money","{0:n}")) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            
           
            <asp:TemplateColumn HeaderText="回标日期">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReturnDate", "{0:g}") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <asp:DataGrid ID="Datagrid1" runat="server" CssClass="list" CellPadding="0"
        GridLines="Horizontal" Width="100%" AllowPaging="False" AutoGenerateColumns="False"
        Visible="False" PageSize="100">
        <FooterStyle CssClass="list-title"></FooterStyle>
        <HeaderStyle CssClass="list-title"></HeaderStyle>
        <Columns>
            <asp:BoundColumn Visible="False" DataField="BiddingReturnCode" HeaderText="BiddingReturnCode">
            </asp:BoundColumn>
            <asp:BoundColumn Visible="False" DataField="BiddingDtlCode" HeaderText="BiddingDtlCode">
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="分标段">
                <ItemTemplate>
                    <%# RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode((string)DataBinder.Eval(Container, "DataItem.BiddingDtlCode")) %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="OrderCode" HeaderText="序号" Visible="False"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="投标单位">
                <ItemTemplate>
                    <a href='javascript:doViewSupplierInfo("<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>");'>
                        <%# RmsPM.BLL.ProjectRule.GetSupplierName((string)DataBinder.Eval(Container, "DataItem.SupplierCode")) %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="回标金额(元)">
                <ItemTemplate>
                   <%# (this.EmitState == 1) ? DataBinder.Eval(Container, "DataItem.Money", "{0:n}") : "****"%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Money","{0:n}") %>'>
                    </asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateColumn>
            
             <asp:TemplateColumn HeaderText="原金额(原币种)">
               <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                     <uc2:manycurrencycostInfo ID="manycurrencycostInfo1" runat="server"></uc2:manycurrencycostInfo>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="回标日期">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReturnDate", "{0:g}") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn>
        </Columns>        
    </asp:DataGrid>
    <table class="tree" width="100%">
        <tr>
            <td style="width: 81px" align="left">
                招标内容<br/>及要求:
            </td>
            <td>
                    <asp:TextBox ID="txtTotalRemark1" runat="server" Width="100%" Height="80px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 81px" align="left">
                相关附件:
            </td>
            <td>
                    <uc1:attachmentadd ID="AttachMentAdd2" runat="server"></uc1:attachmentadd>
                    <uc1:AttachMentList ID="AttachMentList1" runat="server"></uc1:AttachMentList>
            </td>
        </tr>
    </table>
    <br />
    <fieldset>
        <legend>
            <asp:CheckBox ID="CheckBox2" runat="server" ForeColor="Red" Text="使用网上招标" /></legend>
        <table runat="server" id="TbWSZTB">
            <tr>
                <td>
                    开标后投标单位权限：</td>
                <td>
                    <asp:DropDownList ID="dpAllowAfterClose" runat="server" CssClass="input">
                        <asp:ListItem Value="0">不允许继续登录</asp:ListItem>
                        <asp:ListItem Value="1">只可见本单位的报价</asp:ListItem>
                        <asp:ListItem Value="2">只可见本单位报价及名次</asp:ListItem>
                        <asp:ListItem Value="3">可见所有单位报价及名次</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    开标人:</td>
                <td>
                    <asp:CheckBoxList runat="server" ID="chkOpener" RepeatDirection="Horizontal">
                    </asp:CheckBoxList></td>
            </tr>
        </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="List"
        Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="投标单位">
                <ItemTemplate>
                    <%# RmsPM.BLL.ProjectRule.GetSupplierName((string)Eval("SupplierCode")) %>
                </ItemTemplate>
            </asp:TemplateField>          
            <asp:TemplateField HeaderText="发送日志">
                <ItemTemplate>
                    <a href="../EmailHistoryList.aspx?EmailType=BiddingEmitTo&SupplierCode=<%# DataBinder.Eval(Container, "DataItem.SupplierCode").ToString() %>&MasterCode=<%# DataBinder.Eval(Container, "DataItem.BiddingEmitToCode").ToString() %>">发送日志</a>
                </ItemTemplate>
            </asp:TemplateField>            
        </Columns>
        <HeaderStyle CssClass="list-title"></HeaderStyle>
    </asp:GridView>
    </fieldset>
</div>

<script language="javascript" type="text/javascript"> 
function doViewSupplierInfo(code)
{
   OpenFullWindow('../Supplier/SupplierInfo.aspx?SupplierCode=' + code   ,"供应商信息");
}
function ObjectSpread(obj)
{
    obj.style.height = "100px";
}
function ObjectReduce(obj)
{
    obj.style.height = "90%";
}
function BiddingReturnCheckMoney(obj,msg)
{
	if(obj.value.length>0)
	{
		if(obj.value.match("^[0-9]+(\.[0-9]+)?$")==null)
		{
			obj.select();
			obj.focus();
			alert(msg+"请输入数字");
			obj.select();
			return false;
		}
	}
	return true;				
}
</script>

<script>
function TotalMoney(id)
{
	
	//aler
	//alert(id);
}
</script>

