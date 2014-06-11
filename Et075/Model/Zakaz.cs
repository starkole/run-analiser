using System;
using System.Collections.Generic;
using System.Linq;

namespace Et075.Model
{
    /// <summary>
    /// Represents a single order. This is the list of Etyketkas with unique Ids.
    /// </summary>
    public class Zakaz : List<Etyketka>
    {
        #region Properties
               
        /// <summary>
        /// Returns list of indexes for optimal splitting this Zakaz into parts
        /// Each index will be a zero-based start for the new Zakaz
        /// </summary>
        public List<int> SplitMarkers
        { get { return FindSplitMarkers(); } }

        public int MinRun
        { get { return (from e in this select e.Run).Min(); } }

        public int MaxRun
        { get { return (from e in this select e.Run).Max(); } }

        public int CountOnSheetSum
        { get { return (from e in this select e.CountOnSheet).Sum(); } }

        #endregion//Properties

        #region Constructors

        public Zakaz()
            : base() { }

        public Zakaz(IEnumerable<Etyketka> collection)
            : base(collection) { }

        public Zakaz(params int[] runs)
            : base()
        {
            for (int i = 0; i < runs.Length; i++)
            {
                base.Add(new Etyketka(i + 1, "Et" + (i + 1).ToString(), runs[i]));
            }
        }

        #endregion//Constructors

        #region Methods
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Zakaz))
                return false;
            if (((Zakaz)obj).Count != this.Count)
                return false;
            foreach (Etyketka et in this)
            {
                if (!((Zakaz)obj).Contains(et))
                    return false;
            }
            return true;
        }//end:Equals

        public override int GetHashCode()
        {
            return (from e in this orderby e.Id select e.Id).ToList().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Count {0}; MaxRun {1}; MinRun {2}", this.Count, this.MaxRun, this.MinRun);
        }

        /// <summary>
        /// Sorts current Zakaz by run from max to min
        /// and makes all runs multiple of RUN_DELTA
        /// </summary>
        /// <returns>New sorted and normalized Zakaz object</returns>
        public Zakaz NormalizeAndSort()
        {
            if (this.Count == 0)
                return new Zakaz();

            Zakaz result = new Zakaz(from e in this orderby e.Run descending select e);
            if (result.Count < 1)
                return result;
            int nextRun = 0;
            int currentRun = result[0].Run;
            for (int i = 0; i < result.Count - 1; i++)
            {
                nextRun = result[i + 1].Run;
                if (currentRun != nextRun && currentRun - nextRun <= Constants.RUN_DELTA)
                    this[i + 1].Run = currentRun;
                currentRun = nextRun;
            }
            return result;
        }//end:NormalizeAndSort

        /// <summary>
        /// Recursively builds the list of the split markers for current Zakaz
        /// </summary>
        /// <param name="startIndex">Zero-based index to start the analisys from</param>
        /// <returns></returns>
        private List<int> FindSplitMarkers(int startIndex = 0)
        {
            //There is no need to split small Zakaz
            if (this.Count - startIndex <= Constants.ETS_ON_SHEET)
                return new List<int>();

            //Determine if current zakaz is sorted from min to max
            bool sortedAsc = true;
            for (int i = startIndex; i < this.Count - 1 - startIndex; i++)
            {
                if (this[i].Run > this[i + 1].Run)
                {
                    sortedAsc = false;
                    break;
                }
            }
            //Determine if current zakaz is sorted from max to min
            bool sortedDesc = true;
            for (int i = startIndex; i < this.Count - 1 - startIndex; i++)
            {
                if (this[i].Run < this[i + 1].Run)
                {
                    sortedDesc = false;
                    break;
                }
            }
            //Cannot mark for splitting unsorted list
            if (!(sortedAsc || sortedDesc))
                return new List<int>();

            //Differnce between two adjasent runs
            int delta = 0;
            //Default index for the case when the first ETS_ON_SHEET runs are the same
            int splitIndex = Constants.ETS_ON_SHEET;
            //Find the largest diff for the first first ETS_ON_SHEET runs
            for (int i = startIndex;
                i < this.Count() - 1 - startIndex
                    && i < startIndex + Constants.ETS_ON_SHEET;
                i++)
            {
                int tmp_delta = Math.Abs(this[i].Run - this[i + 1].Run);
                if (tmp_delta >= delta) // >= because we want to maximise the part being splitted
                {
                    delta = tmp_delta;
                    splitIndex = i + 1; // Advancing index by 1 to pass it recursively as new splitIndex
                }
            }//end:for

            //Add index to result list
            List<int> result = new List<int>();
            result.Add(splitIndex);
            //Analize the rest of Zakaz
            result.AddRange(FindSplitMarkers(splitIndex));
            return result;
        }//end:FindMagicNumbers

        #endregion//Methods

    }//end:class Zakaz
}
