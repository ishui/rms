<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YF_AssetEdit.aspx.cs" Inherits="RmsOA_YF_AssetEdit" %>

<%@ Register Src="../UserControls/attachmentadd.ascx" TagName="attachmentadd" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/attachmentlist.ascx" TagName="attachmentlist" TagPrefix="uc3" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <title>资产信息查看</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                            固定资产/资产信息</td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:FormView ID="FormView1" runat="server" Width="100%" DataSourceID="ObjectDataSource1" DataKeyNames="Code"
            OnDataBound="FormView1_DataBound" OnItemInserted="FormView1_ItemInserted" OnItemInserting="FormView1_ItemInserting" OnItemUpdated="FormView1_ItemUpdated" OnItemDeleted="FormView1_ItemDeleted">
            <EditItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                            <img align="absMiddle" alt="" src="../images/btn_li.gif">
                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CssClass="button"
                                CommandName="UpDate" Text="更新" />
                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                CssClass="button" Text="取消" />
                            <input class="button" onclick="self.close()" type="button" value="关闭" />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            设备名称
                        </td>
                        <td>
                            <asp:TextBox ID="FacilityNameTextBox" runat="server" CssClass="input" Text='<%# Bind("FacilityName") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            地区
                        </td>
                        <td>
                            <asp:TextBox ID="AreaTextBox" runat="server" CssClass="input" Text='<%# Bind("Area") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            设备编号：</td>
                        <td>
                            <asp:TextBox ID="CodeNOTextBox" runat="server" CssClass="input" Text='<%# Bind("CodeNO") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            品牌
                        </td>
                        <td>
                            <asp:TextBox ID="ProducerTextBox" runat="server" CssClass="input" Text='<%# Bind("Producer") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            购买公司
                        </td>
                        <td>
                            <uc1:inputunit ID="BuyCorpInputunit" runat="server" Value='<%# Bind("BuyCorp") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            型号
                        </td>
                        <td>
                            <asp:TextBox ID="ModelNOTextBox" runat="server" CssClass="input" Text='<%# Bind("ModelNO") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            所在公司
                        </td>
                        <td>
                            <uc1:inputunit ID="LayCorpInputunit" runat="server" Value='<%# Bind("LayCorp") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            类别
                        </td>
                        <td>
                        <asp:TextBox ID="TypeTextBox" runat="server" Text='<%# Bind("Type") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            使用部门
                        </td>
                        <td>
                            <uc1:inputunit ID="UseDeptInputunit" runat="server" Value='<%# Bind("UseDept") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            数量
                        </td>
                        <td>
                            <asp:TextBox ID="CountsTextBox" runat="server" CssClass="input" Text='<%# Bind("Counts") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            存放地点
                        </td>
                        <td>
                            <asp:TextBox ID="LayPlaceTextBox" runat="server" CssClass="input" Text='<%# Bind("LayPlace") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            计量单位
                        </td>
                        <td>
                            <asp:TextBox ID="CountUnitTextBox" runat="server" CssClass="input" Text='<%# Bind("CountUnit") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            产地
                        </td>
                        <td>
                            <asp:TextBox ID="ProdAreaTextBox" runat="server" CssClass="input" Text='<%# Bind("ProdArea") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            序列号
                        </td>
                        <td>
                            <asp:TextBox ID="SortNOTextBox" runat="server" CssClass="input" Text='<%# Bind("SortNO") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            资产分类
                        </td>
                        <td>
                            <asp:DropDownList ID="SortTypeDropDownList" runat="server" SelectedValue='<%# Bind("SortType") %>' DataSourceID="SortDropDownObjectDataSource">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            保修期
                        </td>
                        <td>
                            <asp:TextBox ID="FreeMainTextBox" runat="server" CssClass="input" Text='<%# Bind("FreeMain") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            设备类型
                        </td>
                        <td>
                            <asp:DropDownList ID="EquiTypeDropDownList" runat="server" SelectedValue='<%# Bind("EquiType") %>' DataSourceID="TypeDropDownObjectDataSource">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            供应商
                        </td>
                        <td>
                            <asp:TextBox ID="ProviderTextBox" runat="server" CssClass="input" Text='<%# Bind("Provider") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            购买日期
                        </td>
                        <td>
                            <cc1:Calendar ID="dateBegin" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("BuyDate") %>'>
                            </cc1:Calendar>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            价格
                        </td>
                        <td>
                            <asp:TextBox ID="PriceTextBox" runat="server" CssClass="input" Text='<%# Bind("Price") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PriceTextBox"
                                ErrorMessage="提示：输入字符格式错误！" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PriceTextBox"
                                ErrorMessage="提示不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            登记人
                        </td>
                        <td>
                            <asp:Label ID="RegisterLabel" runat="server" CssClass="input" Text='<%# Bind("Register") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            包卡位置
                        </td>
                        <td>
                            <asp:TextBox ID="MainCardPlaceTextBox" runat="server" CssClass="input" Text='<%# Bind("MainCardPlace") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            登记日期
                        </td>
                        <td>
                            <asp:Label ID="BookINTimeTextBox" runat="server" CssClass="input" Text='<%# Bind("BookINTime") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            状态
                        </td>
                        <td>
                            <asp:DropDownList ID="StoreStatusDropDownList" runat="server" SelectedValue='<%# Bind("StoreStatus") %>' DataSourceID="StatusDropDownObjectDataSource">
                            </asp:DropDownList>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            采购类型
                        </td>
                        <td>
                        <asp:TextBox ID="BuyTypeTextBox" runat="server" Text='<%# Bind("BuyType") %>' CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            附件
                        </td>
                        <td colspan="3">
                            <uc2:attachmentadd ID="Attachmentadd1" runat="server" AttachMentType="YF_AssetManage"
                                MasterCode='<%#Bind("Code") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="RemarkTextBox" runat="server" CssClass="input" Height="60px" Text='<%# Bind("Remark") %>'
                                TextMode="MultiLine" Width="90%">
                            </asp:TextBox>
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
                        <td class="form-item" style="width: 100px;">
                            设备名称
                        </td>
                        <td>
                            <asp:TextBox ID="FacilityNameTextBox" runat="server" CssClass="input" Text='<%# Bind("FacilityName") %>'></asp:TextBox>
                            <span style="color:Red;">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FacilityNameTextBox"
                                ErrorMessage="提示不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            地区
                        </td>
                        <td>
                            <asp:TextBox ID="AreaTextBox" runat="server" CssClass="input" Text='<%# Bind("Area") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            设备编号：</td>
                        <td>
                            <asp:TextBox ID="CodeNOTextBox" runat="server" CssClass="input" Text='<%# Bind("CodeNO") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            品牌
                        </td>
                        <td>
                            <asp:TextBox ID="ProducerTextBox" runat="server" CssClass="input" Text='<%# Bind("Producer") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            购买公司
                        </td>
                        <td>
                            <uc1:inputunit ID="BuyCorpInputunit" runat="server" Value='<%# Bind("BuyCorp") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            型号
                        </td>
                        <td>
                            <asp:TextBox ID="ModelNOTextBox" runat="server" CssClass="input" Text='<%# Bind("ModelNO") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            所在公司
                        </td>
                        <td>
                            <uc1:inputunit ID="LayCorpInputunit" runat="server" Value='<%# Bind("LayCorp") %>' />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            类别
                        </td>
                        <td>
                            <asp:TextBox ID="TypeTextBox" runat="server" Text='<%# Bind("Type") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            使用部门
                        </td>
                        <td>
                            <uc1:inputunit ID="UseDeptInputunit" runat="server" Value='<%# Bind("UseDept") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            数量
                        </td>
                        <td>
                            <asp:TextBox ID="CountsTextBox" runat="server" CssClass="input" Text='<%# Bind("Counts") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            存放地点
                        </td>
                        <td>
                            <asp:TextBox ID="LayPlaceTextBox" runat="server" CssClass="input" Text='<%# Bind("LayPlace") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            计量单位
                        </td>
                        <td>
                            <asp:TextBox ID="CountUnitTextBox" runat="server" CssClass="input" Text='<%# Bind("CountUnit") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            产地
                        </td>
                        <td>
                            <asp:TextBox ID="ProdAreaTextBox" runat="server" CssClass="input" Text='<%# Bind("ProdArea") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            序列号
                        </td>
                        <td>
                            <asp:TextBox ID="SortNOTextBox" runat="server" CssClass="input" Text='<%# Bind("SortNO") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            资产分类
                        </td>
                        <td>
                            <asp:DropDownList ID="SortTypeDropDownList" runat="server" SelectedValue='<%# Bind("SortType") %>' DataSourceID="SortDropDownObjectDataSource">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            保修期
                        </td>
                        <td>
                            <asp:TextBox ID="FreeMainTextBox" runat="server" CssClass="input" Text='<%# Bind("FreeMain") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            设备类型
                        </td>
                        <td>
                            <asp:DropDownList ID="EquiTypeDropDownList" runat="server" SelectedValue='<%# Bind("EquiType") %>' DataSourceID="TypeDropDownObjectDataSource">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            供应商
                        </td>
                        <td>
                            <asp:TextBox ID="ProviderTextBox" runat="server" CssClass="input" Text='<%# Bind("Provider") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            购买日期
                        </td>
                        <td>
                            <cc1:Calendar ID="dateBegin" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("BuyDate") %>'>
                            </cc1:Calendar>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            价格
                        </td>
                        <td>
                            <asp:TextBox ID="PriceTextBox" runat="server" CssClass="input" Text='<%# Bind("Price") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PriceTextBox"
                                ErrorMessage="提示：输入字符格式错误！" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PriceTextBox"
                                ErrorMessage="提示不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            登记人
                        </td>
                        <td>
                            <asp:Label ID="RegisterLabel" runat="server" CssClass="input" Text='<%# Bind("Register") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            包卡位置
                        </td>
                        <td>
                            <asp:TextBox ID="MainCardPlaceTextBox" runat="server" CssClass="input" Text='<%# Bind("MainCardPlace") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            登记日期
                        </td>
                        <td>
                            <asp:Label ID="BookINTimeTextBox" runat="server" CssClass="input" Text='<%# Bind("BookINTime") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            状态
                        </td>
                        <td>
                            <asp:DropDownList ID="StoreStatusDropDownList" runat="server" SelectedValue='<%# Bind("StoreStatus") %>' DataSourceID="StatusDropDownObjectDataSource">
                            </asp:DropDownList>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            采购类型
                        </td>
                        <td>
                            <asp:TextBox ID="BuyTypeTextBox" runat="server" Text='<%# Bind("BuyType") %>' CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            附件
                        </td>
                        <td colspan="3">
                            <uc2:attachmentadd AttachMentType="YF_AssetManage" ID="Attachmentadd1" runat="server" MasterCode='<%#Bind("Code") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="RemarkTextBox" runat="server" CssClass="input" Height="60px" Text='<%# Bind("Remark") %>'
                                TextMode="MultiLine" Width="90%">
                            </asp:TextBox>
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
                            <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                type="button" value=" 关闭 " />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            设备名称
                        </td>
                        <td>
                            <asp:Label ID="FacilityNameLabel" runat="server" Text='<%# Bind("FacilityName") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            地区
                        </td>
                        <td>
                            <asp:Label ID="AreaLabel" runat="server" Text='<%# Bind("Area") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px;" class="form-item">
                            设备编号</td>
                        <td>
                            <asp:Label ID="CodeNOLabel" runat="server" Text='<%# Bind("CodeNO") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            品牌
                        </td>
                        <td>
                            <asp:Label ID="ProducerLabel" runat="server" Text='<%# Bind("Producer") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            购买公司
                        </td>
                        <td>
                            <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("BuyCorp"))%>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            型号
                        </td>
                        <td>
                            <asp:Label ID="ModelNOLabel" runat="server" Text='<%# Bind("ModelNO") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            所在公司
                        </td>
                        <td>
                            <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("LayCorp"))%>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            类别
                        </td>
                        <td>
                            <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            使用部门
                        </td>
                        <td>
                            <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("UseDept"))%>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            数量
                        </td>
                        <td>
                            <asp:Label ID="CountsLabel" runat="server" Text='<%# Bind("Counts") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            存放地点
                        </td>
                        <td>
                            <asp:Label ID="LayPlaceLabel" runat="server" Text='<%# Bind("LayPlace") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            计量单位
                        </td>
                        <td>
                            <asp:Label ID="CountUnitLabel" runat="server" Text='<%# Bind("CountUnit") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            产地
                        </td>
                        <td>
                            <asp:Label ID="ProdAreaLabel" runat="server" Text='<%# Bind("ProdArea") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            序列号
                        </td>
                        <td>
                            <asp:Label ID="SortNOLabel" runat="server" Text='<%# Bind("SortNO") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            资产分类
                        </td>
                        <td>
                            <asp:Label ID="SortTypeLabel" runat="server" Text='<%# Bind("SortType") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            保修期
                        </td>
                        <td>
                            <asp:Label ID="FreeMainLabel" runat="server" Text='<%# Bind("FreeMain") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            设备类型
                        </td>
                        <td>
                            <asp:Label ID="EquiTypeLabel" runat="server" Text='<%# Bind("EquiType") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            供应商
                        </td>
                        <td>
                            <asp:Label ID="ProviderLabel" runat="server" Text='<%# Bind("Provider") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            购买日期
                        </td>
                        <td>
                            <asp:Label ID="BuyDateLabel" runat="server" Text='<%# Bind("BuyDate") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            价格
                        </td>
                        <td>
                            <asp:Label ID="PriceLabel" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            登记人
                        </td>
                        <td>
                            <asp:Label ID="RegisterLabel" runat="server" Text='<%# Bind("Register") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            包卡位置
                        </td>
                        <td>
                            <asp:Label ID="MainCardPlaceLabel" runat="server" Text='<%# Bind("MainCardPlace") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            登记日期
                        </td>
                        <td>
                            <asp:Label ID="BookINTimeLabel" runat="server" Text='<%# Bind("BookINTime") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            状态
                        </td>
                        <td>
                            <asp:Label ID="StoreStatusLabel" runat="server" Text='<%# Bind("StoreStatus") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            采购类型
                        </td>
                        <td>
                            <asp:Label ID="BuyTypeLabel" runat="server" Text='<%# Bind("BuyType") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            附件
                        </td>
                        <td colspan="3">
                            <uc3:attachmentlist ID="Attachmentlist1" runat="server" AttachMentType="YF_AssetManage"
                                MasterCode='<%# Bind("Code") %>'>
                            </uc3:attachmentlist>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="intopic" style="width: 200px;">
                            领用记录
                        </td>
                        <td>
                        <asp:Panel ID="btnDraw" runat="server">
                          <input class="button-small" onclick="OpenMiddleWindow('YF_DrawEdit.aspx?Type=Add&ManageCode=<%#Eval("Code") %>','DrawEdit')"
                                type="button" value="新 增" />
                        </asp:Panel>
                            </td>
                    </tr>
                </table>
                <asp:GridView ID="DrawGridView" runat="server" Width="100%" DataSourceID="DrawObjectDataSource"
                    AutoGenerateColumns="False">
                    <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle CssClass="list-title" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="使用部门">
                            <ItemTemplate>
                                <a href="#" onclick="OpenMiddleWindow('YF_DrawEdit.aspx?Code=<%#Eval("Code") %>&ManageCode=<%=Request["Code"] %>','YF_DrawEdit')">
                                    <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("DrawUnit"))%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="使用人">
                        <ItemTemplate>
                            <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("UserCode")) %>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="领用人">
                            <ItemTemplate>
                                <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("DrawPerson")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DrawDate" HeaderText="领用时间" SortExpression="DrawDate" />
                        <asp:BoundField DataField="BackTime" HeaderText="归还时间" SortExpression="BackTime" />
                    </Columns>
                </asp:GridView>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="intopic" style="width: 200px;">
                            调拨记录
                        </td>
                        <td>
                        <asp:Panel ID="btnTrans" runat="server">
                         <input class="button-small" onclick="OpenMiddleWindow('YF_AssetTransEdit.aspx?Type=Add&ManageCode=<%#Eval("Code") %>','AssetTransferEdit')"
                                type="button" value="新 增" />
                        </asp:Panel>
                            </td>
                    </tr>
                </table>
                <asp:GridView ID="TransGridView" runat="server" Width="100%" DataSourceID="TransObjectDataSource" AutoGenerateColumns="False">
                    <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle CssClass="list-title" HorizontalAlign="Center" />
                    <Columns>
                    <asp:TemplateField HeaderText="申请人">
                    <ItemTemplate>
                        <a href="#" onclick="OpenLargeWindow('YF_AssetTransEdit.aspx?Code=<%#Eval("Code") %>&ManageCode=<%=Request["Code"] %>','YF_AssetTransEdit')">
                            <%#Eval("Applyer") %>
                        </a>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="原所在地">
                    <ItemTemplate>
                    <%#Eval("PreDept") %>
                       <%-- <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("PreDept"))%>--%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="现所在地">
                    <ItemTemplate>
                        <%#Eval("PostDept") %>
                      <%--  <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("PostDept"))%>--%>
                    </ItemTemplate>
                    </asp:TemplateField>
                       <asp:BoundField DataField="TransferTime" HeaderText="转移时间" SortExpression="TransferTime" />
                    </Columns>
                </asp:GridView>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="intopic" style="width: 200px;">
                            维修记录
                        </td>
                        <td>
                        <asp:Panel ID="btnMain" runat="server">
                            <input class="button-small" onclick="OpenMiddleWindow('YF_AssetMainEdit.aspx?Type=Add&ManageCode=<%#Eval("Code") %>','AssetMainEdit')"
                                type="button" value="新 增" />
                        </asp:Panel>
                            <div>
                            </div>
                           </td>
                    </tr>
                </table>
                <asp:GridView ID="MainGridView" runat="server" Width="100%" DataSourceID="MainObjectDataSource" AutoGenerateColumns="False">
                    <HeaderStyle HorizontalAlign="Center" CssClass="list-title" />
                    <RowStyle HorizontalAlign="Center" />
                    <FooterStyle CssClass="list-title" HorizontalAlign="Center" />
                    <Columns>
                    <asp:TemplateField HeaderText="申请人">
                    <ItemTemplate>
                    <a href="#" onclick="OpenLargeWindow('YF_AssetMainRecord.aspx?Code=<%#Eval("Code") %>&ManageCode=<%=Request["Code"] %>','YF_AssetMainRecord')">
                    <%#Eval("UserCode") %>
                    </a>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="申请部门">
                    <ItemTemplate>
                        <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>
                    </ItemTemplate>
                    </asp:TemplateField>
                        <asp:BoundField DataField="ApplyDate" HeaderText="申请时间" SortExpression="ApplyDate" />
                    </Columns>
                </asp:GridView>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="RmsOA.BFL.YF_AssetManageBFL"
            DataObjectTypeName="RmsOA.MODEL.YF_AssetManageModel" DeleteMethod="Delete"
            InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetYF_AssetManageListOne"
            UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="DrawObjectDataSource" runat="server" MaximumRowsParameterName="MaxRecords"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetYF_AssetDrawList"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" TypeName="RmsOA.BFL.YF_AssetDrawBFL">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" />
                <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                <asp:Parameter Name="CodeEqual" Type="Int32" />
                <asp:Parameter Name="ManageCodeEqual" Type="Int32" />
                <asp:Parameter Name="DrawUnitEqual" Type="String" />
                <asp:Parameter Name="DrawDateEqual" Type="DateTime" />
                <asp:Parameter Name="DrawPersonEqual" Type="String" />
                <asp:Parameter Name="UserCodeEqual" Type="String" />
                <asp:Parameter Name="StatusEqual" Type="String" />
                <asp:Parameter Name="UnitEqual" Type="String" />
                <asp:Parameter Name="BackTimeEqual" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="MainObjectDataSource" runat="server" MaximumRowsParameterName="MaxRecords"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetYF_AssetMainApplyList"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" TypeName="RmsOA.BFL.YF_AssetMainApplyBFL">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:Parameter Name="CodeEqual" Type="Int32" />
                <asp:QueryStringParameter Name="ManageCodeEqual" QueryStringField="Code" Type="Int32" />
                <asp:Parameter Name="UserCodeEqual" Type="String" />
                <asp:Parameter Name="DeptEqual" Type="String" />
                <asp:Parameter Name="ApplyDateEqual" Type="String" />
                <asp:Parameter Name="ReasonEqual" Type="String" />
                <asp:Parameter Name="RemarkEqual" Type="String" />
                <asp:Parameter Name="StatusEqual" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="TransObjectDataSource" runat="server" MaximumRowsParameterName="MaxRecords"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetYF_AssetTransferList"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" TypeName="RmsOA.BFL.YF_AssetTransferBFL">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:Parameter Name="CodeEqual" Type="Int32" />
                <asp:QueryStringParameter Name="ManageCodeEqual" QueryStringField="Code" Type="Int32" />
                <asp:Parameter Name="PreDeptEqual" Type="String" />
                <asp:Parameter Name="PostDeptEqual" Type="String" />
                <asp:Parameter Name="TransferTimeEqual" Type="String" />
                <asp:Parameter Name="ApplyerEqual" Type="String" />
                <asp:Parameter Name="StatusEqual" Type="String" />
                <asp:Parameter Name="UnitCodeEqual" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="SortDropDownObjectDataSource" runat="server" TypeName="RmsOA.BFL.YDictionaryExpand" OldValuesParameterFormatString="original_{0}" SelectMethod="AssetSortType"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="TypeDropDownObjectDataSource" runat="server" TypeName="RmsOA.BFL.YDictionaryExpand" OldValuesParameterFormatString="original_{0}" SelectMethod="AssetEquiType"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="StatusDropDownObjectDataSource" runat="server" TypeName="RmsOA.BFL.YDictionaryExpand" OldValuesParameterFormatString="original_{0}" SelectMethod="AssetStoreStatus"></asp:ObjectDataSource>
    </form>



    
</body>
</html>
