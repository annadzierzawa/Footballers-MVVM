using Piłkarze.ViewModel.BaseClass;
using Piłkarze.ViewModel.RelayCommandNamespace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Piłkarze.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        public Model.Model model { get; set; }

        private ObservableCollection<Footballer> footballers = new ObservableCollection<Footballer>();

        public ObservableCollection<Footballer> Footballers {
            get { return footballers; }

            set {
                onPropertyChanged(nameof(Footballers));
                footballers = new ObservableCollection<Footballer>(model.footballers);
                onPropertyChanged(nameof(Footballers));
            }
        }
        public Footballer selectedFootballer { get; set; }
        public int id { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }

        public uint weight { get; set; }

        public uint age { get; set; }
        public ViewModel()
        {
            model = new Model.Model();
            footballers = new ObservableCollection<Footballer>(model.footballers);
            onPropertyChanged(nameof(footballers));
        }

        private void clearForm()
        {
            firstname = null;
            surname = null;
            age = 0;
            weight = 0;
        }

        private ICommand _addCommand = null;


        public ICommand addCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(arg => { 
                        model.addFootballer(firstname, surname, age, weight);
                        footballers = new ObservableCollection<Footballer>(model.footballers);
                        onPropertyChanged(nameof(footballers));
                        clearForm();
                        selectedFootballer = null;
                        onPropertyChanged(nameof(firstname));
                        onPropertyChanged(nameof(surname));
                        onPropertyChanged(nameof(age));
                        onPropertyChanged(nameof(weight));
                        //onPropertyChanged(nameof(footballers));
                    },
                                                    arg => firstname != null && surname != null);
                }
                return _addCommand;
            }
        }

        private ICommand _editCommand = null;
        public ICommand editCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(arg => {
                        model.updateFootballer(selectedFootballer.Id, firstname, surname, age, weight);
                        footballers = new ObservableCollection<Footballer>(model.footballers);
                        onPropertyChanged(nameof(footballers));
                        clearForm();
                        selectedFootballer = null;
                        },
                                                    arg => selectedFootballer != null);
                }
                return _editCommand;
            }
        }

        private ICommand _deleteCommand = null;

        public ICommand deleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(arg => { 
                        model.deleteFootballer(selectedFootballer.Id);
                        footballers = new ObservableCollection<Footballer>(model.footballers);
                        onPropertyChanged(nameof(footballers));
                        clearForm();
                        selectedFootballer=null;
                        },
                                                    arg => selectedFootballer != null);
                }
                return _deleteCommand;
            }
        }

        private ICommand _select = null;
        public ICommand Select
        {
            get
            {
                if (_select == null)
                {
                    _select = new RelayCommand(
                        arg =>
                        {
                            if(selectedFootballer!=null){
                                firstname = selectedFootballer.FirstName;
                                surname = selectedFootballer.Surname;
                                weight = selectedFootballer.Weight;
                                age = selectedFootballer.Age;
                                onPropertyChanged(nameof(firstname));
                                onPropertyChanged(nameof(surname));
                                onPropertyChanged(nameof(age));
                                onPropertyChanged(nameof(weight));
                            }
                            
                        },

                        arg => true

                        );
                }
                return _select;
            }

            //private ICommand _clear = null;
            //public ICommand Clear
            //{
            //    get
            //    {
            //        if (_clear == null)
            //        {
            //            _clear = new RelayCommand(
            //                arg => {
            //    firstname = null;
            //    surname = null;
            //    weight = 0;
            //    age = 0;
            //},

            //                arg => true

            //                );
            //        }
            //        //metod Execute ustawia wynik na pusty (Result=null)
            //        //metoda CanExecute pozwala na wykonnie polecenia zawsze (zawsze zwraca wartośc true)
            //        return _clear;
            //    }
            //}

        }
    }
}
