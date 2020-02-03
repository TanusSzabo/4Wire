using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO.Ports;  //Necessário para ter acesso as portas


namespace Base
{
    public partial class Form1 : Form
    {
        int chanV;       //Channel do voltímetro
        int chanT;       //Channel do termopar
        string thermT;   //Tipo do termopar
        int numbT;       //Número de testes (Se não ilimitado)
        string currV;    //Valor da corrente
        int time;        //Tempo do decorrer do experimento
        string path;     //Caminho de salvamento do arquivo
        DateTime beginExp; //Hora de início do experimento

        string[] infoTextbox; //Para separar cada linha da expRun em 3 valores
        string[] stringSeparators = new string[] { " ", "\r\n", ":", ","}; //Separadores utilizados
        string[] tempValue;   //Para guardar os valores da temperatura
        string[] voltValue;   //Para guardar os valores da tensão
        string[] timeValue;   //Para guardar os valores do tempo
        string[] restValue;   //Para guardar os valores da resistência
        string firstRest;     //Para guardar o valor da primeira resistência para fazer o Resistance/R0
        string rxString;      //Para receber as respostas do equipamento

        int testCount = -1;   //Valor do teste no experimento todo, começa em -1 para pegar o valor da corrente
        int cpCount = 0;      //Valor do teste no presente checkpoint, vai de 0 a 500
        int spaceCount = 0;   //Para diferenciar se está na parte de pegar a tensão ou a temperatura
        int bugCount = 0;     //Conta as tentativas de reconexão com o equipamento falho
        int lastStop = 0;     //Guarda em qual valor foi o ultimo salvamento para não salvar duas vezes o mesmo valor
        int lineRemov = 0;    //Conta quantas linhas foram removidas naquele salvamento
        int tlineRemov = 0;   //Conta quantas linhas foram removidas no total
        
        int testFlag = 0;       //Para avisar qual equipamento queremos conectar no teste de sinal e marca a quantidade de tentativas de conexão
        int stopFlag = 0;       //Para avisar que foi apertado o botão Stop quando acabar o presente teste
        int errorFlag = 0;      //Para avisar que alguma linha apresentou erro no salvamento
        int totalErrorFlag = 0; //Para avisar que alguma linha apresentou erro no experimento como um todo
        int saveFlag = 0;       //Para avisar que ja salvou uma vez o arquivo e impedir que escreva o cabeçalho duas vezes
        int cpFlag = 0;         //Para impedir MessageBox na hora de salvar (Quando chegar ao checkpoint)
        int bugFlag = 0;        //Conta quanto tempo está sem resposta do equipamento

        Form2 f2;                //

        public Form1()
        {
            InitializeComponent();
            Begining_Config();
        }

//
//
// Parte 1: Conexão com os equipamentos
//
//
        private void timerCOM_Tick_1(object sender, EventArgs e)
        {
            RefreshListCOMV();
            RefreshListCOMS();
        }

        private void RefreshListCOMV()
        {
            int i = 0;
            bool difQuant = false;    //Flag para sinalizar que a quantidade de portas mudou

            if (comVolt.Items.Count == SerialPort.GetPortNames().Length)    //Se a quantidade de portas mudou
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (comVolt.Items[i++].Equals(s) == false)
                    {
                        difQuant = true;
                    }
                }
            }

            else
            {
                difQuant = true;
            }

            if (difQuant == false)      //Se não foi detectado diferença
            {
                return;
            }

            comVolt.Items.Clear();

            foreach (string s in SerialPort.GetPortNames())     //adiciona todas as COM diponíveis na lista
            {
                comVolt.Items.Add(s);
            }

