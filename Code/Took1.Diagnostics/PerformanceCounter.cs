///////////////////////////////////////////////////////////
//  PerformanceCounter.cs
//  Implementation of the Class PerformanceCounter
//  Generated by Enterprise Architect
//  Created on:      21-out-2008 14:56:38
//  Original author: Andr� Matos
///////////////////////////////////////////////////////////



using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Diagnostics {
    /// <summary>
    /// Classe para exibir dados da performance de processos (uso de mem�ria,
    /// processador, ...)
    /// </summary>
    public class PerformanceCounter //: DependencyObject 
    {
        //const string MemoryCategory = "Memory";
        private const string ProcessCategory = "Process";
        //const string WorkingSetCounter = "Available MBytes";
        private const string WorkingSetCounter = "Working Set - Private";
        private const string UserTimeName = "% User Time";

        #region PRIVATE MEMBERS
        //delegate void RefreshCallback();

        private bool isActive = false;
        private int memory;
        private int memoryPeak;
        private float processorUse;
        private float processorUsePeak;
        private string processName = "";
        //private Process[] process;

        System.Diagnostics.PerformanceCounter memoryPerformanceCounter;
        System.Diagnostics.PerformanceCounter processorPerformanceCounter;

        #endregion
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Indica se processo est� ativo
        /// </summary>
        public bool IsActive { get; set; }
        //public bool IsActive
        //{
        //    get { return (bool)GetValue(IsActiveProperty); }
        //    set { SetValue(IsActiveProperty, value); }
        //}
        //// Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsActiveProperty =
        //    DependencyProperty.Register("IsActive", typeof(bool), typeof(PerformanceCounter), new UIPropertyMetadata(false));

        /// <summary>
        /// Mostra mem�ria atual utilizada pelo processo, ja formatada, em K bytes
        /// </summary>
        public string Memory { get; set; }
        //public string Memory
        //{
        //    get { return (string)GetValue(MemoryProperty); }
        //    set { SetValue(MemoryProperty, value); }
        //}
        //// Using a DependencyProperty as the backing store for Memory.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MemoryProperty =
        //    DependencyProperty.Register("Memory", typeof(string), typeof(PerformanceCounter), new UIPropertyMetadata("0"));


        /// <summary>
        /// Nome do processo
        /// </summary>
        public string Name { get; set; }
        //public string Name
        //{
        //    get { return (string)GetValue(NameProperty); }
        //    set { SetValue(NameProperty, value); }
        //}
        //// Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty NameProperty =
        //    DependencyProperty.Register("Name", typeof(string), typeof(PerformanceCounter), new UIPropertyMetadata(""));

        /// <summary>
        /// Inidica valor m�ximo de mem�ria registrado para o processo, ja formatado, em K
        /// bytes
        /// </summary>
        public string MemoryPeak { get; set; }
        //public string MemoryPeak
        //{
        //    get { return (string)GetValue(MemoryPeakProperty); }
        //    set { SetValue(MemoryPeakProperty, value); }
        //}
        //// Using a DependencyProperty as the backing store for MemoryPeak.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MemoryPeakProperty =
        //    DependencyProperty.Register("MemoryPeak", typeof(string), typeof(PerformanceCounter), new UIPropertyMetadata("--"));

        /// <summary>
        /// Mostra o uso atual do processador, formatado em percentual. Para processadores
        /// multicore, o valor fornecido � equivalente ao uso do core em que o processo
        /// est� sendo executado (ex. 100% equivale a 50% de um processador dualcore)
        /// </summary>
        public string ProcessorUse { get; set; }
        //public string ProcessorUse
        //{
        //    get { return (string)GetValue(ProcessorUseProperty); }
        //    set { SetValue(ProcessorUseProperty, value); }
        //}
        //// Using a DependencyProperty as the backing store for ProcessorUse.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ProcessorUseProperty =
        //    DependencyProperty.Register("ProcessorUse", typeof(string), typeof(PerformanceCounter), new UIPropertyMetadata("--"));

        /// <summary>
        /// Mostra o uso m�ximo do processador registrado pela classe, formatado em
        /// percentual. Para processadores multicore, o valor fornecido � equivalente ao
        /// uso do core em que o processo est� sendo executado (ex. 100% equivale a 50% de
        /// um processador dualcore)
        /// </summary>
        public string ProcessorUsePeak { get; set; }
        //public string ProcessorUsePeak
        //{
        //    get { return (string)GetValue(ProcessorUsePeakProperty); }
        //    set { SetValue(ProcessorUsePeakProperty, value); }
        //}
        //// Using a DependencyProperty as the backing store for ProcessorUse.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ProcessorUsePeakProperty =
        //    DependencyProperty.Register("ProcessorUsePeak", typeof(string), typeof(PerformanceCounter), new UIPropertyMetadata("--"));

        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="processName">Nome do processo a ser monitorado (sem extens�o ".
        /// exe")</param>
        public PerformanceCounter(string processName)
        {
            this.processName = processName;
            Name = processName;
            try
            {
                memoryPerformanceCounter = new System.Diagnostics.PerformanceCounter(ProcessCategory, WorkingSetCounter, this.processName);
            }
            catch { }
            try
            {
                processorPerformanceCounter = new System.Diagnostics.PerformanceCounter(ProcessCategory, UserTimeName, this.processName);
            }
            catch { }
            Refresh();

        }

        /// <summary>
        /// Atualiza valores das propriedades para refletir estado atual dos contadores.
        /// </summary>
        public void Refresh()
        {
            //if (this.Dispatcher.CheckAccess())
            //{
            try
            {
                memory = 0;
                if (memoryPerformanceCounter != null)
                    memory = (int)memoryPerformanceCounter.NextValue() / 1024;

                processorUse = 0;
                if (processorPerformanceCounter != null)
                    processorUse = (float)processorPerformanceCounter.NextValue();

                if (memory > memoryPeak) memoryPeak = memory;
                if (processorUse > processorUsePeak) processorUsePeak = processorUse;

                Memory = memory.ToString() + "kb";
                MemoryPeak = memoryPeak.ToString() + "kb";
                ProcessorUse = processorUse.ToString("F2") + "%";
                ProcessorUsePeak = processorUsePeak.ToString("F2") + "%";
                IsActive = true;
            }
            catch
            {
                IsActive = false;
                Memory = "--";
                MemoryPeak = "--";
                ProcessorUse = "--";
                ProcessorUsePeak = "--";
            }
            //}
            //else
            //{
            //    RefreshCallback refreshCallback = new RefreshCallback(Refresh);
            //    this.Dispatcher.Invoke(refreshCallback, null);
            //}

        }

    }//end PerformanceCounter

}//end namespace Diagnostics