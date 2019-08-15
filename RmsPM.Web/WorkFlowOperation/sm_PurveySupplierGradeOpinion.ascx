<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sm_PurveySupplierGradeOpinion.ascx.cs" Inherits="WorkFlowOperation_sm_PurveySupplierGradeOpinion" %>
<table width="100%" border="0" cellspacing="0"  bordercolor="#000000" cellpadding="0" class="form">
<div id="OperableDiv" runat="server">

                            <div id="messageMoid" runat="server">
                            <tr>
                            <td style="height: 112px" class="blackbordertdcontent">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#000000">
                                    <tr>
                                        <td class="blackbordertdcontent">
                                            项目名称</td>
                                        <td class="blackbordertd" >
                                            &nbsp;<asp:Label ID="lblProjectNameModi" runat="server"></asp:Label></td>
                                       
                                        <td class="blackbordertd" colspan=2>
                                            &nbsp; </td>
                                    </tr>
                                    <tr>
                                        <td class="blackbordertdcontent">
                                            产品名称</td>
                                        <td class="blackbordertd" colspan="3" style="width: 396px">
                                            &nbsp; <asp:Label ID="lblProduceName" runat="server" ></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="blackbordertdcontent">
                                            供应商</td>
                                        <td class="blackbordertd" colspan="3" style="width: 396px">
                                            &nbsp; <asp:Label ID="SupplierNameList" runat="server" ></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="blackbordertdcontent">
                                            供应商项目负责人</td>
                                        <td class="blackbordertd" colspan="3" style="width: 396px">
                                            &nbsp;<asp:Label ID="SupplierManagerList" runat="server" ></asp:Label></td>
                                    </tr>
                                </table>
                                </td>
                        </tr>
                                </div>
                            
                        <tr>
                            <td class="blackbordertd">
                                评分</td>
                        </tr>
                        
                        <tr>
                            <td colspan="2">
                                &nbsp;
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#000000">
                                    <tr>
                                       
                                        <td rowspan=2>&nbsp;
                                            </td>
                                            <td>部门
                                            </td>
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
                                            <td rowspan="2" align=center>综合得分<br>(满分为100分)</td>
                                    </tr>
                                    <tr>
                                       
                                        <td>权重
                                            </td>
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
                                       
                                       
                                            <asp:Repeater ID="Repeater2" runat="server">
                                                <ItemTemplate>
                                                    <td rowspan="2">
                                                        <%# (string)DataBinder.Eval(Container.DataItem, "ConsiderDiathesis")%>
                                                        &nbsp;</td>
                                                    <td rowspan="2"><%# (string)DataBinder.Eval(Container.DataItem, "tempPercentage") %>%&nbsp;</td>
                                                    <td rowspan="2"><%# ((string)DataBinder.Eval(Container.DataItem, "AgreementIsusing") != "0") ? (string)DataBinder.Eval(Container.DataItem, "AgreementPoint") : "----------"%>&nbsp;</td>
                                                    <td rowspan="2"><%#  ((string)DataBinder.Eval(Container.DataItem, "TechnicIsusing") != "0") ? (string)DataBinder.Eval(Container.DataItem, "TechnicPoint") : "----------"%>&nbsp;</td>
                                                    <td rowspan="2"><%# ((string)DataBinder.Eval(Container.DataItem, "ItemMajordomoIsusing") != "0") ? (string)DataBinder.Eval(Container.DataItem, "ItemMajordomoPoint"): "----------" %>&nbsp;</td>
                                                    <td rowspan="2"><%#((string)DataBinder.Eval(Container.DataItem, "ItemAgreementIsusing") != "0") ?  (string)DataBinder.Eval(Container.DataItem, "ItemAgreementPoint"): "----------" %>&nbsp;</td>
                                                    <td rowspan="2"><%# ((string)DataBinder.Eval(Container.DataItem, "ItemEngineeringIsusing") != "0") ? (string)DataBinder.Eval(Container.DataItem, "ItemEngineeringPoint"): "----------" %>&nbsp;</td>
                                                    <td rowspan="2"><%# ((string)DataBinder.Eval(Container.DataItem, "ItemDesignIsusing") != "0") ? (string)DataBinder.Eval(Container.DataItem, "ItemDesignPoint"): "----------" %>&nbsp;</td>
                                                    <td rowspan="2"><%# ((string)DataBinder.Eval(Container.DataItem, "ClientServiceIsusing") != "0") ? (string)DataBinder.Eval(Container.DataItem, "ClientServicePoint") : "----------" %>&nbsp;</td>
                                                    <td rowspan="2"><%# (string)DataBinder.Eval(Container.DataItem, "UnitTotalPoint")%>&nbsp;</td>
                                                    </tr><tr>
                                                    </tr><tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        
                                    </tr>
                                     <tr>
                                       
                                        <td>
                                             汇总</td>
                                             <td>
                                             100%</td>
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
                                            <td>
                                            <asp:Label ID="lblpoint" runat="server"></asp:Label>&nbsp;</td>
                                    </tr>
                                    
                                      </table>
                                    
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:10%;">
                                            评定结果:</td>
                                         <td width="90%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="6%">
                                            &nbsp;</td>
                                        <td >
                                            91分~100分&nbsp;□ 作为优先推荐单位，自动成为集团相关标段的投标单位，直至另行通知。</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td >
                                            71分~ 90分&nbsp;□ 通告其他项目可优先考虑作为投标单位。</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 19px">
                                            &nbsp;</td>
                                        <td style="height: 19px" >
                                            41分~ 70分&nbsp;□ 继续保留在我司承包商/供应商数据库内供各项目选择。</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 19px">
                                            &nbsp;</td>
                                        <td style="height: 19px" >
                                            21分~ 40分&nbsp;□ 表现差强人意，通告其他项目对此单位更严格监督及留意。</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td >
                                            11分~ 20分&nbsp;□ 禁止其参与投标六个月。</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 28px">
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;&nbsp;0分~10分&nbsp;□ 禁止其参与投标不少于二十四个月，直至另行通知。</td>
                                    </tr>
                                </table>
                               </td>
                        </tr>
                        </div>
                        </table>