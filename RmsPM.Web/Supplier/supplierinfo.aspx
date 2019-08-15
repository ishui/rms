<%@ Page Language="c#" Inherits="RmsPM.Web.Supplier.SupplierInfo" CodeFile="SupplierInfo.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>������Ϣ</title>
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
                    ������Ϣ</td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <input class="button" id="btnModify" onclick="doModify('');return false;" type="button"
                        value="�� ��" name="btnNew" runat="server">
                    <input class="button" id="btnGrade" visible="false" onclick="doGrade('');return false;"
                        type="button" value="�� ��" name="btnGrade" runat="server">
                    <input class="button" id="btnCompanyTitle" type="button" value="������˾����" name="btnCompanyTitle"
                        runat="server" onclick="OpenCompanyModif();return false;">
                    <input class="button" id="btnSupplierAuditing" type="button" value="�ύ���" name="btnSupplierAuditing"
                        runat="server" onclick="DoSupplierAuditing();return false;">
                    <input class="button" id="btnSingleAuditing" type="button" value="�������" name="btnSingleAuditing"
                        runat="server" onclick="if(!confirm('�˳����Ƿ�ͨ�����?'))return false;" onserverclick="btnSingle_Click">
                    <input class="button" id="btnDelete" onclick="if(!(confirm('ȷ��ɾ��������¼ ��'))) return false;"
                        type="button" value="ɾ ��" name="btnNew" runat="server" onserverclick="btnDelete_ServerClick"></td>
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
                                                    ���ƣ�</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblSupplierName" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ���ͣ�</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblTypeName" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ��ƣ�</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblAbbreviation" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ���˴���</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblArtificialPerson" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ������</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblAreaCode" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ע���ַ��</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblRegisteredAddress" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ע���ʽ�</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblRegisteredCapital" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ��ҵ���ʣ�</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblIndustryType" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ��ҵ������</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblIndustrySort" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ˰�����ܣ�</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblSJHG" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ����ִ�պţ�</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblLicenseID" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ˰��ִ�պţ�</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblTaxID" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ˰����룺</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblTaxNo" runat="server"></asp:Label></td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ��Ӫ���ޣ�</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblWorkTimeLimit" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ��Ӫ��ַ��</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblWorkAddress" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ��ҵ���ʣ�</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblStructure" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ��Ӫ��Χ��</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblWorkScope" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ������ʽ��</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblSaleType" runat="server"></asp:Label>
                                                </td>
                                                <td class="form-item" nowrap>
                                                    Ʒ�����</td>
                                                <td nowrap colspan="3">
                                                    &nbsp;<asp:Label ID="lblCharacterType" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    CCC��֤��</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblCCC" runat="server"></asp:Label>
                                                </td>
                                                <td class="form-item" nowrap>
                                                    ISO��֤��</td>
                                                <td nowrap colspan="3">
                                                    &nbsp;<asp:Label ID="lblISO" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ��ע��</td>
                                                <td colspan="5">
                                                    &nbsp;<asp:Label ID="lblRemark" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                    <td class="form-item" nowrap>
                                        �������У�</td>
                                    <td nowrap>
                                    <asp:Label runat="server" ID="lblOpenBank" ></asp:Label>
                                    </td>
                                    <td class="form-item" nowrap>
                                        �����ʺţ�</td>
                                    <td  nowrap>
                                    <asp:Label runat="server" ID="lblAccount" ></asp:Label>
                                    </td>
                                    <td class="form-item" nowrap>
                                        �ܿ��ˣ�</td>
                                    <td  nowrap>
                                    <asp:Label runat="server" ID="lblReciver" ></asp:Label>
                                    </td>
                                </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    �� ϵ �ˣ�</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblContractPerson" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ��ϵ�绰��</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblOfficePhone" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    �������룺</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblPostCode" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    �ֻ���</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblMobile" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ���棺</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="lblFax" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    EMail��</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ��ַ��</td>
                                                <td nowrap colspan="3">
                                                    &nbsp;<asp:Label ID="lblWebAddress" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    �������õȼ���</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblCreditLevel" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item">
                                                    ���ʣ�</td>
                                                <td colspan="3">
                                                    &nbsp;<asp:Label ID="lblQuality" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap>
                                                    ���ʵȼ���</td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblQualityGrade" runat="server"></asp:Label>��</td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ҵ����</td>
                                                <td colspan="3" id="PreWorkFlowPoint" runat="server">
                                                    &nbsp;<asp:Label ID="lblAchievement" runat="server"></asp:Label></td>
                                                <td class="form-item" runat="server" visible="false" id="WorkFlowPoint">
                                                    ����������</td>
                                                <td nowrap align="right">
                                                    &nbsp;<asp:Label ID="lblGradePoint" Visible="false" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ��Ʒ����</td>
                                                <td colspan="3" id="PreAuditted" runat="server">
                                                    &nbsp;<asp:Label ID="lblProduct" runat="server"></asp:Label></td>
                                                <td class="form-item" nowrap id="TdisAuditted" runat="server">
                                                    �Ƿ���ˣ�</td>
                                                <td nowrap>
                                                    &nbsp;<asp:Label ID="isAuditted" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form-item" nowrap>
                                                    ���������</td>
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
                                                ����ĵ�</td>
                                            <td>
                                                <input class="button-small" id="btnNewDocument" onclick="DoAddNewDocument('');return false;"
                                                    type="button" value="�����ĵ�" name="btnNewDocument" runat="server">&nbsp;</td>
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
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="����">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="#" onclick="ShowDocument(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "DocumentCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "Title") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Author" HeaderText="����"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MainText" HeaderText="����"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CreateDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd}">
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                HeaderText="ɾ��" CommandName="Delete"></asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                ������¼</td>
                                            <td>
                                                <input class="button-small" id="btnGradeAdd" visible="false" onclick="doGrade('');return false;"
                                                    type="button" value="�а�������" name="btnNew" runat="server"><br>
                                                <input class="button-small" id="btnPursveWorkflow" visible="false" onclick="doPursveGrade('');return false;"
                                                    type="button" value="��Ӧ������" name="btnNew" runat="server"><input visible="false" class="button-small"
                                                        id="btnPG" onclick="AddOpinion('');return false;" type="button" value="��������"
                                                        name="btnNew" runat="server"></td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="DataGrid_supplierRecord" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False"
                                        PageSize="15" Width="100%" OnDeleteCommand="DataGrid_supplierRecord_DeleteCommand" DataKeyField="supplierOpinionCode">
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="������">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="#" onclick="supplierOpinionModify(this.Code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "supplierOpinionCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "OpinionPerson") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Event" HeaderText="��Ŀ"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="OpinionDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd}">
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Delete" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;">
                                            </asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
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
                                            <asp:TemplateColumn SortExpression="" HeaderText="��Ӧ��">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="##" onclick='gotoDirect("<%# DataBinder.Eval(Container.DataItem, "ApplicationPath") %>","<%# DataBinder.Eval(Container.DataItem, "CaseCode") %>","<%# DataBinder.Eval(Container.DataItem, "ApplicationCode")%>","<%# ((string)DataBinder.Eval(Container.DataItem, "State"))=="0"?"List":""%>"); return false;'>
                                                        <%#  RmsPM.BLL.ProjectRule.GetSupplierName( DataBinder.Eval( Container,"DataItem.SupplierCode" ).ToString() ) %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="�а�����Ŀ����">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "ProjectManage") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="״̬">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.GradeMessage.GetContractStatusName(System.Convert.ToString(DataBinder.Eval(Container.DataItem, "State")))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="��������">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#System.Convert.ToString(DataBinder.Eval(Container.DataItem, "CreateDate"))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                ������ϵ��</td>
                                            <td>
                                                <input class="button-small" id="BtnLinkMan" onclick="DoAddSupplierLinkman();return false;"
                                                    type="button" value="������ϵ��" name="BtnLinkMan" runat="server">&nbsp;</td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="DtLinkmanList" runat="server" CssClass="list" CellPadding="0"
                                        GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                                        Width="100%" DataKeyField="SupplierLinkmanCode" OnDeleteCommand="DtLinkmanList_DeleteCommand">
                                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="��ϵ��">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="#" onclick="SupplierLinkmanInfo(this.Code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "SupplierLinkmanCode") %>'>
                                                        <%#  DataBinder.Eval(Container.DataItem, "ContractPerson")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="OfficePhone" HeaderText="��ϵ�绰"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Email" HeaderText="�����ʼ�"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="AreaName" HeaderText="��������"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ProjectName" HeaderText="��Ŀ����"></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Delete" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;">
                                            </asp:ButtonColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                �������</td>
                                            <td>
                                                <input class="button-small" id="btnModifySubjectSet" onclick="ModifySubjectSet();return false;"
                                                    type="button" value="�޸Ĳ������" name="btnModifySubjectSet" runat="server">&nbsp;</td>
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
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="����">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  RmsPM.BLL.SubjectRule.GetSubjectSetName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "SubjectSetCode"))) %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="��Ŀ����">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "ProjectCode")) == "" ? "����" : RmsPM.BLL.ProjectRule.GetProjectName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "ProjectCode")))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="U8Code" HeaderText="�������"></asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
                                            CssClass="ListHeadTr"></PagerStyle>
                                    </asp:DataGrid>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="intopic" width="200">
                                                ���ʵ���</td>
                                            <td>
                                                <input class="button-small" id="btnZiZhi" onclick="OpensurveyOpinion();return false;"
                                                    type="button" value="�������ʵ���" name="btnZiZhi" runat="server">&nbsp;</td>
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
                                            <asp:TemplateColumn HeaderText="����">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="##" onclick='gotoDirect("<%# DataBinder.Eval(Container.DataItem, "ApplicationPath") %>","<%# DataBinder.Eval(Container.DataItem, "CaseCode") %>","<%# DataBinder.Eval(Container.DataItem, "ApplicationCode")%>","<%# ((string)DataBinder.Eval(Container.DataItem, "State"))=="0"?"List":""%>"); return false;'>
                                                        <%#  RmsPM.BLL.ProjectRule.GetSupplierName( DataBinder.Eval( Container,"DataItem.SupplierCode" ).ToString() ) %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="רԱ����">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  DataBinder.Eval(Container.DataItem, "ZYName")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Grade" HeaderText="�ȼ�"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="״̬">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# RmsPM.BLL.SupplierSurveyOpinion.GetSupplierSurveyOpinionStatusName(System.Convert.ToString(DataBinder.Eval(Container.DataItem, "State")))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="��������">
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  DataBinder.Eval(Container.DataItem, "SurveyDate")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
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
                                ��غ�ͬ</td>
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
                                <asp:TemplateColumn HeaderText="��ͬ����" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                    <ItemTemplate>
                                        <a href="##" onclick="doViewContractInfo(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'>
                                            <%#  DataBinder.Eval(Container.DataItem, "ContractName") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ContractID" HeaderText="��ͬ���" ItemStyle-Wrap="False"
                                    HeaderStyle-Wrap="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="��ͬ����" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                    <ItemTemplate>
                                        <%#  DataBinder.Eval(Container.DataItem, "TypeName") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="��ǰ״̬" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                    <ItemTemplate>
                                        <%#  DataBinder.Eval(Container.DataItem, "StatusName") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="TotalMoney" HeaderText="��ͬ�ܽ��" DataFormatString="{0:N}"
                                    ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                </asp:BoundColumn>
                            </Columns>
                            <PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
                                CssClass="ListHeadTr"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
                <td valign="top">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="intopic" width="200">
                                ѯ�ۼ�¼</td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="dgEnquiry" runat="server" CssClass="list" CellPadding="0" CellSpacing="0"
                        GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                        Width="100%">
                        <AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                        <FooterStyle CssClass="list-title"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="ѯ�۵�">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle Wrap="False"></ItemStyle>
                                <ItemTemplate>
                                    <a href="#" onclick="OpenEnquiry(this.Code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "EnquiryCode") %>'>
                                        <%#  DataBinder.Eval(Container.DataItem, "EnquiryCode") %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="EnquiryDate" HeaderText="ѯ��ʱ��" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Remark" HeaderText="��ע"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
                            CssClass="ListHeadTr"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>

    <script language="javascript">
		
            function gotoDirect ( path , CaseCode,ApplicationCode,frameType)
	        {
		        OpenFullWindow(  path + ((path.indexOf("?")>0)?"&":"?")+"CaseCode=" + CaseCode +"&ApplicationCode="+ApplicationCode+"&frameType="+frameType,'���̴���');
	        }
	        function AddOpinion()
	        {
		        OpenMiddleWindow('SupplierOpinionModify.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>' , '����������¼');
	        }
        	
	        function DoAddNewDocument()
	        {
		        OpenLargeWindow('../Document/DocumentModify.aspx?Action=Insert&DocumentTypeCode=000006&Code=<%=Request.QueryString["SupplierCode"]%>&RefreshScript=RefreshDocument();','���������ĵ�'); 
	        }
        	
        	
	        function DoAddSupplierLinkman()
	        {
	            window.open('SupplierLinkman.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>','����������ϵ��',"width=780,height=380,fullscreen=0,top="+(window.screen.height-480)/2+",left="+(window.screen.width-640)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
        		
	        }
        	
	        function SupplierLinkmanInfo(code)
	        {
	            window.open('SupplierLinkman.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&SupplierLinkmanCode='+code,'����������ϵ��',"width=780,height=380,fullscreen=0,top="+(window.screen.height-480)/2+",left="+(window.screen.width-640)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
        		
	        }
        	
	        function ShowDocument(DocumentCode)
	        {
		       // OpenLargeWindow("../Document/DocumentInfo.aspx?DocumentCode=" + DocumentCode,'' );
		        var href = "";//window.parent.location.href;
                var GroupCodeReadonly = '<%=Request.QueryString["GroupCodeReadonly"]%>';
	            OpenLargeWindow("../Document/DocumentInfo.aspx?FromUrl=" + escape(href) + "&DocumentCode=" + DocumentCode + "&GroupCodeReadonly=" + GroupCodeReadonly,'�ĵ���Ϣ');
	        }
	        function ShowBidding(BiddingCode)
	        {
		        OpenMiddleWindow('../contract/ContractBiddingModify.aspx?Act=Edit&BiddingCode=' +BiddingCode,'�༭��ͬ��Ͷ��');
	        }
	        function RefreshDocument()
	        {
		        window.navigate( '../Supplier/SupplierInfo.aspx?Page=3&SupplierCode=<%=Request.QueryString["SupplierCode"]%>' );
	        }
        	
	        function doModify()
	        {
		        window.navigate('SupplierModify.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>' , '�޸ĳ���');
	        }
        	
	        function doGradeInfo(code)
	        {
		        OpenFullWindow('<%=SupplierGradePage%>?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&GradeMessageCode='+code , '�༭����');
	        }
        	
	        function doGrade()
	        {
	            OpenFullWindow('<%=SupplierGradePage%>?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&view=add','�а�������')
	        }
	        function OpenCompanyModif()
	        {
	            OpenCustomWindow('SupplierTitleModif.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&view=add','��˾����','300','200')
	        }
        	
	        function OpenCompanyInfo(SupplierTitleCode)
	        {
	            OpenCustomWindow('SupplierTitleModif.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&view=add&SupplierTitleCode='+SupplierTitleCode,'��˾����','300','200')
	        }
        	
	        function doPursveGrade()
	        {
	            OpenFullWindow('<%=SupplierPursveGradePage%>?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&view=add','��Ӧ������')
	        }
        	
	        function supplierOpinionModify(supplierOpinionCode)
	        {
		        OpenMiddleWindow('SupplierOpinionModify.aspx?supplierCode=<%=Request.QueryString["SupplierCode"]%>&supplierOpinionCode='+supplierOpinionCode , '�༭������¼');
	        }
        	
	        function doViewContractInfo( code )
	        {
		        OpenFullWindow('../Contract/ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'��ͬ��Ϣ');
	        }
	        function OpenEnquiry(code)
	        {
		        OpenFullWindow('../Material/EnquiryPriceInfo.aspx?EnquiryCode='+code,'ѯ�۵�');
	        }
        	
	        function OpensurveyOpinion()
            {
                OpenFullWindow("<%=SupplierSurveyOpinionPage%>?SupplierCode=<%=Request.QueryString["SupplierCode"]%>", "���ӵ������");
            }
            
            
            function OpensurveyOpinionInfo( code)
            {
                OpenFullWindow("../SupplierGrade/surveyOpinionModif.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>&ApplicationCode="+code, "���ӵ������");
            }


            function ModifySubjectSet()
            {
                OpenCustomWindow("../Finance/FinanceInterfaceAnalysisSupplierModify.aspx?SupplierCode=<%=Request.QueryString["SupplierCode"]%>", "�޸ĳ��̲������", 700, 500);
            }
            
	        function DoSupplierAuditing()
	        {
			    OpenFullWindow('<%=ViewState["_SupplierAuditingURL"]%>?ProjectCode=<%=ViewState["ProjectCode"]%>&ContractCode=<%=Request["ContractCode"]%>&SupplierCode=<%=Request["SupplierCode"]%>','�������_<%=Request["SupplierCode"]%>');	
	        }
    </script>

</body>
</html>
