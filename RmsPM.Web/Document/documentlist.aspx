<%@ Page language="c#" Inherits="RmsPM.Web.Document.DocumentList" CodeFile="DocumentList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ĵ�����</title>
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
						<input class="button" id="btnAdd" onclick="AddDocument('')" type="button" value="�� ��" name="btnAdd" runat="server">
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
													    <td>���⣺<INPUT class="input" id="txtSearchTitle" type="text" size="20" name="txtSearchTitle" runat="server">
													        &nbsp;&nbsp;״̬��<input id="chkStatus0" type="checkbox" value="1" name="chkStatus0" runat="server">����&nbsp; 
															                                    &nbsp;<input id="chkStatus1" type="checkbox" value="1" name="chkStatus1" runat="server">����&nbsp; 
                                                            <input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">
                                                            <img align="absMiddle" src="../images/search_more.gif" title="�߼���ѯ" style="CURSOR:hand"
							                                                            id="imgAdvSearch" onclick="ShowAdvSearch();">
													    </td>
													</TR>
												</table>
												<table style="DISPLAY:none" id="divAdvSearch">
	                                                <tr>
		                                                <td>��ţ�</td>
		                                                <td><INPUT class="input" id="txtSearchDocumentID" type="text" size="16" name="txtSearchDocumentID"
				                                                runat="server"></td>
		                                                <td>�����ˣ�</td>
		                                                <td colspan="3"><INPUT class="input" id="txtSearchAuthor" type="text" size="16" name="txtSearchAuthor"
				                                                runat="server"></td>
	                                                </tr>
	                                                <tr>
		                                                <TD>�����ˣ�</TD>
		                                                <TD><uc1:InputUser id="ucCreatePerson" runat="server"></uc1:InputUser></TD>
		                                                <td>�������ڣ�</td>
		                                                <TD><cc3:calendar id="dtCreateDate_begin" runat="server" CalendarResource="../Images/CalendarResource/"
				                                                ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
		                                                <td>����</td>
		                                                <TD><cc3:calendar id="dtCreateDate_end" runat="server" CalendarResource="../Images/CalendarResource/"
				                                                ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
	                                                </tr>
	                                                <tr>
		                                                <TD>�޸��ˣ�</TD>
		                                                <TD><uc1:InputUser id="ucModifyPerson" runat="server"></uc1:InputUser></TD>
		                                                <td>�޸����ڣ�</td>
		                                                <TD><cc3:calendar id="dtModifyDate_begin" runat="server" CalendarResource="../Images/CalendarResource/"
				                                                ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
		                                                <td>����</td>
		                                                <TD><cc3:calendar id="dtModifyDate_end" runat="server" CalendarResource="../Images/CalendarResource/"
				                                                ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
	                                                </tr>
	                                                <tr>
		                                                <td>������ݣ�</td>
		                                                <td colspan="5"><SELECT class="select" id="sltFixedType" name="sltFixedType" runat="server">
				                                                <OPTION value="" selected>------��ѡ��------</OPTION>
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
								                <asp:TemplateColumn HeaderText="�ĵ�����" SortExpression="Title">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <a href="#" onclick="ViewDocument(this.Code); return false;" Code='<%# DataBinder.Eval(Container.DataItem, "DocumentCode") %>'><%# DataBinder.Eval(Container.DataItem, "Title") %></a>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="���" SortExpression="DocumentID">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# DataBinder.Eval(Container.DataItem, "DocumentID") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="״̬" SortExpression="StatusName">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# DataBinder.Eval(Container.DataItem, "StatusName")%>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="����" SortExpression="GroupFullID">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# DataBinder.Eval(Container.DataItem, "GroupFullName") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="����">
									                <HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
									                <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container, "DataItem.MainText"), 20) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:TemplateColumn HeaderText="����">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# RmsPM.BLL.DocumentRule.Instance().GetAttachListHtml("DocumentAttach", DataBinder.Eval(Container.DataItem, "DocumentCode").ToString()) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:BoundColumn DataField="CreatePersonName" HeaderText="������" SortExpression="CreatePersonName">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="CreateDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}" SortExpression="CreateDate">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="ModifyPersonName" HeaderText="�޸���" SortExpression="ModifyPersonName">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="ModifyDate" HeaderText="�޸�����" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ModifyDate">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								           
								                <asp:BoundColumn DataField="FileKind" HeaderText="�ļ�����"  SortExpression="FileKind">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="Counts" HeaderText="ҳ��/����" SortExpression="Counts">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="FileDate" HeaderText="�ļ�����" DataFormatString="{0:yyyy-MM-dd}" SortExpression="FileDate">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:BoundColumn DataField="Author" HeaderText="������"  SortExpression="Author">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
								                <asp:TemplateColumn HeaderText="��ע">
									                <HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>
									                <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
									                <ItemTemplate>
										                <%# RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container, "DataItem.Remark"), 20) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								                <asp:BoundColumn DataField="SavePlace" HeaderText="���λ��"  SortExpression="SavePlace">
									                <HeaderStyle Wrap="False"></HeaderStyle>
									                <ItemStyle  Wrap="False"></ItemStyle>
								                </asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;">
                                                </asp:ButtonColumn>
							                </Columns>
							                <PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
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
	Items[0][0]="�޸Ŀ�Ŀ";
	Items[0][1]="";
	Items[0][2]="ModifyUnit('"+obj.id+"');";
	Items[1]=new Array(2);
	Items[1][0]="ɾ����Ŀ";
	Items[1][1]="";
	Items[1][2]="RemoveUnit('"+obj.id+"');";
	Items[2]=new Array(2);
	Items[2][0]="������������";
	Items[2][1]="";
	Items[2][2]="InsertUnit('"+obj.id+"');";*/
	CreateContentMenu(Items,cssFile,Form1.txtEvent_x.value-1,Form1.txtEvent_y.value-1);
//	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

function RefreshDocument()
{
	window.location = window.location;
}

//����
function AddDocument()
{
//			var Code = '��ͬ���';
//			OpenMiddleWindow("../Document/DocumentModify.aspx?DocumentTypeCode=000001&Code=" + Code + "&RefreshScript=RefreshDocument();"); 
    var GroupCode = '<%=Request.QueryString["GroupCode"]%>';
    var GroupCodeReadonly = '<%=Request.QueryString["GroupCodeReadonly"]%>';
	OpenCustomWindow("DocumentModify.aspx?GroupCode=" + GroupCode + "&GroupCodeReadonly=" + GroupCodeReadonly + "&RefreshScript=RefreshDocument();", "�ĵ��޸�", 780, 560);
}
	
//�鿴
function ViewDocument(DocumentCode)
{
	var href = "";//window.parent.location.href;
    var GroupCodeReadonly = '<%=Request.QueryString["GroupCodeReadonly"]%>';
	OpenLargeWindow("DocumentInfo.aspx?FromUrl=" + escape(href) + "&DocumentCode=" + DocumentCode + "&GroupCodeReadonly=" + GroupCodeReadonly,'�ĵ���Ϣ');
}

function ViewAttach(AttachmentCode) {
	var w = screen.width;
	var h = screen.height;
	window.open("AttachView.aspx?AttachmentCode=" + AttachmentCode, "" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=1,resizable=1,status:no;");
}
		
if (v_showmenu) {
  ShowEditMenu(this);
}	

//�߼���ѯ
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
		Form1.imgAdvSearch.title = "�߼���ѯ";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		Form1.imgAdvSearch.title = "���ظ߼���ѯ";
	}
}

//�����������س�
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
