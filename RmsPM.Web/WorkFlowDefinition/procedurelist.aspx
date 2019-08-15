<%@ Page Language="c#" Inherits="RmsPM.Web.WorkFlowDefinition.ProcedureList"
    CodeFile="ProcedureList.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>流程列表</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
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
                                    id="spanTitle" runat="server"> 流程定义&nbsp;流程列表</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <img src="../images/btn_li.gif" align="absMiddle">
                    <input class="button" id="btnAdd" onclick="addNewProcedure();return false;" type="button" value="新 增" name="btnAdd">
                    <input class="button" id="BT_OutAll" type="button" value="导出全部" runat="server" name="BT_OutAll" onserverclick="BT_OutAll_ServerClick">
                    <input class="button" id="Button4" type="button" value="导出选定流程" runat="server" name="BT_OutAll" onserverclick="Button4_ServerClick">
                    <input class="button" id="btnWorkflowRoleOut" type="button" value="导出全局流程角色" runat="server" name="btnWorkflowRoleOut" onserverclick="btnWorkflowRoleOut_ServerClick">
                    <input class="button" id="btnWorkflowRoleIn" type="button" value="导入全局流程角色"  runat="server" name="btnWorkflowRoleIn" onserverclick="btnWorkflowRoleIn_ServerClick">
                    <input class="button" id="Button5" type="button" value="导入部分" runat="server" name="BT_OutWorkFlow" onserverclick="Button5_ServerClick" >
                    <input class="button" id="BT_OutWorkFlow" type="button" value="导入全部" runat="server" name="BT_OutWorkFlow" onserverclick="BT_OutWorkFlow_ServerClick"><br>
                    
                    <div runat="server" id="fileRolediv" style="display:none;">
                        <img src="../images/btn_li.gif" align="absMiddle">
                        <input id="UpRoleFile" style="width: 232px; height: 22px" type="file" size="19" runat="server" name="UpRoleFile">
                        <font color="red">导入流程角色</font>
                    </div>
                    <div runat="server" id="filediv" style="display:none;">
                        <img src="../images/btn_li.gif" align="absMiddle">
                        <input id="UpFile" style="width: 232px; height: 22px" runat="server" type="file" size="19" name="UpFile">
                         <font color="red">导入流程</font>
                    </div>
                 </td>
            </tr>
            <tr height="1">
                <td valign="top">
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            流程名称：</td>
                                        <td><input class="input" type="text" runat="server" id="txtDescription" /></td>
                                        <td>
                                            项 &nbsp; &nbsp;&nbsp; 目：</td>
                                        <td>
                                            <asp:DropDownList ID="DropDownProject" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="">--选择--</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>
                                            类 &nbsp; &nbsp;&nbsp; 型：</td>
                                        <td><asp:DropDownList ID="DropDownType" runat="server">
                                            <asp:ListItem Value="">--选择--</asp:ListItem>
                                            <asp:ListItem Value="0">常规类型</asp:ListItem>
                                            <asp:ListItem Value="1">通用类型</asp:ListItem>
                                        </asp:DropDownList></td>
                                        <td rowspan="3"> &nbsp;<input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
                                                onserverclick="btnSearch_ServerClick">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        索 &nbsp; &nbsp;&nbsp; 引：</td>
                                        <td>
                                            <input class="input" type="text" runat="server" id="txtProcedureName" /></td>
                                        <td>
                                            当前有效：</td>
                                        <td><asp:DropDownList ID="DropDownActivity" runat="server">
                                            <asp:ListItem Value="">--选择--</asp:ListItem>
                                            <asp:ListItem Value="1">当前有效</asp:ListItem>
                                            <asp:ListItem Value="0">当前无效</asp:ListItem>
                                        </asp:DropDownList></td>
                                        <td>
                                            版 本 号：</td>
                                        <td>
                                            <input type="text" class="input" runat="server" id="VersionNumber" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <asp:DataGrid ID="dgList" runat="server" Width="100%" PageSize="18" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="0" CssClass="list" OnSelectedIndexChanged="dgList_SelectedIndexChanged" AllowSorting="True" OnSortCommand="dgList_SortCommand">
                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                            <FooterStyle CssClass="list-title"></FooterStyle>
                            <Columns>
                             <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <%# Container.ItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                               
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="流程名称" SortExpression="Description">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <a href="##" onclick='modifyProcedure("<%# DataBinder.Eval(Container.DataItem, "ProcedureCode") %>")'>
                                            <%# DataBinder.Eval(Container.DataItem, "Description") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ProcedureName" HeaderText="索引" SortExpression="ProcedureName">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="类型" SortExpression="Type">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetProcedureTypeNameByValue(DataBinder.Eval(Container, "DataItem.Type").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="版本号" SortExpression="VersionNumber">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.VersionNumber") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="版本说明" SortExpression="VersionDescription">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.VersionDescription")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="当前有效" SortExpression="Activity">
                                    <ItemTemplate>
                                        <%# ((int)DataBinder.Eval(Container, "DataItem.Activity") == 1) ? "是" : "否"%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="项目" SortExpression="ProjectCode">
                                    <ItemTemplate>
                                        <%# RmsPM.BLL.ProjectRule.GetProjectName((string)DataBinder.Eval(Container, "DataItem.ProjectCode"))%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:ButtonColumn CommandName="Select" Text="复制"></asp:ButtonColumn>
                                <asp:BoundColumn DataField="ProcedureCode" Visible="False"></asp:BoundColumn>
                            </Columns>
                            <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                CssClass="ListHeadTr"></PagerStyle>
                        </asp:DataGrid>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="list">
                                <td colspan="2" style="height: 19px">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="list">
                                        <tr class="list-title">
                                            <td style="width: 264px" nowrap="noWrap">
                                                &nbsp;流水号中序列号部分长度：
                                            </td>
                                            <td style="width: 17px">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" Width="168px"><asp:ListItem Value="4" Selected="True">4位</asp:ListItem><asp:ListItem Value="6">6位</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                            <td style="width: 100px">
                                                <asp:Button ID="Button3" runat="server" CssClass="submit" Text="更新" OnClick="Button3_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="100%">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list"
                                        DataKeyNames="ProjectCode" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectName" HeaderText="项目名称" />
                                            <asp:TemplateField HeaderText="流水号中项目代码">
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.SystemRule.GetProjectConfigValue(Eval("ProjectCode").ToString(),"FlowNumber") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="list-title" />
                                    </asp:GridView>
                                </td>
                                <td valign="top">
                                    <table class="list" runat="server" id="FlowConfigTable" visible="false" style="width: 212px">
                                        <tr>
                                            <td style="width: 100px" class="list-title">
                                                项目：</td>
                                            <td style="width: 100px" class="list-title">
                                                <asp:Label ID="lblProjectName" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                                代码：</td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txtFlowNumber" runat="server" CssClass="input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="height: 24px">
                                                <asp:Button ID="Button1" runat="server" CssClass="submit" Text="保存" OnClick="Button1_Click" />
                                                <asp:Button ID="Button2" runat="server" CssClass="submit" Text="取消" OnClick="Button2_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
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

    <script language="javascript">
	
		function addNewProcedure()
		{
			OpenFullWindow( 'ProcedureManage.aspx' ,'流程新增');
		}
	
		function modifyProcedure( procedureCode )
		{
			OpenFullWindow( 'ProcedureManage.aspx?ProcedureCode=' + procedureCode ,'流程修改');
		}


    </script>

</body>
</html>
