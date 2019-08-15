namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class ZC_AssertExtend
    {
        private static readonly int deptIndex = 7;
        private const int expectCount = 12;
        private List<AssetViewModel> listRight;
        private List<WrongModel> listWrong;
        private int rightCount = 0;
        private const char splitChar = ',';
        private const string splitDept = "->";
        private int totalCount = 0;
        private int wrongCount = 0;

        public List<AssetViewModel> GetRight()
        {
            return null;
        }

        public string GetUnitCode(string name)
        {
            UnitHelp help = new UnitHelp();
            if (!name.Contains("->"))
            {
                return help.GetUnitCode(name, name);
            }
            int num = name.LastIndexOf("->");
            string text2 = name.Substring(num + "->".Length);
            return help.GetUnitCode(name, text2);
        }

        public List<WrongModel> GetWrong()
        {
            return null;
        }

        public AssetViewModel InitAssetViewModel(string[] array)
        {
            int result;
            decimal num2;
            AssetViewModel model = new AssetViewModel();
            model.Name = array[0];
            model.Type = array[1];
            model.Number = array[2];
            model.Buydate = DateTime.Parse(array[3]);
            if (int.TryParse(array[4], out result))
            {
                model.BuyCount = result;
            }
            model.Unit = array[5];
            if (decimal.TryParse(array[6], out num2))
            {
                model.Price = num2;
            }
            model.Dept = array[7];
            model.UserName = array[8];
            model.Place = array[9];
            model.TransferRecord = array[10];
            model.Remark = array[11];
            model.Status = WorkFlowStatus.Apply.ToString("d");
            return model;
        }

        public WrongModel InitWrongModel()
        {
            WrongModel model = new WrongModel();
            model.Message = "数据列的格式有误!";
            return model;
        }

        public WrongModel InitWrongModel(string message)
        {
            WrongModel model = new WrongModel();
            model.Message = message;
            return model;
        }

        public void SetObjectProperty(List<string> listValue)
        {
            List<WrongModel> list = new List<WrongModel>();
            List<AssetViewModel> list2 = new List<AssetViewModel>();
            WrongModel item = new WrongModel();
            AssetViewModel model2 = new AssetViewModel();
            this.totalCount = listValue.Count - 1;
            for (int i = 1; i < listValue.Count; i++)
            {
                string[] array = listValue[i].Split(new char[] { ',' });
                int length = array.Length;
                if (length.Equals(12))
                {
                    if (string.IsNullOrEmpty(array[deptIndex]))
                    {
                        model2 = this.InitAssetViewModel(array);
                        model2.NoIndex = i;
                        model2.Index = ++this.rightCount;
                        list2.Add(model2);
                    }
                    else
                    {
                        string name = array[deptIndex];
                        string unitCode = this.GetUnitCode(name);
                        if (string.IsNullOrEmpty(unitCode))
                        {
                            item = this.InitWrongModel("“使用部门”的格式有错误，请注意使用->分割。");
                            item.NoIndex = i;
                            item.Index = ++this.wrongCount;
                            list.Add(item);
                        }
                        else
                        {
                            array[deptIndex] = unitCode;
                            model2 = this.InitAssetViewModel(array);
                            model2.DeptName = name;
                            model2.NoIndex = i;
                            model2.Index = ++this.rightCount;
                            list2.Add(model2);
                        }
                    }
                }
                else
                {
                    item = this.InitWrongModel();
                    item.Index = ++this.wrongCount;
                    item.NoIndex = i;
                    list.Add(item);
                }
            }
            this.ListUserfulModel = list2;
            this.ListWrongModel = list;
        }

        public void SortData(List<string> listValue)
        {
            if ((listValue == null) || (listValue.Count < 1))
            {
                throw new Exception("选择导入的文件没有数据");
            }
            if (listValue[0].Split(new char[] { ',' }).Length != 12)
            {
                throw new Exception("导入文件的内容与要求的格式不符合");
            }
            this.SetObjectProperty(listValue);
        }

        public List<AssetViewModel> ListUserfulModel
        {
            get
            {
                return this.listRight;
            }
            set
            {
                this.listRight = value;
            }
        }

        public List<WrongModel> ListWrongModel
        {
            get
            {
                return this.listWrong;
            }
            set
            {
                this.listWrong = value;
            }
        }

        public int TotalCount
        {
            get
            {
                return this.totalCount;
            }
            set
            {
                this.totalCount = value;
            }
        }

        public int UserfulCount
        {
            get
            {
                return this.rightCount;
            }
            set
            {
                this.rightCount = value;
            }
        }

        public int WrongCount
        {
            get
            {
                return this.wrongCount;
            }
            set
            {
                this.wrongCount = value;
            }
        }
    }
}

