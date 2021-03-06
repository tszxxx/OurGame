﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameClient
{
    public class Unity : MonoBehaviour
    {
        private static Client myClient;
        private Player[] myPlayers;
        GameObject cubeContral = null;
        bool ClientReady = false;
        // Use this for initialization
        void Start()
        {
            if (ClientReady == false)
            {
                myClient = new Client();
                myPlayers = new Player[10];
                Client.Begin();
                cubeContral = GameObject.Find("Cube");
                ClientReady = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                myClient.UseMagicQ((int)Input.mousePosition.x, (int)Input.mousePosition.y);
                myClient.SetSendReady();
                cubeContral.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                myClient.UseMagicW((int)Input.mousePosition.x, (int)Input.mousePosition.y);
                myClient.SetSendReady();
                cubeContral.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                myClient.UseMagicE((int)Input.mousePosition.x, (int)Input.mousePosition.y);
                myClient.SetSendReady();
                cubeContral.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                myClient.UseMagicR((int)Input.mousePosition.x, (int)Input.mousePosition.y);
                myClient.SetSendReady();
                cubeContral.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
            }
        }
        private void FixedUpdate()
        {
            for (int i = 0; i < 10; i++)
            {
                myPlayers[i] = myClient.getPlayer(i);
            }
        }
        private void OnMouseDown()
        {
            myClient.PressMouse((int)Input.mousePosition.x, (int)Input.mousePosition.y);
        }
    }
}
