using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
//namespaces used
//namespace are like libraries in C/C++


namespace WindowsFormsApp1.Monte_Carlo
{
    //Another class is used. So the namespace of that class is used.
    using Business;
    public partial class @try : Form
    {
        
        //Constructor of the class
        public @try()
        {
            //Built in function to Initialize components, handled by OS.
            InitializeComponent();

        }

       
        /***************** : Initialization of variables :***********************/
        string ratio = "";
        int click = 0;
        List<List<int>> peepData = new List<List<int>>();

        List<int> infected = new List<int>();
        List<int> death = new List<int>();
        List<int> childDeath = new List<int>();
        List<int> adultDeath = new List<int>();
        List<int> oldDeath = new List<int>();
        List<int> recovered = new List<int>();
        List<int> childRecovered = new List<int>();
        List<int> adultRecovered = new List<int>();
        List<int> oldDRecovered = new List<int>();
        
        List<List<int>> graphData = new List<List<int>>();

        int setupReady = 0;
        int initalProb = 0;
        int finalProb = 0;
        int childPop = 0;
        int adlutPop = 0;
        int oldPop = 0;
        int childead = 0;
        int adultDead = 0;
        int oldDead = 0;
        int speed = 0;
        int daysBeforQ = 0;


        int population = 0;
        int popIter = 0;
        int popSize = 0;
        int popSizehalf = 0;
        int popConnector = 0;
        int popPaint = 0;

        int simulationLength = 0;

        int setState = 0;
        int SimulateState = 1;
        int resetState = 1;
        int daycount = 2;
        int dailycount = 0;



        Random r = new Random(DateTime.Now.Millisecond);
       

        Point src = new Point(200, 300);
        Point dst = new Point(300, 400);
        int count = 1;
        Graphics g = null;
        /**************: Intialization of variables End: ******************/


        /**************: Intialization of UI :******************/
        //This function is called(and handeld by OS) when the UI components are loaded on screen. 
        //We use it to extract the graphics object of a panel in UI when loaded. 
        private void try_Load(object sender, EventArgs e)
        {       

            g = panel1.CreateGraphics();
           
            click = 0;
        }
        //This fucntion is called(and handled by OS) when UI compleates loading.
        //Use to show a the user welcome message and asking for input
        private void try_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("Enter parameters and press Simulate.");

        }
        /**************: Intialization of UI End: ******************/


