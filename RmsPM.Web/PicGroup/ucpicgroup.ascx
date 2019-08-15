<%@ Control Language="c#" Inherits="RmsPM.Web.PicGroup.UCPicGroup" CodeFile="UCPicGroup.ascx.cs" %>
<TABLE class="list" id="Table1" cellSpacing="0" cellPadding="4" width="100%" height="100%" border="1">
	<script language="javascript">
<!--
	<%=strClientJS%>
	
	var theRootPath = "<%=strRootPath%>";
	var theMasterType = "<%=strMasterType%>";
	var theMasterCode = "<%=strMasterCode%>";
	var thcCurrPGItems = new Array();
	var theCurrPGCode = null;
	var thcCurrPGItemIndex = null;
	var IsAutoPlay = false;
	var thePrivateMaxWidth = 120;
	var thePrivateMaxHeight = 160;
	

	function thePicPrivateOnLoad(obj){
		if( thePrivateMaxWidth<obj.width ){
			obj.width = thePrivateMaxWidth;
		}
		
		setTimeout('funShowNext();',4500);
	}
	
	function thePicPrivateShow(imgArr){
		var imgobj = document.all('PicPrivate');
		
		imgobj.src = theRootPath + "./PicGroup/PicShow.aspx?PicCode=" + imgArr[0];
		if( imgArr[1]>imgArr[2] && thePrivateMaxWidth<imgArr[1] ){
			//宽大于高
			imgobj.width = thePrivateMaxWidth;
			imgobj.height = thePrivateMaxWidth * (imgArr[2]/imgArr[1]);
		}else if( imgArr[1]<imgArr[2] && thePrivateMaxHeight<imgArr[2] ){
			//高大于宽
			imgobj.width = thePrivateMaxHeight * (imgArr[1]/imgArr[2]);
			imgobj.height = thePrivateMaxHeight;
		}else{
			imgobj.width = imgArr[1];
			imgobj.height = imgArr[2];
		}
	}
	
	function funShowNext(){
		var imgobj = document.all('PicPrivate');
		
		if( IsAutoPlay && 1<thcCurrPGItems.length && (thcCurrPGItems.length-1)>thcCurrPGItemIndex ){
			thcCurrPGItemIndex++;
			thePicPrivateShow(thcCurrPGItems[thcCurrPGItemIndex]);
		}
	}
	
	function doThePGWork(obj){
		var doWhat = obj.id;
		var strURL = "";
		var NowTime = new Date();
		
		if( "btnCreatePicGroup"==doWhat ){
			//创建
			
			strURL = theRootPath + "./PicGroup/PicGroupModify.aspx?Act=CreatePicGroup";
			
			strURL += '&MasterType=' + theMasterType;
			strURL += '&MasterCode=' + theMasterCode;
			
			strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
			var theWin = OpenLargeWindow(strURL,doWhat);
			theWin.focus();
		}
		else if( "btnModifyPicGroup"==doWhat ){
			//编辑
			
			if( null==theCurrPGCode ){
				alert("请选择要编辑的图片集！");
				return;
			}
			
			strURL = theRootPath + "./PicGroup/PicGroupModify.aspx?Act=ModifyPicGroup";
			
			strURL += '&MasterType=' + theMasterType;
			strURL += '&MasterCode=' + theMasterCode;
			strURL += '&PBSPicGroupCode=' + theCurrPGCode;
			
			strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
			var theWin = OpenLargeWindow(strURL,doWhat);
			theWin.focus();
		}
		else if( "btnPlay"==doWhat ){
			//播放
			
			if( null==thcCurrPGItemIndex ){
				return;
			}
			
			if( 1>=thcCurrPGItems.length ){
				return;
			}
			
			IsAutoPlay = true;
			setTimeout('funShowNext();',4000);
		}
		else if( "btnStop"==doWhat ){
			//停止
			
			IsAutoPlay = false;
		}
		else if( "btnBack"==doWhat ){
			//上一张

			if( null==thcCurrPGItemIndex ){
				return;
			}
			
			if( 0<thcCurrPGItemIndex ){
				thcCurrPGItemIndex--;
				thePicPrivateShow(thcCurrPGItems[thcCurrPGItemIndex]);
			}
		}
		else if( "btnFw"==doWhat ){
			//下一张
			

			if( null==thcCurrPGItemIndex ){
				return;
			}
			
			if( (thcCurrPGItems.length-1)>thcCurrPGItemIndex ){
				thcCurrPGItemIndex++;
				thePicPrivateShow(thcCurrPGItems[thcCurrPGItemIndex]);
			}
		}
		else if( "btnFull"==doWhat ){
			//显示大尺寸
			
			if( null==theCurrPGCode ){
				alert('请选择要浏览的图片集！');
				return;
			}
			
			if( 0==thcCurrPGItems.length ){
				alert('此图片集没有图片！');
				return;
			}
			
			strURL = theRootPath + "./PicGroup/PicList.aspx?PBSPicGroupCode=" + theCurrPGCode;
			
			strURL += "";
			
			strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
			var theWin = OpenLargeWindow(strURL,doWhat);
			theWin.focus();
		}
	}
	
	function clickPGItem(obj){
		
		obj.bgColor="#ECF4FA";
		
		for(i=0;i<arrPicGroup.length;i++){
			if( obj.id!="PicGroupTable"+arrPicGroup[i][0] ){
				document.all( "PicGroupTable"+arrPicGroup[i][0] ).bgColor="";
			}
			else{
				theCurrPGCode = arrPicGroup[i][0];
				thcCurrPGItems = arrPicGroup[i][1];
			}
		}
		
		if( 0<thcCurrPGItems.length ){
			thcCurrPGItemIndex = 0;
			thePicPrivateShow(thcCurrPGItems[thcCurrPGItemIndex]);
		}
		else{
			thcCurrPGItemIndex = null;
			document.all('PicPrivate').src = theRootPath + "./Images/PicGroup/splash.gif";
			document.all('PicPrivate').width = 67;
			document.all('PicPrivate').height = 58;
		}
	}
	
