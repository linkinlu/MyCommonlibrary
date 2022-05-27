//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using SCSCommon.Emunerable;

//namespace SCSCommon.EFAduit
//{
//    class EfContextAduitTrail
//    {
//        //private void AddAuditLog(ObjectStateEntry entry)
//        //{

//        //    var authUser = GetAuthInfo();

//        //    var logId = Guid.NewGuid().ToString();
//        //    //var modalityIdFieldMetadata = entry.CurrentValues.DataRecordInfo.FieldMetadata.FirstOrDefault(c =>
//        //    //    c.FieldType.Name == nameof(ModalityMaintance.ModalityId));
//        //    var eventType = GetEventState(entry);
//        //    var auditLog = new Audit()
//        //    {
//        //        Action = (int) eventType,
//        //        CreateUserId = authUser.Item1,
//        //        CreateTime = DateTime.Now,
//        //        UserName = authUser.Item2,
//        //        Id = logId,
//        //        //Single PK
//        //        RecordId =  entry.Entity.GetType().GetProperty(nameof(Audit.Id))?.GetValue(entry.Entity)
//        //                ?.ToString(), // entry.EntityKey.EntityKeyValues.FirstOrDefault()?.Value?.ToString(),
//        //        TableName = entry.Entity.GetType().Name,
//        //        //OtherRecordId = modalityIdFieldMetadata.FieldType != null
//        //        //    ? entry.CurrentValues[modalityIdFieldMetadata.Ordinal]?.ToString()
//        //        //    : null,
//        //        //  DomainId = authUser.Item3,
//        //    };
//        //    AuditDetailHandler handler = null;
//        //    switch (eventType)
//        //    {
//        //        case EntityState.Added:
//        //            handler = new AddAduitOperator();
//        //            break;

//        //        case EntityState.Deleted:
//        //            handler = new HardDeleteAuditHandler();
//        //            break;

//        //        case EntityState.Modified:
//        //            handler = new ModifyAuditHandler();
//        //            break;
//        //    }

//        //    if (handler != null)
//        //    {
//        //        //this.Set<Audit>().Add(auditLog);
//        //        var details = handler.CreateAuditDetails(auditLog, entry);

//        //        if (details != null && details.ToList().HasValue())
//        //        {
//        //            // this.Set<AuditDetail>().AddRange(details);
//        //        }
//        //    }
//        //}

//        //private Tuple<string, string, string, string> GetAuthInfo()
//        //{
//        //    var userId = "";
//        //    var userName = "";
//        //    var domainId = string.Empty; //CommonConsts.DefaultDomain;
//        //    var siteId = string.Empty; //CommonConsts.DefaultSiteId;
//        //    if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
//        //    {
//        //        var identities = Thread.CurrentPrincipal.Identity.Name.Split('@');
//        //        if (identities.Length >= 3)
//        //        {
//        //            domainId = identities[1];
//        //            userId = identities[2];
//        //        }

//        //        userName = identities[0];
//        //        if (identities.Length == 4) siteId = identities[3];
//        //    }

//        //    return new Tuple<string, string, string, string>(userId, userName, domainId, siteId);
//        //}

//        //private EntityState GetEventState(ObjectStateEntry entry)
//        //{
//        //    if (entry.State == EntityState.Modified)
//        //    {
//        //        if (entry.Entity is ISoftDelete)
//        //        {
//        //            if (entry.OriginalValues[nameof(ISoftDelete.IsDeleted)] != null &&
//        //                (bool) entry.OriginalValues[nameof(ISoftDelete.IsDeleted)] == false &&
//        //                entry.CurrentValues[nameof(ISoftDelete.IsDeleted)] != null &&
//        //                (bool) entry.CurrentValues[nameof(ISoftDelete.IsDeleted)])
//        //            {

//        //                return EntityState.Deleted;
//        //            }
//        //        }

//        //        return EntityState.Modified;
//        //    }

//        //    return entry.State;

//        //}
//    }

//    public interface ISoftDelete
//    {
//        bool IsDeleted { get; set; }
//    }


//    public class AuditDetail

//    {
//        //public override object Id { get { return Id; } set { Id = value.ToString(); } }

//        public string AuditId { get; set; }

//        public string Id { get; set; }

//        public string OldValue { get; set; }

//        public string NewValue { get; set; }
//        public string PropertyName { get; set; }
//        public string DataType { get; set; }

//        public string PropertyChineseName { get; set; }
//    }

//    public class Audit

//    {
//        //public override object Id { get { return Id; } set { Id = value.ToString(); } }

//        public string Id { get; set; }
//        public string RecordId { get; set; }

//        public string OtherRecordId { get; set; }

//        //public string UserId { get; set; }
//        public string UserName { get; set; }
//        public string TableName { get; set; }
//        public int Action { get; set; }

//        [NotTracking] public string CreateUserId { get; set; }
//        [NotTracking] public DateTime? CreateTime { get; set; }

//    }

