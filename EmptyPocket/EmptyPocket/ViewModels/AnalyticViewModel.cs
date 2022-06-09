using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using EmptyPocket.Models;
using Microcharts;
using Microcharts.Forms;
using System.Linq;

namespace EmptyPocket.ViewModels
{
    public class AnalyticViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; }

        public List<ChartEntry> entries;

        public DonutChart chart;
        public DonutChart Chart
        {
            get => chart;
            set {
                SetProperty(ref chart, value);
            }
        }

        public string[] colors;

        public AnalyticViewModel()
        {
            Categories = new ObservableCollection<Category>();
            entries = new List<ChartEntry>();
            chart = new DonutChart
            {
                LabelTextSize = 35,
                HoleRadius = 0.8f,
            };
            colors = new string[]
            {
                "#2CA58D",
                "#0A2342",
                "#84BC9C",
                "#F46197",
            };
        }

        public async void OnAppearing()
        {
            //await LoadItems(Categories, CatDataStore);
            await LoadCategories();
            LoadEntries();
        }

        public async Task<bool> LoadCategories()
        {
            IsBusy = true;

            try
            {
                Categories.Clear();
                var items = await App.Database.database.Table<Category>().Where(x => x.Type != "Доход").ToListAsync();
                foreach (var item in items)
                {
                    Categories.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            finally
            {
                IsBusy = false;
            }
            return true;
        }

        public void LoadEntries()
        {
            entries.Clear();
            for (var i = 0; i < Categories.Count; i++)
            {
                entries.Add(new ChartEntry(Categories[i].Sum)
                {
                    Label = Categories[i].Name,
                    Color = SkiaSharp.SKColor.Parse(colors[i]),
        
                });
            }
            Chart.Entries = entries;
        }
    }
}
