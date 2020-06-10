using MarvelHeroes.Models;
using MarvelHeroes.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MarvelHeroes.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private ObservableCollection<Personagem> _listaPersonagens;
        private MarvelApiService _marvelApiService;

        public ObservableCollection<Personagem> ListaPersonagem
        {
            get { return _listaPersonagens; }
            set { this.SetProperty(ref _listaPersonagens, value); }
        }

        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Titulo = "Marvel Heroes";
            ListaPersonagem = new ObservableCollection<Personagem>();
            _marvelApiService = new MarvelApiService();
            LoadAsync();
        }

        public override async Task LoadAsync()
        {
            Ocupado = true;
            try
            {
                var personagensMarvel = await _marvelApiService.GetPersonagensAsync();

                ListaPersonagem.Clear();

                foreach (var personagem in personagensMarvel)
                {
                    ListaPersonagem.Add(personagem);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
            finally
            {
                Ocupado = false;
            }
        }
    }
}