            if (serialPortV.IsOpen == false)
            {
                if (comVolt.Items.Count > 0)
                    comVolt.SelectedIndex = 0;
                else
                    comVolt.Text = "";
            }
        }
       
        private void RefreshListCOMS()
        {
            int i = 0;
            bool difQuant = false;    //Flag para sinalizar que a quantidade de portas mudou

            if (comSour.Items.Count == SerialPort.GetPortNames().Length)      //Se a quantidade de portas mudou
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (comSour.Items[i++].Equals(s) == false)
                    {
                        difQuant = true;
                    }
                }
            }
            else
            {
                difQuant = true;
            }

            if (difQuant == false)      //Se não foi detectado diferença
            {
                return;
            }

            comSour.Items.Clear();

            foreach (string s in SerialPort.GetPortNames())  //Adiciona todas as COM diponíveis na lista
            {
                comSour.Items.Add(s);
            }

            if (serialPortS.IsOpen == false)
            {
                if (comSour.Items.Count > 0)
                    comSour.SelectedIndex = comSour.Items.Count - 1;
                else
                    comSour.Text = "";
            }
        }

        private void conectVolt_Click(object sender, EventArgs e)
        {
            if (serialPortV.IsOpen == false)
            {
                try
                {
                    serialPortV.PortName = comVolt.Items[comVolt.SelectedIndex].ToString();
                    serialPortV.Close();
                    serialPortV.Open();
                }
                catch
                {
                    return;
                }
                if (serialPortV.IsOpen)
                {
                    conectVolt.Text = "Disconnect";
                    conectVolt.Enabled = false;   //Desabilitar o botão conectVolt enquanto tenta a conexão
                    conectSour.Enabled = false;
                    comVolt.Enabled = false;
                    if (serialPortS.IsOpen)       //Apenas habilitar o botão start quando ambos aparelhos então conectados
                    {
                        startBt.Enabled = true;
                    }

                    testSignalVolt();
                }
            }
            else
            {
                try
                {
                    if (serialPortV.IsOpen == true)
                        serialPortV.Close();
                    comVolt.Enabled = true;
                    conectVolt.Text = "Conect";
                    startBt.Enabled = false;       //Desabilitar o botão start sempre que uma porta estiver aberta
                }
                catch
                {
                    return;
                }
            }
        }

        private void testSignalVolt()
        {
            this.expRun.Clear();
            testFlag = 7;   //Valor impar para tal poder fazer 3 testes (4 x 2 - 1 - 2)
            expRun.TextChanged += new EventHandler(expRun_TextChanged);
            timerTest.Enabled = true;

            if (serialPortV.IsOpen == true)
            {
                Keithley_Signal("IDNVolt");
            }  
        }

        private void DataReceivedHandlerV(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = serialPortV.ReadExisting();              //Le o dado disponível na serial
            this.Invoke(new EventHandler(trataDadoRecebidoV));   //Chama outra thread para escrever o dado no text box
        }

        private void trataDadoRecebidoV(object sender, EventArgs e)
        {
            this.expRun.Text += rxString;
        }

        private void conectSour_Click(object sender, EventArgs e)
        {
            if (serialPortS.IsOpen == false)
            {
                try
                {
                    serialPortS.PortName = comSour.Items[comSour.SelectedIndex].ToString();
                    serialPortS.Close();
                    serialPortS.Open();
                }
                catch
                {
                    return;
                }
                if (serialPortS.IsOpen)
                {
                    conectSour.Text = "Disconnect";
                    conectSour.Enabled = false;     //Desabilitar o botão conectSour enquanto tenta a conexão
                    conectVolt.Enabled = false;
                    comSour.Enabled = false;
                    if (serialPortV.IsOpen)       //Para apenas habilitar o botão start quando ambos aparelhos então conectados
                    {
                        startBt.Enabled = true;
                    }

                    testSignalSour();
                }
            }
            else
            {
                try
                {
                    if (serialPortS.IsOpen == true)
                        serialPortS.Close();
                    comSour.Enabled = true;
                    conectSour.Text = "Conect";
                    startBt.Enabled = false;       //Desabilitar o botão start sempre que uma porta estiver aberta
                }
                catch
                {
                    return;
                }

            }
        }

        private void testSignalSour()
        {
            this.expRun.Clear();
            testFlag = 8;   //Valor par para tal poder fazer 3 testes (4 x 2 - 2)
            expRun.TextChanged += new EventHandler(expRun_TextChanged);
            timerTest.Enabled = true;

            if (serialPortS.IsOpen == true)
            {
                Keithley_Signal("IDNSour");
            }
        }

        private void DataReceivedHandlerS(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = serialPortS.ReadExisting();              //Le o dado disponível na serial
            this.Invoke(new EventHandler(trataDadoRecebidoS));   //Chama outra thread para escrever o dado no text box
        }

        private void trataDadoRecebidoS(object sender, EventArgs e)
        {
            this.expRun.Text += rxString;
        }

        private void timerTest_Tick(object sender, EventArgs e) //Se em 1 sec não obter resposta de conexão
        {
            if (testFlag <= 2)
            {
                timerTest.Enabled = false;
                expRun.TextChanged -= new EventHandler(expRun_TextChanged);

                if (testFlag % 2 == 1)
                {
                    conectVolt.Enabled = true;
                    conectSour.Enabled = true; 
                    conectVolt.PerformClick();
                }

                if (testFlag % 2 == 0)
                {
                    conectVolt.Enabled = true;
                    conectSour.Enabled = true; 
                    conectSour.PerformClick();
                }

                testFlag = 0;
                MessageBox.Show("The equipment is not responding. \r\n" +
                                "Please check if the equipment is on, the COM port connections or if the correct COM port has been selected, before attempting to connect.",
                                "Equipment error");

                return;
            }

            if (testFlag % 2 == 1)
            {
                try
                {
                    serialPortV.Close();
                    serialPortV.Open();
                }
                catch
                { }

                expRun.TextChanged += new EventHandler(expRun_TextChanged);
                if (serialPortV.IsOpen == true)
                {
                    Keithley_Signal("IDNVolt");
                } 
            }

            if (testFlag % 2 == 0)
            {
                try
                {
                    serialPortS.Close();
                    serialPortS.Open();
                }
                catch
                { }

                expRun.TextChanged += new EventHandler(expRun_TextChanged);
                if (serialPortS.IsOpen == true)
                {
                    Keithley_Signal("IDNSour");
                }
            }

            testFlag -= 2; //Para o nanovoltímetro testará 3 vezes (tesFlag = 7, 5, 3), para o gerador igualmente (testFlag = 8, 6, 4)
        }
