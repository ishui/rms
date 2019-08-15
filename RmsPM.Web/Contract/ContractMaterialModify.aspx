<%@ Register TagPrefix="uc1" TagName="InputMaterial" Src="../UserControls/InputMaterial.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractMaterialModify" CodeFile="ContractMaterialModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�޸Ĳ�������</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
<!--

function InfraQtyChange(oEdit, oldValue, oEvent)
{
	InfraValueSum();
}

//����ϼ�
function InfraValueSum()
{
	var dgName;
	var lblName;
	var txtName;
	var c;
	var tempValue = 0;
	var sum = 0;
	
    lblName = "lblSumQty";
	txtName = "txtQty";
    dgName = "dgList";
    
	c = parseInt(document.all(dgName).rows.length) - 2;

	for(i=0;i<c;i++)
	{
	    tempValue = ConvertFloat(document.all(GetObjectNameInDataGrid(dgName, (i + 2), txtName)).value);
		sum = sum + tempValue;
	}

	//��ʽ��
	sum = formatNumber(sum, "#,###.00");

	document.all(GetObjectNameInDataGrid(dgName, (c + 2), lblName)).innerText = sum;
//	alert(sum);
}

function doSave()
{
	return true;
}	

//-->
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table id="tableMain" height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff"
				border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						<asp:label id="lblTitle" runat="server" BackColor="Transparent">�޸Ĳ�������</asp:label>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="form-item" width="80">��ͬ���ƣ�</TD>
									<TD>
										<asp:Label ID="lblContractName" Runat="server"></asp:Label>
									</TD>
									<TD class="form-item" width="80">��ͬ��ţ�</TD>
									<TD width="100">
										<asp:Label ID="lblContractID" Runat="server"></asp:Label>
									</TD>
									<TD class="form-item" width="80">���ţ�</TD>
									<TD>
										<asp:Label ID="lblUnit" Runat="server"></asp:Label>
									</TD>
								</TR>
								<tr>
									<TD class="form-item">�� Ӧ �̣�</TD>
									<TD>
										<asp:Label ID="lblSupplierName" Runat="server"></asp:Label>
									</TD>
									<TD class="form-item">�� �� ����</TD>
									<TD>
										<asp:Label ID="lblThirdParty" Runat="server"></asp:Label>
									</TD>
									<TD class="form-item">��ͬ���ͣ�</TD>
									<TD>
										<asp:Label ID="lblSystemGroup" Runat="server"></asp:Label>
									</TD>
								</tr>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="intopic" width="200">��������</td>
									<td><input class="button-small" id="btnNewItem" type="button" value="������������" name="btnNewItem"
											runat="server" onserverclick="btnNewItem_ServerClick"></td>
								</tr>
							</table>
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD><asp:datagrid id="dgList" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server"
											Width="100%" CssClass="list" PageSize="15" AutoGenerateColumns="False" AllowSorting="True"
											ShowFooter="True">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ContractMaterialCode"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="���" ItemStyle-Width="80">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��������&lt;font color=red&gt;*&lt;/font&gt;" ItemStyle-Width="150" FooterText="�ϼ�">
													<ItemTemplate>
                                                        <uc1:InputMaterial id="InputMaterial" Value='<%# DataBinder.Eval(Container, "DataItem.MaterialCode") %>'  runat="server" ></uc1:InputMaterial>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����&lt;font color=red&gt;*&lt;/font&gt;" ItemStyle-Width="150">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<igtxt:webnumericedit Width="100" id="txtQty" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.Qty") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
															<ClientSideEvents ValueChange="InfraQtyChange"></ClientSideEvents>
														</igtxt:webnumericedit>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumQty" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����" ItemStyle-Width="150">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<igtxt:webnumericedit Width="100" id="txtPrice" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.Price") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
														</igtxt:webnumericedit>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="ɾ��"
													CommandName="Delete"></asp:ButtonColumn>
											</Columns>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="9" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" onclick="if(!doSave()) return false;" type="button"
										value="ȷ ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> &nbsp; <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="ȡ ��"
										name="btnCancel" runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
		</form>
	</body>
</HTML>
