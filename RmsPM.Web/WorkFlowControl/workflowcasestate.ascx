<%@ Control Language="c#" Inherits="RmsPM.Web.WorkFlowControl.Migrated_WorkFlowCaseState"
    CodeFile="WorkFlowCaseState.ascx.cs" %>
<%@ Register Assembly="RmsPM.BLL" Namespace="Rms.ControlLb" TagPrefix="cc1" %>

<table class="table" id="Table1" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td valign="top">
            <table id="Table2" cellspacing="0" cellpadding="0" border="0" runat="server">
                <tr>
                    <td class="intopic" width="200">
                        <font face="宋体">意 见</font></td>
                    <td>
                        <font face="宋体"></font>
                    </td>
                </tr>
            </table>
            <asp:DataGrid ID="DataGrid4" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
                AutoGenerateColumns="False" PageSize="18" Width="100%">
                <HeaderStyle CssClass="list-title"></HeaderStyle>
                <FooterStyle CssClass="list-title"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="序号">
                        <ItemTemplate>
                            <%# Container.ItemIndex + 1%>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="任 务" Visible="False">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "ToTaskName") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="发件人" Visible="False">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <%# RmsPM.BLL.SystemRule.GetUserNameByProjectCode(DataBinder.Eval(Container.DataItem, "FromUserCode").ToString(),this.ProjectCode,null)%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="发件日期" Visible="False">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "FromDate", "{0:yyyy-MM-dd HH:mm}")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn Visible="False">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            &nbsp;&nbsp;&nbsp;---->
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="岗　　位">
                        <HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "RoleName")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="处理人">
                        <HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <%# RmsPM.BLL.WorkFlowActUserSelect.GetWorkFlowActUser(DataBinder.Eval(Container.DataItem, "ActCode").ToString(),this.ProjectCode) %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="签收日期" Visible="False">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "SignDate", "{0:yyyy-MM-dd HH:mm}")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="日期">
                        <HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <%# RmsPM.BLL.WorkFlowRule.GetFormatExcedableDate((DateTime)DataBinder.Eval(Container.DataItem, "FromDate"),RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container.DataItem, "FinishDate")))%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Opinion" HeaderText="意见">
                        <HeaderStyle Width="100%" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="公开" Visible="false">
                        <ItemTemplate>
                            <font face="宋体">
                                <asp:CheckBox ID="chkopinionshow" runat="server" Visible="False"></asp:CheckBox></font>
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
            <table id="TableTitle" cellspacing="0" cellpadding="0" border="0" runat="server">
                <tr>
                    <td class="intopic" width="200">
                        <font face="宋体">流 程</font> <a  href="###" id="showword" onclick="workflowscoutshow();">点击折叠</a></td>
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
                            <%# RmsPM.BLL.SystemRule.GetUserNameByProjectCode(DataBinder.Eval(Container.DataItem, "FromUserCode").ToString(),this.ProjectCode,null)%>
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
                            <%# RmsPM.BLL.WorkFlowActUserSelect.GetWorkFlowActUser(DataBinder.Eval(Container.DataItem, "ActCode").ToString(),this.ProjectCode) %>
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
                    <asp:TemplateColumn HeaderText="公开">
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
                            <%# RmsPM.BLL.SystemRule.GetUserNameByProjectCode(DataBinder.Eval(Container.DataItem, "FromUserCode").ToString(),this.ProjectCode,null)%>
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
                            <%# RmsPM.BLL.WorkFlowActUserSelect.GetWorkFlowActUser(DataBinder.Eval(Container.DataItem, "ActCode").ToString(),this.ProjectCode) %>
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
                    <asp:BoundColumn DataField="Opinion" HeaderText="意见" Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="公开">
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
        </td>
    </tr>
</table>

<script language="javascript">
function WorkFlowCaseStateOpenOpinionView(OpinionCode)
{
	OpenMiddleWindow('../WorkFlowControl/WorkFlowOpinionView.aspx?OpinionCode='+OpinionCode);
     
}

//流程监控折叠

function workflowscoutshow()
{
   var objdgList = document.all("<%=ClientID%>" + "_" + "dgList");
   var objDataGrid3 = document.all("<%=ClientID%>" + "_" + "DataGrid3");
   
   if(objDataGrid3!=null)
   {
      if(objDataGrid3.style.display=="")
      {
      objDataGrid3.style.display = "none";
      //window.event.srcElement.innerText = "点击展开"；
      //alert( document.anchors("showword"));
      //document.anchors("showword").innerText="点击展开"；
      document.all("showword").innerText="点击展开";
      }
      else
      {
        objDataGrid3.style.display = "";
        document.all("showword").innerText="点击折叠";
      }
   }
   else if(objdgList!=null)
   {
      if(objdgList.style.display=="")
      {
         objdgList.style.display = "none";
         document.all("showword").innerText="点击展开";
      }
      else
      {
         objdgList.style.display = "";
         document.all("showword").innerText="点击折叠";
      }
   }
}
function iniworkflowscoutshow()
{
   var objdgList = document.all("<%=ClientID%>" + "_" + "dgList");
   var objDataGrid3 = document.all("<%=ClientID%>" + "_" + "DataGrid3");
   if(objDataGrid3!=null)
   {
      objDataGrid3.style.display = "none";
      document.all("showword").innerText="点击展开";
   }
   else if(objdgList!=null)
   {
      objdgList.style.display = "none";
      document.all("showword").innerText="点击展开";
   }
}
if("<%=System.Configuration.ConfigurationSettings.AppSettings["PMName"].ToLower() %>"=="shimaopm")
iniworkflowscoutshow();
     // document.all("showworddiv").innerHTML="点击展开";



</script>

<asp:DataGrid ID="DataGrid1" runat="server">
</asp:DataGrid>
<asp:DataGrid ID="DataGrid2" runat="server">
</asp:DataGrid>