//
//
// Parte 2: Decorrer do experimento e comunicação com os equipamentos
//
//
        private void button5_Click(object sender, EventArgs e)   //Botão Start/Stop
        {
            if (startBt.Text == "Start")
            {
                Config("Close");
                startBt.Text = "Stop";
                bugFlag = 0;
                bugCount = 0; 

                if (testCount == -1)
                {
                    //
                    //  Guardar todas as configurações
                    //
                    chanV = (chanVolt.SelectedIndex + 1);
                    thermT = thermType.SelectedItem.ToString();
                    if (testCheck.Checked == false)
                        numbT = Int32.Parse(numbTest.Text);
                    currV = currValue.Text + "e-" + (3 * (currCombo.SelectedIndex + 1));
                    if (chanV == 1)
                        chanT = 2;
                    else
                        chanT = 1;

                    beginExp = DateTime.Now;

                    if (String.IsNullOrWhiteSpace(saveLocal.Text))
                        saveLocal.Text = "C:\\Users\\" + Environment.UserName + "\\Desktop";
                    if ((String.IsNullOrWhiteSpace(saveName.Text)) && (timeCheck.Checked == false))
                        saveName.Text = "4 Wire Res per Temp";

                    if (timeCheck.Checked)
                        path = "4 Wire\\" + saveName.Text + " - " + beginExp.ToString(" yyyy-MM-dd HH.mm");
                    else
                        path = "4 Wire\\" + saveName.Text;

                    expRun.Clear();
                    timerCOM.Enabled = false;

                    expRun.TextChanged += new EventHandler(expRun_TextChanged);
                    Keithley_Signal("CurrOn");
                    Keithley_Signal("RST");
                    timerSpace.Enabled = true;
                }

                else
                {
                    timerTextBox.Enabled = true;
                }
            }

            else if (startBt.Text == "Stop")
            {
                stopFlag = 1;    //Parada ocorrerá quando pegar o valor da temperatura para completar o teste
                startBt.Text = "Start";
                startBt.Enabled = false;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)     //Botão Reset
        {
            DialogResult dialogResult = MessageBox.Show("Did you want reset the experiment?\r\n" +
                                                        "All values will be lost.\r\n",
                                                        "Reset?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string sName = saveName.Text;
                string sLocal = saveLocal.Text;
                expRun.Clear();
                Begining_Config();
                Keithley_Signal("CurrOff");
                saveName.Text = sName;
                saveLocal.Text = sLocal;

                if ((serialPortS.IsOpen == true) && (serialPortV.IsOpen == true))
                    startBt.Enabled = true;

                if (Application.OpenForms.Count == 2)
                {
                    f2.Close();  // fecha o Form2
                }
            }

            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void timerSpace_Tick(object sender, EventArgs e)
        {
            Timer();

            if (startBt.Text != "Start")
            {
                //
                // Se não houver resposta por 10 segundos (100 100mili), ele tentará enviar o sinal de novo. Após 10 tentativas, parará o teste.  
                //
                if (bugCount >= 10)
                {
                    stopFlag = 0;
                    cpFlag = 1;
                    CreateFile();
                    cpFlag = 0;
                    bugFlag = 0;
                    bugCount = 0;

                    startBt.Enabled = true;
                    startBt.Text = "Start";
                    resetBt.Enabled = true;

                    expRun.SelectionStart = expRun.TextLength;
                    expRun.ScrollToCaret();

                    MessageBox.Show("The equipment has stopped responding. \r\n" + 
                                    "Please check if the equipment is on or the COM port connections before restarting.", "Equipment error");
                    return;
                }

                bugFlag++;
                if (bugFlag >= 100)
                {
                    BugBreak();
                    bugCount++;
                }
            }
        }

        private void Timer()
        {
            int day = DateTime.Now.Day - beginExp.Day;
            int hour = DateTime.Now.Hour - beginExp.Hour + (24 * day);
            int minute = DateTime.Now.Minute - beginExp.Minute + (60 * hour);
            int second = DateTime.Now.Second - beginExp.Second + (60 * minute);
            int millisec = DateTime.Now.Millisecond - beginExp.Millisecond + (1000 * second);
            time = millisec;

            timeDisplay.Text = "Time: " + String.Format("{00:00}", Math.Truncate(second / 3600.0)) +                 //millisec->hora
                               ":" + String.Format("{00:00}", Math.Truncate((time % 3600000.0) / 60000.0)) +         //millisec->min
                               ":" + String.Format("{00:00}", Math.Truncate(((time % 3600000.0) % 60000.0)) / 1000) + //millisec->sec
                               ":" + String.Format("{00:00}", Math.Truncate(((time % 3600000.0) % 60000.0)) % 1000); //millisec
        }

        private void Keithley_Signal(string pigas)
        {
            try
            {
                if (serialPortV.IsOpen == true)
                {
                    if (pigas == "RST")
                    {
                        serialPortV.WriteLine("*RST\r\n"); //Reseta as configurações do 2182A, Necessário ao iniciar
                    }

                    if (pigas == "Volt")
                    {
                        serialPortV.WriteLine(":SENS:FUNC 'VOLT'\r\n" +
                                              ":SENS:CHAN " + (chanV) + "\r\n" +
                                              ":READ?\r\n");

                        serialPortV.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandlerV);
                    }

                    if (pigas == "Temp")
                    {
                        serialPortV.WriteLine(":SENS:FUNC 'TEMP'\r\n" +
                                              ":SENS:CHAN " + (chanT) + "\r\n" +
                                              ":SENS:TEMP:TRAN TC\r\n" +              //Sensor thermocouple ou internal?
                                              ":SENS:TEMP:RJUN:RSEL INT\r\n" +        //Escolhendo a simulação de termopar
                                              ":SENS:TEMP:TC " + thermT + "\r\n" +    //Será usado termopar de junta?
                                              ":UNIT:TEMP C\r\n" +
                                              ":READ?\r\n");

                        serialPortV.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandlerV);
                    }

                    if (pigas == "IDNVolt")
                    {
                        serialPortV.WriteLine("*IDN?\r\n");
                        serialPortV.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandlerV);
                    }
                }

                if (serialPortS.IsOpen == true)
                {
                    if (pigas == "CurrOn")
                    {
                        serialPortS.WriteLine("*RST\r\n" +                           //Gera corrente
                                              ":SOUR:FUNC CURR\r\n" +
                                              ":SOUR:CURR:MODE FIX\r\n" +
                                              ":SOUR:CURR:LEV " + currV + "\r\n" +   //Valor da corrente a ser aplicada
                                              ":OUTP ON\r\n" +
                                              ":READ?\r\n");

                        serialPortS.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandlerS);
                    }

                    if (pigas == "CurrOff")
                    {
                        serialPortS.WriteLine(":OUTP OFF\r\n");            //Desliga a Corrente
                    }

                    if (pigas == "CurrRead")
                    {
                        serialPortS.WriteLine(":READ?\r\n");
                        serialPortS.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandlerS);
                    }

                    if (pigas == "IDNSour")
                    {
                        serialPortS.WriteLine("*IDN?\r\n");
                        serialPortS.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandlerS);
                    }
                }
            }

            catch
            {
                if (bugCount >= 10)
                {
                    return;
                }
                Keithley_Signal(pigas);
            }
        }

        private void expRun_TextChanged(object sender, EventArgs e)
        {
            expRun.TextChanged -= new EventHandler(expRun_TextChanged);
            timerTextBox.Enabled = true; //Habilita 200 milisec para a interface escrever o retorno do equipamento
        }

        private void timerTextBox_Tick(object sender, EventArgs e)
        {
            timerTextBox.Enabled = false;
            bugFlag = 0;

            if (testFlag != 0)
            {
                timerTest.Enabled = false;

                if (testFlag % 2 == 1)
                {
                    conectVolt.Enabled = true;
                    conectSour.Enabled = true;

                    if (expRun.Text.Contains("2182A"))
                        MessageBox.Show("Model 2182A Nanovoltmeter OK!", "Equipment's signal");
                    else
                    {
                        MessageBox.Show("Wrong equipment connected. \r\n" + 
                                        "Please check if the correct COM port has been selected", "Equipment's signal");
                        conectVolt.PerformClick();
                    }
                }

                if (testFlag % 2 == 0)
                {
                    conectSour.Enabled = true;
                    conectVolt.Enabled = true;

                    if (expRun.Text.Contains("2400"))
                        MessageBox.Show("Model 2400 SourceMeter OK!", "Equipment's signal");
                    else
                    {
                        MessageBox.Show("Wrong equipment connected. \r\n" + 
                                        "Please check if the correct COM port has been selected", "Equipment's signal");
                        conectSour.PerformClick();
                    }
                }

                testFlag = 0;
                return;
            }

            if (testCount == -1)
            {
                currV = expRun.Text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries)[1].Replace(".",","); //O valor da corrente é o segundo valor mandado pelo 2400.
                expRun.Clear();
                expRun.Text += "BEGINING OF THE EXPERIMENT ";

                testCount++;
                spaceCount = 0;

                graphBt.PerformClick();
            }

            if ((testCount < numbT) || (testCheck.Checked))
            {
                if (spaceCount == 0)
                {
                    expRun.Text = expRun.Text.Substring(0, expRun.Text.Length - 1); //Apaga o último caracter pois o 2182A envia o valor com um caracter a mais
                    this.expRun.Text += " ";

                    if ((stopFlag == 1) && (cpCount > 0))
                    {
                        stopFlag = 0;
                        CreateFile();

                        startBt.Enabled = true;
                        resetBt.Enabled = true;

                        expRun.SelectionStart = expRun.TextLength;
                        expRun.ScrollToCaret();

                        return;
                    }

                    if (cpCount == 100) //A cada 100 testes, salva e apaga, para não pesar a interface
                    {
                        startBt.Enabled = false;
                        cpFlag = 1;
                        CreateFile();
                        startBt.Enabled = true;
                        cpFlag = 0;
                        cpCount = 0;
                        lastStop = 0;

                        expRun.Clear();
                        expRun.Text += "CHECKPOINT " + Math.Round(testCount / 100.0);

                    }

                    expRun.Text += "\r\n";

                    if (cpCount > 0)
                    {
                        try
                        {
                            expRun.SelectionStart = expRun.GetFirstCharIndexFromLine(expRun.Lines.Length - 2);
                            expRun.SelectionLength = expRun.GetFirstCharIndexFromLine(expRun.Lines.Length - 1) - expRun.GetFirstCharIndexFromLine(expRun.Lines.Length - 2) - 2;
                            infoTextbox = this.expRun.SelectedText.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            string strV = expRun.SelectedText.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries)[1];
                            string strT = (expRun.SelectedText.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries)[2]);
                            tempDisplay.Text = "Voltage: " + strV + " V\r\n" +
                                               "Temperature: " + strT + " ºC";


                            if (Application.OpenForms.Count == 2)
                            {
                                f2.ReceivingData(strV, strT); //Mandando os valores da tensão e temperatura para o Form2
                            }
                        }
                        catch { }
                    }
                    
                    spaceCount = 1;
                    expRun.Text += time.ToString("000000000").Remove(6) + "." + time.ToString("000000000").Substring(6) + " ";
                    expRun.TextChanged += new EventHandler(expRun_TextChanged);
                    Keithley_Signal("Volt");
                }

                else if (spaceCount == 1)
                {
                    expRun.Text = expRun.Text.Substring(0, expRun.Text.Length - 1); //Apaga o último caracter pois o 2182A envia o valor com um caracter a mais

                    this.expRun.Text += " ";
                    testCount++;
                    cpCount++;
                    bugCount = 0;   //Aqui sabemos que os equipamentos voltaram a responder
                    spaceCount = 0;
                    expRun.TextChanged += new EventHandler(expRun_TextChanged);
                    Keithley_Signal("Temp");
                }
            }

            else if ((testCount >= numbT) && (testCheck.Checked == false))
            {
                expRun.Text = expRun.Text.Substring(0, expRun.Text.Length - 1); //Apaga o último caracter pois o 2182A envia o valor com um caracter a mais
                this.expRun.Text += " ";
                expRun.Text += "\r\nEND OF EXPERIMENT";
                expRun.SelectionStart = expRun.TextLength;
                expRun.ScrollToCaret();

                Keithley_Signal("CurrOff");
                resetBt.Enabled = true;
                startBt.Enabled = false;
                CreateFile();

                string sName = saveName.Text;
                string sLocal = saveLocal.Text;
                Begining_Config();
                saveName.Text = sName;
                saveLocal.Text = sLocal;
                Config("Open");
            }
        }

        private void BugBreak()
        {
            bugFlag = 0;
            expRun.TextChanged -= new EventHandler(expRun_TextChanged);
            
            if (testCount == -1) //Se testCount = -1, o sinal que não chegou foi do 2400
            {
                try
                {
                    serialPortS.Close();
                    serialPortS.Open();  //Abre e fecha a porta para tentar religar a entrada corretamente
                    expRun.TextChanged += new EventHandler(expRun_TextChanged);
                    Keithley_Signal("CurrOn");
                }
                catch { }

                return;
            }
            //
            // Apaga a última linha que provavelmente falta algum valor
            //
            try
            {
                expRun.Text += "\r\n";
                expRun.SelectionStart = expRun.GetFirstCharIndexFromLine(expRun.Lines.Length - 2);
                expRun.SelectionLength = expRun.GetFirstCharIndexFromLine(expRun.Lines.Length - 1) - expRun.GetFirstCharIndexFromLine(expRun.Lines.Length - 2);
                expRun.SelectedText = null;
                expRun.Text = expRun.Text.Substring(0, expRun.Text.Length - 1);

                if (spaceCount == 0)
                {
                    testCount--;
                    cpCount--;
                }
            }
            catch { }

            try
            {
                serialPortV.Close();
                serialPortV.Open();  //Abre e fecha a porta para tentar religar a entrada corretamente
                Keithley_Signal("RST");
            }
            catch { }

            spaceCount = 0;
            timerTextBox.Enabled = true;
        }
