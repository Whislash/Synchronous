using Photon.Pun;
using Photon.Realtime;

using ExitGames.Client.Photon;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace KWY
{
    public class MainEvent : MonoBehaviourPun
    {
        #region Private Serializable Fields

        [Tooltip("The button to send ready to start simulation to server")]
        [SerializeField] private Button ReadyBtn;

        [Tooltip("The panel to prepare the turn: choosing characters and actions...")]
        [SerializeField] private GameObject TurnReadyPanel;

        [Tooltip("The panel to show 'You win'")]
        [SerializeField] private GameObject WinPanel;

        [Tooltip("The panel to show 'You lose'")]
        [SerializeField] private GameObject LosePanel;

        #endregion

        #region Private Fields

        [Tooltip("������ ������ ���۵Ǹ� �ε�� scene")]
        readonly private string nextLevel = "";

        [Tooltip("Unique user id that the server determined")]
        private string UserId;

        [Tooltip("True: this user is on ready state with the SERVER; False: opposite")]
        bool readyTurn = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// Send to 'ready signal to start simulation' to the Server; content: actionData: Dictionary(int, int)
        /// </summary>
        public void RaiseEventTurnReady()
        {
            Dictionary<int, int> actionData = new Dictionary<int, int>(); // temp

            byte evCode = (byte)EvCode.TurnReady;

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            if (PhotonNetwork.RaiseEvent(evCode, actionData, raiseEventOptions, sendOptions))
            {
                UtilForDebug.LogRaiseEvent(evCode, actionData, raiseEventOptions, sendOptions);
            }
            else
            {
                UtilForDebug.LogErrorRaiseEvent(evCode);
            }
        }

        /// <summary>
        /// Send to 'Simulation end signal' to the Server; content: simulEnd?: bool(always)
        /// </summary>
        public void RaiseEventSimulEnd()
        {
            byte evCode = (byte)EvCode.SimulEnd;

            // ������ ������ �ʾ��� ��� = true, ������ ��� false
            var content = true;

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            if (PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions))
            {
                UtilForDebug.LogRaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            }
            else
            {
                UtilForDebug.LogErrorRaiseEvent(evCode);
            }
        }

        /// <summary>
        /// Send to 'Game ends' signal to the Server; It must be called ONLY Master Client; content: winnerId: string
        /// </summary>
        public void RaiseEventGameEnd()
        {
            byte evCode = (byte)EvCode.GameEnd;

            var content = UserId; // temp �̱� ������ id ���� content��
            // content�� �̱� ������ id �� �ֱ�

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            if (PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions))
            {
                UtilForDebug.LogRaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            }
            else
            {
                UtilForDebug.LogErrorRaiseEvent(evCode);
            }
        }

        public void RaiseEventPlayerSkill1()
        {
            byte evCode = (byte)EvCode.PlayerSkill1;

            object[] content = new object[]
            {

            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            if (PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions))
            {
                UtilForDebug.LogRaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            }
            else
            {
                UtilForDebug.LogErrorRaiseEvent(evCode);
            }
        }

        public void RaiseEventPlayerSkill2()
        {
            byte evCode = (byte)EvCode.PlayerSkill2;

            object[] content = new object[]
            {

            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            if (PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions))
            {
                UtilForDebug.LogRaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            }
            else
            {
                UtilForDebug.LogErrorRaiseEvent(evCode);
            }
        }

        public void RaiseEventPlayerSkill3()
        {
            byte evCode = (byte)EvCode.PlayerSkill3;

            object[] content = new object[]
            {

            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            if (PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions))
            {
                UtilForDebug.LogRaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            }
            else
            {
                UtilForDebug.LogErrorRaiseEvent(evCode);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Callback method when the server raises events
        /// </summary>
        /// <param name="eventData">Received data from the server</param>
        private void OnEvent(EventData eventData)
        {
            UtilForDebug.LogData(eventData);

            switch (eventData.Code)
            {
                case (byte)EvCode.ResTurnReady:
                    OnEventTurnReady(eventData);
                    break;
                case (byte)EvCode.ResSimulEnd:
                    OnEventSimulEnd(eventData);
                    break;
                case (byte)EvCode.ResGameEnd:
                    OnEventGameEnd(eventData);
                    break;
                default:
                    Debug.LogError("There is not matching event code: " + eventData.Code);
                    break;
            }

        }

        /// <summary>
        /// Method when the server responses at client's TurnReady event; data: [userId: string, resOk: bool, startSimul: bool, data?: Dictionary(int, int)]
        /// </summary>
        /// <param name="eventData">Received data from the server</param>
        private void OnEventTurnReady(EventData eventData)
        {
            object[] data = (object[])eventData.CustomData;

            if (UserId == (string)data[0] && (bool) data[1])
            {
                // ������ ���� ready�� ���� ok ������ ���� �� ������
                readyTurn = true; // �غ� �Ϸ� ���·� ���� -> �� �̻� �غ� �Ϸ� ��ư �������� �ϱ�

                // �ӽ÷� ready �Ϸ� �Ǹ� ��ư blue�� ����
                ReadyBtn.GetComponent<Image>().color = Color.blue;
            }

            // check ' start simulation' through data[2]
            if ((bool) data[2])
            {
                Debug.Log("Start Simulation");

                // When simulation starts, hide the TurnReadyPanel
                TurnReadyPanel.SetActive(false);

                // simulation�� master client�� ��츸 ���� -> master client���� object �׼� -> Photon ����ȭ -> �ٸ� client������ �Ȱ��� ����
                // ���� �׽�Ʈ ���� ���Ͽ���!!!!!!!!
                if (PhotonNetwork.IsMasterClient)
                {
                    Simulation((Dictionary<int, int>)data[3]); // Note: if data[2] is false, there is not data[3]
                }
            }
        }

        /// <summary>
        /// Executing simulation; It should be called only on MASTER CLIENT
        /// </summary>
        /// <param name="actionData">Data to simulate characters and actions</param>
        private void Simulation(Dictionary<int, int> actionData)
        {
            // �Ф� 
        }

        /// <summary>
        /// Method when the server responses the singal, 'simulation ended'; data: continue?: bool
        /// </summary>
        /// <param name="eventData">Received data from the server</param>
        private void OnEventSimulEnd(EventData eventData)
        {
            // �ùķ��̼� ���� ��, �ٽ� TurnReadyPanel ���̱�
            TurnReadyPanel.SetActive(true);

            // �غ� ���� �ٽ� false�� �ʱ�ȭ
            readyTurn = false;
        }

        /// <summary>
        /// Method when the server responses the signal, 'the game ended'; data: [winnerId: string]
        /// </summary>
        /// <param name="eventData">Received data from the server</param>
        private void OnEventGameEnd(EventData eventData)
        {
            var data = eventData.CustomData;

            if (!(data is string))
            {
                Debug.LogError("Type Error");
                return;
            }

            // �̰��� ���
            if ((string)data == this.UserId)
            {
                WinPanel.SetActive(true);
            }
            // ���� ���
            else
            {
                LosePanel.SetActive(true);
            }

            // ���� ���� �� �� ����� �ۼ�
        }

        #endregion


        #region MonoBehaviourPun Callbacks
        private void Awake()
        {
            // when the scene loaded, get userid from PhotonNetwork.
            // �ش� �Լ��� ����Ǳ����� ���� ������ ������ ���־����
            try
            {
                UserId = PhotonNetwork.AuthValues.UserId;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                Debug.LogError("Can not get UserId - Check the server connection");
            }
        }

        public void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        public void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }

        #endregion
    }

}