        /**************: Parameter Input from User :******************/
        //This function takes the user input and assign values to required variables.
        //It checks Invalid input and handles error.
        bool setDetails()
        {
            if (txtAdultDead.Text != "" && txtAdultPop.Text != "" && txtChildDead.Text != "" &&
                txtChildPop.Text != "" && txtInProb.Text != "" && txtOldDead.Text != "" &&
                txtOldPop.Text != "" && txtQprob.Text != "" && txtSafeDays.Text != "" && cmbPop.SelectedIndex != -1 && txtMonths.Text != "")
            {
                try
                {
                    initalProb = Convert.ToInt32(txtInProb.Text);
                    finalProb = Convert.ToInt32(txtQprob.Text);
                    childPop = Convert.ToInt32(txtChildPop.Text);
                    adlutPop = Convert.ToInt32(txtAdultPop.Text);
                    oldPop = Convert.ToInt32(txtOldPop.Text);
                    childead = Convert.ToInt32(txtChildDead.Text);
                    adultDead = Convert.ToInt32(txtAdultDead.Text);
                    oldDead = Convert.ToInt32(txtOldDead.Text);
                    daysBeforQ = Convert.ToInt32(txtSafeDays.Text);
                    population = Convert.ToInt32(cmbPop.SelectedItem.ToString());
                    simulationLength = Convert.ToInt32(txtMonths.Text) * 30;
                    setPopulation();
                    if (initalProb == 0 || finalProb == 0 || childPop == 0 || oldPop == 0 || adlutPop == 0 ||
                        childead == 0 || adultDead == 0 || oldDead == 0 || daysBeforQ == 0)
                    {
                        MessageBox.Show("Some fields are 0");
                        return false;
                    }
                    else
                        return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("All fields must be Positive Integer");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Some Fields are Empty");
                return false;
            }

        }
        /**************: Parameter Input from User End :******************/


            
        /**************: Intialization of People Nodes in Data Structure :******************/
        //A helping function for handling graphics propertues of various size population.
        //That is it varies the size of recangles according to population group (625,900,2500 etc). 
        public void setPopulation()
        {
            if (population == 2500)
            {
                popIter = 100;
                popSize = 6;
                popSizehalf = 3;
                popConnector = 50;
                popPaint = 2;
            }
            else if (population == 900)
            {
                popIter = 60;
                popSize = 10;
                popSizehalf = 5;
                popConnector = 30;
                popPaint = 3;

            }
            else if (population == 625)
            {
                popIter = 50;
                popSize = 12;
                popSizehalf = 6;
                popConnector = 25;
                popPaint = 4;
            }
            else if (population == 100)
            {
                popIter = 20;
                popSize = 30;
                popSizehalf = 15;
                popConnector = 10;
                popPaint = 10;
            }
        }
        //A helping function for deciding age group randomly for a node. 
        //The probability of choosing the age group depends on the user supplied probabilities.
        int ageCreater(int val)
        {

            if (1 <= val && val <= childPop)
                return 1;
            else if (childPop + 1 <= val && val <= childPop + adlutPop)
                return 2;
            else if (childPop + adlutPop + 1 <= val)
                return 3;
            else
                return 0;

        }
        //Main function to create Nodes of the Graph.It uses a 2D array data structure. 
        //Each row corresponds to one node and contains information of that node.
        //It contains ID, (X,Y), Health_Stauts, Age Group, Infection Day Count, Prob. of infecting 4 neighbour nodes
        private void createPeepData()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int popProb = childPop + adultDead + oldPop;
            int i, j, count = 0;
            for (i = 0; i < popIter; i = i + 2)
            {
                for (j = 0; j < popIter; j = j + 2)
                {
                    List<int> temp = new List<int>();
                    temp.Add(count + 1);
                    temp.Add(j * popSize + popSizehalf);
                    temp.Add(i * popSize + popSizehalf);
                    temp.Add(0);//healthStatus
                    temp.Add(ageCreater(r.Next(1,popProb)));//AgeGroup
                    temp.Add(0);//InfectionDayCount
                    ratio = ratio + temp[4].ToString() + " ";
                    
                    temp.Add(initalProb);// Prob. of Infectng Right
                    temp.Add(initalProb);// Prob. of Infectng Left
                    temp.Add(initalProb);// Prob. of Infectng Up
                    temp.Add(initalProb);//// Prob. of Infectng Down

                    peepData.Add(temp);
                    count = count + 1;


                }

            }
        }
        /**************: Intialization of People Nodes in Data Structure End :******************/