//
//
// Parte 3: Salvando dados e criando o arquivo
//
//
        private void CreateFile()
        {
            int i;

            int[] errorlines = new int[cpCount + 2]; //Array com o número das linhas que deram problema
            string errorl = "";  //String para impressão dos números das linhas que deram problema

            this.timeValue = new string[cpCount + 2];
            this.restValue = new string[cpCount + 2];

            startBt.Enabled = false;
            resetBt.Enabled = false;

            Split_Values();

            for(i = lastStop; i < cpCount; i++)
            {
                if ((voltValue[i].Contains("9,9E37")) || (tempValue[i].Contains("9,9E37")) ||        //9,9E37 é a resposta do equipamento para overflow,
                    (voltValue[i].Contains("E") == false) || (tempValue[i].Contains("E") == false))  //...ela é uma leitura mais demorada, podendo acarretar ... 
                //... no não recebimento pelo equipamento do valor completo.
                {
                    errorlines[lineRemov] = (i + 1);
                    lineRemov++;
                    tlineRemov++;
                    errorFlag = 1;
                    if (totalErrorFlag != 2)
                        totalErrorFlag += 2;
                }
                //
                // testa se é possivel calcular a resistência, implicando se há erros nos valores dos testes
                //
                else
                {
                    try
                    {
                        restValue[i] = string.Format("{0:0.########E+00}", Decimal.Parse(voltValue[i], System.Globalization.NumberStyles.Float) / Decimal.Parse(currV, System.Globalization.NumberStyles.Float));
                    }

                    catch
                    {
                        errorlines[lineRemov] = (i + 1);
                        lineRemov++;
                        tlineRemov++;
                        errorFlag = 1;
                        if (totalErrorFlag != 1)
                            totalErrorFlag += 1;
                    }
                }
            }
            

            DialogResult dialogResult = DialogResult.Yes;
            if ((errorFlag != 0) && (cpFlag == 0))  //Não permite a mensagem quando salvar no checkpoint, apagando altomaticamente testes errados
            {
                dialogResult = MessageBox.Show("There is errors in " + lineRemov + " lines.\r\n" +
                                               "Did you want to remove it and create the file?",
                                               "Create File?", MessageBoxButtons.YesNo);
            }
            
            if (dialogResult == DialogResult.Yes)
            {
                if (saveFlag == 0)
                {
                    DirectoryInfo di = Directory.CreateDirectory(saveLocal.Text + "\\" + path);   //Cria uma pasta
                    File.Create(saveLocal.Text + "\\" + path + "\\Data.txt").Dispose();    //Cria arquivo texto
                    File.Create(saveLocal.Text + "\\" + path + "\\Obs.txt").Dispose();     //Cria arquivo texto
                }

                using (TextWriter tw = new StreamWriter(saveLocal.Text + "\\" + path + "\\Obs.txt", false)) //false -> escreve por cima
                {
                    tw.WriteLine("Begining of the experiment: " + beginExp.ToString(" yyyy/MM/dd HH:mm:ss"));
                    tw.WriteLine("End of experiment: " + DateTime.Now.ToString(" yyyy/MM/dd HH:mm:ss"));
                    tw.WriteLine("Duration: " + String.Format("{00:00}", Math.Truncate(time / 3600000.0)) +
                                                ":" + String.Format("{00:00}", Math.Truncate((time % 3600000.0) / 60000.0)) +
                                                ":" + String.Format("{00:00}", Math.Truncate(((time % 3600000.0) % 60000.0)) / 1000) +
                                                ":" + String.Format("{00:00}", Math.Truncate(((time % 3600000.0) % 60000.0)) % 1000));
                    tw.WriteLine("Number of test: " + testCount);
                    tw.WriteLine("Number of errors: " + tlineRemov);

                    if (totalErrorFlag == 1)
                    {
                        tw.WriteLine("The tests presented typing errors");
                    }
                    if (totalErrorFlag == 2)
                    {
                        tw.WriteLine("The tests presented overflow values");
                    }
                    if (totalErrorFlag == 3)
                    {
                        tw.WriteLine("The tests presented typing errors and overflow values");
                    }

                }

                using (TextWriter tw = new StreamWriter(saveLocal.Text + "\\" + path + "\\Data.txt", true)) //true -> adiciona no ja existente
                {
                    if (saveFlag == 0)
                    {
                        tw.WriteLine("Time" + "	Temperature" + "	Resistance/R0" + "	Voltage" + "	Resistance");
                        tw.WriteLine("(Sec)" + "	(C)" + "	" + "	(Volt)" + "	(Ohm)");
                        tw.WriteLine("	" + "	" + "	" + "Current Value = " + currV.Replace(",", ".") + "	");
                        firstRest = restValue[0];
                        saveFlag = 1;
                    }

                    for (i = lastStop; i < cpCount; i++)
                    {
                        if ((Array.IndexOf(errorlines, (i + 1)) > -1) == false)
                        {
                            try
                            {
                                tw.WriteLine(timeValue[i].ToString().Replace(",", ".") + "	" + tempValue[i].Replace(",", ".") + "	" +
                                            string.Format("{0:0.########E+00}", Decimal.Parse(restValue[i], System.Globalization.NumberStyles.Float) / Decimal.Parse(firstRest, System.Globalization.NumberStyles.Float)).ToString().Replace(",", ".") +
                                            "	" + voltValue[i].Replace(",", ".") + "	" + restValue[i].Replace(",", "."));
                            }
                            catch { }
                        }
                    } 
                    
                    for (i = lastStop; i < cpCount; i++)
                    {
                        if (Array.IndexOf(errorlines, (i + 1)) > -1)
                        {
                            try
                            {
                                expRun.Focus();
                                expRun.SelectionStart = expRun.GetFirstCharIndexFromLine(i + 1);
                                expRun.SelectionLength = expRun.GetFirstCharIndexFromLine(i + 2) - expRun.GetFirstCharIndexFromLine(i + 1) - 2;
                                expRun.SelectedText = "-------- null ----------";

                                if (i == (cpCount - 1))
                                    expRun.Text = expRun.Text.Substring(0, expRun.Text.Length - 1);
                            }
                            catch { }
                        }
                    }

                    lastStop = cpCount; //Caso va salvar 2 vezes no mesmo checkpoint, ele não escreverá os mesmos valores 2 vezes
                    errorFlag = 0;
                    lineRemov = 0;
                    restValue = null;

                    tw.Close();
                }
            }

            else if (dialogResult == DialogResult.No)
            {
                for (i = 0; i < cpCount; i++)
                {
                    if (Array.IndexOf(errorlines, (i + 1)) > -1)
                    {
                        try
                        {
                            expRun.Focus();
                            expRun.SelectionStart = expRun.GetFirstCharIndexFromLine(i + 1);
                            expRun.SelectionLength = expRun.GetFirstCharIndexFromLine(i + 2) - expRun.GetFirstCharIndexFromLine(i + 1) - 2;
                            errorl += (i + 1) + " - " + expRun.SelectedText + "\r\n";

                            if (i == (cpCount - 1))
                                expRun.Text = expRun.Text.Substring(0, expRun.Text.Length - 2);
                        }
                        catch { }
                    }
                }
                MessageBox.Show("The lines:\r\n" + errorl + "presented error");

                errorFlag = 0;
                lineRemov = 0;
                restValue = null;

                return;
            }

            
            if (cpFlag == 0)
            {
                if (errorFlag == 1)
                {
                    MessageBox.Show("The file   \"" + path.Substring(7) + "\"\r\n" +
                                    "has been created.\r\n" +
                                    "\r\n" +
                                    "Access it at    " + saveLocal.Text + "\\4 Wire\r\n" +
                                    "\r\n" +
                                    "But " + lineRemov + " lines presented error\r\n" +
                                    "and were removed from the file.");
                }
                else
                {
                    MessageBox.Show("The file   \"" + path.Substring(7) + "\"\r\n" +
                                    "has been created.\r\n" +
                                    "\r\n" +
                                    "Access it at    \"" + saveLocal.Text + "\\4 Wire\"");
                }

                startBt.Enabled = true;
                resetBt.Enabled = true;
            }
        }
        
        private void Split_Values()       //Splita o texto na TextBox expRun
        {
            int i;
            this.voltValue = new string[cpCount + 2];
            this.tempValue = new string[cpCount + 2];
            this.timeValue = new string[cpCount + 2];

            expRun.Text += "\r\n";

            for (i = 0; i < cpCount; i++)
            {
                try
                {
                    //
                    // Divide linha por linha para pegar os valores
                    //
                    expRun.Focus();
                    expRun.SelectionStart = expRun.GetFirstCharIndexFromLine(i + 1);
                    expRun.SelectionLength = expRun.GetFirstCharIndexFromLine(i + 2) - expRun.GetFirstCharIndexFromLine(i + 1) - 2;

                    infoTextbox = this.expRun.SelectedText.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    timeValue[i] = infoTextbox[0].Replace(".", ",");   //Para aplicar funções Parse, o decimal é necessário ser "," ao inves de "."
                    voltValue[i] = infoTextbox[1].Replace(".", ","); //Substring para eliminar o sinal do valor
                    tempValue[i] = infoTextbox[2].Replace(".", ","); //Substring para eliminar o sinal do valor
                }
                catch
                {
                    testCount = testCount - cpCount + i;
                    cpCount = i;
                }
            }

            expRun.Text = expRun.Text.Substring(0, expRun.Text.Length - 1);
            expRun.Text += "\r\n";
        }

        private void button7_Click(object sender, EventArgs e)     //Botão "Search"
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                saveLocal.Text = browser.SelectedPath;
            }
        }
