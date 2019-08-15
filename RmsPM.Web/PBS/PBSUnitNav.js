function GotoPBSUnitInfo(PBSUnitCode, FromUrl)
{
	window.location.href = "../PBS/PBSUnitInfo.aspx?FromUrl=" + escape(FromUrl) + "&PBSUnitCode=" + PBSUnitCode;
}

function GotoConstructPlanInfo(PBSUnitCode, FromUrl)
{
	window.location.href = "../Construct/ConstructPlanInfo.aspx?FromUrl=" + escape(FromUrl) + "&PBSUnitCode=" + PBSUnitCode;
}

