using DDD.Domain.Entities;
using DDD.WinForm.ViewModels;
using DDD.WinForm.Views;
using System;
using System.Windows.Forms;

namespace DDD.WinForm
{
    public partial class WeatherLatestView : Form       //WeatherLatestViewはFormを継承している
    {
        private WeatherLatestViewModel _viewModel =         //　_viewModelモデルの生成
            new WeatherLatestViewModel();
        public WeatherLatestView()                      //コンストラクタの生成
        {
            InitializeComponent();

            this.AreasComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.AreasComboBox.DataBindings.Add(
                "SelectedValue", _viewModel, nameof(_viewModel.SelectedAreaId));
            this.AreasComboBox.DataBindings.Add(
                "DataSource", _viewModel, nameof(_viewModel.Areas));
            this.AreasComboBox.ValueMember = nameof(AreaEntity.AreaId);
            this.AreasComboBox.DisplayMember = nameof(AreaEntity.AreaName);

            this.DataDateLabel.DataBindings.Add(
                "Text", _viewModel, nameof(_viewModel.DataDateText));
            this.ConditionLabel.DataBindings.Add(
                "Text", _viewModel, nameof(_viewModel.ConditionText));
            this.TemperatureLabel.DataBindings.Add(
                "Text", _viewModel, nameof(_viewModel.TemperatureText));
        }

        private void LatestButton_Click(object sender, EventArgs e)    //場所を選択して直近値を押すと天気情報を出力
        {
            _viewModel.Search();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(var f = new WeatherListView())                
            {
                f.ShowDialog();                                         //Listボタンを押すと　天気一覧を開く
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using(var f  = new WeatherSaveView())                       
            {
                f.ShowDialog();                                         //追加ボタンを押すと　天気の情報を追加する画面を開く
            }
        }
    }
}
