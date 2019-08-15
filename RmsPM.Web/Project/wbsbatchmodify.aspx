<%@ Reference Page="~/project/wbsstatus.aspx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Project.WBSBatchModify" CodeFile="WBSBatchModify.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�����޸Ĺ���������Ϣ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
		function doCheck()
		{
			var num = document.all("dgDetailData").rows.length;			
			for(i=2;i<=num;i++)
			{
				var taskname = document.all("dgDetailData__ctl"+i+"_txtTaskName").value;
				var percent  = document.all("dgDetailData__ctl"+i+"_txtPercent").value;
				
				if(taskname.length==0)
				{					
					document.all("dgDetailData__ctl"+i+"_txtTaskName").select();
					document.all("dgDetailData__ctl"+i+"_txtTaskName").focus();
					alert("�����빤��������");
					return false;
				}
				if(percent.length==0)
				{					
					document.all("dgDetailData__ctl"+i+"_txtPercent").select();
					document.all("dgDetailData__ctl"+i+"_txtPercent").focus();
					alert("��������ɽ���");
					return false;
				}
			}
			return true;
		}
		function CheckNum(obj)
		{
			if(obj.value.length>0)
			{
				if(obj.value.match("^[0-9]+(\.[0-9]+)?$")==null)
				{
					obj.select();
					obj.focus();
					alert("��������������");
					obj.select();
					return false;
				}
			}
			return true;	
		}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr vAlign="top" align="center">
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" align="center" background="../images/topic_bg.gif" height="25">�����޸Ĺ���������Ϣ</td>
							</tr>
						</TABLE>
						<asp:datagrid id="dgDetailData" runat="server" AutoGenerateColumns="False" CssClass="list" Width="100%"
							DataKeyField="WBSCode">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="����������">
									<ItemTemplate>
										<asp:TextBox CssClass="input" id="txtTaskName" runat="server" value='<%#  DataBinder.Eval(Container.DataItem, "TaskName")  %>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="��ɽ���">
									<ItemTemplate>
										<asp:TextBox CssClass="input" id="txtPercent" runat="server" Width="30" onblur="CheckNum(this);" value='<%#  DataBinder.Eval(Container.DataItem, "CompletePercent")  %>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ȩ��">
									<ItemTemplate>
										<asp:TextBox CssClass="input" id="txtProportion" runat="server" Width="30" onblur="CheckNum(this);" value='<%#  DataBinder.Eval(Container.DataItem, "Proportion")  %>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="�ƻ���ʼʱ��">
									<ItemTemplate>
										<cc3:calendar id="dtbPlannedStartDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "PlannedStartDate") %>'>
										</cc3:calendar>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="�ƻ�����ʱ��">
									<ItemTemplate>
										<cc3:calendar id="dtbPlannedFinishDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "PlannedFinishDate") %>'>
										</cc3:calendar>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="ʵ�ʿ�ʼʱ��">
									<ItemTemplate>
										<cc3:calendar id="dtbActualStartDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "ActualStartDate") %>'>
										</cc3:calendar>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="ʵ�ʽ���ʱ��">
									<ItemTemplate>
										<cc3:calendar id="dtbActualFinishDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "ActualFinishDate") %>'>
										</cc3:calendar>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid>
						<P></P>
						<input class="submit" id="btnSave" type="submit" onclick='if(!doCheck()) return false;'
							value="����" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <!--   -->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
