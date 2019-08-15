<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutInfo" CodeFile="PayoutInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/inputExchangeRate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">������� 
										- �����Ϣ</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle" />
						<input class="button" id="btnModify" onclick="Modify();" type="button" value="�� ��" name="btnModify" runat="server" />
						<input class="button" id="btnModifyEx" style="DISPLAY: none" onclick="Modify();" type="button"	value="��˺��޸�" name="btnModifyEx" runat="server" />
						<input class="button" id="btnCheckDelete" value="��˺�ɾ��" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;" type="button" name="btnCheckDelete" onserverclick="btnCheckDelete_ServerClick" style="DISPLAY:none" runat="server">
						<input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;" type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick" />
						<input class="button" id="btnOldCheck" onclick="javascript:DoOldCheck();" type="button" value="�� ��" name="btnOldCheck" runat="server" /> 
						<input class="button" id="btnCheck" onclick="javascript:DoCheck();" type="button" value="�ύ" name="btnCheck" runat="server" /> 
						<input class="button" id="btnBuildVoucher" onclick="BuildVoucher(true);" type="button"	value="����ƾ֤" name="btnBuildVoucher" runat="server" style="DISPLAY:none" /> 
						<input class="button" id="btnSelectVoucher" onclick="BuildVoucher(false);" type="button" value="����ƾ֤" name="btnSelectVoucher" runat="server" style="DISPLAY:none" /> 
						<input class="button" id="btnApportion" onclick="doApportionPayout();" type="button" value="�� ̯"	name="btnApportion" runat="server" style="DISPLAY:none" /> 
						<input class="button" id="btnGoBack" onclick="GoBack();" type="button" value="�� ��" name="btnGoBack" />
					</td>
				</tr>
				<tr>
					<td class="table">
						<table class="form" id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="form-item">����ţ�</td>
								<td><asp:label id="lblPayoutID" runat="server"></asp:label>
									<asp:label id="lblApportion" runat="server" ForeColor="Red" Font-Bold="True"></asp:label></td>
								<td class="form-item">״ ̬��</td>
								<td><asp:label id="lblStatusName" Runat="server"></asp:label></td>
								<td class="form-item">����������</td>
								<td><asp:label id="lblReceiptCount" runat="server"></asp:label></td>

							</tr>
							<tr>
								<td class="form-item">ϵͳ���ͣ�</td>
								<td><asp:label id="lblGroupCodeName" runat="server"></asp:label></td>
								<td class="form-item">�ܿλ��</td>
								<td colspan="3"><asp:label id="lblSupplyName" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="form-item">�� �� �ˣ�</td>
								<td><asp:label id="lblInputPersonName" runat="server"></asp:label></td>
								<td class="form-item">�������ڣ�</td>
								<td><asp:label id="lblPayoutDate" runat="server"></asp:label></td>
								<td class="form-item">�������ͣ�</td>
								<td><asp:label id="lblPaymentType" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="form-item">������Ŀ��</td>
								<td colspan="3"><asp:label id="lblSubjectName" runat="server"></asp:label></td>
								<td class="form-item">�� �� �ˣ�</td>
								<td><asp:label id="lblPayer" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="form-item">Ʊ �� �ţ�</td>
								<td><asp:label id="lblBillNo" runat="server"></asp:label></td>
								<td class="form-item">�� Ʊ �ţ�</td>
								<td><asp:label id="lblInvoNo" runat="server"></asp:label></td>
								<td class="form-item">ƾ֤�ţ�</td>
								<td><a style="CURSOR:hand" onclick="javascript:GotoVoucher(this.val)" id="lblVoucherID"
										runat="server"></a></td>
							</tr>
							<tr>
								<td class="form-item">�� �� �ˣ�</td>
								<td><asp:label id="lblCheckPersonName" runat="server"></asp:label></td>
								<td class="form-item">������ڣ�</td>
								<td colspan="3"><asp:label id="lblCheckDate" runat="server"></asp:label></td>
							</tr>
							<tr>
							    <td class="form-item">�����ܶ</td>
							    <td colspan="5">
							        <uc1:ExchangeRate ID="ucExchangeRate" runat="server" />
							    </td>
							</tr>
							
							<tr style="DISPLAY:none">
								<td class="form-item">��������</td>
								<td colSpan="5"><asp:label id="lblCheckOpinion" runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
					    <div style="overflow: auto; width: 100%; position: absolute; height: 100%">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic" width="200">������ϸ</td>
									<td><input class="button-small" style="display:none" onclick="BatchApportion()" type="button" value="������̯" name="btnBatchApportion"></td>
								</tr>
							</table>
							<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<tr>
									<td vAlign="top">
									    
					                    <asp:GridView ID="gvPayoutItem" runat="server" DataKeyNames="PayoutItemCode" CellPadding="0" AllowSorting="True"
							                GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="list"
							                OnRowDataBound="gvPayoutItem_RowDataBound">
							                <HeaderStyle Wrap="False" CssClass="list-title" HorizontalAlign="Center" />
							                <RowStyle  Wrap="False" HorizontalAlign="Center" />
							                <FooterStyle Wrap="False" CssClass="list-title" HorizontalAlign="Right"/>
							                <Columns>
								                <asp:TemplateField HeaderText="���" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
									                <ItemTemplate>
										                <%# Container.DataItemIndex + 1 %>
									                </ItemTemplate>
								                </asp:TemplateField>
												<asp:TemplateField HeaderText="��λ����">
													<HeaderStyle HorizontalAlign="Left" Wrap="false"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateField>
	                                            <asp:TemplateField HeaderText="����" ItemStyle-Wrap="false" FooterText="�ϼƣ�" FooterStyle-Wrap="false">
									                <ItemTemplate>
										                <%# DataBinder.Eval(Container, "DataItem.Summary") %>
										                <input type="hidden" id="txtPayoutItemCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PayoutItemCode") %>' />
										                <input type="hidden" id="txtPaymentItemCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentItemCode") %>' />
										                <input type="hidden" id="txtSummary" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.Summary") %>' />
										                <input type="hidden" id="txtTotalPayoutCash" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.TotalPayoutCash") %>' />
										                <input type="hidden" id="txtRemainItemCash" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.RemainItemCash") %>' />
                                                        <input type="hidden" id="txtItemMoney" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.ItemMoney") %>' />
                                                        <input type="hidden" id="txtPaymentMoneyType" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentMoneyType") %>' />
                                                        <input type="hidden" id="txtPaymentExchangeRate" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentExchangeRate") %>' />
									                </ItemTemplate>
								                </asp:TemplateField>
								                <asp:TemplateField HeaderText="�����" ItemStyle-Wrap="false">
								                    <ItemTemplate>
								                        <asp:Label ID="lblPaymentMoneyType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentMoneyType") %>'></asp:Label>��&nbsp;&nbsp;
								                        <asp:Label ID="lblPaymentCash" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash","{0:N}") %>'></asp:Label>
								                    </ItemTemplate>
								                </asp:TemplateField>
								                <asp:BoundField HeaderText="�Ѹ����<br />��ԭ���֣�" DataField="TotalPayoutCash" DataFormatString="{0:N}"  ItemStyle-Wrap="false"  HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"/>
								                <asp:BoundField HeaderText="δ�����<br />��ԭ���֣�" DataField="RemainItemCash" DataFormatString="{0:N}" ItemStyle-Wrap="false"  HtmlEncode="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"/>
								                <asp:BoundField HeaderText="���θ�����<br />��ԭ���֣�" DataField="PayoutCash" DataFormatString="{0:N}" ItemStyle-Wrap="false"  HtmlEncode="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"/>
                                                <asp:TemplateField HeaderText="����"  ItemStyle-Wrap="false">
								                    <ItemTemplate>
								                        <asp:Label ID="lblPayoutExchangeRate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ExchangeRate","{0:N}") %>'></asp:Label>
					        	                    </ItemTemplate>
								                </asp:TemplateField>
								                <asp:TemplateField HeaderText="����֧�����<br>��RMB��"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
								                    <ItemTemplate>
								                        <asp:Label ID="lblPayoutItemMoney" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PayoutMoney","{0:N}") %>' ></asp:Label>
								                    </ItemTemplate>
								                </asp:TemplateField>
								                <asp:BoundField HeaderText="��Ŀ" DataField="SubjectName" ItemStyle-Wrap="false"  HtmlEncode="false" HeaderStyle-Wrap="false" />
								                <asp:TemplateField HeaderText="������"  ItemStyle-Wrap="false">
													<HeaderStyle Wrap="false"></HeaderStyle>
									                <ItemTemplate>
										                <span id="divCostName" runat="server">
										                    <a href="#" onclick="ViewCostCode(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "CostCode") %>'>
												                <%# DataBinder.Eval(Container, "DataItem.CostName") %>
											                </a>
										                </span>
										                <input type="hidden" runat="server" id="txtCostCode" value='<%#DataBinder.Eval(Container, "DataItem.CostCode")%>' />
										                <input type="hidden" runat="server" id="txtCostName" value='<%#DataBinder.Eval(Container, "DataItem.CostName")%>' />
									                </ItemTemplate>
								                </asp:TemplateField>
								                <asp:TemplateField HeaderText="����"  ItemStyle-Wrap="false">
									                <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
									                <ItemStyle Wrap="False"></ItemStyle>
									                <ItemTemplate>
                                                        <a style="cursor:hand" onclick="javascript:GotoPayment(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "PaymentCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.PaymentID") %>
														</a>									                
										                <input type="hidden" id="txtPaymentCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentCode") %>' />
										                <input type="hidden" id="txtPaymentID" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentID") %>' />
									                </ItemTemplate>
								                </asp:TemplateField>
							                </Columns>
					                    </asp:GridView>
                                    </td>
								</tr>
							</table>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="intopic" width="200">��̯��ϸ</td>
								</tr>
							</table>
							<asp:datagrid id="dgGridApportion" runat="server" CellPadding="0" AllowSorting="True" GridLines="Both"
								AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="List">
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="���">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="��̯��ʽ" FooterText="�ϼ�">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.AlloTypeName") %>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="¥������">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.BuildingName") %>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="��̯���(Ԫ)">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "DataItem.ApportionMoney", "{0:N}") %>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
						</div>
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
			</table>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<INPUT id="txtPayoutCode" type="hidden" name="txtPayoutCode" runat="server"> <INPUT id="txtStatus" type="hidden" name="txtStatus" runat="server">
			<INPUT id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server">
		</form>
		<script language="javascript">
