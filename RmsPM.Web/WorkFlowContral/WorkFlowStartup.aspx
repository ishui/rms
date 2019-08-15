<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlowStartup.aspx.cs" Inherits="WorkFlowContral_WorkFlowStartup" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>启动流程</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>


    <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
        type="text/css" />

</head>
<body style="BORDER-RIGHT: 0px" scroll="no">
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
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span
                                    id="spanTitle" runat="server"> 流程&nbsp;启动流程</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
           
            <tr height="100%">
                <td class="table" valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <asp:DataGrid ID="dgList" runat="server" Width="100%" PageSize="18" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="0" CssClass="list" OnSelectedIndexChanged="dgList_SelectedIndexChanged">
                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                            <FooterStyle CssClass="list-title"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="流程名称">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <a href="##" onclick='modifyProcedure("<%# RmsPM.BLL.WorkFlowRule.GetProcedureURLByName((string)DataBinder.Eval(Container.DataItem, "ProcedureName")) %>","<%# DataBinder.Eval(Container.DataItem, "ProcedureName") %>")'>
                                            <%# DataBinder.Eval(Container.DataItem, "Description") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ProcedureName" HeaderText="索引">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="类型">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetProcedureTypeNameByValue(DataBinder.Eval(Container, "DataItem.Type").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="版本号">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.VersionNumber") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="当前有效">
                                    <ItemTemplate>
                                        <%# ((int)DataBinder.Eval(Container, "DataItem.Activity") == 1) ? "是" : "否"%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="项目">
                                    <ItemTemplate>
                                        <%# RmsPM.BLL.ProjectRule.GetProjectName((string)DataBinder.Eval(Container, "DataItem.ProjectCode"))%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:BoundColumn DataField="ProcedureCode" Visible="False"></asp:BoundColumn>
                            </Columns>
                            <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                CssClass="ListHeadTr"></PagerStyle>
                        </asp:DataGrid>
                        
                    </div>
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
    </form>

    <script>
    
    function modifyProcedure(ProcedurePath,ProcedureName)
    {
        OpenFullWindow(ProcedurePath+"?status=new&ProcedureName="+ProcedureName)
    }
	function gotoDirect ( caseCode, actCode , path , applicationCode)
	{
	    
		OpenFullWindow(  path + ((path.indexOf("?")>0)?"&":"?")+'action=Sign&CaseCode='+caseCode + '&actCode=' + actCode + "&applicationCode=" + applicationCode ,'流程处理');
	}
    </script>

</body>
</html>
