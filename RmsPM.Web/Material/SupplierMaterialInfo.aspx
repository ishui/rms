<%@ Page language="c#" Inherits="RmsPM.Web.Material.SupplierMaterialInfo" CodeFile="SupplierMaterialInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���̲�����Ϣ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">���ϼ۸���Ϣ</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><input class="button" id="btnModify" onclick="doModify('');return false;" type="button"
							value="�� ��" name="btnNew" runat="server"> <input class="button" id="btnDelete" onclick="if(!(confirm('ȷ��ɾ��������¼ ��'))) return false;"
							type="button" value="ɾ ��" name="btnNew" runat="server" onserverclick="btnDelete_ServerClick"></td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
					    <div style="overflow:auto;width:100%;height:100%;">
						    <table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
						        <tr>
								    <TD class="form-item" noWrap>�������ͣ�</TD>
								    <TD colspan="3"><asp:label id="lblGroupName" runat="server"></asp:label></TD>
						        </tr>
							    <TR>
								    <TD class="form-item" noWrap>���ң�</TD>
								    <TD><asp:label id="lblSupplierName" runat="server"></asp:label></TD>
								    <TD class="form-item" noWrap>Ʒ�ƣ�</TD>
								    <TD><asp:label id="lblBrand" runat="server"></asp:label></TD>
							    </TR>
							    <TR>
								    <TD class="form-item" noWrap>�ͺţ�</TD>
								    <TD><asp:label id="lblModel" runat="server"></asp:label></TD>
								    <TD class="form-item" noWrap>���</TD>
								    <TD><asp:label id="lblSpec" runat="server"></asp:label></TD>
							    </TR>
							    <TR>
								    <TD class="form-item" noWrap>����/������</TD>
								    <TD><asp:label id="lblNation" runat="server"></asp:label></TD>
								    <TD class="form-item" noWrap>���أ�</TD>
								    <TD><asp:label id="lblAreaCode" runat="server"></asp:label></TD>
							    </TR>
							    <TR>
								    <TD class="form-item" noWrap>��Ʒ��ţ�</TD>
								    <TD colspan="3"><asp:label id="lblSampleID" runat="server"></asp:label></TD>
								</TR>
							    <TR>
								    <TD class="form-item" noWrap width="80">��λ��</TD>
								    <TD><asp:label id="lblUnit" runat="server"></asp:label></TD>
								    <TD class="form-item" noWrap>���ۣ�</TD>
								    <TD><asp:label id="lblPrice" runat="server"></asp:label></TD>
							    </TR>
						    </table>
					    </div>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtSupplierMaterialCode" name="txtSupplierMaterialCode" runat="server" />
		</form>
		<script language="javascript">
		
	function doModify()
	{
		window.navigate('SupplierMaterialModify.aspx?SupplierMaterialCode=' + Form1.txtSupplierMaterialCode.value , '�޸Ĳ��ϼ۸�');
	}
	
		</script>
	</body>
</HTML>
