<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.StationUserListS" CodeFile="StationUserListS.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="winload()">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
				<tr height="100%">
					<td valign="top" align="center">
						<div style="overflow:auto;width:100%;height:100%">
							<table border="0" cellpadding="0" cellspacing="0" width="100%" class="list" id="tbList">
								<tr class="list-title">
									<td width="30"></td>
									<td width="60"><input type=checkbox  id=chkAll onclick="selectAllCheck();" title="全选"></td>
									<td>岗位</td>
								</tr>
								<asp:Repeater ID="repeaterSU" Runat="server">
									<ItemTemplate>
										<tr>
											<td><img border=0 width=15 height=15 src='<%# "../Images/" + DataBinder.Eval( Container,"DataItem.ImageFileName" ) %> '>
											</td>
											<td><input type=checkbox id=chk  NameTemp='<%# DataBinder.Eval( Container,"DataItem.Name" )%>' RelationCode='<%# DataBinder.Eval( Container,"DataItem.RelationCode" )%>' AccessRangeType='<%# DataBinder.Eval( Container,"DataItem.AccessRangeType" )%>' UnitFullName='<%# RmsPM.BLL.SystemRule.GetUnitFullName((string)DataBinder.Eval( Container,"DataItem.UnitCode" ))%>' ></td>
											<td width="100%"><a title='<%# RmsPM.BLL.SystemRule.GetUnitFullName((string)DataBinder.Eval( Container,"DataItem.UnitCode" ))%>' href="#" onclick="ShowRelation(this.val, this.index)" index='<%# Container.ItemIndex + 1 %>' val='<%# DataBinder.Eval( Container,"DataItem.RelationCode" )%>'><%# DataBinder.Eval( Container,"DataItem.Name" )%>(<%# DataBinder.Eval( Container,"DataItem.UserCount" )%>)</a></td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
							</table>
						</div>
					</td>
				</tr>
				<tr style="display:none">
					<td>
						<table cellSpacing="0" cellPadding="10" width="100%" border="0" id="tableButton" runat="server">
							<tr align="center">
								<td><input class="submit" id="btnSelect" type="button" value="选 择" name="btnSelect" onclick="doSelectSU();">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"><input type="hidden" id="txtUnitCode" name="txtUnitCode" runat="server">
		</form>
		<script>
			//父窗口取本窗口所选的值
			function getSelectSU()
			{
				ar = new Array();
				var obj = document.all("chk");
				
				if (!obj) return;
				
				if ( obj[0])
				{
					for ( var i=0;i<obj.length;i++)
					{
						if ( obj[i].checked )
						{
							var aaa = new AccessRange( obj[i].RelationCode , obj[i].AccessRangeType ,obj[i].NameTemp ,obj[i].UnitFullName ) ;
							ar.push(aaa);
						}
					}
				}
				else
				{
					if ( obj.checked)
					{
						var aaa = new AccessRange( obj.RelationCode , obj.AccessRangeType ,obj.NameTemp ,obj.UnitFullName) ;
						ar.push(aaa);
					}
				}
				
				return ar;
			}
			
			function doSelectSU()
			{
				ar = GetSelectSU();
				window.parent.returnSelectedSU(ar);
			}
			
			//全选
			function selectAllCheck()
			{
				var chk = Form1.chkAll.checked;
				var obj = document.all("chk");

				if (!obj) return;

				if ( obj[0])
				{
					for ( var i=0;i<obj.length;i++)
					{
						obj[i].checked = chk;
					}
				}
				else
				{
					obj.checked = chk;
				}
			}
			
			var clickTr;
			
			//显示岗位下的用户
			function ShowRelation(code, index)
			{
				if (clickTr)
				{
					clickTr.className = "";
				}
				
				document.all.tbList.rows[index].className = "list-2";
				clickTr = document.all.tbList.rows[index];
				
				window.parent.document.all("frameUser").src ='StationUserListU.aspx?ProjectCode=' + Form1.txtProjectCode.value + '&StationCode=' + code + '&UnitCode=' + Form1.txtUnitCode.value;
			}

			function winload()
			{
				//缺省为全选
				Form1.chkAll.click();
			}			
		</script>
	</body>
</HTML>
