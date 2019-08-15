<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CL_CarRegisterEdit.aspx.cs"
    Inherits="RmsOA_CL_CarRegisterEdit" %>

<%@ Register Src="../WorkFlowControl/workflowtoolbar.ascx" TagName="workflowtoolbar"
    TagPrefix="uc5" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/inputcontract.ascx" TagName="inputcontract" TagPrefix="uc3" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>车辆登记</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript" language="javascript">
        function SelectUnit()
		{
			OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.FormView1_txtUnitName.value = name;
			window.document.all.FormView1_txtUnit.value = code;
		}	
		
		function OpenRequisition()
        {
		   OpenMiddleWindow('CL_CarMaintenanceEdit.aspx?ActType=add&Car_Code=<%= Request["Car_code"] %> ','CarMaintenanceEdit');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                车辆登记</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" OnItemInserting="FormView1_ItemInserting"
                        DataKeyNames="Car_Code" OnDataBound="FormView1_DataBound" OnDataBinding="FormView1_DataBinding">
                        <EditItemTemplate>
                            <table id="Table2" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" runat="server" CommandName="Update" CssClass="button" Text=" 保存 " />&nbsp;
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        质量记录号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='GKFC-JL-ZY-630203'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="Index_Num" runat="server" CssClass="input" Text='<%# Bind("Index_Num") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        车号：</td>
                                    <td>
                                        <asp:TextBox ID="Car_id" runat="server" CssClass="input" Text='<%# Bind("Car_id") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        车型：</td>
                                    <td>
                                        <asp:TextBox ID="Car_Type" runat="server" CssClass="input" Text='<%# Bind("Car_Type") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        购买日期：</td>
                                    <td>
                                        <cc3:Calendar ID="buydate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("Buy_Date") %>'>
                                        </cc3:Calendar>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        车架号：</td>
                                    <td>
                                        <asp:TextBox ID="Chejia_Id" runat="server" CssClass="input" Text='<%# Bind("Chejia_Id") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        发动机号：</td>
                                    <td>
                                        <asp:TextBox ID="Fadongji_Id" runat="server" CssClass="input" Text='<%# Bind("Fadongji_Id") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table id="Table1" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" runat="server" CommandName="Insert" CssClass="button" Text=" 保存 " />
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        质量记录号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='GKFC-JL-ZY-630203'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="IndexNum" runat="server" CssClass="input" Text='<%# Bind("Index_Num") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        车号：</td>
                                    <td>
                                        <asp:TextBox ID="Car_id" runat="server" CssClass="input" Text='<%# Bind("Car_id") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        车型：</td>
                                    <td>
                                        <asp:TextBox ID="Car_Type" runat="server" CssClass="input" Text='<%# Bind("Car_Type") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        购买日期：</td>
                                    <td>
                                        <cc3:Calendar ID="BuyDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("Buy_Date") %>'>
                                        </cc3:Calendar>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        车架号：</td>
                                    <td>
                                        <asp:TextBox ID="Chejia_Id" runat="server" CssClass="input" Text='<%# Bind("Chejia_Id") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        发动机号：</td>
                                    <td>
                                        <asp:TextBox ID="Fadongji_Id" runat="server" CssClass="input" Text='<%# Bind("Fadongji_Id") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <table id="Table3" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" CssClass="button" Text=" 修改 " />
                                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button"
                                            Text=" 删除 " />
                                        <input name="AddButton" id="AddButton" type="button" value=" 新增维修记录 " class="button"
                                            runat="server" onclick="javascript:OpenRequisition();return false;">
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        质量记录号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='GKFC-JL-ZY-630203'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:Label ID="Index_Num" runat="server" Text='<%# Bind("Index_Num") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        车号：</td>
                                    <td>
                                        <asp:Label ID="Car_id" runat="server" Text='<%# Bind("Car_id") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        车型：</td>
                                    <td>
                                        <asp:Label ID="Car_Type" runat="server" Text='<%# Bind("Car_Type") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        购买日期：</td>
                                    <td>
                                        <asp:Label ID="Buy_Date" runat="server" Text='<%# Bind("Buy_Date") %>'></asp:Label></td>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        车架号：</td>
                                    <td>
                                        <asp:Label ID="Chejia_Id" runat="server" Text='<%# Bind("Chejia_Id") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        发动机号：</td>
                                    <td>
                                        <asp:Label ID="Fadongji_Id" runat="server" Text='<%# Bind("Fadongji_Id") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="470" border="0">
                                <tr id="webtabs">
                                    <td width="20">
                                    </td>
                                    <td class="TabDisplay" id="workflowmsg" runat="server" width="185">
                                        维修保养记录</td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                            AllowSorting="True" CssClass="list" Width="100%" DataSourceID="ObjectDataSource2">
                                            <Columns>
                                                <asp:TemplateField HeaderText="维修/保养内容">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="javascript:OpenMiddleWindow('CL_CarMaintenanceEdit.aspx?Code=<%# Eval("Code")%>','MaintenanceDetail');return false;">
                                                            <%# Eval("MValue")%>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="日期">
                                                    <ItemTemplate>
                                                        <%# Eval("MDate").ToString().Substring(0,10)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Mil" HeaderText="公里数" />
                                                <asp:BoundField DataField="MPrice" HeaderText="花费金额" SortExpression="MPrice" />
                                            </Columns>
                                            <PagerStyle CssClass="list-title" />
                                            <HeaderStyle CssClass="list-title" />
                                            <EmptyDataTemplate>
                                                无匹配数据

                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_CarModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_CarListOne"
                        TypeName="RmsOA.BFL.GK_OA_CarBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Car_Code" QueryStringField="Car_Code" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SelectMethod="GetGK_OA_CarMaintenanceList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
                        TypeName="RmsOA.BFL.GK_OA_CarMaintenanceBFL">
                        <SelectParameters>
                            <asp:Parameter Name="sortColumns" Type="String" />
                            <asp:Parameter Name="startRecord" Type="Int32" />
                            <asp:Parameter Name="maxRecords" Type="Int32" />
                            <asp:Parameter Name="Car_CodeEqual" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
