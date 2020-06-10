﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MarvelHeroes.Services;
using Prism.Mvvm;
using Prism.Navigation;

namespace MarvelHeroes.ViewModels
{
    public class ViewModelBase : BindableBase, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigationService NavigationService { get; set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                SetProperty(ref _titulo, value);
            }
        }

        private bool _ocupado;
        public bool Ocupado
        {
            get { return _ocupado; }
            set
            {
                SetProperty(ref _ocupado, value);
            }
        }

        public virtual Task LoadAsync(object[] args) => Task.FromResult(true);

        public virtual Task LoadAsync() => Task.FromResult(true);
    }
}