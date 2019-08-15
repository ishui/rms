<%@ Page language="c#" Inherits="RmsPM.Web.PBS.RoomModifyArea" CodeFile="RoomModifyArea.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>修改房源面积</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="map.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><input type="button" id="btnHiddenPost" name="btnHiddenPost" runat="server" onclick="document.all.divHintSave.style.display = '';" onserverclick="btnHiddenPost_ServerClick"></div>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">修改房源面积</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr vAlign="top">
									<td><asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" CellPadding="4" AllowSorting="True"
											AutoGenerateColumns="False" PageSize="15" ShowFooter="True" DataKeyField="RoomCode">
											<ItemStyle CssClass="list-i"></ItemStyle>
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="楼栋名称" FooterText="合计" Visible="False">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<a href="#" onclick="OpenBuildingInfo(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>'><%# DataBinder.Eval(Container, "DataItem.BuildingName") %></a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="楼栋名称" FooterText="合计">
													<ItemTemplate>
														<%# DataBinder.Eval(Container, "DataItem.BuildingName") %>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ChamberName" HeaderText="门牌号">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="RoomName" HeaderText="室号">
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FloorName" HeaderText="楼层">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="产品类型">
													<ItemTemplate>
														<%# RmsPM.BLL.PBSRule.GetPBSTypeFullName(DataBinder.Eval(Container, "DataItem.PBSTypeCode")) %>
													</ItemTemplate>
													<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="预测建面(平米)">
													<ItemTemplate>
														<input name="txtPreBuildArea" onfocus="AreaFocus(this)" onblur="AreaBlur(this)" type="text" id="txtPreBuildArea" onkeydown="if(event.keyCode==13) event.keyCode=9" class="input-nember" runat="server" size="6" value='<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container, "DataItem.PreBuildArea")) %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumPreBuildArea"></asp:Label>
													</FooterTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="预测套面(平米)">
													<ItemTemplate>
														<input name="txtPreRoomArea" onfocus="AreaFocus(this)" onblur="AreaBlur(this)" type="text" id="txtPreRoomArea" onkeydown="if(event.keyCode==13) event.keyCode=9" class="input-nember" runat="server" size="6" value='<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container, "DataItem.PreRoomArea")) %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumPreRoomArea"></asp:Label>
													</FooterTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="实测建面(平米)">
													<ItemTemplate>
														<input name="txtBuildArea" onfocus="AreaFocus(this)" onblur="AreaBlur(this)" type="text" id="txtBuildArea" onkeydown="if(event.keyCode==13) event.keyCode=9" class="input-nember" runat="server" size="6" value='<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container, "DataItem.BuildArea")) %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumBuildArea"></asp:Label>
													</FooterTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="实测套面(平米)">
													<ItemTemplate>
														<input name="txtRoomArea" onfocus="AreaFocus(this)" onblur="AreaBlur(this)" type="text" id="txtRoomArea" onkeydown="if(event.keyCode==13) event.keyCode=9" class="input-nember" runat="server" size="6" value='<%# RmsPM.BLL.StringRule.BuildShowNumberString(DataBinder.Eval(Container, "DataItem.RoomArea")) %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumRoomArea"></asp:Label>
													</FooterTemplate>
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server"
										onclick="document.all.divHintSave.style.display = '';" onserverclick="btnSave_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
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
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtSelectRoomCode" name="txtSelectRoomCode" runat="server">
			<input type="hidden" id="txtIsFirst" name="txtIsFirst" runat="server"> <input type="hidden" id="txtAct" name="txtAct" runat="server">
			<input type="hidden" id="txtBuildingCode" name="txtBuildingCode" runat="server">
			<input type="hidden" id="txtReturnScript" name="txtReturnScript" runat="server">
			<INPUT id="txtDetailCount" type="hidden" name="txtDetailCount" runat="server"> <INPUT id="txtSumPreBuildArea" type="hidden" name="txtSumPreBuildArea" runat="server"><INPUT id="txtSumPreRoomArea" type="hidden" name="txtSumPreRoomArea" runat="server">
			<INPUT id="txtSumBuildArea" type="hidden" name="txtSumBuildArea" runat="server"><INPUT id="txtSumRoomArea" type="hidden" name="txtSumRoomArea" runat="server">
		</form>
		<SCRIPT language="javascript">
<!--

	//取父窗口所选择的房间号
	if ((Form1.txtAct.value.toLowerCase() == "parent") && (Form1.txtIsFirst.value == ""))
	{
		Form1.txtIsFirst.value = "1";
		Form1.txtSelectRoomCode.value = window.opener.document.all.txtSelectRoomCode.value;
		Form1.btnHiddenPost.click();
	}
	
var area;

function AreaFocus(obj)
{
	area = obj.value;
}

function AreaBlur(obj)
{
	if (obj.value != area)
	  CalcSum();
}

//计算合计
function CalcSum()
{
	var c = parseInt(Form1.txtDetailCount.value);
	var tempVal = 0;
	var sum1 = 0;
	var sum2 = 0;
	var sum3 = 0;
	var sum4 = 0;
	
	for(i=0;i<c;i++)
	{
		tempVal = ConvertFloat(document.all("dgList__ctl" + (i + 2) + "_txtPreBuildArea").value);
		sum1 = sum1 + tempVal;
		
		tempVal = ConvertFloat(document.all("dgList__ctl" + (i + 2) + "_txtPreRoomArea").value);
		sum2 = sum2 + tempVal;
		
		tempVal = ConvertFloat(document.all("dgList__ctl" + (i + 2) + "_txtBuildArea").value);
		sum3 = sum3 + tempVal;
		
		tempVal = ConvertFloat(document.all("dgList__ctl" + (i + 2) + "_txtRoomArea").value);
		sum4 = sum4 + tempVal;
	}

	//格式化
	sum1 = FormatNumber(sum1, 2);
	sum2 = FormatNumber(sum2, 2);
	sum3 = FormatNumber(sum3, 2);
	sum4 = FormatNumber(sum4, 2);

	Form1.txtSumPreBuildArea.value = sum1;
	Form1.txtSumPreRoomArea.value = sum2;
	Form1.txtSumBuildArea.value = sum3;
	Form1.txtSumRoomArea.value = sum4;
	
	document.all("dgList__ctl" + (c + 2) + "_lblSumPreBuildArea").innerText = sum1;
	document.all("dgList__ctl" + (c + 2) + "_lblSumPreRoomArea").innerText = sum2;
	document.all("dgList__ctl" + (c + 2) + "_lblSumBuildArea").innerText = sum3;
	document.all("dgList__ctl" + (c + 2) + "_lblSumRoomArea").innerText = sum4;
	
//	alert(sum);
}

//-->
		</SCRIPT>
	</body>
</HTML>
