namespace RmsOA.MODEL
{
    using System;

    public class EmployScoreMode
    {
        private string deptCode;
        private string deptName;
        private int manageCode;
        private string marker;
        private string markerCode;
        private string markTime;
        private string score;
        private int scoreCode;
        private string status;
        private string userCode;
        private string userName;

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

        public int ManageCode
        {
            get
            {
                return this.manageCode;
            }
            set
            {
                this.manageCode = value;
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

        public string MarkerCode
        {
            get
            {
                return this.markerCode;
            }
            set
            {
                this.markerCode = value;
            }
        }

        public string MarkTime
        {
            get
            {
                return this.markTime;
            }
            set
            {
                this.markTime = value;
            }
        }

        public string Score
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

        public int ScoreCode
        {
            get
            {
                return this.scoreCode;
            }
            set
            {
                this.scoreCode = value;
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this.userCode;
            }
            set
            {
                this.userCode = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }
    }
}

