using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElaped;
        int matchsFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            tenthOfSecondsElaped++;
            timeTextBlock.Text = (tenthOfSecondsElaped / 10F).ToString("0.0s");
            if(matchsFound==8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - paly again? ";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐎","🐎",
                "🦌","🦌",
                "🦏","🦏",
                "🦛","🦛",
                "🐂","🐂",
                "🐃","🐃",
                "🦘","🦘",
                "🦄","🦄",
            };
            Random random = new Random();
            //foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            //{
            //    int index = random.Next(animalEmoji.Count);
            //    string nextEmoji = animalEmoji[index];
            //    textBlock.Text = nextEmoji;
            //    animalEmoji.RemoveAt(index);
            //}
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if(textBlock.Name!="timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthOfSecondsElaped = 0;
            matchsFound = 0;

        }
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(findingMatch==false)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = true;
                lastTextBlockClicked = textBlock;
            }
            else if(textBlock.Text==lastTextBlockClicked.Text)
            {
                matchsFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchsFound == 8)
                SetUpGame();
        }

        
    }
}
