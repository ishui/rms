namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using RmsOA.MODEL;

    public class YF_AssetFacade
    {
        public AssetModel GetAssetName(string inCode)
        {
            AssetModel model = null;
            int result;
            if (int.TryParse(inCode, out result))
            {
                model = new AssetModel();
                YF_AssetManageBFL ebfl = new YF_AssetManageBFL();
                YF_AssetManageModel model2 = new YF_AssetManageModel();
                model2 = ebfl.GetYF_AssetManage(result);
                model.Code = result;
                model.EquiName = model2.FacilityName;
                model.SortCode = model2.CodeNO;
            }
            return model;
        }

        public string GetMainRecordCodeByMainCode(string incode)
        {
            string text = null;
            int result;
            if (int.TryParse(incode, out result))
            {
                YF_AssetMainRecordBFL dbfl = new YF_AssetMainRecordBFL();
                YF_AssetMainRecordQueryModel queryModel = new YF_AssetMainRecordQueryModel();
                List<YF_AssetMainRecordModel> list = new List<YF_AssetMainRecordModel>();
                queryModel.FKCodeEqual = result;
                list = dbfl.GetYF_AssetMainRecordList(queryModel);
                if (list.Count > 0)
                {
                    text = list[0].Code.ToString();
                }
            }
            return text;
        }
    }
}

