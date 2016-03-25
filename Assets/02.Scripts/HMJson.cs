using System.Collections;
using LitJson;
using System;

namespace itemPool
{
    public class HMJson
    {
        static public ArrayList arrayFromJsonData(JsonData jsonData)
        {
            ArrayList arrayList = new ArrayList();

            if (jsonData.IsArray)
            {
                IList jsonList = (IList)jsonData;
                IEnumerator weaponListIt = jsonList.GetEnumerator();

                while (weaponListIt.MoveNext())
                {
                    JsonData value = (JsonData)weaponListIt.Current;
                    object convertedValue = null;
                    if (value.IsArray)
                    {
                        convertedValue = HMJson.arrayFromJsonData(value);
                    }
                    else if (value.IsObject)
                    {
                        convertedValue = HMJson.hashtableFromJsonData(value);
                    }
                    else
                    {
                        convertedValue = value.ToString();
                    }

                    arrayList.Add(convertedValue);
                }
            }

            return arrayList;
        }

        static public Hashtable hashtableFromJsonData(JsonData jsonData)
        {
            Hashtable hashTable = new Hashtable();

            if (jsonData.IsObject)
            {
                IDictionary jsonDictionary = (IDictionary)jsonData;
                IDictionaryEnumerator it = jsonDictionary.GetEnumerator();

                while (it.MoveNext())
                {
                    String key = it.Key.ToString();
                    JsonData value = (JsonData)it.Value;
                    object convertedValue = null;

                    if (value.IsArray)
                    {
                        convertedValue = HMJson.arrayFromJsonData(value);
                    }
                    else if (value.IsObject)
                    {
                        convertedValue = HMJson.hashtableFromJsonData(value);
                    }
                    else
                    {
                        convertedValue = value.ToString();
                    }

                    hashTable.Add(key, convertedValue);
                }
            }


            return hashTable;
        }

        static public object objectFromJsonString(String jsonString)
        {
            JsonData value = JsonMapper.ToObject(jsonString);

            object ret = null;
            if (value.IsArray)
            {
                ret = HMJson.arrayFromJsonData(value);
            }
            else if (value.IsObject)
            {
                ret = HMJson.hashtableFromJsonData(value);
            }
            else
            {
                ret = value.ToString();
            }

            return ret;
        }
    }
}

