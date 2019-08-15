<%@ Page language="c#" CodeFile="SupplierTypeModify.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Supplier.SupplierTypeModify" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>供应商类型</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">供应商类型</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%">
							<tr>
								<td width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td><input class="button" id="btnSave" type="button" value="保 存" name="btnSave" runat="server">
									<input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
										type="button" value="删 除" name="btnDelete" runat="server"> <INPUT class="button" id="btnClose" onclick="window.self.close();" type="button" value="关 闭"
										name="btnClose" runat="server"></td>
							</tr>
						</table>
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="40%">类型名称：</TD>
								<TD width="60%"><asp:textbox id="txtSupplierTypeName" runat="server" Width="80%" CssClass="input"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="form-item">说明：</TD>
								<TD><asp:textbox id="txtDescription" runat="server" Width="80%" CssClass="input"></asp:textbox></TD>
							</TR>
						</table>
						<br>
						<table id="tableList" cellSpacing="0" cellpadding="0" border="0" width="100%" runat="server">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" border="0" width="100%">
										<tr>
											<td class="intopic" width="200">子类型</td>
											<td><input class="button-small" id="btnAddChild" onclick="InsertSupplierType(); return false;"
													type="button" value="新增子类型" name="btnAddChild" runat="server"></td>
										</tr>
									</table>
									<table class="list" cellSpacing="0" cellPadding="0" width="98%" align="center" border="0">
										<tr class="list-title">
											<td noWrap>类型名称</td>
										</tr>
										<asp:repeater id="repeatList" runat="server">
											<ItemTemplate>
												<tr class="list-i">
													<td nowrap>
														<a href="#"  onclick='ModifySupplierType(this.code)' code='<%# DataBinder.Eval(Container.DataItem, "SupplierTypeCode") %>' >
															<%# DataBinder.Eval(Container.DataItem, "TypeName") %>
														</a>
													</td>
												</tr>
											</ItemTemplate>
										</asp:repeater></table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--
	function InsertSupplierType(){
		OpenMiddleWindow('../Supplier/SupplierTypeModify.aspx?Action=AddChild&supplierTypeCode=<%=Request["supplierTypeCode"]%>',"新增供应商类型");
	}
	
	function ModifySupplierType ( code)
	{
		OpenMiddleWindow('../Supplier/SupplierTypeModify.aspx?Action=Modify&SupplierTypeCode=' + code ,"修改供应商类型");
	}


//-->
		</script>
	</body>
</HTML>
