<%@ Page language="c#" Inherits="RmsPM.Web.Material.ImportMaterialCostDlg" CodeFile="ImportMaterialCostDlg.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������ϼ۸�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">

function winload()
{
	ShowStepPanel();
}

function ShowStepPanel()
{
	var step = parseInt(Form1.txtStep.value);
	
	//ҳ����ʾ
	for(var i=1;i<=9;i++)
	{
		var pnId = "pnStep" + i;
		if (document.all(pnId))
			document.all(pnId).style.display = "none";
	}
	
	var pnId = "pnStep" + step;
	if (document.all(pnId))
		document.all(pnId).style.display = "";
		
	//��ť��ʾ
	Form1.btnPrior.style.display = "none";
	Form1.btnNext.style.display = "none";
	Form1.btnComplete.style.display = "none";
	Form1.btnContinue.style.display = "none";
	Form1.btnClear.style.display = "none";
	
	if (step == 1)
	{
		Form1.btnNext.style.display = "";
		Form1.btnClear.style.display = "";
	}
	else if (step == 2) //���һ��
	{
		Form1.btnPrior.style.display = "";
		Form1.btnComplete.style.display = "";
	}
	else if (step == 9) //�������
	{
		Form1.btnContinue.style.display = "";
	}
	else
	{
		Form1.btnPrior.style.display = "";
		Form1.btnNext.style.display = "";
	}
}

		</script>
	</HEAD>
	<body scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">������ϼ۸�</td>
				</tr>
				<tr align="center">
					<td style="COLOR: blue">�������ݽ����������ϼ۸��</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table width="100%" height="100%" id="pnStep1" style="DISPLAY:none">
							<tr>
								<td valign="top">
									<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="form-item" width="60">�ļ���</td>
											<td><input class="textbox" id="txtFile" type="file" style="WIDTH:100%" size="45" name="txtFile"
													runat="server"></td>
										</tr>
									</TABLE>
									<br>
									<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0">
										<tr>
											<td>�ļ���ʽ˵����<br>
												1.�ļ����ͱ�����csv�����ŷָ���<br>
												2.�ļ��ĵ�1��Ϊ�����У��Ժ�Ϊ�����С�<br>
												3.����������Ϊ��<br>
												&nbsp;&nbsp;<asp:Label Runat="server" ID="lblFieldDesc" ForeColor="blue"></asp:Label><br>
												4.�����ȫ�����á�-&gt;���ָ����磺����-&gt;���磬�ұ����ϵͳ����еĲ��ϼ۸�����Ӧ
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
						</table>
						<table width="100%" height="100%" id="pnStep2" cellpadding="0" cellspacing="0" style="DISPLAY:none">
							<tr>
								<td valign="bottom">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">Ԥ������ϼ۸��б�</td>
											<td><input type="button" runat="server" class="button-small" id="btnDownloadCsv" name="btnDownloadCsv"
													value="���Ϊcsv" onserverclick="btnDownloadCsv_ServerClick"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td valign="top">
									<div style="OVERFLOW:auto;WIDTH:100%;POSITION:absolute;HEIGHT:100%">
										<asp:datagrid id="dgList" runat="server" AllowSorting="True" AutoGenerateColumns="False" PageSize="16"
											AllowPaging="False" GridLines="Horizontal" CellPadding="0" Width="100%" CssClass="list" EnableViewState="True">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="�к�" SortExpression="rowid" ItemStyle-HorizontalAlign="Right">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "rowid") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Ԥ����<br>���" SortExpression="ImportResultName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<font color='<%# GetImportResultColor(DataBinder.Eval(Container.DataItem, "ImportResult"))%>'>
															<%# DataBinder.Eval(Container.DataItem, "ImportResultName") %>
															<font>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����" SortExpression="GroupCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "GroupFullName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��λ" SortExpression="Unit">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Unit")%>
														<font color="red">
															<%# DataBinder.Eval(Container.DataItem, "ImportHint") %>
														</font>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����" SortExpression="Price">
													<HeaderStyle Wrap="False" HorizontalAlign="right"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Price")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��Ŀ" SortExpression="Project">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Project")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��������" SortExpression="BiddingDate">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "BiddingDate", "{0:yyyy-MM-dd}")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����" SortExpression="AreaCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "AreaCode") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����" SortExpression="Description">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container.DataItem, "Description"), 20)%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="description" SortExpression="DescriptionEn">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.StringRule.TruncateString(DataBinder.Eval(Container.DataItem, "DescriptionEn"), 20)%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="category" SortExpression="category">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "category")%>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<table cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td align="right" width="80">���� -
												<asp:Label Runat="server" ID="lblCountAdd" ForeColor="blue">0</asp:Label></td>
											<td align="right" width="80" style="display:none">�޸� -
												<asp:Label Runat="server" ID="lblCountEdit">0</asp:Label></td>
											<td align="right" width="80">�д� -
												<asp:Label Runat="server" ID="lblCountErr" ForeColor="red">0</asp:Label></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table width="100%" height="100%" id="pnStep9" style="DISPLAY:none">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic" width="200">������</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr height="100%">
								<td valign="top">
									<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td><TEXTAREA class="input" id="txtResult" style="WIDTH: 100%; HEIGHT: 100%" name="txtResult"
													runat="server"></TEXTAREA>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE height="100%" cellSpacing="10" cellPadding="0" width="100%" border="0">
							<tr align="center">
								<td>
									<input type="button" class="submit" id="btnClear" name="btnClear" value="��ղ��ϼ۸�" onclick="if (!confirm('ȷʵҪɾ�����в��ϼ۸���')) return false; document.all.divHintSave.style.display='';"
										runat="server" onserverclick="btnClear_ServerClick"> <input type="button" style="DISPLAY:none" class="submit" id="btnPrior" name="btnPrior"
										value="��һ��" onclick="document.all.divHintSave.style.display='';" runat="server" onserverclick="btnPrior_ServerClick">
									<input type="button" style="DISPLAY:none" class="submit" id="btnNext" name="btnNext" value="��һ��"
										onclick="document.all.divHintSave.style.display='';" runat="server" onserverclick="btnNext_ServerClick"> <input type="button" style="DISPLAY:none" class="submit" id="btnComplete" name="btnComplete"
										value="�� ��" onclick="if (!confirm('ȷʵҪ��ʼ������')) return false; document.all.divHintSave.style.display='';" runat="server" onserverclick="btnComplete_ServerClick">
									<input type="button" style="DISPLAY:none" class="submit" id="btnContinue" name="btnContinue"
										value="�� ��" onclick="document.all.divHintSave.style.display='';" runat="server" onserverclick="btnContinue_ServerClick">
									<input type="button" style="DISPLAY:none" class="submit" id="btnOK" name="btnOK" value="ȷ ��"
										onclick="if (!confirm('ȷʵҪ��ʼ������')) return false; document.all.divHintSave.style.display='';"
										runat="server" onserverclick="btnOK_ServerClick"> <input type="button" class="submit" name="btnCancel" value="�� ��" onclick="window.close();"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 100px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input type="hidden" name="txtStep" id="txtStep" runat="server" value="1">
		</form>
	</body>
</HTML>
