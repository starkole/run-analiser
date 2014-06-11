using System;
using System.Collections.Generic;
using System.Linq;

namespace Et075.Model
{
    public class StatsList: List<OutStats>
    {
        /// <summary>
        /// The sum of corrected overprints sums of all OutStats-members.
        /// </summary>
        public int OverprintsSum 
        {
            get { return (from z in this select z.CorrectedOverprintsSum).Sum(); } 
        }

        /// <summary>
        /// Determines the weight coefficient for comparing two StasLists.
        /// The less weight is better, because it means the less costs.
        /// </summary>
        public int Weight 
        {
            //Return overprints sum + cost of every added layout
            get { return this.OverprintsSum + (this.Count - 1) * Constants.LAYOUT_COSTS; } 
        }

        public StatsList()
            : base() { }

        public override string ToString()
        {
            return string.Format("Spuskiv {0}, overprints sum {1}, weight {2}",
                this.Count, this.OverprintsSum, this.Weight);
        }

        public static bool operator <(StatsList a, StatsList b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            return a.Weight < b.Weight;
        }

        public static bool operator >(StatsList a, StatsList b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            return a.Weight > b.Weight;
        }

        public static bool operator <=(StatsList a, StatsList b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            return a.Weight <= b.Weight;
        }

        public static bool operator >=(StatsList a, StatsList b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            return a.Weight >= b.Weight;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(StatsList))
                return false;
            return this.Weight == ((StatsList)obj).Weight;
        }

        public override int GetHashCode()
        {
            return this.Weight.GetHashCode();
        }
        
    }
}
