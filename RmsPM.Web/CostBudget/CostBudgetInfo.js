var headCount = 2;

var LastSelectedRow;
var LastSelectedRowClass;

//返回
function GoBack()
{
	if (document.all.txtFromUrl.value == "")
	{
		window.history.go(-1);
//		window.location.href = "PaymentList.aspx?ProjectCode=" + document.all.txtProjectCode.value;
	}
	else
	{
		window.history.go(-1);
//		window.location.href = document.all.txtFromUrl.value;
	}
}

//刷新
function Refresh()
{
	if (!BeforePost()) return false;
	document.all.btnRefresh.click();
}

//刷新目标费用
function RefreshTarget()
{
	if (!BeforePost()) return false;
	document.all.btnRefreshTarget.click();
}

//刷新预留金额
function RefreshBalance()
{
	if (!BeforePost()) return false;
	document.all.btnRefreshBalance.click();
}

//刷新合同计划
function RefreshPurchase()
{
	if (!BeforePost()) return false;
	document.all.btnRefreshPurchase.click();
}

//刷新合同预算
function RefreshCostBudgetContract()
{
	if (!BeforePost()) return false;
	document.all.btnRefreshCostBudgetContract.click();
}

//提取费用项编号
function PickupCostCode(key)
{
	var CostCode = key;
	
	if (key == "R_0") CostCode = "";
	
	return CostCode;
}

//提取招投标编号
function PickupBiddingCode(key)
{
    //key：Bidding_100001#100002
	var BiddingCode = key.substr("Bidding_".length);
	
	var arr = BiddingCode.split("#");
	if (arr.length > 0)
	{
    	BiddingCode = arr[0];
    }
    
    return BiddingCode;
}

//按行的id取其中的费用项编号
function GetCostCodeById(id)
{
    var CostCode = "";
    
	if ((id.indexOf("TreeNode_C_") >= 0) || (id.indexOf("TreeNode_B_") >= 0)) //合同明细行或预留金额行
	{
        var AllCode = id.substr("TreeNode_C_".length, id.length - "TreeNode_C_".length);
        
        if (AllCode.indexOf(":") >= 0)
            CostCode = AllCode.substr(0, AllCode.indexOf(":"));
        else
            CostCode = AllCode;
    }
    else if (id.indexOf("TreeNode_") >= 0) //费用项行
    {
        CostCode = id.substr("TreeNode_".length);
    }
    
    return CostCode;
}

