using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _6.OperatingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            double sectorCount = double.Parse(sr.ReadLine()!);
            short osCount = short.Parse(sr.ReadLine()!);
            List<OS> operatingSystems = new(osCount);
            for (int i = 0; i < osCount; i++)
            {
                double[] range = sr.ReadLine()!.Split(' ').Select(double.Parse).ToArray();
                operatingSystems.Add(new OS(StartSector: range[0], EndSector: range[1]));
            }

            int result = GetWorkingOS(operatingSystems, sectorCount).Count;
            File.WriteAllText("output.txt", result.ToString());
        }
        private static bool OSOverlap(OS first, OS second)
        {
            return first.StartSector <= second.StartSector && second.StartSector <= first.EndSector
                || first.StartSector <= second.EndSector && second.EndSector <= first.EndSector
                || second.StartSector <= first.StartSector && first.StartSector <= second.EndSector
                || second.StartSector <= first.EndSector && first.EndSector <= second.EndSector;
        }
        public static List<OS> GetWorkingOS(List<OS> oses, double sectorCount)
        {
            if (oses.Count == 0) { return new List<OS>(); }
            if (oses.Count == 1) { return oses; }
            List<OS> workingOS = new(oses.Count) { oses[^1] };
            for (int i = 0; i < oses.Count - 1; i++)
            {
                OS firstOS = oses[i];
                bool isFirstOSWorking = true;
                for (int j = i + 1; j < oses.Count; j++)
                {
                    OS secondOS = oses[j];
                    if (firstOS.StartSector <= firstOS.EndSector)
                    {
                        if (secondOS.StartSector <= secondOS.EndSector)
                        {
                            isFirstOSWorking = !OSOverlap(firstOS, secondOS);
                        }
                    }
                    else
                    {
                        if (secondOS.StartSector <= secondOS.EndSector)
                        {
                            isFirstOSWorking = !OSOverlap(new OS(firstOS.StartSector, sectorCount), secondOS)
                                && !OSOverlap(new OS(0, firstOS.EndSector), secondOS);
                        } else
                        {
                            isFirstOSWorking = !OSOverlap(new OS(firstOS.StartSector, sectorCount), new OS(secondOS.StartSector, sectorCount
                                )) 
                                && !OSOverlap(new OS(0, firstOS.EndSector), new OS(0, secondOS.EndSector));
                        }
                    }
                    if (!isFirstOSWorking)
                    {
                        break;
                    }
                } 
                if (isFirstOSWorking)
                {
                    workingOS.Add(firstOS);
                }
            }
            return workingOS;
        }

        public record struct OS(double StartSector, double EndSector);
    }
}