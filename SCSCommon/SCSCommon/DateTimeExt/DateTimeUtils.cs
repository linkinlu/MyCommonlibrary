using System;

namespace SCSCommon.DateTimeExt
{
    public static class DateTimeUtils
    {
       

        public static string ToHHmmss(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return date.ToString(string.Format("HH{0}mm{0}ss", timeSpliter));
        }

        public static string ToHHmm(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return date.ToString(string.Format("HH{0}mm{0}", timeSpliter));
        }

        public static string ToyyyyMMddHHmmssfffffff(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return string.IsNullOrEmpty(DateSpliterConstant.dateSpliter)
                ? date.ToString("yyyyMMddHHmmssfffffff")
                : date.ToString(string.Format("yyyy{0}MM{0}dd HH{1}mm{1}ss{1}fffffff", dateSpliter, timeSpliter));
        }

        public static string ToyyyyMMddHHmmss(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return string.IsNullOrEmpty(DateSpliterConstant.dateSpliter)
              ? date.ToString("yyyyMMddHHmmss")
              : date.ToString(string.Format("yyyy{0}MM{0}dd HH{1}mm{1}ss", dateSpliter, timeSpliter));
        }

        public static string ToyyMMddHHmm(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return string.IsNullOrEmpty(DateSpliterConstant.dateSpliter)
             ? date.ToString("yyMMddHHmm")
             : date.ToString(string.Format("yy{0}MM{0}dd HH{1}mm", dateSpliter, timeSpliter));
        }

        public static string ToyyMMddHHmmss(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return string.IsNullOrEmpty(DateSpliterConstant.dateSpliter)
            ? date.ToString("yyMMddHHmmss")
            : date.ToString(string.Format("yy{0}MM{0}dd HH{1}mm{1}ss", dateSpliter, timeSpliter));            
        }

        public static string ToyyyyMMddHHmm(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return string.IsNullOrEmpty(DateSpliterConstant.dateSpliter)
           ? date.ToString("yyyyMMddHHmm")
           : date.ToString(string.Format("yyyy{0}MM{0}dd HH{1}mm", dateSpliter, timeSpliter));
        }

        public static string ToyyyyMMdd(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return string.IsNullOrEmpty(DateSpliterConstant.dateSpliter)
         ? date.ToString("yyyyMMdd")
         : date.ToString(string.Format("yyyy{0}MM{0}dd", dateSpliter));
        }

      

        public static string ToyyyyMM(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return string.IsNullOrEmpty(DateSpliterConstant.dateSpliter)
        ? date.ToString("yyyyMM")
        : date.ToString(string.Format("yyyy{0}MM", dateSpliter));
        }

        public static string ToyyMMdd(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {

            return string.IsNullOrEmpty(DateSpliterConstant.dateSpliter)
                ? date.ToString("yyMMdd")
                : date.ToString(string.Format("yyyy{0}MM{0}dd", dateSpliter));
        }

        public static string Toyyyy(this DateTime date,string dateSpliter = DateSpliterConstant.dateSpliter,string timeSpliter = DateSpliterConstant.timeSpliter)
        {
            return date.ToString("yyyy");
        }
    }


    public class DateSpliterConstant
    {
        public const string dateSpliter = "";

        public const string timeSpliter = "";

    }
}