//修改动态费用
function ModifyDynamic()
{
	var CostBudgetCode = document.all("txtCostBudgetCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;
	window.location.href = "CostBudgetModify.aspx?CostBudgetCode=" + CostBudgetCode + "&CostBudgetSetCode=" + CostBudgetSetCode;
//	OpenFullWindow("CostTargetModify.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value, "预算表动态预算修改");
}
	
//调整
function ModifyEx()
{
	window.location.href = "CostBudgetModify.aspx?OldCostBudgetCode=" + document.all.txtCostBudgetCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value;
}
	
//审核
function DoCheck()
{
	if (!confirm("确实审核通过吗？")) return false;
	
	document.all.divHintSave.style.display = '';
	return true;
	
//	OpenCustomWindow("CostTargetCheck.aspx?PaymentCode=' + paymentCode,"预算表动态预算审核", 600, 400);
}

//存档
function Backup()
{
	OpenCustomWindow("CostBudgetBackup.aspx?ProjectCode=" + document.all.txtProjectCode.value, "项目费用备份", 420, 250);
}

//查看存档
function LoadBackup()
{
	OpenCustomWindow("CostBudgetBackupList.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&ProjectCode=" + document.all.txtProjectCode.value, "项目费用备份列表", 780, 560);
}

//查看存档(预算类别)
function LoadBackupGroup()
{
	OpenCustomWindow("CostBudgetBackupList.aspx?GroupCode=" + document.all.txtGroupCode.value + "&ProjectCode=" + document.all.txtProjectCode.value, "项目费用备份列表", 780, 560);
}

//切换到存档
function sltBackupChange(sender)
{
	var CostBudgetBackupCode = sender.value;
	
	if (CostBudgetBackupCode == "more") //弹出存档列表
	{
		OpenCustomWindow("CostBudgetBackupList.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&ProjectCode=" + document.all.txtProjectCode.value, "项目费用备份列表", 780, 560);
	}
	else //查看存档或当前
	{
		window.location.href = "CostBudgetInfo.aspx?CostBudgetBackupCode=" + CostBudgetBackupCode + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value;
	}
}

//切换到存档(预算类别)
function sltBackupGroupChange(sender)
{
	var CostBudgetBackupCode = sender.value;
	
	if (CostBudgetBackupCode == "more") //弹出存档列表
	{
		OpenCustomWindow("CostBudgetBackupList.aspx?GroupCode=" + document.all.txtGroupCode.value + "&ProjectCode=" + document.all.txtProjectCode.value, "项目费用备份列表", 780, 560);
	}
	else //查看存档或当前
	{
		window.location.href = "CostBudgetGroupInfo.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetBackupCode=" + CostBudgetBackupCode + "&GroupCode=" + document.all.txtGroupCode.value;
	}
}

//导出到Excel
function Excel()
{
	OpenCustomWindow("CostBudgetInfo.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostBudgetBackupCode=" + document.all.txtCostBudgetBackupCode.value +"&act=Excel", "CostBudgetInfoExcel", 400, 250);
}

//查看历史
function ViewHistory()
{
	OpenCustomWindow("CostBudgetHistoryList.aspx?TargetFlag=0&CostBudgetCode=" + document.all.txtCostBudgetCode.value, "动态预算历史", 780, 560);
}

//预算表设置
function ModifySet()
{
	OpenCustomWindow("CostBudgetSetModify.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value, "预算表设置", 500, 350);
}

//费用项信息
function ViewCostCode(code)
{
	OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=' + document.all.txtProjectCode.value + '&CostCode=' + code ,'动态费用项信息');
}

//显示历史目标费用
function ShowTargetMoneyHis(VerID)
{
	document.all.txtShowTargetMoneyHisVerID.value = VerID;
	document.all.btnShowTargetMoneyHis.click();
}

//隐藏历史目标费用
function HideTargetMoneyHis()
{
	document.all.btnHideTargetMoneyHis.click();
}

//显示历史预算金额
function ShowBudgetMoneyHis(VerID)
{
	document.all.btnShowBudgetMoneyHis.click();
}

//隐藏历史预算金额
function HideBudgetMoneyHis()
{
	document.all.btnHideBudgetMoneyHis.click();
}

//合同信息	
function ViewContractInfo(ContractCode)
{
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_") //招投标
	{
		var BiddingCode = PickupBiddingCode(ContractCode);
		OpenFullWindow("../BiddingManage/BiddingModify.aspx?state=edit&ProjectCode=" + document.all.txtProjectCode.value + "&ApplicationCode=" + BiddingCode + "&FunctionName=RefreshPurchase", "合同计划");
	}
	else if (ContractCode.substr(0, "Payment_".length) == "Payment_")
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/PaymentInfo.aspx?PaymentCode=" + PaymentCode, "请款单信息");
	}
	else
	{
		OpenFullWindow("../Contract/ContractInfo.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&ContractCode=" + ContractCode,'合同信息');
	}
}

//厂商信息
function ViewSupplierInfo(code)
{
	OpenFullWindow("../Supplier/SupplierInfo.aspx?SupplierCode=" + code, "厂商信息");
}

//查看已批明细
function ViewContractPay(CostCode, ContractCode, IsContract)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //非合同请款
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract, 'ShowPaymentItemList');
	}
	else
	{
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract, 'ShowPaymentItemList');
	}
}

//查看已批明细
function ViewContractPBSPay(CostCode, ContractCode, IsContract, PBSType, PBSCode)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //非合同请款
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPaymentItemList');
	}
	else
	{
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPaymentItemList');
	}
}

//查看已批未付明细
function ViewContractPayRealBalance(CostCode, ContractCode, IsContract)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //非合同请款
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?IsPayout=0,1&ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract, 'ShowPaymentItemList');
	}
	else
	{
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?IsPayout=0,1&ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract, 'ShowPaymentItemList');
	}
}

//查看已批未付明细
function ViewContractPBSPayRealBalance(CostCode, ContractCode, IsContract, PBSType, PBSCode)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //非合同请款
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?IsPayout=0,1&ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPaymentItemList');
	}
	else
	{
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?IsPayout=0,1&ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPaymentItemList');
	}
}

