<%@ Page language="c#" Inherits="RmsPM.Web.Document.DocumentList" CodeFile="DocumentList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>文档管理</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<script language="javascript" src="../Rms.js"></script>
		<script language="javascript">
	var v_showmenu = false;
	var Items=new Array();

		</script>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnAdd" onclick="AddDocument('')" type="button" value="新 增" name="btnAdd" runat="server">
					</td>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<table width="100%" height="100%">
							<tr>
								<td>
									<TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0" onkeydown="SearchKeyDown();">
										<tr>
											<td>
												<table>
													<TR>
													    <td>标题：<INPUT class="input" id="txtSearchTitle" type="text" size="20" name="txtSearchTitle" runat="server">
													        &nbsp;&nbsp;状态：<input id="chkStatus0" type="checkbox" value="1" name="chkStatus0" runat="server">待审&nbsp; 
															                                    &nbsp;<input id="chkStatus1" type="checkbox" value="1" name="chkStatus1" runat="server">已审&nbsp; 
                                                            <input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">
                                                            <img align="absMiddle" src="../images/search_more.gif" title="高级查询" style="CURSOR:hand"
							                                                            id="imgAdvSearch" onclick="ShowAdvSearch();">
													    </td>
													</TR>
												</table>
												<table style="DISPLAY:none" id="divAdvSearch">
	                                                <tr>
		                                                <td>编号：</td>
		                                                <td><INPUT class="input" id="txtSearchDocumentID" type="text" size="16" name="txtSearchDocumentID"
				                                                runat="server"></td>
		                                                <td>交档人：</td>
		                                                <td colspan="3"><INPUT class="input" id="txtSearchAuthor" type="text" size="16" name="txtSearchAuthor"
				                                                runat="server"></td>
	                                                </tr>
	                                                <tr>
		                                                <TD>创建人：</TD>
		                                                <TD><uc1:InputUser id="ucCreatePerson" runat="server"></uc1:InputUser></TD>
		                                                <td>创建日期：</td>
		                                                <TD><cc3:calendar id="dtCreateDate_begin" runat="server" CalendarResource="../Images/CalendarResource/"
				                                                ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
		                                                <td>至：</td>
		                                                <TD><cc3:calendar id="dtCreateDate_end" runat="server" CalendarResource="../Images/CalendarResource/"
				                                                ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
	                                                </tr>
	                                                <tr>
		                                                <TD>修改人：</TD>
		                                                <TD><uc1:InputUser id="ucModifyPerson" runat="server"></uc1:InputUser></TD>
		                                                <td>修改日期：</td>
		                                                <TD><cc3:calendar id="dtModifyDate_begin" runat="server" CalendarResource="../Images/CalendarResource/"
				                                                ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
		                                                <td>至：</td>
		                                                <TD><cc3:calendar id="dtModifyDate_end" runat="server" CalendarResource="../Images/CalendarResource/"
				                                                ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
	                                                </tr>
	                                                <tr>
		                                                <td>相关内容：</td>
		                                                <td colspan="5"><SELECT class="select" id="sltFixedType" name="sltFixedType" runat="server">
				                                                <OPTION value="" selected>------请选择------</OPTION>
			                                                </SELECT>
			                                                <INPUT class="input" id="txtCode" type="text" size="30" name="txtCode" runat="server"></td>
	                                                </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </TABLE>
								</td>
							</tr>
							<tr>
								<td valign="bottom" class="note" style="height: 21px"><asp:label id="lbTitle" runat="server" CssClass="TitleText"></asp:label><asp:label id="lblDocumentTypeName" runat="server" CssClass="Label"></asp:label></td>
							</tr>
							<tr height="100%">
				                <TD vAlign="top" align="left">
					                <DIV style="OVERFLOW: auto; position:absolute; WIDTH: 100%; HEIGHT: 100%">
						                <asp:datagrid id="dgList" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
							                DataKeyField="DocumentCode" PageSize="15" AllowPaging="True" CellPadding="0" CssClass="list" OnDeleteCommand="dgList_DeleteCommand">
							                <HeaderStyle CssClass="list-title"></HeaderStyle>
							                <FooterStyle CssClass="list-title"></FooterStyle>
							                <Columns>
								                <asp:TemplateColumn HeaderText="文档标题" SortExpression="Title">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <a href="#" onclick="ViewDocument(this.Code); return false;" Code='<%# DataBinder.Eval(Container.DataItem, "DocumentCode") %>'><%# DataBinder.Eval(Container.DataItem, "Title") %></a>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="编号" SortExpression="DocumentID">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# DataBinder.Eval(Container.DataItem, "DocumentID") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="状态" SortExpression="StatusName">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# DataBinder.Eval(Container.DataItem, "StatusName")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="类型" SortExpression="GroupFullID">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# DataBinder.Eval(Container.DataItem, "GroupFullName") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="正文">
									                <HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
									                <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container, "DataItem.MainText"), 20) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="附件">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# RmsPM.BLL.DocumentRule.Instance().GetAttachListHtml("DocumentAttach", DataBinder.Eval(Container.DataItem, "DocumentCode").ToString()) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:BoundColumn DataField="CreatePersonName" HeaderText="创建人" SortExpression="CreatePersonName">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="CreateDate" HeaderText="创建日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="CreateDate">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="ModifyPersonName" HeaderText="修改人" SortExpression="ModifyPersonName">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="ModifyDate" HeaderText="修改日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ModifyDate">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								           
								                <asp:BoundColumn DataField="FileKind" HeaderText="文件性质"  SortExpression="FileKind">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="Counts" HeaderText="页数/份数" SortExpression="Counts">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="FileDate" HeaderText="文件日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="FileDate">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="Author" HeaderText="交档人"  SortExpression="Author">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:TemplateColumn HeaderText="备注">
									                <HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
									                <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container, "DataItem.Remark"), 20) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:BoundColumn DataField="SavePlace" HeaderText="存放位置"  SortExpression="SavePlace">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;">
                                                </asp:ButtonColumn>
							                </Columns>
							                <PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
								                CssClass="ListHeadTr"></PagerStyle>
						                </asp:datagrid>&nbsp;
					                </DIV>
				                </TD>
							</tr>
							<tr>
								<td>
									<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange" IsPrintList="true" ></cc1:GridPagination>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<INPUT id="txtEvent_x" type="hidden" name="txtEvent_x" runat="server"><INPUT id="txtEvent_y" type="hidden" name="txtEvent_y" runat="server"><INPUT id="txtAct" type="hidden" runat="server" NAME="txtAct">
			<input type="hidden" runat="server" id="txtAdvSearch" name="txtAdvSearch" value="none"><input type="hidden" runat="server" id="txtStatus" name="txtStatus">
			<input type="hidden" runat="server" id="txtGroupCode" name="txtGroupCode"><input type="hidden" runat="server" id="txtGroupFullID" name="txtGroupFullID">
		</form>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
