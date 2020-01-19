using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;

namespace AvansKTWTool.ViewModel
{
    public class CalculateNotRegistered
    {
        //properties
        public string AllKanidatesInformation { get; set; } = "";
        public string ChosenKanidatesInformation { get; set; } = "";

        //commands
        public ICommand CalculateNotRegisteredCommand { get; set; }

        //private fields
        private List<string> _allKanidatesList;
        private List<string> _chosenKanidatesList;
        private List<string> _leftoverKanidatesList;
        
        //constructor
        public CalculateNotRegistered()
        {
            //Init
            _allKanidatesList = new List<string>();
            _chosenKanidatesList = new List<string>();
            _leftoverKanidatesList = new List<string>();

            //RelayCommands
            CalculateNotRegisteredCommand = new RelayCommand(CalculateNotRegisteredKanidates);

        }

        private void CalculateNotRegisteredKanidates()
        {
            if (AllKanidatesInformation.Equals("") || ChosenKanidatesInformation.Equals("")) return;

            List<string> _allKanidatesList = AllKanidatesInformation.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();   
            
            List<string> _chosenKanidatesList = ChosenKanidatesInformation.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            _leftoverKanidatesList = _allKanidatesList.Except(_chosenKanidatesList).ToList();

            string fileName = @"D:\Kanidates.txt"; 

            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("The following kanidates are not registered:");
                    sw.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");

                    foreach (var name in _leftoverKanidatesList)
                    {
                        sw.WriteLine(name);
                    }

                    sw.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                    sw.WriteLine("Export date: {0}", DateTime.Now.ToString(CultureInfo.CurrentCulture));
                    sw.WriteLine("Application made by {0}", "Kenneth Gerrits: github.com/kennethgerrits");
                }
                MessageBox.Show($"Exported file succesfully to: {fileName} ", "Here you go!", MessageBoxButton.OK);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }
    }
}