        /**************: Infection Spreding Function Start :******************/
        //Function that spreads infection. 
        //Depending on the probablities of the source to infect it's neighbours,
        //this function decides which neighbour node to infect.
        //Once a neighbour is choosen to be infected, newly infected node's prababilities
        //to infect its neoghbours are set randomly.
        //It also updates infected table and infection day count of the node.
        private void infectNeighbours(int source, int prob1, int prob2, int prob3, int prob4)
        {
            int probNew = initalProb;
            if (daycount>daysBeforQ)
            {
                probNew = initalProb * finalProb;
            }
            
            if (peepData[source - 1][3] == 1)
            {
                int prob = 2*(prob1 + prob2 + prob3 + prob4);
                int idex = r.Next(1, prob);
                int toinfect = 0;
                if (1<=idex && idex <=prob1)
                {
                    toinfect = source - popConnector;

                    if (toinfect >= 1 && toinfect <= population)
                    {
                        if (peepData[toinfect - 1][3] == 0)
                        {
                            peepData[toinfect - 1][3] = 1;
                            peepData[toinfect - 1][5] = 1;
                            infected.Add(toinfect);
                            src = new Point(peepData[toinfect - 1][1], peepData[toinfect - 1][2]);
                            dst = new Point(peepData[source - 1][1], peepData[source - 1][2]);
                            drawInfect(src, dst);
                            dailycount = dailycount + 1;

                            Random r2 = new Random(DateTime.Now.Millisecond);
                            peepData[toinfect - 1][6] = r2.Next(1, probNew);
                            peepData[toinfect - 1][7] = r2.Next(1, probNew);
                            peepData[toinfect - 1][8] = r2.Next(1, probNew);
                            peepData[toinfect - 1][9] = r2.Next(1, probNew);


                        }
                    }
                }


                else if (prob1<idex && idex<= prob1+prob2)
                {
                    toinfect = source + popConnector;
                    if (toinfect > 0 && toinfect <= population)
                    {
                        if (peepData[toinfect - 1][3] == 0)
                        {
                            peepData[toinfect - 1][3] = 1;
                            peepData[toinfect - 1][5] = 1;
                            infected.Add(toinfect);
                            src = new Point(peepData[toinfect - 1][1], peepData[toinfect - 1][2]);
                            dst = new Point(peepData[source - 1][1], peepData[source - 1][2]);
                            drawInfect(src, dst);
                            dailycount = dailycount + 1;

                            Random r2 = new Random(DateTime.Now.Millisecond);
                            peepData[toinfect - 1][6] = r2.Next(1, probNew);
                            peepData[toinfect - 1][7] = r2.Next(1, probNew);
                            peepData[toinfect - 1][8] = r2.Next(1, probNew);
                            peepData[toinfect - 1][9] = r2.Next(1, probNew);
                        }

                    }
                }
                else if (prob1+prob2 < idex && idex <= prob2 + prob3)
                {
                    if (source % popConnector == 1)
                    {

                    }
                    else
                    {
                        toinfect = source - 1;
                        if (toinfect > 0 && toinfect <= population)
                        {
                            if (peepData[toinfect - 1][3] == 0)
                            {
                                peepData[toinfect - 1][3] = 1;
                                peepData[toinfect - 1][5] = 1;
                                infected.Add(toinfect);
                                src = new Point(peepData[toinfect - 1][1], peepData[toinfect - 1][2]);
                                dst = new Point(peepData[source - 1][1], peepData[source - 1][2]);
                                drawInfect(src, dst);
                                dailycount = dailycount + 1;

                                Random r2 = new Random(DateTime.Now.Millisecond);
                                peepData[toinfect - 1][6] = r2.Next(1, probNew);
                                peepData[toinfect - 1][7] = r2.Next(1, probNew);
                                peepData[toinfect - 1][8] = r2.Next(1, probNew);
                                peepData[toinfect - 1][9] = r2.Next(1, probNew);
                            }
                        }
                    }
                }

                else if (prob2 + prob3 < idex && idex <= prob3 + prob4)
                {   
                    if (source % popConnector == 0)
                    {

                    }
                    else
                    {
                        toinfect = source + 1;
                        if (toinfect > 0 && toinfect <= population)
                        {
                            if (peepData[toinfect - 1][3] == 0)
                            {
                                peepData[toinfect - 1][3] = 1;
                                peepData[toinfect - 1][5] = 1;
                                infected.Add(toinfect);
                                src = new Point(peepData[toinfect - 1][1], peepData[toinfect - 1][2]);
                                dst = new Point(peepData[source - 1][1], peepData[source - 1][2]);
                                drawInfect(src, dst);
                                dailycount = dailycount + 1;

                                Random r2 = new Random(DateTime.Now.Millisecond);
                                peepData[toinfect - 1][6] = r2.Next(1, probNew);
                                peepData[toinfect - 1][7] = r2.Next(1, probNew);
                                peepData[toinfect - 1][8] = r2.Next(1, probNew);
                                peepData[toinfect - 1][9] = r2.Next(1, probNew);
                            }
                        }
                    }
                }
            }



        }
        //Helping function to Visually represnting Infected Nodes
        //It paints the rectangles representing infected nodes in black
        //Also onnects source and infected with a black line
        public void drawInfect(Point A, Point B)
        {
            Pen hmm = new Pen(Color.Black, 2);
            g.DrawLine(hmm, A, B);
            SolidBrush sb = new SolidBrush(Color.Black);
            Size sd = new Size(popSize + popPaint, popSize + popPaint);
            Point A2 = new Point(A.X - popSizehalf, A.Y - popSizehalf);
            Point B2 = new Point(B.X - popSizehalf, B.Y - popSizehalf);

            Rectangle rb = new Rectangle(A2, sd);
            Rectangle rb2 = new Rectangle(B2, sd);
            g.FillRectangle(sb, rb);
            g.FillRectangle(sb, rb);

            count = count + 1;
        }
        /**************: Infection Spreding Function End :******************/
        

