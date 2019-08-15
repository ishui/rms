<%@ Control Language="C#" AutoEventWireup="true" CodeFile="localworkflowcasestate.ascx.cs"
    Inherits="WorkFlowControl_localworkflowcasestate" %>
<%@ Register Assembly="RmsPM.BLL" Namespace="Rms.ControlLb" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc1" TagName="ImageSign" Src="../WorkFlowControl/WorkFlowFormSign.ascx" %>
<table id="Table1" cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
        <td valign="top" width="100%" >
          <div runat="server" style="border:#000000 0px solid;" id="divList"> 
                <asp:DataGrid ID="DataGrid4" runat="server" CellPadding="0" GridLines="Horizontal"
                    AutoGenerateColumns="False" PageSize="18" Width="100%" ShowHeader="False"
                    BorderColor="Black" OnItemDataBound="DataGrid4_ItemDataBound">
                    <HeaderStyle CssClass="list-title"></HeaderStyle>
                    <FooterStyle CssClass="list-title"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemStyle Wrap="true" Width="40pt" CssClass="blackbordertdcontentRightCenter"  VerticalAlign="middle" HorizontalAlign="center"></ItemStyle>
                            <ItemTemplate>
                            <table>
                                <tr>
                                    <td style="width:10pt;">
                                    <%# DataBinder.Eval(Container.DataItem, "RoleName")%>
                                    </td>
                                </tr>
                            </table>
                                
                            </ItemTemplate>
                            <HeaderStyle Width="10px" Wrap="False" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="意见">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False" Height="100%" VerticalAlign="Bottom"></ItemStyle>
                            <ItemTemplate>
                                <table id="Table2" width="100%" cellspacing="0" cellpadding="0" style="height:100%;">
                                    <tr>
                                        <td style="border: 0;">
                                            <div width="100%" runat="server" style="margin-left: 6px; width: 100%; height:100%;"
                                                id="OpinionTextAreaDiv">
                                                <%# DataBinder.Eval(Container.DataItem, "Opinion")%>
                                            </div>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td align="right">
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                <tr>
                                                    <td align="right">
                                                    <span id="CheckSpan" runat="server"><%# DataBinder.Eval(Container.DataItem, "OpinionConfirm")%></span>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Image ID="imgSign" runat="server"/>&nbsp;
                                                        <input id="HiddenUserCode" type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "ActUserCode")%>' name="HiddenSelectRouterCode" runat="server"/>

                                                     </td>
                                                </tr>
                                                <tr align="right">
                                                    <td style="border: 0;">
                                                        签字：<span runat="server" id="OpinionUser">
                                                            <%# RmsPM.BLL.SystemRule.GetUserName(DataBinder.Eval(Container.DataItem, "ActUserCode").ToString())%>
                                                        </span>&nbsp;&nbsp;&nbsp;&nbsp;日期：<span runat="server" id="OpinionDate">
                                                            <%# RmsPM.BLL.WorkFlowRule.GetFormatExcedableDate((DateTime)DataBinder.Eval(Container.DataItem, "FromDate"),RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "FinishDate")))%>
                                                        </span>
                                                    </td>
                                                </tr>
                                              </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="公开" Visible="false">
                            <ItemTemplate>
                                <font face="宋体">
                                    <asp:CheckBox ID="chkopinionshow" runat="server" Visible="false"></asp:CheckBox></font>
                            </ItemTemplate>
                            <HeaderStyle Width="10px" />
                        </asp:TemplateColumn>
                        
                        <asp:BoundColumn Visible="False" DataField="ActCode"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="FromTaskCode"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="FromUserCode"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="Copy"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                        CssClass="ListHeadTr"></PagerStyle>
                </asp:DataGrid>
            </div>
          
                <table id="TableTitle" cellspacing="0" cellpadding="0" border="0" runat="server">
                    <tr>
                        <td class="intopic" width="200">
                            <font face="宋体">流 程</font></td>
                        <td>
                            <font face="宋体"></font>
                        </td>
                    </tr>
                </table>
                <asp:DataGrid ID="dgList" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
                    AutoGenerateColumns="False" PageSize="18" Width="100%">
                    <HeaderStyle CssClass="list-title"></HeaderStyle>
                    <FooterStyle CssClass="list-title"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="序号">
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="发件人/发件日期">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "FromUserCode").ToString()) %>
                                &nbsp;/<br />
                                <span title="发件日期">
                                    <%# DataBinder.Eval(Container.DataItem, "FromDate", "{0:yyyy-MM-dd HH:mm}")%>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="当前任务">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ToTaskName") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="处理人">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# RmsPM.BLL.WorkFlowActUserSelect.GetWorkFlowActUser(DataBinder.Eval(Container.DataItem, "ActCode").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="签收日期/结束日期">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "SignDate", "{0:yyyy-MM-dd HH:mm}")%>
                                <br />
                                <%# RmsPM.BLL.WorkFlowRule.GetFormatExcedableDate((DateTime)DataBinder.Eval(Container.DataItem, "FromDate"),RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "FinishDate")))%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="Opinion" HeaderText="意见"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="公开" >
                            <ItemTemplate>
                                <font face="宋体">
                                    <asp:CheckBox ID="chkopinionshow" runat="server" Visible="False"></asp:CheckBox></font>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn Visible="False" DataField="ActCode"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="FromTaskCode"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="FromUserCode"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="Copy"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                        CssClass="ListHeadTr"></PagerStyle>
                </asp:DataGrid>
                <asp:DataGrid ID="DataGrid3" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
                    AutoGenerateColumns="False" PageSize="18" Width="100%">
                    <HeaderStyle CssClass="list-title"></HeaderStyle>
                    <FooterStyle CssClass="list-title"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="序号">
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="任 务">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ToTaskName") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="发件人">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "FromUserCode").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="发件日期">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "FromDate", "{0:yyyy-MM-dd HH:mm}")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;---->
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="处理人">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# RmsPM.BLL.WorkFlowActUserSelect.GetWorkFlowActUser(DataBinder.Eval(Container.DataItem, "ActCode").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="签收日期">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "SignDate", "{0:yyyy-MM-dd HH:mm}")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="结束日期">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <ItemTemplate>
                                <%# RmsPM.BLL.WorkFlowRule.GetFormatExcedableDate((DateTime)DataBinder.Eval(Container.DataItem, "FromDate"),RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "FinishDate")))%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="Opinion" HeaderText="意见"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="公开" >
                            <ItemTemplate>
                                <font face="宋体">
                                    <asp:CheckBox ID="chkopinionshow" runat="server"></asp:CheckBox></font>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn Visible="False" DataField="ActCode"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="FromTaskCode"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="FromUserCode"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="Copy"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                        CssClass="ListHeadTr"></PagerStyle>
                </asp:DataGrid>
            
        </td>
    </tr>
</table>

<script language="javascript">
function WorkFlowCaseStateOpenOpinionView(OpinionCode)
{
	OpenMiddleWindow('../WorkFlowControl/WorkFlowOpinionView.aspx?OpinionCode='+OpinionCode);
     
}
</script>

<asp:DataGrid ID="DataGrid1" runat="server">
</asp:DataGrid>
<asp:DataGrid ID="DataGrid2" runat="server">
</asp:DataGrid>
