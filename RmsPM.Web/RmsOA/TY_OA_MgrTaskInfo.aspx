<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TY_OA_MgrTaskInfo.aspx.cs"
    Inherits="RmsOA_TY_OA_MgrTaskInfo" %>

<%@ Register TagPrefix="uc1" TagName="Workflowselect" Src="../WorkFlowControl/WorkFlowselect.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUsers" Src="../UserControls/InputUsers.ascx" %>
<%--<%@ Register TagPrefix="uc1" TagName="WorkFlowControl_workflowselect " Src="../WorkFlowControl/workflowselect.ascx" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>总经理交办事宜</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript">
    function ShowDtlInfo(code)
    {
     OpenLargeWindow('TY_OA_MgrTaskDtlInfo.aspx?MgrDtlCode='+code+'&MgrCode=<%= Request["MgrCode"]+"" %>','任务明细');
    }
    
    function doViewContractInfo(code)
    {
	    OpenFullWindow('../Contract/ContractInfo.aspx?ContractCode=' + code,' 信息');
    }
	function MonitorgotoDirect ( caseCode, actCode , path , applicationCode, status)
	{
//		if ( status == 'End' )
//		{
			OpenFullWindow(  path + ((path.indexOf("?")>0)?"&":"?")+'CaseCode='+caseCode+'&applicationCode=' + applicationCode ,'流程信息');
//		}
//		else
//		{
//			OpenFullWindow(  path + ((path.indexOf("?")>0)?"&":"?")+'frameType=List&action=View&caseCode='+caseCode + '&actCode=' + actCode + "&applicationCode=" + applicationCode ,'流程处理');
//		}
	}
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td style="height: 25px">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                总经理交办事宜</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td valign="top" style="height: 100%">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" OnDataBound="FormView1_DataBound"
                        OnItemInserting="FormView1_ItemInserting" Width="100%" Height="100%" DataKeyNames="code"
                        OnItemDeleted="FormView1_ItemDeleted" OnItemUpdated="FormView1_ItemUpdated" OnItemUpdating="FormView1_ItemUpdating">
                        <EditItemTemplate>
                            <table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                                <tr>
                                    <td>
                                        <table class="table" id="tableToolBar" width="100%">
                                            <tr valign="top">
                                                <td class="tools-area" width="16">
                                                    <img src="../images/btn_li.gif" align="absMiddle"></td>
                                                <td class="tools-area">
                                                    <asp:Button ID="btnSave" Text=" 保存 " CausesValidation="true" CssClass="button" runat="server"
                                                        CommandName="Update" />
                                                    <asp:Button ID="btnCancel" Text=" 取消 " CssClass="button" runat="server" CommandName="Cancel" />
                                                    <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                                        onclick="javascript:window.close();">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="TABLE1" class="form">
                                            <tr>
                                                <td class="form-item" width="15%">
                                                    编号</td>
                                                <td>
                                                    <asp:Label ID="MgrTaskID" runat="server" Text='<%# Bind("MgrTaskID") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="TaskName" runat="server" Text="任务主题"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtTaskName" runat="server" Text='<%# Bind("TaskName") %>'></asp:TextBox>
                                                    <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label2" runat="server" Text="交办事由"></asp:Label></td>
                                                <td>
                                                    <textarea id="TxtTaskTail" style="width: 100%" name="TxtTaskTail" rows="10" value='<%# Bind("TaskDetail") %>'
                                                        runat="server"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="form-item">
                                                    <asp:Label ID="Label3" runat="server" Text="附件"></asp:Label></td>
                                                <td>
                                                    <uc1:AttachMentAdd ID="Attachmentadd1" runat="server" MasterCode='<%# Bind("Code")%>'
                                                        AttachMentType="zjinlifujian"></uc1:AttachMentAdd>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label10" runat="server" Text="相关链接"></asp:Label>
                                                </td>
                                                <td>
                                                    <uc1:Workflowselect ID="WorkFlowMonitor1" runat="server" Value='<%# Bind("ReferLink")%>'>
                                                    </uc1:Workflowselect>
                                                    <%--                                                    <a href="#" onclick="doViewContractInfo(this.code,this.Projectcode);return false;"
                                                        code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>' projectcode='<%#  DataBinder.Eval(Container.DataItem, "Projectcode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "ContractName")%>
                                                    </a>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label4" runat="server" Text="交办日期"></asp:Label></td>
                                                <td>
                                                    <cc3:Calendar ID="CreateDate" runat="server" CalendarResource="../Images/CalendarResource/"
                                                        ReadOnly="False" Display="True" Value='<%# Bind("CreateDate") %>'>
                                                    </cc3:Calendar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    交办事项</td>
                                                <td>
                                                    <input class="button-small" id="btnAddDtl" type="button" value="新 增" name="btnAddDtl"
                                                        runat="server" onserverclick="btnAddDtl_ServerClick"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr height="100%">
                                    <td>
                                        <div style="overflow: auto; width: 100%; position: absolute; height: 100%">
                                            <asp:DataGrid ID="dgDtl" runat="server" DataKeyField="MgrDtlCode" CellPadding="0"
                                                GridLines="Horizontal" AutoGenerateColumns="False" PageSize="15" Width="100%"
                                                CssClass="list" OnDeleteCommand="dgDtl_DeleteCommand">
                                                <HeaderStyle CssClass="list-title" />
                                                <FooterStyle CssClass="list-title" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="序号">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                        <ItemTemplate>
                                                            <%# Container.ItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            交办具体工作事项</HeaderTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Wrap="False" Width="40%" />
                                                        <ItemTemplate>
                                                            <textarea id="TxtTaskDetail" style="width: 100%" name="TxtTaskTail" rows="4" value='<%# Bind("MgrDtlInfo") %>'
                                                                runat="server"></textarea>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <HeaderTemplate>
                                                            要求完成日期</HeaderTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                        <ItemTemplate>
                                                            <cc3:Calendar ID="CalendarInDate" runat="server" CalendarResource="../Images/CalendarResource/"
                                                                ReadOnly="False" Display="True" Value='<%# Bind("DeadLine") %>'>
                                                            </cc3:Calendar>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="责任人">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                        <ItemTemplate>
                                                            <uc1:InputUser ID="ResponsePerson" runat="server" Value='<%# Bind("ResponsePerson") %>'>
                                                            </uc1:InputUser>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="协办人">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Wrap="False" Width="30%" />
                                                        <ItemTemplate>
                                                            <uc1:InputUsers ID="txtteamer" runat="server" Value='<%# Bind("Assistpersons") %>'  MustInput="false" ButtonName="选择协办人">
                                                            </uc1:InputUsers>
                                                        </ItemTemplate>
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
                                            </asp:DataGrid></div>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                                <tr>
                                    <td>
                                        <table class="table" id="tableToolBar" width="100%">
                                            <tr>
                                                <td class="tools-area" width="16">
                                                    <img src="../images/btn_li.gif" align="absMiddle" /></td>
                                                <td class="tools-area">
                                                    <asp:Button ID="btnSave" Text=" 保存 " CausesValidation="true" CssClass="button" runat="server"
                                                        CommandName="Insert" />
                                                    <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                                        onclick="javascript:window.close();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="TABLE1" class="form">
                                            <tr>
                                                <td class="form-item" width="15%">
                                                    编号</td>
                                                <td>
                                                    <asp:Label ID="MgrTaskID" runat="server" Text='<%# Bind("MgrTaskID") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="TaskName" runat="server" Text="任务主题"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtTaskName" runat="server" Text='<%# Bind("TaskName") %>'></asp:TextBox>
                                                    <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label2" runat="server" Text="交办事由"></asp:Label></td>
                                                <td>
                                                    <textarea id="TxtTaskTail" style="width: 100%" name="TxtTaskTail" rows="10" value='<%# Bind("TaskDetail") %>'
                                                        runat="server"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="form-item">
                                                    <asp:Label ID="Label3" runat="server" Text="附件"></asp:Label></td>
                                                <td>
                                                    <uc1:AttachMentAdd ID="Attachmentadd1" runat="server" CtrlPath="../UserControls/"
                                                        MasterCode='<%# Bind("Code")%>' AttachMentType="zjinlifujian"></uc1:AttachMentAdd>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label9" runat="server" Text="相关链接"></asp:Label></td>
                                                <td>
                                                    <uc1:Workflowselect ID="WorkFlowMonitor1" runat="server" Value='<%# Bind("ReferLink")%>'>
                                                    </uc1:Workflowselect>
                                                    <%--                                                    <a href="#" onclick="doViewContractInfo(this.code,this.Projectcode);return false;"
                                                        code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>' projectcode='<%#  DataBinder.Eval(Container.DataItem, "Projectcode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "ContractName")%>
                                                    </a>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label4" runat="server" Text="交办日期"></asp:Label></td>
                                                <td>
                                                    <cc3:Calendar ID="CreateDate" runat="server" CalendarResource="../Images/CalendarResource/"
                                                        ReadOnly="False" Display="True" Value='<%# Bind("CreateDate") %>'>
                                                    </cc3:Calendar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td class="intopic" width="200">
                                                    交办事项</td>
                                                <td>
                                                    <input class="button-small" id="btnAddDtl" type="button" value="新 增" name="btnAddDtl"
                                                        runat="server" onserverclick="btnAddDtl_ServerClick"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr height="100%">
                                    <td>
                                        <div style="overflow: auto; width: 100%; position: absolute; height: 100%">
                                            <asp:DataGrid ID="dgDtl" runat="server" DataKeyField="MgrDtlCode" CellPadding="0"
                                                GridLines="Horizontal" AutoGenerateColumns="False" PageSize="15" Width="100%"
                                                CssClass="list" OnDeleteCommand="dgDtl_DeleteCommand">
                                                <HeaderStyle CssClass="list-title" />
                                                <FooterStyle CssClass="list-title" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="序号">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                        <ItemTemplate>
                                                            <%# Container.ItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            交办具体工作事项</HeaderTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Wrap="False" Width="40%" />
                                                        <ItemTemplate>
                                                            <textarea id="TxtTaskDetail" style="width: 100%" name="TxtTaskDetail" rows="4" value='<%# Bind("MgrDtlInfo") %>'
                                                                runat="server"></textarea>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <HeaderTemplate>
                                                            要求完成日期</HeaderTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                        <ItemTemplate>
                                                            <cc3:Calendar ID="CalendarInDate" runat="server" CalendarResource="../Images/CalendarResource/"
                                                                ReadOnly="False" Display="True" Value='<%# Bind("DeadLine") %>'>
                                                            </cc3:Calendar>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="责任人">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                        <ItemTemplate>
                                                            <uc1:InputUser ID="ResponsePerson" runat="server" Value='<%# Bind("ResponsePerson") %>'>
                                                            </uc1:InputUser>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="协作人">
                                                        <HeaderStyle HorizontalAlign="Right" Width="30%" />
                                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                        <ItemTemplate>
                                                            <uc1:InputUsers ID="txtteamer" runat="server" Value='<%# Bind("Assistpersons") %>' MustInput="false" ButtonName="选择协作人">
                                                            </uc1:InputUsers>
                                                     
                                                        </ItemTemplate>
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
                                            </asp:DataGrid></div>
                                    </td>
                                </tr>
                            </table>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                                <tr>
                                    <td>
                                        <table class="table" id="tableToolBar" width="100%">
                                            <tr>
                                                <td class="tools-area" width="16">
                                                    <img src="../images/btn_li.gif" align="absMiddle"></td>
                                                <td class="tools-area">
                                                    <asp:Button ID="btnModify" Text=" 修改 " CssClass="button" runat="server" CommandName="Edit" />
                                                    <asp:Button ID="btnDelete" Text=" 删除 " CssClass="button" runat="server" CommandName="Delete" />
                                                    <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                                        onclick="javascript:window.close();">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" width="100%" border="0" cellspacing="0" class="form">
                                            <tr>
                                                <td class="form-item" width="15%" align="center">
                                                    编号</td>
                                                <td colspan="3">
                                                    <asp:Label ID="MgrTaskID" runat="server" Text='<%# Bind("MgrTaskID") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label5" runat="server" Text="任务主题"></asp:Label></td>
                                                <td>
                                                    <%# Eval("TaskName") %>
                                                </td>
                                                <td class="form-item" width="15%" align="center">
                                                    <asp:Label ID="Label1" runat="server" Text="表单状态"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="StatusID" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label6" runat="server" Text="交办事由"></asp:Label></td>
                                                <td colspan="3">
                                                    <textarea readonly id="TxtEyeTaskTail" style="width: 100%" name="TxtEyeTaskTail"
                                                        rows="10" value='<%# Bind("TaskDetail") %>' runat="server"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="form-item">
                                                    <asp:Label ID="Label7" runat="server" Text="附件"></asp:Label></td>
                                                <td colspan="3">
                                                    <uc1:AttachMentList ID="AttachMentList1" runat="server" CtrlPath="../UserControls/"
                                                        MasterCode='<%# Bind("Code")%>' AttachMentType="zjinlifujian"></uc1:AttachMentList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label9" runat="server" Text="相关链接"></asp:Label></td>
                                                <td colspan="3" runat="server" id="ReferLinkID">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    <asp:Label ID="Label8" runat="server" Text="交办日期"></asp:Label></td>
                                                <td colspan="3">
                                                    <cc3:Calendar ID="EyeCreateDate" runat="server" CalendarResource="../Images/CalendarResource/"
                                                        ReadOnly="true" Display="True" Value='<%# Bind("CreateDate") %>'>
                                                    </cc3:Calendar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td class="intopic" width="200" nowrap>
                                                    交办事项</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr height="100%">
                                    <td>
                                        <div style="overflow: auto; width: 100%; position: absolute; height: 100%">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                CssClass="list" PageSize="5" Width="100%" DataSourceID="ObjectDataSource2" GridLines="Horizontal" OnDataBound="GridView1_DataBound">
                                                <HeaderStyle CssClass="list-title" />
                                                <FooterStyle CssClass="list-title" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="详细内容">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                        <ItemTemplate>
                                                            <a href="#" onclick="ShowDtlInfo(this.code); return false;" code='<%# DataBinder.Eval(Container.DataItem, "MgrDtlCode") %>'>
                                                                查 看
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="交办具体工作事项">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="40%" />
                                                        <ItemTemplate>
                                                            <textarea id="TxtEyeTaskTail1" style="width: 100%" name="TxtEyeTaskTail1" rows="2"
                                                                value='<%#(DataBinder.Eval(Container.DataItem, "MgrDtlInfo"))%>' runat="server"
                                                                readonly></textarea>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="要求完成日期">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                        <ItemTemplate>
                                                            <%#RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "DeadLine"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="责任人">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="EyeResponseperson" Width="100%" runat="server" Text=' <%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(Eval("ResponsePerson")))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="协作人">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="30%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="Eyeteamer" Width="100%" runat="server" Text=' <%# Eval("Assistpersons")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="审核状态">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                                        <ItemTemplate>
                                                            <asp:Label ID="EyeStatus" Width="100%" runat="server" Text='<%# RmsOA.BLL.TY_OA_MgrTaskBLL.GetStatusName(Eval("State").ToString())%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    无匹配数据
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </td>
            </tr>
        </table>
        <asp:TextBox runat="server" ID="MgrCode" Visible="false"></asp:TextBox>
    </form>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnInserted="ObjectDataSource1_Inserted"
        SelectMethod="GetTY_OA_MgrTask" TypeName="RmsOA.BFL.TY_OA_MgrTaskBFL" OldValuesParameterFormatString="original_{0}"
        DataObjectTypeName="RmsOA.MODEL.TY_OA_MgrTaskModel" DeleteMethod="Delete"
        InsertMethod="Insert" UpdateMethod="Update">
        <SelectParameters>
            <asp:QueryStringParameter Name="Code" QueryStringField="MgrCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" TypeName="RmsOA.BFL.TY_OA_MgrTaskDtlBFL"
        runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetTY_OA_MgrTaskDtlList">
        <SelectParameters>
            <asp:QueryStringParameter Name="MgrCodeID" QueryStringField="MgrCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</body>
</html>
