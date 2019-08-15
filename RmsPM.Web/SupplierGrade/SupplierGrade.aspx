<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierGrade.aspx.cs" Inherits="SupplierGrade_SupplierGrade" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>供应商评分管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
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
                                供应商评分管理 - 供应商评分搜索</td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <img src="../images/btn_li.gif" align="absMiddle">
                    <input class="button" id="btnNewWorkFlow" visible=false onclick="doNewGrade('');return false;" type="button"
                        value="提交承包商评分审核" name="btnNew" runat="server"><input class="button" id="btnNewPursveWorkflow" onclick="doNewPursveGrade('');return false;" type="button"
                        value="提交供应商评分审核" name="btnNew" visible=false runat="server">
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
                                                    <td>评分表类型：</td>
													    <td><asp:DropDownList ID="ddlWorkFlowTypeView" runat="server">
                                                            <asp:ListItem  Value="">所有类型</asp:ListItem>
                                                            <asp:ListItem Value="100001">承包商评分</asp:ListItem>
                                                            <asp:ListItem Value="100002">供应商评分</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    
                                                      <td>  状态：</td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cblStatus" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0">已审</asp:ListItem>
																<asp:ListItem Value="1,8">申请</asp:ListItem>
																<asp:ListItem Value="7,9">审核中</asp:ListItem>
																
                                                        </asp:CheckBoxList></td>
                                                    <td>
                                                        <input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>                                                                                                          
                                                    承包商/供应商：</td>
                                                    <td >
                                                        <input class="input" id="txtSupplierName" type="text" name="txtSupplierName" runat="server">
                                                        <input id="txtSupplierCode" type="hidden" name="txtSupplierCode" runat="server">
                                                        <a onclick="openSelectSupplier();" href="##">
                                                    <img src="../images/ToolsItemSearch.gif" border="0"></a>                                                  </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        承包商项目经理/供应商项目负责人：</td>
                                                    <td>
                                                        <input class="input" id="txtProjectManage" type="text" name="txtProjectManage" runat="server"></td>
                                                </tr>
                                            </table>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr height="100%" >
                            <td valign="top"> 
										<asp:datagrid id="dgList" runat="server" AutoGenerateColumns="False" PageSize="14" AllowPaging="True"
											GridLines="Horizontal" CellPadding="0" Width="100%" CssClass="list" AllowSorting="true" ShowFooter="True">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
											    <asp:BoundColumn DataField="ApplicationCode" Visible=false></asp:BoundColumn>
												
												<asp:TemplateColumn SortExpression="" HeaderText="承包商/供应商">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
													  <a href="##" onclick='gotoDirect("<%# DataBinder.Eval(Container.DataItem, "ApplicationPath") %>","<%# DataBinder.Eval(Container.DataItem, "CaseCode") %>","<%# DataBinder.Eval(Container.DataItem, "ApplicationCode")%>","<%# ((string)DataBinder.Eval(Container.DataItem, "State"))=="0"?"List":""%>"); return false;'>
                                                                <%#  RmsPM.BLL.ProjectRule.GetSupplierName( DataBinder.Eval( Container,"DataItem.SupplierCode" ).ToString() ) %>
                                                        </a>
	
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="" HeaderText="评分类型">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>

	                                                    <%# DataBinder.Eval(Container.DataItem, "Description") %>
													</ItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn HeaderText="承包商项目经理/供应商项目负责人">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "ProjectManage") %>
													</ItemTemplate>
																										
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="状态">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.GradeMessage.GetContractStatusName(System.Convert.ToString(DataBinder.Eval(Container.DataItem, "State")))%>
													</ItemTemplate>
																										
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="创建日期">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%#System.Convert.ToString(DataBinder.Eval(Container.DataItem, "CreateDate"))%>
													</ItemTemplate>
																										
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
										<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc1:GridPagination>
										
									
                                &nbsp;
                               
                            </td>
                        </tr>                        
                    </table>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif" style="height: 12px">
                                <img height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12" style="height: 12px">
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
    </form>

    <script language="javascript">
		function gotoDirect ( path , CaseCode,ApplicationCode,frameType)
	    {
		    OpenFullWindow(  path + ((path.indexOf("?")>0)?"&":"?")+"CaseCode=" + CaseCode +"&ApplicationCode="+ApplicationCode+"&frameType="+frameType,'流程处理');
	    }
		function DoSelectSupplierReturn ( code,name)
		{
			Form1.txtSupplierCode.value = code;
			Form1.txtSupplierName.value = name;
		}

		function openSelectSupplier()
		{
			OpenMiddleWindow( '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=Request["ProjectCode"]%>','选择供应商' );
		}

		function viewSupplier( supplierCode,gradeMessageCode )
		{
			OpenFullWindow( '<%=SupplierGradeInfoPage%>?ProjectCode=<%=Request["ProjectCode"]%>&act=View&SupplierCode=' + supplierCode +'&gradeMessageCode=' + gradeMessageCode , '供应商评分表' );
		}
		
		
		function doNewGrade()
		{
			OpenFullWindow('<%=SupplierGradeInfoPage%>?ProjectCode=<%=Request["ProjectCode"]%>&act=Add','新增评分');
		}
		
		function doNewPursveGrade()
		{
		    OpenFullWindow('<%=PursveSupplierGradeInfoPage%>?ProjectCode=<%=Request["ProjectCode"]%>&act=Add','新增供应商评分');
		}
		
	
		function doViewContractInfo( code )
		{
			OpenFullWindow('ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'合同信息');
		}



//搜索条件按回车
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		Form1.btnSearch.click();
	}
}

//SetAdvSearch();

    </script>

</body>
</html>