        //***************: Infected Fate Decider Functio Start: ************/
        //This function decides the fate of nodes in the infected table.
        //If infection day count is less than 14, it simply increases the count
        //In days 15,16,17 it decies randomly if a node is dead or not. 
        //Death Probabilitis of corresponding age group is used in that case.
        //After day 18 it decides the person as recovered. Changes its health status to recovered.
        private void turnDeathwheel(int patient, int val1, int val2, int val3)
        {

            if (peepData[patient - 1][5] < 14)
            {
                peepData[patient - 1][5] = peepData[patient - 1][5] + 1;


            }
            else
            {
                int x = 0;
                if (peepData[patient - 1][4] == 1 && val1 == 1)
                    x = 1;
                else if (peepData[patient - 1][4] == 2 && val2 == 1)
                    x = 1;
                else if (peepData[patient - 1][4] == 3 && val3 == 1)
                    x = 1;
                if (x == 1)
                {
                    death.Add(patient);
                    peepData[patient - 1][3] = 2;
                    Point tip = new Point(peepData[patient - 1][1] - popSizehalf, peepData[patient - 1][2] - popSizehalf);
                    Color cr = Color.Red;
                    SolidBrush sb = new SolidBrush(cr);
                    Size sz = new Size(popSize + popPaint, popSize + popPaint);
                    Rectangle ret = new Rectangle(tip, sz);
                    g.FillRectangle(sb, ret);

                    if (peepData[patient - 1][4] == 1)
                        childDeath.Add(patient);
                    else if (peepData[patient - 1][4] == 2)
                        adultDeath.Add(patient);
                    else if (peepData[patient - 1][4] == 3)
                        oldDeath.Add(patient);

                }

                peepData[patient - 1][5] = peepData[patient - 1][5] + 1;
                if (peepData[patient - 1][5] > 17)
                {
                    recovered.Add(patient);
                    peepData[patient - 1][3] = 3;//recovered
                    Point tip = new Point(peepData[patient - 1][1] - popSizehalf, peepData[patient - 1][2] - popSizehalf);
                    Color cr = Color.Yellow;
                    SolidBrush sb = new SolidBrush(cr);
                    Size sz = new Size(popSize + popPaint, popSize + popPaint);
                    Rectangle ret = new Rectangle(tip, sz);
                    g.FillRectangle(sb, ret);

                    if (peepData[patient - 1][4] == 1)
                        childRecovered.Add(patient);
                    else if (peepData[patient - 1][4] == 2)
                        adultRecovered.Add(patient);
                    else if (peepData[patient - 1][4] == 3)
                        oldDRecovered.Add(patient);

                }

            }


        }
        //***************: Infected Fate Decider Functio End: ************//


        //***************: Function to clear all tables, data structure and variables: ************/
        private void resetPanel()
        {

            if (resetState == 0)
            {
                resetState = 1;
                setState = 0;

                Color cr = panel1.BackColor;
                g.Clear(cr);
                panel1.Controls.Clear();
                infected.Clear();
                death.Clear();
                peepData.Clear();
                adultDeath.Clear();
                childDeath.Clear();
                oldDeath.Clear();
                recovered.Clear();
                oldDRecovered.Clear();
                childRecovered.Clear();
                adultRecovered.Clear();
                graphData.Clear();
                
                crtDailyCase.Series[0].Points.Clear();
                crtTotalCase.Series[0].Points.Clear();
                crtDailyDeaths.Series[0].Points.Clear();
                crtTotalDeaths.Series[0].Points.Clear();
                crtDeathPie.Series[0].Points.Clear();
                crtRecoveredPie.Series[0].Points.Clear();
                crtDailyReco.Series[0].Points.Clear();
                crtTotalReco2.Series[0].Points.Clear();


            }
            else
                MessageBox.Show("Inappropriate Button.");
        }
        //***************: Reset Function End: ************/