//    [AttributeUsage(AttributeTargets.Property)]
//    public class NotTrackingAttribute : Attribute
//    {

//    }


//    public abstract class AuditDetailHandler
//    {
//        public IEnumerable<AuditDetail> CreateAuditDetails(Audit audit, ObjectStateEntry entry)
//        {
//            var trackingPropertyNames = GetTrackingPropertyInfos(entry);
//            if (trackingPropertyNames.HasValue())
//            {
//                return InnerAuditDetails(audit, entry, trackingPropertyNames);
//            }

//            return null;
//        }

//        public string GetDefaultVale(Type type, object obj)
//        {
//            if (type.IsValueType)
//            {
//                return Activator.CreateInstance(type).ToString();
//            }
//            //else if(type.IsEnum)
//            //{
//            //    return type.GetCustomAttributes<DescriptionAttribute>().
//            //}

//            return null;
//        }

//        public static dynamic ConvertEnumWithDescription<T>() where T : struct, IConvertible
//        {
//            if (!typeof(T).IsEnum)
//            {
//                throw new ArgumentException("Type given T must be an Enum");
//            }

//            var enumType = typeof(T).Name;
//            var valueDescriptions = Enum.GetValues(typeof(T))
//                .Cast<Enum>()
//                .ToDictionary(Convert.ToInt32, GetEnumDescription);
//            return new
//            {
//                Type = enumType,
//                ValueDescriptions = valueDescriptions
//            };

//        }

//        public static string GetEnumDescription(Enum value)
//        {
//            FieldInfo fi = value.GetType().GetField(value.ToString());

//            DescriptionAttribute[] attributes =
//                (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

//            if (attributes.Length > 0)
//                return attributes[0].Description;
//            return value.ToString();
//        }


//        public object GetValueByType(object obj, string propertyName)
//        {
//            if (obj == null || string.IsNullOrEmpty(propertyName)) return null;
//            //var prop = obj.GetType().GetProperty(propertyName);

//            if (obj.GetType().IsEnum)
//            {
//                FieldInfo fi = obj.GetType().GetField(obj.ToString());
//                return fi.GetCustomAttribute<DescriptionAttribute>()?.Description;
//            }

//            if (obj is bool b)
//            {
//                return b ? "是" : "否";

//            }

//            return obj;
//        }

//        public string GetPropertyChineseName(string prop, object obj)
//        {
//            if (string.IsNullOrEmpty(prop))
//            {
//                return null;
//            }

//            return obj.GetType().GetProperty(prop)?.GetCustomAttribute<DescriptionAttribute>()?.Description;
//        }

//        public abstract IEnumerable<AuditDetail> InnerAuditDetails(Audit audit, ObjectStateEntry entry,
//            List<string> trackingProperList);

//        private List<string> GetTrackingPropertyInfos(ObjectStateEntry entry)
//        {
//            var trackProperties = entry.Entity.GetType().GetProperties()
//                .Where(c => c.GetCustomAttribute<NotTrackingAttribute>() == null)
//                .ToList();
//            return trackProperties.Where(c => c.Name != nameof(Audit.Id)).Select(c => c.Name)
//                .ToList(); //.Intersect(entry.GetModifiedProperties()).ToList();
//        }
//    }

//    public class AddAduitOperator : AuditDetailHandler
//    {
//        public override IEnumerable<AuditDetail> InnerAuditDetails(Audit audit, ObjectStateEntry entry,
//            List<string> trackingProperList)
//        {
//            var audits = new List<AuditDetail>();
//            foreach (var prop in trackingProperList)
//            {
//                if (entry.CurrentValues[prop] == null ||
//                    string.IsNullOrEmpty(entry.CurrentValues[prop].ToString())) continue;

//                //var properType = entry.Entity.GetType().GetProperty(prop)?.GetType();
//                var curVal = GetValueByType(entry.CurrentValues[prop], prop);
//                audits.Add(new AuditDetail()
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    AuditId = audit.Id,
//                    NewValue = string.IsNullOrEmpty(curVal?.ToString()) ? null : curVal?.ToString(),
//                    // OldValue = null ,GetDefaultVale(properType, entry.Entity),
//                    PropertyName = prop,
//                    PropertyChineseName = GetPropertyChineseName(prop, entry.Entity),
//                    DataType = curVal?.GetType()?.Name,
//                    //DomainId = audit.DomainId,
//                });
//            }

//            return audits;
//        }
//    }




//    public class ModifyAuditHandler : AuditDetailHandler
//    {
//        public override IEnumerable<AuditDetail> InnerAuditDetails(Audit audit, ObjectStateEntry entry,
//            List<string> trackingProperList)
//        {
//            foreach (var prop in trackingProperList)
//            {
//                var properType = entry.Entity.GetType().GetProperty(prop)?.GetType();
//                if (ComparatorFactory.GetComparator(properType)
//                    .AreEqual(entry.OriginalValues[prop], entry.CurrentValues[prop])) continue;


