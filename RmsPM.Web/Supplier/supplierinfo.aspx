<%@ Page Language="c#" Inherits="RmsPM.Web.Supplier.SupplierInfo" CodeFile="SupplierInfo.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>厂商信息</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    厂商信息</td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <input class="button" id="btnModify" onclick="doModify('');return false;" type="button"
                        value="修 改" name="btnNew" runat="server">
                    <input class="button" id="btnGrade" visible="false" onclick="doGrade('');return false;"
                        type="button" value="评 分" name="btnGrade" runat="server">
                    <input class="button" id="btnCompanyTitle" type="button" value="新增公司标题" name="btnCompanyTitle"
                        runat="server" onclick="OpenCompanyModif();return false;">
                    <input class="button" id="btnSupplierAuditing" type="button" value="提交审核" name="btnSupplierAuditing"
                        runat="server" onclick="DoSupplierAuditing();return false;">
                    <input class="button" id="btnSingleAuditing" type="button" value="单步审核" name="btnSingleAuditing"
                        runat="server" onclick="if(!confirm('此厂商是否通过审核?'))return false;" onserverclick="btnSingle_Click">
                    <input class="button" id="btnDelete" onclick="if(!(confirm('确定删除这条记录 ？'))) return false;"
                        type="button" value="删 除" name="btnNew" runat="server" onserverclick="btnDelete_ServerClick"></td>
            </tr>
            <tr>
                <td valign="top">
                    <table cellspacing="7" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="border-right: #ededed 3px dotted; padding-right: 7px" valign="top" width="60%">
                                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    名称：</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblSupplierName" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    类型：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblTypeName" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    简称：</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblAbbreviation" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    法人代表：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblArtificialPerson" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    地区：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblAreaCode" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    注册地址：</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblRegisteredAddress" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    注册资金：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblRegisteredCapital" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    行业性质：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblIndustryType" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    行业排名：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblIndustrySort" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    税籍户管：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblSJHG" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    工商执照号：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblLicenseID" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    税务执照号：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblTaxID" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    税务代码：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblTaxNo" runat="server"></asp:Label></td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    经营期限：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblWorkTimeLimit" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    经营地址：</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblWorkAddress" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    企业性质：</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblStructure" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    经营范围：</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblWorkScope" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    销售形式：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblSaleType" runat="server"></asp:Label>
                                                </td>
                                                <td class="form-item" nowrap>
                                                    品质类别：</td>
                                                <td nowrap colspan="3">
                                                    &nbsp;<asp:Label ID="lblCharacterType" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    CCC认证：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblCCC" runat="server"></asp:Label>
                                                </td>
                                                <td class="form-item" nowrap>
                                                    ISO认证：</td>
                                                <td nowrap colspan="3">
                                                    &nbsp;<asp:Label ID="lblISO" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    备注：</td>
                                                <td colspan="5">
                                                    &nbsp;<asp:Label ID="lblRemark" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                    <td class="form-item" nowrap>
                                        开户银行：</td>
                                    <td nowrap>
                                    <asp:Label runat="server" ID="lblOpenBank" ></asp:Label>
                                    </td>
                                    <td class="form-item" nowrap>
                                        银行帐号：</td>
                                    <td  nowrap>
                                    <asp:Label runat="server" ID="lblAccount" ></asp:Label>
                                    </td>
                                    <td class="form-item" nowrap>
                                        受款人：</td>
                                    <td  nowrap>
                                    <asp:Label runat="server" ID="lblReciver" ></asp:Label>
                                    </td>
                                </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    联 系 人：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblContractPerson" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    联系电话：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblOfficePhone" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    邮政编码：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblPostCode" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    手机：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblMobile" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    传真：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblFax" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    EMail：</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    网址：</td>
                                                <td nowrap colspan="3">
                                                    &nbsp;<asp:Label ID="lblWebAddress" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    银行信用等级：</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblCreditLevel" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    资质：</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblQuality" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    资质等级：</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblQualityGrade" runat="server"></asp:Label>级</td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    业绩：</td>
                                                <td colspan="3" id="PreWorkFlowPoint" runat="server">
                                                    &nbsp;<asp:Label ID="lblAchievement" runat="server"></asp:Label></td>
                                                <td class="form-item" runat="server" visible="false" id="WorkFlowPoint">
                                                    评估分数：</td>
                                                <td nowrap align="right">
                                                    &nbsp;<asp:Label ID="lblGradePoint" Visible="false" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    产品服务：</td>
                                                <td colspan="3" id="PreAuditted" runat="server">
                                                    &nbsp;<asp:Label ID="lblProduct" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap id="TdisAuditted" runat="server">
                                                    是否审核：</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="isAuditted" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    评价意见：</td>
                                                <td colspan="5">
                                                    &nbsp;<asp:Label ID="lblCheckOpinion" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <div runat="server" id="DivCompanyTitle">
                                                        </div>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                相关文档</td>
                                            <td>
                                                <input class="button-small" id="btnNewDocument" onclick="DoAddNewDocument('');return false;"
                                                    type="button" value="新增文档" name="btnNewDocument" runat="server">&nbsp;</td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="dgDocumentList" runat="server" CssClass="list" CellPadding="0"
                                        CellSpacing="0" GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False"
                                        PageSize="15" Width="100%">
                                        <ItemStyle CssClass=""></ItemStyle>
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="DocumentCode"></asp:BoundColumn>
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="标题">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="#" onclick="ShowDocument(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "DocumentCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "Title") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Author" HeaderText="作者"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MainText" HeaderText="正文"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CreateDate" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd}">
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                HeaderText="删除" CommandName="Delete"></asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                评估记录</td>
                                            <td>
                                                <input class="button-small" id="btnGradeAdd" visible="false" onclick="doGrade('');return false;"
                                                    type="button" value="承包商评估" name="btnNew" runat="server"><br>
                                                <input class="button-small" id="btnPursveWorkflow" visible="false" onclick="doPursveGrade('');return false;"
                                                    type="button" value="供应商评估" name="btnNew" runat="server"><input visible="false" class="button-small"
                                                        id="btnPG" onclick="AddOpinion('');return false;" type="button" value="新增评估"
                                                        name="btnNew" runat="server"></td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="DataGrid_supplierRecord" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False"
                                        PageSize="15" Width="100%" OnDeleteCommand="DataGrid_supplierRecord_DeleteCommand" DataKeyField="supplierOpinionCode">
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="评估人">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="#" onclick="supplierOpinionModify(this.Code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "supplierOpinionCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "OpinionPerson") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Event" HeaderText="项目"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="OpinionDate" HeaderText="评估时间" DataFormatString="{0:yyyy-MM-dd}">
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Delete" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;">
                                            </asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                    <asp:DataGrid ID="DataGrid_supplierGrade" runat="server" CssClass="list" CellPadding="0"
                                        CellSpacing="0" GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False"
                                        PageSize="15" Width="100%">
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn DataField="ApplicationCode" Visible="false"></asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="" HeaderText="供应商">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="##" onclick='gotoDirect("<%# DataBinder.Eval(Container.DataItem, "ApplicationPath") %>","<%# DataBinder.Eval(Container.DataItem, "CaseCode") %>","<%# DataBinder.Eval(Container.DataItem, "ApplicationCode")%>","<%# ((string)DataBinder.Eval(Container.DataItem, "State"))=="0"?"List":""%>"); return false;'>
                                                        <%#  RmsPM.BLL.ProjectRule.GetSupplierName( DataBinder.Eval( Container,"DataItem.SupplierCode" ).ToString() ) %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="承包商项目经理">
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
                                        <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                厂家联系人</td>
                                            <td>
                                                <input class="button-small" id="BtnLinkMan" onclick="DoAddSupplierLinkman();return false;"
                                                    type="button" value="新增联系人" name="BtnLinkMan" runat="server">&nbsp;</td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="DtLinkmanList" runat="server" CssClass="list" CellPadding="0"
                                        GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                                        Width="100%" DataKeyField="SupplierLinkmanCode" OnDeleteCommand="DtLinkmanList_DeleteCommand">
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="联系人">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="#" onclick="SupplierLinkmanInfo(this.Code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "SupplierLinkmanCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "ContractPerson")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="OfficePhone" HeaderText="联系电话"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Email" HeaderText="电子邮件"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="AreaName" HeaderText="地区名称"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ProjectName" HeaderText="项目名称"></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Delete" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;">
                                            </asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                财务编码</td>
                                            <td>
                                                <input class="button-small" id="btnModifySubjectSet" onclick="ModifySubjectSet();return false;"
                                                    type="button" value="修改财务编码" name="btnModifySubjectSet" runat="server">&nbsp;</td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="dgSupplierSubjectSet" runat="server" CssClass="list" CellPadding="0"
                                        CellSpacing="0" GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False"
                                        PageSize="15" Width="100%">
                                        <ItemStyle CssClass=""></ItemStyle>
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="帐套">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  RmsPM.BLL.SubjectRule.GetSubjectSetName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "SubjectSetCode"))) %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="项目名称">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "ProjectCode")) == "" ? "集团" : RmsPM.BLL.ProjectRule.GetProjectName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "ProjectCode")))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="U8Code" HeaderText="财务编码"></asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                资质调查</td>
                                            <td>
                                                <input class="button-small" id="btnZiZhi" onclick="OpensurveyOpinion();return false;"
                                                    type="button" value="新增资质调查" name="btnZiZhi" runat="server">&nbsp;</td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="dgsurvey" runat="server" CssClass="list" CellPadding="0" CellSpacing="0"
                                        GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                                        Width="100%">
                                        <ItemStyle CssClass=""></ItemStyle>
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="厂商">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="##" onclick='gotoDirect("<%# DataBinder.Eval(Container.DataItem, "ApplicationPath") %>","<%# DataBinder.Eval(Container.DataItem, "CaseCode") %>","<%# DataBinder.Eval(Container.DataItem, "ApplicationCode")%>","<%# ((string)DataBinder.Eval(Container.DataItem, "State"))=="0"?"List":""%>"); return false;'>
                                                        <%#  RmsPM.BLL.ProjectRule.GetSupplierName( DataBinder.Eval( Container,"DataItem.SupplierCode" ).ToString() ) %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="专员姓名">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  DataBinder.Eval(Container.DataItem, "ZYName")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Grade" HeaderText="等级"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="状态">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.SupplierSurveyOpinion.GetSupplierSurveyOpinionStatusName(System.Convert.ToString(DataBinder.Eval(Container.DataItem, "State")))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="调查日期">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  DataBinder.Eval(Container.DataItem, "SurveyDate")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                </td>
                </td>
            </tr>
            <tr>
                <td valign="top" height="200px">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="intopic" width="200">
                                相关合同</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <div style="overflow: auto; width: 100%; position: absolute; height: 100%">
                        <asp:DataGrid ID="dgContract" runat="server" CssClass="list" CellPadding="0" CellSpacing="0"
                            GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                            Width="100%">
                            <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                            <FooterStyle CssClass="list-title"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="合同名称" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                    <ItemTemplate>
                                        <a href="##" onclick="doViewContractInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'>
                                            <%#  DataBinder.Eval(Container.DataItem, "ContractName") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ContractID" HeaderText="合同编号" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="合同类型" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                    <ItemTemplate>
                                        <%#  DataBinder.Eval(Container.DataItem, "TypeName") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="当前状态" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                    <ItemTemplate>
                                        <%#  DataBinder.Eval(Container.DataItem, "StatusName") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="TotalMoney" HeaderText="合同总金额" DataFormatString="{0:N}"
                                    ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                </asp:BoundColumn>
                            </Columns>
                            <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                CssClass="ListHeadTr"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
                <td valign="top">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="intopic" width="200">
                                询价记录</td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="dgEnquiry" runat="server" CssClass="list" CellPadding="0" CellSpacing="0"
                        GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                        Width="100%">
                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                        <FooterStyle CssClass="list-title"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="询价单">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle Wrap="False"></ItemStyle>
                                <ItemTemplate>
                                    <a href="#" onclick="OpenEnquiry(this.Code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "EnquiryCode") %>'>
                                        <%#  DataBinder.Eval(Container.DataItem, "EnquiryCode") %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="EnquiryDate" HeaderText="询价时间" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                            CssClass="ListHeadTr"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>

    <script language="javascript">
		
            function gotoDirect ( path , CaseCode,ApplicationCode,frameType)
	        {
		        OpenFullWindow(  path + ((path.indexOf("?")>0)?"&":"?")+"CaseCode=" + CaseCode +"&ApplicationCode="+ApplicationCode+"&frameType="+frameType,'流程处理');
	        }
	        function AddOpinion()
	        {
		        OpenMiddleWindow('SupplierOpinionModify.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>' , '新增评估记录');
	        }
        	
	        function DoAddNewDocument()
	        {
		        OpenLargeWindow('../Document/DocumentModify.aspx?Action=Insert&DocumentTypeCode=000006&Code=<%=Request.QueryString["SupplierCode"]%>&RefreshScript=RefreshDocument();','新增厂商文档'); 
	        }
        	
        	
	        function DoAddSupplierLinkman()
	        {
	            window.open('SupplierLinkman.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>','新增厂商联系人',"width=780,height=380,fullscreen=0,top="+(window.screen.height-480)/2+",left="+(window.screen.width-640)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
        		
	        }
        	
	        function SupplierLinkmanInfo(code)
	        {
	            window.open('SupplierLinkman.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&SupplierLinkmanCode='+code,'新增厂商联系人',"width=780,height=380,fullscreen=0,top="+(window.screen.height-480)/2+",left="+(window.screen.width-640)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
        		
	        }
        	
	        function ShowDocument(DocumentCode)
	        {
		       // OpenLargeWindow("../Document/DocumentInfo.aspx?DocumentCode=" + DocumentCode,'' );
		        var href = "";//window.parent.location.href;
                var GroupCodeReadonly = '<%=Request.QueryString["GroupCodeReadonly"]%>';
	            OpenLargeWindow("../Document/DocumentInfo.aspx?FromUrl=" + escape(href) + "&DocumentCode=" + DocumentCode + "&GroupCodeReadonly=" + GroupCodeReadonly,'文档信息');
	        }
	        function ShowBidding(BiddingCode)
	        {
		        OpenMiddleWindow('../contract/ContractBiddingModify.aspx?Act=Edit&BiddingCode=' +BiddingCode,'编辑合同招投标');
	        }
	        function RefreshDocument()
	        {
		        window.navigate( '../Supplier/SupplierInfo.aspx?Page=3&SupplierCode=<%=Request.QueryString["SupplierCode"]%>' );
	        }
        	
	        function doModify()
	        {
		        window.navigate('SupplierModify.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>' , '修改厂商');
	        }
        	
	        function doGradeInfo(code)
	        {
		        OpenFullWindow('<%=SupplierGradePage%>?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&GradeMessageCode='+code , '编辑评分');
	        }
        	
	        function doGrade()
	        {
	            OpenFullWindow('<%=SupplierGradePage%>?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&view=add','承包商评分')
	        }
	        function OpenCompanyModif()
	        {
	            OpenCustomWindow('SupplierTitleModif.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&view=add','公司标题','300','200')
	        }
        	
	        function OpenCompanyInfo(SupplierTitleCode)
	        {
	            OpenCustomWindow('SupplierTitleModif.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&view=add&SupplierTitleCode='+SupplierTitleCode,'公司标题','300','200')
	        }
        	
	        function doPursveGrade()
	        {
	            OpenFullWindow('<%=SupplierPursveGradePage%>?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&view=add','供应商评分')
	        }
        	
	        function supplierOpinionModify(supplierOpinionCode)
	        {
		        OpenMiddleWindow('SupplierOpinionModify.aspx?supplierCode=<%=Request.QueryString["SupplierCode"]%>&supplierOpinionCode='+supplierOpinionCode , '编辑评估记录');
	        }
        	
	        function doViewContractInfo( code )
	        {
		        OpenFullWindow('../Contract/ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'合同信息');
	        }
	        function OpenEnquiry(code)
	        {
		        OpenFullWindow('../Material/EnquiryPriceInfo.aspx?EnquiryCode='+code,'询价单');
	        }
        	
	        function OpensurveyOpinion()
            {
                OpenFullWindow("<%=SupplierSurveyOpinionPage%>?SupplierCode=<%=Request.QueryString["SupplierCode"]%>", "增加调查意见");
            }
            
            
            function OpensurveyOpinionInfo( code)
            {
                OpenFullWindow("../SupplierGrade/surveyOpinionModif.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&ApplicationCode="+code, "增加调查意见");
            }


            function ModifySubjectSet()
            {
                OpenCustomWindow("../Finance/FinanceInterfaceAnalysisSupplierModify.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>", "修改厂商财务编码", 700, 500);
            }
            
	        function DoSupplierAuditing()
	        {
			    OpenFullWindow('<%=ViewState["_SupplierAuditingURL"]%>?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&SupplierCode=<%=Request["SupplierCode"]%>','厂商审核_<%=Request["SupplierCode"]%>');	
	        }
    </script>

</body>
</html>
