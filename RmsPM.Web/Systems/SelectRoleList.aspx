<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Page language="c#" Codefile="SelectRoleList.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Systems.SelectRoleList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择角色</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XMLTree.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<style>.LeftMenuItem { FONT-SIZE: 12px; MARGIN: 1px; COLOR: #00309c }
	.LeftMenuItemOnMouseOver { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #fffbff }
	.LeftMenuItemOnActivty { BORDER-RIGHT: #2155bd 1px solid; BORDER-TOP: #2155bd 1px solid; FONT-SIZE: 12px; MARGIN: 0px; BORDER-LEFT: #2155bd 1px solid; COLOR: #00309c; BORDER-BOTTOM: #2155bd 1px solid; BACKGROUND-COLOR: #ffe794 }
	A { COLOR: #000000; TEXT-DECORATION: none }
	A:hover { TEXT-DECORATION: none }
		</style>
		<SCRIPT language="javascript">
		
	function AddStrings(s, val) {
		var sreturn = s;
		
		if (s == "") {
			return val;
		}
		
		s = "," + s + ",";
		if (s.indexOf("," + val + ",") < 0) {
			sreturn = sreturn + "," + val;
		}
		
		return sreturn;
	}
	
	function RemoveStrings(s, val) {
		var sreturn = s;
		var i, l;
		
		if (s == "") {
			return "";
		}

		var arr = s.split(",");

		var tempArray = new Array();
		var iCount = arr.length;
		for (i=0; i<iCount; i++)
		{
			if ( arr[i] != val ) {
				tempArray.push(arr[i]);
			}
		}
		
		sreturn = tempArray.join(",");
		return sreturn;
				
/*		s = "," + s + ",";
		i = s.indexOf("," + val + ",");
		if (i >= 0) {
			l = val.length + 1;
			sreturn = s.substr(1, i-1) + s.substr(i + l - 1, s.length - i - l - 2);
		}
		
		return sreturn;*/
	}
	
	function ChkClick(chk) {
		var select = window.parent.document.all.txtSelectRoleCode.value;
		
		if (chk.checked) {
			select = AddStrings(select, chk.value);
		}
		else {
			select = RemoveStrings(select, chk.value);
		}
		
//		alert(select);
		
		window.parent.document.all.txtSelectRoleCode.value = select;
	}
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD vAlign="top" align="center">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
							<tr valign="top">
								<td>
									<div style="OVERFLOW: auto;WIDTH: 100%;HEIGHT: 100%">
										<asp:datagrid id="dgList" runat="server" CellPadding="2" GridLines="Horizontal" AutoGenerateColumns="False"
											AllowSorting="True" Width="100%" DataKeyField="RoleCode" OnSelectedIndexChanged="dgList_SelectedIndexChanged">
											<ItemStyle CssClass="ListBodyTr"></ItemStyle>
											<HeaderStyle CssClass="ListHeadTr"></HeaderStyle>
											<FooterStyle CssClass="ListHeadTr"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="选择">
													<HeaderStyle Width="60px"></HeaderStyle>
													<ItemTemplate>
														<INPUT id="chk" type="checkbox" name="chk" runat="server" onclick="ChkClick(this);" value='<%# DataBinder.Eval (Container.DataItem, "RoleCode") %>' >
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="RoleName" HeaderText="角色"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="部门">
													<HeaderStyle Width="60px"></HeaderStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.SystemRule.GetUnitFullName(DataBinder.Eval (Container.DataItem, "UnitCode").ToString()) %>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
									</div>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<asp:Label Runat="server" ID="lblMessage" ForeColor="red"></asp:Label>
			<input id="txtUserCode" type="hidden" name="txtUserCode" runat="server"><INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server"><INPUT id="txtUnitCode" type="hidden" name="txtUnitCode" runat="server">
			<INPUT id="txtSelectRoleCode" type="hidden" name="txtSelectRoleCode" runat="server">
		</form>
	</body>
</HTML>
