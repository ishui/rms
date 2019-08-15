<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierTitleModif.aspx.cs" Inherits="Supplier_SupplierTitleModif" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>公司标题</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">公司标题</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
				
						    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							    <tr>
							    <td  class="form-item">公司名称</td>
                                <td><asp:Label ID="lblCompanyName" runat="server" ></asp:Label></td>
							    </tr>
							    <TR>
								    <TD width="80" class="form-item">公司标题：</TD>
								    <TD><input type="text" class="input" size="30" id="TxtCompanyTitle" name="TxtCompanyTitle"
										    runat="server"><font color="red">*</font>
								    </TD>
							    </TR>
							    <TR>
								    <TD width="80" class="form-item">银行帐号：</TD>
								    <TD><input type="text" class="input" size="30" id="TxtBankAccount" name="TxtBankAccount"
										    runat="server"><font color="red">*</font>
								    </TD>
							    </TR>
							   
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
	</body>
</HTML>
