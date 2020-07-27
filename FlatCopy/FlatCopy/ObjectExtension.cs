using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlatCopy
{
    public static class ObjectExtension
    {
        public static object GetFlatCopy<T>(this T sourceEntity, int maxDepthLevel = 0)
        {
            var eo = new ExpandoObject();
            var entity = (ICollection<KeyValuePair<string, object>>)eo;


            foreach (PropertyInfo property in sourceEntity.GetType().GetProperties())
            {
                var propertyValue = property.GetValue(sourceEntity);

                if (property.PropertyType.IsSimpleType())
                {
                    entity.Add(new KeyValuePair<string, object>(property.Name, propertyValue));
                }
                else if (property.PropertyType.GetInterfaces().Any(x => x.Name == typeof(IId).Name))
                {
                    if (propertyValue != null)
                    {
                        entity.Add(new KeyValuePair<string, object>(property.Name, maxDepthLevel == 0 ?
                            new { id = (propertyValue as IId).Id } : //Nivel mas chato solo saca el Id
                            (propertyValue as IId).GetFlatCopy(maxDepthLevel - 1) //Un nivel mas interno saca todos sus valores
                            ));
                    }
                    else
                    {
                        entity.Add(new KeyValuePair<string, object>(property.Name, null));
                    }
                }
            }
            return eo as dynamic;
        }
    }
}
