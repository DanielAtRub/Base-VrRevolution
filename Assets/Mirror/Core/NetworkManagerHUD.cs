using UnityEngine;

namespace Mirror
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/Network Manager HUD")]
    [RequireComponent(typeof(NetworkManager))]
    public class NetworkManagerHUD : MonoBehaviour
    {
        NetworkManager manager;

        public bool showGUI = true;
        public int offsetX;
        public int offsetY;

        // Flags externos (Quest / automatización)
        public bool QuestC;
        public bool QuestS;
        public bool QuestSonly;

        // Protección contra múltiples llamadas
        bool clientStarted;
        bool serverStarted;

        void Awake()
        {
            manager = GetComponent<NetworkManager>();
        }

        void Update()
        {
            // CLIENT
            if (QuestC && !clientStarted && !NetworkClient.active)
            {
                clientStarted = true;
                manager.StartClient();
            }

            // HOST
            if (QuestS && !serverStarted && !NetworkServer.active)
            {
                serverStarted = true;
                manager.StartHost();
            }

            // SERVER ONLY
            if (QuestSonly && !serverStarted && !NetworkServer.active)
            {
                serverStarted = true;
                manager.StartServer();
            }

            // Reset flags when disconnected
            if (!NetworkClient.active && !NetworkServer.active)
            {
                clientStarted = false;
                serverStarted = false;
            }
        }

        void OnGUI()
        {
            if (!showGUI)
                return;

            int width = 300;
            GUILayout.BeginArea(new Rect(10 + offsetX, 40 + offsetY, width, 9999));

            if (!NetworkClient.isConnected && !NetworkServer.active)
                StartButtons();
            else
                StatusLabels();

            GUILayout.EndArea();
        }

        void StartButtons()
        {
            if (!NetworkClient.active)
            {
#if UNITY_WEBGL
                GUILayout.Box("(WebGL cannot be server)");
#else
                if (GUILayout.Button("Host (Server + Client)"))
                    manager.StartHost();
#endif
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Client"))
                    manager.StartClient();

                manager.networkAddress = GUILayout.TextField(manager.networkAddress);

                if (Transport.active is PortTransport portTransport)
                {
                    if (ushort.TryParse(
                        GUILayout.TextField(portTransport.Port.ToString()),
                        out ushort port))
                    {
                        portTransport.Port = port;
                    }
                }

                GUILayout.EndHorizontal();

#if !UNITY_WEBGL
                if (GUILayout.Button("Server Only"))
                    manager.StartServer();
#endif
            }
            else
            {
                GUILayout.Label($"Connecting to {manager.networkAddress}...");
                if (GUILayout.Button("Cancel Connection Attempt"))
                    manager.StopClient();
            }
        }

        void StatusLabels()
        {
            if (NetworkServer.active && NetworkClient.active)
            {
                GUILayout.Label("<b>Host</b>");
            }
            else if (NetworkServer.active)
            {
                GUILayout.Label("<b>Server</b>");
            }
            else if (NetworkClient.isConnected)
            {
                GUILayout.Label("<b>Client</b>");
            }
        }

        public void StopButtons()
        {
            if (NetworkServer.active && NetworkClient.active)
            {
                if (GUILayout.Button("Stop Host"))
                {
                    manager.StopHost();
                    ResetFlags();
                }
            }
            else if (NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Client"))
                {
                    manager.StopClient();
                    ResetFlags();
                }
            }
            else if (NetworkServer.active)
            {
                if (GUILayout.Button("Stop Server"))
                {
                    manager.StopServer();
                    ResetFlags();
                }
            }
        }

        void ResetFlags()
        {
            clientStarted = false;
            serverStarted = false;
            QuestC = false;
            QuestS = false;
            QuestSonly = false;
        }
    }
}
