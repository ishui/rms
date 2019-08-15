<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialOutList.aspx.cs" Inherits="Material_MaterialOutList" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>材料领用单</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
	<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript">
function OpenNew()
{    
    OpenLargeWindow('MaterialOutInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>','材料领用新增');
}
function OpenModify(Code)
{   
     OpenLargeWindow('MaterialOutInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&MaterialOutCode='+Code,'材料领用维护');
}
function doViewContractInfo(code,Projectcode)
{
	OpenFullWindow('../Contract/ContractInfo.aspx?ProjectCode='+Projectcode+'&ContractCode=' + code,'合同信息');
}
	
	//高级查询
function ShowAdvSearch()
{
	var display = form1.txtAdvSearch.value;
	if ( display == "none" )
	{
		display = "block";
	}
	else
	{
		display = "none";
	}
	
	form1.txtAdvSearch.value = display;
	
	SetAdvSearch();
}

function SetAdvSearch()
{

	document.all("divAdvSearch").style.display = form1.txtAdvSearch.value;

	if ( form1.txtAdvSearch.value == "none" )
	{
//		Form1.imgAdvSearch.src = "../images/ArrowDown.gif";
		form1.imgAdvSearch.title = "高级查询";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		form1.imgAdvSearch.title = "隐藏高级查询";
	}
}
</script>
</head>
<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no"  rightMargin="0" bgcolor="#ffffff">
    <form id="form1" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td height="25">
                    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
                                <span id="spanTitle"> 项目管理 > 材料管理 > 材料领用单</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
			<tr>
				<td class="tools-area" vAlign="top">
					<IMG src="../images/btn_li.gif" align="absMiddle"> 
					<input name="btnNew" id="btnNew" type="button" value=" 新增领料单 " class="button" runat="server" onclick="javascript:OpenNew();">
				</td>
			</tr>
            <tr>
                <td class="table" vAlign="top">
                <table height="100%" width="100%">
                <tr>
                   <td>
                    <table width="100%" class="search-area" cellspacing="0" cellpadding="0" border="0" onkeydown="SearchKeyDown();">
                        <tr>
                             <td>
                                  <table>
                                     <tr>
                                        <td nowrap>
                                            领料单号：</td>
                                        <td>
                                            <asp:TextBox ID="MaterialOutID" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td nowrap>
                                            领料类型：</td>
                                        <td>
                                        <uc1:InputSystemGroup id="inputSystemGroup" ClassCode="1505" Value='<%# Bind("groupcode") %>'  runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
                                            </td>
                                        <td nowrap><input class="submit" id="btnSearch" type="button" value="搜索" name="btnSearch" runat="server"
																onserverclick="btnSearch_ServerClick">
					                    &nbsp;<img src="../images/search_more.gif" title="高级查询" style="CURSOR:hand" id="imgAdvSearch" onclick="javascript:ShowAdvSearch();"></td>
                                      </tr>
                                     <tr>
                                        <td nowrap>
                                            调拨人：</td>
                                            
                                        <td><uc1:InputUser id="OutPerson" runat="server"></uc1:InputUser>
                                        </td>
                                        <td nowrap>
                                            领料日期：</td>
                                        <td><cc3:calendar id="OutDateStart" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar>
                                                     --&gt;
                                            <cc3:calendar id="OutDateEnd" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar>
                                                    </td>          
                                    </tr>
                                 </table>
                                 <table style="DISPLAY:none" id="divAdvSearch">  
                                    <tr>     
                                        <td nowrap>
                                            制单人：</td>
                                            
                                        <td><uc1:InputUser id="InputPerson" runat="server"></uc1:InputUser>
                                        </td>
                                        <td nowrap>
                                            制单日期：</td>
                                        <td><cc3:calendar id="InputDateStart" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar>
                                                     --&gt;
                                            <cc3:calendar id="InputDateEnd" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar>
                                                    </td>
                                    </tr>
                                </table>
                           </td>
                        </tr>
                    </table>
                   </td>
                 </tr> 
                <tr height="100%">
                    <td  valign="top">
                    <div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" AllowSorting="True" CssClass="list" PageSize="12" 
                            Width="100%" DataSourceID="ObjectDataSource1" GridLines="Horizontal">
                            <Columns>
                                <asp:TemplateField HeaderText="领料单号" SortExpression="MaterialOutID">
                                    <ItemTemplate>
                                        <a href="#" onclick="javascript:OpenModify('<%# Eval("MaterialOutCode") %>');return false;">
                                            <%# Eval("MaterialOutID")%>
                                        </a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="领用类型" SortExpression="GroupCode">
                                    <ItemTemplate>
                                            <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("GroupCode"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调拨人" SortExpression="Outperson">
                                    <ItemTemplate>
                                    <%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(Eval("Outperson")))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="领料日期" SortExpression="OutDate" >
                                    <ItemTemplate>
                                     <%# RmsPM.BLL.StringRule.ShowDate(Eval("OutDate"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="制单人" SortExpression="InputPerson">
                                    <ItemTemplate>
                                    <%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(Eval("InputPerson")))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="制单日期" SortExpression="InputDate">
                                    <ItemTemplate>
                                     <%# RmsPM.BLL.StringRule.ShowDate(Eval("InputDate"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="合同名称" SortExpression="ContractCode">
                                    <ItemTemplate>
                                    <a href="#" onclick="doViewContractInfo(this.code,this.Projectcode);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'
                                    Projectcode='<%#  DataBinder.Eval(Container.DataItem, "Projectcode") %>'>
                                    <%#  DataBinder.Eval(Container.DataItem, "ContractName") %>
                                    </a> 
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="list-title" />
                            <HeaderStyle CssClass="list-title" />
                            <EmptyDataTemplate>
                                无匹配数据
                            </EmptyDataTemplate>
                            <RowStyle HorizontalAlign="Center" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" TypeName="RmsPM.BFL.MaterialOutBFL" runat="server" DataObjectTypeName="TiannuoPM.MODEL.MaterialOutModel" OldValuesParameterFormatString="original_{0}" SelectMethod="GetMaterialOutList" UpdateMethod="Update"
                         OnSelected="ObjectDataSource1_Selected" EnablePaging="True"  MaximumRowsParameterName="MaxRecords" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" InsertMethod="Insert" DeleteMethod="Delete">
                   
                        <SelectParameters>
                            <asp:Parameter Name="SortColumns" Type="String" />
                            <asp:Parameter Name="StartRecord" Type="Int32" />
                            <asp:Parameter Name="MaxRecords" Type="Int32" />
                            <asp:Parameter Name="AccessRange" Type="String" />
                            <asp:Parameter Name="MaterialOutCodeEqual" Type="String" />
                            <asp:ControlParameter ControlID="MaterialOutID" Name="MaterialOutIDEqual" PropertyName="Text"
                                Type="String" />
                            <asp:QueryStringParameter Name="ProjectCodeEqual" QueryStringField="ProjectCode"
                                Type="String" />
                            <asp:ControlParameter ControlID="inputSystemGroup" Name="GroupCodeEqual" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="OutDateStart" Name="OutDateRange1" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="OutDateEnd" Name="OutDateRange2" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="OutPerson" Name="OutPersonEqual" PropertyName="Value"
                                Type="String" />
                            <asp:Parameter Name="StatusEqual" Type="String" />
                            <asp:ControlParameter ControlID="InputPerson" Name="InputPersonEqual" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="InputDateStart" Name="InputDateRange1" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="InputDateEnd" Name="InputDateRange2" PropertyName="Value"
                                Type="String" />
                            <asp:Parameter Name="CheckPersonEqual" Type="String" />
                            <asp:Parameter Name="CheckDateRange1" Type="String" />
                            <asp:Parameter Name="CheckDateRange2" Type="String" />
                            <asp:Parameter Name="ContractCodeEqual" Type="String" />
                            <asp:Parameter Name="RemarkEqual" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
		             <table width="100%" class="list">
			            <tr class="list-title">
				            <td style="height: 23px">
					            共
					            <asp:Label Runat="server" ID="lblRecordCount">0</asp:Label>
					            条
				            </td>
			            </tr>
		            </table>
                   </div> 
                    </td>
                </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
      </table>
      <input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
<script language="javascript">
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		form1.btnSearch.click();
	}
}
SetAdvSearch();
</script>
      </form>
     

</body>
</html>
