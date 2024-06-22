﻿using Layui.Core.Base;
using Layui.Core.Resource;
using LayUI.Wpf.Global;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiComponentExample.ViewModels
{
    public class DataGridViewModel : LayuiViewModelBase
    {
        public DataGridViewModel(IContainerExtension container) : base(container)
        {
        }
        private Visibility _Visibility;
        /// <summary>
        /// 控制当前的列展示状态
        /// </summary>
        public Visibility Visibility
        {
            get { return _Visibility; }
            set { SetProperty(ref _Visibility, value); }
        }
        private bool _IsShow=true;
        public bool IsShow
        {
            get { return _IsShow; }
            set { 
                SetProperty(ref _IsShow, value);
                if (value) Visibility = Visibility.Visible;
                else Visibility = Visibility.Collapsed;
            }
        }
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        private ObservableCollection<Data> _ListData;
        public ObservableCollection<Data> ListData
        {
            get { return _ListData; }
            set { SetProperty(ref _ListData, value); }
        }
        private Task<ObservableCollection<Data>> LoadedListData()
        {
            try
            {
                var random = new Random();
                ObservableCollection<Data> ListData = new ObservableCollection<Data>();
                for (int i = 0; i < 10000; i++)
                {
                    int num = random.Next(1, 101);
                    ListData.Add(new Data (){ Index = i, Name = "测试" + i, ProgressBarValue = num });
                };
                return Task.FromResult(ListData);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async override void ExecuteLoadedCommand()
        {
            base.ExecuteLoadedCommand();
            IsActive = true;
            await Task.Delay(2000);
            ListData = await LoadedListData();
            IsActive = false;

        }
        private DelegateCommand<Data> _GetItemsCommand;
        public DelegateCommand<Data> GetItemsCommand =>
            _GetItemsCommand ?? (_GetItemsCommand = new DelegateCommand<Data>(ExecuteGetItemsCommand));

        void ExecuteGetItemsCommand(Data data)
        {
            LayDialogParameter dialogParameter = new LayDialogParameter();
            dialogParameter.Add("Message", JsonConvert.SerializeObject(data));
            LayDialog.Show("DialogAlert", dialogParameter);
        }
        private DelegateCommand<Data> _DeleteCommand;
        public DelegateCommand<Data> DeleteCommand =>
            _DeleteCommand ?? (_DeleteCommand = new DelegateCommand<Data>(ExecuteDeleteCommand));

        void ExecuteDeleteCommand(Data data)
        {
            ListData.Remove(data);
        }
        public class Data : BindableBase
        {
            private int _Index;
            public int Index
            {
                get { return _Index; }
                set { SetProperty(ref _Index, value); }
            }
            private string _Name;
            public string Name
            {
                get { return _Name; }
                set { SetProperty(ref _Name, value); }
            }
            private double _ProgressBarValue;
            public double ProgressBarValue
            {
                get { return _ProgressBarValue; }
                set { SetProperty(ref _ProgressBarValue, value); }
            }
        }
    }
}
