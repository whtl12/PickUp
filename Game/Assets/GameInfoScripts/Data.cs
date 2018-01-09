using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//데이터 최상위 클레스.
public abstract class Data : MonoBehaviour
{
    //독스 번호.
    public enum DocsTable
    {
        Common = 0,
        Item = 883628827,
        Map = 15220500,
        Character = 463712266,
        Shop = 1225390854,
        Skill = 975315946,
        Max
    }

    //데이터 읽어오기 ( 추상화 )
    public abstract void LoadData();


}
