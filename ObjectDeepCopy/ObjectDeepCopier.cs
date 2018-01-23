using System;
using System.Collections.Generic;

namespace ObjectDeepCopy
{
    public static class ObjectDeepCopier
    {
        public static Object Copy(Object objectToDeepCopy, Dictionary<object, object> originalToCopyMappings = null)
        {
            if (objectToDeepCopy == null)
            {
                throw new ArgumentNullException("objectToDeepCopy","Object to deep copy cannot be null");
            }

            Type t = objectToDeepCopy.GetType();
            Object deepCopyObject = Activator.CreateInstance(t);

            if (originalToCopyMappings == null)
            {
                originalToCopyMappings = new Dictionary<object, object>();
                originalToCopyMappings.Add(objectToDeepCopy, deepCopyObject);
            }

            foreach (var field in t.GetFields())
            {
                if (field.FieldType.IsValueType || field.FieldType == typeof(String))
                {
                    deepCopyObject.GetType().GetField(field.Name).SetValue(deepCopyObject, t.GetField(field.Name).GetValue(objectToDeepCopy));
                }
                else
                {
                    Object key = t.GetField(field.Name).GetValue(objectToDeepCopy);
                    // if current processed field value is null, then continue processing with the next field
                    if(key == null) {
                        continue;
                    }

                    if (originalToCopyMappings.ContainsKey(key))
                    {
                        deepCopyObject.GetType().GetField(field.Name).SetValue(deepCopyObject, originalToCopyMappings[key]);
                    }
                    else
                    {
                        Object newOne = Copy(key, originalToCopyMappings);
                        originalToCopyMappings.Add(key, newOne);
                        deepCopyObject.GetType().GetField(field.Name).SetValue(deepCopyObject, newOne);
                    }
                }
            }

            foreach (var property in t.GetProperties())
            {
                if (property.PropertyType.IsValueType || property.PropertyType == typeof(String))
                {
                    deepCopyObject.GetType().GetProperty(property.Name).SetValue(deepCopyObject, t.GetProperty(property.Name).GetValue(objectToDeepCopy));
                }
                else
                {
                    Object key = t.GetProperty(property.Name).GetValue(objectToDeepCopy);
                    // if current processed property value is null, then continue processing with the next property
                    if (key == null)
                    {
                        continue;
                    }

                    if (originalToCopyMappings.ContainsKey(key))
                    {
                        deepCopyObject.GetType().GetProperty(property.Name).SetValue(deepCopyObject, originalToCopyMappings[key]);
                    }
                    else
                    {
                        Object newOne = Copy(key, originalToCopyMappings);
                        originalToCopyMappings.Add(key, newOne);
                        deepCopyObject.GetType().GetProperty(property.Name).SetValue(deepCopyObject, newOne);
                    }
                }
            }

            return deepCopyObject;
        }
    }
}
