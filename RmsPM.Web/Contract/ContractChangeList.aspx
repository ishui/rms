<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractChangeList.aspx.cs"
    Inherits="Contract_ContractChangeList" %>

<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>��ͬ���̨��</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
                                ��Ŀ����>��ͬ����>��ͬ���</td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <table width="100%" height="100%">
                        <tr>
                            <td>
                                <table class="search-area" cellspacing="0" cellpadding="0" width="100%" border="0"
                                    onkeydown="SearchKeyDown();">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        �������ţ�</td>
                                                    <td>
                                                        <input class="input" id="txtContractChangeCode" type="text" name="txtContractChagneCode"
                                                            runat="server"></td>
                                                    <td>
                                                        ���״̬��</td>
                                                    <td colspan="3">
                                                        <asp:CheckBoxList ID="cblChangeStatus" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0">����</asp:ListItem>
                                                            <asp:ListItem Value="1">����</asp:ListItem>
                                                            <asp:ListItem Value="2">����������</asp:ListItem>
                                                        </asp:CheckBoxList></td>
                                                    <td>
                                                        <input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server"
                                                            onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick">
                                                        &nbsp;<img src="../images/search_more.gif" title="�߼���ѯ" style="cursor: hand" id="imgAdvSearch"
                                                            onclick="ShowAdvSearch();">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ��ͬ��ţ�</td>
                                                    <td>
                                                        <input class="input" id="txtContractID" type="text" name="txtContractID" runat="server" /></td>
                                                    <td>
                                                        �����</td>
                                                    <td>
                                                        <uc1:InputUser ID="txtChangePerson" runat="server" />
                                                    </td>
                                                    <td>
                                                        �������ţ�</td>
                                                    <td>
                                                        <input class="input" id="txtContractChangeId" type="text" name="txtContractChangeID"
                                                            runat="server" /></td>
                                                </tr>
                                            </table>
                                            <table style="display: none" id="divAdvSearch">
                                                <tr id="trAdIssueDate" runat="server">
                                                    <td>
                                                        ������ڣ�<cc3:Calendar ID="dtChangeDate0" runat="server" CalendarResource="../Images/CalendarResource/"
                                                            ReadOnly="False" Display="True" Value="">
                                                        </cc3:Calendar>
                                                        ����<cc3:Calendar ID="dtChangeDate1" runat="server" CalendarResource="../Images/CalendarResource/"
                                                            ReadOnly="False" Display="True" Value="">
                                                        </cc3:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr height="100%">
                            <td>
                                <div style="overflow: auto; width: 100%; position: absolute; height: 100%">
                                    <asp:DataGrid ID="dgList" runat="server" CssClass="list" Width="100%" CellPadding="0"
                                        AutoGenerateColumns="False" AllowPaging="True" GridLines="Horizontal" ShowFooter="True"
                                        PageSize="14" AllowSorting="True" OnItemDataBound="dgChangeList_ItemDataBound"  OnSortCommand="dgList_SortCommand" >
                                        <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                        <ItemStyle CssClass=""></ItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="��������"
                                                ItemStyle-Width="60">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="#" id="ALink" runat="server" onclick="DoViewChange(this.Code,this.ContractCode);return false;"
                                                        code='<%#  DataBinder.Eval(Container.DataItem, "ContractChangeCode") %>' contractcode='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'>
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
                                            <asp:TemplateColumn ItemStyle-Width="160px" HeaderText="��ͬ����" SortExpression="ContractName" >
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="##" onclick="doViewContractInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'
                                                        title='<%# DataBinder.Eval(Container.DataItem, "ContractName")%>'>
                                                        <%# DataBinder.Eval(Container.DataItem, "ContractName") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Width="150px" HeaderText="��ͬ���" SortExpression="ContractID" >
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="##" onclick="doViewContractInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'
                                                        title='<%# DataBinder.Eval(Container.DataItem, "ContractName")%>'>
                                                        <%# DataBinder.Eval(Container.DataItem, "ContractID") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="OriginalMoney" HeaderText="ԭʵ�ʽ�Ԫ��" DataFormatString="{0:N}" >
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TotalChangeMoney" HeaderText="�ۼ����������Ԫ��" DataFormatString="{0:N}">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="�ں��ۼ����������Ԫ��">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAuditedTotalChangeMoney" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="���α����Ԫ��" FooterText="�ϼ�" SortExpression="ChangeMoney">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.ChangeMoney", "{0:N}")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblChangeMoney"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="NewMoney" HeaderText="Ԥ�ƽ�Ԫ��" DataFormatString="{0:N}">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="״̬">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.ContractRule.GetContractChangeStatusName(  DataBinder.Eval(Container.DataItem, "Status").ToString() )%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="�����" SortExpression="ChangePerson">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.SystemRule.GetUserName(  DataBinder.Eval(Container.DataItem, "ChangePerson").ToString() )%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ChangeDate" HeaderText="����" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ChangeDate">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ"
                                            HorizontalAlign="Right" CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:GridPagination ID="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/"
                                    DataGridId="dgList" OnPageIndexChange="GridPagination1_PageIndexChange" IsPrintList="true">
                                </cc1:GridPagination>
                            </td>
                        </tr>
                    </table>
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
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
        </table>
        <div id="divHintLoad" style="display: none; left: 1px; width: 100%; position: absolute;
            top: 200px; background-color: transparent">
            <table id="tableHintLoad" height="100" cellspacing="0" cellpadding="0" width="100%"
                border="0">
                <tr>
                    <td valign="top" align="center">
                        <iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameborder="no" width="100%"
                            scrolling="auto" height="100%"></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divHintSave" style="display: none; left: 1px; width: 100%; position: absolute;
            top: 200px">
            <table id="tableHintSave" height="100" cellspacing="0" cellpadding="0" width="100%"
                border="0">
                <tr>
                    <td valign="top" align="center">
                        <iframe id="frameSave" src="../Cost/SavingWating.htm" frameborder="no" width="100%"
                            scrolling="auto" height="100%"></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
    </form>

    <script language="javascript">
			
		function DoSelectSupplierReturn ( code,name)
		{
			Form1.txtSupplierCode.value = code;
			Form1.txtSupplierName.value = name;
		}

		function openSelectSupplier()
		{
			OpenMiddleWindow( '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=Request["ProjectCode"]%>','ѡ��Ӧ��' );
		}

		function viewSupplier( supplierCode )
		{
			OpenFullWindow( '../Supplier/SupplierInfo.aspx?SupplierCode=' + supplierCode , '��Ӧ����Ϣ' );
		}
		function doNewContract()
		{
			OpenFullWindow('ContractModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&act=Add','������ͬ');
		}
		
	
		function doViewContractInfo( code )
		{
			OpenFullWindow('ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'��ͬ��Ϣ');
		}
		
		function DoViewChange(ContractChangeCode,ContractCode)
	{
		OpenFullWindow('../Contract/ContractChangeInfo.aspx?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode='+ContractCode+'&ContractChangeCode='+ContractChangeCode,'��ͬ�����Ϣ');
	}

//�߼���ѯ
function ShowAdvSearch()
{
	var display = Form1.txtAdvSearch.value;
	
	if ( display == "none" )
	{
		display = "block";
	}
	else
	{
		display = "none";
	}
	
	Form1.txtAdvSearch.value = display;
	
	SetAdvSearch();;
}

function SetAdvSearch()
{
	document.all("divAdvSearch").style.display = Form1.txtAdvSearch.value;

	if ( Form1.txtAdvSearch.value == "none" )
	{
//		Form1.imgAdvSearch.src = "../images/ArrowDown.gif";
		Form1.imgAdvSearch.title = "�߼���ѯ";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		Form1.imgAdvSearch.title = "���ظ߼���ѯ";
	}
}

//�����������س�
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		Form1.btnSearch.click();
	}
}

SetAdvSearch();

    </script>

</body>
</html>
