<%@ Page language="c#" Inherits="RmsPM.Web.Supplier.ImportSupplierDlg" CodeFile="ImportSupplierDlg.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>导入厂商</title>
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
	
	//页面显示
	for(var i=1;i<=9;i++)
	{
		var pnId = "pnStep" + i;
		if (document.all(pnId))
			document.all(pnId).style.display = "none";
	}
	
	var pnId = "pnStep" + step;
	if (document.all(pnId))
		document.all(pnId).style.display = "";
		
	//按钮显示
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
	else if (step == 2) //最后一步
	{
		Form1.btnPrior.style.display = "";
		Form1.btnComplete.style.display = "";
	}
	else if (step == 9) //导入完成
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
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">导入厂商</td>
				</tr>
				<tr align="center">
					<td style="COLOR: blue">导入内容将按【厂商名称＋厂商类型】覆盖原有厂商</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<table width="100%" height="100%" id="pnStep1" style="DISPLAY:none">
							<tr>
								<td valign="top">
									<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="form-item" width="60">文件：</td>
											<td><input class="textbox" id="txtFile" type="file" style="WIDTH:100%" size="45" name="txtFile"
													runat="server"></td>
										</tr>
									</TABLE>
									<br>
									<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0">
										<tr>
											<td>文件格式说明：<br>
												1.文件类型必须是csv（逗号分隔）<br>
												2.文件的第1行为标题行，以后为数据行。<br>
												3.数据行依次为：<br>
												&nbsp;&nbsp;<asp:Label Runat="server" ID="lblFieldDesc" ForeColor="blue"></asp:Label><br>
												4.“类别全名”用“-&gt;”分隔，如：厂商-&gt;承包商-&gt;弱电，且必须和系统类别中的厂商类别对应
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
											<td class="intopic" width="200">预导入厂商列表</td>
											<td><input type="button" runat="server" class="button-small" id="btnDownloadCsv" name="btnDownloadCsv"
													value="另存为csv" onserverclick="btnDownloadCsv_ServerClick"></td>
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
												<asp:TemplateColumn HeaderText="行号" SortExpression="rowid" ItemStyle-HorizontalAlign="Right">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "rowid") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="预导入<br>结果" SortExpression="ImportResultName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<font color='<%# GetImportResultColor(DataBinder.Eval(Container.DataItem, "ImportResult"))%>'>
															<%# DataBinder.Eval(Container.DataItem, "ImportResultName") %>
															<font>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="厂商名称" SortExpression="SupplierName">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "SupplierName") %>
														<font color="red">
															<%# DataBinder.Eval(Container.DataItem, "ImportHint") %>
														</font>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="厂商简称" SortExpression="Abbreviation">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Abbreviation") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="类型" SortExpression="SupplierTypeCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "SupplierTypeFullName") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="资质" SortExpression="Quality">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Quality") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="地区" SortExpression="AreaCode">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "AreaCode") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="地址" SortExpression="RegisteredAddress">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "RegisteredAddress") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="联系人" SortExpression="ContractPerson">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "ContractPerson") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="电话" SortExpression="OfficePhone">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "OfficePhone") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="手机" SortExpression="Mobile">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Mobile") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="传真" SortExpression="Fax">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Fax") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="E-Mai" SortExpression="Email">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Email") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="网址" SortExpression="WebAddress">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "WebAddress") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="业绩" SortExpression="Achievement">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Achievement") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="评价意见" SortExpression="CheckOpinion">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "CheckOpinion") %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="信用等级" SortExpression="CreditLevel">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "CreditLevel") %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<table cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td align="right" width="80">新增 -
												<asp:Label Runat="server" ID="lblCountAdd" ForeColor="blue">0</asp:Label></td>
											<td align="right" width="80">修改 -
												<asp:Label Runat="server" ID="lblCountEdit">0</asp:Label></td>
											<td align="right" width="80">有错 -
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
											<td class="intopic" width="200">导入结果</td>
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
									<input type="button" class="submit" id="btnClear" name="btnClear" value="清空厂商" onclick="if (!confirm('确实要删除所有厂商吗？')) return false; document.all.divHintSave.style.display='';"
										runat="server" onserverclick="btnClear_ServerClick"> <input type="button" style="DISPLAY:none" class="submit" id="btnPrior" name="btnPrior"
										value="上一步" onclick="document.all.divHintSave.style.display='';" runat="server" onserverclick="btnPrior_ServerClick">
									<input type="button" style="DISPLAY:none" class="submit" id="btnNext" name="btnNext" value="下一步"
										onclick="document.all.divHintSave.style.display='';" runat="server" onserverclick="btnNext_ServerClick"> <input type="button" style="DISPLAY:none" class="submit" id="btnComplete" name="btnComplete"
										value="完 成" onclick="if (!confirm('确实要开始导入吗？')) return false; document.all.divHintSave.style.display='';" runat="server" onserverclick="btnComplete_ServerClick">
									<input type="button" style="DISPLAY:none" class="submit" id="btnContinue" name="btnContinue"
										value="继 续" onclick="document.all.divHintSave.style.display='';" runat="server" onserverclick="btnContinue_ServerClick">
									<input type="button" style="DISPLAY:none" class="submit" id="btnOK" name="btnOK" value="确 定"
										onclick="if (!confirm('确实要开始导入吗？')) return false; document.all.divHintSave.style.display='';"
										runat="server" onserverclick="btnOK_ServerClick"> <input type="button" class="submit" name="btnCancel" value="关 闭" onclick="window.close();"></td>
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
