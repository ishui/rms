<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sm_SupplierGrade.ascx.cs" Inherits="WorkFlowOperation_sm_SupplierGrade" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="form">
<div id="OperableDiv" runat="server">

                        <tr>
                            <td colspan="2">
                                <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <div id="messageMoid" runat="server">
                                                <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="height: 19px" >
                                                            &nbsp;项目名称：</td>
                                                        <td style="height: 19px">
                                                            <asp:Label ID="lblProjectNameModi" runat="server" ></asp:Label>
                                                            <asp:dropdownlist id="DropDownProject" Visible="false" AutoPostBack="True" runat="server" Width="88px" onselectedindexchanged="DropDownProject_SelectedIndexChanged"></asp:dropdownlist>
                                                        </td>
                                                        <td>
                                                            承包商名称：</td>
                                                        <td>
                                                            <input class="input" readonly id="txtSupplierName" type="text" name="txtSupplierName" runat="server">
                                                            <input id="txtSupplierCode" type="hidden" name="txtSupplierCode" runat="server"><font
                                                                color="#ff0000">*</font> <a runat=server id="supplierChange" onclick="openSelectSupplier();return false;" href="##">
                                                                    <img src="../images/ToolsItemSearch.gif" border="0"></a>
                                                        </td>
                                                        <td style="height: 19px" >
                                                            承包商项目经理：&nbsp;</td>
                                                        <td style="height: 19px">
                                                            &nbsp;<input id="SupplierManagerModi" type="text" runat="server" class="input" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                &nbsp;</td>
                            <td>
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#000000">
                                    <tr>
                                        <td align="center">
                                            恶劣<br>
                                            （1~2分）</td>
                                        <td align="center">
                                            待改善<br>
                                            （3~4分）</td>
                                        <td align="center">
                                            尚可<br>
                                            （5~6分）</td>
                                        <td align="center">
                                            满意<br>
                                            （7~8分）</td>
                                        <td align="center">
                                            卓越<br>
                                            （9~10分）</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ● 远远达不到预期<br>
                                            &nbsp;&nbsp;&nbsp;表现及合同要求<br>
                                            ● 明显无力改善</td>
                                        <td>
                                            ● 达不到预期表现，<br>
                                            &nbsp;&nbsp;&nbsp;并需在督促协助下<br>
                                            &nbsp;&nbsp;&nbsp;才能满足合同要求<br>
                                            ● 改善态度消极
                                        </td>
                                        <td >
                                            ● 能达到行业及<br>
                                            &nbsp;&nbsp;&nbsp;合同基本要求<br>
                                            ● 较积极地回应<br>
                                            &nbsp;&nbsp;&nbsp;改善的要求
                                        </td>
                                        <td>
                                            ● 达到预期表现及<br>
                                            &nbsp;&nbsp;&nbsp;合同要求<br>
                                            ● 主动寻求改善
                                        </td>
                                        <td>
                                            ● 表现超越预期<br>
                                            ● 积极改善，<br>
                                            &nbsp;&nbsp;&nbsp;精益求精
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#000000">
                                    <tr>
                                        <td colspan="3">
                                            考虑因素</td>
                                        <td>
                                            评分指标</td>
                                        <td>
                                            权重</td>
                                        <td>
                                            集团合约部</td>
                                        <td>
                                            集团技术部</td>
                                        <td>
                                            项目总监</td>
                                        <td>
                                            项目合约部</td>
                                        <td>
                                            项目工程部</td>
                                        <td>
                                            项目设计部</td>
                                        <td>
                                            客服部</td>
                                    </tr>
                                    <tr>
                                        <div id="GradeModify" runat="server">
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <ItemTemplate>                                                
                                                    <asp:Label ID="LabelCode" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "tempConsiderDiathesisCode")%>'></asp:Label>
                                                    <asp:Label ID="LabelFlag" runat="server" Visible="False" Text='<%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "1":"0"%>'></asp:Label>
                                                    <asp:Label ID="Labelsubtotal" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "issubtotal")%>'></asp:Label>
                                                    <asp:Label ID="lblAgreement1" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "AgreementIsusing")%>'></asp:Label>
                                                    <asp:Label ID="lblTechnic1" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "TechnicIsusing")%>'></asp:Label>
                                                    <asp:Label ID="lblItemMajordomo1" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemMajordomoIsusing")%>'></asp:Label>
                                                    <asp:Label ID="lblItemAgreement1" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemAgreementIsusing")%>'></asp:Label>
                                                    <asp:Label ID="lblItemEngineering1" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemEngineeringIsusing")%>'></asp:Label>
                                                    <asp:Label ID="lblItemDesign1" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemDesignIsusing")%>'></asp:Label>
                                                    <asp:Label ID="lblClientService1" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ClientServiceIsusing")%>'></asp:Label>
                                                    
                                                    <td rowspan="<%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") == "1" ) ? "2" : (string)DataBinder.Eval(Container.DataItem, "ChildCount") %>" colspan="<%# ((string)DataBinder.Eval(Container.DataItem, "deep") == "2" && ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" ))?"2":"0" %>">
                                                        <%# (string)DataBinder.Eval(Container.DataItem, "ConsiderDiathesis")%>
                                                        &nbsp;</td>
                                                    <%#  ((string)DataBinder.Eval(Container.DataItem, "issubtotal") == "1") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "GradeGuideline") + "&nbsp;</td>" : ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0") ? "<td>" + (string)DataBinder.Eval(Container.DataItem, "GradeGuideline") + "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td rowspan=\"2\" NOWRAP>" : ""%>
                                                    <asp:TextBox style="TEXT-ALIGN: right" Enabled='<%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>'  Width="35" ID="TxtPercentage" runat="server" Text='<%# (string)DataBinder.Eval(Container.DataItem, "tempPercentage")%>'
                                                        Visible=' <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                                                   
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "%&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td rowspan=\"2\" NOWRAP>" : ""%>
                                                    <asp:TextBox style="TEXT-ALIGN: right" Enabled='<%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="txtAgreement" runat="server" Text='<%# (string)DataBinder.Eval(Container.DataItem, "AgreementPoint")%>'
                                                        Visible=' <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "AgreementIsusing")!="0")%>'></asp:TextBox>
                                                        <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "AgreementIsusing") == "0") ? "" : ""%>
                                                    <asp:Label ID="lblAgreement" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "AgreementCode")%>'></asp:Label>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td rowspan=\"2\" NOWRAP>" : ""%>
                                                    <asp:TextBox style="TEXT-ALIGN: right" Enabled='<%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="txtTechnic" runat="server" Text='<%# (string)DataBinder.Eval(Container.DataItem, "TechnicPoint")%>'
                                                        Visible=' <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "TechnicIsusing")!="0")%>'></asp:TextBox>
                                                        <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "TechnicIsusing") == "0") ? "" : ""%>
                                                    <asp:Label ID="lblTechnic" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "TechnicCode")%>'></asp:Label>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td rowspan=\"2\" NOWRAP>" : ""%>
                                                    <asp:TextBox style="TEXT-ALIGN: right" Enabled='<%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="txtItemMajordomo" runat="server" Text='<%#(string)DataBinder.Eval(Container.DataItem, "ItemMajordomoPoint")%>'
                                                        Visible=' <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemMajordomoIsusing")!="0")%>'></asp:TextBox>
                                                        <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemMajordomoIsusing") == "0") ? "" : ""%>
                                                    <asp:Label ID="lblItemMajordomo" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemMajordomoCode")%>'></asp:Label>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td rowspan=\"2\" NOWRAP>" : ""%>
                                                    <asp:TextBox style="TEXT-ALIGN: right" Enabled='<%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="txtItemAgreement" runat="server" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemAgreementPoint")%>'
                                                        Visible=' <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1"  && (string)DataBinder.Eval(Container.DataItem, "ItemAgreementIsusing")!="0")%>'></asp:TextBox>
                                                        <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemAgreementIsusing") == "0") ? "" : ""%>
                                                   <asp:Label ID="lblItemAgreement" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemAgreementCode")%>'></asp:Label>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td rowspan=\"2\" NOWRAP>" : ""%>
                                                    <asp:TextBox style="TEXT-ALIGN: right"  Enabled='<%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="txtItemEngineering" runat="server" Text='<%#(string)DataBinder.Eval(Container.DataItem, "ItemEngineeringPoint")%>'
                                                        Visible=' <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemEngineeringIsusing")!="0")%>'></asp:TextBox>
                                                        <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemEngineeringIsusing") == "0") ? "" : ""%>
                                                    <asp:Label ID="lblItemEngineering" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemEngineeringCode")%>'></asp:Label>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td rowspan=\"2\" NOWRAP>" : ""%>
                                                    <asp:TextBox style="TEXT-ALIGN: right" Enabled='<%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="txtItemDesign" runat="server" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemDesignPoint")%>'
                                                        Visible=' <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemDesignIsusing")!="0")%>'></asp:TextBox>
                                                        <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemDesignIsusing") == "0") ? "" : ""%>
                                                    <asp:Label ID="lblItemDesign" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ItemDesignCode")%>'></asp:Label>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td rowspan=\"2\" NOWRAP>" : ""%>
                                                    <asp:TextBox style="TEXT-ALIGN: right" Enabled='<%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="txtClientService" runat="server" Text='<%#(string)DataBinder.Eval(Container.DataItem, "ClientServicePoint")%>'
                                                        Visible=' <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1"  && (string)DataBinder.Eval(Container.DataItem, "ClientServiceIsusing")!="0")%>'></asp:TextBox>
                                                         <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ClientServiceIsusing") == "0") ? "" : ""%>
                                                    <asp:Label ID="lblClientService" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "ClientServiceCode")%>'></asp:Label>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0")?"</tr><tr>":"" %>
                                                     <%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") == "1") ? "</tr><tr>" : ""%>
                                                    
                                                </ItemTemplate>
                                                
                                            </asp:Repeater>
                                        </div>
                                        
                                    </tr>
                                        <tr>
                                        <td colspan="3">总计</td>
                                        <td ></td>
                                        <td>
                                             <asp:Label ID="OPlblTotalPercentage" runat="server"></asp:Label> &nbsp;</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalAgreement" runat="server"></asp:Label> &nbsp;</td>
                                        <td>
                                           <asp:Label ID="OPlblTotalTechnic" runat="server"></asp:Label> &nbsp;</td>
                                        <td>
                                           <asp:Label ID="OPlblTotalItemMajordomo" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalItemAgreement" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalItemEngineering" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalItemDesign" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalClientService" runat="server"></asp:Label>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"></td>
                                        <td>
                                             调整系数</td>
                                        <td runat="server" id="OPTZAgreement">
                                            *1</td>
                                        <td runat="server" id="OPTZTechnic">
                                           *1</td>
                                        <td runat="server" id="OPTZItemMajordomo">
                                           *1</td>
                                        <td runat="server" id="OPTZItemAgreement">
                                            *5/3</td>
                                        <td runat="server" id="OPTZItemEngineering">
                                            *5/3</td>
                                        <td runat="server" id="OPTZItemDesign">
                                            *2</td>
                                        <td runat="server" id="OPTZClientService">
                                            *10</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"></td>
                                        <td >
                                             权重</td>
                                        <td runat="server" id="OPQZAgreement">
                                            *25%</td>
                                        <td runat="server" id="OPQZTechnic">
                                           *25%</td>
                                        <td runat="server" id="OPQZItemMajordomo">
                                           *20%</td>
                                        <td runat="server" id="OPQZItemAgreement">
                                            *10%</td>
                                        <td runat="server" id="OPQZItemEngineering">
                                            *10%</td>
                                        <td runat="server" id="OPQZItemDesign">
                                            *5%</td>
                                        <td runat="server" id="OPQZClientService">
                                            *5%</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">综 合 得 分</td>
                                        <td><asp:Label ID="OPlblpoint" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                             汇总</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalAgreement1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                           <asp:Label ID="OPlblTotalTechnic1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                           <asp:Label ID="OPlblTotalItemMajordomo1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalItemAgreement1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalItemEngineering1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalItemDesign1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="OPlblTotalClientService1" runat="server"></asp:Label>&nbsp;</td>
                                    </tr>
                                     </table>
                                    
                            </td>
                        </tr>
                   
   </div>
   <div id="EyeableDiv" runat="server">
           
                        <tr>
                            <td colspan="2">
                                <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                           
                                            <div id="messagelist" runat="server">
                                                <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="height: 19px">
                                                            &nbsp;项目名称：</td>
                                                        <td style="height: 19px">
                                                            <asp:Label ID="lblProjectNameList" runat="server" ></asp:Label>
                                                        </td>
                                                        <td >
                                                            承包商名称：</td>
                                                        <td>
                                                            <asp:Label ID="SupplierNameList" runat="server" ></asp:Label>                                                            
                                                        </td>
                                                        <td style="height: 19px" >
                                                            承包商项目经理&nbsp;</td>
                                                        <td style="height: 19px">
                                                            &nbsp;<asp:Label ID="SupplierManagerList" runat="server" ></asp:Label>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                &nbsp;</td>
                            <td>
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#000000">
                                    <tr>
                                        <td align="center">
                                            恶劣<br>
                                            （1~2分）</td>
                                        <td align="center">
                                            待改善<br>
                                            （3~4分）</td>
                                        <td align="center">
                                            尚可<br>
                                            （5~6分）</td>
                                        <td align="center">
                                            满意<br>
                                            （7~8分）</td>
                                        <td align="center">
                                            卓越<br>
                                            （9~10分）</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ● 远远达不到预期<br>
                                            &nbsp;&nbsp;&nbsp;表现及合同要求<br>
                                            ● 明显无力改善</td>
                                        <td>
                                            ● 达不到预期表现，<br>
                                            &nbsp;&nbsp;&nbsp;并需在督促协助下<br>
                                            &nbsp;&nbsp;&nbsp;才能满足合同要求<br>
                                            ● 改善态度消极
                                        </td>
                                        <td>
                                            ● 能达到行业及<br>
                                            &nbsp;&nbsp;&nbsp;合同基本要求<br>
                                            ● 较积极地回应<br>
                                            &nbsp;&nbsp;&nbsp;改善的要求
                                        </td>
                                        <td>
                                            ● 达到预期表现及<br>
                                            &nbsp;&nbsp;&nbsp;合同要求<br>
                                            ● 主动寻求改善
                                        </td>
                                        <td>
                                            ● 表现超越预期<br>
                                            ● 积极改善，<br>
                                            &nbsp;&nbsp;&nbsp;精益求精
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#000000">
                                    <tr>
                                        <td colspan="3">
                                            考虑因素</td>
                                        <td>
                                            评分指标</td>
                                        <td>
                                            权重</td>
                                        <td>
                                            集团合约部</td>
                                        <td>
                                            集团技术部</td>
                                        <td>
                                            项目总监</td>
                                        <td>
                                            项目合约部</td>
                                        <td>
                                            项目工程部</td>
                                        <td>
                                            项目设计部</td>
                                        <td>
                                            客服部</td>
                                    </tr>
                                    <tr>
                                       
                                        <div id="GradeList" runat="server">
                                            <asp:Repeater ID="Repeater2" runat="server">
                                                <ItemTemplate>
                                                    <td rowspan="<%# ((string)DataBinder.Eval(Container.DataItem, "ConsiderDiathesis") == "小计" ) ? "2" : (string)DataBinder.Eval(Container.DataItem, "ChildCount") %>" colspan="<%# ((string)DataBinder.Eval(Container.DataItem, "deep") == "2" && ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" ))?"2":"0" %>">
                                                        <%# (string)DataBinder.Eval(Container.DataItem, "ConsiderDiathesis")%>
                                                        &nbsp;</td>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") == "1") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "GradeGuideline") + "&nbsp;</td>" : ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0") ? "<td>" + (string)DataBinder.Eval(Container.DataItem, "GradeGuideline") + "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "tempPercentage") + "%&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "AgreementIsusing") != "0") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "AgreementPoint") + "&nbsp;</td>" : ""%>
                                                     <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "AgreementIsusing") == "0") ? "<td rowspan=\"2\">&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "TechnicIsusing") != "0") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "TechnicPoint") + "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "TechnicIsusing") == "0") ? "<td rowspan=\"2\">&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemMajordomoIsusing") != "0") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "ItemMajordomoPoint") + "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemMajordomoIsusing") == "0") ? "<td rowspan=\"2\">&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemAgreementIsusing") != "0") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "ItemAgreementPoint") + "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemAgreementIsusing") == "0") ? "<td rowspan=\"2\">&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemEngineeringIsusing") != "0") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "ItemEngineeringPoint") + "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemEngineeringIsusing") == "0") ? "<td rowspan=\"2\">&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemDesignIsusing") != "0") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "ItemDesignPoint") + "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ItemDesignIsusing") == "0") ? "<td rowspan=\"2\">&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ClientServiceIsusing") != "0") ? "<td rowspan=\"2\">" + (string)DataBinder.Eval(Container.DataItem, "ClientServicePoint") + "&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1" && (string)DataBinder.Eval(Container.DataItem, "ClientServiceIsusing") == "0") ? "<td rowspan=\"2\">&nbsp;</td>" : ""%>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "ChildCount") == "0")?"</tr><tr>":"" %>
                                                    <%# ((string)DataBinder.Eval(Container.DataItem, "issubtotal") == "1") ? "</tr><tr>" : ""%>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </tr>
                                     <tr>
                                        <td colspan="3">总计</td>
                                         <td ></td>
                                        <td>
                                             <asp:Label ID="lblTotalPercentage" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTotalAgreement" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                           <asp:Label ID="lblTotalTechnic" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                           <asp:Label ID="lblTotalItemMajordomo" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTotalItemAgreement" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTotalItemEngineering" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTotalItemDesign" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTotalClientService" runat="server"></asp:Label>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"></td>
                                        <td>
                                             调整系数</td>
                                        <td runat="server" id="TZAgreement">
                                            *1</td>
                                        <td runat="server" id="TZTechnic">
                                           *1</td>
                                        <td runat="server" id="TZItemMajordomo">
                                           *1</td>
                                        <td runat="server" id="TZItemAgreement">
                                            *5/3</td>
                                        <td runat="server" id="TZItemEngineering">
                                            *5/3</td>
                                        <td runat="server" id="TZItemDesign">
                                            *2</td>
                                        <td runat="server" id="TZClientService">
                                            *10</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"></td>
                                        <td >
                                             权重</td>
                                        <td runat="server" id="QZAgreement">
                                            *25%</td>
                                        <td runat="server" id="QZTechnic">
                                           *25%</td>
                                        <td runat="server" id="QZItemMajordomo">
                                           *20%</td>
                                        <td runat="server" id="QZItemAgreement">
                                            *10%</td>
                                        <td runat="server" id="QZItemEngineering">
                                            *10%</td>
                                        <td runat="server" id="QZItemDesign">
                                            *5%</td>
                                        <td runat="server" id="QZClientService">
                                            *5%</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">综 合 得 分</td>
                                        <td><asp:Label ID="lblpoint" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                             汇总</td>
                                        <td>
                                            <asp:Label ID="lblTotalAgreement1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                           <asp:Label ID="lblTotalTechnic1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                           <asp:Label ID="lblTotalItemMajordomo1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTotalItemAgreement1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTotalItemEngineering1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTotalItemDesign1" runat="server"></asp:Label>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTotalClientService1" runat="server"></asp:Label>&nbsp;</td>
                                    </tr>
                                      </table>
                                    
                            </td>
                        </tr>
               
   
   </div>
      
      </table>