        //***************: Optinal Functions to create Pie-Charts ************/
        private void deathpainter()
        {
            
            crtDeathPie.Series[0].Points.AddXY(1,childDeath.Count);
            crtDeathPie.Series[0].Points[0].Label = "Child";
            crtDeathPie.Series[0].Points[0].Color = Color.LightSalmon;
            crtDeathPie.Series[0].Points.AddXY(2,adultDeath.Count);
            crtDeathPie.Series[0].Points[1].Label = "Adult";
            crtDeathPie.Series[0].Points[1].Color = Color.LightGreen;
            crtDeathPie.Series[0].Points.AddXY(3,oldDeath.Count);
            crtDeathPie.Series[0].Points[2].Label = "Old";
            crtDeathPie.Series[0].Points[2].Color = Color.Blue;
            crtDeathPie.Update();
            
        }
        private void recoveredpainter()
        {
            
            int i, childReco, adultReco, oldReco;
            childReco = adultReco = oldReco = 0;
            for (i = 0; i < peepData.Count; i++)
            {
                if (peepData[i][3] == 1 && peepData[i][4] == 1)
                    childReco = childReco + 1;
                else if (peepData[i][3] == 1 && peepData[i][4] == 2)
                    adultReco = adultReco + 1;
                else if (peepData[i][3] == 1 && peepData[i][4] == 3)
                    oldReco = oldReco + 1;
            }

           
            crtRecoveredPie.Series[0].Points.AddXY(1,childRecovered.Count);
            crtRecoveredPie.Series[0].Points[0].Label = "Child";
            crtRecoveredPie.Series[0].Points[0].Color = Color.LightSalmon;
            crtRecoveredPie.Series[0].Points.AddXY(2, adultRecovered.Count);
            crtRecoveredPie.Series[0].Points[1].Label = "Adult";
            crtRecoveredPie.Series[0].Points[1].Color = Color.LightGreen;
            crtRecoveredPie.Series[0].Points.AddXY(3,oldDRecovered.Count);
            crtRecoveredPie.Series[0].Points[2].Label = "Old";
            crtRecoveredPie.Series[0].Points[2].Color = Color.Blue;
            crtRecoveredPie.Update();
          
        }
        //***************: Pie-Chart Functios End: ************/







