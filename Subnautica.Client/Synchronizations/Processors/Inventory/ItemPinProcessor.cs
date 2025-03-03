﻿namespace Subnautica.Client.Synchronizations.Processors.Inventory
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    
    using System.Collections;

    using UnityEngine;

    using ServerModel = Subnautica.Network.Models.Server;

    public class ItemPinProcessor : NormalProcessor
    {
        /**
         *
         * Zamanlanmış veri gönderim durumu
         *
         * @author Ismail <ismaiil_0234@hotmail.com>
         *
         */
        private static bool IsSending { get; set; } = false;

        /**
         *
         * Gelen veriyi işler
         *
         * @author Ismail <ismaiil_0234@hotmail.com>
         *
         */
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            return true;
        }

        /**
         *
         * Yeni pin eklendiğinde tetiklenir.
         * Pin kaldırıldığında tetiklenir.
         * Pin taşındığında tetiklenir.
         *
         * @author Ismail <ismaiil_0234@hotmail.com>
         *
         */
        public static void OnProcessPin()
        {
            if (!IsSending && !EventBlocker.IsEventBlocked(ProcessType.ItemPin))
            {
                UWE.CoroutineHost.StartCoroutine(SendServerData());
            }
        }

        /**
         *
         * Zamanlanmış veriyi sunucuya gönderir.
         *
         * @author Ismail <ismaiil_0234@hotmail.com>
         *
         */
        private static IEnumerator SendServerData()
        {
            IsSending = true;

            yield return new WaitForSecondsRealtime(0.5f);

            ServerModel.ItemPinArgs result = new ServerModel.ItemPinArgs()
            {
                Items = PinManager.main.pins
            };

            NetworkClient.SendPacket(result);

            IsSending = false;
        }
    }
}

