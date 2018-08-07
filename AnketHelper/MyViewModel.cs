using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Windows.Media;

namespace AnketHelper
{
    class MyViewModel : ViewModelBase
    {
        private string _PathToSave;

        public string PathToSave
        {
            set
            {
                _PathToSave = value;
                OnPropertyChanged("PathToSave");


                try
                {
                    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var settings = configFile.AppSettings.Settings;
                    if (settings["TestPath"] == null)
                    {
                        settings.Add("TestPath", _PathToSave);
                    }
                    else
                    {
                        settings["TestPath"].Value = _PathToSave;
                    }
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                }
                catch (ConfigurationErrorsException)
                {
                    Console.WriteLine("Error writing app settings");
                }

            }
            get
            {
                return _PathToSave;
            }
        }

        private Process _fileProcess;
        private string _Name = "";
        public string Name
        {
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
                updateNewFileName();
            }
            get
            {
                return _Name;
            }
        }
        private string _Inn = "";
        public string Inn
        {
            set
            {
                _Inn = value;
                OnPropertyChanged("Inn");
                updateNewFileName();
                innIsCorrect= CheckerINN.check_INN(_Inn);
            }
            get
            {
                return _Inn;
            }
        }
        private DateTime _ADate = DateTime.Now;
        private bool _innIsCorrect;
        private bool innIsCorrect
        {
            set
            {
                _innIsCorrect = value;
                OnPropertyChanged("innIsCorrect");
                OnPropertyChanged("innBackground");
               // MessageBox.Show(_innIsCorrect.ToString());
            }
            get
            {
                return _innIsCorrect;
            }
        }
        public Brush InnBackground //плохо но ладно
        {
            get
            {
                return innIsCorrect ? Brushes.Black : Brushes.Coral;
            }
        }
        public DateTime ADate
        {
            set
            {
                _ADate = value;
                OnPropertyChanged("ADate");
                updateNewFileName();
            }
            get
            {
                return _ADate;
            }
        }
        private bool _IsKlient;
        public bool IsKlient
        {
            set
            {
                _IsKlient = value;
                OnPropertyChanged("IsKlient");
                // MessageBox.Show(_IsKlient.ToString());
                updateNewFileName();
            }
            get
            {
                return _IsKlient;
            }
        }
        private TrulyObservableCollection<BoolNotifyPropertyChanged> _ex;
        public TrulyObservableCollection<BoolNotifyPropertyChanged> ex
        {
            set
            {
                _ex = value;
                OnPropertyChanged("ex");
            }
            get
            {
                return _ex;
            }
        }
        private TrulyObservableCollection<BoolNotifyPropertyChanged> _kl;
        public TrulyObservableCollection<BoolNotifyPropertyChanged> kl
        {
            set
            {
                _kl = value;
                OnPropertyChanged("kl");
            }
            get
            {
                return _kl;
            }
        }
        private TrulyObservableCollection<BoolNotifyPropertyChanged> _kr;
        public TrulyObservableCollection<BoolNotifyPropertyChanged> kr
        {
            set
            {
                _kr = value;
                OnPropertyChanged("kr");
            }
            get
            {
                return _kr;
            }
        }
        private bool _IsKredit;
        public bool IsKredit
        {
            set
            {
                _IsKredit = value;
                OnPropertyChanged("IsKredit");
                updateNewFileName();
            }
            get
            {
                return _IsKredit;
            }
        }
        private string _NewFileName;
        public string NewFileName
        {
            set
            {
                _NewFileName = value;
                OnPropertyChanged("NewFileName");
            }
            get
            {
                return _NewFileName;
            }
        }

