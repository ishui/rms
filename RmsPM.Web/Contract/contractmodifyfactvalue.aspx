<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc3" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractModifyFactValue" CodeFile="ContractModifyFactValue.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>修改实际产值</title>
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

function InfraFactValueChange(oEdit, oldValue, oEvent)
{
	InfraValueSum(1);
}

//计算合计
function InfraValueSum(IsFact)
{
	var dgName;
	var lblName;
	var txtName;
	var c;
	var tempValue = 0;
	var sum = 0;
	
    lblName = "lblSumFactValue";
	txtName = "txtFactValue";

	if ( IsFact == 0 )
	{
		dgName = "dgValueList";
		c = parseInt(document.all.dgValueList.rows.length) - 2;
	}
	else
	{
		dgName = "dgFactValueList";
		c = parseInt(document.all.dgFactValueList.rows.length) - 2;
	}

	for(i=0;i<c;i++)
	{
	    tempValue = ConvertFloat(document.all(GetObjectNameInDataGrid(dgName, (i + 2), txtName)).value);
		sum = sum + tempValue;
	}

	//格式化
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
						<asp:label id="lblTitle" runat="server" BackColor="Transparent">修改实际产值</asp:label>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="form-item" width="80">合同名称：</TD>
									<TD>
										<asp:Label ID="lblContractName" Runat="server"></asp:Label>
									</TD>
									<TD class="form-item" width="80">合同编号：</TD>
									<TD width="100">
										<asp:Label ID="lblContractID" Runat="server"></asp:Label>
									</TD>
									<TD class="form-item" width="80">部门：</TD>
									<TD>
										<asp:Label ID="lblUnit" Runat="server"></asp:Label>
									</TD>
								</TR>
								<tr>
									<TD class="form-item">供 应 商：</TD>
									<TD>
										<asp:Label ID="lblSupplierName" Runat="server"></asp:Label>
									</TD>
									<TD class="form-item">第 三 方：</TD>
									<TD>
										<asp:Label ID="lblThirdParty" Runat="server"></asp:Label>
									</TD>
									<TD class="form-item">合同类型：</TD>
									<TD>
										<asp:Label ID="lblSystemGroup" Runat="server"></asp:Label>
									</TD>
								</tr>
								<TR>
									<TD class="form-item">经 办 人：</TD>
									<TD colSpan="5">
										<asp:Label ID="lblContractPersonName" Runat="server"></asp:Label>
									</TD>
								</TR>
								<TR>
									<TD class="form-item">备注：</TD>
									<TD colSpan="5">
										<asp:Label ID="lblRemark" Runat="server"></asp:Label>
									</TD>
								</TR>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="intopic" width="200">实际产值</td>
									<td><input class="button-small" id="btnNewFactValueItem" type="button" value="新增实际产值" name="btnNewFactValueItem"
											runat="server" onserverclick="btnNewFactValueItem_ServerClick"></td>
								</tr>
							</table>
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD><asp:datagrid id="dgFactValueList" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server"
											Width="100%" CssClass="list" PageSize="15" AutoGenerateColumns="False" AllowSorting="True"
											ShowFooter="True">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ContractProductionCode"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="序号" ItemStyle-Width="80">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="日期&lt;font color=red&gt;*&lt;/font&gt;" ItemStyle-Width="150" FooterText="合计">
													<ItemTemplate>
														<cc3:calendar id="dtFactProductionDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "ProductionDate")  %>'>
														</cc3:calendar>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="实际产值&lt;font color=red&gt;*&lt;/font&gt;" ItemStyle-Width="150">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<igtxt:webnumericedit Width="100" id="txtFactValue" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.ProductionValue") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
															<ClientSideEvents ValueChange="InfraFactValueChange"></ClientSideEvents>
														</igtxt:webnumericedit>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblSumFactValue" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="删除"
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
										value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> &nbsp; <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
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
