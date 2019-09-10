using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSLecProj1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void optimal(int no_of_frames, int no_of_pages, int[] pages)
        {
            int flag1 = 0, flag2 = 0, flag3 = 0, faults = 0, pos = 0, max = -9999, u = 1;
            int[] frames = new int[no_of_frames];
            int[] temp = new int[no_of_frames];
            string pagesan=null; //= new string[99999];
            for (int i = 0; i < no_of_frames; ++i)
            {
                frames[i] = -1;
            }
            //==============================================
            for (int i = 0; i < no_of_pages; ++i)
            {
                flag1 = flag2 = 0;

                for (int j = 0; j < no_of_frames; ++j)
                {
                    if (frames[j] == pages[i])
                    {
                        flag1 = flag2 = 1;
                        break;
                    }
                }

                if (flag1 == 0)
                {
                    for (int j = 0; j < no_of_frames; ++j)
                    {
                        if (frames[j] == -1)
                        {
                            faults++;
                            frames[j] = pages[i];
                            flag2 = 1;
                            break;
                        }
                    }
                }

                if (flag2 == 0)
                {
                    flag3 = 0;

                    for (int j = 0; j < no_of_frames; ++j)
                    {
                        temp[j] = -1;

                        for (int k = i + 1; k < no_of_pages; ++k)
                        {
                            if (frames[j] == pages[k])
                            {
                                temp[j] = k;
                                break;
                            }
                        }
                    }

                    for (int j = 0; j < no_of_frames; ++j)
                    {
                        if (temp[j] == -1)
                        {
                            pos = j;
                            flag3 = 1;
                            break;
                        }
                    }

                    if (flag3 == 0)
                    {
                        max = temp[0];
                        pos = 0;

                        for (int j = 1; j < no_of_frames; ++j)
                        {
                            if (temp[j] > max)
                            {
                                max = temp[j];
                                pos = j;
                            }
                        }
                    }

                    frames[pos] = pages[i];
                    faults++;
                }
                pagesan += "Frame" + u + ":";
                for (int j = 0; j < no_of_frames; ++j)
                {
                    pagesan += frames[j];
                    if (j + 1 < no_of_frames)
                    {
                        pagesan += ",";
                    }
                }
                u++;
                pagesan += System.Environment.NewLine;
            }
            //MessageBox.Show(pagesan);
            richTextBox3.Text = pagesan;
            textBox6.Text = ""+faults;
        }
        int findLRU(int []time, int n)
        {
            int i, minimum = time[0], pos = 0;
 
             for(i = 1; i < n; ++i)
             {
                if(time[i] < minimum)
                {
                    minimum = time[i];
                    pos = i;
                }
            }
    
            return pos;
        }
        void LRU(int no_of_frames, int no_of_pages, int[] pages)
        {
            int flag1 = 0, flag2 = 0, flag3 = 0, faults = 0, pos = 0, counter = 0, u = 1;
            int[] frames = new int[no_of_frames];
            int[] time = new int[no_of_pages];
            string pagesan = null; //= new string[99999];
            for (int i = 0; i < no_of_frames; ++i)
            {
                frames[i] = -1;
            }
            //====================================================
            for (int i = 0; i < no_of_pages; ++i)
            {
                flag1 = flag2 = 0;

                for (int j = 0; j < no_of_frames; ++j)
                {
                    if (frames[j] == pages[i])
                    {
                        counter++;
                        time[j] = counter;
                        flag1 = flag2 = 1;
                        break;
                    }
                }

                if (flag1 == 0)
                {
                    for (int j = 0; j < no_of_frames; ++j)
                    {
                        if (frames[j] == -1)
                        {
                            counter++;
                            faults++;
                            frames[j] = pages[i];
                            time[j] = counter;
                            flag2 = 1;
                            break;
                        }
                    }
                }

                if (flag2 == 0)
                {
                    pos = findLRU(time, no_of_frames);
                    counter++;
                    faults++;
                    frames[pos] = pages[i];
                    time[pos] = counter;
                }
                pagesan += "Frame" + u + ":";
                for (int j = 0; j < no_of_frames; ++j)
                {
                    pagesan += frames[j];
                    if (j + 1 < no_of_frames)
                    {
                        pagesan += ",";
                    }
                }
                u++;
                pagesan += System.Environment.NewLine;
            }
            //MessageBox.Show(pagesan);
            richTextBox2.Text = pagesan;
            textBox5.Text = "" + faults;
        }
        /*void FIFO(int no_of_frames, int no_of_pages, int[] pages)
        {
            int[] frames = new int[no_of_frames];
            int faults = 0, num = 0, u = 1;
            bool flag = false;
            string pagesan = null; //= new string[99999];
            for (int i = 0; i < no_of_frames; ++i)
            {
                frames[i] = -1;
            }
            //====================================================
            for (int i = 0; i < no_of_pages; i++)
            {
                flag = true;
                int page = pages[i];
                for (int j = 0; j < no_of_frames; j++)
                {
                    if (frames[j] == page)
                    {
                        flag = false;
                        faults++;
                        break;
                    }
                }
                if (num == no_of_frames)
                {
                    num = 0;
                }

                if (flag)
                {
                    frames[num] = page;
                    num++;
                }
                pagesan+="Frame" + u + ":";
                for (int j = 0; j < no_of_frames; ++j)
                {
                    pagesan += frames[j];
                    if (j+1<no_of_frames)
                    {
                        pagesan += ",";
                    }
                }
                u++;
                pagesan += System.Environment.NewLine;
            }
            //MessageBox.Show(pagesan);
            richTextBox1.Text = pagesan;
            textBox4.Text = "" + faults;
           
        }*/
        void FIFO(int no_of_frames, int no_of_pages, int[] pages)
        {
            int faults = 0,u = 1, s = 0;
            int[] frames = new int[no_of_frames];
            int[] time = new int[no_of_pages];
            string pagesan = null; //= new string[99999];
            for (int i = 0; i < no_of_frames; ++i)
            {
                frames[i] = -1;
            }
            //====================================================
            for (int i = 0; i < no_of_pages; i++)
            {
                s = 0;
                for (int j = 0; j < no_of_frames; j++)
                {
                    if (pages[i] == frames[j])
                    {
                        s++;
                        faults--;
                    }
                }
                faults++;
                if ((i < no_of_frames) && (s == 0))
                {
                    frames[i] = pages[i];
                }
                else if (s == 0)
                {
                    frames[(faults - 1) % no_of_frames] = pages[i];
                }
                pagesan += "Frame" + u + ":";
                for (int j = 0; j < no_of_frames; ++j)
                {
                    pagesan += frames[j];
                    if (j + 1 < no_of_frames)
                    {
                        pagesan += ",";
                    }
                }
                u++;
                pagesan += System.Environment.NewLine;
            }
            //MessageBox.Show(pagesan);
            richTextBox1.Text = pagesan;
            textBox4.Text = "" + faults;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="Enter"&&textBox2.Text!="Enter"&&textBox3.Text!="Enter")
            {
                int no_of_frames = Convert.ToInt32(textBox1.Text);
                int no_of_pages = Convert.ToInt32(textBox2.Text);
                string[] refrence = textBox3.Text.Split(',');
                int[] refrence_string = new int[no_of_pages];
                bool flagg = true;
                for (int i = 0; i < no_of_pages;i++)
                {
                    try
                    {
                        refrence_string[i] = Convert.ToInt32(refrence[i]);
                    }
                    catch
                    {
                        MessageBox.Show("NonInteger Number Found");
                        flagg = false;
                        break;
                    }
                }
                if (flagg == true)
                {
                    if (refrence_string.Length != no_of_pages)
                    {
                        MessageBox.Show("Error not same number");
                    }
                    else
                    {
                        if (radioButton1.Checked == true)
                        {
                            FIFO(no_of_frames, no_of_pages, refrence_string);
                        }
                        else if (radioButton2.Checked == true)
                        {
                            LRU(no_of_frames, no_of_pages, refrence_string);
                        }
                        else if (radioButton3.Checked == true)
                        {
                            optimal(no_of_frames, no_of_pages, refrence_string);
                        }
                        else
                        {
                            MessageBox.Show("Please Select an Algorithm To Work With");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Enter All The Data Correctly");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
