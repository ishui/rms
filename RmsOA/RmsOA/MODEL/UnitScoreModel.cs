namespace RmsOA.MODEL
{
    using System;

    public class UnitScoreModel
    {
        private string deptCode;
        private string deptName;
        private string marker;
        private int score;

        public string DeptCode
        {
            get
            {
                return this.deptCode;
            }
            set
            {
                this.deptCode = value;
            }
        }

        public string DeptName
        {
            get
            {
                return this.deptName;
            }
            set
            {
                this.deptName = value;
            }
        }

        public string Marker
        {
            get
            {
                return this.marker;
            }
            set
            {
                this.marker = value;
            }
        }

        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
            }
        }
    }
}

