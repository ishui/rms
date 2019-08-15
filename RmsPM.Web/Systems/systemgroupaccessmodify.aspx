<%@ Register TagPrefix="uc5" TagName="InputStationUser" Src="../UserControls/InputStationUser.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Systems.SystemGroupAccessModify" CodeFile="SystemGroupAccessModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>系统类别访问权限</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<script language="javascript" src="../Images/ContentMenu.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnSelectReturn" type="button" value="btnSelectReturn" name="btnSelectReturn"
					runat="server" onclick="if (!BeforeSubmit()) return false;" onserverclick="btnSelectReturn_ServerClick">
			</div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">系统类别访问权限</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="note" height="25">类别：<asp:label id="lblParentName" Runat="server"></asp:label>
									-&gt;<asp:label id="lblGroupName" Runat="server"></asp:label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic" width="200">权限列表</td>
								<td><input class="button-small" id="btnAdd" onclick="Add();" type="button" value="添 加" name="btnAdd"
										runat="server"></td>
								<TD>&nbsp;<input class="button-small" id="btnBatchModify" onclick="BatchModify()" type="button" value="批量配置"
										name="btnBatchModify" style="DISPLAY:none"></TD>
								<td>&nbsp;<input class="button-small" id="btnBatchDelete" onclick="if (!BatchDelete()) return false;"
										type="button" value="批量删除" name="btnBatchDelete" runat="server" onserverclick="btnBatchDelete_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgList" runat="server" AllowPaging="false" Width="100%" CssClass="list" CellPadding="0"
								AllowSorting="True" AutoGenerateColumns="False" PageSize="15" ShowFooter="False" DataKeyField="SystemID">
								<ItemStyle CssClass=""></ItemStyle>
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='全选或全不选'&gt;">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="20"></ItemStyle>
										<ItemTemplate>
											<input type="checkbox" name="chkSelect" class="list-checkbox" onclick="ChkSelectRow('<%# Container.ItemIndex + 1%>', this, document.all.dgList, 'list-2', '');" value='<%#DataBinder.Eval(Container, "DataItem.SystemID")%>'>
											<input id="txtAccessRangeType" type="hidden" name="txtAccessRangeType" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "AccessRangeType") %>'>
											<input id="txtRelationCode" type="hidden" name="txtRelationCode" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "RelationCode") %>'>
										</ItemTemplate>
									</asp:TemplateColumn>
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
											<table cellpadding="0" cellpadding="0" border="0">
												<tr>
													<td nowrap>
														<input type="checkbox" id="chkOpAll" name="chkOpAll" SystemID='<%# DataBinder.Eval(Container.DataItem, "SystemID")%>' onclick="ChkOpSelectAll(this);">
														<a href="#" onclick="ShowEditMenuAll(this);" SystemID='<%# DataBinder.Eval(Container.DataItem, "SystemID")%>'>全选</a>
													</td>
													<td>
														<asp:Repeater Runat="server" ID="dgOperation">
															<ItemTemplate>
																<input type="checkbox" id='chkOp' name="chkOp" <%# DataBinder.Eval(Container.DataItem, "Checked").ToString()=="True"?"checked":"" %> sno='<%# DataBinder.Eval(Container.DataItem, "Sno")%>' SystemID='<%# DataBinder.Eval(Container.DataItem, "SystemID")%>' OperationCode='<%# DataBinder.Eval(Container.DataItem, "OperationCode")%>' AccessRangeType='<%# DataBinder.Eval(Container.DataItem, "AccessRangeType")%>' RelationCode='<%# DataBinder.Eval(Container.DataItem, "RelationCode")%>'>
																<a id='a<%# DataBinder.Eval(Container.DataItem, "Sno")%>' sno='<%# DataBinder.Eval(Container.DataItem, "Sno")%>' href="#" onclick="ShowEditMenu(this);">
																	<%# DataBinder.Eval(Container.DataItem, "OperationName")%>
																	<span style="color:red" id='span<%# DataBinder.Eval(Container.DataItem, "Sno")%>' RoleLevel='<%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container.DataItem, "RoleLevel"))%>'><%# RmsPM.BLL.ConvertRule.ToInt(DataBinder.Eval(Container.DataItem, "RoleLevel"))==0?"":DataBinder.Eval(Container.DataItem, "RoleLevelName")%></span>
																</a>
															</ItemTemplate>
														</asp:Repeater>
													</td>
												</tr>
											</table>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Center"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
							<table class="form" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
								border="0">
								<TR>
									<TD class="form-item" width="100">权限范围：</TD>
									<TD><uc5:inputstationuser id="ucRelation" runat="server"></uc5:inputstationuser></TD>
								</TR>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" onclick="if (!BeforeSubmit()) return false;"
										runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
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
			<input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtGroupCode" type="hidden" name="txtGroupCode" runat="server">
			<input id="txtParentCode" type="hidden" name="txtParentCode" runat="server"> <input id="txtClassCode" type="hidden" name="txtClassCode" runat="server">
			<input id="txtUsers" type="hidden" name="txtUsers" runat="server"><input id="txtStations" type="hidden" name="txtStations" runat="server">
			<input id="txtDetailSno" type="hidden" name="txtDetailSno" runat="server"><input id="txtRelationSno" type="hidden" name="txtRelationSno" runat="server">
			<input id="txtDetailCount" type="hidden" name="txtDetailCount" runat="server"><input id="txtSelectCode" type="hidden" name="txtSelectCode" runat="server">
			<input id="txtCheckedCode" type="hidden" name="txtCheckedCode" runat="server"><input id="txtOperationSno" type="hidden" name="txtOperationSno" runat="server">
			<input id="txtIsResource" type="hidden" name="txtIsResource" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

