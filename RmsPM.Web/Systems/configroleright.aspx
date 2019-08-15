<%@ Page language="c#" Inherits="RmsPM.Web.Systems.ConfigRoleRight" CodeFile="ConfigRoleRight.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>配置角色权限</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">配置角色权限</td>
				</tr>
				<tr>
					<td vAlign="top">
						<asp:DataList id="dtlMainFunction" runat="server" RepeatDirection="Horizontal" HorizontalAlign="Left"
							GridLines="Horizontal" RepeatColumns="4" Width="100%">
							<ItemTemplate>
								<a href='##' onclick='showMainClass(mainCode);' mainCode='<%# DataBinder.Eval(Container.DataItem, "FunctionStructureCode") %>' >
									<%# DataBinder.Eval(Container.DataItem, "FunctionStructureName") %>
								</a>
							</ItemTemplate>
						</asp:DataList>
					</td>
				</tr>
				<tr height="100%" id="trOperator" runat="server">
					<td valign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="1" class="table">
							<tr>
								<td>编号</td>
								<td>名称</td>
								<td>说明</td>
								<td>项目特别说明</td>
							</tr>
							<asp:repeater id="rptFunction" runat="server">
								<ItemTemplate>
									<tr>
										<td><b><%# DataBinder.Eval(Container.DataItem, "ShowCode") %>
												<%# DataBinder.Eval(Container.DataItem, "functionStructureCode") %>
												<input type="checkbox" id='chkA' onclick='selectAll(this);'  value='<%# DataBinder.Eval(Container.DataItem, "FunctionStructureCode") %>' >
											</b>
										</td>
										<td>
											<%# DataBinder.Eval(Container.DataItem, "FunctionStructureName") %>
										</td>
										<td><%# DataBinder.Eval(Container.DataItem, "Description") %></td>
										<td><%# DataBinder.Eval(Container.DataItem, "ProjectSpecialDescription") %></td>
								</ItemTemplate>
							</asp:repeater></table>
					</td>
				</tr>
				<tr id="trSave">
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" onclick=" if ( !doGetSelectCode() ) return false; "
										type="button" value="保 存" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp; <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtAllCode" runat="server"> <input type="hidden" id="txtACode" runat="server">
			<script language="javascript">
<!--
/*
	function SelectAll(){
		var checked;
		var i;
		var count = parseInt(Form1.txtModuleCount.value) + 1;

		chk = document.all.chkAll;
		checked = chk.checked;

		for (i=1;i<=count;i++)
		{
			obj = document.all("dgList:_ctl" + i + ":chk");

			if (obj)
			{
				obj.checked=checked;
			}
		}
	}

	function doCheck( thisObj )
	{
		var checked = thisObj.checked;
		var code = thisObj.value;
		var i;
		var count = parseInt(Form1.txtModuleCount.value) + 1;

		for (i=1;i<=count;i++)
		{
			var obj = document.all("dgList:_ctl" + i + ":chk");
			if (obj)
			{
				var v = obj.value;
				if ( v.length > code.length && v.substring(0,code.length)==code )
					obj.checked=checked;
			}
		}
	}
*/
/*
	function SelectAll(){
		var checked;

		chk = document.all.chkAll;
		checked = chk.checked;
		
		var obj=document.all("chk");

		if (obj == null)
			return false;
			
		
		if(obj[0]){
			for(var i=0;i<obj.length;i++){
				obj[i].checked=checked;
			}
		}else{
			if(obj){
				obj.checked=checked;
			}
		}
	}
*/

	


	IniTrSave();
	IniCheck();
	
	function IniTrSave()
	{
		var code = '<%=Request["MainCode"]%>';
		if ( code == '' )
			document.all("trSave").style.display='none';
	}
	
	function IniCheck()
	{
		var codes = Form1.txtACode.value.split(',');
		var iCount = codes.length;
		
		var obj=document.all("chkA");
		if (obj == null)
			return ;
		
		for ( var j=0;j<iCount;j++)
		{
			var code = codes[j];
			if ( code != "" )
			{
				if(obj[0]){
					for(var i=0;i<obj.length;i++){
						if (obj[i].value==code)
						{
							obj[i].checked = true;
						}
					}
				}else{
					if(obj){
						if (obj.value==code)
						{
							obj.checked = true;
						}
					}
				}
			}
		}
	}


	function selectAll(obj)
	{
		var tempCode = obj.value;

		var check = obj.checked;
		var cl = tempCode.length;
		
		var obj=document.all("chkA");
		if (obj[0] == null)
			return ;

		for(var i=0;i<obj.length;i++)
		{
			var ccc = obj[i].value;
			if ( ccc.length > cl )
			{
				if ( ccc.substring(0,cl) == tempCode )
				{
					obj[i].checked = check;
				}
			}
			else if ( (ccc.length < cl) && check==true )
			{
				if ( tempCode.substring(0,ccc.length) == ccc )
				{
					obj[i].checked = true;
				}
			}
		}
	}

	function doGetSelectCode()
	{
		var my_array = new Array();
		var obj=document.all("chkA");
		
		if (obj == null)
			return true;

		if(obj[0]){
			for(var i=0;i<obj.length;i++){
				if (obj[i].checked)
				{
					my_array.push(obj[i].value);
				}
			}
		}else{
			if(obj){
				if (obj.checked)
				{
					my_array.push(obj.value);
				}
			}
		}
		
		Form1.txtACode.value = my_array.join(',');
		return true;
	}

	function showMainClass( mainCode)
	{
		window.navigate( 'ConfigRoleRight.aspx?RoleCode=<%=Request["RoleCode"]%>&MainCode=' + mainCode );
	}

//-->
			</script>
		</form>
	</body>
</HTML>