//-->
	</script>
	<TR class="list-title">
		<TD noWrap>&nbsp;<a id="btnCreatePicGroup" href="#" onclick="doThePGWork(this);return false;">创建</a>&nbsp;<a id="btnModifyPicGroup" href="#" onclick="doThePGWork(this);return false;">编辑</a></TD>
	</TR>
	<TR>
		<TD style="height:100%">
			<TABLE id="Table2" cellSpacing="0" cellPadding="4" width="100%" height="100%" align="center" border="0">
				<TR>
					<TD vAlign="middle" noWrap align="center" colSpan="2" style="height:170px"><IMG id="PicPrivate" alt="" src="<%=strRootPath%>Images/PicGroup/splash.gif" border="0" onload="thePicPrivateOnLoad(this);"></TD>
				</TR>
				<TR>
					<TD noWrap align="left"><a id="btnPlay" href="#" onclick="doThePGWork(this);return false;"><IMG alt="播放" src="<%=strRootPath%>Images/PicGroup/button_play.gif" border="0"></a>&nbsp;<a id="btnStop" href="#" onclick="doThePGWork(this);return false;"><IMG alt="停止" src="<%=strRootPath%>Images/PicGroup/button_stop.gif" border="0"></a>&nbsp;<a id="btnBack" href="#" onclick="doThePGWork(this);return false;"><IMG alt="上一张" src="<%=strRootPath%>Images/PicGroup/button_back.gif" border="0"></a>&nbsp;<a id="btnFw" href="#" onclick="doThePGWork(this);return false;"><IMG alt="下一张" src="<%=strRootPath%>Images/PicGroup/button_fw.gif" border="0"></a></TD>
					<TD noWrap align="right"><a id="btnFull" href="#" onclick="doThePGWork(this);return false;"><IMG alt="查看完全尺寸图片。" src="<%=strRootPath%>Images/PicGroup/button_full.gif" border="0"></a></TD>
				</TR>
				<TR>
					<TD colSpan="2" valign="top" style="height:100%">
						<div style="overflow: auto; WIDTH: 100%; HEIGHT: 100%">
							<asp:DataGrid id="dgList" runat="server" AutoGenerateColumns="False" CellPadding="5" Width="100%"
								GridLines="None" ShowHeader="False" HorizontalAlign="Center">
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<table id='PicGroupTable<%# DataBinder.Eval(Container, "DataItem.PBSPicGroupCode") %>' border="1" cellpadding="4" cellspacing="0" width="100%" align="center" bordercolor="#A6C6DD"
												style="border-collapse: collapse;cursor:hand;" onclick="clickPGItem(this);return false;">
												<tr>
													<td align="center" valign="middle">
														<b>
															<%# DataBinder.Eval(Container, "DataItem.GroupName") %>
														</b>
														<br>
														<%# DataBinder.Eval(Container, "DataItem.PicNumber") %>
														&nbsp;张图片
													</td>
												</tr>
											</table>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
							</asp:DataGrid>
						</div>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<Script language="JavaScript" type="text/javascript">
<!--

if( 0<arrPicGroup.length ){
	document.all( 'PicGroupTable' + arrPicGroup[0][0] ).click();
}

//-->
</Script>
