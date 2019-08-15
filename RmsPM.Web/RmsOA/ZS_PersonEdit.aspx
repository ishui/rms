<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZS_PersonEdit.aspx.cs" Inherits="PersonalManage_ZS_PersonEdit" %>

<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人事信息维护</title>
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
		
		function AddHomeDtl()
        {
            OpenMiddleWindow('ZS_PersonHomeEdit.aspx?personid=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>','家庭情况', 300, 200);
        }
        
        function AddRewardDtl()
        {
            OpenMiddleWindow('ZS_PersonRewardEdit.aspx?personid=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>','奖惩情况', 300, 200);
        }
      
        function AddStudyDtl()
        {
            OpenMiddleWindow('ZS_PersonStudyEdit.aspx?personid=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>','学习经历', 300, 200);
        }
        
        function AddTrainDtl()
        {
            OpenMiddleWindow('ZS_PersonTrainEdit.aspx?personid=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>','培训经历', 300, 200);
        }
      
        function AddContractDtl()
        {
            OpenMiddleWindow('ZS_PersonContractEdit.aspx?personid=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>','合同信息', 300, 200);
        }
        function AddWorkDtl()
        {
            OpenMiddleWindow('ZS_PersonWorkEdit.aspx?personid=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>','合同信息', 300, 200);
        }
        function AddPolityDtl()
        {
            OpenMiddleWindow('ZS_PersonPolityEdit.aspx?personid=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>','合同信息', 300, 200);
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
                                人事信息维护</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    &nbsp;<asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="Code" OnDataBound="FormView1_DataBound" OnPageIndexChanging="FormView1_PageIndexChanging">
                        <EditItemTemplate>
                            <table id="Table1" class="table" width="100%">
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
                            <table class="form" width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                                <tbody>
                                    <tr>
                                        <td class="form-item">
                                            姓名：</td>
                                        <td>
                                            <asp:TextBox ID="cnameTextBox" runat="server" CssClass="input" Text='<%# Bind("cname") %>' />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cnameTextBox"
                                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        <td class="form-item">
                                            编号：</td>
                                        <td>
                                            <asp:TextBox CssClass="input" ID="workNoTextBox" runat="server" Text='<%# Bind("workNo") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            民族：</td>
                                        <td>
                                            <asp:TextBox ID="folkTextBox" runat="server" CssClass="input" Text='<%# Bind("folk") %>' /></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            身份证号：</td>
                                        <td>
                                            <asp:TextBox ID="IDcardTextBox" runat="server" CssClass="input" Text='<%# Bind("IDcard") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="IDcardTextBox"
                                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        <td class="form-item">
                                            政治面貌：</td>
                                        <td>
                                            <asp:TextBox CssClass="input" ID="polityTextBox" runat="server" Text='<%# Bind("polity") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            入党团时间：</td>
                                        <td>
                                            <cc3:Calendar CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ID="Calendar6" ReadOnly="False" runat="server" Value='<%# Bind("rdt_date") %>'>
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            贯籍：</td>
                                        <td>
                                            <asp:TextBox CssClass="input" ID="nativeplaceTextBox" runat="server" Text='<%# Bind("nativeplace") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            出生地：</td>
                                        <td>
                                            <asp:TextBox ID="homeplaceTextBox" CssClass="input" runat="server" Text='<%# Bind("homeplace") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            出生日期：</td>
                                        <td>
                                            <cc3:Calendar CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ID="Calendar7" ReadOnly="False" runat="server" Value='<%# Bind("birthday") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            身高：</td>
                                        <td>
                                            <asp:TextBox ID="statureTextBox" runat="server" CssClass="input" Text='<%# Bind("stature") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            血型：</td>
                                        <td>
                                            <asp:TextBox ID="bloodgroupTextBox" runat="server" CssClass="input" Text='<%# Bind("bloodgroup") %>'></asp:TextBox></td>
                                           <td class="form-item">
                                            <span style="display:none;">体重</span></td>
                                            <td></td>
                                        <%--<td style="display:none;">
                                            <asp:TextBox CssClass="input" ID="avoirdupoisTextBox" runat="server" Text='<%# Bind("avoirdupois") %>'></asp:TextBox>
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            学历：</td>
                                        <td>
                                            <asp:TextBox ID="educationTextBox" runat="server" CssClass="input" Text='<%# Bind("education") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            学位：</td>
                                        <td>
                                            <asp:TextBox CssClass="input" ID="degreeTextBox" runat="server" Text='<%# Bind("degree") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            职称：</td>
                                        <td>
                                            <asp:TextBox CssClass="input" ID="zcTextBox" runat="server" Text='<%# Bind("zc") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        
                                        <td class="form-item">
                                            性别：</td>
                                        <td>
                                        <asp:DropDownList runat="server" SelectedValue='<%# Bind("sex") %>' ID="SexDropDownList" Font-Size="9pt">
                                        <asp:ListItem Text=""></asp:ListItem>
                                        <asp:ListItem Selected="True" Text="男"></asp:ListItem>
                                        <asp:ListItem Text="女" ></asp:ListItem>      
                                        </asp:DropDownList>
                                           </td>
                                        <td class="form-item">
                                            职务：</td>
                                        <td>
                                            <asp:TextBox CssClass="input" ID="dutyTextBox" runat="server" Text='<%# Bind("duty") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            家庭住址：</td>
                                        <td>
                                            <asp:TextBox ID="addressTextBox" runat="server" CssClass="input" Text='<%# Bind("address") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            参加工作时间：</td>
                                        <td>
                                            <cc3:Calendar ID="Calendar8" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("cjgz_date") %>'>
                                            </cc3:Calendar>
                                        </td>
                                        <td class="form-item">
                                            入公司时间：</td>
                                        <td>
                                            <cc3:Calendar ID="Calendar9" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("rgs_date") %>'>
                                            </cc3:Calendar>
                                        </td>
                                        <td class="form-item">
                                            户口地址：</td>
                                        <td>
                                            <asp:TextBox ID="fkdzTextBox" runat="server" CssClass="input" Text='<%# Bind("fkdz") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            联系电话：</td>
                                        <td>
                                            <asp:TextBox CssClass="input" ID="phoneTextBox" runat="server" Text='<%# Bind("phone") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            手机：</td>
                                        <td>
                                            <asp:TextBox CssClass="input" ID="mobileTextBox" runat="server" Text='<%# Bind("mobile") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="mobileTextBox"
                                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        <td class="form-item">
                                            邮编：</td>
                                        <td>
                                            <asp:TextBox ID="postcodeTextBox" runat="server" CssClass="input" Text='<%# Bind("postcode") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            Email：</td>
                                        <td>
                                            <asp:TextBox ID="emailTextBox" runat="server" CssClass="input" Text='<%# Bind("email") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            婚姻状况：</td>
                                        <td>
                                            <asp:TextBox ID="ismarryTextBox" runat="server" CssClass="input" Text='<%# Bind("ismarry") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            合同到期日：</td>
                                        <td>
                                            <cc3:Calendar ID="Calendar10" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("htc_date") %>'>
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                     
                                        <td class="form-item">
                                            工作部门：</td>
                                        <td>
                                            <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                                height: 18px" type="hidden" value='<%# Bind("yard") %>' /><input id="txtUnitName"
                                                    runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                    type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                        style="cursor: hand" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtUnitName"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="form-item">
                                            直属领导：</td>
                                        <td>
                                            <uc1:InputUser ID="InPersonBox" runat="server" Value='<%# Bind("leader") %>'></uc1:InputUser>
                                        </td>
                                          <%-- <td class="form-item">
                                            工作证号：</td>
                                        <td>
                                            <asp:TextBox ID="jobnoTextBox" runat="server" CssClass="input" Text='<%# Bind("jobno") %>'></asp:TextBox></td>--%>
                                               <td class="form-item">
                                            </td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                              健康状况：</td>
                                        <td>
                                            <asp:TextBox ID="HealthStatusTextBox" runat="server" CssClass="input" Text='<%# Bind("HealthStatus") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                              专业：</td>
                                        <td>
                                            <asp:TextBox ID="SpecificTextBox" runat="server" CssClass="input" Text='<%# Bind("Specific") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="SpecificTextBox"
                                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                       　<td class="form-item">
                                              资格：</td>
                                        <td>
                                            <asp:TextBox ID="SeniorityTextBox" runat="server" CssClass="input" Text='<%# Bind("Seniority") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" >
                                              业务专长：</td>
                                        <td colspan= "5">
                                            <asp:TextBox ID="WorkKillsTextBox" runat="server" CssClass="input" Text='<%# Bind("WorkKills") %>' Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" >
                                              部门意见：</td>
                                        <td colspan= "5">
                                            <asp:TextBox ID="DepartmentOpitionTextBox" runat="server" CssClass="input" Text='<%# Bind("DepartmentOpinion") %>' Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" >
                                              单位意见：</td>
                                        <td colspan= "5">
                                            <asp:TextBox ID="UnitOpitionTextBox" runat="server" CssClass="input" Text='<%# Bind("UnitOpinion") %>' Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                    </tr>
                                </tbody>
                            </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table id="Table4" class="table" width="100%">
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
                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tbody>
                                    <tr>
                                        <td class="form-item">
                                            姓名：</td>
                                        <td>
                                            <asp:TextBox ID="cnameTextBox" runat="server" CssClass="input" Text='<%# Bind("cname") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cnameTextBox"
                                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        <td class="form-item">
                                            编号：</td>
                                        <td>
                                            <asp:TextBox ID="workNoTextBox" runat="server" CssClass="input" Text='<%# Bind("workNo") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            民族：</td>
                                        <td>
                                            <asp:TextBox ID="folkTextBox" runat="server" CssClass="input" Text='<%# Bind("folk") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            身份证号：</td>
                                        <td>
                                            <asp:TextBox ID="IDcardTextBox" runat="server" CssClass="input" Text='<%# Bind("IDcard") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="IDcardTextBox"
                                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        <td class="form-item">
                                            政治面貌：</td>
                                        <td>
                                            <asp:TextBox ID="polityTextBox" runat="server" CssClass="input" Text='<%# Bind("polity") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            入党团时间：</td>
                                        <td>
                                            <cc3:Calendar ID="Calendar1" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("rdt_date") %>'>
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            贯籍：</td>
                                        <td>
                                            <asp:TextBox ID="nativeplaceTextBox" runat="server" CssClass="input" Text='<%# Bind("nativeplace") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            出生地：</td>
                                        <td>
                                            <asp:TextBox ID="homeplaceTextBox" runat="server" CssClass="input" Text='<%# Bind("homeplace") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            出生日期：</td>
                                        <td>
                                            <cc3:Calendar ID="Calendar2" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("birthday") %>'>
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            身高：</td>
                                        <td>
                                            <asp:TextBox ID="statureTextBox" runat="server" CssClass="input" Text='<%# Bind("stature") %>'></asp:TextBox></td>
<%--                                        <td class="form-item">
                                            体重：</td>
                                        <td>
                                            <asp:TextBox ID="avoirdupoisTextBox" runat="server" CssClass="input" Text='<%# Bind("avoirdupois") %>'></asp:TextBox>
                                        </td>--%>
                                        <td class="form-item">
                                            血型：</td>
                                        <td>
                                            <asp:TextBox ID="bloodgroupTextBox" runat="server" CssClass="input" Text='<%# Bind("bloodgroup") %>'></asp:TextBox></td>
                                            <td class="form-item">
                                            </td>
                                            <td></td>
                                        <td style="display:none;">
                                            <asp:TextBox CssClass="input" ID="avoirdupoisTextBox" runat="server" Text='<%# Bind("avoirdupois") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            学历：</td>
                                        <td>
                                            <asp:TextBox ID="educationTextBox" runat="server" CssClass="input" Text='<%# Bind("education") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            学位：</td>
                                        <td>
                                            <asp:TextBox ID="degreeTextBox" runat="server" CssClass="input" Text='<%# Bind("degree") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            职称：</td>
                                        <td>
                                            <asp:TextBox ID="zcTextBox" runat="server" CssClass="input" Text='<%# Bind("zc") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            性别：</td>
                                        <td>
                                        <asp:DropDownList runat="server" SelectedValue='<%# Bind("sex") %>' ID="SexDropDownList" Font-Size="9pt">
                                        <asp:ListItem Text=""></asp:ListItem>
                                        <asp:ListItem Selected="True" Text="男"></asp:ListItem>
                                        <asp:ListItem Text="女" ></asp:ListItem>      
                                        </asp:DropDownList></td>
                                        <td class="form-item">
                                            职务 ：</td>
                                        <td>
                                            <asp:TextBox ID="dutyTextBox" runat="server" CssClass="input" Text='<%# Bind("duty") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            家庭住址：</td>
                                        <td>
                                            <asp:TextBox ID="addressTextBox" runat="server" CssClass="input" Text='<%# Bind("address") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            参加工作时间：</td>
                                        <td>
                                            <cc3:Calendar ID="Calendar3" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("cjgz_date") %>'>
                                            </cc3:Calendar>
                                        </td>
                                        <td class="form-item">
                                            入公司时间：</td>
                                        <td>
                                            <cc3:Calendar ID="Calendar4" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("rgs_date") %>'>
                                            </cc3:Calendar>
                                        </td>
                                        <td class="form-item">
                                            户口地址：</td>
                                        <td>
                                            <asp:TextBox ID="fkdzTextBox" runat="server" CssClass="input" Text='<%# Bind("fkdz") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            联系电话：</td>
                                        <td>
                                            <asp:TextBox ID="phoneTextBox" runat="server" CssClass="input" Text='<%# Bind("phone") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            手机：</td>
                                        <td>
                                            <asp:TextBox ID="mobileTextBox" runat="server" CssClass="input" Text='<%# Bind("mobile") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="mobileTextBox"
                                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        <td class="form-item">
                                            邮编：</td>
                                        <td>
                                            <asp:TextBox ID="postcodeTextBox" runat="server" CssClass="input" Text='<%# Bind("postcode") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            Email：</td>
                                        <td>
                                            <asp:TextBox ID="emailTextBox" runat="server" CssClass="input" Text='<%# Bind("email") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            婚姻状况：</td>
                                        <td>
                                            <asp:TextBox ID="ismarryTextBox" runat="server" CssClass="input" Text='<%# Bind("ismarry") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                            合同到期日：</td>
                                        <td>
                                            <cc3:Calendar ID="Calendar5" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("htc_date") %>'>
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--<td class="form-item">
                                            工作证号：</td>
                                        <td>
                                            <asp:TextBox ID="jobnoTextBox" runat="server" CssClass="input" Text='<%# Bind("jobno") %>'></asp:TextBox></td>--%>
                                        <td class="form-item">
                                            工作部门：</td>
                                        <td>
                                            <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                                height: 18px" type="hidden" value='<%# Bind("yard") %>' /><input id="txtUnitName"
                                                    runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                    type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                        style="cursor: hand" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnitName"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        
                                        <td class="form-item">
                                            直属领导：</td>
                                        <td>
                                            <uc1:InputUser ID="InPersonBox" runat="server" Value='<%# Bind("Leader") %>'></uc1:InputUser>
                                        </td>
                                    <%--        <td class="form-item">
                                            工作证号：</td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="input" Text='<%# Bind("jobno") %>'></asp:TextBox></td>
                                        <td class="form-item">--%>
                                            <td class="form-item">
                                            </td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                              健康状况：</td>
                                        <td>
                                            <asp:TextBox ID="HealthStatusTextBox" runat="server" CssClass="input" Text='<%# Bind("HealthStatus") %>'></asp:TextBox></td>
                                        <td class="form-item">
                                              专业：</td>
                                        <td>
                                            <asp:TextBox ID="SpecificTextBox" runat="server" CssClass="input" Text='<%# Bind("Specific") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="SpecificTextBox"
                                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                       　<td class="form-item">
                                              资格：</td>
                                        <td>
                                            <asp:TextBox ID="SeniorityTextBox" runat="server" CssClass="input" Text='<%# Bind("Seniority") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" >
                                              业务专长：</td>
                                        <td colspan= "5">
                                            <asp:TextBox ID="WorkKillsTextBox" runat="server" CssClass="input" Text='<%# Bind("WorkKills") %>' Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" >
                                              部门意见：</td>
                                        <td colspan= "5">
                                            <asp:TextBox ID="DepartmentOpitionTextBox" runat="server" CssClass="input" Text='<%# Bind("DepartmentOpinion") %>' Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" >
                                              单位意见：</td>
                                        <td colspan= "5">
                                            <asp:TextBox ID="UnitOpitionTextBox" runat="server" CssClass="input" Text='<%# Bind("UnitOpinion") %>' Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                    </tr>
                                </tbody>
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
                                        &nbsp;
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tbody>
                                    <tr>
                                        <td class="form-item">
                                            姓名：</td>
                                        <td>
                                            <asp:Label ID="cnameLabel" runat="server" Text='<%# Bind("cname") %>'></asp:Label></td>
                                        <td class="form-item">
                                            编号：</td>
                                        <td>
                                            <asp:Label ID="workNoLabel" runat="server" Text='<%# Bind("workNo") %>'></asp:Label></td>
                                        <td class="form-item">
                                            民族：</td>
                                        <td>
                                            <asp:Label ID="folkLabel" runat="server" Text='<%# Bind("folk") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            身份证号：</td>
                                        <td>
                                            <asp:Label ID="IDcardLabel" runat="server" Text='<%# Bind("IDcard") %>'></asp:Label></td>
                                        <td class="form-item">
                                            政治面貌：</td>
                                        <td>
                                            <asp:Label ID="polityLabel" runat="server" Text='<%# Bind("polity") %>'></asp:Label></td>
                                        <td class="form-item">
                                            入党团时间：</td>
                                        <td>
                                            <asp:Label ID="rdt_dateLabel" runat="server" Text='<%# Bind("rdt_date") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            贯籍：</td>
                                        <td>
                                            <asp:Label ID="nativeplaceLabel" runat="server" Text='<%# Bind("nativeplace") %>'></asp:Label></td>
                                        <td class="form-item">
                                            出生地：</td>
                                        <td>
                                            <asp:Label ID="homeplaceLabel" runat="server" Text='<%# Bind("homeplace") %>'></asp:Label></td>
                                        <td class="form-item">
                                            出生日期：</td>
                                        <td>
                                            <asp:Label ID="birthdayLabel" runat="server" Text='<%# Bind("birthday") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            身高：</td>
                                        <td>
                                            <asp:Label ID="statureLabel" runat="server" Text='<%# Bind("stature") %>'></asp:Label></td>
                                        <%--<td class="form-item">
                                            体重：</td>
                                        <td>
                                            <asp:Label ID="avoirdupoisLabel" runat="server" Text='<%# Bind("avoirdupois") %>'></asp:Label></td>--%>
                                        <td class="form-item">
                                            血型：</td>
                                        <td>
                                            <asp:Label ID="bloodgroupLabel" runat="server" Text='<%# Bind("bloodgroup") %>'></asp:Label></td>
                                                                                       <td class="form-item">
                                            <span style="display:none;">体重</span></td>
                                      <td>
                                            <%--<asp:TextBox CssClass="input" ID="avoirdupoisTextBox" runat="server" Text='<%# Bind("avoirdupois") %>'></asp:TextBox>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            学历：</td>
                                        <td>
                                            <asp:Label ID="educationLabel" runat="server" Text='<%# Bind("education") %>'></asp:Label></td>
                                        <td class="form-item">
                                            学位：</td>
                                        <td>
                                            <asp:Label ID="degreeLabel" runat="server" Text='<%# Bind("degree") %>'></asp:Label></td>
                                        <td class="form-item">
                                            职称：</td>
                                        <td>
                                            <asp:Label ID="zcLabel" runat="server" Text='<%# Bind("zc") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            性别：</td>
                                        <td>
                                            <asp:Label ID="sexLabel" runat="server" Text='<%# Bind("sex") %>'></asp:Label></td>
                                        <td class="form-item">
                                            职务：</td>
                                        <td>
                                            <asp:Label ID="dutyLabel" runat="server" Text='<%# Bind("duty") %>'></asp:Label></td>
                                        <td class="form-item">
                                            家庭住址：</td>
                                        <td>
                                            <asp:Label ID="addressLabel" runat="server" Text='<%# Bind("address") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            参加工作时间：</td>
                                        <td>
                                            <asp:Label ID="cjgz_dateLabel" runat="server" Text='<%# Bind("cjgz_date") %>'></asp:Label></td>
                                        <td class="form-item">
                                            入公司时间：</td>
                                        <td>
                                            <asp:Label ID="rgs_dateLabel" runat="server" Text='<%# Bind("rgs_date") %>'></asp:Label></td>
                                        <td class="form-item">
                                            户口地址：</td>
                                        <td>
                                            <asp:Label ID="fkdzLabel" runat="server" Text='<%# Bind("fkdz") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            联系电话：</td>
                                        <td>
                                            <asp:Label ID="phoneLabel" runat="server" Text='<%# Bind("phone") %>'></asp:Label></td>
                                        <td class="form-item">
                                            手机：</td>
                                        <td>
                                            <asp:Label ID="mobileLabel" runat="server" Text='<%# Bind("mobile") %>'></asp:Label></td>
                                        <td class="form-item">
                                            邮编：</td>
                                        <td>
                                            <asp:Label ID="postcodeLabel" runat="server" Text='<%# Bind("postcode") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            Email：</td>
                                        <td>
                                            <asp:Label ID="emailLabel" runat="server" Text='<%# Bind("email") %>'></asp:Label></td>
                                        <td class="form-item">
                                            婚姻状况：</td>
                                        <td>
                                            <asp:Label ID="ismarryLabel" runat="server" Text='<%# Bind("ismarry") %>'></asp:Label></td>
                                        <td class="form-item">
                                            合同到期日：</td>
                                        <td>
                                            <asp:Label ID="htc_dateLabel" runat="server" Text='<%# Bind("htc_date") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        
                                        
                                        <td class="form-item">
                                            工作部门：</td>
                                        <td>
                                            <asp:Label ID="yardLabel" runat="server" Text='<%# Bind("yard") %>'></asp:Label></td>
                                        <td class="form-item">
                                            直属领导：</td>
                                        <td>
                                            <asp:Label ID="LeaderLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("Leader")) %>'></asp:Label></td>
                                            <%--<td class="form-item">
                                            工作证号：
                                            </td>--%>
                                        <%--<td>
                                            <asp:Label ID="jobnoLabel" runat="server" Text='<%# Bind("jobno") %>'></asp:Label></td>--%>
                                            <td class="form-item">
                                            </td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            健康状况：</td>
                                        <td>
                                            <asp:Label ID="HealthStatusLabel" runat="server" Text='<%# Bind("HealthStatus") %>'></asp:Label></td>
                                            <td class="form-item">
                                            专业：</td>
                                        <td>
                                            <asp:Label ID="SpecificLabel" runat="server" Text='<%# Bind("Specific") %>'></asp:Label></td>
                                            <td class="form-item">
                                            资格：</td>
                                        <td>
                                            <asp:Label ID="SeniorityLabel" runat="server" Text='<%# Bind("Seniority") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" >
                                              业务专长：</td>
                                        <td colspan= "5">
                                            <asp:Label ID="WorkKillsLabel" runat="server" Text='<%# Bind("WorkKills") %>'></asp:Label></td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="form-item" >
                                              部门意见：</td>
                                        <td colspan= "5">
                                            <asp:Label ID="DepartmentOpitionLabel" runat="server" Text='<%# Bind("DepartmentOpinion") %>'></asp:Label></td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="form-item" >
                                              单位意见：</td>
                                        <td colspan= "5">
                                            <asp:Label ID="UnitOpitionLabel" runat="server" Text='<%# Bind("UnitOpinion") %>'></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        学习经历</td>
                                    <td>
                                        <asp:Button ID="btnAddStudy" runat="server" CssClass="button-small" Text="新 增" /></td>
                                </tr>
                            </table>
                            <asp:GridView ID="GridViewStudy" runat="server" AutoGenerateColumns="False" CssClass="List"
                                DataSourceID="ObjectDataSource2" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="BEGIN_DATE" HeaderText="开始时间" SortExpression="BEGIN_DATE" />
                                    <asp:BoundField DataField="END_DATE" HeaderText="结束时间" SortExpression="END_DATE" />
                                    <asp:BoundField DataField="SCHOOL_NAME" HeaderText="学校名称" SortExpression="SCHOOL_NAME" />
                                    <asp:TemplateField HeaderText="专业">
                                        <ItemTemplate>
                                            <a href="#" onclick="javascript:OpenMiddleWindow('ZS_PersonStudyEdit.aspx?Code=<%# Eval("Code")%>','StudyDetail');return false;">
                                                <%# Eval("SPECIALITY")%>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DEGREE" HeaderText="学位" SortExpression="DEGREE" />
                                    <asp:BoundField DataField="LETTER_NAME" HeaderText="获得证书" SortExpression="LETTER_NAME" />
                                </Columns>
                                <EmptyDataTemplate>
                                    无学习经历记录

                                </EmptyDataTemplate>
                                <PagerStyle CssClass="list-title" />
                                <HeaderStyle CssClass="list-title" />
                            </asp:GridView>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        工作经历</td>
                                    <td>
                                        <asp:Button ID="BtnAddWork" runat="server" CssClass="button-small" Text="新 增" /></td>
                                </tr>
                            </table>
                            <asp:GridView ID="GridViewWork" runat="server" AutoGenerateColumns="False" CssClass="List"
                                DataSourceID="ObjectDataSource7" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="BeginDate" HeaderText="开始时间" SortExpression="BeginDate" />
                                    <asp:BoundField DataField="EndDate" HeaderText="结束时间" SortExpression="EndDate" />
                                    <asp:BoundField DataField="WorkUnit" HeaderText="就职单位" SortExpression="WorkUnit" />
                                    <asp:TemplateField HeaderText="职务">
                                        <ItemTemplate>
                                            <a href="#" onclick="javascript:OpenMiddleWindow('ZS_PersonWorkEdit.aspx?Code=<%# Eval("Code")%>','WorkDetail');return false;">
                                                <%# Eval("JobPost")%>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Duty" HeaderText="职责" SortExpression="Duty" />
                                    <asp:BoundField DataField="Certifier" HeaderText="证明人" SortExpression="Certifier" />
                                </Columns>
                                <EmptyDataTemplate>
                                    无工作经历记录

                                </EmptyDataTemplate>
                                <PagerStyle CssClass="list-title" />
                                <HeaderStyle CssClass="list-title" />
                            </asp:GridView>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        政治经历</td>
                                    <td>
                                        <asp:Button ID="BtnAddPolity" runat="server" CssClass="button-small" Text="新 增" /></td>
                                </tr>
                            </table>
                            <asp:GridView ID="GridViewPolity" runat="server" AutoGenerateColumns="False" CssClass="List"
                                DataSourceID="ObjectDataSource8" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="BeginDate" HeaderText="开始时间" SortExpression="BeginDate" />
                                    <asp:BoundField DataField="EndDate" HeaderText="结束时间" SortExpression="EndDate" />
                                    <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" />
                                    <asp:TemplateField HeaderText="职务">
                                        <ItemTemplate>
                                            <a href="#" onclick="javascript:OpenMiddleWindow('ZS_PersonPolityEdit.aspx?Code=<%# Eval("Code")%>','PolityDetail');return false;">
                                                <%# Eval("Name")%>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Duty" HeaderText="职责" SortExpression="Duty" />
                                    <asp:BoundField DataField="Certifier" HeaderText="证明人" SortExpression="Certifier" />
                                </Columns>
                                <EmptyDataTemplate>
                                    无政治经历记录

                                </EmptyDataTemplate>
                                <PagerStyle CssClass="list-title" />
                                <HeaderStyle CssClass="list-title" />
                            </asp:GridView>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        家庭信息</td>
                                    <td>
                                        <asp:Button ID="btnAddHome" runat="server" CssClass="button-small" Text="新 增" /></td>
                                </tr>
                            </table>
                            <asp:GridView ID="GridViewHome" runat="server" AutoGenerateColumns="False" CssClass="List"
                                DataSourceID="ObjectDataSource5" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="appname" HeaderText="称谓" SortExpression="appname" />
                                    <asp:TemplateField HeaderText="姓名">
                                        <ItemTemplate>
                                            <a href="#" onclick="javascript:OpenMiddleWindow('ZS_PersonHomeEdit.aspx?Code=<%# Eval("Code")%>','HomeDetail');return false;">
                                                <%# Eval("cname")%>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="idcard" HeaderText="身份证号" SortExpression="idcard" />
                                    <asp:BoundField DataField="polity" HeaderText="政治面貌" SortExpression="polity" />
                                    <asp:BoundField DataField="workplace" HeaderText="工作单位" SortExpression="workplace" />
                                    <asp:BoundField DataField="duty" HeaderText="职务" SortExpression="duty" />
                                    <asp:BoundField DataField="phone" HeaderText="联系电话" SortExpression="phone" />
                                </Columns>
                                <EmptyDataTemplate>
                                    无家庭信息记录

                                </EmptyDataTemplate>
                                <PagerStyle CssClass="list-title" />
                                <HeaderStyle CssClass="list-title" />
                            </asp:GridView>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        奖惩情况</td>
                                    <td>
                                        <asp:Button ID="btnAddReward" runat="server" CssClass="button-small" Text="新 增" /></td>
                                </tr>
                            </table>
                            <asp:GridView ID="GridViewReward" runat="server" AutoGenerateColumns="False" CssClass="List"
                                DataSourceID="ObjectDataSource3" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="dj_date" HeaderText="日期" SortExpression="dj_date" />
                                    <asp:TemplateField HeaderText="内容">
                                        <ItemTemplate>
                                            <a href="#" onclick="javascript:OpenMiddleWindow('ZS_PersonRewardEdit.aspx?Code=<%# Eval("Code")%>','RewardDetail');return false;">
                                                <%# Eval("content")%>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="cause" HeaderText="原因" SortExpression="cause" />
                                    <asp:BoundField DataField="remark" HeaderText="备注" SortExpression="remark" />
                                </Columns>
                                <EmptyDataTemplate>
                                    无奖惩情况记录

                                </EmptyDataTemplate>
                                <PagerStyle CssClass="list-title" />
                                <HeaderStyle CssClass="list-title" />
                            </asp:GridView>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        培训情况</td>
                                    <td>
                                        <asp:Button ID="btnAddTrain" runat="server" CssClass="button-small" Text="新 增" /></td>
                                </tr>
                            </table>
                            <asp:GridView ID="GridViewTrain" runat="server" AutoGenerateColumns="False" CssClass="List"
                                DataSourceID="ObjectDataSource4" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="BEGIN_DATE" HeaderText="开始时间" SortExpression="BEGIN_DATE" />
                                    <asp:BoundField DataField="END_DATE" HeaderText="结束时间" SortExpression="END_DATE" />
                                    <asp:BoundField DataField="TRAIN_HOUR" HeaderText="培训课时" SortExpression="TRAIN_HOUR" />
                                    <asp:TemplateField HeaderText="内容">
                                        <ItemTemplate>
                                            <a href="#" onclick="javascript:OpenMiddleWindow('ZS_PersonTrainEdit.aspx?Code=<%# Eval("Code")%>','TrainDetail');return false;">
                                                <%# Eval("TRAIN_CONTENT")%>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TRAIN_METHOD" HeaderText="培训方式" SortExpression="TRAIN_METHOD" />
                                </Columns>
                                <EmptyDataTemplate>
                                    无培训情况记录

                                </EmptyDataTemplate>
                                <PagerStyle CssClass="list-title" />
                                <HeaderStyle CssClass="list-title" />
                            </asp:GridView>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        合同信息</td>
                                    <td>
                                        <asp:Button ID="btnAddContract" runat="server" CssClass="button-small" Text="新 增" /></td>
                                </tr>
                            </table>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="List"
                                DataSourceID="ObjectDataSource6" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="合同编号">
                                        <ItemTemplate>
                                            <a href="#" onclick="javascript:OpenMiddleWindow('ZS_PersonContractEdit.aspx?Code=<%# Eval("Code")%>','ContractDetail');return false;">
                                                <%# Eval("ContractCode")%>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RegisterDate" HeaderText="登记日期" SortExpression="RegisterDate" />
                                    <asp:BoundField DataField="StartDate" HeaderText="合同开始日期" SortExpression="StartDate" />
                                    <asp:BoundField DataField="EndDate" HeaderText="合同到期日期" SortExpression="EndDate" />
                                    <asp:BoundField DataField="StationCode" HeaderText="岗位" SortExpression="StationCode" />
                                    <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" />
                                </Columns>
                                <EmptyDataTemplate>
                                    无合同信息记录

                                </EmptyDataTemplate>
                                <PagerStyle CssClass="list-title" />
                                <HeaderStyle CssClass="list-title" />
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.OAPersonModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetOAPersonListOne"
                        TypeName="RmsOA.BFL.OAPersonBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetOAPersonStudyList"
            TypeName="RmsOA.BFL.OAPersonStudyBFL" MaximumRowsParameterName="MaxRecords"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" DefaultValue="" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:QueryStringParameter Name="personidEqual" QueryStringField="Code" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetOAPersonRewardList"
            TypeName="RmsOA.BFL.OAPersonRewardBFL" MaximumRowsParameterName="MaxRecords"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" DefaultValue="" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:QueryStringParameter Name="personidEqual" QueryStringField="Code" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="GetOAPersonTrainList"
            TypeName="RmsOA.BFL.OAPersonTrainBFL" MaximumRowsParameterName="MaxRecords"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" DefaultValue="" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:QueryStringParameter Name="personidEqual" QueryStringField="Code" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" SelectMethod="GetOAPersonHomeList"
            TypeName="RmsOA.BFL.OAPersonHomeBFL" MaximumRowsParameterName="MaxRecords"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" DefaultValue="" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:QueryStringParameter Name="personidEqual" QueryStringField="Code" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" SelectMethod="GetOAPersonContractList"
            TypeName="RmsOA.BFL.OAPersonContractBFL" MaximumRowsParameterName="MaxRecords"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" DefaultValue="" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:QueryStringParameter Name="personidEqual" QueryStringField="Code" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource7" runat="server" SelectMethod="GetGK_OAPersonWorkList"
            TypeName="RmsOA.BFL.GK_OAPersonWorkBFL" MaximumRowsParameterName="MaxRecords"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" DefaultValue="" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:QueryStringParameter Name="personidEqual" QueryStringField="Code" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" SelectMethod="GetGK_OAPersonPolityList"
            TypeName="RmsOA.BFL.GK_OAPersonPolityBFL" MaximumRowsParameterName="MaxRecords"
            SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" DefaultValue="" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:QueryStringParameter Name="personidEqual" QueryStringField="Code" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
