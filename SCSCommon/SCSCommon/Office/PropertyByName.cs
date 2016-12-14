using SCSCommon.Convertion;
using System;


namespace SCSCommon.Office
{
    public class PropertyByName<T>
    {
        private object propertyValue;
        private int columnOrder = -1;

        public PropertyByName(string name, Func<T, object> func =null, bool isIngore = false)
        {
            Name = name;
            Func = func;
            Ingore = isIngore;
        }

        public Func<T, object> Func { get; set; }

        public string Name { get; set; }

        public bool Ingore { get; set; }



        public int? GetIntNullVal()
        {
            return propertyValue.To(default(int?));
        }

        public int GetIntVal()
        {
            return propertyValue.To(default(int));
        }

        public decimal? GetDecimalNullVal()
        {
            return propertyValue.To(default(decimal?));
        }

        public decimal GetDecimalVal()
        {
            return propertyValue.To(default(decimal));
        }

        public string GetStringVal()
        {
            return propertyValue.To(string.Empty);
        }

        public double? GetDoubleNullVal()
        {
            return propertyValue.To(default(double?));
        }

        public double GetDoubleVal()
        {
            return propertyValue.To(default(double));
        }


        public DateTime? GetDateTimeNullVal()
        {
            return propertyValue.To(default(DateTime?));
        }

        public DateTime GetDateTimeVal()
        {
            return propertyValue.To(default(DateTime));
        }


        public object PropertyVal {
            get { return propertyValue; }
            set { propertyValue = value; }
        }

        public int ColumnOrder {
            get { return columnOrder; }
            set { columnOrder = value; }
        }
    }
}
