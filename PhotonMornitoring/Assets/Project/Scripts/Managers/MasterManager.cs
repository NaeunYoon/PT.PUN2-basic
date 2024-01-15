using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Master", menuName = "Singletons/MasterManager", order = int.MinValue)]

public class MasterManager : ScriptableObjectSigleton<MasterManager>
{
    //싱글톤 scriptableObject를 상속받는 MasterManager 가 GameSetting을 갖고있음. 게임셋팅은 기본적인 정보를 갖고있음. 추가가능
    [SerializeField] private GameSetting _gameSettings;
    public static GameSetting GameSettings
    {
        get { return Instance._gameSettings; }
    }

}
