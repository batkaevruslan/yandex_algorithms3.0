using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Final.C
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);
            StringBuilder sb = new();

            int n = int.Parse(sr.ReadLine());
            List<Job> jobs = new(n);
            for(int i = 1; i <= n; i++)
            {
                string v = sr.ReadLine();
                short[] jobData = v.Trim().Split(" ").Select(short.Parse).ToArray();
                jobs.Add(new Job(i, jobData[0], jobData[1]));
            }
            //var man1Jobs = jobs.OrderByDescending(j => j.Time1).ToList();
            //var man2Jobs = jobs.OrderByDescending(j => j.Time2).ToList();
            short totalTime1 = 0;
            short totalTime2 = 0;
            List<(int jobNumber, byte manNumber)> result = new();
            /*while(man1Jobs.Count > 0 && man2Jobs.Count > 0)
            {
                if(totalTime1 > totalTime2)
                {
                    Job longestJob = man2Jobs.First();
                    result.Add((longestJob.Number, 2));
                    man2Jobs.RemoveAt(0);
                    man1Jobs.RemoveAll(j => j.Number == longestJob.Number);
                    totalTime2 += longestJob.Time2;
                } else if(totalTime1 < totalTime2)
                {
                    Job longestJob = man1Jobs.First();
                    result.Add((longestJob.Number, 1));
                    man1Jobs.RemoveAt(0);
                    man2Jobs.RemoveAll(j => j.Number == longestJob.Number);
                    totalTime1 += longestJob.Time1;
                } else
                {
                    if(man1Jobs.First().Time1 > man2Jobs.First().Time2)
                    {
                        Job longestJob = man1Jobs.First();
                        result.Add((longestJob.Number, 1));
                        man1Jobs.RemoveAt(0);
                        man2Jobs.RemoveAll(j => j.Number == longestJob.Number);
                        totalTime1 += longestJob.Time1;
                    }
                    else {
                        Job longestJob = man2Jobs.First();
                        result.Add((longestJob.Number, 2));
                        man2Jobs.RemoveAt(0);
                        man1Jobs.RemoveAll(j => j.Number == longestJob.Number);
                        totalTime2 += longestJob.Time2;
                    }
                }
            }*/
            jobs = jobs.OrderByDescending(x => Math.Min(x.Time1, x.Time2)).ToList();
            for(int i = 0; i < jobs.Count; i++)
            {
                if(totalTime1 > totalTime2)
                {
                    result.Add((jobs[i].Number, 2));
                    totalTime2 += jobs[i].Time2;
                } else if(totalTime2 < totalTime1)
                {
                    result.Add((jobs[i].Number, 1));
                    totalTime1 += jobs[i].Time1;
                }
                else
                {
                    if (Math.Min(jobs[i].Time1, jobs[i].Time2) == jobs[i].Time1)
                    {
                        result.Add((jobs[i].Number, 1));
                        totalTime1 += jobs[i].Time1;
                    }
                    else
                    {
                        result.Add((jobs[i].Number, 2));
                        totalTime2 += jobs[i].Time2;
                    }
                }
            }
            
            File.WriteAllText("output.txt", string.Join(" ", result.OrderBy(j => j.jobNumber).Select(j => j.manNumber)));
        }

        record Job(int Number, short Time1, short Time2)
        {
        };
    }
}