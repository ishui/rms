<%@ Reference Page="~/project/wbs.aspx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSInfo" CodeFile="WBSInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UCAttention" Src="../UserControls/UCAttention.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WBSAlertStatus" Src="WBSAlertStatus.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>工作信息</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
		<!--
			var ProjectCode = '<%=strProjectCode%>';			
			function winload()
			{
				if (Form1.txtFlag.value == "1")
				{
					if(Form1.btnExportTmpl!=null)
						Form1.btnExportTmpl.style.display = "none";
					if(Form1.btnImportTmpl!=null)
						Form1.btnImportTmpl.style.display = "none";
				}
			}
			
			//删除该节点后刷新列表
			function Delete(ParentCode)
			{
				window.opener.TreeSplitTop.updateChildNodes(ParentCode);
			}
			
			function ChangeTaskInfo(Action)
			{
				//if(CheckAuthority())
					OpenCustomWindow("WBSModify.aspx?Action=" + Action + "&WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode="+ProjectCode+"&FatherCode=<%=strFatherCode%>","",800,650);
			}
							
			//增加子节点
			function DoAddNewChildTask()
			{
				OpenCustomWindow('WBSModify.aspx?Action=Insert&WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode,'',800,600);
			}
				
			//打开工作项属性窗体 保留
			function OpenTask(WBSCode) 
			{
				OpenFullWindow('WBSInfo.aspx?WBSCode=' + WBSCode+'&ProjectCode='+ProjectCode+'&ParentWBSCode=<%=Request.QueryString["WBSCode"]%>',"");
			}
			
			//添加相关工作项
			function DoAddNewRelatedTask()
			{
				OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=2&WBSCode=<%=Request["WBSCode"]%>&ProjectCode="+ProjectCode);
			}
			
			//打开合同属性窗体
			function OpenContract(ContractCode)
			{
				OpenLargeWindow("../Contract/ContractInfo.aspx?ContractCode=" + ContractCode+"&ProjectCode="+ProjectCode,"",900,600);
			}
			
			//添加相关合同
			function DoAddNewContract()
			{				
				OpenLargeWindow('../SelectBox/SelectContract.aspx?Status=0&ProjectCode=' + ProjectCode );
			}
			

			//打开工作报告属性项
			function OpenExecute(ExecuteCode)
			{
				OpenFullWindow('WBSExecuteInfo.aspx?TaskExecuteCode=' + ExecuteCode+'&ProjectCode='+ProjectCode,'');
			}
			
			//添加工作报告
			function DoAddNewExecute()
			{
				//if(CheckAuthority())
					OpenFullWindow('WBSExecute.aspx?ActionState=Insert&WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode+'&RefreshScript=SelectTaskExecute()','');
			}
			
			//添加工作指导
			function DoAddNewGuid()
			{
				//if(CheckAuthority())
				OpenCustomWindow('WBSGuidModify.aspx?Action=Insert&WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode,'',640,300);
			}	
			
			// 打开工作指导明细
			function OpenTaskGuid(GuidCode)
			{
				OpenCustomWindow('WBSGuid.aspx?WBSCode=<%=Request.QueryString["WBSCode"]%>&TaskGuidCode='+GuidCode+'&ProjectCode='+ProjectCode,'',640,300);
			}		

			//打开附件
			function OpenDocument(DocumentCode)
			{
				OpenLargeWindow("../Document/DocumentInfo.aspx?DocumentCode=" + DocumentCode+'&ProjectCode='+ProjectCode); 
			}
			
			//添加文档
			function DoAddNewDocument()
			{
				//if(CheckAuthority())
					OpenLargeWindow('../SelectBox/SelectDocument.aspx?&ProjectCode='+ProjectCode);  
			}
			
			//新增文档
			function DoAddDocument()
			{
				//if(CheckAuthority())
					OpenMiddleWindow("../Document/DocumentModify.aspx?&Action=Insert&Return=Task&ProjectCode="+ProjectCode);  
			}
			
			//刷新页面
			function Update(PageNo)
			{
				//window.location.href = "../Project/WBSInfo.aspx?WBSCode=" + <%=Request.QueryString["WBSCode"]%> +"&PageNo=" + PageNo;
				document.forms[0].submit();
			}
			
			function TaskRefresh(Flag,Code)
			{
				document.forms[0].submit();
			}
			
			function CheckAuthority()
			{
				if (document.all.AuthorityFlag.value == "0")
				{
					alert("对不起，您没有权限！");
					return false;
				}
				else
					return true;
			}
			
			function ShowDiv(divID)
			{
				var obj = document.all[divID];
				obj.style.display = (obj.style.display !=  "none")?"none":"block";
			}

			function SelectContract(code)
			{
				document.all.hContractCode.value = code;	
				window.document.Form1.submit();
			}
			function SelectDocument(code)
			{
				document.all.hDocumentCode.value = code;	
				window.document.Form1.submit();
			}
			function SelectTaskExecute()
			{
				window.document.Form1.submit();
			}
			
			function SelectTask()
			{
				window.document.Form1.submit();
			}
			
			//显示工作信息
			function OpenTask(WBSCode)
			{
				OpenFullWindow('WBSInfo.aspx?WBSCode=' + WBSCode+'&ProjectCode='+ProjectCode+'&ParentWBSCode=<%=Request.QueryString["WBSCode"]%>',"","");
			}
			
			function doModifyTaskBudget()
			{
				var projectCode = Form1.txtProjectCode.value;
				OpenFullWindow( 'TaskBudgetModify.aspx?WBSCode=<%=Request["WBSCode"]%>&ProjectCode=' + projectCode ,'维护工作预算' ,"");
			}
			
			
			function SelectChangeStatus()
			{
				window.document.Form1.submit();
			}
			function DoAddAttention()
			{
				OpenMiddleWindow("../SelectBox/SelectSUMain.aspx?UserCodes="+window.document.all.hAttention.value+"&StationCodes="+window.document.all.hAttentionStation.value+"","selectPerson");
			}
			/*
			function DoSelectUser(userCode,userName,flag)
　　		{
　　			window.document.all.hAttention.value = userCode;	
				window.document.all.hIsAddAttention.value = "1";
				window.document.Form1.submit();
　　		}
　　		*/
			function getReturnStationUser(userCodes,userNames,stationCodes,stationNames,flag)
			{
				window.document.all.hAttention.value = userCodes;	
				window.document.all.hAttentionStation.value = stationCodes;	
				window.document.all.hIsAddAttention.value = "1";
				window.document.Form1.submit();
			}
			function SelectTaskReturn(code, name)
			{
//				if(type=='Task')
//				{
					window.document.all.hTaskCode.value = code;
					window.document.Form1.submit();
//				}
			}
			function SelectTaskReturn(code,name)
			{
				window.document.all.hTaskCode.value = code;
				window.document.Form1.submit();
			}
			function DeleteConfirm()
			{
				if(window.confirm('确实要删除该工作项吗？'))
						return true;
					else
						return false;
			}
			/*
			function Wait()
			{
				document.all.divHintLoad.style.display = '';				
				
				<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>				
				
			}onload='Wait();'
			*/
			
			//导出成模板
			function ExportTmpl()
			{
				OpenSmallWindow( 'TaskTempletOut.aspx?WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode,"模板导入" );
			}
			
			//从模板导入
			function ImportTmpl()
			{
				OpenSmallWindow( 'TaskTempletIn.aspx?WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode,"模板导入" );
			}
			
			//查看进度图
			function OpenProgressChart()
			{
				OpenFullWindow("../ConstructProg/ProjectProgressInfo.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&WBSCode=<%=Request.QueryString["WBSCode"]%>&GanttType=A" , "");
			}
			// 查看指定人员的工作
			function OpenMyTask(user)
			{
				OpenFullWindow("WBSMyTask.aspx?myUserTask="+user+"&ProjectCode="+ProjectCode+"&Type=ThisWeek");
			}
			function DoBatchModifyTask()
			{
				OpenLargeWindow("WBSBatchModify.aspx?SelectCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode="+ProjectCode,"批量修改");
			}
		//-->
		</script>
	</HEAD>
	<body onload="winload();" scroll="auto">
		<form id="Form1" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="tools-area">
						<input class="button" id="ModifyButton" onclick="ChangeTaskInfo('Modify');return false;"
							type="button" value="修 改" name="ModifyButton" runat="server"> <input class="button" id="btDelete" type="button" value="删 除" name="btDelete" runat="server"
							onclick="if(!DeleteConfirm()) return false;" onserverclick="btDelete_ServerClick"> <input class="button" id="btClose" onclick="javascript:window.close();" type="button" value="关 闭"
							name="btClose">
						<uc1:wbsalertstatus id="myStatus" runat="server"></uc1:wbsalertstatus>
						<uc1:UCAttention id="myUCAttention" runat="server"></uc1:UCAttention>
						<input class="button" id="btAttention" onclick="DoAddAttention();return false;" type="button"
							value="设定关注人" runat="server" name="btAttention"> <input class="button" id="btnExportTmpl" onclick="ExportTmpl();" type="button" value="导出成模板"
							runat="server" name="btnExportTmpl"> <input class="button" id="btnImportTmpl" onclick="ImportTmpl();" type="button" value="从模板导入"
							runat="server" name="btnImportTmpl"> <input class="button" id="btnOpenProgressChart" onclick="OpenProgressChart();" type="button"
							value="进度图" runat="server" name="btnOpenProgressChart">
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr width="100%">
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<td class="form-item">工作名称：</td>
											<td><asp:label id="lblTaskCode" runat="server"></asp:label>&nbsp;&nbsp;<asp:label id="lblTaskName" runat="server"></asp:label></td>
											<TD class="form-item">项目名称：</TD>
											<TD><asp:label id="lblProjectName" runat="server"></asp:label></TD>
										</TR>
										<tr>
											<td class="form-item" width="20%">父工作项：</td>
											<td id="tdParentName" width="30%" runat="server"><asp:label id="lblFather" runat="server"></asp:label></td>
											<td class="form-item" width="20%">当前进度(%)：</td>
											<td width="30%"><asp:label id="lblCompletePercent" runat="server"></asp:label></td>
										</tr>
										<tr>
											<td class="form-item">工作状态：</td>
											<td><asp:label id="lblTaskStatus" runat="server"></asp:label></td>
											<TD class="form-item">重要程度：</TD>
											<td><asp:label id="lblImportantLevel" runat="server"></asp:label></td>
										</tr>
										<tr>
											<td class="form-item">类型：</td>
											<td><asp:Label id="lblRelaName" runat="server"></asp:Label></td>
											<td class="form-item">图标文件：</td>
											<td><a href="" target="_blank" runat="server" id="hrefImageFileName"><asp:Label id="lblImageFileName" runat="server"></asp:Label></a></td>
										</tr>
										<TR>
											<TD class="form-item">计划开始时间：</TD>
											<TD><asp:label id="lblPlannedStartDate" runat="server"></asp:label></TD>
											<TD class="form-item">计划结束时间：</TD>
											<TD><asp:label id="lblPlannedFinishDate" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="form-item">实际开始时间：</TD>
											<TD><asp:label id="lblActualStartDate" runat="server"></asp:label></TD>
											<TD class="form-item">实际结束时间：</TD>
											<TD><asp:label id="lblActualFinishDate" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<td class="form-item">前置工作：</td>
											<td id="tdPreTask" runat="server"></td>
											<TD class="form-item">预计完成时间：</TD>
											<TD><asp:label id="lblEarlyFinishDate" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="form-item">最后修改人：</TD>
											<TD><asp:label id="lblLastModifyUser" runat="server"></asp:label></TD>
											<TD class="form-item">最后修改时间：</TD>
											<TD><asp:label id="lblLastModifyDate" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="form-item">负责人：</TD>
											<TD><asp:label id="lblMaster" runat="server"></asp:label></TD>
											<td class="form-item">工作项部门：</td>
											<TD><asp:Label id="lblDept" runat="server"></asp:Label></TD>
										</TR>
										<TR>
											<TD class="form-item">监督人：</TD>
											<TD id="tdPreProportion" runat="server"><asp:label id="lblMonitor" runat="server"></asp:label></TD>
											<TD class="form-item" id="tdProportionText" runat="server">工作项权重：</TD>
											<TD id="tdProportionValue" runat="server"><asp:label id="lblProportion" runat="server"></asp:label></TD>
										</TR>
										<tr>
											<TD class="form-item">参与人：</TD>
											<TD><asp:label id="lblExecuter" runat="server"></asp:label></TD>
											<TD class="form-item">录入人：</TD>
											<TD><asp:label id="lblInputor" runat="server"></asp:label></TD>
										</tr>
										<TR id="trPauseReason" runat="server">
											<TD class="form-item">暂停原因：</TD>
											<TD id="tdPauseReason" colSpan="3" runat="server"><FONT face="宋体"></FONT></TD>
										</TR>
										<TR id="trCancelReason" runat="server">
											<TD class="form-item">取消原因：</TD>
											<TD id="tdCancelReason" colSpan="5" runat="server"></TD>
										</TR>
										<TR>
											<TD class="form-item">工作说明：</TD>
											<TD id="tdTaskDetail" colSpan="3" runat="server"></TD>
										</TR>
									</table>
								</td>
								<TD vAlign="top" height="100%">
									<TABLE height="100%" width="100%" border="0">
										<TR height="50%">
											<TD vAlign="top" height="50%">
												<TABLE cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="intopic">工作报告</TD>
														<td><input class="button-small" id="btnAddNewExecute" onclick="DoAddNewExecute();return false;"
																type="button" value="新增工作报告" name="AddNewExcuteButton" runat="server"></td>
													</TR>
												</TABLE>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr width="100%">
														<td width="100%">
															<div id="divExecute" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100px" runat="server"><asp:datagrid id="dgExecuteList" runat="server" CssClass="list" PageSize="1" AutoGenerateColumns="False"
																	DataKeyField="TaskExecuteCode" ShowHeader="False" Width="100%">
																	<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
																	<ItemStyle Height="5px"></ItemStyle>
																	<HeaderStyle CssClass="list-title"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="提交日期">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenExecute(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "TaskExecuteCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "ExecuteDate", "{0:yyyy-MM-dd}") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="Detail" HeaderText="内容">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="ExecutePerson" HeaderText="提交人">
																			<HeaderStyle Width="20%"></HeaderStyle>
																		</asp:BoundColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tbNoExecute" cellPadding="0" width="100%" border="0" runat="server">
													<tr align="center">
														<td colSpan="2">无工作报告</td>
													</tr>
												</table>
											</TD>
										</TR>
										<tr height="50%">
											<td vAlign="top">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic">工作指示</td>
														<td><input class="button-small" id="btnAddNewGuid" onclick="DoAddNewGuid();return false;" type="button"
																value="新增工作指示" name="btnAddNewGuid" runat="server"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr width="100%">
														<td width="100%">
															<div id="divGuid" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100px" runat="server"><asp:datagrid id="dgGuidList" runat="server" CssClass="list" PageSize="1" AutoGenerateColumns="False"
																	DataKeyField="TaskGuidCode" ShowHeader="False" Width="100%">
																	<Columns>
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="提交日期">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenTaskGuid(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "TaskGuidCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "CreateDate") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="TaskGuidContent" HeaderText="指示">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="TaskGuidPerson" HeaderText="提交人">
																			<HeaderStyle Width="20%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:ButtonColumn Text="&lt;Img border=0 width=15 highth=15 src='../images/Del.gif'&gt;" HeaderText="操作"
																			CommandName="Delete">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:ButtonColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tbNoGuid" width="100%" align="center" runat="server">
													<tr align="center">
														<td colSpan="2">无工作指示</td>
													</tr>
												</table>
											</td>
										</tr>
									</TABLE>
								</TD>
							</tr>
							<tr width="100%">
								<td colSpan="2">
									<hr width="99%" SIZE="1">
									<table width="100%" border="0">
										<tr>
											<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="50%">
												<!-- Start 工作子项 -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">工作子项</td>
														<td><input class="button-small" id="btnAddNewChild" onclick="DoAddNewChildTask();return false;"
																type="button" value="新增工作子项" name="AddNewChildButton" runat="server"></td>
														<td align="center" width="120"><input class="button-small" id="btBatchModify" onclick="DoBatchModifyTask();return false;"
																type="button" value="批量修改" name="BatchModify" runat="server"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td width="100%">
															<div id="divChildTask" style="WIDTH: 100%; HEIGHT: 100px" runat="server">
																<asp:datagrid id="dgChildTaskList" runat="server" CssClass="list" PageSize="1" AutoGenerateColumns="False"
																	DataKeyField="WBSCode" Width="100%">
																	<FooterStyle CssClass="FooterStyle"></FooterStyle>
																	<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
																	<ItemStyle Height="5px"></ItemStyle>
																	<HeaderStyle CssClass="list-title"></HeaderStyle>
																	<Columns>
																		<asp:BoundColumn DataField="StatusName" HeaderText="工作项"></asp:BoundColumn>
																		<asp:BoundColumn DataField="Master" HeaderText="责任人">
																			<HeaderStyle Width="20%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="CompletePercent" HeaderText="进度" DataFormatString="{0}%">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Proportion" HeaderText="权重"></asp:BoundColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tbNoChild" width="100%" align="center" runat="server">
													<tr align="center">
														<td colSpan="2">无工作子项</td>
													</tr>
												</table>
												<!-- End 工作子项 -->
												<!-- Start 工作项相关 -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">工作项相关</td>
														<td><input class="button-small" id="btnAddNewRelatedTask" onclick="DoAddNewRelatedTask();return false;"
																type="button" value="新增工作相关" name="AddNewRelatedButton" runat="server"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td>
															<div id="divRelateTask" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100px" runat="server"><asp:datagrid id="dgRelatedTask" runat="server" CssClass="list" PageSize="1" AutoGenerateColumns="False"
																	DataKeyField="TaskRelatedCode" Width="100%">
																	<FooterStyle CssClass="FooterStyle"></FooterStyle>
																	<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
																	<HeaderStyle CssClass="list-title"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="工作项名称">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenTask(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "RelatedWBSCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "StatusName") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="Master" HeaderText="责任人">
																			<HeaderStyle Width="20%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="CompletePercent" HeaderText="进度" DataFormatString="{0}%">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:ButtonColumn Text="&lt;Img border=0 width=15 highth=15 src='../images/cut.gif'&gt;" HeaderText="取消"
																			CommandName="Delete">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:ButtonColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tbNoRelatedTask" width="100%" align="center" runat="server">
													<tr align="center">
														<td colSpan="2">无相关工作项</td>
													</tr>
												</table>
												<!-- End 工作项相关 -->
											</td>
											<td></td>
											<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top">
												<!-- Start 工作预算 -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">工作预算</td>
														<td><input class="button-small" id="btnTaskBudget" onclick="doModifyTaskBudget();return false;"
																type="button" value="维护工作预算" name="btnTaskBudget" runat="server"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr width="100%">
														<td width="100%">
															<div id="divTaskBudget" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100px" runat="server">
																<asp:datagrid id="dgTaskBudget" runat="server" CssClass="list" PageSize="1" AutoGenerateColumns="False"
																	DataKeyField="WBSCode" Width="100%">
																	<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
																	<HeaderStyle CssClass="list-title"></HeaderStyle>
																	<FooterStyle CssClass="list-title"></FooterStyle>
																	<Columns>
																		<asp:TemplateColumn HeaderText="序号">
																			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																			<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
																			<ItemTemplate>
																				<%# Container.ItemIndex + 1 %>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="ItemName" HeaderText="款 项"></asp:BoundColumn>
																		<asp:BoundColumn DataField="PlanningPayDate" HeaderText="付款时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
																		<asp:TemplateColumn HeaderText="付款条件">
																			<ItemStyle></ItemStyle>
																			<ItemTemplate>
																				<span id="spanPayConditionHtml">
																					<%#  DataBinder.Eval(Container.DataItem, "PayConditionHtml")  %>
																				</span>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="Money" HeaderText="金 额" DataFormatString="{0:N}">
																			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Right"></ItemStyle>
																			<FooterStyle HorizontalAlign="Right"></FooterStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
																		CssClass="list-title"></PagerStyle>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tableTaskBudget" width="100%" align="center" runat="server">
													<tr align="center">
														<td colSpan="3">无预算</td>
													</tr>
												</table>
												<!-- End 工作预算 -->
												<!-- Start 合同相关 -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">合同相关</td>
														<td><input class="button-small" id="btnAddNewContract" onclick="DoAddNewContract();return false;"
																type="button" value="新增合同相关" name="AddNewContract" runat="server"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr width="100%">
														<td>
															<div id="divContract" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100px" runat="server"><asp:datagrid id="dgContractList" runat="server" CssClass="list" AutoGenerateColumns="False" DataKeyField="TaskContractCode"
																	Width="100%">
																	<FooterStyle CssClass="FooterStyle"></FooterStyle>
																	<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
																	<ItemStyle Height="5px"></ItemStyle>
																	<HeaderStyle CssClass="list-title"></HeaderStyle>
																	<Columns>
																		<asp:BoundColumn HeaderText="状态"></asp:BoundColumn>
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="合同名称">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenContract(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "ContractName") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn HeaderText="供应商" DataField="SupplierName"></asp:BoundColumn>
																		<asp:BoundColumn DataField="ContractDate" HeaderText="申请时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
																		<asp:ButtonColumn Text="&lt;Img border=0 width=15 highth=15 src='../images/cut.gif'&gt;" HeaderText="取消"
																			CommandName="Delete">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:ButtonColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tbNoContract" width="100%" align="center" runat="server">
													<tr align="center">
														<td colSpan="2">无相关合同</td>
													</tr>
												</table>
												<!-- End 合同相关 -->
												<!-- Start 文档相关 -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">文档相关</td>
														<td><input class="button-small" id="btnAddRelatDocument" onclick="DoAddNewDocument();return false;"
																type="button" value="新增文档相关" name="AddNewDocument" runat="server"> <input class="button-small" id="btnAddNewDocument" onclick="DoAddDocument();return false;"
																type="button" value="新增文档" name="btnAddNewDocument" runat="server">
														</td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td>
															<div id="divDocument" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100px" runat="server"><asp:datagrid id="dgDocumentList" runat="server" CssClass="list" PageSize="1" AutoGenerateColumns="False"
																	DataKeyField="DocumentCode" Width="100%">
																	<FooterStyle CssClass="FooterStyle"></FooterStyle>
																	<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
																	<ItemStyle Height="5px"></ItemStyle>
																	<HeaderStyle CssClass="list-title"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="文档标题">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenDocument(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "DocumentCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "Title") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="CreateDate" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
																		<asp:BoundColumn DataField="Author" HeaderText="创建人"></asp:BoundColumn>
																		<asp:ButtonColumn Text="&lt;Img border=0 width=15 highth=15 src='../images/cut.gif'&gt;" HeaderText="取消"
																			CommandName="Delete">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:ButtonColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tbNoDocument" width="100%" align="center" runat="server">
													<tr align="center">
														<td colSpan="2">无相关文档</td>
													</tr>
												</table>
												<!-- End 文档相关 -->
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="5">
					<td><FONT face="宋体"></FONT></td>
				</tr>
			</table>
			<input id="hTaskCode" type="hidden" name="hTaskCode" runat="server">
			<input id="hDocumentCode" type="hidden" name="hDocumentCode" runat="server"> <input id="hContractCode" type="hidden" name="hContractCode" runat="server">
			<input id="hAttention" type="hidden" name="hAttention" runat="server"> <input id="hAttentionStation" type="hidden" name="hAttentionStation" runat="server">
			<input id="hIsAddAttention" type="hidden" name="hIsAddAttention" runat="server"><input id="txtFlag" type="hidden" name="txtFlag" runat="server">
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
		</form>
	</body>
</HTML>
