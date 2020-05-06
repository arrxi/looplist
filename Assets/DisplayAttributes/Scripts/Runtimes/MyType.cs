using UnityEngine;
  
public class MyType:ExposableMonobehaviour
{
        [HideInInspector, SerializeField] int m_SomeInt;
        [HideInInspector, SerializeField] float m_SomeFloat;
        [HideInInspector, SerializeField] bool m_SomeBool;
        [HideInInspector, SerializeField] string m_Etc;
        [HideInInspector, SerializeField] MonoBehaviour m_Obj;
  
        [ExposeProperty] public int SomeInt
        {
                get { return m_SomeInt; }
                set { m_SomeInt = value; }
        }
  
        [ExposeProperty] public float SomeFloat
        {
                get { return m_SomeFloat; }
                set { m_SomeFloat = value; }
        }
  
        [ExposeProperty] public bool SomeBool
        {
                get { return m_SomeBool; }
                set { m_SomeBool = value; }
        }
  
        [ExposeProperty] public string SomeString
        {
                get { return m_Etc; }
                set { m_Etc = value; }
        }
  
        [ExposeProperty] public MonoBehaviour Somenoxss
        {
                get { return m_Obj; }
                set { m_Obj = value; }
        }
        public enum TT{
            ONE,
            TWO
        }
          [HideInInspector, SerializeField]TT tT;
          [ExposeProperty]public TT MT{
              get{return tT;}
              set{tT=value;}
          }
}