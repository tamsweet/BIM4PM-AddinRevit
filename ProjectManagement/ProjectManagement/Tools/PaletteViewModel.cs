﻿namespace BIM4PM.UI.Tools
{
    using GalaSoft.MvvmLight;
    using BIM4PM.UI.Commun;
    using BIM4PM.UI.Tools.Discussion;
    using BIM4PM.UI.Tools.History;
    using BIM4PM.UI.Tools.Project;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Windows.Controls;
    using Prism.Events;
    using BIM4PM.UI.Events;
    using System;

    public class PaletteViewModel : ViewModelBase, IConnectObserver
    {
       public static ObservableCollection<TabItem> TabItems { get; set; } = new ObservableCollection<TabItem>();

        public PaletteViewModel(IEventAggregator eventAggregator)
        { 
            //if (TabItems.Count == 0)
            //{
            //    TabItems.Add(new TabItem
            //    {
            //        Content = new ProjectView() { DataContext = new ProjectViewModel() },
            //        Header = "Connect"
            //    });
            //}

            eventAggregator.GetEvent<AuthEvent>().Subscribe(OnAuth);
        }

        private void OnAuth(bool obj)
        {
            if (TabItems.Count == 0)
            {
                TabItems.Add(new TabItem
                {
                    Content = new ProjectView() { DataContext = new ProjectViewModel() },
                    Header = "Connect"
                });
            }
        }

        public void Update(IConnect connect)
        {
           
            if(ProjectModelConnect.IsConnected == true)
            {
                TabItems.Add(new TabItem
                {
                    Content = new HistoryView() { DataContext = new HistoryViewModel() },
                    Header = "History"
                });
                TabItems.Add(new TabItem
                {
                    Content = new DiscussionView() { DataContext = new DiscussionViewModel() },
                    Header = "Discussion"
                });
            }
            else
            {
                int count = TabItems.Count;
                for (int i = 1; i < count; i++)
                {
                    TabItems.RemoveAt(1);

                }
            }
        }
    }
}