//查看已付明细
function ViewContractPayReal(CostCode, ContractCode, IsContract, PayoutDateBegin, PayoutDateEnd)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //非合同请款
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract + "&PayoutDateBegin=" + PayoutDateBegin + "&PayoutDateEnd=" + PayoutDateEnd, 'ShowPayoutItemList');
	}
	else  
	{
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract + "&PayoutDateBegin=" + PayoutDateBegin + "&PayoutDateEnd=" + PayoutDateEnd, 'ShowPayoutItemList');
	}
}

//查看已付明细
function ViewContractPBSPayReal(CostCode, ContractCode, IsContract, PayoutDateBegin, PayoutDateEnd, PBSType, PBSCode)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //非合同请款
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract + "&PayoutDateBegin=" + PayoutDateBegin + "&PayoutDateEnd=" + PayoutDateEnd + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPayoutItemList');
	}
	else  
	{
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract + "&PayoutDateBegin=" + PayoutDateBegin + "&PayoutDateEnd=" + PayoutDateEnd + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPayoutItemList');
	}
}

//查看楼栋
function ViewBuilding(BuildingCode)
{
	OpenCustomWindow("../PBS/PBSBuildInfo.aspx?BuildingCode=" + BuildingCode + "&OpenModal=open&action=view", "BuildingInfo", 700, 540);
}

//打印
function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=tdMaster$tdList1$tbl-container&css=" + escape("../CostBudget/CostBudget.css"), "打印");
}

