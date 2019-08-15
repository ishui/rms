<%@ Reference Page="~/project/wbs.aspx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSInfo" CodeFile="WBSInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UCAttention" Src="../UserControls/UCAttention.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WBSAlertStatus" Src="WBSAlertStatus.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������Ϣ</title>
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
			
			//ɾ���ýڵ��ˢ���б�
			function Delete(ParentCode)
			{
				window.opener.TreeSplitTop.updateChildNodes(ParentCode);
			}
			
			function ChangeTaskInfo(Action)
			{
				//if(CheckAuthority())
					OpenCustomWindow("WBSModify.aspx?Action=" + Action + "&WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode="+ProjectCode+"&FatherCode=<%=strFatherCode%>","",800,650);
			}
							
			//�����ӽڵ�
			function DoAddNewChildTask()
			{
				OpenCustomWindow('WBSModify.aspx?Action=Insert&WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode,'',800,600);
			}
				
			//�򿪹��������Դ��� ����
			function OpenTask(WBSCode) 
			{
				OpenFullWindow('WBSInfo.aspx?WBSCode=' + WBSCode+'&ProjectCode='+ProjectCode+'&ParentWBSCode=<%=Request.QueryString["WBSCode"]%>',"");
			}
			
			//�����ع�����
			function DoAddNewRelatedTask()
			{
				OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=2&WBSCode=<%=Request["WBSCode"]%>&ProjectCode="+ProjectCode);
			}
			
			//�򿪺�ͬ���Դ���
			function OpenContract(ContractCode)
			{
				OpenLargeWindow("../Contract/ContractInfo.aspx?ContractCode=" + ContractCode+"&ProjectCode="+ProjectCode,"",900,600);
			}
			
			//�����غ�ͬ
			function DoAddNewContract()
			{				
				OpenLargeWindow('../SelectBox/SelectContract.aspx?Status=0&ProjectCode=' + ProjectCode );
			}
			

			//�򿪹�������������
			function OpenExecute(ExecuteCode)
			{
				OpenFullWindow('WBSExecuteInfo.aspx?TaskExecuteCode=' + ExecuteCode+'&ProjectCode='+ProjectCode,'');
			}
			
			//��ӹ�������
			function DoAddNewExecute()
			{
				//if(CheckAuthority())
					OpenFullWindow('WBSExecute.aspx?ActionState=Insert&WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode+'&RefreshScript=SelectTaskExecute()','');
			}
			
			//��ӹ���ָ��
			function DoAddNewGuid()
			{
				//if(CheckAuthority())
				OpenCustomWindow('WBSGuidModify.aspx?Action=Insert&WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode,'',640,300);
			}	
			
			// �򿪹���ָ����ϸ
			function OpenTaskGuid(GuidCode)
			{
				OpenCustomWindow('WBSGuid.aspx?WBSCode=<%=Request.QueryString["WBSCode"]%>&TaskGuidCode='+GuidCode+'&ProjectCode='+ProjectCode,'',640,300);
			}		

			//�򿪸���
			function OpenDocument(DocumentCode)
			{
				OpenLargeWindow("../Document/DocumentInfo.aspx?DocumentCode=" + DocumentCode+'&ProjectCode='+ProjectCode); 
			}
			
			//����ĵ�
			function DoAddNewDocument()
			{
				//if(CheckAuthority())
					OpenLargeWindow('../SelectBox/SelectDocument.aspx?&ProjectCode='+ProjectCode);  
			}
			
			//�����ĵ�
			function DoAddDocument()
			{
				//if(CheckAuthority())
					OpenMiddleWindow("../Document/DocumentModify.aspx?&Action=Insert&Return=Task&ProjectCode="+ProjectCode);  
			}
			
			//ˢ��ҳ��
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
					alert("�Բ�����û��Ȩ�ޣ�");
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
			
			//��ʾ������Ϣ
			function OpenTask(WBSCode)
			{
				OpenFullWindow('WBSInfo.aspx?WBSCode=' + WBSCode+'&ProjectCode='+ProjectCode+'&ParentWBSCode=<%=Request.QueryString["WBSCode"]%>',"","");
			}
			
			function doModifyTaskBudget()
			{
				var projectCode = Form1.txtProjectCode.value;
				OpenFullWindow( 'TaskBudgetModify.aspx?WBSCode=<%=Request["WBSCode"]%>&ProjectCode=' + projectCode ,'ά������Ԥ��' ,"");
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
����		{
����			window.document.all.hAttention.value = userCode;	
				window.document.all.hIsAddAttention.value = "1";
				window.document.Form1.submit();
����		}
����		*/
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
				if(window.confirm('ȷʵҪɾ���ù�������'))
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
			
			//������ģ��
			function ExportTmpl()
			{
				OpenSmallWindow( 'TaskTempletOut.aspx?WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode,"ģ�嵼��" );
			}
			
			//��ģ�嵼��
			function ImportTmpl()
			{
				OpenSmallWindow( 'TaskTempletIn.aspx?WBSCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode='+ProjectCode,"ģ�嵼��" );
			}
			
			//�鿴����ͼ
			function OpenProgressChart()
			{
				OpenFullWindow("../ConstructProg/ProjectProgressInfo.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&WBSCode=<%=Request.QueryString["WBSCode"]%>&GanttType=A" , "");
			}
			// �鿴ָ����Ա�Ĺ���
			function OpenMyTask(user)
			{
				OpenFullWindow("WBSMyTask.aspx?myUserTask="+user+"&ProjectCode="+ProjectCode+"&Type=ThisWeek");
			}
			function DoBatchModifyTask()
			{
				OpenLargeWindow("WBSBatchModify.aspx?SelectCode=<%=Request.QueryString["WBSCode"]%>&ProjectCode="+ProjectCode,"�����޸�");
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
							type="button" value="�� ��" name="ModifyButton" runat="server"> <input class="button" id="btDelete" type="button" value="ɾ ��" name="btDelete" runat="server"
							onclick="if(!DeleteConfirm()) return false;" onserverclick="btDelete_ServerClick"> <input class="button" id="btClose" onclick="javascript:window.close();" type="button" value="�� ��"
							name="btClose">
						<uc1:wbsalertstatus id="myStatus" runat="server"></uc1:wbsalertstatus>
						<uc1:UCAttention id="myUCAttention" runat="server"></uc1:UCAttention>
						<input class="button" id="btAttention" onclick="DoAddAttention();return false;" type="button"
							value="�趨��ע��" runat="server" name="btAttention"> <input class="button" id="btnExportTmpl" onclick="ExportTmpl();" type="button" value="������ģ��"
							runat="server" name="btnExportTmpl"> <input class="button" id="btnImportTmpl" onclick="ImportTmpl();" type="button" value="��ģ�嵼��"
							runat="server" name="btnImportTmpl"> <input class="button" id="btnOpenProgressChart" onclick="OpenProgressChart();" type="button"
							value="����ͼ" runat="server" name="btnOpenProgressChart">
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
											<td class="form-item">�������ƣ�</td>
											<td><asp:label id="lblTaskCode" runat="server"></asp:label>&nbsp;&nbsp;<asp:label id="lblTaskName" runat="server"></asp:label></td>
											<TD class="form-item">��Ŀ���ƣ�</TD>
											<TD><asp:label id="lblProjectName" runat="server"></asp:label></TD>
										</TR>
										<tr>
											<td class="form-item" width="20%">�������</td>
											<td id="tdParentName" width="30%" runat="server"><asp:label id="lblFather" runat="server"></asp:label></td>
											<td class="form-item" width="20%">��ǰ����(%)��</td>
											<td width="30%"><asp:label id="lblCompletePercent" runat="server"></asp:label></td>
										</tr>
										<tr>
											<td class="form-item">����״̬��</td>
											<td><asp:label id="lblTaskStatus" runat="server"></asp:label></td>
											<TD class="form-item">��Ҫ�̶ȣ�</TD>
											<td><asp:label id="lblImportantLevel" runat="server"></asp:label></td>
										</tr>
										<tr>
											<td class="form-item">���ͣ�</td>
											<td><asp:Label id="lblRelaName" runat="server"></asp:Label></td>
											<td class="form-item">ͼ���ļ���</td>
											<td><a href="" target="_blank" runat="server" id="hrefImageFileName"><asp:Label id="lblImageFileName" runat="server"></asp:Label></a></td>
										</tr>
										<TR>
											<TD class="form-item">�ƻ���ʼʱ�䣺</TD>
											<TD><asp:label id="lblPlannedStartDate" runat="server"></asp:label></TD>
											<TD class="form-item">�ƻ�����ʱ�䣺</TD>
											<TD><asp:label id="lblPlannedFinishDate" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="form-item">ʵ�ʿ�ʼʱ�䣺</TD>
											<TD><asp:label id="lblActualStartDate" runat="server"></asp:label></TD>
											<TD class="form-item">ʵ�ʽ���ʱ�䣺</TD>
											<TD><asp:label id="lblActualFinishDate" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<td class="form-item">ǰ�ù�����</td>
											<td id="tdPreTask" runat="server"></td>
											<TD class="form-item">Ԥ�����ʱ�䣺</TD>
											<TD><asp:label id="lblEarlyFinishDate" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="form-item">����޸��ˣ�</TD>
											<TD><asp:label id="lblLastModifyUser" runat="server"></asp:label></TD>
											<TD class="form-item">����޸�ʱ�䣺</TD>
											<TD><asp:label id="lblLastModifyDate" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="form-item">�����ˣ�</TD>
											<TD><asp:label id="lblMaster" runat="server"></asp:label></TD>
											<td class="form-item">������ţ�</td>
											<TD><asp:Label id="lblDept" runat="server"></asp:Label></TD>
										</TR>
										<TR>
											<TD class="form-item">�ල�ˣ�</TD>
											<TD id="tdPreProportion" runat="server"><asp:label id="lblMonitor" runat="server"></asp:label></TD>
											<TD class="form-item" id="tdProportionText" runat="server">������Ȩ�أ�</TD>
											<TD id="tdProportionValue" runat="server"><asp:label id="lblProportion" runat="server"></asp:label></TD>
										</TR>
										<tr>
											<TD class="form-item">�����ˣ�</TD>
											<TD><asp:label id="lblExecuter" runat="server"></asp:label></TD>
											<TD class="form-item">¼���ˣ�</TD>
											<TD><asp:label id="lblInputor" runat="server"></asp:label></TD>
										</tr>
										<TR id="trPauseReason" runat="server">
											<TD class="form-item">��ͣԭ��</TD>
											<TD id="tdPauseReason" colSpan="3" runat="server"><FONT face="����"></FONT></TD>
										</TR>
										<TR id="trCancelReason" runat="server">
											<TD class="form-item">ȡ��ԭ��</TD>
											<TD id="tdCancelReason" colSpan="5" runat="server"></TD>
										</TR>
										<TR>
											<TD class="form-item">����˵����</TD>
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
														<TD class="intopic">��������</TD>
														<td><input class="button-small" id="btnAddNewExecute" onclick="DoAddNewExecute();return false;"
																type="button" value="������������" name="AddNewExcuteButton" runat="server"></td>
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
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�ύ����">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenExecute(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "TaskExecuteCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "ExecuteDate", "{0:yyyy-MM-dd}") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="Detail" HeaderText="����">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="ExecutePerson" HeaderText="�ύ��">
																			<HeaderStyle Width="20%"></HeaderStyle>
																		</asp:BoundColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tbNoExecute" cellPadding="0" width="100%" border="0" runat="server">
													<tr align="center">
														<td colSpan="2">�޹�������</td>
													</tr>
												</table>
											</TD>
										</TR>
										<tr height="50%">
											<td vAlign="top">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic">����ָʾ</td>
														<td><input class="button-small" id="btnAddNewGuid" onclick="DoAddNewGuid();return false;" type="button"
																value="��������ָʾ" name="btnAddNewGuid" runat="server"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr width="100%">
														<td width="100%">
															<div id="divGuid" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100px" runat="server"><asp:datagrid id="dgGuidList" runat="server" CssClass="list" PageSize="1" AutoGenerateColumns="False"
																	DataKeyField="TaskGuidCode" ShowHeader="False" Width="100%">
																	<Columns>
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�ύ����">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenTaskGuid(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "TaskGuidCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "CreateDate") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="TaskGuidContent" HeaderText="ָʾ">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="TaskGuidPerson" HeaderText="�ύ��">
																			<HeaderStyle Width="20%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:ButtonColumn Text="&lt;Img border=0 width=15 highth=15 src='../images/Del.gif'&gt;" HeaderText="����"
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
														<td colSpan="2">�޹���ָʾ</td>
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
												<!-- Start �������� -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">��������</td>
														<td><input class="button-small" id="btnAddNewChild" onclick="DoAddNewChildTask();return false;"
																type="button" value="������������" name="AddNewChildButton" runat="server"></td>
														<td align="center" width="120"><input class="button-small" id="btBatchModify" onclick="DoBatchModifyTask();return false;"
																type="button" value="�����޸�" name="BatchModify" runat="server"></td>
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
																		<asp:BoundColumn DataField="StatusName" HeaderText="������"></asp:BoundColumn>
																		<asp:BoundColumn DataField="Master" HeaderText="������">
																			<HeaderStyle Width="20%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="CompletePercent" HeaderText="����" DataFormatString="{0}%">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Proportion" HeaderText="Ȩ��"></asp:BoundColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tbNoChild" width="100%" align="center" runat="server">
													<tr align="center">
														<td colSpan="2">�޹�������</td>
													</tr>
												</table>
												<!-- End �������� -->
												<!-- Start ��������� -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">���������</td>
														<td><input class="button-small" id="btnAddNewRelatedTask" onclick="DoAddNewRelatedTask();return false;"
																type="button" value="�����������" name="AddNewRelatedButton" runat="server"></td>
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
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="����������">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenTask(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "RelatedWBSCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "StatusName") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="Master" HeaderText="������">
																			<HeaderStyle Width="20%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="CompletePercent" HeaderText="����" DataFormatString="{0}%">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:ButtonColumn Text="&lt;Img border=0 width=15 highth=15 src='../images/cut.gif'&gt;" HeaderText="ȡ��"
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
														<td colSpan="2">����ع�����</td>
													</tr>
												</table>
												<!-- End ��������� -->
											</td>
											<td></td>
											<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top">
												<!-- Start ����Ԥ�� -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">����Ԥ��</td>
														<td><input class="button-small" id="btnTaskBudget" onclick="doModifyTaskBudget();return false;"
																type="button" value="ά������Ԥ��" name="btnTaskBudget" runat="server"></td>
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
																		<asp:TemplateColumn HeaderText="���">
																			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																			<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
																			<ItemTemplate>
																				<%# Container.ItemIndex + 1 %>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="ItemName" HeaderText="�� ��"></asp:BoundColumn>
																		<asp:BoundColumn DataField="PlanningPayDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
																		<asp:TemplateColumn HeaderText="��������">
																			<ItemStyle></ItemStyle>
																			<ItemTemplate>
																				<span id="spanPayConditionHtml">
																					<%#  DataBinder.Eval(Container.DataItem, "PayConditionHtml")  %>
																				</span>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="Money" HeaderText="�� ��" DataFormatString="{0:N}">
																			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Right"></ItemStyle>
																			<FooterStyle HorizontalAlign="Right"></FooterStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
																		CssClass="list-title"></PagerStyle>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
												<table class="list" id="tableTaskBudget" width="100%" align="center" runat="server">
													<tr align="center">
														<td colSpan="3">��Ԥ��</td>
													</tr>
												</table>
												<!-- End ����Ԥ�� -->
												<!-- Start ��ͬ��� -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">��ͬ���</td>
														<td><input class="button-small" id="btnAddNewContract" onclick="DoAddNewContract();return false;"
																type="button" value="������ͬ���" name="AddNewContract" runat="server"></td>
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
																		<asp:BoundColumn HeaderText="״̬"></asp:BoundColumn>
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="��ͬ����">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenContract(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "ContractCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "ContractName") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn HeaderText="��Ӧ��" DataField="SupplierName"></asp:BoundColumn>
																		<asp:BoundColumn DataField="ContractDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
																		<asp:ButtonColumn Text="&lt;Img border=0 width=15 highth=15 src='../images/cut.gif'&gt;" HeaderText="ȡ��"
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
														<td colSpan="2">����غ�ͬ</td>
													</tr>
												</table>
												<!-- End ��ͬ��� -->
												<!-- Start �ĵ���� -->
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="intopic" width="200">�ĵ����</td>
														<td><input class="button-small" id="btnAddRelatDocument" onclick="DoAddNewDocument();return false;"
																type="button" value="�����ĵ����" name="AddNewDocument" runat="server"> <input class="button-small" id="btnAddNewDocument" onclick="DoAddDocument();return false;"
																type="button" value="�����ĵ�" name="btnAddNewDocument" runat="server">
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
																		<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="�ĵ�����">
																			<HeaderStyle Wrap="False"></HeaderStyle>
																			<ItemStyle Wrap="False"></ItemStyle>
																			<ItemTemplate>
																				<a href="#" onclick="OpenDocument(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "DocumentCode") %>'>
																					<%#  DataBinder.Eval(Container.DataItem, "Title") %>
																				</a>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="CreateDate" HeaderText="ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
																		<asp:BoundColumn DataField="Author" HeaderText="������"></asp:BoundColumn>
																		<asp:ButtonColumn Text="&lt;Img border=0 width=15 highth=15 src='../images/cut.gif'&gt;" HeaderText="ȡ��"
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
														<td colSpan="2">������ĵ�</td>
													</tr>
												</table>
												<!-- End �ĵ���� -->
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="5">
					<td><FONT face="����"></FONT></td>
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
