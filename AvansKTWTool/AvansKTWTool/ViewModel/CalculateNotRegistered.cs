using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace AvansKTWTool.ViewModel
{
    public class CalculateNotRegistered
    {
        //properties
        public string AllKanidatesInformation { get; set; }
        public string ChosenKanidatesInformation { get; set; }

        //commands
        public ICommand CalculateNotRegisteredCommand { get; set; }

        //private fields
        private List<string> _allKanidatesList;
        private List<string> _chosenKanidatesList;
        private List<string> _leftoverKanidatesList;
        
        //constructor
        public CalculateNotRegistered()
        {
            //RelayCommands
            CalculateNotRegisteredCommand = new RelayCommand(CalculateNotRegisteredKanidates);

            //Init
            _allKanidatesList = new List<string>();
            _chosenKanidatesList = new List<string>();
            _leftoverKanidatesList = new List<string>();
        }

        private void CalculateNotRegisteredKanidates()
        {

            List<string> _allKanidatesList = AllKanidatesInformation.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();   
            
            List<string> _chosenKanidatesList = ChosenKanidatesInformation.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            _leftoverKanidatesList = _allKanidatesList.Except(_chosenKanidatesList).ToList();
        }
    }
}
