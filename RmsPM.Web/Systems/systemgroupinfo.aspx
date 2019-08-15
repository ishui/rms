<%@ Page language="c#" Inherits="RmsPM.Web.Systems.SystemGroupInfo" CodeFile="SystemGroupInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Images/XmlTree.js"></SCRIPT>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	
	//释放源
	function ClearSrc()
	{
		window.parent.document.all("txtIsCut").value = "";
		window.parent.document.all("txtSrcDesc").value = "";
		window.parent.document.all("txtSrcGroupCode").value = "";
		window.parent.document.all("txtSrcClassCode").value = "";
	}
	
	//刷新
	function Refresh(act)
	{
		if (act.toLowerCase() == "insert")
		{
			window.parent.frameMain.MyRefreshChild();
			window.location = window.location;
		}
		else
		{
			if (act.toLowerCase() == "move")
			{
				window.parent.frameMain.MyRefreshChild();
				window.parent.frameMain.MyDeleteSrc();

				ClearSrc();
				window.location = window.location;
			}
			else
			{
				window.parent.frameMain.MyRefreshNode();
				window.location = window.location;
			}
		}
	}
	
		</SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR id="trToolbar" runat="server">
					<TD class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" onclick="Modify()" type="button" value="修 改" name="btnModify"
							runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除吗（包括所有子项）？')) return false; document.all.divHintSave.style.display = '';"
							type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnAddChild" onclick="Insert();" type="button" value="新增子项" name="btnAddChild"
							runat="server"> <IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnCut" onclick="Cut()" type="button" value="剪 切" name="btnCut"
							runat="server"> <input class="button" id="btnCopy" onclick="Copy()" type="button" value="复 制" name="btnCopy"
							runat="server"> <input class="button" id="btnPaste" onclick="if (!Paste()) return false;" type="button"
							value="粘 贴" name="btnPaste" runat="server" onserverclick="btnPaste_ServerClick">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="80">类别名称：</TD>
								<TD><asp:label id="lblParentName" Runat="server"></asp:label>-&gt;<asp:label id="lblGroupName" Runat="server"></asp:label></TD>
								<td class="form-item" width="80">编 号：</td>
								<td><asp:label id="lblSortID" Runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="form-item">英文名称：</TD>
								<TD colspan="3"><asp:label id="lblGroupNameEn" Runat="server"></asp:label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table cellSpacing="0" cellPadding="0" border="0" width="100%">
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td class="intopic">权限</td>
												<td><input class="button-small" id="btnAddAccess" onclick="AddAccess();" type="button" value="配置权限"
														name="btnAddAccess" runat="server"></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<asp:datagrid id="dgList" runat="server" ShowFooter="False" PageSize="15" AutoGenerateColumns="False"
											AllowSorting="True" CellPadding="0" CssClass="list" Width="100%" AllowPaging="false">
											<ItemStyle CssClass=""></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="权限范围">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<img src='../images/<%# DataBinder.Eval(Container.DataItem, "AccessRangeTypeImageName") %>' align="absMiddle">
														<%# DataBinder.Eval(Container.DataItem, "RelationName")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="操作">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "OperationHtml")%>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr id="tablePaste" style="DISPLAY:none">
					<td class="table" vAlign="bottom" align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="note">剪贴板：<span id="spanSrcDesc"></span></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtGroupCode" type="hidden" name="txtGroupCode" runat="server"> <input id="txtClassCode" type="hidden" name="txtClassCode" runat="server">
			<input id="txtAct" type="hidden" name="txtAct" runat="server"> <input id="txtParentCode" type="hidden" name="txtParentCode" runat="server"><input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
			<input type="hidden" id="txtSrcGroupCode" name="txtSrcGroupCode" runat="server"><input type="hidden" id="txtSrcClassCode" name="txtSrcClassCode" runat="server">
			<input type="hidden" id="txtIsCut" name="txtIsCut" runat="server"> <input id="txtIsResource" type="hidden" name="txtIsResource" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

	//新增子项
	function Insert(){
		OpenCustomWindow("SystemGroupModify.aspx?Action=Insert&ParentCode=" + Form1.txtGroupCode.value + "&ClassCode=" + Form1.txtClassCode.value, "系统类别修改", 500, 300);
	}

	//修改
	function Modify(){
		OpenCustomWindow("SystemGroupModify.aspx?Action=Modify&GroupCode=" + Form1.txtGroupCode.value, "系统类别修改", 500, 300);
	}
	
	//剪切
	function Cut()
	{
		window.parent.document.all("txtIsCut").value = "1";
		window.parent.document.all("txtSrcGroupCode").value = Form1.txtGroupCode.value;
		window.parent.document.all("txtSrcClassCode").value = Form1.txtClassCode.value;
		
		var desc = document.all.lblGroupName.innerText;
		window.parent.document.all("txtSrcDesc").value = desc;
		
		window.parent.frameMain.CutNode();
		
		IniPaste();
		
	}
	
	//复制
	function Copy()
	{
		window.parent.document.all("txtIsCut").value = "";
		window.parent.document.all("txtSrcGroupCode").value = Form1.txtGroupCode.value;
		window.parent.document.all("txtSrcClassCode").value = Form1.txtClassCode.value;
		
		var desc = document.all.lblGroupName.innerText;
		window.parent.document.all("txtSrcDesc").value = desc;
		
		window.parent.frameMain.CopyNode();

		IniPaste();
	}
	
	//粘贴
	function Paste()
	{
		var srcGroupCode = window.parent.document.all("txtSrcGroupCode").value;
		var srcClassCode = window.parent.document.all("txtSrcClassCode").value;
		//alert(window.parent.document.all("txtSrcDesc").value);
		var srcDesc = window.parent.document.all("txtSrcDesc").value;
		var isCut = window.parent.document.all("txtIsCut").value;
		
		var dstGroupCode = Form1.txtGroupCode.value;
		var dstClassCode = Form1.txtClassCode.value;

		if (srcGroupCode == "") return false;

		if (isCut == "1")
		{
			if (dstGroupCode == srcGroupCode)
			{
				alert("无法移动 " + srcDesc + "：源结点和目标结点相同");
				return false;
			}
			
			if (!confirm("确实要移动 " + srcDesc + " 吗？")) return false;
		}
		else
		{
			if (!confirm("确实要粘贴吗？")) return false;
		}
		
		Form1.txtSrcGroupCode.value = srcGroupCode;
		Form1.txtSrcClassCode.value = srcClassCode;
		Form1.txtIsCut.value = isCut;
		
		return true;
		
//		window.parent.Paste(Form1.txtClassCode.value, Form1.txtGroupCode.value);
	}
	
	function IniPaste()
	{
		var srcGroupCode = window.parent.document.all("txtSrcGroupCode").value;
		if(window.parent.document.all("txtSrcDesc")!=null)
		{
		   var srcDesc = window.parent.document.all("txtSrcDesc").value;
		}
		document.all.spanSrcDesc.innerText = srcDesc;
		
		if (srcGroupCode == "")
		{
			if (Form1.btnPaste) Form1.btnPaste.style.display = "none";
			document.all.tablePaste.style.display = "none";
		}
		else
		{
			if (Form1.btnPaste) Form1.btnPaste.style.display = "";
			document.all.tablePaste.style.display = "";
		}
	}

	function winload()
	{
		IniPaste();
	}
	
	function AddAccess()
	{
		OpenCustomWindow("SystemGroupAccessModify.aspx?Action=Insert&GroupCode=" + Form1.txtGroupCode.value + "&ClassCode=" + Form1.txtClassCode.value, "系统类别访问权限修改", 760, 540);
	}
	
//-->	
		</SCRIPT>
	</body>
</HTML>