<!--

function ShowEditMenu(obj){
	var cssFile="../Images/ContentMenu.css";		
/*	Items[0]=new Array(2);
	Items[0][0]="修改科目";
	Items[0][1]="";
	Items[0][2]="ModifyUnit('"+obj.id+"');";
	Items[1]=new Array(2);
	Items[1][0]="删除科目";
	Items[1][1]="";
	Items[1][2]="RemoveUnit('"+obj.id+"');";
	Items[2]=new Array(2);
	Items[2][0]="新增下属部门";
	Items[2][1]="";
	Items[2][2]="InsertUnit('"+obj.id+"');";*/
	CreateContentMenu(Items,cssFile,Form1.txtEvent_x.value-1,Form1.txtEvent_y.value-1);
//	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

function RefreshDocument()
{
	window.location = window.location;
}

//新增
function AddDocument()
{
//			var Code = '合同编号';
//			OpenMiddleWindow("../Document/DocumentModify.aspx?DocumentTypeCode=000001&Code=" + Code + "&RefreshScript=RefreshDocument();"); 
    var GroupCode = '<%=Request.QueryString["GroupCode"]%>';
    var GroupCodeReadonly = '<%=Request.QueryString["GroupCodeReadonly"]%>';
	OpenCustomWindow("DocumentModify.aspx?GroupCode=" + GroupCode + "&GroupCodeReadonly=" + GroupCodeReadonly + "&RefreshScript=RefreshDocument();", "文档修改", 780, 560);
}
	
//查看
function ViewDocument(DocumentCode)
{
	var href = "";//window.parent.location.href;
    var GroupCodeReadonly = '<%=Request.QueryString["GroupCodeReadonly"]%>';
	OpenLargeWindow("DocumentInfo.aspx?FromUrl=" + escape(href) + "&DocumentCode=" + DocumentCode + "&GroupCodeReadonly=" + GroupCodeReadonly,'文档信息');
}

function ViewAttach(AttachmentCode) {
	var w = screen.width;
	var h = screen.height;
	window.open("AttachView.aspx?AttachmentCode=" + AttachmentCode, "" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=1,resizable=1,status:no;");
}
		
if (v_showmenu) {
  ShowEditMenu(this);
}	

//高级查询
function ShowAdvSearch()
{
	var display = Form1.txtAdvSearch.value;
	
	if ( display == "none" )
	{
		display = "block";
	}
	else
	{
		display = "none";
	}
	
	Form1.txtAdvSearch.value = display;
	
	SetAdvSearch();;
}

function SetAdvSearch()
{
	document.all("divAdvSearch").style.display = Form1.txtAdvSearch.value;

	if ( Form1.txtAdvSearch.value == "none" )
	{
//		Form1.imgAdvSearch.src = "../images/ArrowDown.gif";
		Form1.imgAdvSearch.title = "高级查询";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		Form1.imgAdvSearch.title = "隐藏高级查询";
	}
}

//搜索条件按回车
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		Form1.btnSearch.click();
	}
}

SetAdvSearch();

//-->
		</script>
	</body>
</HTML>
