using UnityEngine;
[CreateAssetMenu(fileName = "GameSetting", menuName = "Manager/GameSettingr", order = int.MinValue)]
public class GameSetting : ScriptableObject
{
    [SerializeField] private string gameVersion = "0.0.0";
    public string GameVersion { get { return gameVersion; }}

    [SerializeField] private string nickName = "Whale";
    public string NickName
    {
        set { nickName = value;}
        get
        {
            return nickName;
        }
    }
}
