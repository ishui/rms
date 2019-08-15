<%@ Page Language="c#" Inherits="RmsPM.Web.WorkFlowDefinition.TaskManager" CodeFile="TaskManager.aspx.cs" %>

<%@ Register TagPrefix="igtbl1" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v5.1" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics.WebUI.UltraWebTab.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>任务管理</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="Table2" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    任务信息</td>
            </tr>
            <tr>
                <td valign="top">
                    <table id="Table3" cellspacing="7" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td style="border-right: #ededed 3px dotted; padding-right: 7px" valign="top" width="60%">
                                <table class="form" id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="form-item" nowrap width="20%">
                                            任务名称：</td>
                                        <td nowrap width="30%">
                                            <input class="input" id="txtTaskName" type="text" name="txtTaskName" runat="server"></td>
                                        <td class="form-item" nowrap width="20%">
                                            任务代码：</td>
                                        <td nowrap width="30%">
                                            <input class="input" id="txtTaskID" type="text" name="txtDescription" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap width="20%">
                                            任务类型：</td>
                                        <td nowrap width="30%">
                                            <select id="sltTaskType" name="sltTaskType" runat="server">
                                                <option value="0" selected>一般节点</option>
                                                <option value="1">开始</option>
                                                <option value="2">结束</option>
                                                <option value="3">并流起点</option>
                                                <option value="4">并流交点</option>
                                                <option value="5">会签点</option>
                                            </select>
                                        </td>
                                        <td class="form-item" nowrap>
                                            任务提示：</td>
                                        <td>
                                            <input class="input" id="txtTaskTitle" type="text" name="txtDescription" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap>
                                            流程角色：</td>
                                        <td>
                                            <select id="sltTaskRole" runat="server">
                                            </select>
                                        </td>
                                        <td class="form-item" nowrap>
                                            选人方式：</td>
                                        <td nowrap>
                                            <select id="sltWayOfSelectPerson" name="sltWayOfSelectPerson" runat="server">
                                                <option value="NoSelect">不用选择</option>
                                                <option value="SinglePerson" selected>人员单选</option>
                                                <option value="MultiPerson">人员多选</option>
                                                <option value="UnLimited">任意选择</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap width="20%">
                                            符合限定：</td>
                                        <td nowrap width="30%">
                                            <select id="sltTaskProperty" runat="server">
                                            </select>
                                        </td>
                                        <td class="form-item" nowrap width="20%">
                                            抄送：</td>
                                        <td nowrap width="30%">
                                            &nbsp;<input id="chkTaskCopy" type="checkbox" name="chkTaskCopy" runat="server">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap width="20%">
                                            需要验证密码：</td>
                                        <td nowrap width="60%">
                                            &nbsp;<input id="ChkCanManual" type="checkbox" name="ChkCanManual" runat="server">
                                        </td>
                                        <td class="form-item" nowrap width="20%">
                                            辅助控制项：</td>
                                        <td nowrap width="60%">
                                            &nbsp;<input class="input" id="txtOpinionType" type="text" name="txtOpinionType" runat="server">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap style="height: 31px">
                                            辅助控制：</td>
                                        <td nowrap colspan="3" style="height: 31px">
                                            <input class="input" id="txtModuleState" style="width: 464px; height: 18px" type="text"
                                                size="72" name="txtModuleState" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap>
                                            说明：</td>
                                        <td nowrap colspan="3">
                                            <input class="input" id="txtDescription" style="width: 464px; height: 18px" type="text"
                                                size="72" name="txtDescription" runat="server"></td>
                                    </tr>
                                </table>
                                <br>
                                <igtab:UltraWebTab ID="UltraWebTab1" runat="server" Width="100%" ImageDirectory="../Images/infragistics/images/"
                                    JavaScriptFileName="../Images/infragistics/20051/scripts/ig_webtab.js" JavaScriptFileNameCommon="../Images/infragistics/20051/scripts/ig_shared.js"
                                    Height="30px">
                                    <Tabs>
                                        <igtab:Tab Text="- 会 签 -">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td align="right" width="80">
                                                            会签类型：</td>
                                                        <td align="left" width="200">
                                                            <select id="sltMeetType" runat="server">
                                                            </select>
                                                        </td>
                                                        <td align="right" width="80">
                                                            顺序会签：</td>
                                                        <td align="left" width="*">
                                                            <input id="chkMeetOrder" type="checkbox" runat="server"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <igtbl1:UltraWebGrid ID="UltraWebGrid3" runat="server" Width="100%" Height="300px">
                                                                <DisplayLayout JavaScriptFileName="../Images/infragistics/20051/scripts/ig_WebGrid.js"
                                                                    StationaryMargins="HeaderAndFooter" AutoGenerateColumns="False" AllowAddNewDefault="Yes"
                                                                    AllowSortingDefault="OnClient" JavaScriptFileNameCommon="../Images/infragistics/20051/Scripts/ig_shared.js"
                                                                    RowHeightDefault="20px" Version="4.00" HeaderClickActionDefault="SortSingle"
                                                                    BorderCollapseDefault="Separate" Name="UltraWebTab1UltraWebGrid3" TableLayout="Fixed"
                                                                    CellClickActionDefault="Edit" AllowUpdateDefault="Yes">
                                                                    <AddNewBox Hidden="False" Prompt="添加">
                                                                        <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																			</Style>
                                                                    </AddNewBox>
                                                                    <Pager>
                                                                        <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																			</Style>
                                                                    </Pager>
                                                                    <HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
                                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                                    </HeaderStyleDefault>
                                                                    <FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
                                                                        Height="300px">
                                                                    </FrameStyle>
                                                                    <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                                    </FooterStyleDefault>
                                                                    <EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
                                                                    </EditCellStyleDefault>
                                                                    <RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
                                                                        <Padding Left="3px"></Padding>
                                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                                    </RowStyleDefault>
                                                                    <ImageUrls ImageDirectory="../Images/infragistics//Images/"></ImageUrls>
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand>
                                                                        <columns>
																				<igtbl:UltraGridColumn HeaderText="Code" Key="" Hidden="True" BaseColumnName="TaskActorCode">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="Code"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="参与者类型" Key="" Type="DropDownList" BaseColumnName="TaskActorType" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="参与者类型"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="参与者" Key="" Type="DropDownList" BaseColumnName="ActorCode" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="参与者"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="符合限定" Key="" Type="DropDownList" BaseColumnName="ActorProperty" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="符合限定"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="必选" Key="ChkActorNeed" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkActorNeed"></Footer>
																					<Header Key="ChkActorNeed" Caption="必选"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="辅助控制" Key="" BaseColumnName="ActorModuleState" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="辅助控制"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="必选" Key="ActorNeed" Hidden="True" DataType="System.Boolean" BaseColumnName="ActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ActorNeed"></Footer>
																					<Header Key="ActorNeed" Caption="必选"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="允许发送" Key="ChkActorNeed" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkActorNeed"></Footer>
																					<Header Key="ChkActorNeed" Caption="允许发送"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="顺序号" Key="" BaseColumnName="IOrder" DataType="System.Int32" AllowResize="Free" NullText="1" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="顺序号"></Header>
																				</igtbl:UltraGridColumn><igtbl:UltraGridColumn HeaderText="辅助控制项" Key="" BaseColumnName="OpinionType" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="辅助控制项"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn Key="" Type="HyperLink" BaseColumnName="" AllowResize="Free" AllowUpdate="No">
																					<Footer Key=""></Footer>
																					<Header Key=""></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn Key="" Hidden="True" BaseColumnName="TaskActorName">
																					<Footer Key=""></Footer>
																					<Header Key=""></Header>
																				</igtbl:UltraGridColumn>
																				
																			</columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl1:UltraWebGrid></td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </igtab:Tab>
                                        <igtab:Tab Text="- 抄 送 -">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td align="right" width="80">
                                                            抄送提示：</td>
                                                        <td align="left" width="200">
                                                            <input id="txtCopyTitle" type="text" runat="server"></td>
                                                        <td align="right" width="80">
                                                            是否等待：</td>
                                                        <td align="left" width="*">
                                                            <input id="chkWaitForCopy" type="checkbox" runat="server"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <igtbl1:UltraWebGrid ID="UltraWebGrid3" runat="server" Width="100%" Height="300px">
                                                                <DisplayLayout JavaScriptFileName="../Images/infragistics/20051/scripts/ig_WebGrid.js"
                                                                    StationaryMargins="HeaderAndFooter" AutoGenerateColumns="False" AllowAddNewDefault="Yes"
                                                                    AllowSortingDefault="OnClient" JavaScriptFileNameCommon="../Images/infragistics/20051/Scripts/ig_shared.js"
                                                                    RowHeightDefault="20px" Version="4.00" HeaderClickActionDefault="SortSingle"
                                                                    BorderCollapseDefault="Separate" Name="UltraWebTab1UltraWebGrid3" TableLayout="Fixed"
                                                                    CellClickActionDefault="Edit" AllowUpdateDefault="Yes">
                                                                    <AddNewBox Hidden="False" Prompt="添加">
                                                                        <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																			</Style>
                                                                    </AddNewBox>
                                                                    <Pager>
                                                                        <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																			</Style>
                                                                    </Pager>
                                                                    <HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
                                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                                    </HeaderStyleDefault>
                                                                    <FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
                                                                        Height="300px">
                                                                    </FrameStyle>
                                                                    <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                                    </FooterStyleDefault>
                                                                    <EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
                                                                    </EditCellStyleDefault>
                                                                    <RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
                                                                        <Padding Left="3px"></Padding>
                                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                                    </RowStyleDefault>
                                                                    <ImageUrls ImageDirectory="../Images/infragistics//Images/"></ImageUrls>
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand>
                                                                        <columns>
																				<igtbl:UltraGridColumn HeaderText="Code" Key="" Hidden="True" BaseColumnName="TaskActorCode">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="Code"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="参与者类型" Key="" Type="DropDownList" BaseColumnName="TaskActorType" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="参与者类型"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="参与者" Key="" Type="DropDownList" BaseColumnName="ActorCode" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="参与者"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="符合限定" Key="" Type="DropDownList" BaseColumnName="ActorProperty" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="符合限定"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="必选" Key="ChkActorNeed" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkActorNeed"></Footer>
																					<Header Key="ChkActorNeed" Caption="必选"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="辅助控制" Key="" BaseColumnName="ActorModuleState" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="辅助控制"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="必选" Key="ActorNeed" Hidden="True" DataType="System.Boolean" BaseColumnName="ActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ActorNeed"></Footer>
																					<Header Key="ActorNeed" Caption="必选"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn Key="" Type="HyperLink" BaseColumnName="" AllowResize="Free" AllowUpdate="No">
																					<Footer Key=""></Footer>
																					<Header Key=""></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="继续抄送" Key="ChkCopyNeed" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkCopyNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkCopyNeed"></Footer>
																					<Header Key="ChkCopyNeed" Caption="继续抄送"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="继续抄送" Key="CopyNeed" Hidden="True" DataType="System.Boolean" BaseColumnName="TaskActorName"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="CopyNeed"></Footer>
																					<Header Key="CopyNeed" Caption="继续抄送"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="静默抄送" Key="ChkActorType" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkActorType"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkActorType"></Footer>
																					<Header Key="ChkActorType" Caption="静默抄送"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="静默抄送" Key="ActorType" Hidden="True" DataType="System.Boolean" BaseColumnName="ActorType"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ActorType"></Footer>
																					<Header Key="ActorType" Caption="静默抄送"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="辅助控制项" Key="" BaseColumnName="OpinionType" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="辅助控制项"></Header>
																				</igtbl:UltraGridColumn>
																			</columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl1:UltraWebGrid></td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </igtab:Tab>
                                    </Tabs>
                                </igtab:UltraWebTab>
                                <br>
                                <table id="Table5" width="100%">
                                    <tr>
                                        <td align="center">
                                            <input class="submit" id="btnSave" type="button" value="确 定" name="btnSave" runat="server"
                                                onserverclick="btnSave_ServerClick">
                                            <input class="submit" id="btnDelete" onclick="if(!confirm('是否确定删除 ？')) return false;"
                                                type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">
                                            <input class="submit" id="btnCancel" onclick="window.close();" type="button" value="取 消"
                                                name="btnCancel" runat="server">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input type="hidden" id="DelObjectCode" name="DelObjectCode" runat="server">
        <input type="button" style="display: none" id="btnDelActor" name="btnDelActor" runat="server"
            value="删除会签成员" onserverclick="btnDelActor_ServerClick">
        <input type="button" style="display: none" id="btnDelActorCopy" name="btnDelActorCopy"
            runat="server" value="删除抄送成员" onserverclick="btnDelActorCopy_ServerClick">
    </form>

    <script language="javascript">
			function addNewTaskActor()
			{
				OpenLargeWindow('../SelectBox/SelectSUMain.aspx?ReturnFunc=SelectReturn','选择执行人');
			}
			
			function SelectReturn(userCodes,userNames,stationCodes,stationNames)
			{
				Form1.txtUserCodes.value = userCodes;
				Form1.txtStationCodes.value = stationCodes;
				Form1.btnRefresh.click();
			}
			function deleteActor(ActorCode,CopyFlag)
			{
			    document.Form1.DelObjectCode.value = ActorCode;
			    if(CopyFlag == "0")
			    {
			        document.Form1.btnDelActor.click();
			    }
			    else
			    {
			        document.Form1.btnDelActorCopy.click();
			    }
			}
		
    </script>

</body>
</html>