//选择岗位人员
function Add()
{
	OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes=&StationCodes=&ReturnFunc=SelectReturn");
}

//选择岗位人员返回
function SelectReturn(userCodes,userNames,stationCodes,stationNames)
{
	Form1.txtUsers.value = userCodes;
	Form1.txtStations.value = stationCodes;
	
	Form1.btnSelectReturn.click();
}

//批量删除
function BatchDelete()
{
	var s = ChkGetSelected(document.all.chkSelect);

	if (s == "")
	{
		alert('请选择一条或多条记录');
		return false;
	}
	
	Form1.txtSelectCode.value = s;

	if (!BeforeSubmit()) return false;
	
	document.all.divHintSave.style.display = "block";
	return true;
}

//form提交前
function BeforeSubmit()
{
	SaveCheckedCode();
	return true;
}

//将选中的操作代码保存在文本框中
function SaveCheckedCode()
{
	Form1.txtCheckedCode.value = "";
	
	var objCheck = document.all("chkOp");
	
	if (!objCheck) return;
	
	var s = "";
	var val;
	var sno;
	var AccessRangeType;
	var RelationCode;
	var OperationCode;
	var RoleLevel;
	
	if(objCheck[0])
	{
		l = objCheck.length;
	
		for(var i=0;i<l;i++)
		{
			if (objCheck[i].checked)
			{
				sno = objCheck[i].sno;
				AccessRangeType = objCheck[i].AccessRangeType;
				RelationCode = objCheck[i].RelationCode;
				OperationCode = objCheck[i].OperationCode;
				RoleLevel = GetRoleLevelBySno(sno);
				
				val = AccessRangeType + "|" + RelationCode + "|" + OperationCode + "|" + RoleLevel;
				
				s = JoinStr(s, val);
			}
		}
	}
	else
	{
		if(objCheck)
		{
			if (objCheck.checked)
			{
				sno = objCheck.sno;
				AccessRangeType = objCheck.AccessRangeType;
				RelationCode = objCheck.RelationCode;
				OperationCode = objCheck.OperationCode;
				RoleLevel = GetRoleLevelBySno(sno);
				
				val = AccessRangeType + "|" + RelationCode + "|" + OperationCode + "|" + RoleLevel;
				
				s = JoinStr(s, val);
			}
		}
	}

	Form1.txtCheckedCode.value = s;
	
//	alert(s);
	return;
}

