using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample
{
    [Serializable]
    public class MainClass
    {

        public int MyProperty { get; set; }
        public String prop1 { get; set; }
        public int prop2 { get; set; }
        public String prop3 { get; set; }

        public string prop4 { get; set; }
        public string prop5 { get; set; }
        public string prop6 { get; set; }
        public string prop7 { get; set; }
        public string prop8 { get; set; }
        public string prop9 { get; set; }
        public string prop41 { get; set; }
        public string prop42 { get; set; }
        public string prop43 { get; set; }
        public string prop44 { get; set; }
        public string prop45 { get; set; }
        public string prop46 { get; set; }

        public IList<SubClass1> subClass1 { get; set; }
        public List<SubClass2> subClass2 { get; set; }
        public List<SubClass3> subClass3 { get; set; }
        public List<SubClass4> subClass4 { get; set; }
        public List<SubClass5> subClass5 { get; set; }
    }
    [Serializable]
    public class SubClass1
    {
        public int subprop1 { get; set; }
        public int subprop2 { get; set; }
        public int subprop3 { get; set; }
        public int subprop4 { get; set; }
        public int subprop5 { get; set; }
    }
    [Serializable]
    public class SubClass2
    {
        public int subprop1 { get; set; }
        public int subprop2 { get; set; }
        public int subprop5 { get; set; }
    }
    [Serializable]
    public class SubClass3
    {
        public int subprop2 { get; set; }
        public int subprop3 { get; set; }
        public int subprop4 { get; set; }
        public int subprop5 { get; set; }
    }
    [Serializable]
    public class SubClass4
    {
        public int subprop1 { get; set; }
        public int subprop2 { get; set; }
        public int subprop3 { get; set; }
        public int subprop4 { get; set; }
        public int subprop5 { get; set; }
    }

    [Serializable]
    public class SubClass5
    {
        public int subprop1 { get; set; }
        public int subprop2 { get; set; }
        public int subprop3 { get; set; }
        public int subprop4 { get; set; }
        public int subprop5 { get; set; }
    }
}
