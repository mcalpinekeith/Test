using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Test.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Data
{
    public class Recipe : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _name;

        [MaxLength(255)]
        public string Name {
            get { return _name; }
            set
            {
                if (_name == value) return;

                _name = value;

                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SqlPage : ContentPage
    {
        private SQLiteAsyncConnection _db;
        private ObservableCollection<Recipe> _recipes;

        public SqlPage()
        {
            InitializeComponent();

            _db = DependencyService.Get<ISQLiteService>().GetConnection("Test.db");
        }

        protected override async void OnAppearing()
        {
            await _db.CreateTableAsync<Recipe>();

            var recipes = await _db.Table<Recipe>().ToListAsync();
            _recipes = new ObservableCollection<Recipe>(recipes);

            RecipeListView.ItemsSource = _recipes;

            base.OnAppearing();
        }

        async void OnAdd(object sender, EventArgs e)
        {
            var recipe = new Recipe { Name = "Recipe " + DateTime.Now.Ticks };

            await _db.InsertAsync(recipe);

            _recipes.Add(recipe);
        }

        async void OnUpdate(object sender, EventArgs e)
        {
            var recipe = _recipes[0];

            recipe.Name += " UPDATED";

            await _db.UpdateAsync(recipe);
        }

        async void OnDelete(object sender, EventArgs e)
        {
            var recipe = _recipes[0];

            await _db.DeleteAsync(recipe);

            _recipes.Remove(recipe);
        }
    }
}