//
//
// Parte 4: Configurações gerais da interface 
//
//
        private void Begining_Config()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; //Impermitir de mudar tamanho do Form
            MaximizeBox = false;   //desabilitar botão de maximizar
            this.Text = "4 Wire"; //Nome no titulo do form
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(10, 10);  //Posição
            this.Icon = Properties.Resources.Blackvariant_Button_Ui_Requests_9_Wire;  //Icone

            expRun.TextChanged -= new EventHandler(expRun_TextChanged); //Não haverá o evento de mudança até iniciar o experimento
            Config("Open");

            timerCOM.Interval = 1000;   //timerCOM atualiza a lista de portas COM
            timerCOM.Enabled = true;
            timerTest.Interval = 1000;    //timertest é o timer para obter resposta de conexão do equipamento
            timerTest.Enabled = false;
            timerSpace.Interval = 100;    //timerSpace é o timer do decorrer do experimento
            timerSpace.Enabled = false;
            timerTextBox.Interval = 100;    //timerTextBox da um tempo para receber e escrever os dados do equipamento
            timerTextBox.Enabled = false;
            time = 0;

            chanVolt.Items.Clear();
            chanVolt.Items.Add("Channel 1");
            chanVolt.Items.Add("Channel 2");
            chanVolt.SelectedIndex = 0;

            thermType.Items.Clear();
            thermType.Items.Add("J");
            thermType.Items.Add("K");
            thermType.Items.Add("T");
            thermType.Items.Add("E");
            thermType.Items.Add("R");
            thermType.Items.Add("S");
            thermType.Items.Add("B");
            thermType.Items.Add("N");
            thermType.SelectedIndex = 1;

            currValue.Text = "100.0";

            currCombo.Items.Clear();
            currCombo.Items.Add("mA");
            currCombo.Items.Add("µA");
            currCombo.SelectedIndex = 0;

            resetBt.Enabled = false;
            numbTest.Enabled = false;
            testCheck.Checked = true;
            timeCheck.Checked = true;
            if ((serialPortS.IsOpen == true) && (serialPortV.IsOpen == true))      //Botão "start" será apenas habilitado quando ambos equipamentos forem conectados
                startBt.Enabled = true;
            else
                startBt.Enabled = false;

            startBt.Text = "Start";
            saveLocal.Text = "C:\\Users\\" + Environment.UserName + "\\Desktop";
            saveName.Text = "4 Wire Res per Temp";
            timeDisplay.Text = "Time: 00:00:00:000";
            tempDisplay.Text = "Voltage: +0.00000000E+00 V\r\n" + 
                               "Temperature: +0.00000000E+00 ºC";

            errorFlag = 0;
            saveFlag = 0;
            cpFlag = 0;
            bugFlag = 0;
            bugCount = 0;
            testCount = -1;
            cpCount = 0;
            lastStop = 0;
            lineRemov = 0;
            tlineRemov = 0;
            spaceCount = 0;
            time = 0;
        }

        private void Config(string chapigas)
        {
            if (chapigas == "Open")     //Volta a ser seguro habilitar a desconexão e edição das configurações.
            {
                conectSour.Enabled = true;
                conectVolt.Enabled = true;
                chanVolt.Enabled = true;
                thermType.Enabled = true;
                if (testCheck.Checked == false)
                    numbTest.Enabled = true;
                testCheck.Enabled = true;
                currValue.Enabled = true;
                currCombo.Enabled = true;
                resetBt.Enabled = true;
                saveLocal.Enabled = true;
                saveName.Enabled = true;
                timeCheck.Enabled = true;
                searchBt.Enabled = true;
            }
            if (chapigas == "Close")      //Desabilita configurações 
            {
                conectSour.Enabled = false;
                conectVolt.Enabled = false;
                chanVolt.Enabled = false;
                thermType.Enabled = false;
                numbTest.Enabled = false;
                testCheck.Enabled = false;
                currValue.Enabled = false;
                currCombo.Enabled = false;
                resetBt.Enabled = false;
                saveLocal.Enabled = false;
                saveName.Enabled = false;
                timeCheck.Enabled = false;
                searchBt.Enabled = false;
            }
        }

        private void info1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Voltmeter and SourceMeter Conections Info\r\n" +
                            "  * Voltmeter COM: Select the COM Port that plugged the voltmeter and connect with \"Conect\" button down bellow.\r\n" +
                            "     Compatible with KEITHLEY INSTRUMENTS INC.,MODEL 2182A\r\n" + 
                            "  * SourceMeter COM: Select the COM Port that plugged the sourcemeter and connect with \"Conect\" button down bellow.\r\n" +
                            "     Compatible with KEITHLEY INSTRUMENTS INC.,MODEL 2400\r\n" + 
                            "\r\n" +
                            "Settings' info\r\n" +
                            "  * Voltmeter Channel: Select voltmeter's channel. Voltmeter's range and thermocouple's channel will be set automatically.\r\n" +
                            "  * Thermocouple Type: Select thermocouple's type.\r\n" +
                            "  * Number of Tests: Set the max number of tests to be done. Each test lasts around 3 seconds or choose an unlimited experiment.\r\n" +
                            "  * Current Value: Set the current's value to be apply.   Min = 1 μA, Max = 1000 mA\r\n" +
                            "\r\n" +
                            "Save Settings' info\r\n" +
                            "  * Save at: Search the location to save the file with \"Search\" button.\r\n" +
                            "  * Save as: Set the file's name.\r\n" +
                            "  * Add time to file's name: It add the actual day and time, with configuration \"yyyy-mm-dd h.-mm\", to file's name.\r\n" +
                            "\r\n" + 
                            "Test Return and Experiment Running's info\r\n" +
                            "  * Test Return: It should appears the identification's name of the instruments.\r\n" +
                            "    It test the conection and make sure that the voltmeter and sourcemeter are in correctaly COM Ports.\r\n" +
                            "  * Experiment Running:\r\n" +
                            "      (1) \"BEGINING OF EXPERIMENT\" will appears.\r\n" +
                            "      (2) It should appears, with an approximate rate of 50 tests per minute, the elapsed time of the experiment, voltage's value and temperature's value return from voltmeter.\r\n" +
                            "      (3) Each 100 tests, the data will become already saved, counting a checkpoint.\r\n" +
                            "      (4) If seted, past the max number of tests, \"END OF EXPERIMENT\" will appears.\r\n" +
                            "  * Start Button: Initiate or continue the experiment running and all setup's configurations will be disabled.\r\n" +
                            "  * Stop Button: It will make one more test and then stop and save the current data.\r\n" +
                            "    Lines with errors or overflow values ​​may be deleted.\r\n" +
                            "\r\n" +
                            "WARNING: Do not change the file's name or lacation during the experiment!\r\n" + 
                            "V 2.0.1.00\r\n"
                            , "Info");
        }

        private void graphBt_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                int height;
                int width;
                height = this.Location.Y + this.Height + 5;
                width = this.Location.X;  //Ao lado do Form1 com 5 pixels de espaço
                f2 = new Form2();  //Abre um novo Form2 para plotar o gráfico.
                f2.Show();
                try
                {
                    f2.Location = new Point(width, height);
                    Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                    f2.Size = new Size(screen.Width - 20, screen.Height - this.Size.Height - 20);
                    f2.cpuChart.Size = new Size(f2.Size.Width - 40, f2.Size.Height - 60);
                }
                catch { }
            }
        }

        private void comVolt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                conectVolt.PerformClick();
            }
        }

        private void comSour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                conectSour.PerformClick();
            }
        }

        private void numbTest_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); //Apenas permitir digitar números
        }

        private void currValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) //Apenas permitir 1 ponto
            {
                e.Handled = true;
            }
        }

        private void saveName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar.ToString() == "/") || (e.KeyChar.ToString() == "\\*") || //Caracteres não permitidos em nome de arquivos
                (e.KeyChar.ToString() == ":") || (e.KeyChar.ToString() == "*") ||
                (e.KeyChar.ToString() == "<") || (e.KeyChar.ToString() == ">") ||
                (e.KeyChar.ToString() == "?") || (e.KeyChar.ToString() == "|") ||
                (e.KeyChar.ToString() == "\""))
                e.Handled = true;
        }

        private void saveLocal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        private void testCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (testCheck.Checked)
            {
                numbTest.Enabled = false;
                numbTest.Clear(); 
            }

            else
            {
                numbTest.Enabled = true;
                numbTest.Text = "500"; 
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (serialPortS.IsOpen == true)
                {
                    serialPortS.WriteLine(":OUTP OFF\r\n");    //Desliga a Corrente
                    serialPortS.Close();         //Desconectar quando fechar programa
                }
                if (serialPortV.IsOpen == true)
                    serialPortV.Close();         //Desconectar quando fechar programa
            }
            catch
            {
                return;
            }
        }
    }
}