//                var oldVal = GetValueByType(entry.OriginalValues[prop], prop);
//                var newVal = GetValueByType(entry.CurrentValues[prop], prop);
//                yield return new AuditDetail()
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    AuditId = audit.Id,
//                    NewValue = string.IsNullOrEmpty(newVal?.ToString())
//                        ? null
//                        : newVal?.ToString(), //GetValueByType( entry.CurrentValues[prop], prop),
//                    OldValue = string.IsNullOrEmpty(oldVal?.ToString()) ? null : oldVal?.ToString(),
//                    PropertyName = prop,
//                    DataType = string.IsNullOrEmpty(oldVal?.ToString())
//                        ? newVal?.GetType()?.Name
//                        : oldVal?.GetType()?.Name,
//                    PropertyChineseName = GetPropertyChineseName(prop, entry.Entity),
//                };
//            }
//        }
//    }

//    //public class SoftDeleteAuditHandler : AuditDetailHandler
//    //{
//    //    public override IEnumerable<AuditDetail> InnerAuditDetails(Audit audit, ObjectStateEntry entry, List<string> trackingProperList)
//    //    {
//    //        foreach (var prop in trackingProperList)
//    //        {
//    //            if (entry.OriginalValues[prop] == null ||
//    //                string.IsNullOrEmpty(entry.OriginalValues[prop].ToString())) continue;

//    //            yield return new AuditDetail()
//    //            {
//    //                Id = Guid.NewGuid().ToString(),
//    //                Audit = audit.Id,
//    //                OldValue = entry.OriginalValues[prop].ToString(),
//    //                PropertyName = prop,
//    //                PropertyChineseName = GetPropertyChineseName(prop, entry.Entity),
//    //            };
//    //        }
//    //    }
//    //}

//    public class HardDeleteAuditHandler : AuditDetailHandler
//    {
//        public override IEnumerable<AuditDetail> InnerAuditDetails(Audit audit, ObjectStateEntry entry,
//            List<string> trackingProperList)
//        {
//            foreach (var prop in trackingProperList)
//            {
//                if (entry.OriginalValues[prop] == null ||
//                    string.IsNullOrEmpty(entry.OriginalValues[prop].ToString())) continue;

//                var oldVal = GetValueByType(entry.OriginalValues[prop], prop);
//                yield return new AuditDetail()
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    AuditId = audit.Id,
//                    OldValue = string.IsNullOrEmpty(oldVal?.ToString()) ? null : oldVal?.ToString(),
//                    DataType = oldVal?.GetType().Name,
//                    PropertyName = prop,
//                    PropertyChineseName = GetPropertyChineseName(prop, entry.Entity),
//                };
//            }
//        }
//    }

//    public class ComparatorFactory
//    {
//        public static Comparator GetComparator(Type type)
//        {
//            if (Nullable.GetUnderlyingType(type) == typeof(DateTime))
//            {
//                return new NullableDateComparator();
//            }

//            if (type == typeof(DateTime))
//            {
//                return new DateComparator();
//            }

//            if (type == typeof(string))
//            {
//                return new StringComparator();
//            }

//            if (Nullable.GetUnderlyingType(type) == null)
//            {
//                return new NullableComparator();
//            }

//            if (type.IsValueType)
//            {
//                return new ValueTypeComparator();
//            }

//            return new Comparator();
//        }

//    }

//    public class NullableComparator : Comparator
//    {
//        public override bool AreEqual(object value1, object value2)
//        {
//            if (value1 == null && value2 == null) return true;
//            if (value1 == null && value2 != null) return value2.Equals(value1);

//            return value1.Equals(value2);
//        }
//    }

//    public class NullableDateComparator : DateComparator
//    {
//        public override bool AreEqual(object value1, object value2)
//        {
//            DateTime? date1 = (DateTime?) value1 ?? DateTime.MinValue;
//            DateTime? date2 = (DateTime?) value2 ?? DateTime.MinValue;

//            return base.AreEqual(date1, date2);
//        }
//    }

//    public class DateComparator : Comparator
//    {
//        public override bool AreEqual(object value1, object value2)
//        {
//            DateTime date1 = (DateTime) value1;
//            DateTime date2 = (DateTime) value2;

//            return date1.Year == date2.Year &&
//                   date1.Month == date2.Month &&
//                   date1.Day == date2.Day &&
//                   date1.Hour == date2.Hour &&
//                   date1.Minute == date2.Minute &&
//                   date1.Second == date2.Second;
//        }
//    }

//    public class StringComparator : Comparator
//    {
//        public override bool AreEqual(object value1, object value2)
//        {
//            return String.CompareOrdinal(Convert.ToString(value1), Convert.ToString(value2)) == 0;
//        }
//    }

//    public class ValueTypeComparator : Comparator
//    {
//        public override bool AreEqual(object value1, object value2)
//        {
//            return value1.Equals(value2);
//        }
//    }

//    public class Comparator
//    {
//        public virtual bool AreEqual(object value1, object value2)
//        {
//            return value1 == value2;
//        }
//    }


//}
