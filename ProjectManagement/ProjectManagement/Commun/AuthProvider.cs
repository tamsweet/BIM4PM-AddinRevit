﻿namespace BIM4PM.UI.Commun
{
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using BIM4PM.UI.Models;
    using BIM4PM.UI.Tools;
    using RestSharp;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class AuthProvider : ObservableObject
    {
        private AuthProvider()
        {
        }

        public static AuthProvider Instance { get; } = new AuthProvider();

        public User CurrentUser { get; set; }

        public Token token { get; set; }

        private bool _isAuthenticated;

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                _isAuthenticated = value;
                OnAuthenticationChanged();
            }
        }
        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;

            set {
                _isConnected = value;
                OnConnectionChanged();
            }
        }
       

        
        public void Logout()
        {
            IsAuthenticated = false;
            token = null;
            IsConnected = false;
            PaletteViewModel.TabItems.RemoveAt(0);
           
        }

        public event EventHandler AuthenticationChanged ;
        public event EventHandler ConnectionChanged;

        protected virtual void OnAuthenticationChanged()
        {
            (AuthenticationChanged as EventHandler)?.Invoke(this, EventArgs.Empty);
            if (IsAuthenticated == true)
            {
                new Thread(() => PaletteUtilities.LaunchPanel())
                {
                    Priority = ThreadPriority.BelowNormal,
                    IsBackground = true
                }.Start();

                RestRequest req = new RestRequest(Route.GetMe, Method.GET);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);

                IRestResponse res = Route.Client.Execute(req);

                UserRes User = JsonConvert.DeserializeObject<UserRes>(res.Content);

                CurrentUser = User.data;
            }
        }
        
        protected virtual void OnConnectionChanged()
        {
            (ConnectionChanged as EventHandler)?.Invoke(this, EventArgs.Empty);
        }
    }
}
