using System;


namespace Et075.Model
{
    public class OutStats
    {
        #region Properties
        public int Run { get; set; }

        private int _overprintsSum;
        /// <summary>
        /// Overprint sum without blank positions on incomplete filled sheet
        /// </summary>
        public int OverprintsSum
        {
            get
            { return this._overprintsSum; }
            set { this._overprintsSum = value; }
        }

        /// <summary>
        /// Take into account if the sheet was filled incompletely,
        /// so we add the blank positions to the overprint sum
        /// </summary>
        public int CorrectedOverprintsSum
        {
            get
            {
                if (!IsValid)
                    throw new ArithmeticException("Invalid OutStats object detected.");

                return this._overprintsSum + (Constants.ETS_ON_SHEET - this.EtsOnSheetCount) * this.Run;
            }
        }

        public int EtsOnSheetCount { get; set; }

        public Zakaz Ets { get; set; }

        public bool IsValid
        {
            get
            {
                return EtsOnSheetCount <= Constants.ETS_ON_SHEET
                    && EtsOnSheetCount >= 0;
            }
        }

        #endregion //Properties

        #region Constructors

        public OutStats()
        {
            Ets = new Zakaz();
        }

        public OutStats(Zakaz ets, int run, int overprintsSum, int etsOnSheetCount)
        {
            Ets = ets;
            Run = run;
            OverprintsSum = overprintsSum;
            EtsOnSheetCount = etsOnSheetCount;
        }

        #endregion
        

        #region Methods
        
        public override string ToString()
        {
            return string.Format("Run {0}, EtsOnSheetCount {1}, CorOverprintsSum {2}, EtsNumber {3}",
                Run, EtsOnSheetCount, CorrectedOverprintsSum, Ets.Count);
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(OutStats))
                return false;
            return ((OutStats)obj).CorrectedOverprintsSum == this.CorrectedOverprintsSum;
        }

        public override int GetHashCode()
        {
            return this.CorrectedOverprintsSum.GetHashCode();
        }
        
        public static bool operator <(OutStats a, OutStats b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            return
                a.CorrectedOverprintsSum < b.CorrectedOverprintsSum;
        }

        public static bool operator >(OutStats a, OutStats b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            return a.CorrectedOverprintsSum > b.CorrectedOverprintsSum;
        }

        public static bool operator <=(OutStats a, OutStats b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            return
                a.CorrectedOverprintsSum <= b.CorrectedOverprintsSum;
        }

        public static bool operator >=(OutStats a, OutStats b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            return a.CorrectedOverprintsSum >= b.CorrectedOverprintsSum;
        }

        #endregion //Methods
    }//class OutStats
}
