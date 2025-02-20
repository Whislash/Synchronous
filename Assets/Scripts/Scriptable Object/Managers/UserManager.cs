using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KWY
{
    public class UserManager : MonoBehaviour
    {
        static GameObject _userData;
        static GameObject userData { get { return _userData; } }

        static UserManager _instance;

        public static UserManager Instance
        {
            get
            {
                if (!_instance)
                {
                    _userData = new GameObject
                    {
                        name = "UserData"
                    };
                    _instance = userData.AddComponent(typeof(UserManager)) as UserManager;
                    DontDestroyOnLoad(_userData);
                }
                return _instance;
            }
        }

        private string _accountId; // ������
        private Sprite _userIcon;
        private int _userLevel;
        private ulong _userId;
        private string _userName;

        public static string AccountId { get { return Instance._accountId; } }
        public static Sprite UserIcon { get { return Instance._userIcon; } }
        public static int UserLevel { get { return Instance._userLevel; } }
        public static ulong UserId { get { return Instance._userId; } }
        public static string UserName { get { return Instance._userName; } }

        /// <summary>
        /// It should be called when login is successful; before using the instance; 
        /// </summary>
        /// <param name="userIcon">user profile icon</param>
        /// <param name="accountId">user unique id allocated from the server</param>
        /// <param name="userLevel">user level</param>
        /// <param name="userId">user id</param>
        /// <param name="userName">user name</param>
        public static void InitData(Sprite userIcon, string accountId, int userLevel, ulong userId, string userName)
        {
            Instance._accountId = accountId;
            Instance._userIcon = userIcon;
            Instance._userLevel = userLevel;
            Instance._userId = userId;
            Instance._userName = userName;
        }

    }
}
