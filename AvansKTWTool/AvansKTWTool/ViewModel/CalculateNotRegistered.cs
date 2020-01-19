using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

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
        private List<string> _leftoverKanidatesList;

        //constructor
        public CalculateNotRegistered()
        {
            //Init
            _leftoverKanidatesList = new List<string>();

            //RelayCommands
            CalculateNotRegisteredCommand = new RelayCommand(CalculateNotRegisteredKanidates);
        }

        private void CalculateNotRegisteredKanidates()
        {
            if (AllKanidatesInformation.Equals("") || ChosenKanidatesInformation.Equals("")) return;

            List<string> allKanidatesList = AllKanidatesInformation.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            List<string> chosenKanidatesList = ChosenKanidatesInformation.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            _leftoverKanidatesList = allKanidatesList.Except(chosenKanidatesList).ToList();

            string filePath = @"D:\Kanidates.txt";

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("The following kanidates are not scheduled:");
                    sw.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");

                    foreach (var name in _leftoverKanidatesList)
                        sw.WriteLine(name);
                    
                    sw.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                    sw.WriteLine("Export date: {0}", DateTime.Now.ToString(CultureInfo.CurrentCulture));
                    sw.WriteLine("Application made by {0}", "Kenneth Gerrits: github.com/kennethgerrits");
                }
                MessageBox.Show($"Exported file succesfully to: {filePath} ", "Here you go!", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
