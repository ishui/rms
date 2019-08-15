<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractMaterialMonth.aspx.cs"
    Inherits="RmsPM.Web.Contract.Contract_ContractMaterialMonth" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>月度计划表</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript" src="../images/convert.js"></script>

    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js">
    </script>

    <script language="javascript">
    function InfraFactValueChange(oEdit, oldValue, oEvent)
{
	InfraValueSum();
}

//计算合计
function InfraValueSum()
{
	var dgName;
	var lblName;
	var txtName;
	var c;
	var tempValue = 0;
	var sum = 0;
	
    lblName = "lblSumValue";
	txtName = "txtQty";

	dgName = "dgDtl";
	c = parseInt(document.all.dgDtl.rows.length) - 2;

	for(i=0;i<c;i++)
	{
	    tempValue = ConvertFloat(document.all(GetObjectNameInDataGrid(dgName, (i + 2), txtName)).value);
		sum = sum + tempValue;
	}

	//格式化
	sum = formatNumber(sum, "#,###.00");

	document.all(GetObjectNameInDataGrid(dgName, (c + 2), lblName)).innerText = sum;
//	alert(sum);
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table id="tableMain" height="100%" cellspacing="0" cellpadding="0" width="100%"
            bgcolor="#ffffff" border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    <asp:Label ID="lblTitle" runat="server" BackColor="Transparent">月度计划表</asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <%-- <div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">--%>
                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="form-item" width="80">
                                合同名称：</td>
                            <td>
                                <asp:Label ID="lblContractName" runat="server"></asp:Label>
                            </td>
                            <td class="form-item" width="80">
                                合同编号：</td>
                            <td width="100">
                                <asp:Label ID="lblContractID" runat="server"></asp:Label>
                            </td>
                            <td class="form-item" width="80">
                                部门：</td>
                            <td>
                                <asp:Label ID="lblUnit" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                供 应 商：</td>
                            <td>
                                <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                            </td>
                            <td class="form-item">
                                第 三 方：</td>
                            <td>
                                <asp:Label ID="lblThirdParty" runat="server"></asp:Label>
                            </td>
                            <td class="form-item">
                                合同类型：</td>
                            <td>
                                <asp:Label ID="lblSystemGroup" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                经 办 人：</td>
                            <td colspan="5">
                                <asp:Label ID="lblContractPersonName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                备注：</td>
                            <td colspan="5">
                                <asp:Label ID="lblRemark" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelEdit" runat="server">
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
                                <td class="intopic" width="200">
                                    月度计划</td>
                                <td>
                                    <input class="button-small" id="btnAddDtl" type="button" value="新 增" name="btnAddDtl"
                                        runat="server" onserverclick="btnNewMaterialMonthItem_ServerClick"></td>
                            </tr>
                        </table>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <%--    <div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">--%>
                                    <asp:DataGrid ID="dgDtl" runat="server" DataKeyField="ContractMaterialMonthCode"
                                        CellPadding="0" AutoGenerateColumns="False" GridLines="Horizontal" OnItemDataBound="dgDtl_DataBinding"
                                        ShowFooter="True" Width="100%" CssClass="list" OnDeleteCommand="dgDtl_DeleteCommand">
                                        <HeaderStyle CssClass="list-title" />
                                        <FooterStyle CssClass="list-title" />
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ContractMaterialMonthCode"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="序号">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    月份<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                <ItemTemplate>
                                                    <cc3:Calendar ID="dtContractMaterialMonth" runat="server" Display="True" ReadOnly="False"
                                                        CalendarResource="../Images/CalendarResource/" Value='<%#  DataBinder.Eval(Container.DataItem, "ContractMaterialMonth")  %>'>
                                                    </cc3:Calendar>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    需求数量<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                 <FooterStyle Wrap="false" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <igtxt:WebNumericEdit ID="txtQty" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'
                                                        CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        MinDecimalPlaces="Two">
                                                        <ClientSideEvents ValueChange="InfraFactValueChange"></ClientSideEvents>
                                                    </igtxt:WebNumericEdit>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    合计：
                                                    <asp:Label ID="lblSumValue" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="删除">
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<img src=../images/del.gif width=16 height=16 border=0>"
                                                        CausesValidation="false" CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    <%--  </div>--%>
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="0" cellpadding="9" width="100%">
                            <tr>
                                <td align="center">
                                    <input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server"
                                        onserverclick="btnSaveClick">
                                    &nbsp;
                                    <input class="submit" id="btnCancel" onserverclick="btnCancelClick" type="button"
                                        value="取 消" name="btnCancel" runat="server">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PanelItem" runat="server">
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
                                <td class="intopic" width="200">
                                    月度计划</td>
                                <td>
                                    <input class="button-small" id="btnEdit" type="button" value="修 改" name="btnEdit"
                                        runat="server" onserverclick="btnEdit_ServerClick"></td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CssClass="list" PageSize="11" Width="100%" GridLines="Horizontal" ShowFooter="True"
                            OnRowDataBound="GridView1_RowDataBound">
                            <HeaderStyle CssClass="list-title" />
                            <FooterStyle CssClass="list-title" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="false" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="月份">
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="false" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "ContractMaterialMonth")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="需求数量">
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="false" HorizontalAlign="Center" />
                                    <FooterStyle Wrap="false" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Qty") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        合计：<asp:Label ID="SumQty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                无匹配数据
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                    <%--   </div>--%>
                </td>
            </tr>
        </table>
        <input id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
        <input id="txtMaterialCode" type="hidden" name="txtMaterialCode" runat="server">
    </form>
</body>
</html>
