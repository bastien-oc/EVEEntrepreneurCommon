using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
namespace Doku {
  public static class ExtensionMethods {

      #region Strings
      public static bool IsValidEmail(string strIn)
      {
          // Return true if strIn is in valid e-mail format.
          return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
      }

      public static string FirstCharToUpper(this string s)
      {
          if (string.IsNullOrEmpty(s))
              throw new ArgumentException("There is no first letter");

          char[] a = s.ToCharArray();
          a[0] = char.ToUpper(a[0]);
          return new string(a);
      }

      #endregion

      #region Digits

      /// <summary>
      /// Cap the specified value to limit.
      /// </summary>
      /// <returns>The cap.</returns>
      /// <param name="value">Value.</param>
      /// <param name="limit">Limit.</param>
      public static float Cap(this float value, float limit)
      {
          if (value > limit) return limit;
          else return value;
      }

      /// <summary>
      /// Cap the specified value to limit.
      /// </summary>
      /// <returns>The cap.</returns>
      /// <param name="value">Value.</param>
      /// <param name="limit">Limit.</param>
      public static int Cap(this int value, int limit)
      {
          if (value > limit) return limit;
          else return value;
      }

      public static double RoundDown(double figure, int precision)
      {
          return Math.Floor(figure * Math.Pow(10, precision)) / Math.Pow(10, precision);
      }

      public static double RoundUp(double figure, int precision)
      {
          return Math.Ceiling(figure * Math.Pow(10, precision)) / Math.Pow(10, precision);
      }

      public static float ClosestTo(this IEnumerable<float> collection, float target)
      {

          var closest = float.MaxValue;
          var minDifference = float.MaxValue;

          foreach (var element in collection)
          {
              var difference = Mathf.Abs((long)element - target);
              if (minDifference > difference)
              {
                  minDifference = (float)difference;
                  closest = element;
              }
          }

          return closest;
      }  

      #endregion

      #region Dates

      public static DateTime FromUnixTime(int unixTime)
      {
          return epoch.AddSeconds(unixTime);
      }

      private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
      //string time = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
      //t.Hours,
      //t.Minutes,
      //t.Seconds,
      //t.Milliseconds);

      #endregion
  }
}