        /***************: Functions to handle button clicks : ************/
        //Function to handle Simulate button. 
        //It also iterates the simulation() function $n$ times.
        private void button1_Click(object sender, EventArgs e)
        {
            if (setState == 0)
            {
                int iteration = Convert.ToInt32(txtIter.Text);
                //exception handling not done
                
                if (setDetails())
                {

                    for (int z = 0; z < iteration; z++)
                    { 
                        createPeepData();
                        setState = 1;
                        SimulateState = 0;
                        List<string> initialInfectors = new List<string>();
                        initialInfectors = Common.splitString(txtInitalInfector.Text, ',');
                        for (int i = 0; i < initialInfectors.Count; i++)
                        {
                            int val;
                            try
                            {
                                val = Convert.ToInt32(initialInfectors[i]);
                                infected.Add(val);
                                peepData[val - 1][3] = 1;
                                src = new Point(peepData[val - 1][1], peepData[val - 1][2]);
                                dst = new Point(peepData[val - 1][1], peepData[val - 1][2]);
                                drawInfect(src, dst);

                                peepData[val - 1][6] = initalProb;
                                peepData[val - 1][7] = initalProb;
                                peepData[val - 1][8] = initalProb;
                                peepData[val - 1][9] = initalProb;


                            }
                            catch(Exception bb)
                            {
                                MessageBox.Show("Intial Infectors not set properly");
                                SimulateState = 1;
                            }
                           
                           
                        }

                        List<int> temp = new List<int>();
                        temp.Add(1);
                        temp.Add(infected.Count);
                        temp.Add(infected.Count);
                        temp.Add(0);
                        temp.Add(0);
                        temp.Add(0);
                        temp.Add(0);
                        graphData.Add(temp);

                        if (SimulateState == 0)
                        {
                            simulateStart();
                            SimulateState = 1;
                            resetState = 0;
                        }

                        resetPanel();
                    }

                    MessageBox.Show("Done");
                    setState = 0;
                }
                
            }

            else
                MessageBox.Show("Inappropriate Button.");
        }
        //Function to handle Get Data button
        private void button5_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("The data recorded in the files");

        }
        //***************: Button Control End: ************/


        /*************: Main function to start the simulation: ******************/
        //uses all the helping functions defind above. Runs as per the pseudocode.
        private void simulateStart()
        {
           
            for (int i = 0; i < peepData.Count; i++)
            {
                Point tip = new Point(peepData[i][1] - popSizehalf, peepData[i][2] - popSizehalf);
                Color cr;
                if (peepData[i][4] == 1)
                    cr = Color.Green;
                else if (peepData[i][4] == 2)
                    cr = Color.LightGreen;
                else
                    cr = Color.Blue;
                SolidBrush sb = new SolidBrush(cr);
                Size sz = new Size(popSize + popPaint, popSize + popPaint);
                Rectangle ret = new Rectangle(tip, sz);
                g.FillRectangle(sb, ret);

            }
            daycount = 2;

          
           
            int deadcont = 0;
            int recocont = 0;
            int oldDeadcont = 0;

            Random child = new Random(DateTime.Now.Millisecond);

            Thread.Sleep(20);

            Random adult = new Random(DateTime.Now.Millisecond);


            Thread.Sleep(20);

            Random old = new Random(DateTime.Now.Millisecond);

           
            while (infected.Count != 0 && infected.Count <= population && daycount <= simulationLength)
            {
                List<int> graphBits = new List<int>();
                DataPoint dp2 = new DataPoint();
                dp2.SetValueY(infected.Count);
                crtTotalCase.Series[0].Points.Add(dp2);
                crtTotalCase.Update();



                int i;
                dailycount = 0;
                int k = infected.Count;
                for (i = 0; i < k; i++)
                {
                    
                        infectNeighbours(infected[i], peepData[infected[i]-1][6], peepData[infected[i]-1][7], peepData[infected[i]-1][8], peepData[infected[i]-1][9]);


                }


                graphBits.Add(daycount);
                graphBits.Add(dailycount);
                graphBits.Add(infected.Count);
                DataPoint dp = new DataPoint();
                dp.SetValueY(dailycount);
                crtDailyCase.Series[0].Points.Add(dp);
                crtDailyCase.Update();
               

                daycount = daycount + 1;

                
                    for (i = 0; i < infected.Count; i++)
                    {
                        int child2 = child.Next(1, childead);
                        int adult2 = adult.Next(1, adultDead);
                        int old2 = old.Next(1, oldDead);
                        if (peepData[infected[i] - 1][3] != 2 && peepData[infected[i] - 1][3] != 3)
                            turnDeathwheel(infected[i], child2, adult2, old2);
                    }
                    graphBits.Add(death.Count - deadcont);
                    DataPoint dp4 = new DataPoint();
                    dp4.SetValueY(death.Count - deadcont);
                    crtDailyDeaths.Series[0].Points.Add(dp4);
                    crtDailyDeaths.Update();
                    graphBits.Add(death.Count);

                    graphBits.Add(recovered.Count - recocont);
                    graphBits.Add(recovered.Count);
                    DataPoint dp6 = new DataPoint();
                    dp6.SetValueY(recovered.Count - recocont);
                    crtDailyReco.Series[0].Points.Add(dp6);
                    crtDailyReco.Update();



                

                deadcont = death.Count;
                recocont = recovered.Count;
                oldDeadcont = oldDeath.Count;

                DataPoint dp3 = new DataPoint();
                dp3.SetValueY(deadcont);
                crtTotalDeaths.Series[0].Points.Add(dp3);
                crtTotalDeaths.Update();

                DataPoint dp5 = new DataPoint();
                dp5.SetValueY(recocont);
                crtTotalReco2.Series[0].Points.Add(dp5);
                crtTotalReco2.Update();

                graphData.Add(graphBits);



            }
            deathpainter();
            recoveredpainter();
            List<List<string>> summaryData = new List<List<string>>();
            List<string> tempo = new List<string>();
            tempo.Add(Convert.ToString(peepData.Count));
            tempo.Add(Convert.ToString(infected.Count));
            tempo.Add(Convert.ToString(recovered.Count));
            tempo.Add(Convert.ToString(death.Count));
            tempo.Add(Convert.ToString(childRecovered.Count));
            tempo.Add(Convert.ToString(adultRecovered.Count));
            tempo.Add(Convert.ToString(oldDRecovered.Count));
            tempo.Add(Convert.ToString(childDeath.Count));
            tempo.Add(Convert.ToString(adultDeath.Count));
            tempo.Add(Convert.ToString(oldDeath.Count));
            double val;
            val = Convert.ToDouble(infected.Count) / Convert.ToDouble(peepData.Count)*100;
            tempo.Add(Convert.ToString(val));
            val = Convert.ToDouble(recovered.Count) / Convert.ToDouble(peepData.Count) * 100;
            tempo.Add(Convert.ToString(val));
            val = Convert.ToDouble(death.Count) / Convert.ToDouble(peepData.Count) * 100;
            tempo.Add(Convert.ToString(val));
            summaryData.Add(tempo);
            
            MyFile.writeListOfList("simulation_data.txt", peepData, " ");
            MyFile.writeListOfList("simulation_data2.txt", graphData, " ");
            MyFile.writeListOfListString("simulation_data3.txt", summaryData, " ");

          
        }
        /*************: Main function End: ******************/





    }

//**************************------------Thank you: THE END----------***************************//
}

       