function JoinStr(str, val)
{
	var s = str;
	
	if (s != "")
	{
		s = s + ",";
	}
	
	s = s + val;
	
	return s;
}

	function ChkOpSelectAll(objChkAll)
	{
		var SelectedCount = 0;

		var ischecked = objChkAll.checked;
		var AllSystemID = objChkAll.SystemID;

		var objCheck = document.all("chkOp");
		
		if (!objCheck) return SelectedCount;
			
		var SystemID;
		
		if(objCheck[0])
		{
			l = objCheck.length;
		
			for(var i=0;i<l;i++)
			{
				SystemID = objCheck[i].SystemID;
				if (SystemID == AllSystemID)
				{
					if (objCheck[i].checked != ischecked)
					{
						objCheck[i].click();
					}
					
					if (objCheck[i].checked)
					{
						SelectedCount = SelectedCount + 1;
					}
				}
			}
		}
		else
		{
			if(objCheck)
			{
				SystemID = objCheck.SystemID;
				if (SystemID == AllSystemID)
				{
					if (objCheck.checked != ischecked)
					{
						objCheck.click();
					}
					
					if (objCheck.checked)
					{
						SelectedCount = SelectedCount + 1;
					}
				}
			}
		}

		return SelectedCount;
	}

	function SetRoleLevelAll(AllRoleLevel, AllSystemID)
	{
		var objCheck = document.all("chkOp");
		
		if (!objCheck) return SelectedCount;
			
		var SystemID;
		var sno;
		
		if(objCheck[0])
		{
			l = objCheck.length;
		
			for(var i=0;i<l;i++)
			{
				SystemID = objCheck[i].SystemID;
				if (SystemID == AllSystemID)
				{
					sno = objCheck[i].sno;
					SetSpanRoleLevel(AllRoleLevel, sno);
				}
			}
		}
		else
		{
			if(objCheck)
			{
				SystemID = objCheck.SystemID;
				if (SystemID == AllSystemID)
				{
					sno = objCheck.sno;
					SetSpanRoleLevel(AllRoleLevel, sno);
				}
			}
		}
	}

function ShowEditMenuAll(obj)
{
	var cssFile="../Images/ContentMenu.css";
	var SystemID = obj.SystemID;

		var i=-1;
		var Items = new Array(2);

		i++;
		Items[i] = new Array(3);
		Items[i][0] = "所有";
		Items[i][1] = "";
		Items[i][2] = "RoleLevelAllClick(0, '" + SystemID + "');";

		i++;
		Items[i] = new Array(3);
		Items[i][0] = "个人";
		Items[i][1] = "";
		Items[i][2] = "RoleLevelAllClick(1, '" + SystemID + "');";

		CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

function ShowEditMenu(obj)
{
	var cssFile="../Images/ContentMenu.css";
	var id = obj.sno;
	var RoleLevel = GetRoleLevelBySno(id);
	var RoleLevelN;
	
		var i=-1;
		var Items = new Array(1);

		i++;
		Items[i] = new Array(3);
		if (RoleLevel == "1")
		{
			Items[i][0] = "所有";
			RoleLevelN = "0";
		}
		else
		{
			Items[i][0] = "个人";
			RoleLevelN = "1";
		}
		Items[i][1] = "";
		Items[i][2] = "RoleLevelClick(" + RoleLevelN + ", '" + id + "');";

/*
		var Items = new Array(2);

		i++;
		Items[i] = new Array(3);
		if (RoleLevel == "1")
		{
			Items[i][0] = "&nbsp;&nbsp;&nbsp;所有";
		}
		else
		{
			Items[i][0] = "●&nbsp;所有";
		}
		Items[i][1] = "";
		Items[i][2] = "RoleLevelClick(0, '" + id + "');";

		i++;
		Items[i] = new Array(3);
		if (RoleLevel == "1")
		{
			Items[i][0] = "●&nbsp;个人";
		}
		else
		{
			Items[i][0] = "&nbsp;&nbsp;&nbsp;个人";
		}
		Items[i][1] = "";
		Items[i][2] = "RoleLevelClick(1, '" + id + "');";
*/

		CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

function RoleLevelAllClick(RoleLevel, sno)
{
	HideContentMenu();	
	SetRoleLevelAll(RoleLevel, sno);
}

function RoleLevelClick(RoleLevel, sno)
{
	HideContentMenu();	
	SetSpanRoleLevel(RoleLevel, sno);
}

function SetSpanRoleLevel(RoleLevel, sno)
{
	var span = document.all("span" + sno);
	var val = "";
	
	if (RoleLevel == "1")
	{
		val = "个人";
	}
	
	span.innerText = val;
	span.RoleLevel = RoleLevel;
}

function GetRoleLevelBySno(sno)
{
	var span = document.all("span" + sno);
	return span.RoleLevel;
}

//-->	
		</SCRIPT>
	</body>
</HTML>
