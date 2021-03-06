﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Et075.Model
{
    public class Analizer
    {
        #region PublicMethods

        /// <summary>
        /// Tries to process provided list of ets simply aligning them on the single sheet.
        /// If it fails, Split would be called.
        /// </summary>
        public static StatsList FirstPass(Zakaz ets)
        {
            //Make given ets immutable
            Zakaz _ets = new Zakaz(ets);

            StatsList result;

            // If number of ets > Constants.ETS_ON_SHEET apply Split
            if (_ets.Count > Constants.ETS_ON_SHEET)
            {
                result = SplitFromMinToMax(_ets);

                if (_ets.Count <= Constants.ETS_ON_SHEET * 2)
                {
                    StatsList tmpResult = TwoPartsRebalancer(_ets);
                    if (tmpResult < result)
                        result = tmpResult;
                }

                return result;
            }

            result = new StatsList() { DecreaseRun(_ets, GetMinValidRun(_ets)) };

            return result;
        }//end:FirstPass

        /// <summary>
        /// Splits ets list based on 'MagicNumbers' and processes produced sublists
        /// </summary>
        public static StatsList FindBestRunWithShifting(Zakaz ets)
        {
            //Given ets become immutable, because NormalizeAndSort() returns new object,
            //and sorted from max to min by ryn
            Zakaz _ets = ets.NormalizeAndSort();

            //For small Zakaz just find the best run
            if (_ets.Count <= Constants.ETS_ON_SHEET)
            {
                return new StatsList() { FindBestRun(_ets) };
            }

            StatsList result = new StatsList();
            Zakaz tmpEts = new Zakaz();
            List<int> magicNumbers = _ets.SplitMarkers;
            for (int i = 0; i < _ets.Count; i++)
            {
                if (!magicNumbers.Contains(i))
                    tmpEts.Add(_ets[i]);
                else
                {
                    result.Add(FindBestRun(tmpEts));
                    tmpEts = new Zakaz() { _ets[i] };
                }
            }//end:for

            if (tmpEts.Count > 0 && tmpEts.Count <= Constants.ETS_ON_SHEET)
                result.Add(FindBestRun(tmpEts));
            else if (tmpEts.Count <= Constants.ETS_ON_SHEET * 2)
                result.AddRange(TwoPartsRebalancer(tmpEts));
            else if (tmpEts.Count > Constants.ETS_ON_SHEET * 2)
                result.AddRange(SplitFromMinToMax(tmpEts));

            return result;
        }//end:FindBestRunWithShifting

        ///<summary>
        ///Splits provided ets by GCD and applies FirstPass on each sublist.
        ///Collects OutStats from each FirstPass and returns them as single list.
        ///</summary>
        public static StatsList SplitByGcd(Zakaz ets)
        {
            //ets.NormalizeAndSort();
            Zakaz _ets = new Zakaz(ets);
            _ets.Sort(new CompareEtsByRunMinToMax());

            List<Zakaz> groups = new List<Zakaz>();
            Zakaz garbageGroup = new Zakaz();
            foreach (Zakaz group in SplitByGcdToGroups(_ets))
            {
                //Sort ets of current group from max to min run
                group.Sort(new CompareEtsByRunMaxToMin());

                //Split big groups to smaller ones
                while (group.Count >= Constants.ETS_ON_SHEET)
                {
                    groups.Add(new Zakaz(group.Take(Constants.ETS_ON_SHEET)));
                    group.RemoveRange(0, Constants.ETS_ON_SHEET);
                }

                //All groups of one et are merged into one 'garbage' group
                if (group.Count < 2)
                    garbageGroup.AddRange(group);
                else
                    groups.Add(group);
            }
            if (garbageGroup.Count > 0)
                groups.Add(garbageGroup);

            StatsList result = new StatsList();
            foreach (Zakaz z in groups)
                result.AddRange(FirstPass(z));
            return result;
        }//end:SplitByGcd

        #endregion//PublicMethods


        #region ServiceMethods

        /// <summary>
        /// Returns the greatest common divisor of a and b
        /// </summary>
        protected static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }//end:GCD

        /// <summary>
        /// Determines if the baseEt is a GCD for the comparedEt
        /// </summary>
        protected static bool IsGcd(Etyketka baseEt, Etyketka comparedEt)
        {
            return baseEt.Run <= comparedEt.Run
                && (comparedEt.Run / baseEt.Run) < Constants.ETS_ON_SHEET
                && GCD(baseEt.Run, comparedEt.Run) == baseEt.Run;
        }//end:IsGcd

        /// <summary>
        /// Calculates statistics for given set of ets,
        /// which would be printed with given run
        /// </summary>
        protected static OutStats GetStatsForRun(Zakaz ets, int run)
        {
            OutStats result = new OutStats();
            result.Run = run;
            foreach (Etyketka et in ets)
            {
                Etyketka tmpEt = new Etyketka(et);
                if (et.Run > run)
                {
                    tmpEt.CountOnSheet = et.Run / run;  //ex. 7/3=2
                    int remainder = et.Run % run; //Get the remainder
                    if (remainder > 0)  // "Round up" if there is some remainer
                    {
                        tmpEt.Overprint = run - remainder;
                        tmpEt.CountOnSheet++;
                    }
                }
                else
                {
                    tmpEt.CountOnSheet = 1;
                    tmpEt.Overprint = run - et.Run;
                }
                if (tmpEt.Overprint < 0)
                    throw new ArithmeticException("GetStatsForRun: something is wrong, negative Overprints detected!");
                result.Ets.Add(tmpEt);
                result.EtsOnSheetCount += tmpEt.CountOnSheet;
                result.OverprintsSum += tmpEt.Overprint;
            }//foreach
            return result;
        }//end:GetEtsOnSheetForRun

        /// <summary>
        /// Tries to decrease given run to maximize the fill of the sheet with given ets
        /// </summary>
        protected static OutStats DecreaseRun(Zakaz ets, int run)
        {
            //Get the etsOnSheetSum for the run
            OutStats result = GetStatsForRun(ets, run);
            if (!result.IsValid)
                throw new ArithmeticException("DecreaseRun: EtsOnSheetCount sum bigger than ETS_ON_SHEET. That is nonesense!");
            if (result.EtsOnSheetCount == Constants.ETS_ON_SHEET)
                return result;

            //Try to increase the run for every single et and choose the best result
            OutStats tmpStats = null;
            int etCount;
            foreach (Etyketka et in ets)
            {
                etCount = et.CountOnSheet;
                do
                {
                    etCount += 1;
                    //Fit the run so, that etsOnSheet count for the et increased by 1
                    //and get the stats for the newRun
                    tmpStats = GetStatsForRun(ets, et.Run / etCount);
                    //Choose the best result
                    if (tmpStats.IsValid && tmpStats < result)
                        result = tmpStats;
                } while (tmpStats.IsValid);
            }//foreach

            //If something went wrong and all ets have some greater-than-zero overprint, fix that
            int resultMinOverprint = (from e in result.Ets select e.Overprint).Min();
            if (resultMinOverprint > 0)
                return GetStatsForRun(result.Ets, result.Run - resultMinOverprint);
            else
                return result;
        }//end:DecreaseRun

        /// <summary>
        /// Finds the run with the least remains for given ets
        /// by trying every run from ets.MaxRun to 0.
        /// </summary>
        /// <param name="ets">Ets count cannot be > ETS_ON_SHEET</param>
        /// <returns></returns>
        protected static OutStats FindBestRun(Zakaz ets)
        {
            if (ets.Count > Constants.ETS_ON_SHEET)
                throw new ArgumentException("FindBestRun cannot operate with so many ets.");

            int currentRun = ets.MaxRun;
            OutStats bestResult = GetStatsForRun(ets, currentRun); //Initialize bestResult
            if (!bestResult.IsValid)
                throw new Exception("FindBestRun: Invalid stats obtained for MaxRun. Internal logic error.");
            const int STEP = 100;
            currentRun -= STEP;
            while (currentRun > 0)
            {
                OutStats result = GetStatsForRun(ets, currentRun);
                if (result.IsValid && result < bestResult)
                    bestResult = result;
                currentRun -= STEP;
            }
            return bestResult;
        }//end:FindBestRun

        ///<summary>
        ///Splits provided list of ets and applies FirstPass on each sublist.
        ///Collects OutStats from each FirstPass and returns them as single list.
        ///</summary>
        protected static StatsList SplitFromMinToMax(Zakaz ets)
        {
            //Make given ets immutable
            Zakaz _ets = new Zakaz(ets);
            //Order ets by runs
            _ets.Sort(new CompareEtsByRunMinToMax());

            int runIncrement = 0;
            StatsList TheBestResult = null;
            Zakaz zakazForMinRun = null;
            OutStats statsForMinRun = null;

            do
            {
                //Split ets to groups with the sum count on the sheet <= Constants.ETS_ON_SHEET
                //and build temporary stats list
                statsForMinRun = GetStatsForRun(_ets, _ets.MinRun + runIncrement);
                //For now statsForMinRun are not valid, but we don't bother, 
                //because we would split ets later
                zakazForMinRun = statsForMinRun.Ets;

                StatsList tmpResult = new StatsList();
                Zakaz part1;
                Zakaz part2;
                do
                {
                    part1 = new Zakaz();
                    part2 = new Zakaz();
                    int count = 0;
                    foreach (Etyketka et in zakazForMinRun)
                    {
                        if (count + et.CountOnSheet <= Constants.ETS_ON_SHEET)
                        {
                            //Select first ets group that fits at one sheet
                            part1.Add(et);
                        }
                        else
                        {
                            //Add all other ets to the second group
                            part2.Add(et);
                        }
                        count += et.CountOnSheet;
                    }//foreach
                    //Part1 is valid, as far as previous if-expression is true
                    OutStats os = GetStatsForRun(part1, part1.MinRun + runIncrement);
                    if (!os.IsValid)
                    {
                        if (TheBestResult != null)
                            return TheBestResult;
                        else
                            throw new Exception("Invalid Algorithm!");
                    }
                    tmpResult.Add(os);
                    //For second group recalculating stats with its MinRun
                    if (part2.Count > 0)
                    {
                        statsForMinRun = GetStatsForRun(part2, part2.MinRun + runIncrement);
                        //For now statsForMinRun are not valid, but we don't bother, 
                        //because we would split part2 in the next loop
                        zakazForMinRun = statsForMinRun.Ets;
                    }
                } while (part2.Count > 0);

                //Select the best result as the final result returned
                if (TheBestResult == null)
                    TheBestResult = tmpResult;
                if (TheBestResult > tmpResult)
                    TheBestResult = tmpResult;

                runIncrement += 100;
            } while (_ets.MinRun + runIncrement <= _ets.MaxRun);
            return TheBestResult;
        }//end:SplitFromMinToMax

        /// <summary>
        /// Determines minimum run for given Zakaz,
        /// which produces valid OutStats
        /// </summary>
        /// <param name="ets">Given zakaz remains immutable</param>
        /// <returns>Minimum valid run</returns>
        protected static int GetMinValidRun(Zakaz ets)
        {
            // Obtain stats for minimum run
            int run = ets.MinRun;
            OutStats tmpStats = GetStatsForRun(ets, run);
            while (!tmpStats.IsValid)
            {
                run += 100; //Increase the run and the EtsOnSheetCount being decreased
                tmpStats = GetStatsForRun(ets, run);
            }
            return run;
        }//end:GetMinValidRun

        /// <summary>
        /// Sorts given ets, splits them into two groups 
        /// and optimizes these groups via rebalancing them
        /// </summary>
        /// <param name="ets">Ets count has to be > 2 and < 2xETS_ON_SHEET</param>
        protected static StatsList TwoPartsRebalancer(Zakaz ets)
        {
            //Ets count has to be > 2 and < 2xETS_ON_SHEET
            if (ets.Count <= 2 || ets.Count >= Constants.ETS_ON_SHEET * 2)
            {
                throw new ArgumentOutOfRangeException();
            }

            //Make given ets immutable
            Zakaz _ets = new Zakaz(ets);
            StatsList result = new StatsList();

            _ets.Sort(new CompareEtsByRunMinToMax());

            //When we have _ets less than for two sheets,
            //split them into two parts
            //Take the first ETS_ON_SHEET ets to the first part
            Zakaz part1 = new Zakaz(_ets.GetRange(0, Constants.ETS_ON_SHEET));
            //Take all remained ets to the second part
            Zakaz part2 = new Zakaz(_ets.GetRange(Constants.ETS_ON_SHEET,
                                                  _ets.Count - 1 - Constants.ETS_ON_SHEET));
            //Calculate stats for both parts
            result.Add(GetStatsForRun(part1, GetMinValidRun(part1)));
            result.Add(GetStatsForRun(part2, GetMinValidRun(part2)));

            //Try to rebalance Part1 and Part2 to obtain the best stats
            StatsList tmpResult;
            while (part2.Count < Constants.ETS_ON_SHEET)
            {
                part2.Add(part1[part1.Count - 1]);
                part1.RemoveAt(part1.Count - 1);
                tmpResult = new StatsList();
                tmpResult.Add(GetStatsForRun(part1, GetMinValidRun(part1)));
                tmpResult.Add(GetStatsForRun(part2, GetMinValidRun(part2)));
                if (tmpResult < result)
                    result = tmpResult;
            }//while

            return result;
        }//end:PackIntoSheet

        /// <summary>
        /// Splits given ets to groups based on GCD.
        /// </summary>
        protected static List<Zakaz> SplitByGcdToGroups(Zakaz ets)
        {
            //Sort ets by run from max to min
            ets.Sort(new CompareEtsByRunMaxToMin());

            //Take every et and collect all other ets which runs are GCD of this et run
            List<Zakaz> gcdGroups = new List<Zakaz>();
            foreach (Etyketka et in ets)
            {
                Zakaz group = new Zakaz(from e in ets where IsGcd(et, e) select e);
                if (group.Count > 0 && !gcdGroups.Contains(group))
                    gcdGroups.Add(group);
            }

            //Remove dublicated ets, so that result returned 
            //would have only one instance of each et
            List<Zakaz> result = new List<Zakaz>();
            Zakaz garbage = new Zakaz();
            bool present; //Indicates if current et is already present in the result

            //Take every group from gcdGroups sorted by number of ets from max to min
            foreach (Zakaz group in (from z in gcdGroups orderby z.Count descending select z))
            {
                present = false;
                foreach (Etyketka et in group)
                    foreach (Zakaz resultGroup in result)
                        //If current et is already in the result, then drop this group
                        if (resultGroup.Contains(et))
                        {
                            present = true;
                            goto FIN; //Exit from inner and outer for-loops
                        }
            FIN:
                if (!present)
                {
                    //If all ets of current group are not present in the result,
                    //add this group to the result
                    result.Add(group);
                }
                else
                {
                    //Add current group to garbage
                    foreach (Etyketka et in group)
                        if (!garbage.Contains(et))
                            garbage.Add(et);
                }//else
            }//foreach
            if (garbage.Count > 0)
                result.Add(garbage);
            return result;
        }//end:SplitByGcdToGroups

        #endregion//ServiceMethods

    }//end:class Analizer
}
