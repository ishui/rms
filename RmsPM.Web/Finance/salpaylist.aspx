<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Finance.SalPayList" CodeFile="SalPayList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SalPayList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<script language="javascript">
	function MakeVoucher(VoucherCode, param)
	{
//		var param = Form1.txtParam.value;
//		var RelaCode = Form1.txtSelect.value;
		OpenCustomWindow("../Finance/VoucherModify.aspx?VoucherCode=" + VoucherCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&Act=SalAdd&Param=" + param + "&RelaCodeSession=RelaCode" + "&RefreshScript=DoRefresh();","ƾ֤�޸�", 780, 580);
	}
	
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none">
				<input id="btnMakeVoucherHidden" type="button" name="btnMakeVoucherHidden" onclick="document.all.divHintSave.style.display='';"
					runat="server" onserverclick="btnMakeVoucherHidden_ServerClick">
			</div>
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">������� 
										- �������</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnBuildVoucher" onclick="BuildVoucher(true);" type="button"
							value="����ƾ֤" name="btnBuildVoucher" runat="server"> <input class="button" id="btnSelectVoucher" onclick="BuildVoucher(false);" type="button"
							value="����ƾ֤" name="btnSelectVoucher" runat="server"> <input class="button" id="btnMapSupl" onclick="MapSalSupl()" type="button" value="��Ӧ��Ӧ��"
							name="btnMapSupl" runat="server"> <input class="button" id="btnMapRoom" onclick="if (!MapRoom()) return false; " type="button"
							value="��Ӧ��Դ" name="btnMapRoom" runat="server" onserverclick="btnMapRoom_ServerClick"> <input class="button" id="btnImportSalOld" onclick="ImportSalOld()" type="button" value="��������������"
							name="btnImportSalOld" runat="server">
					</td>
				</TR>
				<tr height="100%">
					<td class="table" vAlign="top">
					    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
					        <tr>
					            <td>
						            <TABLE class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0" onkeydown="SearchKeyDown();">
							            <tr>
								            <td>
									            <table>
										            <TR>
											            <TD nowrap>��ͬ��ţ�</TD>
											            <TD nowrap><input class="input" id="txtSearchContractID" type="text" name="txtSearchContractID" runat="server"></TD>
											            <TD nowrap>״̬��</TD>
											            <TD nowrap><select class="select" id="sltSearchStatus" name="sltSearchStatus" runat="server">
													            <option value=" " selected>ȫ��</option>
													            <option value="0">δ����</option>
													            <option value="2">����ƾ֤</option>
												            </select>
											            </TD>
											            <TD nowrap>���ţ�</TD>
											            <TD nowrap><select class="select" id="sltSearchBuildingName" name="sltSearchBuildingName" runat="server">
													            <option value="" selected>-------��ѡ��-------</option>
												            </select>
											            </TD>
											            <TD nowrap vAlign="middle" align="right"><input class="submit" id="btnSearch" type="button" value="�� ��" name="btnSearch" runat="server"
													            onclick="document.all.divHintLoad.style.display='block';" onserverclick="btnSearch_ServerClick"></TD>
										            </TR>
										            <tr>
											            <TD nowrap>�տ����ڣ�</TD>
											            <TD nowrap><cc3:calendar id="dtSearchDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
													            ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
											            <TD nowrap>&nbsp;��&nbsp;��</TD>
											            <TD nowrap><cc3:calendar id="dtSearchDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
													            ReadOnly="False" Display="True" Value=""></cc3:calendar></TD>
											            <td nowrap colSpan="3"><asp:checkbox id="chkNotBalance" Text="����" Runat="server" Checked="True"></asp:checkbox><asp:checkbox id="chkBalance" Text="�۸񲹲�" Runat="server" Checked="True"></asp:checkbox></td>
										            </tr>
										            <tr>
										                <td colspan="7">�ͻ�������<input class="input" id="txtClientName" type="text" name="txtClientName" runat="server">
										                </td>
										            </tr>
									            </table>
								            </td>
							            </tr>
						            </TABLE>
					            </td>
					        </tr>
					        <tr height="100%">
					            <td valign="top">
						            <div style="OVERFLOW: auto; position:absolute; width:100%; HEIGHT: 100%">
							            <asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="0" GridLines="Horizontal"
								            PageSize="15" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" ShowFooter="True">
								            <HeaderStyle CssClass="list-title"></HeaderStyle>
								            <FooterStyle CssClass="list-title"></FooterStyle>
								            <Columns>
									            <asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
										            <HeaderStyle HorizontalAlign="Center" Wrap="false"></HeaderStyle>
										            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										            <ItemTemplate>
											            <input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.PayCode")%>'>
										            </ItemTemplate>
									            </asp:TemplateColumn>
									            <asp:TemplateColumn HeaderText="���" Visible="false">
										            <HeaderStyle HorizontalAlign="Center" Wrap="false"></HeaderStyle>
										            <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
										            <ItemTemplate>
											            <%# Container.ItemIndex + 1%>
										            </ItemTemplate>
									            </asp:TemplateColumn>
									            <asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="��ͬ���" FooterText="�ϼ�">
										            <HeaderStyle Wrap="False"></HeaderStyle>
										            <ItemStyle Wrap="False"></ItemStyle>
										            <ItemTemplate>
											            <a href="#" onclick="ViewSalContract(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'><%#  DataBinder.Eval(Container.DataItem, "ContractID") %></a>
										            </ItemTemplate>
									            </asp:TemplateColumn>
									            <asp:BoundColumn DataField="ClientName" HeaderText="�ͻ�����">
										            <HeaderStyle Wrap="False"></HeaderStyle>
										            <ItemStyle Wrap="False"></ItemStyle>
									            </asp:BoundColumn>
									            <asp:BoundColumn DataField="BuildingName" HeaderText="����" Visible="False">
										            <HeaderStyle Wrap="False"></HeaderStyle>
										            <ItemStyle Wrap="False"></ItemStyle>
									            </asp:BoundColumn>
									            <asp:BoundColumn DataField="PayDate" HeaderText="�տ�����" DataFormatString="{0:yyyy-MM-dd}">
										            <HeaderStyle Wrap="False"></HeaderStyle>
										            <ItemStyle Wrap="false"></ItemStyle>
									            </asp:BoundColumn>
									            <asp:BoundColumn DataField="PayMoney" HeaderText="�տ���(Ԫ)" DataFormatString="{0:n}">
										            <HeaderStyle HorizontalAlign="Right" Wrap="false"></HeaderStyle>
										            <ItemStyle HorizontalAlign="Right" Wrap="false"></ItemStyle>
										            <FooterStyle HorizontalAlign="Right"></FooterStyle>
									            </asp:BoundColumn>
									            <asp:TemplateColumn HeaderText="����">
										            <HeaderStyle HorizontalAlign="Left" Wrap="false"></HeaderStyle>
										            <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
										            <ItemTemplate>
											            <%# RmsPM.BLL.SalRule.GetSalPayItemName(DataBinder.Eval(Container, "DataItem.PayCode").ToString(), RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.ContractCode")))%>
										            </ItemTemplate>
									            </asp:TemplateColumn>
									            <asp:BoundColumn DataField="CheckDate" HeaderText="�տ��������" DataFormatString="{0:yyyy-MM-dd}">
										            <HeaderStyle Wrap="False"></HeaderStyle>
										            <ItemStyle Wrap="false"></ItemStyle>
									            </asp:BoundColumn>
									            <asp:BoundColumn DataField="SuplName" HeaderText="��Ӧ��">
										            <HeaderStyle Wrap="false"></HeaderStyle>
										            <ItemStyle Wrap="false"></ItemStyle>
									            </asp:BoundColumn>
									            <asp:TemplateColumn HeaderText="ƾ֤��">
										            <HeaderStyle Wrap="false"></HeaderStyle>
										            <ItemStyle Wrap="false"></ItemStyle>
										            <ItemTemplate>
											            <a href="#" onclick="GotoVoucher(this.val);" val='<%# DataBinder.Eval(Container, "DataItem.VoucherCode").ToString() %>'>
												            <%# RmsPM.BLL.PaymentRule.GetVoucherName(DataBinder.Eval(Container, "DataItem.VoucherCode").ToString())%>
											            </a>
										            </ItemTemplate>
									            </asp:TemplateColumn>
									            <asp:TemplateColumn HeaderText="״̬">
										            <HeaderStyle Wrap="False"></HeaderStyle>
										            <ItemStyle Wrap="False"></ItemStyle>
										            <ItemTemplate>
											            <%# DataBinder.Eval(Container,"DataItem.VoucherCode").ToString()==""?"δ����":"����ƾ֤"%>
										            </ItemTemplate>
									            </asp:TemplateColumn>
								            </Columns>
								            <PagerStyle Visible="false" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
									            CssClass="ListHeadTr"></PagerStyle>
							            </asp:datagrid>
						            </div>
					            </td>
					        </tr>
				            <tr>
					            <td>
						            <cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../Images/GridPaginationSource/" DataGridId="dgList" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination>
					            </td>
				            </tr>
					    </table>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</TABLE>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtSelect" type="hidden" name="txtSelect" runat="server">
			<input id="txtParam" type="hidden" name="txtParam" runat="server"><input id="txtVoucherCode" type="hidden" name="txtVoucherCode" runat="server">
			<div style="DISPLAY: none"><input id="btnBatchHidden" type="button" name="btnBatchHidden" runat="server"></div>
		</form>
		<script language="javascript">
<!--
//����ƾ֤
	function BuildVoucher(isNew)
	{
		var s = ChkGetSelected(document.all.chkSelect);
			
		if (s == "")
		{
			alert('��ѡ��һ���������¼');
			return false;
		}
		
		Form1.txtSelect.value = s;

		var param;
//		param = Form1.txtParam.value;

		if ((Form1.chkBalance.checked) && (!Form1.chkNotBalance.checked))
		{
			param = 1;
		}
		else
		{
			param = 0;
		}
		
		Form1.txtParam.value = param;
		
		if (isNew)
		{
			Form1.txtVoucherCode.value = "";
			Form1.btnMakeVoucherHidden.click();
//			OpenCustomWindow("../Finance/VoucherModify.aspx?Act=SalAdd&ProjectCode=" + Form1.txtProjectCode.value + "&Param=" + param + "&RelaCode=" + s + "&RefreshScript=DoRefresh();","ƾ֤�޸�", 780, 580);
		}
		else
		{
			OpenMiddleWindow("../Finance/SelectVoucher.aspx?Act=SalAdd&ProjectCode=" + Form1.txtProjectCode.value,"ѡ��ƾ֤");
		}

//		document.all.btnBatchHidden.click();
//		return true;
	}
	
	//����ƾ֤
	function SelectVoucherReturn(VoucherCode)
	{
		var param = Form1.txtParam.value;
		var RelaCode = Form1.txtSelect.value;
		Form1.txtVoucherCode.value = VoucherCode;
		Form1.btnMakeVoucherHidden.click();
//		OpenCustomWindow("../Finance/VoucherModify.aspx?VoucherCode=" + VoucherCode + "&ProjectCode=" + Form1.txtProjectCode.value + "&Act=SalAdd&Param=" + param + "&RelaCode=" + RelaCode + "&RefreshScript=DoRefresh();","ƾ֤�޸�", 780, 580);
	}
	
	//�鿴���ۺ�ͬ
	function ViewSalContract(ContractCode)
	{
		OpenCustomWindow("../Sal/SalContractView.aspx?Action=view&FromUrl=" + escape(window.location.href) + "&ContractCode=" + ContractCode, "��ͬ��ϸ", 650, 560);
	}
	
	//��Ӧ��Ӧ��
	function MapSalSupl()
	{
		OpenCustomWindow("../Sal/MapSalSupl.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&RefreshScript=DoRefresh();", "��Ӧ��Ӧ��", 400, 350);
	}
	
	//��Ӧ��Դ
	function MapRoom()
	{
		if (!confirm("�����������Ӧ����Ŀϵͳ��Դ���Ƿ������")) return false;
		document.all.divHintSave.style.display = "";
		return true;
	}
	
	//������������������
	function ImportSalOld()
	{
		OpenCustomWindow("../Sal/ImportSalOldDlg.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&RefreshScript=DoRefresh();","������������", 400, 300);
	}
	
	//�鿴ƾ֤
	function GotoVoucher(VoucherCode)
	{
		OpenCustomWindow("../Finance/VoucherInfo.aspx?VoucherCode=" + VoucherCode + "&Open=1", "ƾ֤��Ϣ", 760, 540);
	}
	
	//ˢ��
	function DoRefresh()
	{
		Form1.btnSearch.click();
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

//-->
		</script>
	</body>
</HTML>