        private string _FileName;
        public string FileName
        {
            set
            {
                _FileName = value;
                updateNewFileName();
                OnPropertyChanged("FileName");
                try
                {
                    if (_fileProcess.IsRunning()) //extention method
                    {
                        MessageBox.Show("looks  run");
                        _fileProcess.CloseMainWindow();
                        _fileProcess.Close();
                    }
                    if (_FileName!="")
                    {
                        _fileProcess.StartInfo.FileName = _FileName;
                        _fileProcess.StartInfo.CreateNoWindow = true;
                        _fileProcess.Start();
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            get
            {
                return _FileName;
                
            }
        }

        public MyViewModel()
        {
            Init();
        }
        private void Init()
        {
            var appsettings = ConfigurationManager.AppSettings;
            _PathToSave = appsettings["TestPath"] ?? @"c:\";
            _fileProcess = new Process();
            FileName = "";
            NewFileName = "";
            Name = "";
            Inn = "";
            ADate = DateTime.Now;
            updateNewFileName();
            IsKlient = false;
            IsKredit = false;

            // инициализируем три 


            ex = new TrulyObservableCollection<BoolNotifyPropertyChanged>();
            ex.CollectionChanged += MyItemsSource_CollectionChanged;
            for (int i = 0; i < 16; i++)
            {
                ex.Add(new BoolNotifyPropertyChanged() { val = false });
            }
            kl = new TrulyObservableCollection<BoolNotifyPropertyChanged>();
            kl.CollectionChanged += MyItemsSource_CollectionChanged;
            for (int i = 0; i < 12; i++)
            {
                kl.Add(new BoolNotifyPropertyChanged() { val = false });
            }
            kr = new TrulyObservableCollection<BoolNotifyPropertyChanged>();
            kr.CollectionChanged += MyItemsSource_CollectionChanged;
            for (int i = 0; i < 12; i++)
            {
                kr.Add(new BoolNotifyPropertyChanged() { val = false });
            }
        }
        private void Reset()
        {
            Init();
        }
       
        public void save()
        {
            if(!(IsKlient | IsKredit)) { MessageBox.Show("Нужно выбрать тип анкеты"); return; }
            if (FileName != "")
            {
                if (_fileProcess.IsRunning())
                {
                    _fileProcess.CloseMainWindow();
                    _fileProcess.Kill();
                    _fileProcess.WaitForExit(2000);
                    if (!_fileProcess.HasExited) { MessageBox.Show(" нужно закрыть файл "); }
                }
                    if (File.Exists(PathToSave + NewFileName))
                    { NewFileName =(Path.GetFileNameWithoutExtension(NewFileName)+"_"+ DateTime.Now.ToString()+Path.GetExtension(NewFileName)).Replace(":",""); }
                try
                {
                    MessageBox.Show("переименоываваем в " + PathToSave + NewFileName, "Move to");
                    File.Move(FileName, PathToSave + NewFileName);
                    Reset();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                
                
            }
            else { MessageBox.Show("нет файла! ничего не сохранилось"); }
            //Reset();
            return;
        } 
        public void cancel()
        {
            if (_fileProcess.IsRunning())
            {
                _fileProcess.CloseMainWindow();
                _fileProcess.Kill();
               // _fileProcess.Close();
            }
            MessageBox.Show("отмена", "clear");
            Reset();
        }


        void MyItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            updateNewFileName();
        }

        private void updateNewFileName()
        {
            string NFName = _ADate.ToString("yyyyMMdd") + "_" + _Inn + "_" + _Name;
            
            if (_IsKlient)
            {
                NFName = NFName + "_ex" + boolArrToLiteral(_ex) + "_kl" +boolArrToLiteral(_kl);
            }
            if (_IsKredit)
            {
                NFName = NFName + "_kr" + boolArrToLiteral(_kr);
            }
            NFName += System.IO.Path.GetExtension(FileName);
            NewFileName = NFName;
        }
        private string boolArrToLiteral(TrulyObservableCollection<BoolNotifyPropertyChanged> x)
        {
            string str = "{";
            char[] charsToTrim = { ',', ' ' };
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i].val)
                {
                    str += (i + 1).ToString() + ',';
                }
            }
            str = str.Trim(charsToTrim);
            str += "}";
            return str;
        }

        public ICommand SaveBtnClickCommand
        {
            get
            {
                return new BtnClickCommand();
            }
        }
        public ICommand ClearBtnClickCommand
        {
            get
            {
                return new CnlBtnClickCommand();
            }
        }

        
    }


}
