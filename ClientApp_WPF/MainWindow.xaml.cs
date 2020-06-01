using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace ClientApp_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppViewModel appVM = new AppViewModel();
        private DoubleAnimation widthAnimation;        

        public MainWindow()
        {                
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            DataContext = appVM;
            widthAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(2),
                AutoReverse = true
            };
            textStatusBar.SetBinding(TextBlock.TextProperty, new Binding(nameof(appVM.CurrentFileName))
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            resultList.ItemsSource = appVM.ResultList;
            resultList.SetBinding(ListBox.SelectedItemProperty, new Binding(nameof(appVM.SelectedItem))
            {
                Mode = BindingMode.TwoWay
            });
            humanResponse.ItemsSource = appVM.HumanResponseList;
            PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Application.Current.Shutdown(); };
        }

        private void SendRequest()
        {
            if (request.Text.Length != 0)
            {
                appVM.SendRequest(request.Text);
                requestSent.BeginAnimation(OpacityProperty, widthAnimation);
            }

            //Storyboard sbOnMouseLeave = new Storyboard();
            //var doubleAnimationUsingKeyFrames2 = new DoubleAnimationUsingKeyFrames();
            //Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames2, new PropertyPath("(UIElement.Opacity)"));
            //Storyboard.SetTargetName(doubleAnimationUsingKeyFrames2, Name);
            //doubleAnimationUsingKeyFrames2.KeyFrames.Add(new EasingDoubleKeyFrame(opacity,
            //KeyTime.FromTimeSpan(
            //    TimeSpan.FromMilliseconds(400))));
        }

        private void Click_SendRequest(object sender, RoutedEventArgs e)
        {
            SendRequest();            
        }

        private void KeyDown_Request(object sender, KeyEventArgs e)
        {
            if (appVM.DataType == DataType.Human && e.Key == Key.Enter) SendRequest();
        }

        private void ShowResultList() 
        {
            result.Visibility = Visibility.Visible;
        }

        private void HideResultList()
        {
            result.Visibility = Visibility.Collapsed;
        }

        private void ShowResponseDetails()
        {
            responseButtons.Visibility = Visibility.Visible;
            appVM.SetResponseDetails();
            if (appVM.DataType == DataType.Human) ShowHumanRequestList();
            else ShowMathRequestList();
        }

        private void HideResponseButtons()
        {
            responseButtons.Visibility = Visibility.Collapsed;
        }

        private void ShowHumanRequestList()
        {
            humanResponse.Visibility = Visibility.Visible;
        }

        private void HideHumanRequestList()
        {
            humanResponse.Visibility = Visibility.Collapsed;
        }

        private void ShowMathRequestList()
        {
            mathResponse.Visibility = Visibility.Visible;
            responseStack.Children.Clear();
            foreach (var detail in appVM.MathResponseList)
            {
                responseStack.Children.Add(new UniformGrid()
                {
                    Columns = 3,
                    Children = {
                        new ScrollViewer()
                        {
                            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                            Content = new TextBlock()
                            {
                                Text = detail.Common
                            }
                        },
                        new ScrollViewer()
                        {
                            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                            Content = new TextBlock()
                            {
                                Text = detail.Diff1
                            }
                        },
                        new ScrollViewer()
                        {
                            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                            Content = new TextBlock()
                            {
                                Text = detail.Diff2
                            }
                        }
                    }
                });
            }
        }

        private void HideMathRequestList()
        {
            mathResponse.Visibility = Visibility.Collapsed;
        }

        private void listResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (resultList.SelectedIndex == -1) return;
            HideResultList();
            ShowResponseDetails();                            
        }

        private void Click_Back(object sender, RoutedEventArgs e)
        {
            HideResponseButtons();
            HideHumanRequestList();
            HideMathRequestList();
            ShowResultList();
            resultList.SelectedIndex = -1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Human.IsChecked = true;
            appVM.Start();
        }        

        private void Checked_Human(object sender, RoutedEventArgs e)
        {
            appVM.DataType = DataType.Human;
            Math.IsChecked = false;
            appVM.RebuildResponseList();
            request.AcceptsReturn = false;
            request.Height = 30;
        }        

        private void Checked_Math(object sender, RoutedEventArgs e)
        {
            appVM.DataType = DataType.Math;
            Human.IsChecked = false;
            appVM.RebuildResponseList();
            request.AcceptsReturn = true;
            request.Height = mainGrid.RowDefinitions[Grid.GetRow(request)].ActualHeight * 0.8;
        }

        private void Click_MenuCopy(object sender, RoutedEventArgs e)
        {
            request.Copy();
        }

        private void Click_MenuPaste(object sender, RoutedEventArgs e)
        {
            request.Paste();
        }

        private void Click_MenuCut(object sender, RoutedEventArgs e)
        {
            request.Cut();
        }

        private void Click_MenuSelectAll(object sender, RoutedEventArgs e)
        {
            request.SelectAll();
        }

        private void Click_MenuClear(object sender, RoutedEventArgs e)
        {
            request.Clear();
        }

        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            if (request.SelectedText == "")
                ItemCopy.IsEnabled = ItemCut.IsEnabled = false;
            else
                ItemCopy.IsEnabled = ItemCut.IsEnabled = true;

            if (Clipboard.ContainsText())
                ItemPaste.IsEnabled = true;
            else
                ItemPaste.IsEnabled = false;
        }

        private void Click_Draw(object sender, RoutedEventArgs e)
        {
            appVM.Draw();
        }

        private void txtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int row = request.GetLineIndexFromCharacterIndex(request.CaretIndex);
            int col = request.CaretIndex - request.GetCharacterIndexFromLineIndex(row);
            lblCursorPosition.Text = "Line " + (row + 1) + ", Char " + (col + 1);
        }

        private void Click_NewFile(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text file (*.txt)|*.txt|Dat File (*.dat)|*.dat",
                RestoreDirectory = true
            };
            if (saveFileDialog.ShowDialog() == true) appVM.CurrentFileName = saveFileDialog.FileName;
        }

        private void Click_OpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Text file (*.txt)|*.txt|Dat File (*.dat)|*.dat",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                request.Text = File.ReadAllText(openFileDialog.FileName);
                appVM.CurrentFileName = openFileDialog.FileName;
            }
        }

        private void Click_SaveFile(object sender, RoutedEventArgs e)
        {
            if (request.Text == string.Empty) MessageBox.Show("Запрос не введен");
            else
            {
                if (appVM.CurrentFileName == null) SaveAsFile();
                else File.WriteAllText(appVM.CurrentFileName, request.Text);
            }
        }
            
        private void Click_SaveAsFile(object sender, RoutedEventArgs e)
        {
            if (request.Text == string.Empty) MessageBox.Show("Запрос не введен");
            else SaveAsFile();
        }
        
        private void SaveAsFile(){
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text file (*.txt)|*.txt|Dat File (*.dat)|*.dat",
                RestoreDirectory = true
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                appVM.CurrentFileName = saveFileDialog.FileName;
                File.WriteAllText(appVM.CurrentFileName, request.Text);
            }
        }

        private void Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\u00A9 2020 Microwife Corporation.\nAll rights reserved.", "About");
        }

        private void Ribbon_Loaded(object sender, RoutedEventArgs e)
        {
            Grid child = VisualTreeHelper.GetChild((DependencyObject)sender, 0) as Grid;
            if (child != null)
            {
                child.RowDefinitions[0].Height = new GridLength(0);
            }
        }

        private void Click_Exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }    
}
