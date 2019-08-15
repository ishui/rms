<%@ Page Language="c#" Inherits="RmsPM.Web.WorkFlowDefinition.TaskManager" CodeFile="TaskManager.aspx.cs" %>

<%@ Register TagPrefix="igtbl1" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v5.1" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics.WebUI.UltraWebTab.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>�������</title>
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
                    ������Ϣ</td>
            </tr>
            <tr>
                <td valign="top">
                    <table id="Table3" cellspacing="7" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td style="border-right: #ededed 3px dotted; padding-right: 7px" valign="top" width="60%">
                                <table class="form" id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="form-item" nowrap width="20%">
                                            �������ƣ�</td>
                                        <td nowrap width="30%">
                                            <input class="input" id="txtTaskName" type="text" name="txtTaskName" runat="server"></td>
                                        <td class="form-item" nowrap width="20%">
                                            ������룺</td>
                                        <td nowrap width="30%">
                                            <input class="input" id="txtTaskID" type="text" name="txtDescription" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap width="20%">
                                            �������ͣ�</td>
                                        <td nowrap width="30%">
                                            <select id="sltTaskType" name="sltTaskType" runat="server">
                                                <option value="0" selected>һ��ڵ�</option>
                                                <option value="1">��ʼ</option>
                                                <option value="2">����</option>
                                                <option value="3">�������</option>
                                                <option value="4">��������</option>
                                                <option value="5">��ǩ��</option>
                                            </select>
                                        </td>
                                        <td class="form-item" nowrap>
                                            ������ʾ��</td>
                                        <td>
                                            <input class="input" id="txtTaskTitle" type="text" name="txtDescription" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap>
                                            ���̽�ɫ��</td>
                                        <td>
                                            <select id="sltTaskRole" runat="server">
                                            </select>
                                        </td>
                                        <td class="form-item" nowrap>
                                            ѡ�˷�ʽ��</td>
                                        <td nowrap>
                                            <select id="sltWayOfSelectPerson" name="sltWayOfSelectPerson" runat="server">
                                                <option value="NoSelect">����ѡ��</option>
                                                <option value="SinglePerson" selected>��Ա��ѡ</option>
                                                <option value="MultiPerson">��Ա��ѡ</option>
                                                <option value="UnLimited">����ѡ��</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap width="20%">
                                            �����޶���</td>
                                        <td nowrap width="30%">
                                            <select id="sltTaskProperty" runat="server">
                                            </select>
                                        </td>
                                        <td class="form-item" nowrap width="20%">
                                            ���ͣ�</td>
                                        <td nowrap width="30%">
                                            &nbsp;<input id="chkTaskCopy" type="checkbox" name="chkTaskCopy" runat="server">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap width="20%">
                                            ��Ҫ��֤���룺</td>
                                        <td nowrap width="60%">
                                            &nbsp;<input id="ChkCanManual" type="checkbox" name="ChkCanManual" runat="server">
                                        </td>
                                        <td class="form-item" nowrap width="20%">
                                            ���������</td>
                                        <td nowrap width="60%">
                                            &nbsp;<input class="input" id="txtOpinionType" type="text" name="txtOpinionType" runat="server">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap style="height: 31px">
                                            �������ƣ�</td>
                                        <td nowrap colspan="3" style="height: 31px">
                                            <input class="input" id="txtModuleState" style="width: 464px; height: 18px" type="text"
                                                size="72" name="txtModuleState" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap>
                                            ˵����</td>
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
                                        <igtab:Tab Text="- �� ǩ -">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td align="right" width="80">
                                                            ��ǩ���ͣ�</td>
                                                        <td align="left" width="200">
                                                            <select id="sltMeetType" runat="server">
                                                            </select>
                                                        </td>
                                                        <td align="right" width="80">
                                                            ˳���ǩ��</td>
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
                                                                    <AddNewBox Hidden="False" Prompt="���">
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
																				<igtbl:UltraGridColumn HeaderText="����������" Key="" Type="DropDownList" BaseColumnName="TaskActorType" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="����������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="������" Key="" Type="DropDownList" BaseColumnName="ActorCode" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="�����޶�" Key="" Type="DropDownList" BaseColumnName="ActorProperty" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="�����޶�"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��ѡ" Key="ChkActorNeed" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkActorNeed"></Footer>
																					<Header Key="ChkActorNeed" Caption="��ѡ"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��������" Key="" BaseColumnName="ActorModuleState" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="��������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��ѡ" Key="ActorNeed" Hidden="True" DataType="System.Boolean" BaseColumnName="ActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ActorNeed"></Footer>
																					<Header Key="ActorNeed" Caption="��ѡ"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="������" Key="ChkActorNeed" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkActorNeed"></Footer>
																					<Header Key="ChkActorNeed" Caption="������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="˳���" Key="" BaseColumnName="IOrder" DataType="System.Int32" AllowResize="Free" NullText="1" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="˳���"></Header>
																				</igtbl:UltraGridColumn><igtbl:UltraGridColumn HeaderText="����������" Key="" BaseColumnName="OpinionType" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="����������"></Header>
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
                                        <igtab:Tab Text="- �� �� -">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td align="right" width="80">
                                                            ������ʾ��</td>
                                                        <td align="left" width="200">
                                                            <input id="txtCopyTitle" type="text" runat="server"></td>
                                                        <td align="right" width="80">
                                                            �Ƿ�ȴ���</td>
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
                                                                    <AddNewBox Hidden="False" Prompt="���">
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
																				<igtbl:UltraGridColumn HeaderText="����������" Key="" Type="DropDownList" BaseColumnName="TaskActorType" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="����������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="������" Key="" Type="DropDownList" BaseColumnName="ActorCode" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="�����޶�" Key="" Type="DropDownList" BaseColumnName="ActorProperty" AllowResize="Free"
																					AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="�����޶�"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��ѡ" Key="ChkActorNeed" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkActorNeed"></Footer>
																					<Header Key="ChkActorNeed" Caption="��ѡ"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��������" Key="" BaseColumnName="ActorModuleState" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="��������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��ѡ" Key="ActorNeed" Hidden="True" DataType="System.Boolean" BaseColumnName="ActorNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ActorNeed"></Footer>
																					<Header Key="ActorNeed" Caption="��ѡ"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn Key="" Type="HyperLink" BaseColumnName="" AllowResize="Free" AllowUpdate="No">
																					<Footer Key=""></Footer>
																					<Header Key=""></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��������" Key="ChkCopyNeed" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkCopyNeed"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkCopyNeed"></Footer>
																					<Header Key="ChkCopyNeed" Caption="��������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��������" Key="CopyNeed" Hidden="True" DataType="System.Boolean" BaseColumnName="TaskActorName"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="CopyNeed"></Footer>
																					<Header Key="CopyNeed" Caption="��������"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��Ĭ����" Key="ChkActorType" Type="CheckBox" DataType="System.Boolean" BaseColumnName="ChkActorType"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ChkActorType"></Footer>
																					<Header Key="ChkActorType" Caption="��Ĭ����"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="��Ĭ����" Key="ActorType" Hidden="True" DataType="System.Boolean" BaseColumnName="ActorType"
																					AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key="ActorType"></Footer>
																					<Header Key="ActorType" Caption="��Ĭ����"></Header>
																				</igtbl:UltraGridColumn>
																				<igtbl:UltraGridColumn HeaderText="����������" Key="" BaseColumnName="OpinionType" AllowResize="Free" AllowUpdate="Yes">
																					<Footer Key=""></Footer>
																					<Header Key="" Caption="����������"></Header>
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
                                            <input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnSave" runat="server"
                                                onserverclick="btnSave_ServerClick">
                                            <input class="submit" id="btnDelete" onclick="if(!confirm('�Ƿ�ȷ��ɾ�� ��')) return false;"
                                                type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">
                                            <input class="submit" id="btnCancel" onclick="window.close();" type="button" value="ȡ ��"
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
            value="ɾ����ǩ��Ա" onserverclick="btnDelActor_ServerClick">
        <input type="button" style="display: none" id="btnDelActorCopy" name="btnDelActorCopy"
            runat="server" value="ɾ�����ͳ�Ա" onserverclick="btnDelActorCopy_ServerClick">
    </form>

    <script language="javascript">
			function addNewTaskActor()
			{
				OpenLargeWindow('../SelectBox/SelectSUMain.aspx?ReturnFunc=SelectReturn','ѡ��ִ����');
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
