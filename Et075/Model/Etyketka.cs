using System.Collections.Generic;

namespace Et075.Model
{
    public class Etyketka
    {
        #region Properties
        
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Run { get; set; }
        public int CountOnSheet { get; set; }
        public int Overprint { get; set; }
        public int RunNormalization { get; set; }

        #endregion//Properties

        #region Constructors

        public Etyketka(int run)
        {
            Run = run;
            Overprint = 0;
        }

        public Etyketka(int id, int run)
        {
            Id = id;
            Run = run;
            Overprint = 0;
        }

        public Etyketka(int id, string name, int run)
        {
            Id = id;
            Name = name;
            Run = run;
            Overprint = 0;
        }

        public Etyketka(Etyketka et)
        {
            Id = et.Id;
            Name = et.Name;
            Run = et.Run;
            Overprint = et.Overprint;
        }

        #endregion//Constructors

        #region Methods
                
        public override string ToString()
        {
            return string.Format("Id {0}, Run {1}, OnSheet {2}, Ovprnt {3}, Name {4}", Id, Run, CountOnSheet, Overprint, Name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Etyketka))
                return false;
            return ((Etyketka)obj).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion//Methods

    }//class Etyketka

    public class CompareEtsByRunMinToMax : Comparer<Etyketka>
    {
        public override int Compare(Etyketka x, Etyketka y)
        {
            if (x.Run == y.Run)
                return 0;
            if (x.Run < y.Run)
                return -1;
            return 1;
        }
    }

    public class CompareEtsByRunMaxToMin : Comparer<Etyketka>
    {
        public override int Compare(Etyketka x, Etyketka y)
        {
            if (x.Run == y.Run)
                return 0;
            if (x.Run < y.Run)
                return 1;
            return -1;
        }
    }
}