//修改单项目标费用
function ModifyTargetMoney(CostCode)
{
	var ProjectCode = document.all("txtProjectCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;
	OpenCustomWindow("CostTargetModifyItem.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode, "修改单项目标费用", 400, 200);
}

//修改动态费用的预留金额
function ModifyBalance(CostCode, ContractMoney)
{
	var ProjectCode = document.all("txtProjectCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;
	OpenCustomWindow("DynamicBalanceModify.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode + "&ContractMoney=" + ContractMoney, "修改动态费用的预留金额", 400, 200);
}

//新增合同计划
function AddPurchase(CostCode)
{ 
	var ProjectCode = document.all("txtProjectCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;
	var PBSType = document.all("txtPBSType").value;
	var PBSCode = document.all("txtPBSCode").value;
//	alert("正在开发");
//	return;

	OpenFullWindow("../BiddingManage/BiddingModify.aspx?state=edit&ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode + "&CostCode=" + CostCode + "&FunctionName=RefreshPurchase", "合同计划");
	
//	OpenCustomWindow("CostBudgetPurchaseModify.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode, "新增合同计划", 760, 580);
}

//修改合同、招标计划预算
function ModifyContractBudget(CostCode, ContractCode)
{
	var ProjectCode = document.all("txtProjectCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;

	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_") //招标计划
	{
		var BiddingCode = PickupBiddingCode(ContractCode);
		OpenCustomWindow("CostBudgetContractModify.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode + "&RelationType=Bidding&ContractCode=" + BiddingCode, "修改合同预算", 400, 200);
	}
	else //合同
	{
		OpenCustomWindow("CostBudgetContractModify.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode + "&RelationType=Contract&ContractCode=" + ContractCode, "修改合同预算", 400, 200);
	}
}

function RowMouseClick(sender)
{
	if (event.button==2)
	{
		ShowEditMenu(sender);
		CBTree_SetRowSelected(sender);
	}
}

//弹出菜单
function ShowEditMenu(sender)
{
	var cssFile="../Images/ContentMenu.css";

	var rowCBS, key;
	
	//查看备份时无弹出菜单
	if ((document.all("txtCostBudgetBackupCode")) && (document.all("txtCostBudgetBackupCode").value != ""))
	{
		alert("只有在即时状态下才能操作项目费用，请先切换到即时状态");
		return;
	}
	
	if ((sender.id.indexOf("TreeNode_C_") >= 0) || (sender.id.indexOf("TreeNode_B_") >= 0)) //合同明细行或预留金额行
	{
		rowCBS = document.all("TreeNode_" + GetCostCodeById(sender.id));
		key = CBTree_GetTreeNodeKey(rowCBS);
	}
	else //费用项行
	{
		rowCBS = sender;
		key = CBTree_GetTreeNodeKey(rowCBS);
		
		//合计行
		if (key.substr(0, 2) == "R_") return;
	}

	var Items = new Array();

	var i = -1;
	
	//子节点数
	var ChildCount = 0;
	if (rowCBS.ChildCount)
	{
		ChildCount = parseInt(rowCBS.ChildCount);
	}
	
	i++;
	Items[i] = new Array(3);
	Items[i][0] = "修改目标费用";
	Items[i][1] = "";
	Items[i][2] = "ModifyTargetMoney('" + key + "');";

	if (ChildCount == 0) //叶节点的费用项
	{
	    var BalanceContractMoney = 0;
	    if (rowCBS.BalanceContractMoney) BalanceContractMoney = rowCBS.BalanceContractMoney;
	    
		i++;
		Items[i] = new Array(3);
		Items[i][0] = "修改预留金额";
		Items[i][1] = "";
		Items[i][2] = "ModifyBalance('" + key + "', " + BalanceContractMoney + ");";

		i++;
		Items[i] = new Array(3);
		Items[i][0] = "新增招标计划";
		Items[i][1] = "";
		Items[i][2] = "AddPurchase('" + key + "');";
	}

	if (sender.id.indexOf("TreeNode_C_") >= 0) //合同、招标计划
	{
//		var pos = sender.id.indexOf("TreeNode_C_") + "TreeNode_C_".length;
		var pos = sender.id.indexOf(":") + ":".length;
		var ContractCode = sender.id.substr(pos, sender.id.length - pos);
		
		i++;
		Items[i] = new Array(3);
		Items[i][0] = "修改合同预算";
		Items[i][1] = "";
		Items[i][2] = "ModifyContractBudget('" + key + "','" + ContractCode + "');";
	}
	
	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

/*
//设置目标费用标题是否有弹出菜单
function InitTargetHeadMenuLink()
{
	var objHref = document.all("hrefTargetVerID");
	var objSpan = document.all("spanTargetVerID");
	if ((objHref) && (objSpan))
	{
		if (document.all.txtHasTargetHis.value == "1")  //有历史目标费用
		{
			objHref.innerText = objSpan.innerText;
			objSpan.innerText = "";
		}
	}
}
*/

//目标费用标题弹出菜单
function ShowTargetHeadMenu(sender)
{
	var cssFile="../Images/ContentMenu.css";

	var Items = new Array();

	var i = -1;
	
	if (document.all.txtShowTargetHis.value == "1")
	{
		i++;
		Items[i] = new Array(3);
		Items[i][0] = "隐藏历史预算";
		Items[i][1] = "";
		Items[i][2] = "HideTargetMoneyHis();";
	}
	else
	{
		if (document.all.txtHasTargetHis.value == "1")
		{
			i++;
			Items[i] = new Array(3);
			Items[i][0] = "显示所有历史预算";
			Items[i][1] = "";
			Items[i][2] = "ShowTargetMoneyHis('');";
		}
	}

	if (i >= 0)
	{
		var offsetTop = 0;
		var offsetLeft = 0;
		
//		if (document.all(sender.id)[0])
//		{
			offsetTop = document.all("tbl-container").offsetTop + 2;
			offsetLeft = document.all("tbl-container").offsetLeft + 2 + document.all("tdTargetHead").offsetLeft;
//		}
		CreateContentMenu(Items,cssFile, event.x + offsetLeft, event.y + offsetTop);
	}
}

//列表键盘事件
function ListKeyDown(sender)
{
//alert(event.keyCode);

	if (event.keyCode == "38") //上箭头
	{
		var ActiveRow = GetActiveRow();
		var NextRow = ActiveRow.previousSibling;
		while ((NextRow) && (NextRow.style.display == "none"))
		{
			NextRow = NextRow.previousSibling;
		}

		if (NextRow)
		{			
			NextRow.click();
		}

		return false;
	}
	else if (event.keyCode == "40") //下箭头
	{
		var ActiveRow = GetActiveRow();
		var NextRow = ActiveRow.nextSibling;
		while ((NextRow) && (NextRow.style.display == "none"))
		{
			NextRow = NextRow.nextSibling;
		}

		if (NextRow)
		{			
			NextRow.click();
		}

		return false;
	}
	else if ((event.keyCode == "187") //+
		|| (event.keyCode == "13")) //Enter
	{
		//展开结点
		var ActiveRow = GetActiveRow();
		
		if ((ActiveRow) && (ActiveRow.expand == "0"))
		{
			CBTree_ImgExpandClick(ActiveRow);
		}

		return false;
	}
	else if ((event.keyCode == "189") //-
		|| (event.keyCode == "8")) //退格
	{
		//折叠结点
		var ActiveRow = GetActiveRow();
		
		if ((ActiveRow) && (ActiveRow.expand == "1"))
		{
			CBTree_ImgExpandClick(ActiveRow);
		}

		return false;
	}

	return true;
}

/*
//行键盘事件
function RowKeyDown(sender)
{
	var ActiveRow = GetActiveRow();
	var NextRow;
	
	if (event.keyCode == "40") //下箭头
	{
		NextRow = ActiveRow.nextSibling;
		if (NextRow)
		{
			NextRow.click();
		}
	}
	
//alert(event.keyCode);
}
*/

//行单击
function RowClick(sender)
{
	SetRowSelected(sender);
}

//XmlTree的行单击
function XmlTreeRowClick()
{
	if (LastSelectedRow)
	{
        for (var i=0;i<LastSelectedRow.childNodes.length;i++)
        {
        	LastSelectedRow.childNodes[i].className = LastSelectedRowClass;
        }
		
		LastSelectedRowClass = "";
		LastSelectedRow = "";
	}

	LastSelectedRow = this;
	LastSelectedRowClass = LastSelectedRow.childNodes[0].className;

    for (var i=0;i<this.childNodes.length;i++)
    {
    	this.childNodes[i].className = "tree-tr-highlight";
    }
}

//设置行选中
function SetRowSelected(sender)
{
	ClearRowSelected();
	
	LastSelectedRow = sender;
	LastSelectedRowClass = LastSelectedRow.className;
	LastSelectedRow.className = "list-highlight";
}

//取当前选中的一行
function GetActiveRow()
{
	return LastSelectedRow;
}

//清除行选中
function ClearRowSelected()
{
	if (LastSelectedRow)
	{
		LastSelectedRow.className = LastSelectedRowClass;
		
		LastSelectedRowClass = "";
		LastSelectedRow = "";
	}
}

//初始化行选中
function InitRowSelected()
{
	var LastSelectedRowID = document.all("txtLastSelectedRowID").value;
	if (LastSelectedRowID != "")
	{
		var obj = document.all(LastSelectedRowID);
		SetRowSelected(obj);
	}
}

//初始化列表状态
function InitListStatus()
{
	//列表滚动位置
	if (document.all.txtListScrollTop.value != "")
	{
		var scrollTop = parseInt(document.all.txtListScrollTop.value);
		var scrollLeft = parseInt(document.all.txtListScrollLeft.value);
		
		if (!isNaN(scrollTop)) document.all("tbl-container").scrollTop = scrollTop;
		if (!isNaN(scrollLeft)) document.all("tbl-container").scrollLeft = scrollLeft;
	}
	
	//行选中
	InitRowSelected();
}

//页面提交前
function BeforePost()
{
	document.all.txtListScrollTop.value = document.all("tbl-container").scrollTop;
	document.all.txtListScrollLeft.value = document.all("tbl-container").scrollLeft;
	
	document.all.txtLastSelectedRowID.value = "";
	document.all.txtLastSelectedRowClass.value = "";
	if (LastSelectedRow)
	{
		document.all.txtLastSelectedRowID.value = LastSelectedRow.id;
		document.all.txtLastSelectedRowClass.value = LastSelectedRowClass;
	}

	//记录树展开状态
	if (document.all.txtExpandNode) document.all.txtExpandNode.value = m_arrExpandNode.join(",");
		
	return true;

//alert(document.all("tbl-container").scrollTop);
//return;
}

function winload()
{
	if (document.all("tbl-container"))	document.all("tbl-container").oncontextmenu=Function("return false;");

//var d1 = new Date();

	CBTree_InitTree("Tree", "../images/plus.gif", "../images/minus.gif", headCount);

	CBTree_ExpandTreeByNodeDefaultExpand();
	ClearRowSelected();

	InitListStatus();
	
//	LockColumn("Tree", 1);
	
//	InitTargetHeadMenuLink();
	
//	MyLockTable();
	
//var d2 = new Date();
//alert(d1.getMinutes() + ":" + d1.getSeconds() + "." + d1.getMilliseconds() + " - " + d2.getMinutes() + ":" + d2.getSeconds() + "." + d2.getMilliseconds());	
}

/*
function MyLockTable()
{
	LockTable(Tree,0,2,0);
	if (document.all("DivHeadTar"))	document.all("DivHeadTar").oncontextmenu=Function("return false;");
}
*/

