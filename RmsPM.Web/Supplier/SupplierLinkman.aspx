<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierLinkman.aspx.cs" Inherits="Supplier_SupplierLinkman" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSubjectSet" Src="../UserControls/InputSubjectSet.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>厂商联系人</title>
    <meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	<script language="javascript" src="../Rms.js"></script>
	
</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">厂商联系人</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
				
						    <table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TBODY>
									
									<TR>
										<TD class="form-item" noWrap>联 系 人：</TD>
										<TD noWrap><asp:textbox id="txtContractPerson" runat="server" CssClass="input"></asp:textbox><span runat="server" id="SpanPerson"><font color="red">*</font></span></TD>
										<TD class="form-item" noWrap>联系电话：</TD>
										<TD><asp:textbox id="txtOfficePhone" runat="server" CssClass="input"></asp:textbox><span runat="server" id="SpanPhone"><font color="red">*</font></span></TD>
										
									</TR>
										<TR>
										<TD class="form-item" noWrap>邮政编码：</TD>
										<TD noWrap><asp:textbox id="txtPostCode" runat="server" CssClass="input"></asp:textbox></TD>
										<TD class="form-item" noWrap>地区名称：</TD>
										<TD noWrap><asp:textbox id="TxtAreaName" runat="server"  CssClass="input"></asp:textbox></TD>
										
									</TR>
									<TR>
									    <TD class="form-item" noWrap>项目名称：</TD>
										<TD noWrap><asp:textbox id="TxtProjectName" runat="server" CssClass="input"></asp:textbox></TD>
										<TD class="form-item" noWrap>手机：</TD>
										<TD noWrap><asp:textbox id="txtMobile" runat="server" CssClass="input"></asp:textbox></TD>
										
									</TR>
									<TR>
									   <TD class="form-item" noWrap>传真：</TD>
										<TD><asp:textbox id="txtFax" runat="server" CssClass="input"></asp:textbox></TD>
										<TD class="form-item" noWrap>EMail：</TD>
										<TD noWrap><asp:textbox id="txtEmail" runat="server" CssClass="input"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="form-item" style="height: 77px">备注：</TD>
										<TD noWrap colSpan="3" style="height: 77px"><asp:textbox id="TxtRemark" runat="server" Width="650px" CssClass="input" Height="60px"
												Rows="2" TextMode="MultiLine"></asp:textbox></TD>
									</TR>
								</TBODY>
							</table>
					
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
									<input id="btnDelete" name="btnDelete" type="button" class="submit" value="删 除" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
										runat="server" onserverclick="btnDelete_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtSystemID" type="hidden" name="txtSystemID" runat="server">
		</form>
		<script language="javascript">
	        function BiddingLog()
            {

                OpenLargeWindow('BiddingLogList.aspx','招投标日志');
            }
        </script>
	</body>
</HTML>
