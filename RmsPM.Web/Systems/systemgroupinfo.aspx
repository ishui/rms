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
	
	//�ͷ�Դ
	function ClearSrc()
	{
		window.parent.document.all("txtIsCut").value = "";
		window.parent.document.all("txtSrcDesc").value = "";
		window.parent.document.all("txtSrcGroupCode").value = "";
		window.parent.document.all("txtSrcClassCode").value = "";
	}
	
	//ˢ��
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
						<input class="button" id="btnModify" onclick="Modify()" type="button" value="�� ��" name="btnModify"
							runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ���𣨰������������')) return false; document.all.divHintSave.style.display = '';"
							type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnAddChild" onclick="Insert();" type="button" value="��������" name="btnAddChild"
							runat="server"> <IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnCut" onclick="Cut()" type="button" value="�� ��" name="btnCut"
							runat="server"> <input class="button" id="btnCopy" onclick="Copy()" type="button" value="�� ��" name="btnCopy"
							runat="server"> <input class="button" id="btnPaste" onclick="if (!Paste()) return false;" type="button"
							value="ճ ��" name="btnPaste" runat="server" onserverclick="btnPaste_ServerClick">
					</TD>
				</TR>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="80">������ƣ�</TD>
								<TD><asp:label id="lblParentName" Runat="server"></asp:label>-&gt;<asp:label id="lblGroupName" Runat="server"></asp:label></TD>
								<td class="form-item" width="80">�� �ţ�</td>
								<td><asp:label id="lblSortID" Runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="form-item">Ӣ�����ƣ�</TD>
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
												<td class="intopic">Ȩ��</td>
												<td><input class="button-small" id="btnAddAccess" onclick="AddAccess();" type="button" value="����Ȩ��"
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
												<asp:TemplateColumn HeaderText="Ȩ�޷�Χ">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<img src='../images/<%# DataBinder.Eval(Container.DataItem, "AccessRangeTypeImageName") %>' align="absMiddle">
														<%# DataBinder.Eval(Container.DataItem, "RelationName")%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="����">
													<HeaderStyle Wrap="False"></HeaderStyle>
													<ItemStyle Wrap="True"></ItemStyle>
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "OperationHtml")%>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Center"
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
								<TD class="note">�����壺<span id="spanSrcDesc"></span></TD>
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

	//��������
	function Insert(){
		OpenCustomWindow("SystemGroupModify.aspx?Action=Insert&ParentCode=" + Form1.txtGroupCode.value + "&ClassCode=" + Form1.txtClassCode.value, "ϵͳ����޸�", 500, 300);
	}

	//�޸�
	function Modify(){
		OpenCustomWindow("SystemGroupModify.aspx?Action=Modify&GroupCode=" + Form1.txtGroupCode.value, "ϵͳ����޸�", 500, 300);
	}
	
	//����
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
	
	//����
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
	
	//ճ��
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
				alert("�޷��ƶ� " + srcDesc + "��Դ����Ŀ������ͬ");
				return false;
			}
			
			if (!confirm("ȷʵҪ�ƶ� " + srcDesc + " ��")) return false;
		}
		else
		{
			if (!confirm("ȷʵҪճ����")) return false;
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
		OpenCustomWindow("SystemGroupAccessModify.aspx?Action=Insert&GroupCode=" + Form1.txtGroupCode.value + "&ClassCode=" + Form1.txtClassCode.value, "ϵͳ������Ȩ���޸�", 760, 540);
	}
	
//-->	
		</SCRIPT>
	</body>
</HTML>