<!--

	//����
	function GoBack()
	{
		if (Form1.txtFromUrl.value == "")
		{
			window.history.go(-1);
	//		window.location.href = "PayoutList.aspx?ProjectCode=" + Form1.txtProjectCode.value;
		}
		else
		{
			window.history.go(-1);
	//		window.location.href = Form1.txtFromUrl.value;
		}
	}

	//�޸�
	function Modify()
	{
		var PayoutCode = Form1.txtPayoutCode.value;
		
		OpenFullWindow("../Finance/PayoutDetailModify.aspx?PayoutCode=" + PayoutCode, "����޸�", 780, 560);
	}

	function doApportionPayout()
	{
		var PayoutCode = Form1.txtPayoutCode.value;
		OpenCustomWindow("../Finance/PayoutDetailApportion.aspx?PayoutCodes=" + PayoutCode, "�����̯", 780, 560);
	}

	function doApportionPayoutItem(payoutItemCode)
	{
		OpenCustomWindow("../Finance/PayoutDetailApportion.aspx?PayoutItemCodes=" + payoutItemCode, "�����̯", 780, 560);
	}
		
	//������̯
	function BatchApportion()
	{
		var s = ChkGetSelected(document.all.chkSelect);

		if (s == "")
		{
			alert('��ѡ��һ���������¼');
			return false;
		}
		
		OpenCustomWindow("../Finance/PayoutDetailApportion.aspx?PayoutItemCodes=" + s, "�����̯", 780, 560);
	}
	
	//���
	function DoOldCheck()
	{
		var PayoutCode = Form1.txtPayoutCode.value;
		OpenCustomWindow('../Finance/PayoutCheckInput.aspx?PayoutCode=' + PayoutCode,"������", 780, 560);
//		OpenCustomWindow('../Finance/PayoutCheck.aspx?PayoutCode=' + PayoutCode,"������", 300, 160);
	}

	function DoCheck()
	{
		OpenFullWindow('<%=ViewState["_AuditingURL"]%>?PayoutCode=<%=Request["PayoutCode"]%>&ApplicationCode1=<%=Request["PayoutCode"]%>','������_<%=Request["PayoutCode"]%>');
	}
	
	//����ƾ֤
	function BuildVoucher(isNew)
	{
		var RelaCode = Form1.txtPayoutCode.value;

		if (isNew)
		{
			OpenCustomWindow("../Finance/VoucherModify.aspx?Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value + "&RelaCode=" + RelaCode,"ƾ֤�޸�", 780, 580);
		}
		else
		{
			OpenMiddleWindow("../Finance/SelectVoucher.aspx?Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value,"ѡ��ƾ֤");
		}

	//		document.all.btnBatchHidden.click();
	//		return true;
	}

		//����ƾ֤
	function SelectVoucherReturn(VoucherCode)
	{
		var RelaCode = Form1.txtPayoutCode.value;
		OpenCustomWindow("../Finance/VoucherModify.aspx?VoucherCode=" + VoucherCode + "&Act=PayAdd&ProjectCode=" + Form1.txtProjectCode.value + "&RelaCode=" + RelaCode,"ƾ֤�޸�", 780, 580);
	}

	//�鿴ƾ֤
	function GotoVoucher(VoucherCode)
	{
		OpenCustomWindow("../Finance/VoucherInfo.aspx?VoucherCode=" + VoucherCode + "&Open=1", "ƾ֤��Ϣ", 760, 540);
	}

	//��������Ϣ
	function ViewCostCode(code)
	{
		OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=' + Form1.txtProjectCode.value + '&CostCode=' + code ,'��̬��������Ϣ');
	}

	//�鿴��
	function GotoPayment(PaymentCode)
	{
		OpenCustomWindow("../Finance/PaymentInfo.aspx?PaymentCode=" + PaymentCode + "&Open=1", "��", 760, 540);
	}

	//-->
		</script>
	</body>
</HTML>
