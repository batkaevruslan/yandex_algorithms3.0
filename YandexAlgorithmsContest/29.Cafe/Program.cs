using System;
using System.Collections.Generic;
using System.Linq;

namespace _29.Cafe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int totalDays = int.Parse(Console.ReadLine());

            int[] costPerDay = new int[totalDays];
            for (int i = 0; i < totalDays; i++)
            {
                costPerDay[i] = int.Parse(Console.ReadLine());
            }

            var result = GetMinimalCost(costPerDay);

            Console.WriteLine(result.totalCost);
            Console.WriteLine($"{result.leftCoupons} {result.usedCoupons}");
            Console.WriteLine(string.Join(" ", result.daysWhenCouponWasUsed));
        }

        private static (int totalCost, int leftCoupons, int usedCoupons, List<int> daysWhenCouponWasUsed) GetMinimalCost(int[] costPerDay)
        {
            int totalDays = costPerDay.Length;
            EndDayResult?[,] dp = new EndDayResult?[totalDays, totalDays + 2];
            for (int day = 0; day < totalDays; day++)
            {
                for (int couponCount = 0; couponCount <= day + 1; couponCount++)
                {
                    int dinnerCost = costPerDay[day];
                    EndDayResult? costIfCouponUsed = day > 0 && dp[day - 1, couponCount + 1] != null
                                            ? new EndDayResult(WasCouponUsed: true, dp[day - 1, couponCount + 1].Value.TotalCost)
                                            : null;
                    EndDayResult? costIfCouponCountDoesNotChange = dinnerCost <= 100
                                                                    ? day > 0 && dp[day - 1, couponCount] != null
                                                                        ? new EndDayResult(WasCouponUsed: false, dp[day - 1, couponCount].Value.TotalCost + dinnerCost)
                                                                        : day == 0 && couponCount == 0
                                                                            ? new EndDayResult(WasCouponUsed: false, dinnerCost)
                                                                            : null
                                                                    : null;
                    EndDayResult? costIfCouponEarned = dinnerCost > 100 && couponCount > 0
                                                        ? day > 0 
                                                            ? dp[day - 1, couponCount - 1] != null
                                                                ? new EndDayResult(WasCouponUsed: false, dp[day - 1, couponCount - 1].Value.TotalCost + dinnerCost)
                                                                : null
                                                            : new EndDayResult(WasCouponUsed: false, dinnerCost)
                                                        : null;
                    if (costIfCouponUsed != null)
                    {
                        if (costIfCouponCountDoesNotChange != null)
                        {
                            dp[day, couponCount] = costIfCouponUsed.Value.TotalCost < costIfCouponCountDoesNotChange.Value.TotalCost
                                                    ? costIfCouponUsed
                                                    : costIfCouponCountDoesNotChange;
                        }
                        else if (costIfCouponEarned != null)
                        {
                            dp[day, couponCount] = costIfCouponUsed.Value.TotalCost < costIfCouponEarned.Value.TotalCost
                                                    ? costIfCouponUsed
                                                    : costIfCouponEarned;
                        }
                        else
                        {
                            dp[day, couponCount] = costIfCouponUsed;
                        }
                    }
                    else
                    {
                        dp[day, couponCount] = costIfCouponCountDoesNotChange != null ? costIfCouponCountDoesNotChange : costIfCouponEarned;
                    }

                }
            }
            EndDayResult? bestDay = null;
            int couponsLeftCount = 0;
            for (int couponCount = totalDays; couponCount >= 0 && totalDays > 0; couponCount--)
            {
                if (dp[totalDays - 1, couponCount].HasValue)
                {
                    if (!bestDay.HasValue)
                    {
                        bestDay = dp[totalDays - 1, couponCount];
                        couponsLeftCount = couponCount;
                    }
                    else if (bestDay.Value.TotalCost > dp[totalDays - 1, couponCount].Value.TotalCost)
                    {
                        bestDay = dp[totalDays - 1, couponCount];
                        couponsLeftCount = couponCount;
                    }
                }
            }

            int usedCouponCount = 0;
            int currentCouponCount = couponsLeftCount;
            int currentDay = totalDays - 1;
            List<int> dayWhenCouponWasUsed = new List<int>();
            while (currentDay >= 0)
            {
                if (dp[currentDay, currentCouponCount].Value.WasCouponUsed)
                {
                    currentCouponCount++;
                    usedCouponCount++;
                    dayWhenCouponWasUsed.Add(currentDay + 1);
                }
                else if (costPerDay[currentDay] > 100)
                {
                    currentCouponCount--;
                }
                currentDay--;
            }
            dayWhenCouponWasUsed.Reverse();
            return (totalCost: bestDay?.TotalCost ?? 0, leftCoupons: couponsLeftCount, usedCount: usedCouponCount, couponUsagePerDay: dayWhenCouponWasUsed);
        }

        record struct EndDayResult(bool WasCouponUsed, int TotalCost);
    }
}